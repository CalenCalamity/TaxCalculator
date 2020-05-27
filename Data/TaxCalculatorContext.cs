using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models;

namespace TaxCalculator.Data
{
    public class TaxCalculatorContext : DbContext
    {
        public TaxCalculatorContext(DbContextOptions<TaxCalculatorContext> options) : base(options)
        {
        }

        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<ProgressiveTax> ProgressiveTaxes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCode>().ToTable("PostalCode");
            modelBuilder.Entity<TaxType>().ToTable("TaxType");
            modelBuilder.Entity<ProgressiveTax>().ToTable("ProgressiveTax");
        }
    }
}
