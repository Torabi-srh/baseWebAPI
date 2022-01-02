using System.Collections.Generic;
using System.Data.SqlClient;

namespace baseWebAPI
{
    /// <summary>
    /// پیاده سازی کلاس های مربوط با پارامتر های SQL
    /// </summary>
    public interface ISqlServerParameterInterface
    {
        /// <summary>
        /// لیست پارامتر ها را فراخوانی می کند
        /// </summary>
        /// <returns></returns>
        List<SqlParameter> GetSqlParameters();
    }
}
