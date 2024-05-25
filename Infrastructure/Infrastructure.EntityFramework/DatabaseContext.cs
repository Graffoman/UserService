using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.EntityFramework
{
    /// <summary>
    /// Контекст.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Группы пользователей.
        /// </summary>
        public DbSet<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// Группы.
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Роли пользователей.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Права доступа.
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<UserGroup>().ToTable("usergroup");
            modelBuilder.Entity<Group>().ToTable("group");
            modelBuilder.Entity<Role>().ToTable("role");
            modelBuilder.Entity<UserRole>().ToTable("userrole");
            modelBuilder.Entity<Permission>().ToTable("permission");

            modelBuilder.Entity<UserGroup>()
                .HasIndex(cs => new { cs.UserId, cs.GroupId });

            modelBuilder.Entity<UserRole>()
                .HasIndex(cs => new { cs.UserId, cs.RoleId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(u => u.User)
                .WithMany(c => c.UserGroups)
                .HasForeignKey(cs => new { cs.UserId })
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<UserGroup>()
                .HasOne(u => u.Group)
                .WithMany(c => c.UserGroups)
                .HasForeignKey(cs => new { cs.GroupId })
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(cs => new { cs.UserId })
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(cs => new { cs.RoleId })
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasMany(u => u.Permissions)
                .WithOne(c => c.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            modelBuilder.Entity<User>().HasIndex(u => new { u.Email, u.PasswordHash });
            modelBuilder.Entity<User>().Property(c => c.Name).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(c => c.LastName).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(c => c.MiddleName).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(c => c.Department).HasMaxLength(512);
            modelBuilder.Entity<User>().Property(c => c.Email).HasMaxLength(256);
            modelBuilder.Entity<Group>().Property(c => c.Name).HasMaxLength(512);
            modelBuilder.Entity<Role>().Property(c => c.Name).HasMaxLength(256);

            modelBuilder.Entity<Permission>().Property(c => c.Name).HasMaxLength(256);
            modelBuilder.Entity<Permission>().HasIndex(c => c.RoleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=userservice;UserId=postgres;Password=admin");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }


    }
}