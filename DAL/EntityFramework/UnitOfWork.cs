using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DigitalSignageDbContext iDbContext;

        /// <summary>
        /// Da acceso al repositorio de banners
        /// </summary>
        public IBannerRepository BannerRepository { get; private set; }

        /// <summary>
        /// Da acceso al repositorio de campañas
        /// </summary>
        public ICampaignRepository CampaignRepository { get; private set; }

        /// <summary>
        /// Da acceso al repositorio de Banners RSS
        /// </summary>
        public IRSSSourceRepository RSSSourceRepository { get; private set; }

        /// <summary>
        /// Da acceso al repositorio de Banners de texto
        /// </summary>
        public ITextRepository TextRepository { get; private set; }

        /// <summary>
        /// Constructor de la clase, utiliza el contexto para inicializar los repositorios
        /// </summary>
        /// <param name="pContext">Contexto que representa una sesión con la base de datos</param>
        public UnitOfWork(DigitalSignageDbContext pContext)
        {
            if (pContext is null)
            {
                throw new ArgumentNullException(nameof(pContext));
            }

            this.iDbContext = pContext;

            this.CampaignRepository = new CampaignRepository(this.iDbContext);
            this.BannerRepository = new BannerRepository(this.iDbContext);
            this.RSSSourceRepository = new RSSSourceRepository(this.iDbContext);
            this.TextRepository = new TextRepository(this.iDbContext);
        }

        public void Complete()
        {
            this.iDbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.iDbContext.Dispose();
        }
    }
}
