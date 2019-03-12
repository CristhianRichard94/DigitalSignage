using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    public class CampaignRepository : Repository<Campaign, DigitalSignageDbContext>, ICampaignRepository
    {
        public CampaignRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }


        public void Update(Campaign updatedCampaign)
        {
            //Verifica y actualiza las Imagenes relacionadas con la campaña
            var oldCampaign = this.iDbContext.Campaigns
                .Include("Images")
                .SingleOrDefault(p => p.Id == updatedCampaign.Id);

            if (oldCampaign != null)
            {
                // Actualiza el padre
                this.iDbContext.Entry(oldCampaign).CurrentValues.SetValues(updatedCampaign);

                // Elimina imagenes anteriores
                foreach (var existingImage in oldCampaign.Images.ToList())
                {
                    if (updatedCampaign.Images.All(c => c.Id != existingImage.Id))
                        this.iDbContext.Images.Remove(existingImage);
                }

                // Actualiza e inserta las nuevas imagenes
                foreach (var updatedImage in updatedCampaign.Images)
                {
                    var existingImage = oldCampaign.Images
                        .SingleOrDefault(c => c.Id == updatedImage.Id);

                    if (existingImage != null)
                        // Actualiza los hijos
                        this.iDbContext.Entry(existingImage).CurrentValues.SetValues(updatedImage);
                    else
                    {
                        // Inserta hijo
                        var newImage = new Image()
                        {
                            Description = updatedImage.Description,
                            Data= updatedImage.Data,
                            Duration = updatedImage.Duration,
                            Position= updatedImage.Position
                        };
                        oldCampaign.Images.Add(newImage);
                    }
                }

                this.iDbContext.SaveChanges();
            }
        }

        public IEnumerable<Campaign> GetCampaignsByName(string pName)
        {
            if (pName == null)
                throw new ArgumentNullException("pName");

            return base.iDbContext.Set<Campaign>()
                //Busca las campañas que contengan el nombre especificado
                .Where(c => c.Name.IndexOf(pName) >= 0)
                .ToList();
        }

        public IEnumerable<Campaign> GetCampaignsActiveInDate(DateTime pDate)
        {
            if (pDate == null)
                throw new ArgumentNullException("pDate");

            return base.iDbContext.Set<Campaign>()
                //Compara si la campaña esta activa en la fecha
                .Where(c => DbFunctions.TruncateTime(c.InitialDate) <= DbFunctions.TruncateTime(pDate))
                .Where(c => DbFunctions.TruncateTime(c.EndDate) >= DbFunctions.TruncateTime(pDate))
                .ToList();
        }

        public IEnumerable<Campaign> GetCampaignsActiveInRange(DateTime pDate, TimeSpan pFromTime, TimeSpan pToTime)
        {
            if (pDate == null)
                throw new ArgumentNullException("pDate");
            if (pFromTime == null)
                throw new ArgumentNullException("pTimeFrom");
            if (pToTime == null)
                throw new ArgumentNullException("pTimeTo");
            if (pFromTime.CompareTo(pToTime) > -1)
                throw new InvalidOperationException("El tiempo de inicio debe ser menor al de fin");

            return base.iDbContext.Set<Campaign>()
                //Compara si la campaña esta activa en la fecha
                .Where(b => DbFunctions.TruncateTime(b.InitialDate) <= DbFunctions.TruncateTime(pDate))
                .Where(b => DbFunctions.TruncateTime(b.EndDate) >= DbFunctions.TruncateTime(pDate))
                //Compara si la campaña esta activa en el rango horario
                .Where(b => b.InitialTime <= pToTime && b.EndTime >= pFromTime)
                .ToList();
        }
    }
}
