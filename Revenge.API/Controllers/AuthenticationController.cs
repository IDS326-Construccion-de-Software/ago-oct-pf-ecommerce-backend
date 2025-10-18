using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using Auth0.ManagementApi;
using System.Text.Json;
using Auth0.ManagementApi.Models;
using Revenge.Core.Models;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationRepository _authenticationRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public AuthenticationController(
            IAuthenticationRepository authenticationRepository,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var token = await GetAuth0TokenAsync();
                var domain = _configuration["Auth0:Domain"];//En appsettings
                var connectionName = _configuration["Auth0:Connection"];//En appsettings

                //Crear usuario en Auth0
                var client = new ManagementApiClient(token, domain);
                var userRequest = new UserCreateRequest
                {
                    Email = registerUserDTO.Email,
                    Password = registerUserDTO.Password, //Auth0 Encripta
                    Connection = connectionName,
                    EmailVerified = false,
                    VerifyEmail = true,
                    UserMetadata = new
                    {
                        full_name = registerUserDTO.Name,
                        cellphone = registerUserDTO.Cellphone,
                        birthdate = registerUserDTO.Birthdate?.ToString("yyyy-MM-dd"),
                        //Directions = registerUserDTO.Directions != null ? System.Text.Json.JsonSerializer.Serialize(registerUserDTO.Directions) : null,
                        numIdentification = registerUserDTO.NumIdentification
                    }
                };
                var auth0User = await client.Users.CreateAsync(userRequest);

                if (auth0User != null)
                {
                    await _authenticationRepository.AddUserAsync(new Infrestructure.Entities.User
                    {
                        Id = new Guid(),
                        Email = registerUserDTO.Email,
                        Name = registerUserDTO.Name,
                        Cellphone = registerUserDTO.Cellphone,
                        CreatedAt = DateTime.UtcNow,
                        Password = auth0User.Identities[0].UserId.ToString(),

                    }, cancellationToken);
                }

                return CreatedAtAction(
                    nameof(Register),
                    new { id = auth0User.UserId },
                    new
                    {
                        message = "Usuario registrado exitosamente. Por favor, verifica tu email para activar tu cuenta.",
                        auth0UserId = auth0User.UserId,
                        email = auth0User.Email,
                        emailVerified = auth0User.EmailVerified,
                        createdAt = auth0User.CreatedAt
                    }
                );
            }
            catch (Auth0.Core.Exceptions.ApiException ex)
            {
                if (ex.Message.Contains("user already exists") || ex.Message.Contains("already exist"))
                    return Conflict(new { message = "El email ya está registrado" });

                if (ex.Message.Contains("PasswordStrengthError") || ex.Message.Contains("password"))
                    return BadRequest(new { message = "La contraseña no cumple con los requisitos de seguridad. Debe tener al menos 8 caracteres." });

                return StatusCode(500, new { message = $"Error en Auth0: {ex.Message}" });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, new { message = "Solicitud cancelada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno: {ex.Message}" });
            }
        }
        [HttpGet("test-auth0")]
        public async Task<ActionResult> TestAuth0Connection()
        {
            try
            {
                var token = await GetAuth0TokenAsync();
                var domain = _configuration["Auth0:Domain"];

                return Ok(new
                {
                    message = "Conexión exitosa con Auth0",
                    hasToken = !string.IsNullOrEmpty(token),
                    domain = domain,
                    tokenPreview = token?.Substring(0, Math.Min(20, token.Length)) + "..."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error de conexión: {ex.Message}" });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _authenticationRepository.LoginUserAsync(
                    email: loginUserDTO.Email,
                    plainPassword: loginUserDTO.Password,
                    cancellationToken: cancellationToken);

                if (result is null)
                    return Unauthorized(new { message = "Credenciales inválidas o usuario no verificado." });

                return Ok(new
                {
                    message = "Login exitoso",
                    tokens = new
                    {
                        access_token = result.AccessToken,
                        id_token = result.IdToken,
                        token_type = result.TokenType,
                        expires_in = result.ExpiresIn
                    }
                });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, new { message = "Solicitud cancelada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno: {ex.Message}" });
            }
        }
        private async Task<string> GetAuth0TokenAsync()
        {
            var domain = _configuration["Auth0:Domain"];//Desde el appsettings
            var clientId = _configuration["Auth0:ClientId"];//Desde el appsettings
            var clientSecret = _configuration["Auth0:ClientSecret"];//Desde el appsettings

            var tokenRequest = new
            {
                client_id = clientId,
                client_secret = clientSecret,
                audience = $"https://{domain}/api/v2/",
                grant_type = "client_credentials"
            };

            var response = await _httpClient.PostAsJsonAsync($"https://{domain}/oauth/token", tokenRequest);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error obteniendo token de Auth0");

            var tokenData = await response.Content.ReadFromJsonAsync<JsonElement>();
            return tokenData.GetProperty("access_token").GetString()!;
        }
    }
}