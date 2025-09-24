using Microsoft.EntityFrameworkCore;
using StoreSample.Domain.Model.Dto;
namespace DbContexto
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        //Entities
        public DbSet<Item> Providers { get; set; }
        public DbSet<Patients> Services { get; set; }
        public DbSet<City> Countries { get; set; }
        public DbSet<ServiceCountry> ServiceCountries { get; set; }
        public DbSet<ProviderCustomField> ProviderCustomFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

    }
}
