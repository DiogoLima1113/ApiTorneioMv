using System.Collections.Generic;
using ApiTorneioMv.Dominio.Entidade;

namespace ApiTorneioMv.Servico
{
    public interface IServicosTorneio
    {
        bool TimeValido(Time time);
        IEnumerable<Jogador> ObterJogadoresDoTime(Time time);
        Time ObterTimeDoJogador(Jogador jogador);
        Time AtualizaJogadoresTime(Time time);
        Time GerarTimeComJogadores(Time time);
        Jogo GerarJogoComTimes(Jogo jogo);
        bool JogoValido(Jogo jogo);
    }
}