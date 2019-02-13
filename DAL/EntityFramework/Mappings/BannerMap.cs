using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class BannerMap : EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {
            ToTable("Banners");

            this.HasKey(pCampign => pCampign.Id)
                .Property(pCampign => pCampign.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            
            this.Property(pBanner => pBanner.Name)
                .IsRequired();

            this.Property(pBanner => pBanner.Description)
                .IsRequired();

            this.Property(pBanner => pBanner.InitialDate)
                .IsRequired();

            this.Property(pBanner => pBanner.EndDate)
                .IsRequired();

            this.Property(pBanner => pBanner.InitialTime)
                .IsRequired();

            this.Property(pBanner => pBanner.EndDate)
                .IsRequired();

        }
    

    }
}
