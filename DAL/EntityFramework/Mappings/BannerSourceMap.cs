using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class BannerSourceMap : EntityTypeConfiguration<BannerSource>
    {
        public void BannerSource()
        {
            ToTable("BannerSources");

            this.HasKey(pBannerSource => pBannerSource.Id)
                .Property(pBannerSource => pBannerSource.Id)
                .HasColumnName("BannerSourceId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}
