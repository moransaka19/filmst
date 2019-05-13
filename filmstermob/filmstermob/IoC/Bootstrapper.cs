using Autofac;
using filmstermob.Contracts.Helpers;
using filmstermob.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace filmstermob.IoC
{
    public class Bootstrapper
    {
        public static IContainer Container { get; set; }
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new HttpHelper()).As<IHttpHelper>();

            Bootstrapper.Container = builder.Build();
        }
    }
}
