using AutoMapper;

using api.Models.Dtos.Team;
using api.Models.Entities;


namespace api.Profiles
{
	public class TeamMappingProfile : Profile
	{
		public TeamMappingProfile()
		{
			CreateMap<Team, TeamDto>().ReverseMap();
			CreateMap<Team, TeamCreationDto>().ReverseMap();
			CreateMap<TeamDto, TeamCreationDto>().ReverseMap();
			CreateMap<TeamDto, TeamUpdateDto>();
		}
	}
}
