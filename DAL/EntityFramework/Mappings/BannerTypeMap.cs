using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class BannerTypeMap : EntityTypeConfiguration<BannerSource>
    {
        public void BannerType()
        {
            ToTable("BannerTypes");

            this.HasKey(pBannerType => pBannerType.Id)
                .Property(pBannerType => pBannerType.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}
