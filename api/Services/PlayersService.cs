using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Entities;
using api.Repositories.Interfaces;


namespace api.Services
{
	public class PlayersService
	{
		protected readonly IBaseCRUDRepository<Player> _repo;
		protected readonly IMapper _mapper;


		public PlayersService(IBaseCRUDRepository<Player> playersRepository, IMapper mapper)
		{
			_repo = playersRepository;
			_mapper = mapper;
		}


		public async Task<PlayerDto> Create(PlayerCreationDto playerCreationDto)
		{
			Player persistedPlayer = await _repo.Create(_mapper.Map<Player>(playerCreationDto));
			PlayerDto newPlayer = _mapper.Map<PlayerDto>(persistedPlayer);
			return newPlayer;
		}


		public async Task<PlayerDto?> GetById(int id)
		{
			Player target = await getWithId(id);
			return _mapper.Map<PlayerDto>(target);
		}


		public async Task<IEnumerable<PlayerDto>> GetAll()
		{
			return _mapper.Map<IEnumerable<PlayerDto>>(await _repo.ReadMany());
		}


		public async Task DeleteWithId(int id)
		{
			Player target = await getWithId(id);
			await _repo.Delete(target);
		}


		public async Task UpdateWith(PlayerDto playerDto)
		{
			await _repo.Update(_mapper.Map<Player>(playerDto));
		}



		private async Task<Player> getWithId(int id)
		{
			Player? player = await _repo.ReadSingle(filter: p => p.Id == id, tracked: false);
			if (player == null)
				throw new Exception();
			return player!;
		}
	}
}


		/*public async Task<PlayerDto> Create(PlayerDto playerDto)
		{
			var newPlayer = await _repo.Create(_mapper.Map<Player>(playerDto));
			return _mapper.Map<PlayerDto>(newPlayer);
		}

		public async Task<PlayerDto> Create(PlayerCreationDto playerCreationDto)
		{
			return await Create(_mapper.Map<PlayerDto>(playerCreationDto));
		}


		public async Task Delete(int id)
		{
			await Delete(await Get(id));
		}

		public async Task Delete(PlayerDto? playerDto)
		{
			if (playerDto == null)
				throw new Exception();
			var player = _mapper.Map<Player>(playerDto!);
			await _repo.Delete(player);
		}


		public async Task<IEnumerable<PlayerDto>> Get()
		{
			return (await _repo.ReadMany()).Select(p => _mapper.Map<PlayerDto>(p));
		}


		public async Task<PlayerDto?> Get(int id)
		{
			var player = await _repo.ReadSingle(p => p.Id == id);
			if (player == null)
				return null;
			return _mapper.Map<PlayerDto>(player);
		}

		
		public async Task Update(PlayerDto playerDto)
		{
			await _repo.Update(_mapper.Map<Player>(playerDto));
		}
	}
}*/