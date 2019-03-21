using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    /// <summary>
    /// Clase de mapeo de imagenes en la DB
    /// </summary>
    class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            // Tabla en la que se mapea la entidad
            this.ToTable("Images");

            // Clave primaria de la entidad, se encuentra en la columna ImageId, generada por la DB
            this.HasKey(pImage => pImage.Id)
                .Property(pImage => pImage.Id)
                .HasColumnName("ImageId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            // Propiedades Requeridas (No nulas)
            this.Property(pImage => pImage.Description)
                .IsRequired();

            this.Property(pImage => pImage.Data)
                .IsRequired();

            this.Property(pImage => pImage.Position)
                .IsRequired();

        }

    }
}
