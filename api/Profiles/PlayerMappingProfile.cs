using AutoMapper;

using api.Models.Entities;
using api.Models.Dtos.Player;


namespace api.Profiles
{
    public class PlayerMappingProfile : Profile
    {
        public PlayerMappingProfile()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Player, PlayerCreationDto>().ReverseMap();
            CreateMap<PlayerDto, PlayerCreationDto>().ReverseMap();
        }
    }
}