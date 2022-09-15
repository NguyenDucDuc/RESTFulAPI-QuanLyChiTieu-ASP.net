using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace QLCT.DAL.Models
{
    public partial class QuanLyChiTieuContext : DbContext
    {
        public QuanLyChiTieuContext()
        {
        }

        public QuanLyChiTieuContext(DbContextOptions<QuanLyChiTieuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BelongTo> BelongTos { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<IncomeOrSpending> IncomeOrSpendings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToGroup> UserToGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QuanLyChiTieu;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BelongTo>(entity =>
            {
                entity.ToTable("BelongTo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.GroupId).HasColumnName("Group_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.BelongTos)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_BelongTo_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BelongTos)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_BelongTo_User");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Groupname).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Group_User");
            });

            modelBuilder.Entity<IncomeOrSpending>(entity =>
            {
                entity.ToTable("IncomeOrSpending");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Money).HasColumnType("money");

                entity.Property(e => e.Purpose).HasMaxLength(100);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(10);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IncomeOrSpendings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_IncomeOrSpending_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<UserToGroup>(entity =>
            {
                entity.ToTable("UserToGroup");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.GroupId).HasColumnName("Group_Id");

                entity.Property(e => e.Money).HasColumnType("money");

                entity.Property(e => e.Purpose).HasMaxLength(100);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserToGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_UserToGroup_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserToGroups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserToGroup_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
