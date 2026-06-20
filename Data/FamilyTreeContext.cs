using System.Data.Entity;
using FamilyTreeApp.Models;

namespace FamilyTreeApp.Data
{
    public class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext() : base("name=FamilyTreeConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình bảng Users
            modelBuilder.Entity<User>()
                .HasMany(u => u.FamilyMembers)
                .WithRequired(fm => fm.User)
                .HasForeignKey(fm => fm.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.News)
                .WithRequired(n => n.User)
                .HasForeignKey(n => n.UserId);

            // Cấu hình quan hệ gia đình
            modelBuilder.Entity<FamilyMember>()
                .HasOptional(f => f.Father)
                .WithMany()
                .HasForeignKey(f => f.FatherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FamilyMember>()
                .HasOptional(f => f.Mother)
                .WithMany()
                .HasForeignKey(f => f.MotherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FamilyMember>()
                .HasOptional(f => f.Spouse)
                .WithMany()
                .HasForeignKey(f => f.SpouseId)
                .WillCascadeOnDelete(false);

            // Cấu hình Images
            modelBuilder.Entity<Image>()
                .HasOptional(i => i.FamilyMember)
                .WithMany(fm => fm.Images)
                .HasForeignKey(i => i.FamilyMemberId)
                .WillCascadeOnDelete(true);
        }
    }
}