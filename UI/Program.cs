using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DigitalSignage.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AutoMapperConfig.RegisterMappings();
            //addTestData();
            Application.Run(new HomeForm());
        }


        static void addTestData()
        {
            DigitalSignageDbContext context = new DigitalSignageDbContext();
            UnitOfWork uow = new UnitOfWork(new DigitalSignageDbContext());

            Campaign campaign = new Campaign()
            {
                Name = "Prueba",
                Description = "Prueba de una campaña",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2019, 08, 08),
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

            RSSSource sourceRSS = new RSSSource()
            {
                Description = "Prueba de source",
                Url = "http://www.bbc.co.uk/mundo/temas/tecnologia/index.xml",
                RSSItems = new List<RSSItem>
                {
                    new RSSItem()
                    {
                        Date = DateTime.Now,
                        Url ="https://www.bbc.com/mundo/internacional/2016/06/160603_sociedad_corea_del_sur_miedo_ventiladores_causa_muerte_ps",
                        Title = "Por qué en Corea del Sur tanta gente tiene pánico de los ventiladores",
                        Description = "¿Qué pasa si duermes en una habitación cerrada con un ventilador encendido?",
                    }
                }
            };
            uow.RSSSourceRepository.Add(sourceRSS);
            uow.Complete();

            Banner banner1 = new Banner()
            {
                Name = "Prueba",
                Description = "Prueba de banner",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2019, 08, 08),
                Source = sourceRSS,
                SourceId = sourceRSS.Id
            };
            uow.BannerRepository.Add(banner1);

            uow.Complete();

            Text textSource = new Text()
            {
                Data = "Prueba de texto",
            };

            Banner banner2 = new Banner()
            {
                Name = "Prueba",
                Description = "Prueba de banner",
                InitialTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(0, 0, 30),
                InitialDate = new DateTime(2018, 02, 07),
                EndDate = new DateTime(2019, 08, 08),
                Source = textSource
            };
            uow.BannerRepository.Add(banner2);
        }
    }


}






