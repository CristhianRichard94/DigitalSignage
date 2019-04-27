using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{

    /// <summary>
    /// Clase que implementa el repositorio de banners
    /// </summary>
    public class BannerRepository : Repository<Banner, DigitalSignageDbContext>, IBannerRepository
    {
        public BannerRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }


        /// <summary>
        /// Obtiene todos los banners
        /// </summary>
        /// <returns>Enumerable de campañas</returns>
        public override IEnumerable<Banner> GetAll()
        {
            IEnumerable<Banner> banners = base.GetAll();
            foreach (Banner banner in banners)
            {
                //Incluye las fuentes de los banners
                this.iDbContext.Entry(banner).Reference(p => p.Source).Load();
            }
            return banners;
        }

        /// <summary>
        /// Actualiza un banner del repositorio
        /// </summary>
        /// <param name="updatedBanner">Banner actualizado</param>
        public override void Remove(Banner banner)
        {
            if (banner.Source is TextSource)
            {
                this.iDbContext.TextSources.Remove((TextSource)banner.Source);
            }
            base.Remove(banner);
        }

        /// <summary>
        /// Actualiza un banner del repositorio
        /// </summary>
        /// <param name="updatedBanner">Banner actualizado</param>
        public void Update(Banner updatedBanner)
        {
            var oldBanner = this.iDbContext.Banners
                .SingleOrDefault(p => p.Id == updatedBanner.Id);

            //Actualiza los datos
            oldBanner.Description = updatedBanner.Description;
            oldBanner.InitialDate = updatedBanner.InitialDate;
            oldBanner.EndDate = updatedBanner.EndDate;
            oldBanner.InitialTime = updatedBanner.InitialTime;
            oldBanner.EndTime = updatedBanner.EndTime;
            oldBanner.Name = updatedBanner.Name;

            //if (updatedBanner.Source.Id > 0)
            //{

            //    oldBanner.SourceId = updatedBanner.Source.Id;

            //}
            //else
            //{

            //    oldBanner.Source = updatedBanner.Source;

            //}



            if (oldBanner.Source is TextSource)
            {
                TextSource oldSource = (TextSource)oldBanner.Source;
                oldBanner.Source = updatedBanner.Source;
                this.iDbContext.TextSources.Remove(oldSource);
            } else
            {
                oldBanner.Source = updatedBanner.Source;
            }
            //Guardando los cambios
            this.iDbContext.SaveChanges();

        }

        /// <summary>
        /// Obtiene los banners que contengan cierta cadena en su nombre
        /// </summary>
        /// <param name="pName">Cadena de caracteres a cumplir</param>
        /// <returns></returns>
        public IEnumerable<Banner> GetBannersByName(string pName)
        {
            if (pName == null)
                throw new ArgumentNullException("pName");

            return base.iDbContext.Set<Banner>()
                //Busca los banners que contengan el nombre especificado
                .Where(c => c.Name.IndexOf(pName) >= 0)
                .Include("Source")
                .ToList();
        }

        /// <summary>
        /// Obtiene los banners activos en una fecha
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <returns></returns>
        public IEnumerable<Banner> GetBannersActiveInDate(DateTime pDate)
        {
            if (pDate == null)
                throw new ArgumentNullException("pDate");

            return base.iDbContext.Set<Banner>()
                //Compara si el banner esta activo en la fecha
                .Where(c => DbFunctions.TruncateTime(c.InitialDate) <= DbFunctions.TruncateTime(pDate))
                .Where(c => DbFunctions.TruncateTime(c.EndDate) >= DbFunctions.TruncateTime(pDate))
                .Include("Source")
                .ToList();
        }


        /// <summary>
        /// Obtiene los banners activos en un rango horario en una fecha
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <param name="pFromTime">Tiempo de inicio</param>
        /// <param name="pToTime">Tiempo de fin</param>
        /// <returns></returns>
        public IEnumerable<Banner> GetBannersActiveInRange(DateTime pDate, TimeSpan pFromTime, TimeSpan pToTime)
        {
            if (pDate == null)
                throw new ArgumentNullException("pDate");
            if (pFromTime == null)
                throw new ArgumentNullException("pTimeFrom");
            if (pToTime == null)
                throw new ArgumentNullException("pTimeTo");
            if (pFromTime.CompareTo(pToTime) > -1)
                throw new InvalidOperationException("El tiempo de inicio debe ser menor al de fin");

            List<Banner> banners = base.iDbContext.Set<Banner>()
                //Compara si el banner esta activo en la fecha
                .Where(b => DbFunctions.TruncateTime(b.InitialDate) <= DbFunctions.TruncateTime(pDate))
                .Where(b => DbFunctions.TruncateTime(b.EndDate) >= DbFunctions.TruncateTime(pDate))
                //Compara si el banner esta activo en el rango horario
                .Where(b => b.InitialTime <= pToTime && b.EndTime >= pFromTime)
                .Include("Source")
                .ToList();

            // Para todos los banners, si su fuente es RSS, obtiene los Items RSS de la misma
            foreach (Banner banner in banners)
            {
                if (banner.Source is RSSSource)
                {
                    this.iDbContext.Entry((RSSSource)banner.Source).Collection(p => p.RSSItems).Load();
                }
            }
            return banners;
        }
    }
}
