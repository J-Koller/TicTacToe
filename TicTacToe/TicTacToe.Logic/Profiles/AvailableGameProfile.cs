using AutoMapper;
using System.Linq;
using TicTacToe.Data.Entities;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Logic.Profiles
{
    public class AvailableGameProfile : Profile
    {
        public AvailableGameProfile()
        {
            CreateMap<Game, AvailableGameDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Players.Single().Id));
        }
    }
}
