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
                .HasColumnName("ImageId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.Property(pImage => pImage.Description)
                .IsRequired();

            this.Property(pImage => pImage.Data)
                .HasColumnName("Bytes")
                .IsRequired();

            this.Property(pImage => pImage.Position)
                .HasColumnName("Order")
                .IsRequired();

        }

    }
}
