using System;
using System.Collections.Generic;
using EFAssignment1.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFAssignment1.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=database.db",
                    b => b.MigrationsAssembly("EFAssignment1.Infrastructure"));
            }
        }
        public virtual DbSet<BlogType> BlogTypes { get; set; }

        public virtual DbSet<PostType> PostTypes { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).UpdatedDateTime = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).CreatedDateTime = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).UpdatedDateTime = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).CreatedDateTime = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

