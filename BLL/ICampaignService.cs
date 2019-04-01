using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalSignage.DTO;

namespace DigitalSignage.BLL
{
    /// <summary>
    /// Interfaz del servicio de campañas
    /// </summary>
    public interface ICampaignService : IObservable<byte[]>
    {
        IEnumerable<CampaignDTO> GetAll();

        CampaignDTO Get(int pId);

        void Update(CampaignDTO pCampaign);

        void Create(CampaignDTO pCampaign);

        void Remove(CampaignDTO pCampaign);

        IEnumerable<CampaignDTO> getCampaignsByName(string pName);

        IEnumerable<CampaignDTO> GetCampaignsActiveInDate(DateTime pDate);

        void RefreshActiveCampaigns();
    }
}
