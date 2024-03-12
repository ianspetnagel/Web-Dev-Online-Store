using System.Data.Common;
using System.Data;

namespace ProductsAppRPSpetnagel.DataLayer
{
    public interface IDataAccess
    {
        // transaction capable methods, last three parameters: Dbonnection,
        // DbTransaction and bTransaction are optional
        object GetSingleAnswer(string sql, List<DbParameter> PList,
        DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false);
        DataTable GetManyRowsCols(string sql, List<DbParameter> PList,
        DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false);
        int InsertUpdateDelete(string sql, List<DbParameter> PList,
        DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false);
    }
}
