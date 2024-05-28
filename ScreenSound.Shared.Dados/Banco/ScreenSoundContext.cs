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
      base.OnConfiguring(optionsBuilder);
      optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer(connectionString);
    }

  }
}
