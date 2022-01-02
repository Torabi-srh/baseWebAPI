using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace baseWebAPI
{
    public interface IExternalDataResolver
    {
        string ConnectionString { get; }
        DataTable GetQueryResult(string queryText);
        DataTable GetQueryResult(string storeProcedureName, string[,] parameters);
        object GetQuerySingleResult(string storeProcedureName, string[,] parameters);
        DataTable GetQueryResult(string storeProcedureName, List<SqlParameter> parameters);
        List<DataTable> GetQueryResults(string storeProcedureName, List<SqlParameter> parameters);
        Task<DataTable> GetQueryResultAsync(string queryText);
        int GetQueryExecutionResult(string queryText); 
        int GetQueryExecutionResult(string storeProcedureName, List<SqlParameter> parameters);
        object GetSingleObjectResult(string queryText);
        Task<object> GetSingleObjectResultAsync(string queryText);
        object GetSingleObjectResult(string queryText, List<SqlParameter> parameters, bool isStoreProcedure = true);
        List<ViewModel> GetFromSp<ViewModel>(string[,] Parametr, string NameSp) where ViewModel : new();
        List<ViewModel> GetFromSp<ViewModel>(string nameSp,List<SqlParameter> parameters) where ViewModel : new();
        List<ViewModel> GetFromSp<ViewModel>(string NameSp) where ViewModel : new();
        List<ViewModel> GetFromCommand<ViewModel>(string nameSp, List<SqlParameter> parameters) where ViewModel : new();
        List<ViewModel> GetFromCommand<ViewModel>(string CommandText) where ViewModel : new();
    }
}
