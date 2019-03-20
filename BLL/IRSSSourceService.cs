using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.BLL
{
    interface IRSSSourceService
    {
        IEnumerable<RSSSourceDTO> GetAll();

        RSSSourceDTO Get(int pId);

        void Update(RSSSourceDTO pRSSSourceDTO);

        void Create(RSSSourceDTO pRSSSourceDTO);

        void Remove(RSSSourceDTO pRSSSourceDTO);
    }
}
