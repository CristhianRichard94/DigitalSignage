using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using DigitalSignage.Domain;
using System.IO;

namespace DigitalSignage.DAL.EntityFramework
{
    /// <summary>
    /// Clase que inicializa la database y agrega datos de prueba
    /// </summary>
    public class DatabaseInitialization : DropCreateDatabaseIfModelChanges<DigitalSignageDbContext>
    {
        protected override void Seed(DigitalSignageDbContext pContext)
        {

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
            pContext.Campaigns.Add(campaign);

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
            pContext.RSSSources.Add(sourceRSS);

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

            pContext.Banners.Add(banner1);

            TextSource textSource = new TextSource()
            {
                Data = "Prueba de texto",
            };
            pContext.Texts.Add(textSource);

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

            pContext.Banners.Add(banner2);

            base.Seed(pContext);
        }
    }
}
