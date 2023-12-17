using DataModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
            : base(options)
        {
        }
        public DbSet<GoodEnitty> Goods { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
