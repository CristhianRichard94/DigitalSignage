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

            this.HasKey(pBanner => pBanner.Id)
                .Property(pBanner => pBanner.Id)
                .HasColumnName("BannerId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            
            this.Property(pBanner => pBanner.Name)
                .IsRequired();

            this.Property(pBanner => pBanner.Description)
                .IsRequired();

            this.Property(pBanner => pBanner.InitialDate)
                .HasColumnName("InitDate")
                .IsRequired();

            this.Property(pBanner => pBanner.EndDate)
                .IsRequired();

            this.Property(pBanner => pBanner.InitialTime)
                .HasColumnName("InitTime")
                .IsRequired();

            this.Property(pBanner => pBanner.EndDate)
                .IsRequired();

            this.Property(pBanner => pBanner.SourceId)
                .HasColumnName("SourceId")
                .IsRequired();

        }
    

    }
}
