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
    /// Clase de mapeo de Banner en la DB
    /// </summary>
    class BannerMap : EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {
            // Tabla en la que se mapea
            ToTable("Banners");

            // Clave primaria de la entidad, se encuentra en la columna BannerId, generada por la DB
            this.HasKey(pBanner => pBanner.Id)
                .Property(pBanner => pBanner.Id)
                .HasColumnName("BannerId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            // Propiedades Requeridas (No nulas)

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
