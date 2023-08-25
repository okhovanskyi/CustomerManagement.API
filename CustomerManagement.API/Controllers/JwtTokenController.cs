using CustomerManagement.API.Persistence;
using CustomerManagement.API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CustomerManagement.API.Controllers
{
    /// <summary>
    /// ONLY FOR TESTING!
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ExcludeFromCodeCoverage]
    public class JwtTokenController : ControllerBase
    {
        private readonly UserCollection _userCollection;

        public JwtTokenController(UserCollection userCollection) 
        {
            _userCollection = userCollection;
        }

        // GET: api/<JwtTokenController>
        [HttpGet(Name = "GenerateJwtTokenForNewUser")]
        public IResult Get(string name, string surname) 
        {
            var newUserGuid = Guid.NewGuid();

            if (!_userCollection.AddUser(newUserGuid, name, surname))
            {
                return Results.Conflict();
            }

            var token = JwtTokenUtility.GenerateCustomJwtToken(newUserGuid);

            return Results.Ok(new { newUserGuid, token });
        }
    }
}
