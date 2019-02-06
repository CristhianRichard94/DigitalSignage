using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DigitalSignage.Domain;

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
            //Database.SetInitializer<DigitalSignageDbContext>(new CreateDatabaseIfNotExists<DigitalSignageDbContext>());
            //Database.SetInitializer<DigitalSignageDbContext>(new DropCreateDatabaseIfModelChanges<DigitalSignageDbContext>());
            //Database.SetInitializer<DigitalSignageDbContext>(new DropCreateDatabaseAlways<DigitalSignageDbContext>());

            // Se establece la estrategia personalizada de inicialización de la BBDD.
            this.Configuration.LazyLoadingEnabled = true;
           // Database.SetInitializer<DigitalSignageDbContext>(new DatabaseInitialization());

        }
    }
}
