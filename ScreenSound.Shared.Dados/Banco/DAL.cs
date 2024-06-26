﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScreenSound.Modelos;
using System.Data;

namespace ScreenSound.Banco
{
  public class DAL<T> where T : class
  {
    protected readonly ScreenSoundContext _contexto;

    public DAL(ScreenSoundContext contexto)
    {
      _contexto = contexto;
    }

    public virtual IEnumerable<T> Listar()
    {
      return _contexto.Set<T>().ToList();
    }

    public virtual void Adicionar(T model)
    {
      using var transaction = _contexto.Database.BeginTransaction();
      try
      {
        _contexto.Set<T>().Add(model);
        var retorno = _contexto.SaveChanges();

        if (retorno > 0)
          Console.WriteLine("Salvo com sucesso!");
        else
          Console.WriteLine("Não foi possivel salvar!");

        transaction.Commit();
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine(ex.Message);
      }
    }

    public virtual void Atualizar(T model, int id)
    {
      using var transaction = _contexto.Database.BeginTransaction();
      try
      {
        _contexto.Set<T>().Update(model);
        var retorno = _contexto.SaveChanges();

        if (retorno > 0)
          Console.WriteLine("Dasos atualizado com sucesso!");
        else
          Console.WriteLine("Não foi possivel atualizar!");

        transaction.Commit();
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine(ex.Message);
      }
    }

    public virtual void Deletar(T model)
    {
      using var transaction = _contexto.Database.BeginTransaction();
      try
      {
        _contexto.Set<T>().Remove(model);
        var retorno = _contexto.SaveChanges();

        if (retorno > 0)
          Console.WriteLine("Deletado com sucesso!");
        else
        {
          Console.WriteLine("Dadodo para deleção não foi encontrado!");
          throw new Exception("Dadodo para deleção não foi encontrado!");
        }

        transaction.Commit();
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine(ex.Message);
        throw new Exception("Dadodo para deleção não foi encontrado!");
      }
    }

    public virtual T? RecuperarPor(Func<T, bool> condicao)
    {
      return _contexto.Set<T>().FirstOrDefault(condicao);
    }

    public virtual IEnumerable<T> ListarPor(Func<T, bool> condicao)
    {
      return _contexto.Set<T>().Where(condicao).ToList();
    }
  }
}
