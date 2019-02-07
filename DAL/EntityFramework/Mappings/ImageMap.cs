using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            this.ToTable("Images");

            this.HasKey(pImage => pImage.Id)
                .Property(pImage => pImage.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.Property(pImage => pImage.Description)
                .IsRequired();

            this.Property(pImage => pImage.Data)
                .IsRequired();

            this.Property(pImage => pImage.Duration)
                .IsRequired();

            this.Property(pImage => pImage.Position)
                .IsRequired();

            this.Property(pImage => pImage.CampaignId)
                .IsRequired();

        }

    }
}
