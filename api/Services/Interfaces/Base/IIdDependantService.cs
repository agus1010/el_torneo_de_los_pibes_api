namespace api.Services.Interfaces.Base
{
    public interface IIdDependantService<TDto, TCreationDto> : IBaseService<TDto>
        where TDto : class
        where TCreationDto : class
    {
        Task DeleteWithId(int id);
        Task<TDto?> GetById(int id);
		Task<IEnumerable<TDto>> GetById(IEnumerable<int> ids);
	}
}