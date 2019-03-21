using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    /// <summary>
    /// Clase que implementa el repositorio de campañas
    /// </summary>
    public class CampaignRepository : Repository<Campaign, DigitalSignageDbContext>, ICampaignRepository
    {
        public CampaignRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }

        /// <summary>
        /// Obtiene todas las campañas
        /// </summary>
        /// <returns>Enumerable de campañas</returns>
        public override IEnumerable<Campaign> GetAll()
        {
            IEnumerable<Campaign> campaigns = base.GetAll();
            foreach (Campaign campaign in campaigns)
            {
                //Incluye las imagenes de la campaña
                this.iDbContext.Entry(campaign).Collection(p => p.Images).Load();
            }
            return campaigns;
        }

        /// <summary>
        /// Actualiza una campaña
        /// </summary>
        /// <param name="updatedCampaign">Campaña actualizada</param>
        public void Update(Campaign updatedCampaign)
        {
            //Verifica y actualiza las Imagenes relacionadas con la campaña
            var oldCampaign = this.iDbContext.Campaigns
                .Include("Images")
                .SingleOrDefault(p => p.Id == updatedCampaign.Id);

            if (oldCampaign != null)
            {
                // Actualiza la campaña
                this.iDbContext.Entry(oldCampaign).CurrentValues.SetValues(updatedCampaign);

                // Elimina imagenes
                foreach (var existingImage in oldCampaign.Images.ToList())
                {
                    if (!updatedCampaign.Images.Any(c => c.Id == existingImage.Id))
                        this.iDbContext.Images.Remove(existingImage);
                }

                // Actualiza e inserta nuevas imagenes
                foreach (var updatedImage in updatedCampaign.Images)
                {
                    var existingImage = oldCampaign.Images
                        .Where(c => c.Id == updatedImage.Id)
                        .SingleOrDefault();

                    if (existingImage != null)
                        // Actualiza imagen
                        this.iDbContext.Entry(existingImage).CurrentValues.SetValues(updatedImage);
                    else
                    {
                        // Inserta imagen
                        var newImage = new Image()
                        {
                            Description = updatedImage.Description,
                            Data = updatedImage.Data,
                            Duration = updatedImage.Duration,
                            Position = updatedImage.Position
                        };
                        oldCampaign.Images.Add(newImage);
                    }
                }

                this.iDbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Obtiene campañas que cumplan contengan una cadena en su nombre
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public IEnumerable<Campaign> GetCampaignsByName(string pName)
        {
            if (pName == null)
                throw new ArgumentNullException("pName");

            return base.iDbContext.Set<Campaign>()
                //Busca las campañas que contengan el nombre especificado
                .Where(c => c.Name.IndexOf(pName) >= 0)
                .Include("Images")
                .ToList();
        }

        /// <summary>
        /// Obtiene las campañas activas en una fecha
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
        public IEnumerable<Campaign> GetCampaignsActiveInDate(DateTime pDate)
        {
            if (pDate == null)
                throw new ArgumentNullException("pDate");

            return base.iDbContext.Set<Campaign>()
                //Compara si la campaña esta activa en la fecha
                .Where(c => DbFunctions.TruncateTime(c.InitialDate) <= DbFunctions.TruncateTime(pDate))
                .Where(c => DbFunctions.TruncateTime(c.EndDate) >= DbFunctions.TruncateTime(pDate))
                .Include("Images")
                .ToList();
        }

        /// <summary>
        /// Obtiene las campañas activas en un rango horario en una fecha
        /// </summary>
        /// <param name="pDate">Fecha</param>
        /// <param name="pFromTime">Tiempo de inicio</param>
        /// <param name="pToTime">Tiempo de fin</param>
        /// <returns></returns>
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
                .Include("Images")
                .ToList();
        }
    }
}
