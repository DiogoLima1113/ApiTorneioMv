using System.Collections.Generic;
using System.Linq;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Interface;
using Dapper;

namespace api_torneio_mv.Dominio.Repositorio.Classe
{
    public class DbRepositorioJogador : IDbRepositorio<Jogador>
    {
        private IConnectionProvider _connectionProvider;
        const string SelectField = "id, nome";
        const string NomeTabela = "Jogador";

        public DbRepositorioJogador(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void Atualizar(Jogador obj)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"UPDATE {NomeTabela}
                                SET nome = @Nome
                                WHERE id = @Id;",
                                new { Id = obj.Id,
                                    Nome = obj.Nome });
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

        public void Inserir(Jogador obj)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"INSERT INTO {NomeTabela} VALUES
                                (@Nome);", new { Nome = obj.Nome });
            }
        }

        public Jogador Obter(int id)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<Jogador>($@"SELECT {SelectField}
                                                FROM {NomeTabela}
                                                WHERE id = @Id",
                                                new {Id = id}).FirstOrDefault();
            }
        }

        public IEnumerable<Jogador> ObterTodos()
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<Jogador>($@"SELECT {SelectField}
                                                FROM {NomeTabela}");
            }
        }
    }
}