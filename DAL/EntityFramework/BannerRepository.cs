using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    public class BannerRepository : Repository<Banner, DigitalSignageDbContext>, IBannerRepository
    {
        public BannerRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }

        public IEnumerable<Banner> GetBannersByName(string pName)
        {
            if (pName == null)
                throw new ArgumentNullException("pName");

            return base.iDbContext.Set<Banner>()
                //Busca los banners que contengan el nombre especificado
                .Where(c => c.Name.IndexOf(pName, StringComparison.Ordinal) >= 0)
                .ToList();
        }

        public IEnumerable<Banner> GetBannersActiveInDate(DateTime pDate)
        {
            if (pDate == null)
                throw new ArgumentNullException("pDate");

            return base.iDbContext.Set<Banner>()
                //Compara si el banner esta activo en la fecha
                .Where(c => DbFunctions.TruncateTime(c.InitialDate) <= DbFunctions.TruncateTime(pDate))
                .Where(c => DbFunctions.TruncateTime(c.EndDate) >= DbFunctions.TruncateTime(pDate))
                .ToList();
        }

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

            return base.iDbContext.Set<Banner>()
                //Compara si el banner esta activo en la fecha
                .Where(b => DbFunctions.TruncateTime(b.InitialDate) <= DbFunctions.TruncateTime(pDate))
                .Where(b => DbFunctions.TruncateTime(b.EndDate) >= DbFunctions.TruncateTime(pDate))
                //Compara si el banner esta activo en el rango horario
                .Where(b => b.InitialTime <= pToTime && b.EndTime >= pFromTime)
                .ToList();
        }
    }
}
