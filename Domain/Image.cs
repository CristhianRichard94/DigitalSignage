using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public class Image
    {
        public int Id { get; }

        public byte[] Data { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        public int Position { get; set; }

        public int CampaignId { get; set; }

    }
}
