﻿using ScreenSound.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenSound.Shared.Modelos.Modelos;

[Table("Discografia")]
public class Discografia
{
  public int Id { get; set; }
  public string? Nome { get; set; } = string.Empty;
  public string? Descricao { get; set; } = string.Empty;
  public virtual ICollection<Musica> Musicas { get; set; }

  public override string ToString()
  {
    return $"Nome: {Nome} - Descrição: {Descricao}";
  }
}
