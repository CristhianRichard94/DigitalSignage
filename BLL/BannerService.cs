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

        private string iCurrentText = "No hay ninguna campaña activa en este momento";

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

        public IEnumerable<BannerDTO> getActiveBanners()
        {
            throw new NotImplementedException();
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
                // Envia al nuevo s observador el texto actual.
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
