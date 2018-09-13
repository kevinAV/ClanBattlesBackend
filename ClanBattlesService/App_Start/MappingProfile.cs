using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClanBattlesService.Models;
using ClanBattlesService.Dtos;
using ClanBattlesService.Dtos.Info;

namespace ClanBattlesService.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>()
                .ForMember(m => m.id, opt => opt.Ignore());
            CreateMap<Game, GameInfo>();
            CreateMap<Clan, ClanDto>();

            CreateMap<ClanDto, Clan>()
                .ForMember(m => m.id, opt => opt.Ignore());
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>()
                .ForMember(m => m.id, opt => opt.Ignore());
            CreateMap<Member, MemberInfo>();
            CreateMap<Gamer, GamerDto>();
            CreateMap<GamerDto, Gamer>()
                .ForMember(m => m.id, opt => opt.Ignore());
            CreateMap<Gamer, GamerInfo>();
            CreateMap<LanCenter, LanCenterDto>();
            CreateMap<LanCenterDto, LanCenter>()
                .ForMember(m => m.id, opt => opt.Ignore());
            CreateMap<Reservation, ReservationInfo>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>()
                .ForMember(m => m.id, opt => opt.Ignore());
                
        }
    }
}