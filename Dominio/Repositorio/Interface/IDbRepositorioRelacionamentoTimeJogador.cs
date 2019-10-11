using System.Collections.Generic;
using ApiTorneioMv.Dominio.Entidade;

namespace ApiTorneioMv.Dominio.Repositorio.Interface
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