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
            Mapper.Initialize(x =>
            {
                x.CreateMap<Benutzer, PasswortAendernModel>()
                .ForMember(dest => dest.NeuesPasswort, opts => opts.Ignore())
                .ForMember(dest => dest.NeuesPasswortBestätigung, opts => opts.Ignore());

                x.CreateMap<Benutzer, ProfilMitarbeiterModel>()
                .ForMember(dest => dest.FirmenName, opts => opts.MapFrom(source => source.Firma.Bezeichnung))
                .ForMember(dest => dest.FirmenName, opts => opts.NullSubstitute("Innovation  4 Austria"));
            });


        }
    }
}