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


        [HttpGet("{id}")]
		public virtual async Task<ActionResult<PlayerDto>> GetPlayer(int id)
        {
            if (id <= 0)
                return BadRequest();
            
            var playerDto = await playersService.GetAsync(id);
            if (playerDto == null)
                return NotFound();
            
            return Ok(playerDto);
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutPlayer(int id, PlayerDto playerDto)
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

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public virtual async Task<ActionResult<PlayerDto>> PostPlayer(PlayerCreationDto playerCreationDto)
        {
            if (playerCreationDto == null)
                return BadRequest();
            var newPlayerDto = await playersService.CreateAsync(playerCreationDto);
            return CreatedAtAction("GetPlayer", new { id = newPlayerDto.Id }, newPlayerDto);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
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
