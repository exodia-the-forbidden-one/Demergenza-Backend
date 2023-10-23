using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Demergenza.Domain.Entities.Menu;
using Demergenza.Domain.Entities.Admin;
using System.Data.Common;

namespace Demergenza.Persistence
{
    public class DemergenzaDbContext : DbContext
    {
        public DemergenzaDbContext(DbContextOptions<DemergenzaDbContext> options) : base(options)
        {

        }
        public DemergenzaDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine(Configurations.ConnectionString);
            optionsBuilder.UseNpgsql(Configurations.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("tbl_admin");

                entity.Property(admin => admin.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(admin => admin.Username).HasColumnName("username").HasColumnType("varchar").HasMaxLength(24);
                entity.Property(admin => admin.Password).HasColumnName("password").HasColumnType("varchar").HasMaxLength(32);
                entity.Property(admin => admin.FullName).HasColumnName("fullName").HasColumnType("varchar").HasMaxLength(48);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("tbl_category");

                entity.Property(category => category.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(category => category.Name).HasColumnName("name").HasColumnType("varchar").HasMaxLength(48);
                entity.Property(category => category.Image).HasColumnName("image").HasColumnType("varchar").HasMaxLength(250);
            });


            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("tbl_menu");

                entity.Property(menu => menu.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(menu => menu.Name).HasColumnName("name").HasColumnType("varchar").HasMaxLength(48);
                entity.Property(menu => menu.Image).HasColumnName("image").HasColumnType("varchar").HasMaxLength(250);
                entity.Property(menu => menu.Ingredients).HasColumnName("Ingredients").HasColumnType("varchar").HasMaxLength(250);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Admin> Admins;
        public DbSet<Category> Categories;
        public DbSet<Menu> Menus;
    }
}