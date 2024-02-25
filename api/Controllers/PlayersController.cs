using Microsoft.AspNetCore.Mvc;

using api.Models.Dtos.Player;
using api.Services.Interfaces;


namespace api.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _service;

        public PlayersController(IPlayersService playerService)
        {
            _service = playerService;
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
		public async Task<ActionResult<PlayerDto>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();
            
            var playerDto = await _service.GetById(id);

            if (playerDto == null)
                return NotFound();

            return Ok(playerDto);
        }


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<PlayerDto>> Create(PlayerCreationDto playerCreationDto)
		{
			if (playerCreationDto == null)
				return BadRequest();

			var newPlayerDto = await _service.Create(playerCreationDto);

			return CreatedAtAction("Get", new { id = newPlayerDto.Id }, newPlayerDto);
		}


		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Update(int id, PlayerDto playerDto)
        {
            if (id <= 0 || playerDto == null || id != playerDto.Id)
                return BadRequest();

            await _service.UpdateWith(playerDto);

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
