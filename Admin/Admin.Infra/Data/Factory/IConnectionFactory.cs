using System.Data;

namespace Admin.Infra.Data.Factory
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
