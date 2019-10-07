using System.Data;
using System.Data.SqlClient;
using api_torneio_mv.Dominio.Repositorio.Interface;
using Microsoft.Extensions.Configuration;

namespace api_torneio_mv.Dominio.Repositorio.Classe
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionString ;

        public ConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("torneio");
        }

        public IDbConnection CriarConexao()
        {
            return new SqlConnection(_connectionString);
        }
    }
}