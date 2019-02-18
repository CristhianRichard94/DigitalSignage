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
    public class DigitalSignageDbContext : DbContext
    {
        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<RSSSource> RSSSources { get; set; }

        public DbSet<Text> Texts { get; set; }

        public DbSet<RSSItem> RSSItems { get; set; }

        public DbSet<Image> Images { get; set; }


        public DigitalSignageDbContext() : base("DigitalSignage")
        {
            Database.SetInitializer<DigitalSignageDbContext>(new CreateDatabaseIfNotExists<DigitalSignageDbContext>());
            ///Database.SetInitializer<DigitalSignageDbContext>(new DropCreateDatabaseIfModelChanges<DigitalSignageDbContext>());
            //Database.SetInitializer<DigitalSignageDbContext>(new DropCreateDatabaseAlways<DigitalSignageDbContext>());

            // Se establece la estrategia personalizada de inicialización de la BBDD.
            this.Configuration.LazyLoadingEnabled = true;
           //Database.SetInitializer<DigitalSignageDbContext>(new DatabaseInitialization());

        }


        public DigitalSignageDbContext(String name) : base(name)
        {
            this.Configuration.LazyLoadingEnabled = true;
            // Se establece la estrategia personalizada de inicialización de la BBDD.
            Database.SetInitializer<DigitalSignageDbContext>(new DropCreateDatabaseIfModelChanges<DigitalSignageDbContext>());
        }


        protected override void OnModelCreating(DbModelBuilder pModelBuilder)
        {
            pModelBuilder.Configurations.Add(new CampaignMap());
            pModelBuilder.Configurations.Add(new BannerMap());
            pModelBuilder.Configurations.Add(new BannerSourceMap());
            pModelBuilder.Configurations.Add(new RSSSourceMap());
            pModelBuilder.Configurations.Add(new TextMap());
            pModelBuilder.Configurations.Add(new RSSItemMap());
            pModelBuilder.Configurations.Add(new ImageMap());

            base.OnModelCreating(pModelBuilder);
        }
    }
}
