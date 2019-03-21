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
    class RSSItemMap : EntityTypeConfiguration<RSSItem>
    {
        public RSSItemMap()
        {
            // Tabla en la que se mapea la entidad
            ToTable("RSSItems");

            // Clave primaria de la entidad, se encuentra en la columna RSSItemId, generada por la DB
            this.HasKey(pRSSItem => pRSSItem.Id)
                .Property(pRSSItem => pRSSItem.Id)
                .HasColumnName("RSSItemId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            // Propiedades Requeridas (No nulas)
            this.Property(pRSSItem => pRSSItem.Description)
                .IsRequired();

            this.Property(pRSSItem => pRSSItem.Url)
                .IsRequired();

            this.Property(pRSSItem => pRSSItem.Title)
                .IsRequired();

            this.Property(pRSSItem => pRSSItem.Date)
                .IsRequired();
        }
    }
}
