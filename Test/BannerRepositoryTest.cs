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

        /// <summary>
        /// Prueba de agregar un banner al repositorio
        /// </summary>
        [TestMethod]
        public void AddBannerRepositoryTest()
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

            Assert.IsNotNull(uow.BannerRepository.Get(banner.Id));

        }

        /// <summary>
        /// Prueba de actualizar un banner del repositorio
        /// </summary>
        [TestMethod]
        public void UpdateBannerRepository()
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

            Banner updatedBanner = uow.BannerRepository.Get(banner.Id);
            updatedBanner.Name = "Prueba2";
            updatedBanner.Description = "Prueba de un banner 2";
            updatedBanner.InitialTime = new TimeSpan(0, 8, 1);
            updatedBanner.EndTime = new TimeSpan(0, 7, 30);
            updatedBanner.InitialDate = new DateTime(2015, 02, 07);
            updatedBanner.EndDate = new DateTime(2017, 02, 08);
            updatedBanner.Source = new RSSSource()
            {

                Description = "Fuente RSS actualizada",
                Url = "URL actualizada",
                RSSItems = new List<RSSItem>
                {
                    new RSSItem()
                    {
                        Description = "Item RSS2",
                        Url = "URL2",
                        Title = "Titulo2",
                        Date = DateTime.Now
                    }
                }

            };
            uow.BannerRepository.Update(updatedBanner);

            uow.Complete();

            Assert.AreEqual(updatedBanner, uow.BannerRepository.Get(updatedBanner.Id));

        }


        /// <summary>
        /// Prueba de eliminar un banner del repositorio
        /// </summary>
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

            uow.BannerRepository.Remove(banner);

            uow.Complete();

            Assert.IsNull(uow.BannerRepository.Get(banner.Id));

        }

        /// <summary>
        /// Prueba de obtener un banner del repositorio
        /// </summary>
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

            Assert.AreEqual(uow.BannerRepository.Get(banner.Id), banner);

        }
    }
}
