using System;
using System.Collections.Generic;
using System.IO;
using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalSignage.Test
{
    [TestClass]
    public class CampaignRepositoryTest
    {
        UnitOfWork uow = new UnitOfWork(new DigitalSignageDbContext());

        [TestMethod]
        public void CreateCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0,0,1),
                EndTime = new TimeSpan(0,0,30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Images = new List<Image>
                {
                    new Image()
                    {
                        Description = "Imagen 1",
                        Duration = 1,
                        Position = 1,
                        Data = File.ReadAllBytes("../../../assets/images/1.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 2",
                        Duration = 2,
                        Position = 2,
                        Data = File.ReadAllBytes("../../../assets/images/2.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 3",
                        Duration = 3,
                        Position = 3,
                        Data = File.ReadAllBytes("../../../assets/images/3.jpeg")
                    },
                }
            };
            uow.CampaignRepository.Add(campaign);

            uow.Complete();

            IEnumerable<Campaign> result = uow.CampaignRepository.GetAll();

            IEnumerator<Campaign> e = result.GetEnumerator();
            e.MoveNext();
            Assert.IsNotNull(e.Current);

        }


        [TestMethod]
        public void UpdateCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0,0,1),
                EndTime = new TimeSpan(0,0,30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Images = new List<Image>
                {
                    new Image()
                    {
                        Description = "Imagen 1",
                        Duration = 1,
                        Position = 1,
                        Data = File.ReadAllBytes("../../../assets/images/1.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 2",
                        Duration = 2,
                        Position = 2,
                        Data = File.ReadAllBytes("../../../assets/images/2.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 3",
                        Duration = 3,
                        Position = 3,
                        Data = File.ReadAllBytes("../../../assets/images/3.jpeg")
                    },
                }
            };
            uow.CampaignRepository.Add(campaign);

            uow.Complete();

            IEnumerable<Campaign> result = uow.CampaignRepository.GetAll();

            IEnumerator<Campaign> e = result.GetEnumerator();
            e.MoveNext();

            Campaign camp = e.Current;
            camp.Name = "Prueba2";
            camp.Description = "Prueba de una campaña2";
            camp.InitialTime = new TimeSpan(0, 8, 1);
            camp.EndTime = new TimeSpan(0, 7, 30);
            camp.InitialDate = new DateTime(2015, 02, 07);
            camp.EndDate = new DateTime(2017, 02, 08);
            camp.Images = new List<Image>()
            {
                new Image()
                {
                    Description = "Imagen 2",
                    Duration = 2,
                    Position = 2,
                    Data = File.ReadAllBytes("../../../assets/images/2.jpg")
                },
            };
            uow.CampaignRepository.Update(camp);

            uow.Complete();

            Assert.AreEqual(camp, uow.CampaignRepository.Get(camp.Id));

        }

        [TestMethod]
        public void RemoveCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0,0,1),
                EndTime = new TimeSpan(0,0,30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Images = new List<Image>
                {
                    new Image()
                    {
                        Description = "Imagen 1",
                        Duration = 1,
                        Position = 1,
                        Data = File.ReadAllBytes("../../../assets/images/1.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 2",
                        Duration = 2,
                        Position = 2,
                        Data = File.ReadAllBytes("../../../assets/images/2.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 3",
                        Duration = 3,
                        Position = 3,
                        Data = File.ReadAllBytes("../../../assets/images/3.jpeg")
                    },
                }
            };
            uow.CampaignRepository.Add(campaign);

            uow.Complete();

            IEnumerable<Campaign> result = uow.CampaignRepository.GetAll();

            IEnumerator<Campaign> e = result.GetEnumerator();
            e.MoveNext();

            uow.CampaignRepository.Remove(campaign);

            uow.Complete();

            Assert.IsNull(uow.CampaignRepository.Get(campaign.Id));

        }

        [TestMethod]
        public void GetCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0,0,1),
                EndTime = new TimeSpan(0,0,30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Images = new List<Image>
                {
                    new Image()
                    {
                        Description = "Imagen 1",
                        Duration = 1,
                        Position = 1,
                        Data = File.ReadAllBytes("../../../assets/images/1.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 2",
                        Duration = 2,
                        Position = 2,
                        Data = File.ReadAllBytes("../../../assets/images/2.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 3",
                        Duration = 3,
                        Position = 3,
                        Data = File.ReadAllBytes("../../../assets/images/3.jpeg")
                    },
                }
            };
            uow.CampaignRepository.Add(campaign);

            uow.Complete();

            Assert.AreEqual(campaign, uow.CampaignRepository.Get(campaign.Id));

        }


    }
}
