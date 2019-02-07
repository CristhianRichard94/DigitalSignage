using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.DAL.EntityFramework.Mappings
{
    class CampaignMap : EntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {
            ToTable("Campaigns");

            this.HasKey(pCampign => pCampign.Id)
                .Property(pCampign => pCampign.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.Property(pCampaign => pCampaign.Name)
                .IsRequired();

            this.Property(pCampaign => pCampaign.Description)
                .IsRequired();

            this.Property(pCampaign => pCampaign.InitialDate)
                .IsRequired();

            this.Property(pCampaign => pCampaign.EndDate)
                .IsRequired();

            this.Property(pCampaign => pCampaign.InitialTime)
                .IsRequired();

            this.Property(pCampaign => pCampaign.EndDate)
                .IsRequired();

            this.HasMany<Image>(pCampaign => pCampaign.Images)
                 .WithRequired()
                 .HasForeignKey<int>(i => i.CampaignId)
                 .WillCascadeOnDelete();

        }
    }
}
