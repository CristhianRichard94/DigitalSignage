using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DTO
{
    /// <summary>
    /// Clase que representa el DTO de una campaña
    /// </summary>
    public class CampaignDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan InitialTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public IList<ImageDTO> Images { get; set; }
    }
}
