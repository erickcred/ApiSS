using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System.Reflection;

namespace ScreenSound.Banco
{
  internal class ArtistaDAL
  {
    private readonly ScreenSoundContext _contexto;

    public ArtistaDAL(ScreenSoundContext contexto)
    {
      _contexto = contexto;
    }

    public IEnumerable<Artista> ListarArtistas()
    {
      var lista = _contexto.Artistas.AsNoTracking().ToList();
      return lista;
    }

    public IEnumerable<Artista> ListarArtista(string nomeArtista)
    {
      var artista = _contexto.Artistas.AsNoTracking().Where(x => x.Nome.Contains(nomeArtista)).ToList();
      return artista;
    }

    public Artista ListarArtista(string nomeArtista, int resultado)
    {
      var artista = _contexto.Artistas.AsNoTracking().FirstOrDefault(x => x.Nome.Contains(nomeArtista));
      return artista;
    }

    public void AdicionarArtista(Artista model)
    {
      using var transaction = _contexto.Database.BeginTransaction();
      try
      {
        ValidaStringLength("Nome", model.Nome);
        ValidaStringLength("Biografia", model.Bio);
        ValidaStringLength("Foto Perfil", model.FotoPerfil);
        _contexto.Artistas.Add(model);
        var retorno = _contexto.SaveChanges();

        if (retorno > 0)
          Console.WriteLine("Artista salvo com sucesso!");
        else
          Console.WriteLine("Não foi possivel salvar o artista!");

        transaction.Commit();
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine(ex.Message);
      }
    }

    public void AtualizarArtista(Artista model, int id)
    {
      using var transaction = _contexto.Database.BeginTransaction();
      try
      {
        var artista = _contexto.Artistas.FirstOrDefault(x => x.Id == id);
        if (artista == null)
          throw new Exception($"Artista {model.Nome} não encontrado! Verifique se o nome está correto!");

        ValidaStringLength("Nome", model.Nome);
        ValidaStringLength("Biografia", model.Bio);
        ValidaStringLength("Foto Perfil", model.FotoPerfil);

        artista.Nome = model.Nome;
        artista.Bio = model.Bio;
        artista.FotoPerfil = model.FotoPerfil;

        _contexto.Artistas.Update(artista);
        var retorno = _contexto.SaveChanges();

        if (retorno > 0)
          Console.WriteLine("Artista atualizado com sucesso!");
        else
          Console.WriteLine("Não foi possivel atualizar o artista!");

        transaction.Commit();
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine(ex.Message);
      }
    }

    public void DeletarArtista(int id)
    {
      var artista = _contexto.Artistas.FirstOrDefault(x => x.Id == id);
      if (artista is null)
        throw new Exception("Artista não encontrado!");


      using var transaction = _contexto.Database.BeginTransaction();
      try
      {
        _contexto.Artistas.Remove(artista);
        var retorno = _contexto.SaveChanges();

        if (retorno > 0)
          Console.WriteLine("Artista deletado com sucesso!");
        else
          Console.WriteLine("Não foi possivel deletar o Artista!");

        transaction.Commit();
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine(ex.Message);
      }
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
