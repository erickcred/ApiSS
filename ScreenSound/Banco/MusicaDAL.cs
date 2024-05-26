﻿using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
  internal class MusicaDAL : DAL<Musica>
  {
    public MusicaDAL(ScreenSoundContext contexto) : base(contexto)
    { }

    public override void Adicionar(Musica model)
    {
      if (model is null) 
        throw new ArgumentNullException("Todos os dados da musica devem ser preenchidos");

      base.Adicionar(model);
    }

    public override void Atualizar(Musica model, int id)
    {
      var musica = _contexto.Musicas
        .AsNoTracking()
        .FirstOrDefault(x => x.Id == id);
      if (musica is null)
        throw new Exception("Musica não encontrada!");
      
      base.Atualizar(model, id);
    }

    public override void Deletar(Musica model)
    {
      var musica = _contexto.Musicas.Any(x => x.Id == model.Id);
      base.Deletar(model);
    }
  }
}
