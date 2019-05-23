using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dvt.Features.Core.Entities
{
    public partial class DvtDatabaseContext : DbContext
    {
        public DvtDatabaseContext()
        {
        }

        public DvtDatabaseContext(DbContextOptions<DvtDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<JobRequest> JobRequest { get; set; }
        public virtual DbSet<SystemFunction> SystemFunction { get; set; }
        public virtual DbSet<SystemFunctionGroup> SystemFunctionGroup { get; set; }
        public virtual DbSet<SystemProfile> SystemProfile { get; set; }
        public virtual DbSet<SystemProfileFunction> SystemProfileFunction { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<TokenType> TokenType { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<Course> Course { get; set; }

        // using the implementation method of existing class known as parent class.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //DbContextOptionsBuilder = Provides a simple API surface for configuring
              if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=.;Database=DvtDatabase;Trusted_Connection=True;");
            }
        }

        //ModelBuilder = use to create, edit, and manage models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // string annotation and object value. a way of attaching inforamtion
            //o the model that can later be read from the model
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.EmailId).ValueGeneratedNever();

                entity.Property(e => e.Body)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Result).IsUnicode(false);

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobRequest>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cron).HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SystemFunction>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_SystemFunction")
                    .IsUnique();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.SystemFunctionGroup)
                    .WithMany(p => p.SystemFunction)
                    .HasForeignKey(d => d.SystemFunctionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemFunction_SystemFunctionGroup");
            });

            modelBuilder.Entity<SystemFunctionGroup>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_SystemFunctionGroup")
                    .IsUnique();

                entity.Property(e => e.SystemFunctionGroupId).ValueGeneratedNever();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SystemProfile>(entity =>
            {
                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SystemProfileFunction>(entity =>
            {
                entity.HasKey(e => new { e.SystemFunctionId, e.SystemProfileId });

                entity.HasOne(d => d.SystemFunction)
                    .WithMany(p => p.SystemProfileFunction)
                    .HasForeignKey(d => d.SystemFunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemProfileFunction_SystemFunction");

                entity.HasOne(d => d.SystemProfile)
                    .WithMany(p => p.SystemProfileFunction)
                    .HasForeignKey(d => d.SystemProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemProfileFunction_SystemProfile");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.HasOne(d => d.TokenType)
                    .WithMany(p => p.Token)
                    .HasForeignKey(d => d.TokenTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Token_TokenType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Token)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Token_User");
            });

            modelBuilder.Entity<TokenType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.Property(e => e.UserAccountId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ContactNumber).HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.KnownAs).HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(64);

                entity.Property(e => e.Salt).HasMaxLength(64);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.SystemProfile)
                    .WithMany(p => p.UserAccount)
                    .HasForeignKey(d => d.SystemProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAccou__Syste__29572725");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.UserAccount)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccount_UserStatus");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });


            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100000);
            });
        }
    }
}
