namespace api.Services.Interfaces.Base
{
    public interface IBaseService<TDto, TCreationDto>
        where TDto : class
        where TCreationDto : class
    {
        Task<TDto> Create(TCreationDto creationDto);
        Task Delete(TDto dto);
        Task UpdateWith(TDto teamDto);
    }
}