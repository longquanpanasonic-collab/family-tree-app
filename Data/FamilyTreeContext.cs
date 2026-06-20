using System.Data.Entity;
using FamilyTreeApp.Models;

namespace FamilyTreeApp.Data
{
    public class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext() : base("name=FamilyTreeConnection")
        {
            Database.SetInitializer<FamilyTreeContext>(new DropCreateDatabaseIfModelChanges<FamilyTreeContext>());
        }

        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FamilyMember - Father relationship
            modelBuilder.Entity<FamilyMember>()
                .HasOptional(f => f.Father)
                .WithMany()
                .HasForeignKey(f => f.FatherId)
                .WillCascadeOnDelete(false);

            // FamilyMember - Mother relationship
            modelBuilder.Entity<FamilyMember>()
                .HasOptional(f => f.Mother)
                .WithMany()
                .HasForeignKey(f => f.MotherId)
                .WillCascadeOnDelete(false);

            // FamilyMember - Spouse relationship
            modelBuilder.Entity<FamilyMember>()
                .HasOptional(f => f.Spouse)
                .WithMany()
                .HasForeignKey(f => f.SpouseId)
                .WillCascadeOnDelete(false);

            // GalleryImage - FamilyMember relationship
            modelBuilder.Entity<GalleryImage>()
                .HasOptional(g => g.FamilyMember)
                .WithMany()
                .HasForeignKey(g => g.FamilyMemberId)
                .WillCascadeOnDelete(false);
        }
    }
}
