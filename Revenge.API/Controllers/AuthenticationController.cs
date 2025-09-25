using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NuGet.Protocol;
using Revenge.Data.Context;
using Revenge.Data.Models;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System.Runtime.InteropServices;

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


        //endpoint temporal para probar la conexion 
        [HttpGet]
        public async Task<bool> Test(CancellationToken cancellationToken)
        {
            try
            {
                await _authenticationRepository.LoginUserAsync("", "", cancellationToken);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
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

        // aun no funciona
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                // Buscar usuario por email
                var usuario = await _context.Users
                    .FirstOrDefaultAsync(c => c.Email == loginUserDTO.Email, cancellationToken);

                if (usuario == null)
                    return Unauthorized(new { mensaje = "Usuario no encontrado" });

                // Verificar contraseña
                // necesito primero que se hasheen las contraseñas para yo saber que metodo tamo usando xd
                //var result = _passwordHasher.VerifyHashedPassword( 
                //    loginUserDTO.Email,
                //    usuario.Password, // aquí debe estar guardada la contraseña hasheada
                //    loginUserDTO.Password
                //);

                if (result == PasswordVerificationResult.Failed)
                    return Unauthorized(new { mensaje = "Contraseña incorrecta" });
            }
            
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }

        }
    }
}