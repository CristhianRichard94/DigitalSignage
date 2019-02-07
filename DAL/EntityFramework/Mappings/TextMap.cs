using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class TextMap : EntityTypeConfiguration<Text>
    {
        public TextMap()
        {
            this.ToTable("Texts");

            this.Property(pText => pText.Data)
                .IsRequired();

        }

    }
}
