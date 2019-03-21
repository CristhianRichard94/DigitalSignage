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

        private string iCurrentText;

        public BannerService()
        {
            this.iUnitOfWork = new UnitOfWork(new DigitalSignageDbContext());
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
    }
}
