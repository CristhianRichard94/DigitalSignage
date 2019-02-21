using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL
{
    public interface ICampaignRepository : IRepository<Campaign>
    {

        void Update(Campaign updatedCampaign);

        IEnumerable<Campaign> GetCampaignsByName(string pName);

        IEnumerable<Campaign> GetCampaignsActiveInDate(DateTime pDate);
        
        IEnumerable<Campaign> GetCampaignsActiveInRange(DateTime pDate, TimeSpan pFromTime, TimeSpan pToTime);
    }
}
