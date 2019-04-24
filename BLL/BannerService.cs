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
        private List<IObserver<IList<string>>> observers;

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

        /// <summary>
        /// Texto actual a mostrar, enviado a los observadores
        /// </summary>
        private IList <string> iCurrentText = new List<string>();

        /// <summary>
        /// Token para verificar si existen solicitudes de cancelacion
        /// </summary>
        private CancellationToken cancellationToken;

        /// <summary>
        ///  Token para cancelar tareas asincronas
        /// </summary>
        private CancellationTokenSource tokenSource;



        public BannerService()
        {
            iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());
            observers = new List<IObserver<IList<string>>>();
            iCurrentBanners = new List<Banner>();

            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;

        }


        /// <summary>
        /// Obtiene todos los banners
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Obtiene el banner del ID especificado
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public BannerDTO Get(int pId)
        {
            try
            {
                Log.Information(String.Format("Obteniendo banner con Id {0}.", pId));
                var banner = iUnitOfWork.BannerRepository.Get(pId);
                Log.Information("Banner obtenido con exito.");
                if (banner == null)
                {
                    return null;
                }
                else
                {
                    var bannerDTO = new BannerDTO();
                    AutoMapper.Mapper.Map(banner, bannerDTO);
                    return bannerDTO;
                }
            }
            catch (Exception)
            {
                Log.Error("Error al obtener banner.");
                throw;
            }


        }


        /// <summary>
        /// Actualiza el banner especficado
        /// </summary>
        /// <param name="pBanner"></param>
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

        /// <summary>
        /// Crea el banner especificado
        /// </summary>
        /// <param name="pBanner"></param>
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

        /// <summary>
        /// Elimina el banner especificado
        /// </summary>
        /// <param name="pBanner"></param>
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


        /// <summary>
        /// Obtiene los banners que contienen en su nombre pName
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Obtiene los banners activos en cierta fecha
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Inicia una tarea asincrona de actualización de siguientes banners
        /// </summary>
        private void GetNextActiveBannersLoop()
        {
            Log.Information(String.Format("Iniciando bucle para obtener banners a mostrar en {0} Minutos.", UPDATE_TIME.Minutes));
            RunPeriodicAsync(GetNextActiveBanners, UPDATE_TIME, cancellationToken);
        }

        /// <summary>
        /// Inicia una tarea asincrona de actualización de banners
        /// </summary>
        private void UpdateBannerListsLoop()
        {
            Log.Information(String.Format("Iniciando bucle para actualizar banners a mostrar cada {0} Segundos.", REFRESH_TIME.Seconds));
            RunPeriodicAsync(UpdateBannerLists, REFRESH_TIME, cancellationToken);

        }

        /// <summary>
        /// Actualiza la lista de banners activos en base a iNextBanners
        /// </summary>
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

        /// <summary>
        /// Actualiza el texto actual a mostrar con el texto de los banners y lo notifica a los observadores
        /// </summary>
        private void UpdateCurrentText()
        {
            Log.Information("Actualizando texto de banners activos.");
            List<string> updatedText = new List<string>();
            // Concatena los textos de los banners activos
            foreach (Banner banner in iCurrentBanners)
            {
                IList<string> bannerTexts = banner.GetText();
                foreach (string text in bannerTexts)
                {
                    updatedText.Add(text);
                }
            }

            iCurrentText = updatedText;

            foreach (var observer in observers)
            {


                if (iCurrentBanners.Count > 0)
                {
                    // Envia al nuevo observador el texto actual.
                    observer.OnNext(iCurrentText);
                }
                else
                {
                    // Envia al nuevo observador una lista vacía para indicar que no hay banners.
                    observer.OnNext(new List<String>());
                }
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


        /// <summary>
        /// Ejecuta tareas de actualizacion de feeds de las fuentes RSS activas 
        /// </summary>
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

        /// <summary>
        /// Actualiza los feeds de una fuente RSS
        /// </summary>
        /// <param name="source"></param>
        private void readFeeds(RSSSource source)
        {
            try
            {
                IRSSReader rSSReader = new XMLRSSReader();
                Uri uri;
                Uri.TryCreate(source.Url, UriKind.Absolute, out uri);
                Log.Information("Leyendo fuente RSS.");
                var newRSSItems = rSSReader.Read(uri).ToList();
                // Si se pudo conectar y obtuvo al menos un feed nuevo, actualiza la lista almacenada
                if (newRSSItems != null && newRSSItems.Count > 0)
                {
                    Log.Information("Asignando items RSS a la fuente.");
                    source.RSSItems = AutoMapper.Mapper.Map<IList<RSSItemDTO>, IList<RSSItem>>(newRSSItems);
                }
            }
            catch (Exception)
            {
                Log.Error("Error al actualizar Fuentes RSS.");
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
        public IDisposable Subscribe(IObserver<IList<string>> observer)
        {
            // verifica que el observador no exista en la lista
            if (!observers.Contains(observer))
            {
                Log.Information("Se ha subscrito un nuevo observador al servicio de banner");
                observers.Add(observer);
                if (iCurrentBanners.Count > 0)
                {
                    // Envia al nuevo observador el texto actual.
                    observer.OnNext(iCurrentText);
                } else
                {
                    // Envia al nuevo observador una lista vacía para indicar que no hay banners.
                    observer.OnNext(new List<String>());
                }
                Log.Information("Texto notificado a nuevo observador.");


            }
            return new Unsubscriber<IList<string>>(observers, observer);
        }


        /// <summary>
        /// Inicia tareas asincronas de actualizacion de banners a mostrar
        /// </summary>
        public void StartAsyncTasks()
        {
            GetNextActiveBannersLoop();
            UpdateBannerListsLoop();
        }


        /// <summary>
        /// Solicita la cancelación de las tareas asincronas corriendo
        /// </summary>
        public void CancelAsyncTasks()
        {
            tokenSource.Cancel();
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
