using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace baseWebAPI
{
    public class SqlServerDataResolver : IExternalDataResolver
    {
        public string ConnectionString { get; set; }
        public DataTable GetQueryResult(string queryText)
        {
            string tableName = "QueryResultTable";
            DataSet dset = new DataSet(tableName);
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(queryText, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        dAdapter.Fill(dset, tableName);
                    }
                    catch (Exception error)
                    {
                        throw new Exception("خطا در دریافت اطلاعات از منبع خارجی", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return dset.Tables[tableName];
        }
        public DataTable GetQueryResult(string storeProcedureName, string[,] parameters)
        {
            string tableName = "QueryResultTable";
            DataSet dset = new DataSet(tableName);
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(storeProcedureName, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        dAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        var countParametr = parameters.GetLength(0);
                        for (int i = 0; i < countParametr; i++)
                        {
                            dAdapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = parameters[i, 0], Value = parameters[i, 1] });
                        }
                        dAdapter.Fill(dset, tableName);
                    }
                    catch (Exception error)
                    {
                        throw new Exception("خطا در دریافت اطلاعات از منبع خارجی", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return dset.Tables[tableName];
        }
        public object GetQuerySingleResult(string storeProcedureName, string[,] parameters)
        {
            string tableName = "QueryResultTable";
            DataSet dset = new DataSet(tableName);
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(storeProcedureName, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        dAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        var countParametr = parameters.GetLength(0);
                        for (int i = 0; i < countParametr; i++)
                        {
                            dAdapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = parameters[i, 0], Value = parameters[i, 1] });
                        }
                        dAdapter.Fill(dset, tableName);
                    }
                    catch (Exception error)
                    {
                        throw new Exception("خطا در دریافت اطلاعات از منبع خارجی", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return dset.Tables[tableName].Rows[0].ItemArray[0];
        }
        public DataTable GetQueryResult(string storeProcedureName, List<SqlParameter> parameters)
        {
            string tableName = "QueryResultTable";
            DataSet dset = new DataSet(tableName);
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(storeProcedureName, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        dAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        foreach (var p in parameters)
                        {
                            dAdapter.SelectCommand.Parameters.Add(p);
                        }
                        dAdapter.Fill(dset, tableName);
                    }
                    catch (Exception error)
                    {
                        throw new Exception("خطا در دریافت اطلاعات از منبع خارجی", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return dset.Tables[tableName];
        }
        public List<DataTable> GetQueryResults(string storeProcedureName, List<SqlParameter> parameters)
        {
            string tableName = "QueryResultTable";
            DataSet dset = new DataSet(tableName);
            List<DataTable> result = new List<DataTable>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(storeProcedureName, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        dAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        foreach (var p in parameters)
                        {
                            dAdapter.SelectCommand.Parameters.Add(p);
                        }
                        dAdapter.Fill(dset, tableName);
                    }
                    catch (Exception error)
                    {
                        throw new Exception("خطا در دریافت اطلاعات از منبع خارجی", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                    for (int i = 0; i < dset.Tables.Count; i++)
                    {
                        result.Add(dset.Tables[i]);
                    }
                }
            }
            return result;
        }
        public async Task<DataTable> GetQueryResultAsync(string queryText)
        {
            string tableName = "QueryResultTable";
            DataSet dset = new DataSet(tableName);
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(queryText, sqlConnection))
                {
                    try
                    {
                        await sqlConnection.OpenAsync();
                        dAdapter.Fill(dset, tableName);
                    }
                    catch (Exception error)
                    {
                        throw new Exception("خطا در دریافت اطلاعات از منبع خارجی", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return dset.Tables[tableName];
        }
        public object GetSingleObjectResult(string queryText, List<SqlParameter> parameters, bool isStoreProcedure = true)
        {
            object result = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryText, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();

                        sqlCommand.CommandType = isStoreProcedure == true ? CommandType.StoredProcedure : CommandType.Text;
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                        result = sqlCommand.ExecuteScalar();
                    }
                    catch (Exception error)
                    {
                        throw new Exception("GetSingleObjectResult", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return result;
        }
        public object GetSingleObjectResult(string queryText)
        {
            object result = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryText, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        result = sqlCommand.ExecuteScalar();
                    }
                    catch (Exception error)
                    {
                        throw new Exception("GetSingleObjectResult", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return result;
        }
        public async Task<object> GetSingleObjectResultAsync(string queryText)
        {
            object result = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryText, sqlConnection))
                {
                    try
                    {
                        await sqlConnection.OpenAsync();
                        result = await sqlCommand.ExecuteScalarAsync();
                    }
                    catch (Exception error)
                    {
                        throw new Exception("GetSingleObjectResult", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return result;
        }
        public int GetQueryExecutionResult(string queryText)
        {
            int result = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryText, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        result = sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception error)
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                        throw new Exception("GetQueryExecutionResult", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return result;
        }
        public int GetQueryExecutionResult(string storeProcedureName, List<SqlParameter> parameters)
        {
            int result = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(storeProcedureName, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                        result = sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception error)
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                        throw new Exception("GetQueryExecutionResult", error);
                    }
                    finally
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                            sqlConnection.Close();
                    }
                }
            }
            return result;
        }

        public List<ViewModel> GetFromSp<ViewModel>(string[,] Parametr, string NameSp) where ViewModel : new()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = NameSp;
                    cmd.CommandType = CommandType.StoredProcedure;

                    var countParametr = Parametr.GetLength(0);
                    for (int i = 0; i < countParametr; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = Parametr[i, 0], Value = Parametr[i, 1] });
                    }
                    List<ViewModel> list = new List<ViewModel>();
                    cmd.Connection.Open();
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                var entity = typeof(ViewModel);
                                var propDict = new Dictionary<string, PropertyInfo>();
                                var props = entity.GetProperties
                       (BindingFlags.Instance | BindingFlags.Public);
                                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                                while (reader.Read())
                                {
                                    ViewModel newobject = new ViewModel();

                                    for (int index = 0; index < reader.FieldCount; index++)
                                    {
                                        if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                                        {
                                            var info = propDict[reader.GetName(index).ToUpper()];
                                            if ((info != null) && info.CanWrite)
                                            {
                                                var val = reader.GetValue(index);
                                                info.SetValue(newobject, (val == DBNull.Value) ? null : val, null);
                                            }
                                        }
                                    }
                                    list.Add(newobject);
                                }
                            }
                            return list;
                        }
                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }
                }
            }
        }
         
        public List<ViewModel> GetFromSp<ViewModel>(string nameSp, List<SqlParameter> parameters) where ViewModel : new()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = nameSp;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    List<ViewModel> list = new List<ViewModel>();
                    cmd.Connection.Open();
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                var entity = typeof(ViewModel);
                                var propDict = new Dictionary<string, PropertyInfo>();
                                var props = entity.GetProperties
                       (BindingFlags.Instance | BindingFlags.Public);
                                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                                while (reader.Read())
                                {
                                    ViewModel newobject = new ViewModel();

                                    for (int index = 0; index < reader.FieldCount; index++)
                                    {
                                        if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                                        {
                                            var info = propDict[reader.GetName(index).ToUpper()];
                                            if ((info != null) && info.CanWrite)
                                            {
                                                var val = reader.GetValue(index);
                                                info.SetValue(newobject, (val == DBNull.Value) ? null : val, null);
                                            }
                                        }
                                    }
                                    list.Add(newobject);
                                }
                            }
                            return list;
                        }
                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }
                }
            }
        }
        public List<ViewModel> GetFromSp<ViewModel>(string nameSp) where ViewModel : new()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = nameSp;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection.Open();
                    List<ViewModel> list = new List<ViewModel>();
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                var entity = typeof(ViewModel);
                                var propDict = new Dictionary<string, PropertyInfo>();
                                var props = entity.GetProperties
                       (BindingFlags.Instance | BindingFlags.Public);
                                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                                while (reader.Read())
                                {
                                    ViewModel newobject = new ViewModel();

                                    for (int index = 0; index < reader.FieldCount; index++)
                                    {
                                        if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                                        {
                                            var info = propDict[reader.GetName(index).ToUpper()];
                                            if ((info != null) && info.CanWrite)
                                            {
                                                var val = reader.GetValue(index);
                                                info.SetValue(newobject, (val == DBNull.Value) ? null : val, null);
                                            }
                                        }
                                    }
                                    list.Add(newobject);
                                }
                            }
                            return list;
                        }
                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }
                }
            }
        }
        public List<ViewModel> GetFromCommand<ViewModel>(string nameSp, List<SqlParameter> parameters) where ViewModel : new()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = nameSp;
                    cmd.Parameters.AddRange(parameters.ToArray());
                    List<ViewModel> list = new List<ViewModel>();
                    cmd.Connection.Open();
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                var entity = typeof(ViewModel);
                                var propDict = new Dictionary<string, PropertyInfo>();
                                var props = entity.GetProperties
                       (BindingFlags.Instance | BindingFlags.Public);
                                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                                while (reader.Read())
                                {
                                    ViewModel newobject = new ViewModel();

                                    for (int index = 0; index < reader.FieldCount; index++)
                                    {
                                        if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                                        {
                                            var info = propDict[reader.GetName(index).ToUpper()];
                                            if ((info != null) && info.CanWrite)
                                            {
                                                var val = reader.GetValue(index);
                                                info.SetValue(newobject, (val == DBNull.Value) ? null : val, null);
                                            }
                                        }
                                    }
                                    list.Add(newobject);
                                }
                            }
                            return list;
                        }
                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }
                }
            }
        }
        public List<ViewModel> GetFromCommand<ViewModel>(string commandText) where ViewModel : new()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.Text;

                    List<ViewModel> list = new List<ViewModel>();
                    try
                    {
                        cmd.Connection.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                var entity = typeof(ViewModel);
                                var propDict = new Dictionary<string, PropertyInfo>();
                                var props = entity.GetProperties
                       (BindingFlags.Instance | BindingFlags.Public);
                                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                                while (reader.Read())
                                {
                                    ViewModel newobject = new ViewModel();

                                    for (int index = 0; index < reader.FieldCount; index++)
                                    {
                                        if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                                        {
                                            var info = propDict[reader.GetName(index).ToUpper()];
                                            if ((info != null) && info.CanWrite)
                                            {
                                                var val = reader.GetValue(index);
                                                info.SetValue(newobject, (val == DBNull.Value) ? null : val, null);
                                            }
                                        }
                                    }
                                    list.Add(newobject);
                                }
                            }
                            return list;
                        }
                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                        {
                            cmd.Connection.Close();
                        }
                    }
                }
            }
        }

    }
}
