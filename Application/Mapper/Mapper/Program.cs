using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using innovation4austria.web;

namespace Mapper
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoMapperConfig.RegisterMappings();
            var context = new Innovation4AustriaEntities();

            Bauwerk dasBauwerk = context.Bauwerk.Where(x => x.id == 1).FirstOrDefault();
            Haus  model = AutoMapperConfig.CommonMapper.Map<Haus>(dasBauwerk);

            Console.WriteLine(model.Strasse);

        }
    }
}
