using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework
{
    public class TextRepository: Repository<Text, DigitalSignageDbContext>, ITextRepository
    {
        public TextRepository(DigitalSignageDbContext pContext) : base(pContext)
        {

        }
    }
}
