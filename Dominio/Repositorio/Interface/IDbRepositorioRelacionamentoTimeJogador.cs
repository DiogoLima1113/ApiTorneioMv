using System.Collections.Generic;
using api_torneio_mv.Dominio.Entidade;

namespace api_torneio_mv.Dominio.Repositorio.Interface
{
    public interface IDbRepositorioRelacionamentoTimeJogador
    {
        void Atualizar(Time time, Jogador jogador);
        IEnumerable<Jogador> ObterJogadoresTime(Time time);
        void Remover(Time time);
        void Inserir(int idTime, int idJogador);
        Time ObterTimeDoJogador(int idJogador);
    }
}