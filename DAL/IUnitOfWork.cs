using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Da acceso al repositorio de banners
        /// </summary>
        IBannerRepository BannerRepository { get; }

        /// <summary>
        /// Da acceso al repositorio de campañas
        /// </summary>
        ICampaignRepository CampaignRepository { get; } 
        
        /// <summary>
        /// Da acceso al repositorio de Banners RSS
        /// </summary>
        IRSSSourceRepository RSSSourceRepository { get; }


        void Complete();
    }
}
