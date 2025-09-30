using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApp.Data
{
    public class FamilyDbContext : DbContext
    {
        public FamilyDbContext(DbContextOptions<FamilyDbContext> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed family tree
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Thomas", ParentId = null },   // Grandfather
                new Person { Id = 2, Name = "Dipuo", ParentId = null },    // Grandmother
                new Person { Id = 3, Name = "Thabo", ParentId = 1 },       // Father
                new Person { Id = 4, Name = "Tseleng", ParentId = 2 },     // Mother
                new Person { Id = 5, Name = "Lethabo", ParentId = 3 },     // First born
                new Person { Id = 6, Name = "Tsepiso", ParentId = 3 }      // Last born
            );
        }
    }
}
