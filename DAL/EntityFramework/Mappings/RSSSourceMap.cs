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
    /// Clase de mapeo de fuentes RSS
    /// </summary>
    class RSSSourceMap : EntityTypeConfiguration<RSSSource>
    {
        public RSSSourceMap()
        {
            // Tabla en la que se mapea la entidad
            this.ToTable("RSSSources");

            // Propiedades Requeridas (No nulas)
            this.Property(pBannerSource => pBannerSource.Description)
                .IsRequired();

            this.Property(pRssSource => pRssSource.Url)
                .IsRequired();

            // Posee muchos Items RSS con clave Foranea RSSSourceId que se eliminan junto con la fuente
            this.HasMany<RSSItem>(pRssSource => pRssSource.RSSItems)
                .WithRequired()
                .HasForeignKey<int>(i => i.RSSSourceId)
                .WillCascadeOnDelete();

        }
    }
}
