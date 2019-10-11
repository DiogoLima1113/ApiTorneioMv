using System.Collections.Generic;
using System.Linq;
using ApiTorneioMv.Dominio.Entidade;
using ApiTorneioMv.Dominio.Repositorio.Interface;

namespace ApiTorneioMv.Servico
{
    public class ServicosTorneio : IServicosTorneio
    {
        public ServicosTorneio(IDbRepositorioRelacionamentoTimeJogador repositorioRelacionamento,
                            IDbRepositorio<Jogador> repositorioJogador,
                            IDbRepositorio<Time> repositorioTime)
        {
            _repositorioRelacionamento = repositorioRelacionamento;
            _repositorioJogador = repositorioJogador;
            _repositorioTime = repositorioTime;
        }

        private readonly IDbRepositorioRelacionamentoTimeJogador _repositorioRelacionamento;
        private readonly IDbRepositorio<Jogador> _repositorioJogador;
        private readonly IDbRepositorio<Time> _repositorioTime;

        public bool TimeValido(Time time)
        {
            return ObterJogadoresDoTime(time).Count() >= 5;
        }

        public IEnumerable<Jogador> ObterJogadoresDoTime(Time time)
        {
            return _repositorioRelacionamento.ObterJogadoresTime(time);
        }

        public Time ObterTimeDoJogador(Jogador jogador)
        {
            Time time = _repositorioRelacionamento.ObterTimeDoJogador(jogador.Id);

            return time == null ? null : time;
        }

        public Time AtualizaJogadoresTime(Time time)
        {
            if((time.ListaJogadores != _repositorioRelacionamento.ObterJogadoresTime(time)) && (time.ListaJogadores != null) )
            {
                _repositorioRelacionamento.Remover(time);

                foreach (var jogador in time.ListaJogadores)
                {
                    _repositorioRelacionamento.Atualizar(time, jogador);
                }
            }

            return GerarTimeComJogadores(time);

        }

        public Time GerarTimeComJogadores(Time time)
        {
            time.ListaJogadores = ObterJogadoresDoTime(time);
            
            return time;
        }

        public Jogo GerarJogoComTimes(Jogo jogo)
        {
            jogo.TimeCasa = _repositorioTime.Obter(jogo.IdTimeCasa);
            jogo.TimeVisitante = _repositorioTime.Obter(jogo.IdTimeVisitante);

            return jogo;
        }

        public bool JogoValido(Jogo jogo)
        {
            jogo = GerarJogoComTimes(jogo);
            
            return TimeValido(jogo.TimeCasa) && TimeValido(jogo.TimeVisitante);
        }
    }
}