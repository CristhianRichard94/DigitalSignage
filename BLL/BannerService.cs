using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalSignage.BLL
{
    public class BannerService : IBannerService
    {
        private UnitOfWork iUnitOfWork;

        // Lista de subscriptos para obtener el texto del banner actual
        private List<IObserver<string>> observers;

        // Lista de banners activos en los proximos <UPDATE_TIME_IN_MINUTES> minutos
        private List<Banner> iNextBanners;

        // Lista de banners activos en este momento
        private List<Banner> iCurrentBanners;

        // Intervalo de tiempo en minutos en los que se vuelve a actualizar la lista actual de banners
        private const int UPDATE_TIME_IN_MINUTES = 10;


        // Intervalo de tiempo en segundos en los que se actualizan las listas de banners
        private const int REFRESH_TIME_IN_SECONDS = 10;

        private string iCurrentText = "No hay ningun banner activo en este momento";

        // Token para cancelar tareas asincronas
        private CancellationToken cancellationToken;
        private CancellationTokenSource tokenSource;


        public BannerService()
        {
            this.iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());

            observers = new List<IObserver<string>>();
            iCurrentBanners = new List<Banner>();

            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;

            GetNextActiveBannersLoop();
            UpdateBannerListsLoop();

        }

        public IEnumerable<BannerDTO> GetAll()
        {
            IEnumerable<Banner> banners = this.iUnitOfWork.BannerRepository.GetAll();
            return AutoMapper.Mapper.Map<IEnumerable<BannerDTO>>(banners);
        }

        public BannerDTO Get(int pId)
        {
            var banner = iUnitOfWork.BannerRepository.Get(pId);

            var bannerDTO = new BannerDTO();
            AutoMapper.Mapper.Map(banner, bannerDTO);
            return bannerDTO;

        }

        public void Update(BannerDTO pBanner)
        {
            Banner banner = new Banner();
            AutoMapper.Mapper.Map(pBanner, banner);
            this.iUnitOfWork.BannerRepository.Update(banner);
            this.iUnitOfWork.Complete();
        }

        public void Create(BannerDTO pBanner)
        {
            Banner banner = new Banner();
            AutoMapper.Mapper.Map(pBanner, banner);
            this.iUnitOfWork.BannerRepository.Add(banner);
            this.iUnitOfWork.Complete();
        }

        public void Remove(BannerDTO pBanner)
        {
            Banner banner = new Banner();
            AutoMapper.Mapper.Map(pBanner, banner);
            this.iUnitOfWork.BannerRepository.Remove(banner);
            this.iUnitOfWork.Complete();

        }

        public IEnumerable<BannerDTO> getBannersByName(string pName)
        {
            IEnumerable<Banner> banners = this.iUnitOfWork.BannerRepository.GetBannersByName(pName);
            return AutoMapper.Mapper.Map<IEnumerable<BannerDTO>>(banners);
        }

        public IEnumerable<BannerDTO> getBannersActiveInDate(DateTime pDate)
        {
            IEnumerable<Banner> banners = this.iUnitOfWork.BannerRepository.GetBannersActiveInDate(pDate);
            return AutoMapper.Mapper.Map<IEnumerable<BannerDTO>>(banners);
        }



        private void UpdateBannerListsLoop()
        {
            var interval = TimeSpan.FromSeconds(REFRESH_TIME_IN_SECONDS);

            RunPeriodicAsync(UpdateBannerLists, interval, cancellationToken);

        }

        private void UpdateBannerLists()
        {
            bool change = false;
            
            // Verifica que los banners que se estan mostrando no se hayan vencido
                iCurrentBanners.RemoveAll(b => !b.IsActiveNow());


            // verifica que no haya nuevos banners para agregar
            foreach (Banner banner in iNextBanners)
            {

                if (banner.IsActiveNow())
                {
                    change = true;
                    iCurrentBanners.Add(banner);
                }

            }
            // Elimina los banners activos de la lista de siguientes banners
            iNextBanners.RemoveAll(b => b.IsActiveNow());

            if (change)
            {

                UpdateCurrentText();

            }
        }

        private void UpdateCurrentText()
        {
            string updatedText = "";
            foreach (Banner banner in iCurrentBanners)
            {

                updatedText += banner.GetText() + " /// ";

            }
            iCurrentText = updatedText;
            foreach (var observer in observers)
            {

                observer.OnNext(iCurrentText);

            }
        }

        private void GetNextActiveBannersLoop()
        {
            var interval = TimeSpan.FromMinutes(UPDATE_TIME_IN_MINUTES);

            RunPeriodicAsync(GetNextActiveBanners, interval, cancellationToken);
        }

        private void GetNextActiveBanners()
        {
            // Obtiene los banners que estaran activos en algun momento de los siguientes <UPDATE_TIME_IN_MINUTES> minutos
            var now = DateTime.Now;
            var actualTimespan = new TimeSpan(now.Hour, now.Minute, 0);
            iCurrentBanners.Clear();


            // Obtiene los banner de la base de datos
            iNextBanners = iUnitOfWork.BannerRepository.GetBannersActiveInRange(now, actualTimespan, actualTimespan.Add(TimeSpan.FromMinutes(UPDATE_TIME_IN_MINUTES))).ToList();
            
            // Actualiza los feeds RSS de los banners
            UpdateRSSSources();
        }

        private void UpdateRSSSources()
        {
            foreach (Banner banner in iNextBanners)
            {

                if (banner.Source is RSSSource)
                {
                    var source = (RSSSource)banner.Source;
                    
                    
                    // Implementar lectura de rss

                }

            }
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
            // Repeat this loop until cancelled.
            while (!token.IsCancellationRequested)
            {
                // Call our onTick function.
                onTick?.Invoke();

                // Wait to repeat again.
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }





        public void RefreshActiveBanners()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();

            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;

            GetNextActiveBannersLoop();
            UpdateBannerListsLoop();
        }



        /// <summary>
        /// Subscripcion para recibir el texto del banner actual
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<string> observer)
        {
            // verifica que el observador no exista en la lista
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                // Envia al nuevo observador el texto actual.
                observer.OnNext(iCurrentText);
            }
            return new Unsubscriber<string>(observers, observer);
        }
    }

    // Clase que permite a los observadores eliminar su subscripcion
    public class Unsubscriber<T> : IDisposable
    {
        private List<IObserver<T>> _observers;
        private IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
