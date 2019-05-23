using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dvt.webapp.AppCode;
using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using wtw.webapp.Helpers;


namespace wtw.webapp.Controllers
{
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IConfiguration configuration, IMediator mediator) : base(configuration, mediator) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await Mediator.Send(new UserLoginQueryRequest
            {
                TransferObject = loginViewModel
            });

            if (response.Status != EnumOperationResult.Ok || response.Entity.IsNull()) return Unauthorized();

            var user = response.Entity.Result;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginViewModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimType.UserId,user.UserAccountId.ToString()),
                new Claim(ClaimType.Email,user.Email),
                new Claim(ClaimType.SystemFunctions,string.Join(";", user.SystemFunctions.Select(x => x.ToString()).ToArray()))
            };
            var days = loginViewModel.RememberMe ? 10 : 1;
            var token = new JwtSecurityToken
            (
                Configuration["Issuer"],
                Configuration["Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(days),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"])),
                                                           SecurityAlgorithms.HmacSha256)
            );

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}
