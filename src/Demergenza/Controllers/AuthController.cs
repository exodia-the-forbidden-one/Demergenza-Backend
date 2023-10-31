using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demergenza.Application.Abstractions.Repositories;
using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Application.Helpers.Authentication;
using Demergenza.Domain.Entities.Admin;
using Demergenza.Domain.Entities.Menu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demergenza.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAdminReadRepository _adminRead;
        private readonly TokenHelper _tokenHelper;
        public AuthController(IAdminReadRepository adminRead, TokenHelper tokenHelper)
        {
            _adminRead = adminRead;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginObject)
        {
            Admin admin = await _adminRead.GetFirstAsync(admin => admin.Username == loginObject.Username && admin.Password == loginObject.Password);

            if (admin != null)
            {
                return Ok(new
                {
                    Message = "Success",
                    Token = _tokenHelper.CreateToken(admin)
                });
            }
            else
            {
                return BadRequest(new
                {
                    error = "Hatalı giriş"
                });
            }

        }
    }
}