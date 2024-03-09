using Microsoft.AspNetCore.Mvc;

using api.Models.Dtos.Match;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        [HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> CreateMatch(PlayersOnlyFriendlyMatchCreationDto matchCreationDto)
        {
            throw new NotImplementedException();
        }


		[HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> GetMatch(int id)
        {
            throw new NotImplementedException();
        }


        [HttpPut("{id}")]
        
        public async Task<ActionResult> EditMatch(int id, MatchUpdateDto matchUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}