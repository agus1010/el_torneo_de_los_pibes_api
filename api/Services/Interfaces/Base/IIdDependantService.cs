using api.Models.Dtos.Player;

namespace api.Services.Interfaces.Base
{
    public interface IIdDependantService<TDto, TCreationDto> : IBaseService<TDto>
        where TDto : class
        where TCreationDto : class
    {
        Task DeleteWithId(int id);
        Task<TDto?> GetById(int id);
		public Task<IEnumerable<TDto>> GetById(IEnumerable<int> ids);
	}
}