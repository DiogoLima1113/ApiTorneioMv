using System.Data;
using System.Data.SqlClient;
using ApiTorneioMv.Dominio.Repositorio.Interface;
using Microsoft.Extensions.Configuration;

namespace ApiTorneioMv.Dominio.Repositorio.Classe
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