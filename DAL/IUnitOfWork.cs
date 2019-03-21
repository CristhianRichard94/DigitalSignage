using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{
    /// <summary>
    /// Interfaz generica que realiza transacciones con la base de datos
    /// </summary>
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

        /// <summary>
        /// Guarda los cambios
        /// </summary>
        void Complete();
    }
}
