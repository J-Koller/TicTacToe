using AutoMapper;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;

namespace TicTacToe.Api.Logic.Profiles
{
    public class PlayerCredentialsProfile : Profile
    {
        public PlayerCredentialsProfile()
        {
            CreateMap<PlayerCredentialsDto, Player>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}
