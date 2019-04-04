using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DTO
{
    /// <summary>
    /// Clase que representa el DTO de una fuente RSS
    /// </summary>
    public class RSSSourceDTO : BannerSourceDTO
    {
        public string Url { get; set; }

        public string Description { get; set; }

        public IList<RSSItemDTO> RSSItems { get; set; }
    }
}
