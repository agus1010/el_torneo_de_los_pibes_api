using AutoMapper;

using api.Models.Entities;
using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Models.Dtos.Match;
using api.Models.Dtos.Tournament;


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
			CreateMap<Team, TeamUpdateDto>().ReverseMap();
			CreateMap<TeamDto, TeamCreationDto>().ReverseMap();
			CreateMap<TeamDto, TeamUpdateDto>().ReverseMap();

            CreateMap<Match, MatchDto>().ReverseMap();
            CreateMap<Match, MatchCreationDto>().ReverseMap();
            CreateMap<Match, PlayersOnlyFriendlyMatchCreationDto>().ReverseMap();
            CreateMap<MatchCreationDto, PlayersOnlyFriendlyMatchCreationDto>().ReverseMap();

            CreateMap<Tournament, TournamentDto>().ReverseMap();
		}
    }
}