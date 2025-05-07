using Microsoft.EntityFrameworkCore;
using bookSansar.Entities;

namespace bookSansar
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique index for reviews (one review per purchase)
            modelBuilder.Entity<Review>()
                .HasIndex(r => r.PurchaseId)
                .IsUnique();

            // Configure relationship between Review and Purchase
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Purchase)
                .WithOne()
                .HasForeignKey<Review>(r => r.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique index for purchases (one purchase per user per book)
            modelBuilder.Entity<Purchase>()
                .HasIndex(p => new { p.BookId, p.UserId })
                .IsUnique();
        }
    }
}
