﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.Banco
{
  public class ScreenSoundContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
  {
    private string connectionString = "Data Source=localhost\\SQLEXPRESS;Database=ScreenSoundV0;User Id=sa;Password=123;TrustServerCertificate=True";

    public ScreenSoundContext() { }
    public ScreenSoundContext(DbContextOptions options) : base(options) { }

    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Discografia> Discografias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (optionsBuilder.IsConfigured)
        return;
      optionsBuilder
        .UseSqlServer(connectionString)
        .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Musica>()
        .HasMany(g => g.Generos)
        .WithMany(m => m.Musicas);

      modelBuilder.Entity<Musica>()
        .HasMany(d => d.Discografias)
        .WithMany(m => m.Musicas);
    }

  }
}
