using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var usetToLogin = _authService.Login(userForLoginDto);
            if(!usetToLogin.Succsess)
            {
                return BadRequest(usetToLogin);
            }

            var result = _authService.CreateAccsessToken(usetToLogin.Data);
            if (result.Succsess)
                return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRigsterDto userForRigster)
        {
            var usetToLogin = _authService.UserExists(userForRigster.Email);
            if (!usetToLogin.Succsess)
            {
                return BadRequest(usetToLogin);
            }


            var resultRegister = _authService.Register(userForRigster, userForRigster.Password);
            var result = _authService.CreateAccsessToken(resultRegister.Data);
            if (result.Succsess)
                return Ok(result.Data);

            return BadRequest(result);
        }
    }
}
