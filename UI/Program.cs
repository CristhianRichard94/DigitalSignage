using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
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
            DigitalSignageDbContext context = new DigitalSignageDbContext();
            UnitOfWork uow = new UnitOfWork(new DigitalSignageDbContext());

        /*   Campaign campaign = new Campaign()
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
            */
            IEnumerable<Campaign> result = uow.CampaignRepository.GetAll();
            IEnumerator<Campaign> e = result.GetEnumerator();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CampaignForm(e));
        }
    }
}
