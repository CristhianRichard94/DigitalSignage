using System;
using System.Collections.Generic;
using System.IO;
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

        private List<IObserver<byte[]>> observers;

        private List<Campaign> iCurrentCampaigns;

        private List<Image> iCurrentImages;

        private int iCurrentImageIndex;

        private byte[] iDefaultImage = File.ReadAllBytes("../../../assets/images/1.jpg");

        public CampaignService()
        {

            this.iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());

            observers = new List<IObserver<byte[]>>();
            iCurrentCampaigns = new List<Campaign>();
            iCurrentImages = new List<Image>();
            iCurrentImageIndex = 0;

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

        public IEnumerable<CampaignDTO> getCampaignsByName(string pName)
        {
            IEnumerable<Campaign> campaigns = this.iUnitOfWork.CampaignRepository.GetCampaignsByName(pName);
            return AutoMapper.Mapper.Map<IEnumerable<CampaignDTO>>(campaigns);
        }

        public void UpdateCampaigns()
        {

           // tokenSource.Cancel();
            //tokenSource.Dispose();

            //tokenSource = new CancellationTokenSource();
            //cancellationToken = tokenSource.Token;

            //GetNextActiveCampaignsLoop();
            //UpdateCampaignListsLoop();
            //UpdateCurrentImageIndex();
        }


        public IDisposable Subscribe(IObserver<byte[]> observer)
        {
            // verifica que el observador no exista en la lista
            if (!observers.Contains(observer))
            {
                observers.Add(observer);

                if (iCurrentImages.Count > 0)
                {
                    // Envia al nuevo observador la imagen actual.
                    observer.OnNext(iCurrentImages[iCurrentImageIndex].Data);

                }
                else
                {
                    // Envia al nuevo observador la imagen por defecto
                    observer.OnNext(iDefaultImage);

                }

            }
            return new Unsubscriber<byte[]>(observers, observer);
        }

    }
}
