using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace baseWebAPI
{
    public static class IntListUserDefineSqlParameter
    {
        public static SqlParameter GetSqlParameter(string parameterName, List<int> data, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = new SqlParameter($"@{parameterName}", SqlDbType.Structured);
            parameter.Direction = direction;
            using (var filterTable = new DataTable())
            {
                filterTable.Columns.Add("Id", typeof(int)).SetOrdinal(0);

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        filterTable.Rows.Add(item);
                    }
                }

                parameter.TypeName = "dbo.IntList";
                parameter.Value = filterTable;
            }
            return parameter;
        }
    }
}
