using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    /// <summary>
    /// Clase que implementa el repositorio de fuentes RSS
    /// </summary>
    public class RSSSourceRepository : Repository<RSSSource, DigitalSignageDbContext>, IRSSSourceRepository
    {
        public RSSSourceRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }

        /// <summary>
        /// Obtiene los banners asociados a una fuente RSS
        /// </summary>
        /// <param name="pSourceId"> Id de la fuente RSS</param>
        /// <returns>Enumerable de banners</returns>
        public IEnumerable<Banner> GetBannersWithSource(int pSourceId)
        {
            return this.iDbContext.Banners
                .Where(b => b.SourceId == pSourceId)
                .ToList();
        }


        /// <summary>
        /// Actualiza una fuente RSS
        /// </summary>
        /// <param name="updatedRSSSource"></param>
        public void Update(RSSSource updatedRSSSource)
        {
            //Verifica y actualiza los Items RSS 
            var oldRSSSource = this.iDbContext.RSSSources
                .Include("RSSItems")
                .SingleOrDefault(p => p.Id == updatedRSSSource.Id);

            if (oldRSSSource != null)
            {
                // Actualiza padre
                this.iDbContext.Entry(oldRSSSource).CurrentValues.SetValues(updatedRSSSource);

                // Elimina los items anteriores
                foreach (var iRSSItem in oldRSSSource.RSSItems.ToList())
                {
                    this.iDbContext.RSSItems.Remove(iRSSItem);
                }

                oldRSSSource.RSSItems = updatedRSSSource.RSSItems;

                this.iDbContext.SaveChanges();
            }
        }
    }
}
