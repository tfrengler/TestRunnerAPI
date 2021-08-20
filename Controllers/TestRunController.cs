using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestRunner.Controllers
{
    [ApiController]
    [Route("TestRun")]
    public class TestRunController : ControllerBase
    {
        private readonly ILogger<TestRunController> Logger;

        public TestRunController(ILogger<TestRunController> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Starts a test run
        /// </summary>
        /// <param name="Scenarios">A semi-colon separated list of test scenario names</param>
        /// <returns>A session id belonging to the test run that was started</returns>
        [HttpGet("Start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StartResponseObject))]
        [Produces("application/json")]
        public ActionResult<StartResponseObject> Start([FromQuery] string Scenarios)
        {
            Logger.LogInformation($"/TestRunner/Start - {(HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress : "UNKNOWN")}");

            if (string.IsNullOrEmpty(Scenarios))
            {
                var Response = new StartResponseObject();
                Response.Errors.Add("Param 'scenarios' is missing or empty");
                Logger.LogError($"/TestRunner/Start - Param 'scenarios' is missing or empty");

                return new BadRequestObjectResult(Response);
            }

            int ExpectedScenarios = Scenarios.Count(character => character == ';') + 1;
            List<string> ChosenScenarios = Scenarios.Split(';', ExpectedScenarios, StringSplitOptions.TrimEntries).ToList();

            Logger.LogInformation($"Scenarios ({ExpectedScenarios}): " + Scenarios);

            return new OkObjectResult(new StartResponseObject()
            {
                SessionID = Guid.NewGuid().ToString()
            });
        }

        /// <summary>
        /// Used to query the status of a given test run
        /// </summary>
        /// <param name="SessionID">The ID of the session whose status you wish to query</param>
        /// <returns>A session id belonging to the test run that was started</returns>
        [HttpGet("Status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StatusResponseObject))]
        [Produces("application/json")]
        public ActionResult<StatusResponseObject> Status([FromQuery] string SessionID)
        {
            Logger.LogInformation($"/TestRunner/Status - {(HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress : "UNKNOWN")}");

            if (string.IsNullOrEmpty(SessionID))
            {
                Logger.LogError($"/TestRunner/Start - Param 'scenarios' is missing or empty");
                return new BadRequestObjectResult(new StatusResponseObject()
                {
                    Errors = new List<string>() { $"Param '{nameof(SessionID)}' is missing or empty" }
                });
            }

            return new StatusResponseObject()
            {
                Status = $"{SessionID} is doing great!"
            };
        }
    }
}
