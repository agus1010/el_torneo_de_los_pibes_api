using Microsoft.AspNetCore.Mvc;

using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Services.Interfaces;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _service;

        public TeamsController(ITeamsService service)
        {
            _service = service;
        }


		[HttpGet("{id}/addPlayer")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> AddPlayer(int id, int playerId)
		{
			if (id <= 0 || playerId <= 0)
				return BadRequest();
			
			try
			{
				await _service.AddPlayer(id, playerId);
			}
			catch (Exception)
			{
				return BadRequest();
			}

			return NoContent();
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<PlayerDto>>> Get()
		{
			return Ok(await _service.GetAll());
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<TeamDto>> Get(int id)
		{
			if (id <= 0)
				return BadRequest();

			var teamDto = await _service.GetById(id);

			if (teamDto == null)
				return NotFound();

			return Ok(teamDto);
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<TeamDto>> Create(TeamCreationDto teamCreationDto)
		{
			if (teamCreationDto == null)
				return BadRequest();

			var newTeamDto = await _service.Create(teamCreationDto);

			return CreatedAtAction("Get", new { id = newTeamDto.Id }, newTeamDto);
		}


		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Update(int id, TeamDto teamDto)
		{
			if (id <= 0 || teamDto == null || id != teamDto.Id)
				return BadRequest();

			await _service.UpdateWith(teamDto);

			return NoContent();
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
				return BadRequest();

			await _service.DeleteWithId(id);
			return NoContent();
		}
	}
}
