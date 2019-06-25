using AutoMapper;
using DataTransferObjects.Game;
using Entities.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.MappingProfiles
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Game, GameDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.IsFinished, opt => opt.MapFrom(s => s.IsFinished))
                .ForMember(d => d.PlayerSign, opt => opt.MapFrom(s => s.PlayerSign))
                .ReverseMap();

            CreateMap<Move, MoveDto>()
                .ForMember(d => d.IsPlayer, opt => opt.MapFrom(s => s.IsPlayer))
                .ForMember(d => d.Position, opt => opt.MapFrom(s => (int)s.Position))
                .ForMember(d => d.Order, opt => opt.MapFrom(s => s.Order))
                .ReverseMap();
        }
    }
}
