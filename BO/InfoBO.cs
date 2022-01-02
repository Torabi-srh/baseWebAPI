using baseWebAPI.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace baseWebAPI.BO
{
    /// <summary>
    /// مدیریت ساختار های بانک اطلاعاتی
    /// </summary>
    public class InfoBO
    {
        private readonly IExternalDataResolver _externalDataReolver;
        /// <summary>
        /// سازنده
        /// </summary>
        public InfoBO(IExternalDataResolver externalDataReolver)
        {
            _externalDataReolver = externalDataReolver;
        }

        /// <summary>
        /// دریافت اطلاعات اتصال به بانک اطلاعاتی
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public SqlConnectionInfo ConnectionInfo(string connectionString)
        {
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(connectionString);
            if (!string.IsNullOrEmpty(connectionString))
            {
                SqlConnectionInfo connectionInfo = new SqlConnectionInfo
                {
                    ServerAddress = connectionBuilder.DataSource,
                    DatabaseName = connectionBuilder.InitialCatalog,
                    DatabaseUsername = connectionBuilder.UserID,
                };
                return connectionInfo;
            }
            else
                return null;
        }
        /// <summary>
        /// تست بر قراری ارتباط با سرور
        /// </summary>
        /// <returns></returns>
        public bool TestConnection()
        {
            bool result = false;
            try
            {
                using SqlConnection connection = new SqlConnection()
                {
                    ConnectionString = _externalDataReolver.ConnectionString
                };
                connection.Open();
                result = true;
            }
            catch {
                result = false;
            }

            return result;
        }
    }
}
