[assembly: WebActivator.PreApplicationStartMethod(typeof(BuildersAlliances.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(BuildersAlliances.Web.App_Start.NinjectWebCommon), "Stop")]

namespace BuildersAlliances.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using BuildersAlliances.Services.Interfaces;
    using BuildersAlliances.Services;
 
    using System.Collections.Generic;
    using Ninject.Modules;
    using System.Web.Http;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);


            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUsers>().To<UserService>();
            kernel.Bind<IManufacturer>().To<ManufacturerService>();
            kernel.Bind<IItem>().To<ItemService>();
            kernel.Bind<IInventory>().To<InventoryService>();
            kernel.Bind<IOrder>().To<OrderService>();
            kernel.Bind<ITruck>().To<TruckService>();
            kernel.Bind<IBuilder>().To<BuilderService>();
            kernel.Bind<ILoginfo>().To<LogInfoServices>();

        }        
    }
}
