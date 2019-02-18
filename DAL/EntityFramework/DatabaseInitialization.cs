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
    public class DatabaseInitialization : DropCreateDatabaseIfModelChanges<DigitalSignageDbContext>
    {
        protected override void Seed(DigitalSignageDbContext pContext)
        {

            pContext.Campaigns.Add(new Campaign()
            {
                Name = "CatName1",
                Description = "Desc1",
                InitialDate = new DateTime(2019, 02, 13),
                EndDate = new DateTime(2019, 02, 20),
                InitialTime = new TimeSpan(0, 8, 0, 0, 0),
                EndTime = new TimeSpan(0, 12, 0, 0, 0),
                Images = new List<Image> {
                    new Image()
                    {
                        Description = "Imagen 1",
                        Position = 1,
                        Duration = 1,
                        Data = File.ReadAllBytes("../../../assets/images/1.jpeg")
                    },
                    new Image()
                    {
                        Description = "Imagen 2",
                        Position = 2,
                        Duration = 2,
                        Data = File.ReadAllBytes("../../../assets/images/2.jpeg")
                    },
                    new Image()
                    {
                        Description = "tercera imagen",
                        Position = 3,
                        Duration = 3,
                        Data = File.ReadAllBytes("../../../assets/images/3.jpeg")
                    }
                }
            });

            base.Seed(pContext);
        }
    }
}
