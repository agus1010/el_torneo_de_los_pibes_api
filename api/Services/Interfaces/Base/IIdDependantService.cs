namespace api.Services.Interfaces.Base
{
    public interface IIdDependantService<TDto, TCreationDto> : IBaseService<TDto, TCreationDto>
        where TDto : class
        where TCreationDto : class
    {
        Task DeleteWithId(int id);
        Task<TDto?> GetById(int id);
    }
}