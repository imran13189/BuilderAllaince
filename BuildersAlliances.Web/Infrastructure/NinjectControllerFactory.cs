using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ninject;
using System.Web.Mvc;

using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Services;

namespace BuildersAlliances.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {

        private IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();

        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)this._kernel.Get(controllerType);
        }


        private void AddBindings()
        {
            this._kernel.Bind<IUsers>().To<UserService>();
            this._kernel.Bind<IManufacturer>().To<ManufacturerService>();
            this._kernel.Bind<IItem>().To<ItemService>();
            this._kernel.Bind<IInventory>().To<InventoryService>();
            this._kernel.Bind<IOrder>().To<OrderService>();
            this._kernel.Bind<ITruck>().To<TruckService>();
            this._kernel.Bind<IBuilder>().To<BuilderService>();
            this._kernel.Bind<ILoginfo>().To<LogInfoServices>();
            this._kernel.Bind<IQoute>().To<QouteService>();
            this._kernel.Bind<IInvoice>().To<InvoiceService>();





        }
    }
}