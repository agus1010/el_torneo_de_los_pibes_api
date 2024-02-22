using AutoMapper;

using api.Models.Entities;
using api.Models.Dtos.Player;


namespace api.Models.Mappers
{
    public class PlayerMapper : Profile
	{
		public PlayerMapper() 
		{
			CreateMap<Player, PlayerDto>().ReverseMap();
			CreateMap<Player, PlayerCreationDto>().ReverseMap();
			CreateMap<PlayerDto, PlayerCreationDto>().ReverseMap();
		}
	}
}