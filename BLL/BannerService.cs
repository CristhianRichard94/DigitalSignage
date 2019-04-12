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
using Serilog;

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

        private const string EMPTY_BANNERS_TEXT = "No hay ningun banner activo en este momento";

        private string iCurrentText = "";

        /// <summary>
        ///  Token para cancelar tareas asincronas
        /// </summary>
        private CancellationToken cancellationToken;
        private CancellationTokenSource tokenSource;



        public BannerService()
        {
            iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());
            observers = new List<IObserver<string>>();
            iCurrentBanners = new List<Banner>();

            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;


            GetNextActiveBannersLoop();
            UpdateBannerListsLoop();

        }

        public IEnumerable<BannerDTO> GetAll()
        {
            try
            {
                Log.Information("Obteniendo todos los banners.");
                IEnumerable<Banner> banners = iUnitOfWork.BannerRepository.GetAll();
                Log.Information("Banners obtenidos con exito.");
                return AutoMapper.Mapper.Map<IEnumerable<BannerDTO>>(banners);
            }
            catch (Exception)
            {
                Log.Error("Error al obtener todos los banners.");
                throw;
            }

        }

        public BannerDTO Get(int pId)
        {
            try
            {
                Log.Information(String.Format("Obteniendo banner con Id {0}.", pId));
                var banner = iUnitOfWork.BannerRepository.Get(pId);
                Log.Information("Banner obtenido con exito.");

                var bannerDTO = new BannerDTO();
                AutoMapper.Mapper.Map(banner, bannerDTO);
                return bannerDTO;
            }
            catch (Exception)
            {
                Log.Error("Error al obtener banner.");
                throw;
            }


        }

        public void Update(BannerDTO pBanner)
        {
            try
            {
                Log.Information(String.Format("Actualizando banner con Id {0}.", pBanner.Id));
                Banner banner = new Banner();
                AutoMapper.Mapper.Map(pBanner, banner);
                iUnitOfWork.BannerRepository.Update(banner);
                iUnitOfWork.Complete();
                Log.Information("Banner actualizado con exito.");

            }
            catch (Exception)
            {
                Log.Error(String.Format("error al actualizar el banner con Id {0}.", pBanner.Id));
                throw;
            }

        }

        public void Create(BannerDTO pBanner)
        {
            try
            {
                Log.Information("Creando banner.");

                Banner banner = new Banner();
                AutoMapper.Mapper.Map(pBanner, banner);
                if (pBanner.Source is RSSSourceDTO)
                {
                    banner.Source = iUnitOfWork.RSSSourceRepository.Get(banner.Source.Id);
                }
                iUnitOfWork.BannerRepository.Add(banner);
                iUnitOfWork.Complete();
                Log.Information("Banner creado con exito.");

            }
            catch (Exception)
            {
                Log.Error("Error al crear el banner.");
                throw;
            }

        }

        public void Remove(BannerDTO pBanner)
        {
            try
            {
                Log.Information(String.Format("Eliminando banner con Id {0}.", pBanner.Id));


                Banner banner = iUnitOfWork.BannerRepository.Get(pBanner.Id);
                iUnitOfWork.BannerRepository.Remove(banner);
                iUnitOfWork.Complete();
                Log.Information("Banner Eliminado con exito.");
            }
            catch (Exception)
            {
                Log.Error(String.Format("Error al eliminar banner con Id {0}.", pBanner.Id));
                throw;
            }
        }

        public IEnumerable<BannerDTO> getBannersByName(string pName)
        {
            try
            {
                Log.Information(String.Format("Obteniendo banners con nombre: {0}.", pName));

                IEnumerable<Banner> banners = iUnitOfWork.BannerRepository.GetBannersByName(pName);
                Log.Information("Banners obtenidos con exito.");
                return AutoMapper.Mapper.Map<IEnumerable<BannerDTO>>(banners);
            }
            catch (Exception)
            {
                Log.Error("Error al obtener todos los banners por nombre.");
                throw;
            }

        }

        public IEnumerable<BannerDTO> getBannersActiveInDate(DateTime pDate)
        {
            try
            {
                Log.Information(String.Format("Obteniendo banners activos en: {0}.", pDate));
                IEnumerable<Banner> banners = iUnitOfWork.BannerRepository.GetBannersActiveInDate(pDate);
                Log.Information("Banners obtenidos con exito.");

                return AutoMapper.Mapper.Map<IEnumerable<BannerDTO>>(banners);
            }
            catch (Exception)
            {
                Log.Error("Error al obtener todos los banners activos en fecha.");
                throw;
            }

        }



        private void GetNextActiveBannersLoop()
        {
            Log.Information(String.Format("Iniciando bucle para obtener banners a mostrar en {0} Minutos.", UPDATE_TIME.Minutes));
            RunPeriodicAsync(GetNextActiveBanners, UPDATE_TIME, cancellationToken);
        }

        private void UpdateBannerListsLoop()
        {
            Log.Information(String.Format("Iniciando bucle para actualizar banners a mostrar cada {0} Segundos.", REFRESH_TIME.Seconds));
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
            Log.Information("Actualizando texto de banners activos.");
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
                Log.Information("Texto notificado a observadores.");

            }
        }

        private void GetNextActiveBanners()
        {
            // Obtiene los banners que estaran activos en algun momento de los siguientes <UPDATE_TIME_IN_MINUTES> minutos
            var now = DateTime.Now;
            var actualTimespan = new TimeSpan(now.Hour, now.Minute, 0);
            iCurrentBanners.Clear();


            // Obtiene los banners de la BD
            Log.Information("Obteniendo banners actualmente activos");
            iNextBanners = iUnitOfWork.BannerRepository.GetBannersActiveInRange(now, actualTimespan, actualTimespan.Add(UPDATE_TIME)).ToList();
            Log.Information("Banners obtenidos con exito");

            // Actualiza los feeds RSS de los banners
            UpdateRSSSources();
        }

        private void UpdateRSSSources()
        {
            Log.Information("Actualizando fuentes RSS.");

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
                Log.Information("Leyendo fuente RSS.");
                var newRSSItems = rSSReader.Read(uri).ToList();
                Log.Information("Asignando items RSS a la fuente.");
                if (newRSSItems != null && newRSSItems.Count > 0)
                {
                    source.RSSItems = AutoMapper.Mapper.Map<IList<RSSItemDTO>, IList<RSSItem>>(newRSSItems);
                }
            }
            catch (Exception)
            {
                Log.Error("Error al actualizar Fuentes RSS");
                throw;
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
                Log.Information("Se ha subscrito un nuevo observador al servicio de banner");
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
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
