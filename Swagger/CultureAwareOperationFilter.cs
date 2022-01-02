using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace baseWebAPI
{
    /// <summary>
    /// فیلتر اعمال محدودیت ها برای زبان های مختلف
    /// </summary>
    public class CultureAwareOperationFilter : IOperationFilter
    {
        /// <summary>
        /// apply culture filter on swagger custom filters
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //throw new NotImplementedException();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Description = "زبان دریافت اطلاعات از سرور را مشخص کنید",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Enum = new[] { new OpenApiString("fa-IR"), new OpenApiString("en-US"),new OpenApiString("ar-AE")},
                    Type = "String",
                    Default = new OpenApiString("fa-IR")
                }
            });
        }
    }
}
