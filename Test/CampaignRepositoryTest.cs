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

        /// <summary>
        /// Prueba de agregar una camapaña al repositorio
        /// </summary>
        [TestMethod]
        public void AddCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
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


            Assert.IsNotNull(uow.CampaignRepository.Get(campaign.Id));

        }

        /// <summary>
        /// Prueba de actualizar una camapaña del repositorio
        /// </summary>
        [TestMethod]
        public void UpdateCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
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

            Campaign updatedCampaign = new Campaign()
            {
                Id = campaign.Id,
                Name = "Prueba2",
                Description = "Prueba de una campaña2",
                InitialTime = new TimeSpan(0, 8, 1),
                EndTime = new TimeSpan(0, 7, 30),
                InitialDate = new DateTime(2015, 02, 07),
                EndDate = new DateTime(2017, 02, 08),
                Images = new List<Image>()
                {
                        new Image()
                        {
                            Description = "asdasdasdasd",
                            Duration = 2,
                            Position = 1,
                            Data = File.ReadAllBytes("../../../assets/images/1.jpg")
                        },
                },
            };
            uow.CampaignRepository.Update(updatedCampaign);

            uow.Complete();

            Assert.AreEqual(campaign, uow.CampaignRepository.Get(updatedCampaign.Id));

        }

        /// <summary>
        /// Prueba de eliminar una camapaña del repositorio
        /// </summary>
        [TestMethod]
        public void RemoveCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
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

            uow.CampaignRepository.Remove(campaign);

            uow.Complete();

            Assert.IsNull(uow.CampaignRepository.Get(campaign.Id));

        }

        /// <summary>
        /// Prueba de obtener una camapaña del repositorio
        /// </summary>
        [TestMethod]
        public void GetCampaignRepository()
        {
            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
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
