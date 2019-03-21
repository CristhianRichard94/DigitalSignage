using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{
    /// <summary>
    /// Interfaz de repositorio de banners
    /// </summary>
    public interface IBannerRepository : IRepository<Banner>
    {


        /// <summary>
        /// Actualiza un banner del repositorio
        /// </summary>
        /// <param name="updatedBanner">Banner actualizado</param>
        void Update(Banner updatedBanner);


        /// <summary>
        /// Obtiene los banners que contengan cierta cadena en su nombre
        /// </summary>
        /// <param name="pName">Cadena de caracteres a cumplir</param>
        /// <returns></returns>
        IEnumerable<Banner> GetBannersByName(string pName);


        /// <summary>
        /// Obtiene los banners activos en una fecha
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <returns></returns>
        IEnumerable<Banner> GetBannersActiveInDate(DateTime pDate);


        /// <summary>
        /// Obtiene los banners activos en un rango horario en una fecha
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <param name="pFromTime">Tiempo de inicio</param>
        /// <param name="pToTime">Tiempo de fin</param>
        /// <returns></returns>
        IEnumerable<Banner> GetBannersActiveInRange(DateTime pDate, TimeSpan pFromTime, TimeSpan pToTime);
    }
}
