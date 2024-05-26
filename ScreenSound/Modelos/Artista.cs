﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenSound.Modelos;

[Table("Artista")]
public class Artista
{
  public string Nome { get; set; }
  public string FotoPerfil { get; set; }
  public string Bio { get; set; }
  public int Id { get; set; }

  public virtual ICollection<Musica> Musidcas { get; set; } = new List<Musica>();

  public Artista(string nome, string bio)
  {
    Nome = nome;
    Bio = bio;
    FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
  }

  public void AdicionarMusica(Musica musica)
  {
    Musidcas.Add(musica);
  }

  public void ExibirDiscografia()
  {
    Console.WriteLine($"Discografia do artista {Nome}");
    foreach (var musica in Musidcas)
    {
      Console.WriteLine($"Música: {musica.Nome} - Ano de Lançamento: {musica.AnoLancamento}");
    }
  }

  public override string ToString()
  {
    return $@"Id: {Id}
          Nome: {Nome}
          Foto de Perfil: {FotoPerfil}
          Bio: {Bio}";
  }
}