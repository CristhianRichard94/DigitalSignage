using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{
    public interface IBannerRepository : IRepository<Banner>
    {

        void Update(Banner updatedBanner);


        IEnumerable<Banner> GetBannersByName(string pName);


        IEnumerable<Banner> GetBannersActiveInDate(DateTime pDate);

        
        IEnumerable<Banner> GetBannersActiveInRange(DateTime pDate, TimeSpan pFromTime, TimeSpan pToTime);
    }
}
