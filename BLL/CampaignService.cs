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
        private TimeSpan UPDATE_TIME = new TimeSpan(0,10,0);


        /// <summary>
        /// Intervalo de tiempo en segundos en que se actualiza la lista de campañas
        /// </summary>
        private TimeSpan REFRESH_TIME = new TimeSpan(0,0,10);

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

            observers = new List<IObserver<byte[]>>();
            iCurrentCampaigns = new List<Campaign>();
            iCurrentImages = new List<Image>();
            iCurrentImageIndex = 0;

            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;

            GetNextActiveCampaignsLoop();
            UpdateCampaignListsLoop();
            UpdateCurrentImageIndex();
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
            iUnitOfWork.CampaignRepository.Update(campaign);
            iUnitOfWork.Complete();
        }


        /// <summary>
        /// Crea una campaña
        /// </summary>
        /// <param name="pCampaign">Campaña creada</param>
        public void Create(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            iUnitOfWork.CampaignRepository.Add(campaign);
            iUnitOfWork.Complete();
        }

        /// <summary>
        /// Elimina una campaña
        /// </summary>
        /// <param name="pCampaign">Campaña a eliminar</param>
        public void Remove(CampaignDTO pCampaign)
        {
            Campaign campaign = new Campaign();
            AutoMapper.Mapper.Map(pCampaign, campaign);
            iUnitOfWork.CampaignRepository.Remove(campaign);
            iUnitOfWork.Complete();

        }


        /// <summary>
        /// Obtiene campañas por nombre
        /// </summary>
        /// <param name="pName">nombre de campaña por el cual buscar</param>
        /// <returns></returns>
        public IEnumerable<CampaignDTO> getCampaignsByName(string pName)
        {
            IEnumerable<Campaign> campaigns = iUnitOfWork.CampaignRepository.GetCampaignsByName(pName);
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

        public void GetActiveCampaigns()
        {
            // Obtiene las campañas que se encontraran activas en algun momento de los siguientes <UPDATE_TIME_IN_MINUTES> minutos
            var now = DateTime.Now;
            var actualTimespan = new TimeSpan(now.Hour, now.Minute, 0);

            // Limpia la lista de campañas
            iCurrentCampaigns.Clear();

            iNextCampaigns = iUnitOfWork.CampaignRepository.GetCampaignsActiveInRange(now, actualTimespan, actualTimespan.Add(UPDATE_TIME)).ToList();

        }

        /// <summary>
        /// Invoca un bucle para obtener las siguientes campañas a mostrar
        /// </summary>
        private void GetNextActiveCampaignsLoop()
        {

            RunPeriodicAsync(GetActiveCampaigns, UPDATE_TIME, cancellationToken);

        }


        /// <summary>
        /// Invoca un bucle para actualizar las listas de campañas 
        /// </summary>
        private void UpdateCampaignListsLoop()
        {


            RunPeriodicAsync(UpdateCampaignLists, REFRESH_TIME, cancellationToken);

        }

        /// <summary>
        /// Actualiza la lista de campañas activas
        /// </summary>
        private void UpdateCampaignLists()
        {
                // Verifica que las campañas que se estan mostrando no se hayan vencido
                iCurrentCampaigns.RemoveAll(c => !c.IsActiveNow());

                // Verifica que no haya nuevas campañas para agregar
                foreach (Campaign campaign in iNextCampaigns)
                {

                    if (campaign.IsActiveNow())
                    {
                        iCurrentCampaigns.Add(campaign);
                    }

                }
                // Elimina los elementos de la lista de next campaigns
                iNextCampaigns.RemoveAll(c => c.IsActiveNow());

            
        }


        /// <summary>
        /// Corre una tarea asincrona cada cierto intervalo
        /// </summary>
        /// <param name="onTick">Tarea a ejecutar</param>
        /// <param name="interval">Intervalo de tiempo cada cuanto se invoca la tarea</param>
        /// <param name="token">Token para canelar tarea</param>
        /// <returns></returns>
        private static async Task RunPeriodicAsync(Action onTick, TimeSpan interval, CancellationToken token)
        {
            // Repite el bucle hasta que se cancela la tarea.
            while (!token.IsCancellationRequested)
            {
                // Invoca la tarea pasada como parametro.
                onTick?.Invoke();

                // Espera para repetir.
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }


        /// <summary>
        /// Actualiza el indice actual de la imagen
        /// </summary>
        private void UpdateCurrentImageIndex()
        {
            // Verifica que no se haya terminado de recorrer la lista de imagenes
            iCurrentImageIndex++;
            if (iCurrentImageIndex >= iCurrentImages.Count)
            {

                UpdateCurrentImages();
                iCurrentImageIndex = 0;

            }

            TimeSpan interval;

            // Si hay imagenes para mostrar
            if (iCurrentImages.Count > 0)
            {

                // Notifica a los observadores
                foreach (var observer in observers)
                {
                    observer.OnNext(iCurrentImages[iCurrentImageIndex].Data);
                }

                // Pospone la actualizacion dependiendo de la duracion de la imagen actual
                interval = TimeSpan.FromSeconds(iCurrentImages[iCurrentImageIndex].Duration);

            } // No hay imagenes para mostrar
            else
            {

                // Notifica a los observadores con la imagen por default
                foreach (var observer in observers)
                {
                    observer.OnNext(iDefaultImage);
                }

                // Pospone la siguiente actualizacion en un tiempo por defecto
                interval = TimeSpan.FromSeconds(5);

            }

            // Corre una tarea de espera para volver a actualizar el indice
            Task.Run(async () => {

                // Espera que se ejecute la tarea de espera
                await Task.Delay(interval, cancellationToken);

                // Si no se solicito cancelar, se actualiza el indice
                if (!cancellationToken.IsCancellationRequested)
                {
                    UpdateCurrentImageIndex();
                }

            }, cancellationToken);

        }

        /// <summary>
        /// Actualiza la lista de imagenes actuales
        /// </summary>
        private void UpdateCurrentImages()
        {
            // Vacia la lista de imagenes
            iCurrentImages.Clear();

            //Agrega las imagenes de las siguientes campañas
            foreach (Campaign campaign in iCurrentCampaigns)
            {

                iCurrentImages.AddRange(campaign.Images);

            }
        }


        /// <summary>
        /// Reinicia todas las tareas asincronas del servicio
        /// </summary>
        public void RefreshActiveCampaigns()
        {

            // Cancela las tareas actuales por medio del token
            tokenSource.Cancel();
            tokenSource.Dispose();

            // Genera un nuevo token
            cancellationToken = new CancellationTokenSource().Token;

            // Vuelve a correr bucles de actualizacion de campañas e imagenes
            GetNextActiveCampaignsLoop();
            UpdateCampaignListsLoop();
            UpdateCurrentImageIndex();
        }


        /// <summary>
        /// Subscripcion para recibir las imagenes de la campaña actual
        /// </summary>
        /// <param name="observer">Nuevo observador que desea subscribirse</param>
        /// <returns></returns>
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
