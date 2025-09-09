using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Revenge.Infrestructure.Repositories;
using Revenge.Data.Models;
using Revenge.Infrestructure.Entities;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationRepository _authenticationRepository;

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
                    id = Guid.NewGuid(),
                    name = registerUserDTO.Name,
                    email = registerUserDTO.Email,
                    password = registerUserDTO.Password, //Por hacer: Encriptar
                    cellphone = registerUserDTO.Cellphone,
                    birthdate = registerUserDTO.Birthdate,
                    directions = registerUserDTO.Directions!= null ? System.Text.Json.JsonSerializer.Serialize(registerUserDTO.Directions) : null,
                    numIdentification = registerUserDTO.NumIdentification,
                    createdAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow

                };
                var result = await _authenticationRepository.AddUserAsync(user, cancellationToken);

                if (!result)
                    return StatusCode(500, "Error al registrar usuario");

                return CreatedAtAction(
                    nameof(Register),
                    new { id = user.id },
                    new { message = "Usuario registrado exitosamente", userID = user.id }
                );
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }

        }
    }
}
