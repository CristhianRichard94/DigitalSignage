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
        /// Obtiene todas las fuentes
        /// </summary>
        /// <returns>Enumerable de fuentes RSS</returns>
        public override IEnumerable<RSSSource> GetAll()
        {
            IEnumerable<RSSSource> sources = base.GetAll();
            foreach (RSSSource source in sources)
            {
                //Incluye los items de la fuente
                this.iDbContext.Entry(source).Collection(p => p.RSSItems).Load();
            }
            return sources;
        }

        /// <summary>
        /// Obtiene todas las fuentes
        /// </summary>
        /// <returns>Enumerable de campañas</returns>
        public override RSSSource Get(int pId)
        {
            RSSSource source = base.Get(pId);

            if (source != null)
            {
                this.iDbContext.Entry(source).Collection(p => p.RSSItems).Load();
            }

            return source;
        }



        /// <summary>
        /// Obtiene los banners asociados a una fuente RSS
        /// </summary>
        /// <param name="pSourceId"> Id de la fuente RSS</param>
        /// <returns>Enumerable de banners</returns>
        public IEnumerable<Banner> GetBannersWithSource(int pSourceId)
        {
            return base.iDbContext.Set<Banner>()
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

        /// <summary>
        /// Obtiene las fuentes RSS que posean en su URL parte del parámetro ingresado
        /// </summary>
        /// <param name="pURL">parte de URL a buscar</param>
        /// <returns>Lista de fuentes RSS</returns>
        public IEnumerable<RSSSource> GetSourcesByURL(string pURL)
        {
            List<RSSSource> sources = base.iDbContext.Set<RSSSource>()
                //Busca las fuentes que contengan la url especificada
                .Where(c => c.Url.IndexOf(pURL) >= 0)
                .ToList();

            foreach (RSSSource source in sources)
            {
                //Incluye los items de la fuente
                this.iDbContext.Entry(source).Collection(p => p.RSSItems).Load();
            }
            return sources;
        }
    }
}
