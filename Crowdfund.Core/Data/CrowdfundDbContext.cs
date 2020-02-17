using Microsoft.EntityFrameworkCore;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Data {
    public class CrowdfundDbContext : DbContext {
        /// <summary>
        /// The string to connect with the 
        /// database
        /// </summary>
        private readonly string connectionString_;

        /// <summary>
        /// Give the connectionString the right value
        /// </summary>
        public CrowdfundDbContext() : base()
        {
            connectionString_ = @"Server=localhost;Database=crowdfund;User Id=sa; Password=QWE123!@#";
        }

        /// <summary>
        /// The conctructor to initialize the connectionstring
        /// </summary>
        /// <param name="connString"></param>
        public CrowdfundDbContext(string connString)
        {
            connectionString_ = connString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
               .Entity<Owner>()
               .ToTable("Owner");

            modelBuilder
               .Entity<Owner>()
               .HasIndex(e => e.Email)
               .IsUnique();

            modelBuilder
              .Entity<Owner>()
              .Property(a => a.Age)
              .IsRequired();

            modelBuilder
               .Entity<Buyer>()
               .ToTable("Buyer");


            modelBuilder
               .Entity<Buyer>()
               .HasIndex(e => e.Email)
               .IsUnique();

            modelBuilder
              .Entity<Buyer>()
              .Property(a => a.Age)
              .IsRequired();

            modelBuilder
               .Entity<Reward>()
               .ToTable("Reward");

            modelBuilder
               .Entity<Project>()
               .ToTable("Project");

            modelBuilder
               .Entity<ProjectBuyer>()
               .ToTable("ProjectBuyer");

            modelBuilder
              .Entity<ProjectBuyer>()
              .HasKey(op => new { op.BuyerId, op.ProjectId });

            modelBuilder
               .Entity<BuyerReward>()
               .ToTable("BuyerReward");

            modelBuilder
              .Entity<BuyerReward>()
              .HasKey(op => new { op.BuyerId, op.RewardId });



            
        }

        /// <summary>
        /// Create the connection
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
