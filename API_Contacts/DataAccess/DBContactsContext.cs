using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_Contacts
{
    public partial class DBContactsContext : DbContext
    {
        public DBContactsContext()
        {
        }

        public DBContactsContext(DbContextOptions<DBContactsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactSkill> ContactSkill { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder
                    .UseSqlServer("Data Source=DESKTOP-RMPC51H\\SQLEXPRESS;Initial Catalog=DBContacts;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<ContactSkill>(entity =>
            {
                entity.HasKey(e => new { e.IdContact, e.IdSkill });

                entity.Property(e => e.IdContact).HasColumnName("Id_Contact");

                entity.Property(e => e.IdSkill).HasColumnName("Id_Skill");

                entity.HasOne(d => d.IdContactNavigation)
                    .WithMany(p => p.ContactSkill)
                    .HasForeignKey(d => d.IdContact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactSkill_Contact");

                entity.HasOne(d => d.IdSkillNavigation)
                    .WithMany(p => p.ContactSkill)
                    .HasForeignKey(d => d.IdSkill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactSkill_Skill");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.SkillLevel).HasMaxLength(50);

                entity.Property(e => e.SkillName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
