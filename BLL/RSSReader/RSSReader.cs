using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalSignage.DTO;

namespace DigitalSignage.BLL.RSSReader
{
    public abstract class RSSReader : IRSSReader
    {
        public IEnumerable<RSSItemDTO> Read(String pUrl)
        {
            if (String.IsNullOrWhiteSpace(pUrl))
            {
                throw new ArgumentException("pUrl");
            }

            return this.Read(new Uri(pUrl));
        }

        public abstract IEnumerable<RSSItemDTO> Read(Uri pUri);
    }
}
