using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace baseWebAPI.ViewModels.DTO
{
    /// <summary>
    /// پارامتر های صفحه بندی
    /// </summary>
    public class PagingParameterDTO : ISqlServerParameterInterface
    {
        /// <summary>
        /// صفحه درخواستی
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// تعداد رکورد ها در هر صفحه
        /// </summary>
        public int PageSize { get; set; }
        public List<SqlParameter> GetSqlParameters()
        {
            return new List<SqlParameter>()
            {
new SqlParameter(nameof(PageIndex),PageIndex),
new SqlParameter(nameof(PageSize),PageSize), 
            };
        }
    }
}
