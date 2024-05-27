using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
  public class ArtistaDAL : DAL<Artista>
  {
    public ArtistaDAL(ScreenSoundContext contexto) : base(contexto)
    { }

    public override void Adicionar(Artista model)
    {
      ValidaStringLength("Nome", model.Nome);
      ValidaStringLength("Biografia", model.Bio);
      ValidaStringLength("Foto Perfil", model.FotoPerfil);
      base.Adicionar(model);
    }

    public override void Atualizar(Artista model, int id)
    {
      ValidaStringLength("Nome", model.Nome);
      ValidaStringLength("Biografia", model.Bio);
      ValidaStringLength("Foto Perfil", model.FotoPerfil);
      base.Atualizar(model, id);
    }

    public override void Deletar(Artista model)
    {
      var artista = _contexto.Artistas.Any(x => x.Id == model.Id);
      if (artista == false)
        throw new Exception("Artista não encontrado!");

      base.Deletar(model);
    }

    #region Validações
    private void ValidaStringLength(string nomeItem, string valor)
    {
      if (string.IsNullOrWhiteSpace(valor))
        throw new Exception($"O campo ({nomeItem}) deve ser preenchido!");

      if (valor.Length > 255)
        throw new Exception($"{nomeItem} ultrapassa 255 caracteres, tendo no total {valor.Length}!");
    }
    #endregion
  }
}
