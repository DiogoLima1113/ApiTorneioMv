using System.Collections.Generic;

namespace api_torneio_mv.Dominio.Repositorio.Interface
{
    public interface IDbRepositorio<T> where T : new()
    {
         void Inserir(T obj);
         void Atualizar(T obj);
         void Deletar(int id);
         T Obter(int id);
         IEnumerable<T> ObterTodos();
    }
}