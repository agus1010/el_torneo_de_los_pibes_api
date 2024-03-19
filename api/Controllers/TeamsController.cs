using Microsoft.AspNetCore.Mvc;

using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Services.Interfaces;
using api.Services.Errors;



namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        protected readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }


        [HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public virtual async Task<ActionResult<TeamDto>> CreateTeam(TeamCreationDto teamCreationDto)
        {
            if (teamCreationDto == null)
                return BadRequest();
            var newTeamDto = await teamsService.CreateTeam(teamCreationDto);
            return CreatedAtAction("GetTeam", new { id = newTeamDto.Id }, newTeamDto);
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public virtual async Task<ActionResult<TeamDto>> GetTeam(int id, bool includePlayers = false)
        {
            if (id <= 0)
                return BadRequest();
            var teamDto = await teamsService.GetTeam(id, includePlayers);
            if (teamDto == null)
                return NotFound();
            return Ok(teamDto);
        }


        [HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async virtual Task<ActionResult> EditTeam(int id, [FromBody] TeamUpdateDto teamUpdateDto, bool patchPlayers = false)
        {
            if (id <= 0 || teamUpdateDto == null || teamUpdateDto.Id != id || (patchPlayers && teamUpdateDto.PlayerIds == null))
                return BadRequest();
            try
            {
                await teamsService.EditTeam(teamUpdateDto, patchPlayers);
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
		public async virtual Task<ActionResult> DeleteTeam(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                await teamsService.DeleteTeam(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }



        [HttpGet("{teamId}/Players")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async virtual Task<ActionResult<ISet<PlayerDto>>> GetTeamPlayers(int teamId)
        {
            if (teamId <= 0)
                return BadRequest();
            ISet<PlayerDto> players;
            try
            {
                players = await teamsService.GetPlayers(teamId);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            return Ok(players);
        }
        


        [HttpPut("{teamId}/Players")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async virtual Task<ActionResult> EditTeamPlayers(int teamId, [FromBody] TeamPlayersEditDto teamPlayersEditDto)
        {
            if (teamId <= 0 || teamPlayersEditDto == null)
                return BadRequest();
            try
            {
                await teamsService.EditPlayers(teamId, teamPlayersEditDto);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            return NoContent();
        }
	}
}
