using Microsoft.EntityFrameworkCore;


namespace Ledger.Data
{
    public class LedgerDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Entry> Entries { get; set; }
        
        public LedgerDBContext(DbContextOptions<LedgerDBContext> options) : base(options)
        {

        }

        public LedgerDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-KUC56DD5\\SQLEXPRESS; Database=Ledger;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
