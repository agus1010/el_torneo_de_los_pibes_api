using AutoMapper;

using api.Models.Entities;
using api.Models.Dtos.Player;
using api.Models.Dtos.Team;


namespace api.Profiles
{
    public class PlayerMappingProfile : Profile
    {
        public PlayerMappingProfile()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Player, PlayerCreationDto>().ReverseMap();
            CreateMap<PlayerDto, PlayerCreationDto>().ReverseMap();

			CreateMap<Team, TeamDto>().ReverseMap();
			CreateMap<Team, TeamCreationDto>().ReverseMap();
			CreateMap<TeamDto, TeamCreationDto>().ReverseMap();
			CreateMap<TeamDto, TeamUpdateDto>().ReverseMap();
		}
    }
}