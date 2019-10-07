using System.Collections.Generic;
using api_torneio_mv.Dominio.Entidade;

namespace api_torneio_mv.Servico
{
    public interface IMeusServicos
    {
        bool TimeValido(Time time);
        IEnumerable<Jogador> ObterJogadoresDoTime(Time time);
        Time ObterTimeDoJogador(Jogador jogador);
    }
}