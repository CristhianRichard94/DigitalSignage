using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public class RSSSource : Banner
    {
        public string Url { get; set; }
        public RSSItem RSSItems { get; set; }
        
    }
}
