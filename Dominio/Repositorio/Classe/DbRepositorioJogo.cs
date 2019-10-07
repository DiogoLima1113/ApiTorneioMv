using System.Collections.Generic;
using System.Data;
using System.Linq;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Interface;
using Dapper;

namespace api_torneio_mv.Dominio.Repositorio.Classe
{
    public class DbRepositorioJogo : IDbRepositorio<Jogo>
    {
        private IConnectionProvider _connectionProvider;
        const string SelectField = "id, idTimeCasa, idTimeVisitante, pontuacaoTimeCasa, pontuacaoTimeVisitante";
        const string NomeTabela = "Jogo";

        public DbRepositorioJogo(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void Atualizar(Jogo obj)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"UPDATE {NomeTabela}
                                SET idTimeCasa = @IdTimeCasa,
                                    idTimeVisitante = @IdTimeVisitante,
                                    pontuacaoTimeCasa = @PontuacaoTimeCasa,
                                    pontuacaoTimeVisitante = @PontuacaoTimeVisitante
                                WHERE id = @Id;",
                                new { Id = obj.Id,
                                    idTimeCasa = obj.IdTimeCasa,
                                    idTimeVisitante = obj.IdTimeVisitante,
                                    pontuacaoTimeCasa = obj.PontuacaoTimeCasa,
                                    pontuacaoTimeVisitante = obj.PontuacaoTimeVisitante });
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

        public void Inserir(Jogo obj)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                connection.Query($@"INSERT INTO {NomeTabela} VALUES
                                (@IdTimeCasa, @IdTimeVisitante, @PontuacaoTimeCasa,@PontuacaoTimeVisitante);",
                                new { idTimeCasa = obj.IdTimeCasa,
                                    idTimeVisitante = obj.IdTimeVisitante,
                                    pontuacaoTimeCasa = obj.PontuacaoTimeCasa,
                                    pontuacaoTimeVisitante = obj.PontuacaoTimeVisitante });
            }
        }

        public Jogo Obter(int id)
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<Jogo>($@"SELECT {SelectField}
                                            FROM {NomeTabela}
                                            WHERE id = @Id",
                                            new {Id = id}).FirstOrDefault();
            }
        }

        public IEnumerable<Jogo> ObterTodos()
        {
            using(var connection = _connectionProvider.CriarConexao())
            {
                return connection.Query<Jogo>($@"SELECT {SelectField}
                                            FROM {NomeTabela}");
            }
        }
    }
}