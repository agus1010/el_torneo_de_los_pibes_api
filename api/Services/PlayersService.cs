using api.General;
using api.Models.Dtos;
using api.Repositories;


namespace api.Services
{
	public class PlayersService : IAsyncCRUD<PlayerDto>
	{
		protected readonly PlayersRepository _repo;



		public PlayersService(PlayersRepository playersRepository)
		{
			_repo = playersRepository;
		}



		public Task<PlayerDto> Create(PlayerDto entity)
		{
			throw new NotImplementedException();
		}


		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}
		public Task Delete(PlayerDto entity)
		{
			throw new NotImplementedException();
		}

		
		public Task<IEnumerable<PlayerDto>> Get()
		{
			throw new NotImplementedException();
		}


		public Task<PlayerDto?> Get(int id)
		{
			throw new NotImplementedException();
		}

		
		public Task Update(int id, PlayerDto entity)
		{
			throw new NotImplementedException();
		}
	}
}