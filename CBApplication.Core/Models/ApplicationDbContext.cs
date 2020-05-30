using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(configure => configure.HasPrecision(20, 10));
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<GlAccountManagement> GlAccount { get; set; }
        public DbSet<GlCategoryManagement> GlCategory { get; set; }
        public DbSet<MainAccountCategory> MainAccountCategories { get; set; }
        public DbSet<TellerManagement> TellerManagements { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<SavingsConfig> SavingsAccount { get; set; }
        public DbSet<CurrentConfig> CurrentAccount { get; set; }
        public DbSet<LoanConfig> LoanConfig { get; set; }
        public DbSet<GlPosting> GlPosting { get; set; }
        public DbSet<BankConfig> BankConfigs { get; set; }
        public DbSet<TellerPosting> TellerPostings { get; set; }
        public DbSet<COTPost> CotPosts { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<TransactionsLog> Transactions { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
