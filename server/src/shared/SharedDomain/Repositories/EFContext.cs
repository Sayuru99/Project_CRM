using Microsoft.EntityFrameworkCore;

namespace MyApp.SharedDomain.Repositories
{
    public abstract class EFContext : DbContext
    {
        public EFContext() : base() { } 
        public EFContext(DbContextOptions options) : base(options) { }
    }
}
