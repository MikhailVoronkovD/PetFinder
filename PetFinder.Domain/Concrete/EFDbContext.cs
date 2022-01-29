using PetFinder.Domain.Entities;
using System.Data.Entity;

namespace PetFinder.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
    }
}
