using AutoMapper;
using System.Linq;
using TicTacToe.Data.Entities;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
               .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank))
               .ForMember(dest => dest.GamesWon, opt => opt.MapFrom(src => src.Games.Where(g => g.WinnerId == src.Id).Count()))
               .ForMember(dest => dest.GamesLost, opt => opt.MapFrom(src => src.Games.Where(g => g.LoserId == src.Id).Count()))
               .ForMember(dest => dest.GamesTied, opt => opt.MapFrom(src => src.Games.Where(g => g.IsTied).Count()));
        }
    }
}
