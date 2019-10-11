using System.Data;

namespace ApiTorneioMv.Dominio.Repositorio.Interface
{
    public interface IConnectionProvider
    {
        IDbConnection CriarConexao();
    }
}