using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace baseWebAPI.DataLayer.InputFormatter.applicationJson
{
    /// <summary>
    /// هدر های معتبر
    /// </summary>
    internal class MediaTypeHeaderValues
    {
        public static readonly MediaTypeHeaderValue ApplicationJson
            = MediaTypeHeaderValue.Parse("application/json").CopyAsReadOnly();
    }
    /// <summary>
    /// تبدیل اعداد فارسی و انگلیسی و حروف ک و ی
    /// </summary>
    public class CustomPersianCharBodyFormatter : IInputFormatter
    {
        public bool CanRead(InputFormatterContext context)
        {
            return true;
        }

        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            try
            {
                if (context == null)
                {
                    return await InputFormatterResult.SuccessAsync(null);
                }
                var request = context.HttpContext.Request;
                using var reader = new StreamReader(request.Body);
                var content = await reader.ReadToEndAsync();
                // ک و ی
                content = content.Replace((char)1610, (char)1740).Replace((char)1603, (char)1705);
                // اعداد بین 0 تا ۹
                for (int i = 1776; i < 1785; i++)
                {
                    var oldChar = (char)i;
                    var newChar = (char)(i - 1728);
                    content = content.Replace(oldChar, newChar);
                }
                Type type = context.ModelType;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                var results = JsonConvert.DeserializeObject(content, type, settings);
                return await InputFormatterResult.SuccessAsync(results);
            }
            catch
            {
                return await InputFormatterResult.SuccessAsync(null);
            }
        }

    }
}
