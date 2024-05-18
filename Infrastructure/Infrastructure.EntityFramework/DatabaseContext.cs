using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        /// Роли пользователей.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Права доступа.
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);   

            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<UserGroup>().ToTable("usergroup");
            modelBuilder.Entity<Role>().ToTable("role");
            modelBuilder.Entity<Permission>().ToTable("permission");

            modelBuilder.Entity<UserGroup>()
                .HasMany(c => c.Users)
                .WithMany(s => s.UserGroups)
                .UsingEntity(j => j.ToTable("usertousergroup"));

            modelBuilder.Entity<Role>()
                .HasMany(c => c.Users)
                .WithMany(s => s.Roles)
                .UsingEntity(j => j.ToTable("usertoroles"));

            modelBuilder.Entity<Role>()
                .HasMany(u => u.Permissions)
                .WithOne(c => c.Role )
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            modelBuilder.Entity<User>().HasIndex(u => new { u.Email, u.PasswordHash });
            modelBuilder.Entity<User>().Property(c => c.Name).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(c => c.LastName).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(c => c.MiddleName).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(c => c.Department).HasMaxLength(512);
            modelBuilder.Entity<User>().Property(c => c.Email).HasMaxLength(256);

            modelBuilder.Entity<UserGroup>().Property(c => c.Name).HasMaxLength(256);

            modelBuilder.Entity<Role>().Property(c => c.Name).HasMaxLength(256);

            modelBuilder.Entity<Permission>().Property(c => c.Name).HasMaxLength(256);
            modelBuilder.Entity<Permission>().HasIndex(c => c.RoleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);   
        }
    }
}