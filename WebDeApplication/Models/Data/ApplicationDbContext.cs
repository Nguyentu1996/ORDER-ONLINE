using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DataDauVao> DataDauVao { get; set; }
        public DbSet<DataProfitOrder> DataProfitOrder { get; set; }
        public DbSet<EmailReader> EmailReader { get; set; }
        public DbSet<EmailGroup> EmailGroup { get; set; }        
        public DbSet<EmailCancel> EmailCancel { get; set; }
        public DbSet<EmailDelay> EmailDelay { get; set; }
        public DbSet<DashboardData> DashboardData { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<SubProfitOrder> SubProfitOrder { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailReader>()              
                .HasIndex(p => new { p.ODNumber, p.status2 })
                .HasFilter(null);
            modelBuilder.Entity<EmailReader>()
                .HasIndex(p => p.messageId)
                .HasFilter(null);
            modelBuilder.Entity<EmailReader>()
                    .Property(b => b.odParrent)
                    .HasDefaultValue(0);
            modelBuilder.Entity<DataDauVao>()
               .HasIndex(p => p.Name)
               .HasFilter(null);
            base.OnModelCreating(modelBuilder);

        }
    }
}
