using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.BLL
{
    class BannerService : IBannerService
    {
        private UnitOfWork iUnitOfWork;
        private List<IObserver<string>> observers;

        private string iCurrentText;

        public BannerService()
        {
            this.iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());

            this.observers = new List<IObserver<string>>();
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



        public void UpdateBanners()
        {
            /*
            tokenSource.Cancel();
            tokenSource.Dispose();

            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;

            GetNextActiveBannersLoop();
            UpdateBannerListsLoop();
            */
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            // Verifica que el observador no exista en la lista
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(iCurrentText);
            }
            return new Unsubscriber<string>(observers, observer);
        }
    }

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
