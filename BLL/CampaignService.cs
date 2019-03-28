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

    /// <summary>
    /// Clase que implementa la interfaz de IRSSSource que brinda servicios a la vista
    /// </summary>
    public class CampaignService : ICampaignService
    {
        private UnitOfWork iUnitOfWork;

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
    }
}
