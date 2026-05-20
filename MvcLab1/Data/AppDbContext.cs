using Microsoft.EntityFrameworkCore;
using MvcLab1.Models;

namespace MvcLab1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet для каждой модели
        public DbSet<Product> Products { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка для Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.HasIndex(p => p.Category);
            });

            // Настройка для Workout (если нужно)
            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(100);
                entity.Property(w => w.Type).IsRequired().HasMaxLength(50);
                entity.Property(w => w.Difficulty).HasMaxLength(20);
            });
        }
    }
}