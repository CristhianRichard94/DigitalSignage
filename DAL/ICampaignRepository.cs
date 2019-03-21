using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{
    /// <summary>
    /// Interfaz de repositorio de campañas
    /// </summary>
    public interface ICampaignRepository : IRepository<Campaign>
    {


        /// <summary>
        /// Actualiza una campaña
        /// </summary>
        /// <param name="updatedCampaign">Campaña actualizada</param>
        void Update(Campaign updatedCampaign);


        /// <summary>
        /// Obtiene campañas que cumplan contengan una cadena en su nombre
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        IEnumerable<Campaign> GetCampaignsByName(string pName);


        /// <summary>
        /// Obtiene las campañas activas en una fecha
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
        IEnumerable<Campaign> GetCampaignsActiveInDate(DateTime pDate);


        /// <summary>
        /// Obtiene las campañas activas en un rango horario en una fecha
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <param name="pFromTime">Tiempo de inicio</param>
        /// <param name="pToTime">Tiempo de fin</param>
        /// <returns></returns>
        IEnumerable<Campaign> GetCampaignsActiveInRange(DateTime pDate, TimeSpan pFromTime, TimeSpan pToTime);
    }
}
