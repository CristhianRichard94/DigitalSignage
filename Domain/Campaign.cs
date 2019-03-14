using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.Domain
{
    public class Campaign
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan InitialTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public IList<Image> Images {get; set;}

        /*
        public bool IsActiveNow()
        {

            bool isActive;
            var today = DateTime.Now;
            //se encuentra activo en la fecha
            isActive = this.InitDate.Date <= today.Date && today.Date <= this.EndDate.Date;
            isActive &= this.InitTime <= today.TimeOfDay && today.TimeOfDay <= this.EndTime;
            return isActive;

        }*/
    }
}
