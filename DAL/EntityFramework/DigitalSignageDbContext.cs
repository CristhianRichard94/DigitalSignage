using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DigitalSignage.Domain;
using DigitalSignage.DAL.EntityFramework.Mappings;

namespace DigitalSignage.DAL.EntityFramework
{
    /// <summary>
    /// Clase que representa un contexto de acceso a la base de datos
    /// </summary>
    public class DigitalSignageDbContext : DbContext
    {
        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<RSSSource> RSSSources { get; set; }

        public DbSet<TextSource> Texts { get; set; }

        public DbSet<RSSItem> RSSItems { get; set; }

        public DbSet<Image> Images { get; set; }


        /// <summary>
        /// Constructor, contiene estrategias de inicialización de la base de datos
        /// </summary>
        public DigitalSignageDbContext() : base("DigitalSignage")
        {
            // Se establece la estrategia personalizada de inicialización de la BBDD.
            this.Configuration.LazyLoadingEnabled = false;
           Database.SetInitializer<DigitalSignageDbContext>(new DatabaseInitialization());
           //Database.SetInitializer<DigitalSignageDbContext>(new DropCreateDatabaseAlways<DigitalSignageDbContext>());


        }

        /// <summary>
        /// Realiza los mapeos cuando se genera el modelo
        /// </summary>
        /// <param name="pModelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder pModelBuilder)
        {
            pModelBuilder.Configurations.Add(new CampaignMap());
            pModelBuilder.Configurations.Add(new BannerMap());
            pModelBuilder.Configurations.Add(new BannerSourceMap());
            pModelBuilder.Configurations.Add(new RSSSourceMap());
            pModelBuilder.Configurations.Add(new TextSourceMap());
            pModelBuilder.Configurations.Add(new RSSItemMap());
            pModelBuilder.Configurations.Add(new ImageMap());

            base.OnModelCreating(pModelBuilder);
        }
    }
}
