using System.Collections.Generic;
using System.Linq;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Interface;

namespace api_torneio_mv.Servico
{
    public class MeusServicos : IMeusServicos
    {
        public MeusServicos(IDbRepositorio<RelacionamentoTimeJogador> repositorioRelacionamento,
                            IDbRepositorio<Jogador> repositorioJogador,
                            IDbRepositorio<Time> repositorioTime)
        {
            _repositorioRelacionamento = repositorioRelacionamento;
            _repositorioJogador = repositorioJogador;
            _repositorioTime = repositorioTime;
        }

        private readonly IDbRepositorio<RelacionamentoTimeJogador> _repositorioRelacionamento;
        private readonly IDbRepositorio<Jogador> _repositorioJogador;
        private readonly IDbRepositorio<Time> _repositorioTime;

        public bool TimeValido(Time time)
        {
            return ObterJogadoresDoTime(time).Count() >= 5;
        }

        public IEnumerable<Jogador> ObterJogadoresDoTime(Time time)
        {
            var listaRelacionamentos = _repositorioRelacionamento.ObterTodos().Where( e => e.IdTime == time.Id ).ToList();

            foreach (var relacionamento in listaRelacionamentos)
            {
                yield return _repositorioJogador.Obter(relacionamento.IdJogador);
            }
        }

        public Time ObterTimeDoJogador(Jogador jogador)
        {
            var relacionamento = _repositorioRelacionamento.ObterTodos().Where( e => e.IdJogador == jogador.Id ).ToList().FirstOrDefault();

            return _repositorioTime.Obter(relacionamento.IdTime);
        }
    }
}