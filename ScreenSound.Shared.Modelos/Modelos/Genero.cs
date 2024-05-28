using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenSound.Shared.Modelos.Modelos;

[Table("Genero")]
public class Genero
{
  public int Id { get; set; }
  public string? Nome { get; set; } = string.Empty;
  public string? Descricao { get; set; } = string.Empty;

  public override string ToString()
  {
    return $"Nome: {Nome} - Descrição: {Descricao}";
  }
}
