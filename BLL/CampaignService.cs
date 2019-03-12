using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;

namespace DigitalSignage.BLL
{
    public class CampaignService : ICampaignService
    {
        private UnitOfWork iUnitOfWork;

        public CampaignService()
        {

            this.iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());

            //observers = new List<IObserver<byte[]>>();
           // iCurrentCampaigns = new List<Campaign>();
           // iCurrentImages = new List<Image>();
           // iCurrentImageIndex = 0;

           // tokenSource = new CancellationTokenSource();
           // cancellationToken = tokenSource.Token;

          //  log.Info("Iniciando tareas asincronas...");
          //  GetNextActiveCampaignsLoop();
          //  UpdateCampaignListsLoop();
          //  UpdateCurrentImageIndex();

        }

        /// <summary>
        /// Obtiene las campañas del repositorio, las mapea y devuelve a la vista.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CampaignDTO> GetAll()
        {
            IEnumerable<Campaign> campaigns = this.iUnitOfWork.CampaignRepository.GetAll();
            return AutoMapper.Mapper.Map<IEnumerable<CampaignDTO>>(campaigns);
        }

        public CampaignDTO Get(int pId)
        {
            var campaign = iUnitOfWork.CampaignRepository.Get(pId);

            var campaignDTO = new CampaignDTO();
            AutoMapper.Mapper.Map(campaign, campaignDTO);
            return campaignDTO;

        }

        public void Update(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            this.iUnitOfWork.CampaignRepository.Update(campaign);
            this.iUnitOfWork.Complete();
        }

        public void Create(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            this.iUnitOfWork.CampaignRepository.Add(campaign);
            this.iUnitOfWork.Complete();
        }

        public void Remove(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            this.iUnitOfWork.CampaignRepository.Remove(campaign);
            this.iUnitOfWork.Complete();

        }
    }
}
