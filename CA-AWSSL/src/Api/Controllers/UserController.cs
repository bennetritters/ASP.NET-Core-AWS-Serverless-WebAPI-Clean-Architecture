using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namotion.Reflection;

namespace Api.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create(CreateInput input)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var command = new CreateUserCommand
                {
                    UserId = identity.FindFirst("sub").Value,
                    DisplayName = input.DisplayName
                };

                var result = await Mediator.Send(command);

                return Ok(result);
            }
            else return BadRequest();
        }
    }
    
    //Input Definitions
    public class CreateInput
    {
        public string DisplayName { get; set; }
    }
}