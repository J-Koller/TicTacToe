using AutoMapper;
using System.Drawing;
using TicTacToe.Data.Entities;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Logic.Profiles
{
    public class MoveProfile : Profile
    {
        public MoveProfile()
        {
            CreateMap<Move, MoveDto>()
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => new Point(src.PositionX, src.PositionY)))
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol.Value))
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId));

            CreateMap<NewMoveDto, Move>()
               .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId))
               .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
               .ForMember(dest => dest.PositionX, opt => opt.MapFrom(src => src.PositionX))
               .ForMember(dest => dest.PositionY, opt => opt.MapFrom(src => src.PositionY));
        }
    }
}
