using Microsoft.EntityFrameworkCore;
using StoreSample.Domain.Model.Dto;
namespace DbContexto
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        //Entities
        public DbSet<Patients> Patients { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

    }
}
