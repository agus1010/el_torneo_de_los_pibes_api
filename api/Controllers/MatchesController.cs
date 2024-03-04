using Microsoft.AspNetCore.Mvc;

using api.Models.Dtos.Match;
using api.Services;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly MatchesService _service;


        public MatchesController(MatchesService matchesService)
        {
            _service = matchesService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> GetMatch(int id)
        {
            if (id <= 0)
                return BadRequest();
            var matchDto = await _service.GetById(id);
            if (matchDto == null)
                return NotFound();
            return Ok(matchDto);
        }


        [HttpPost]
        public async Task<ActionResult<MatchDto>> CreatePlayersFriendlyMatch(PlayersOnlyFriendlyMatchCreationDto? creationDto)
        {
			if (creationDto == null)
				return BadRequest();
			if (creationDto.Team1Dto == null || creationDto.Team2Dto == null)
				return BadRequest();
			if (creationDto.Team1Dto.PlayerIds.Count == 0 || creationDto.Team2Dto.PlayerIds.Count == 0)
				return BadRequest();
			var newMatch = await _service.Create(creationDto);
            return CreatedAtRoute("GetMatch", new { id = newMatch.Id }, newMatch);
        }
    }
}