using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{

    /// <summary>
    /// Interfaz de repositorio de Fuentes RSS
    /// </summary>
    public interface IRSSSourceRepository : IRepository<RSSSource>
    {

        /// <summary>
        /// Actualiza una fuente RSS  
        /// </summary>
        /// <param name="updatedRssSource"></param>
        void Update(RSSSource updatedRssSource);

        /// <summary>
        /// Obtiene los banners que estan asociados a una fuente RSS especifica
        /// </summary>
        /// <param name="pSourceId"></param>
        /// <returns></returns>
        IEnumerable<Banner> GetBannersWithSource(int pSourceId);

        /// <summary>
        /// Obtiene las fuentes RSS que posean en su URL parte del parámetro ingresado
        /// </summary>
        /// <param name="pURL">parte de URL a buscar</param>
        /// <returns>Lista de fuentes RSS</returns>
        IEnumerable<RSSSource> GetSourcesByURL(string pURL);
    }
}
