﻿using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.BLL
{
    /// <summary>
    /// Interfaz del servicio de Banners
    /// </summary>
    public interface IBannerService : IObservable<IList<string>>
    {
        IEnumerable<BannerDTO> GetAll();

        BannerDTO Get(int pId);

        void Update(BannerDTO pBanner);

        void Create(BannerDTO pBanner);

        void Remove(BannerDTO pBanner);

        IEnumerable<BannerDTO> getBannersByName(string pName);

        IEnumerable<BannerDTO> getBannersActiveInDate(DateTime pDate);

        void StartAsyncTasks();

        void CancelAsyncTasks();

    }
}
