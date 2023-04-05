using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Services.Integration.Sql
{
    public interface IConnectionManager
    {
        SqlConnection Connection { get; }
        Task Establish();
        Task Close();
    }
}
