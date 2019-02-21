using System;
using System.Collections.Generic;
using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalSignage.Test
{
    [TestClass]
    public class RSSSourceRepositoryTest
    {

        UnitOfWork uow = new UnitOfWork(new DigitalSignageDbContext());


        [TestMethod]
        public void CreateRSSSourceRepository()
        {
            RSSSource sourceRSS = new RSSSource()
            {
                Description = "Fuente RSS de prueba",
                Url = "URL de prueba",
                RSSItems = new List<RSSItem>()
                {
                    new RSSItem()
                    {
                        Url = "Url Item 1",
                        Title = "Item 1",
                        Description = "Item 1 RSSSource prueba",
                        Date = DateTime.Now
                    },
                    new RSSItem()
                    {
                        Url = "Url Item 2",
                        Title = "Item 2",
                        Description = "Item 2 RSSSource prueba",
                        Date = DateTime.Now
                    },
                    new RSSItem()
                    {
                        Url = "Url Item 3",
                        Title = "Item 3",
                        Description = "Item 3 RSSSource prueba",
                        Date = DateTime.Now
                    }
                }
            };

            uow.RSSSourceRepository.Add(sourceRSS);

            uow.Complete();

            IEnumerable<RSSSource> result = uow.RSSSourceRepository.GetAll();

            IEnumerator<RSSSource> e = result.GetEnumerator();
            e.MoveNext();
            Assert.IsNotNull(e.Current);
        }

        [TestMethod]
        public void RemoveRSSSourceRepository()
        {
            RSSSource sourceRSS = new RSSSource()
            {
                Description = "Fuente RSS de prueba",
                Url = "URL de prueba",
                RSSItems = new List<RSSItem>()
                {
                    new RSSItem()
                    {
                        Url = "Url Item 1",
                        Title = "Item 1",
                        Description = "Item 1 RSSSource prueba",
                        Date = DateTime.Now
                    },
                    new RSSItem()
                    {
                        Url = "Url Item 2",
                        Title = "Item 2",
                        Description = "Item 2 RSSSource prueba",
                        Date = DateTime.Now
                    },
                    new RSSItem()
                    {
                        Url = "Url Item 3",
                        Title = "Item 3",
                        Description = "Item 3 RSSSource prueba",
                        Date = DateTime.Now
                    }
                }
            };

            uow.RSSSourceRepository.Add(sourceRSS);

            uow.Complete();

            IEnumerable<RSSSource> result = uow.RSSSourceRepository.GetAll();

            IEnumerator<RSSSource> e = result.GetEnumerator();
            e.MoveNext();

            uow.RSSSourceRepository.Remove(sourceRSS);

            uow.Complete();

            Assert.IsNull(uow.RSSSourceRepository.Get(sourceRSS.Id));

        }

        [TestMethod]
        public void GetCampaignRepository()
        {
            RSSSource sourceRSS = new RSSSource()
            {
                Description = "Fuente RSS de prueba",
                Url = "URL de prueba",
                RSSItems = new List<RSSItem>()
                {
                    new RSSItem()
                    {
                        Url = "Url Item 1",
                        Title = "Item 1",
                        Description = "Item 1 RSSSource prueba",
                        Date = DateTime.Now
                    },
                    new RSSItem()
                    {
                        Url = "Url Item 2",
                        Title = "Item 2",
                        Description = "Item 2 RSSSource prueba",
                        Date = DateTime.Now
                    },
                    new RSSItem()
                    {
                        Url = "Url Item 3",
                        Title = "Item 3",
                        Description = "Item 3 RSSSource prueba",
                        Date = DateTime.Now
                    }
                }
            };

            uow.RSSSourceRepository.Add(sourceRSS);

            uow.Complete();

            Assert.AreEqual(sourceRSS, uow.RSSSourceRepository.Get(sourceRSS.Id));

        }
    }
}
