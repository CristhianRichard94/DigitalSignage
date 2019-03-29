using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;

namespace DigitalSignage.BLL
{

    /// <summary>
    /// Clase que implementa la interfaz de IRSSSource que brinda servicios a la vista
    /// </summary>
    public class CampaignService : ICampaignService
    {
        /// <summary>
        /// Instancia de unidad de trabajo que representa sesion con la BD
        /// </summary>
        private UnitOfWork iUnitOfWork;

        /// <summary>
        /// Lista de subscriptos para obtener la lista de imagenes actual
        /// </summary>
        private List<IObserver<byte[]>> observers;

        /// <summary>
        /// Lista de campañas en los proximos <UPDATE_TIME_IN_MINUTES> minutos
        /// </summary>
        private List<Campaign> iNextCampaigns;

        /// <summary>
        /// Lista de campañas activos en este momento
        /// </summary>
        private List<Campaign> iCurrentCampaigns;

        /// <summary>
        /// Lista de campañas activas en este momento que se estan mostrando en pantalla
        /// Sirve para evitar un cambio de golpe cuando se agrega o elimina una campaña de iCurrentCampaigns
        /// </summary>
        private List<Campaign> iShowingCampaigns;

        /// <summary>
        /// Lista de imagenes (concatenacion de imagenes de las campañas activas)
        /// </summary>
        private List<Image> iCurrentImages;

        /// <summary>
        ///  Indice de la imagen actual
        /// </summary>
        private int iCurrentImageIndex = 0;

        /// <summary>
        /// Imagen por defecto
        /// </summary>
        private byte[] iDefaultImage = File.ReadAllBytes("../../../assets/images/no_campaigns.png");

        /// <summary>
        /// Intervalo de tiempo en minutos en que se vuelve a actualizar la lista actual de campañas
        /// </summary>
        private const int UPDATE_TIME_IN_MINUTES = 10;


        /// <summary>
        /// Intervalo de tiempo en segundos en que se actualiza la lista de campañas
        /// </summary>
        private const int REFRESH_TIME_IN_SECONDS = 10;

        /// <summary>
        /// Token para cancelar tareas asincronas
        /// </summary>
        private CancellationToken cancellationToken;
        private CancellationTokenSource tokenSource;


        /// <summary>
        /// Constructor para usar contexto por defecto
        /// </summary>
        public CampaignService()
        {
            this.iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());
        }


        /// <summary>
        /// Obtiene todas las campañas del repositorio
        /// </summary>
        /// <returns>Enumerable de campañas</returns>
        public IEnumerable<CampaignDTO> GetAll()
        {
            IEnumerable<Campaign> campaigns = this.iUnitOfWork.CampaignRepository.GetAll();
            return AutoMapper.Mapper.Map<IEnumerable<CampaignDTO>>(campaigns);
        }


        /// <summary>
        /// Obtiene una campaña por su id
        /// </summary>
        /// <param name="pId">Id de la campaña</param>
        /// <returns>Campaña</returns>
        public CampaignDTO Get(int pId)
        {
            var campaign = iUnitOfWork.CampaignRepository.Get(pId);

            var campaignDTO = new CampaignDTO();
            AutoMapper.Mapper.Map(campaign, campaignDTO);
            return campaignDTO;

        }


        /// <summary>
        /// Actualiza una campaña
        /// </summary>
        /// <param name="pCampaign">Campaña modificada</param>
        public void Update(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            this.iUnitOfWork.CampaignRepository.Update(campaign);
            this.iUnitOfWork.Complete();
        }


        /// <summary>
        /// Crea una campaña
        /// </summary>
        /// <param name="pCampaign">Campaña creada</param>
        public void Create(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            this.iUnitOfWork.CampaignRepository.Add(campaign);
            this.iUnitOfWork.Complete();
        }

        /// <summary>
        /// Elimina una campaña
        /// </summary>
        /// <param name="pCampaign">Campaña a eliminar</param>
        public void Remove(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            this.iUnitOfWork.CampaignRepository.Remove(campaign);
            this.iUnitOfWork.Complete();

        }


        /// <summary>
        /// Obtiene campañas por nombre
        /// </summary>
        /// <param name="pName">nombre de campaña por el cual buscar</param>
        /// <returns></returns>
        public IEnumerable<CampaignDTO> getCampaignsByName(string pName)
        {
            IEnumerable<Campaign> campaigns = this.iUnitOfWork.CampaignRepository.GetCampaignsByName(pName);
            return AutoMapper.Mapper.Map<IEnumerable<CampaignDTO>>(campaigns);
        }


        /// <summary>
        /// Obtiene todas las camapañas activas en un dia determinado
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <returns></returns>
        public IEnumerable<CampaignDTO> GetCampaignsActiveInDate(DateTime pDate)
        {

                IEnumerable<Campaign> campaigns = iUnitOfWork.CampaignRepository.GetCampaignsActiveInDate(pDate);

                var campaignsDTO = AutoMapper.Mapper.Map<IEnumerable<Campaign>, IEnumerable<CampaignDTO>>(campaigns);

                return campaignsDTO;

        }

        /// <summary>
        /// Obtiene todas las camapañas activas en un momento determinado
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <returns></returns>
        public IEnumerable<CampaignDTO> GetCampaignsActiveInRange(DateTime pDate, TimeSpan pFromTime, TimeSpan pToTime)
        {

            IEnumerable<Campaign> campaigns = iUnitOfWork.CampaignRepository.GetCampaignsActiveInRange(pDate, pFromTime,pToTime);
            var campaignsDTO = AutoMapper.Mapper.Map<IEnumerable<Campaign>, IEnumerable<CampaignDTO>>(campaigns);

            return campaignsDTO;

        }

        public IEnumerable<CampaignDTO> GetActiveCampaigns()
        {
            throw new NotImplementedException();
        }
    }
}
