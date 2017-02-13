using AutoMapper;
using innovation4austria;
using Innovation4Austria.logic;
using Innovation4Austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace innovation4austria.web
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x => {
                x.CreateMap<Benutzer, ProfilAnzeigeModel>()
                .ForMember(dest => dest.NeuesPasswort, opts => opts.Ignore())
                .ForMember(dest => dest.NeuesPasswortBestätigung, opts => opts.Ignore())
                .ForMember(dest => dest.Bezeichnung, opts => opts.MapFrom(source => source.Firma.Bezeichnung))
                .ForMember(dest => dest.Bezeichnung, opts => opts.NullSubstitute("Innovation  4 Austria"));

                //x.CreateMap()
                });


        }
    }
}