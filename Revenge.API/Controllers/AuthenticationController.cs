using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Revenge.Infrestructure.Repositories;

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

    }
}
