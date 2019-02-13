using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class RSSSourceMap : EntityTypeConfiguration<RSSSource>
    {
        public RSSSourceMap()
        {
            this.ToTable("RSSSources");
            
            this.Property(pBannerSource => pBannerSource.Description)
                .IsRequired();
            
            this.Property(pRssSource => pRssSource.Url)
                .IsRequired();
            
            this.HasMany<RSSItem>(pRssSource => pRssSource.RSSItems)
                .WithRequired()
                .HasForeignKey<int>(i => i.RSSSourceId)
                .WillCascadeOnDelete();

        }
    }
}
