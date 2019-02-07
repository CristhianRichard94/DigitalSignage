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
                Name = "Campaña de prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(),
                EndTime = new TimeSpan(2),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2018, 02, 08),
                Images = new List<Image>
                {
                    new Image()
                    {
                        Description = "Imagen 1",
                        Duration = new TimeSpan(4),
                        Position = 1,
                        Data = File.ReadAllBytes("../../../assets/images/1.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 2",
                        Duration = new TimeSpan(8),
                        Position = 2,
                        Data = File.ReadAllBytes("../../../assets/images/2.jpg")
                    },
                    new Image()
                    {
                        Description = "Imagen 3",
                        Duration = new TimeSpan(8),
                        Position = 2,
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
    }
}
