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
    /// Clase de mapeo de fuentes de banners en la DB
    /// </summary>
    class BannerSourceMap : EntityTypeConfiguration<BannerSource>
    {
        public void BannerSource()
        {
            //Tabla en la que se mapea la entidad
            ToTable("BannerSources");

            // Clave primaria de la entidad, se encuentra en la columna BannerSourceId, generada por la DB
            this.HasKey(pBannerSource => pBannerSource.Id)
                .Property(pBannerSource => pBannerSource.Id)
                .HasColumnName("BannerSourceId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}
