using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DTO
{
    public class RSSSourceDTO : BannerSourceDTO
    {

        public string Url { get; set; }

        public string Description { get; set; }

        public IList<RSSItemDTO> RssItems { get; set; }
    }
}
