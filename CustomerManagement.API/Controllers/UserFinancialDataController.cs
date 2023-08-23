using Microsoft.AspNetCore.Mvc;
using CustomerManagement.API.Command.Commands;
using CustomerManagement.API.Command.Interfaces;
using CustomerManagement.API.Command;
using CustomerManagement.API.Query.Interfaces;
using CustomerManagement.API.Query.Queries;
using CustomerManagement.API.Query.QueryResults;
using Microsoft.AspNetCore.Authorization;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserFinancialDataController : ControllerBase
    {
        private readonly ICommandHandler<OpenNewAccountForUserCommand, CommandResult> _commandHandler;
        private readonly IQueryHandler<GetUserFinancialDataQuery, GetUserFinancialDataQueryResult> _queryHandler;

        public UserFinancialDataController(
            ICommandHandler<OpenNewAccountForUserCommand, CommandResult> commandHandler,
            IQueryHandler<GetUserFinancialDataQuery, GetUserFinancialDataQueryResult> queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        // GET: api/<UserFinancialDataController>
        [HttpGet(Name = "GetUserFinancialData")]
        public async Task<GetUserFinancialDataQueryResult> GetAsync()
        {
            if (!Guid.TryParse(HttpContext.User.FindFirst("CustomerUid")?.Value, out Guid customerUid))
            {
                return new GetUserFinancialDataQueryResult 
                {
                    HttpStatusCode = System.Net.HttpStatusCode.Unauthorized
                };
            }

            return await _queryHandler.HandleAsync(new GetUserFinancialDataQuery
            {
                CustomerUid = customerUid
            });
        }

        // POST api/<UserFinancialDataController>
        [HttpPost(Name = "OpenNewAccountForExistingUser")]
        public async Task<CommandResult> Post(Guid customerUid, long initialCredit)
        {
            return await _commandHandler.HandleAsync(new OpenNewAccountForUserCommand
            {
                CustomerUid = customerUid,
                InitialCredit = initialCredit
            });
        }

        // POST api/<UserFinancialDataController>
        [HttpPost("currentUser", Name = "OpenNewAccountForCurrentUser")]
        public async Task<CommandResult> Post(long initialCredit)
        {
            if (!Guid.TryParse(HttpContext.User.FindFirst("CustomerUid")?.Value, out Guid customerUid))
            {
                return new CommandResult
                {
                    HttpStatusCode = System.Net.HttpStatusCode.Unauthorized
                };
            }

            return await _commandHandler.HandleAsync(new OpenNewAccountForUserCommand
            {
                CustomerUid = customerUid,
                InitialCredit = initialCredit
            });
        }
    }
}
