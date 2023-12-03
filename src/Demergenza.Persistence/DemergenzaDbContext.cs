using Microsoft.EntityFrameworkCore;
using Demergenza.Domain.Entities.Menu;
using Demergenza.Domain.Entities.Admin;
using Demergenza.Application.Helpers.Configuration;
using Demergenza.Domain.Entities.PageContents;

namespace Demergenza.Persistence
{
    public class DemergenzaDbContext : DbContext
    {
        private readonly ConfigurationHelper _configurations;

        public DemergenzaDbContext(DbContextOptions<DemergenzaDbContext> options, ConfigurationHelper configurations) :
            base(options)
        {
            _configurations = configurations;
        }

        public DemergenzaDbContext(ConfigurationHelper configurations)
        {
            _configurations = configurations;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine(_configurations.ConnectionString);
            optionsBuilder.UseNpgsql(_configurations.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("tbl_admin");

                entity.Property(admin => admin.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(admin => admin.Date).HasColumnName("date")
                    .HasColumnType("timestamp with time zone");
                entity.Property(admin => admin.Username).HasColumnName("username").HasColumnType("varchar(24)");
                entity.Property(admin => admin.Password).HasColumnName("password").HasColumnType("varchar(32)");
                entity.Property(admin => admin.FullName).HasColumnName("fullName").HasColumnType("varchar(48)");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("tbl_category");

                entity.Property(category => category.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(category => category.Date).HasColumnName("date")
                    .HasColumnType("timestamp with time zone");
                entity.Property(category => category.Name).HasColumnName("name").HasColumnType("varchar(48)");
                entity.Property(category => category.Image).HasColumnName("image").HasColumnType("varchar(250)");
            });


            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("tbl_menu");

                entity.Property(menu => menu.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(menu => menu.Date).HasColumnName("date")
                    .HasColumnType("timestamp with time zone");
                entity.Property(menu => menu.Name).HasColumnName("name").HasColumnType("varchar(48)");
                entity.Property(menu => menu.Image).HasColumnName("image").HasColumnType("varchar(250)");
                entity.Property(menu => menu.Ingredients).HasColumnName("Ingredients").HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<AboutUs>(entity =>
            {
                entity.ToTable("tbl_about_us");

                entity.Property(content => content.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(content => content.Date).HasColumnName("date")
                    .HasColumnType("timestamp with time zone");
                entity.Property(content => content.TextContent).HasColumnName("text-content")
                    .HasColumnType("varchar(2000)");
                entity.Property(content => content.ImagePath).HasColumnName("image-path").HasColumnType("varchar(150)");
            });

            modelBuilder.Entity<Hero>(entity =>
            {
                entity.ToTable("tbl_hero");

                entity.Property(hero => hero.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(hero => hero.Date).HasColumnName("date")
                    .HasColumnType("timestamp with time zone");
                entity.Property(hero => hero.ImageSrc).HasColumnName("image-src").HasColumnType("varchar(250)");
                entity.Property(hero => hero.ImageWidth).HasColumnName("image-width").HasColumnType("varchar(8)");
                entity.Property(hero => hero.Alt).HasColumnName("alt").HasColumnType("varchar(30)");
                entity.Property(hero => hero.Title).HasColumnName("title").HasColumnType("varchar(40)");
                entity.Property(hero => hero.TitleSrc).HasColumnName("title-src").HasColumnType("varchar(250)");
                entity.Property(hero => hero.Subtitle).HasColumnName("subtitle").HasColumnType("varchar(600)");
                entity.Property(hero => hero.Top).HasColumnName("top").HasColumnType("varchar(8)");
            });
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("tbl_contact");

                entity.Property(contact => contact.Id).HasColumnName("id").HasColumnType("uuid");
                entity.Property(contact => contact.Date).HasColumnName("date")
                    .HasColumnType("timestamp with time zone");
                entity.Property(contact => contact.Address).HasColumnName("address").HasColumnType("varchar(150)");
                entity.Property(contact => contact.Email).HasColumnName("email").HasColumnType("varchar(60)");
                entity.Property(contact => contact.Phone).HasColumnName("phone").HasColumnType("varchar(24)");
                entity.Property(contact => contact.SecondPhone).HasColumnName("second-phone")
                    .HasColumnType("varchar(24)");
            });
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Admin>? Admins;
        public DbSet<Category>? Categories;
        public DbSet<Menu>? Menus;
        public DbSet<AboutUs>? AboutUs;
    }
}