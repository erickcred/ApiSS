using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
  internal class ScreenSoundContext : DbContext
  {
    private string connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ScreenSound;User Id=sa;Password=123;TrustServerCertificate=True";

    public DbSet<Artista> Artistas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder.UseSqlServer(connectionString);
    }

  }
}
