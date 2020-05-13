using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kinotiki.Domain.Concrete;
using kinotiki.BLL.Entity;
using kinotiki.BLL.Abstract;
using Ninject.Modules;
using Ninject.Web.Common;
using AutoMapper;
using Ninject;

namespace kinotiki.Web.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IGlobalSettingsService>().To<GlobalSettingsService>().InRequestScope();
            Bind<IUserService>().To<UserService>().InRequestScope();

            //AutoMapper Binds
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create automapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddProfile(new BLL.Infrastructure.AutoMapperProfile());
                cfg.AddProfile(new AutoMapperProfile());
            });

            return config;
        }
    }
}