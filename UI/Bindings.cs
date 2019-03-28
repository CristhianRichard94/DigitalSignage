using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using DigitalSignage.BLL;

namespace DigitalSignage.UI
{
    /// <summary>
    /// Implementación del modulo IOC que define que implementacion usar para cada servicio
    /// </summary>
    class Bindings : NinjectModule
    {

        public override void Load()
        {
            // Para el servicio de campañas usar la implementacion CampaignService
            Bind<ICampaignService>().To<CampaignService>();
            Bind<IBannerService>().To<BannerService>();
            Bind<IRSSSourceService>().To<RSSSourceService>();
        }
    }
}

