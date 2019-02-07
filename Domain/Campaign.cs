using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public class Campaign
    {
        public int Id { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan InitialTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public IList<Image> Images {get; set;}
    }
}
