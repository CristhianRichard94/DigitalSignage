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
    /// Clase de mapeo de Campaña en la DB
    /// </summary>
    class CampaignMap : EntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {

            //Tabla en la que se mapea la entidad
            ToTable("Campaigns");

            // Clave primaria de la entidad, se encuentra en la columna BannerSourceId, generada por la DB
            this.HasKey(pCampign => pCampign.Id)
                .Property(pCampign => pCampign.Id)
                .HasColumnName("CampaignId")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            // Columnas requeridas (No nulas)

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

            this.Property(pCampaign => pCampaign.EndTime)
                .IsRequired();

            // Posee muchas imagenes con claves foraneas CampaignId, Se eliminan junto con la campaña
            this.HasMany<Image>(pCampaign => pCampaign.Images)
                .WithRequired()
                .HasForeignKey<int>(i => i.CampaignId)
                .WillCascadeOnDelete();

        }
    }
}
