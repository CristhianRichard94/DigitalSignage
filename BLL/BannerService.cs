using DigitalSignage.BLL.RSSReader;
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
        /// <summary>
        /// Instancia de unidad de trabajo
        /// </summary>
        private UnitOfWork iUnitOfWork;


        /// <summary>
        /// Lista de subscriptos para obtener el texto del banner actual
        /// </summary>
        private List<IObserver<string>> observers;

        /// <summary>
        /// Lista de banners activos en el proximo <UPDATE_TIME>
        /// </summary>
        private List<Banner> iNextBanners;

        /// <summary>
        /// Lista de banners activos en este momento
        /// </summary>
        private List<Banner> iCurrentBanners;

        /// <summary>
        /// Intervalo de tiempo en minutos en que se vuelve a actualizar la lista actual de campañas
        /// </summary>
        private TimeSpan UPDATE_TIME = new TimeSpan(0, 10, 0);


        /// <summary>
        /// Intervalo de tiempo en segundos en que se actualiza la lista de campañas
        /// </summary>
        private TimeSpan REFRESH_TIME = new TimeSpan(0, 0, 10);

        private const string  EMPTY_BANNERS_TEXT = "No hay ningun banner activo en este momento";

        private string iCurrentText = "";

        /// <summary>
        ///  Token para cancelar tareas asincronas
        /// </summary>
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
            if (pBanner.Source is RSSSourceDTO)
            {
                banner.Source = this.iUnitOfWork.RSSSourceRepository.Get(banner.Source.Id);
            }
            this.iUnitOfWork.BannerRepository.Add(banner);
            this.iUnitOfWork.Complete();
        }

        public void Remove(BannerDTO pBanner)
        {
            Banner banner = this.iUnitOfWork.BannerRepository.Get(pBanner.Id);
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



        private void GetNextActiveBannersLoop()
        {

            RunPeriodicAsync(GetNextActiveBanners, UPDATE_TIME, cancellationToken);
        }

        private void UpdateBannerListsLoop()
        {

            RunPeriodicAsync(UpdateBannerLists, REFRESH_TIME, cancellationToken);

        }

        private void UpdateBannerLists()
        {
            bool change = false;

            // Verifica que los banners que se estan mostrando no se hayan vencido
            iCurrentBanners.RemoveAll(b =>
            {
                if (!b.IsActiveNow())
                {
                    change = true;
                    return true;
                }

                return false;
            }
            );

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

            if (iCurrentBanners.Count == 0)
            {
                iCurrentText = EMPTY_BANNERS_TEXT;                
            }
            foreach (var observer in observers)
            {

                observer.OnNext(iCurrentText);

            }
        }

        private void GetNextActiveBanners()
        {
            // Obtiene los banners que estaran activos en algun momento de los siguientes <UPDATE_TIME_IN_MINUTES> minutos
            var now = DateTime.Now;
            var actualTimespan = new TimeSpan(now.Hour, now.Minute, 0);
            iCurrentBanners.Clear();


            // Obtiene los banners de la BD
            iNextBanners = iUnitOfWork.BannerRepository.GetBannersActiveInRange(now, actualTimespan, actualTimespan.Add(UPDATE_TIME)).ToList();
            
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

                    Task.Run(() => readFeeds(source));

                }

            }
        }

        private void readFeeds(RSSSource source)
        {
            try
            {
                IRSSReader rSSReader = new XMLRSSReader();
                Uri uri;
                Uri.TryCreate(source.Url, UriKind.Absolute, out uri);
                var newRSSItems = rSSReader.Read(uri).ToList();
                if (newRSSItems != null && newRSSItems.Count > 0)
                {
                    source.RSSItems = AutoMapper.Mapper.Map<IList<RSSItemDTO>, IList<RSSItem>>(newRSSItems);
                }
            }
            catch (Exception)
            {
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
