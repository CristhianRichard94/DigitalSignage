using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        public int Position { get; set; }

        public int CampaignId { get; set; }

    }
}
