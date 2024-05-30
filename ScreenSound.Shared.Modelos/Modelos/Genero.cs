using ScreenSound.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenSound.Shared.Modelos.Modelos;

[Table("Genero")]
public class Genero
{
  public int Id { get; set; }
  public string Nome { get; set; } = string.Empty;
  public string? Descricao { get; set; } = string.Empty;
  public virtual ICollection<Musica> Musicas { get; set; }

  public Genero(string nome)
  {
    Nome = nome;
  }

  public override string ToString()
  {
    return $"Nome: {Nome} - Descrição: {Descricao}";
  }
}
