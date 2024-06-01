namespace ScreenSound.Web.Response.Musica.Generos;

public record GeneroResponse(
  int Id,
  string Nome,
  string Descricao)
{
  public override string ToString()
  {
    return $"{Nome}";
  }
}