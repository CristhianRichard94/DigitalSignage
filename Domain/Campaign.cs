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
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan InitialTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Image[] Images {get; set;}
    }
}
