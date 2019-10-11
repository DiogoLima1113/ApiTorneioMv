using System.Collections.Generic;
using System.Linq;
using ApiTorneioMv.Dominio.Entidade;
using ApiTorneioMv.Dominio.Repositorio.Interface;
using Dapper;

namespace ApiTorneioMv.Dominio.Repositorio.Classe
{
    public class DbRepositorioRelacionamentoTimeJogador : IDbRepositorioRelacionamentoTimeJogador
    {
        private IConnectionProvider _connectionProvider;
        const string SelectField = "id, idTime, idJogador";
        const string NomeTabela = "RelacionamentoTimeJogador";

        public DbRepositorioRelacionamentoTimeJogador(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
        
        public void Atualizar(Time time, Jogador jogador)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"UPDATE {NomeTabela}
                                SET idTime = @IdTime
                                WHERE idJogador = @IdJogador;",
                                new { IdTime = time.Id,
                                    IdJogador = jogador.Id });
            }
        }

        public IEnumerable<Jogador> ObterJogadoresTime(Time time)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<Jogador>($@"SELECT j.id, j.nome
                                                FROM {NomeTabela} as r
                                                JOIN Jogador as j
                                                ON (j.id = r.idJogador)
                                                WHERE r.idTime = @IdTime;",
                                                new { IdTime = time.Id });
            }
        }

        public void Remover(Time time)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"UPDATE {NomeTabela}
                                SET idJogador = null
                                WHERE idTime = @IdTime;",
                                new { IdTime = time.Id });
            }
        }

        public void Inserir(int idTime, int idJogador)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"INSERT INTO {NomeTabela} VALUES
                                (@IdTime, @IdJogador);",
                                new { IdTime = idTime, IdJogador = idJogador });
            }
        }

        public Time ObterTimeDoJogador(int idJogador)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<Time>($@"SELECT t.id, t.nome
                                                FROM {NomeTabela} r
                                                JOIN Time t
                                                ON (t.id = r.idTime)
                                                WHERE r.idJogador = @IdJogador",
                                                new {IdJogador = idJogador}).FirstOrDefault();
            }
        }
    }
}