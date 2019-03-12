using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DTO
{
    public class RSSItemDTO
    {

        public String Title { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }
        public DateTime? Date { get; set; }
    }
}
