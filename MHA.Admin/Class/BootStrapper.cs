using Autofac;
using Autofac.Integration.Mvc;
using MHA.Core.Repository.Contract;
using MHA.Core.Repository.Contrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHA.Admin.Class
{
    public class BootStrapper
    {
        //boot zamanında çalışacak class
        //burada register edilmeyenler çalışmayacak
        public static void RunConfig()
        {
            BuildAutoFac();
        }
        private static void BuildAutoFac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NewsRepository>().As<INewsRepository>();
            builder.RegisterType<ImageRepository>().As<IImageRepository>();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<TagRepository>().As<ITagRepository>();
            builder.RegisterType<SliderRepository>().As<ISliderRepository>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}