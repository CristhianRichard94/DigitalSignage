using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class RSSItemMap : EntityTypeConfiguration<RSSItem>
    {
        public RSSItemMap()
        {
            ToTable("RSSItems");

            this.HasKey(pRSSItem => pRSSItem.Id)
                .Property(pRSSItem => pRSSItem.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.Property(pRSSItem => pRSSItem.Description)
                .IsRequired();

            this.Property(pRSSItem => pRSSItem.Url)
                .IsRequired();

            this.Property(pRSSItem => pRSSItem.Title)
                .IsRequired();

            this.Property(pRSSItem => pRSSItem.Date)
                .IsRequired();

            this.Property(pRSSItem => pRSSItem.RSSSourceId)
                .IsRequired();
        }
    }
}
