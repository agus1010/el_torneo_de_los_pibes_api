using AutoMapper;

using api.General;
using api.Models.Dtos.Player;
using api.Models.Entities;
using api.Repositories.Interfaces;


namespace api.Services
{
    public class PlayersService : IAsyncCRUD<PlayerDto>
	{
		protected readonly IBaseCRUDRepository<Player> _repo;
		protected readonly IMapper _mapper;


		public PlayersService(IBaseCRUDRepository<Player> playersRepository, IMapper mapper)
		{
			_repo = playersRepository;
			_mapper = mapper;
		}


		public async Task<PlayerDto> Create(PlayerDto playerDto)
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
}