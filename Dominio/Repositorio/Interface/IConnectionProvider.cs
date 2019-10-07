using System.Data;

namespace api_torneio_mv.Dominio.Repositorio.Interface
{
    public interface IConnectionProvider
    {
        IDbConnection CriarConexao();
    }
}