using Microsoft.EntityFrameworkCore;

namespace FitnessApp;

public class FitnessContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Укажите свои параметры подключения к PostgreSQL (Username и Password)
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Fitnes;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients");
            entity.HasKey(c => c.ClientId);
            entity.Property(c => c.ClientId).HasColumnName("client_id");
            entity.Property(c => c.FirstName).HasColumnName("first_name");
            entity.Property(c => c.LastName).HasColumnName("last_name");
            entity.Property(c => c.Status).HasColumnName("status");
        });
    }
}