using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Errors;
using api.Services.Interfaces;


namespace api.Services
{
    public class PlayersService : IPlayersService
	{
		protected readonly IPlayersRepository playersRepo;
		protected readonly IMapper mapper;

		public PlayersService(IPlayersRepository playersRepository, IMapper mapper)
		{
			playersRepo = playersRepository;
			this.mapper = mapper;
		}


		public virtual async Task<PlayerDto?> GetAsync(int id)
		{
			var player = await playersRepo.GetAsync(id);
			PlayerDto? playerDto = null;
			if (player != null)
				playerDto = mapper.Map<PlayerDto>(player);
			return playerDto;
		}

		public virtual async Task<IEnumerable<PlayerDto>> GetAsync(IEnumerable<int> ids)
		{
			var players = await playersRepo.GetAsync(ids);
			return players.Select(p => mapper.Map<PlayerDto>(p));
		}


		public virtual async Task<PlayerDto> CreateAsync(PlayerCreationDto playerCreationDto)
		{
			var player = mapper.Map<Player>(playerCreationDto);
			player = await playersRepo.CreateAsync(player);
			return mapper.Map<PlayerDto>(player);
		}


		public virtual async Task UpdateAsync(PlayerDto playerDto)
		{
			var player = await findPlayerEntityOrThrowError(playerDto.Id);
			player = mapper.Map<Player>(playerDto);
			await playersRepo.UpdateAsync(player);
		}


		public virtual async Task DeleteAsync(int id)
		{
			var player = await findPlayerEntityOrThrowError(id);
			await playersRepo.DeleteAsync(player);
		}



		protected async Task<Player?> findPlayerEntity(int id)
			=> await playersRepo.GetAsync(id);

		protected async Task<Player> findPlayerEntityOrThrowError(int id)
		{
			var player = await findPlayerEntity(id);
			if (player == null)
				throw new EntityNotFoundException();
			return player;
		}
	}
}
