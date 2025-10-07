using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Revenge.Data.Context;
using Revenge.Data.Models;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationRepository _authenticationRepository;
        private readonly RevengeDbContext _context;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //var existingUser = await _authenticationRepository.get;
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = registerUserDTO.Name,
                    Email = registerUserDTO.Email,
                    Password = registerUserDTO.Password, //Por hacer: Encriptar
                    Cellphone = registerUserDTO.Cellphone,
                    Birthdate = registerUserDTO.Birthdate,
                    Directions = registerUserDTO.Directions != null ? System.Text.Json.JsonSerializer.Serialize(registerUserDTO.Directions) : null,
                    NumIdentification = registerUserDTO.NumIdentification,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow

                };
                var result = await _authenticationRepository.AddUserAsync(user, cancellationToken);

                if (!result)
                    return StatusCode(500, "Error al registrar usuario");

                return CreatedAtAction(
                    nameof(Register),
                    new { id = user.Id },
                    new { message = "Usuario registrado exitosamente", userID = user.Id }
                );
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // funcionamiento parcial
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _authenticationRepository.LoginUserAsync(
                    email: loginUserDTO.Email,
                    plainPassword: loginUserDTO.Password,
                    cancellationToken: cancellationToken
                    );

                if (user is null)
                    return Unauthorized(new { mensaje = "Credenciales inválidas" });
                else
                {
                    return Ok(new
                    {
                        mensaje = "Login exitoso",
                        user = new { user.Id, user.Name, user.Email }
                    });
                }
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Solicitud cancelada por el cliente");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        //[HttpPost("auth0Login")]
        //public async Task<IActionResult> Auth0Login([FromBody] LoginUserDTO auth0Login, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return 0;
        //}
    }
}