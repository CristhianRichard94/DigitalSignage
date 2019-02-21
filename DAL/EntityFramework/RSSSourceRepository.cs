using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    public class RSSSourceRepository : Repository<RSSSource, DigitalSignageDbContext>, IRSSSourceRepository
    {
        public RSSSourceRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }

        public IEnumerable<Banner> GetBannersWithSource(int pSourceId)
        {
            return this.iDbContext.Banners
                .Where(b => b.SourceId == pSourceId)
                .ToList();
        }

        public void Update(RSSSource updatedRssSource)
        {
            //Verifica y actualiza el estado de los Items RSS 
            var oldRssSource = this.iDbContext.RSSSources
                .SingleOrDefault(p => p.Id == updatedRssSource.Id);

            if (oldRssSource != null)
            {
                // Actualiza padre
                this.iDbContext.Entry(oldRssSource).CurrentValues.SetValues(updatedRssSource);

                // Elimina los items anteriores
                foreach (var rssItem in oldRssSource.RSSItems.ToList())
                {
                    this.iDbContext.RSSItems.Remove(rssItem);
                }

                oldRssSource.RSSItems = updatedRssSource.RSSItems;

                this.iDbContext.SaveChanges();
            }
        }
    }
}
