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

        /// <summary>
        /// Prueba de agregar una fuente RSS al repositorio
        /// </summary>
        [TestMethod]
        public void AddRSSSourceRepository()
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

        /// <summary>
        /// Prueba de actualizar una fuente RSS del repositorio
        /// </summary>
        [TestMethod]
        public void UpdateRSSSourceRepository()
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

            //Actualizando fuente
            RSSSource source2 = e.Current;
            source2.Description = "Fuente RSS2";
            source2.Url = "URL2";
            source2.RSSItems = new List<RSSItem>()
                {
                    new RSSItem()
                    {
                        Url = "Url1",
                        Title = "Item1",
                        Description = "Item 1.2",
                        Date = DateTime.Now
                    },
                };

            uow.RSSSourceRepository.Update(sourceRSS);
            uow.Complete();

            Assert.AreNotEqual(sourceRSS, uow.RSSSourceRepository.Get(source2.Id));

        }

        /// <summary>
        /// Prueba de eliminar una fuente RSS del repositorio
        /// </summary>
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

            uow.RSSSourceRepository.Remove(sourceRSS);

            uow.Complete();

            Assert.IsNull(uow.RSSSourceRepository.Get(sourceRSS.Id));

        }

        /// <summary>
        /// Prueba de obtener una fuente RSS del repositorio
        /// </summary>
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
