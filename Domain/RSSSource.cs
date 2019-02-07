using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public class RSSSource : BannerSource
    {
        public string Url { get; set; }

        public IList<RSSItem> RSSItems { get; set; }
        
        public override string GetText()
        {
            return "";
        }
    }
}
