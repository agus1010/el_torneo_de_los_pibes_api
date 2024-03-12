using Microsoft.AspNetCore.Mvc;

using api.Models.Dtos.Match;
using api.Services;
using api.Services.Errors;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        protected readonly MatchesService _matchesService;
        public MatchesController(MatchesService matchesService)
        {
            _matchesService = matchesService;
        }


        [HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<MatchDto>> CreateMatch(PlayersOnlyFriendlyMatchCreationDto matchCreationDto)
        {
            if (matchCreationDto.Team1Dto == null || matchCreationDto.Team2Dto == null)
                return BadRequest();
            var createdMatch = await _matchesService.CreateMatch(matchCreationDto);
            return CreatedAtAction("GetMatch", new { id = createdMatch.Id }, createdMatch);
        }


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<MatchDto>> GetMatch(int id)
        {
            if (id <= 0)
                return BadRequest();
            var matchDto = await _matchesService.GetAsync(id);
            if (matchDto == null)
                return NotFound();
            return Ok(matchDto);
        }


        [HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> EditMatch(int id, MatchUpdateDto matchUpdateDto)
        {
			if (id <= 0)
				return BadRequest();
			try
			{
				await _matchesService.UpdateAsync(id, matchUpdateDto);
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
			return NoContent();
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> DeleteMatch(int id, MatchUpdateDto matchUpdateDto)
		{
			if (id <= 0)
				return BadRequest();
			try
			{
				await _matchesService.DeleteAsync(id);
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}