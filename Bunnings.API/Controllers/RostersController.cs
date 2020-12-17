using Bunnings.API.Filters;
using Bunnings.Domain.Entities;
using Bunnings.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bunnings.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RostersController : ControllerBase
    {
        private readonly IRosterService rosterService;
        public RostersController(IRosterService rosterService)
        {
            this.rosterService = rosterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRosters([FromQuery(Name = "from")] DateTime? from, [FromQuery(Name = "to")] DateTime? to)
        {
            // Validate the incoming query paramters
            if (from == null || to == null)
            {
                return NoContent();
            }

            return Ok(await rosterService.GetRosters(from.GetValueOrDefault(), to.GetValueOrDefault()));
        }

        /// <summary>
        /// Save Roster (Anonymous).
        /// Validations:
        /// 1. Model validation - The model has been annotated with data attribues. Therefore, all the incoming requests are automatically validated against the model because of the ControllerBase
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SaveRoster(Roster roster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await rosterService.SaveRoster(roster);
            return NoContent();
        }

        /// <summary>
        /// Save Roster (Protected)
        /// Validation Notes:
        /// 1. Model validation - The model has been annotated with data attribues. Therefore, all the incoming requests are automatically validated against the model because of the ControllerBase
        /// 2. [ValidApiKey] Filter - Checking the incoming request for the valid api key.
        /// </summary>
        [HttpPost("/private/rosters")]
        [ValidApiKey]
        //[Roles("Managers")]
        public async Task<IActionResult> SaveRosterAuthorised(Roster roster)
        {
            await rosterService.SaveRoster(roster);
            return NoContent();
        }
    }
}
