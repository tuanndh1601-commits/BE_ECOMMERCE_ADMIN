using System.Data;

namespace ECOM.APPLICATION.Interfaces.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
