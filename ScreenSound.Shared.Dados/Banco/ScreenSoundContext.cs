using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.Banco
{
  public class ScreenSoundContext : DbContext
  {
    private string connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ScreenSoundV0;User Id=sa;Password=123;TrustServerCertificate=True";

    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Discografia> Discografias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Musica>()
        .HasMany(g => g.Generos)
        .WithMany(m => m.Musicas);

      modelBuilder.Entity<Musica>()
        .HasMany(d => d.Discografias)
        .WithMany(m => m.Musicas);
    }

  }
}
