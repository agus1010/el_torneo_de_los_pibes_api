namespace api.Services.Interfaces.Base
{
    public interface IBaseService<TDto>
        where TDto : class
    {
        Task<TDto> Create(TDto dto);
        Task Delete(TDto dto);
        Task UpdateWith(TDto teamDto);
    }
}