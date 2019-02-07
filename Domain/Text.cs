using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public class Text : BannerSource
    {
        public string Data { get; set; }

        public override string GetText()
        {
            return Data;
        }
    }
}
