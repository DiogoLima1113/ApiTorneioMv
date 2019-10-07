using System.Collections.Generic;
using System.Data;
using System.Linq;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Interface;
using Dapper;

namespace api_torneio_mv.Dominio.Repositorio.Classe
{
    public class DbRepositorioRelacionamentoTimeJogador : IDbRepositorio<RelacionamentoTimeJogador>
    {
        private IConnectionProvider _connectionProvider;
        const string SelectField = "id, idTime, idJogador";
        const string NomeTabela = "RelacionamentoTimeJogador";

        public DbRepositorioRelacionamentoTimeJogador(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
        
        public void Atualizar(RelacionamentoTimeJogador obj)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"UPDATE {NomeTabela}
                                SET idTime = @IdTime, idJogador = @IdJogador
                                WHERE id = @Id;",
                                new { Id = obj.Id,
                                    IdTime = obj.IdTime,
                                    IdJogador = obj.IdJogador });
            }
        }

        public void Deletar(int id)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"DELETE FROM {NomeTabela}
                                WHERE id = @Id;",
                                new { Id = id });
            }
        }

        public void Inserir(RelacionamentoTimeJogador obj)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"INSERT INTO {NomeTabela} VALUES
                                (@IdTime, @IdJogador);",
                                new { IdTime = obj.IdTime, IdJogador = obj.IdJogador });
            }
        }

        public RelacionamentoTimeJogador Obter(int id)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<RelacionamentoTimeJogador>($@"SELECT {SelectField}
                                                                FROM {NomeTabela}
                                                                WHERE id = @Id",
                                                                new {Id = id}).FirstOrDefault();
            }
        }

        public IEnumerable<RelacionamentoTimeJogador> ObterTodos()
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<RelacionamentoTimeJogador>($@"SELECT {SelectField}
                                                                FROM {NomeTabela}");
            }
        }
    }
}