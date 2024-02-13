using Microsoft.EntityFrameworkCore;
using PhoneBook.DataAccess.Models;

namespace PhoneBook.DataAccess
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(builder =>
            {
                builder.Property(p => p.FirstName).HasMaxLength(100);
                builder.Property(p => p.MiddleName).HasMaxLength(100);
                builder.Property(p => p.LastName).HasMaxLength(100);
            });

            modelBuilder.Entity<PhoneNumber>(builder =>
            {
                builder.Property(p => p.Phone).HasMaxLength(50);

                builder.HasOne(p => p.Contact)
                    .WithMany(c => c.PhoneNumbers)
                    .HasForeignKey(p => p.ContactId);

            });

        }
    }
}