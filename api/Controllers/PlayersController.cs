using Microsoft.AspNetCore.Mvc;

using api.Models.Dtos.Player;
using api.Services.Errors;
using api.Services.Interfaces;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        protected readonly IPlayersService playersService;

        public PlayersController(IPlayersService playersService)
        {
            this.playersService = playersService;
        }


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public virtual async Task<ActionResult<PlayerDto>> CreatePlayer(PlayerCreationDto playerCreationDto)
		{
			if (playerCreationDto == null)
				return BadRequest();
			var newPlayerDto = await playersService.CreateAsync(playerCreationDto);
			return CreatedAtAction("GetPlayer", new { id = newPlayerDto.Id }, newPlayerDto);
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public virtual async Task<ActionResult<PlayerDto>> GetPlayer(int id)
        {
            if (id <= 0)
                return BadRequest();
            
            var playerDto = await playersService.GetAsync(id);
            if (playerDto == null)
                return NotFound();
            
            return Ok(playerDto);
        }


		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public virtual async Task<IActionResult> EditPlayer(int id, PlayerDto playerDto)
        {
            if (id <= 0 || playerDto == null || playerDto.Id <= 0 || id != playerDto.Id)
                return BadRequest();
            try
            {
                await playersService.UpdateAsync(playerDto);
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
		public virtual async Task<IActionResult> DeletePlayer(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                await playersService.DeleteAsync(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
