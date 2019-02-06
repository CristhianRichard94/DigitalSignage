using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    public class RSSSourceRepository : Repository<RSSSource, DigitalSignageDbContext>, IRSSSourceRepository
    {
        public RSSSourceRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }
    }
}
