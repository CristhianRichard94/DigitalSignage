using System;
using System.Collections.Generic;
using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalSignage.Test
{
    [TestClass]
    public class BannerRepositoryTest
    {
        UnitOfWork uow = new UnitOfWork(new DigitalSignageDbContext());

        [TestMethod]
        public void CreateBannerRepositoryTest()
        {
            Banner banner = new Banner()
            {
                Name = "Prueba",
                Description = "Prueba de una Banner",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Source = new RSSSource()
                {

                    Description = "Fuente RSS",
                    Url = "URL",
                    RSSItems = new List<RSSItem> {
                        new RSSItem()
                        {
                            Description = "Item RSS",
                            Url = "URL",
                            Title = "Titulo",
                            Date = DateTime.Now
                        }
                    }

                }
            };
            uow.BannerRepository.Add(banner);

            uow.Complete();

            IEnumerable<Banner> result = uow.BannerRepository.GetAll();

            IEnumerator<Banner> e = result.GetEnumerator();
            e.MoveNext();
            Assert.IsNotNull(e.Current);

        }

        [TestMethod]
        public void RemoveBannerRepositoryTest()
        {
            Banner banner = new Banner()
            {
                Name = "Prueba",
                Description = "Prueba de un Banner",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Source = new RSSSource()
                {

                    Description = "Fuente RSS",
                    Url = "URL",
                    RSSItems = new List<RSSItem> {
                        new RSSItem()
                        {
                            Description = "Item RSS",
                            Url = "URL",
                            Title = "Titulo",
                            Date = DateTime.Now
                        }
                    }

                }
            };
            uow.BannerRepository.Add(banner);

            uow.Complete();

            IEnumerable<Banner> result = uow.BannerRepository.GetAll();

            IEnumerator<Banner> e = result.GetEnumerator();
            e.MoveNext();

            uow.BannerRepository.Remove(banner);

            uow.Complete();

            Assert.IsNull(uow.BannerRepository.Get(banner.Id));

        }

        [TestMethod]
        public void GetBannerRepository()
        {
            Banner banner = new Banner()
            {
                Name = "Prueba",
                Description = "Prueba de un Banner",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Source = new RSSSource()
                {

                    Description = "Fuente RSS",
                    Url = "URL",
                    RSSItems = new List<RSSItem> {
                        new RSSItem()
                        {
                            Description = "Item RSS",
                            Url = "URL",
                            Title = "Titulo",
                            Date = DateTime.Now
                        }
                    }

                }
            };
            uow.BannerRepository.Add(banner);

            uow.Complete();

            Assert.AreEqual(banner, uow.BannerRepository.Get(banner.Id));

        }
    }
}
