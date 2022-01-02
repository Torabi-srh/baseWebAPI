using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using baseWebAPI.BO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace baseWebAPI
{
    /// <summary>
    /// Startip class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// کلید برای رمز کردن پسورد دیتابیس
        /// </summary>
        public static string EncriptionKey = "BFDCT2017SCS";
        /// <summary>
        /// کلید برای رمز کردن پسورد کاربر
        /// </summary>
        public static string UEncriptionKey = "btee123!@#";
        /// <summary>
        /// تنظیمات سیستم را نگهداری می کند
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// ارتباط رمزگشایی شده از بانک اطلاعاتی
        /// </summary>
        public SqlConnectionStringBuilder DecryptedConnection { get; set; }
        /// <summary>
        /// CTR
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
            var conString = Configuration.GetConnectionString("LocalConnection");
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Docker")
            {
                conString = Configuration.GetConnectionString("DefaultConnection");
            }
            DecryptedConnection = new SqlConnectionStringBuilder(conString);
            DecryptedConnection.Password = PersianUtilCore.Security.Cryptography.DecryptByMD5(DecryptedConnection.Password, EncriptionKey);

        }
        /// <summary>
        /// Configuration Available Service
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddDataProtection();
            services.Configure<baseWebAPI.Option.BridgeOption>(Configuration); 
            services.AddMvc() 
                .AddNewtonsoftJson((option) =>
                {
                    option.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddMvcOptions(option =>
                {
                    option.AllowEmptyInputInBodyModelBinding = false;
                    option.EnableEndpointRouting = false;
                    //option.InputFormatters.Insert(0, new CustomPersianCharBodyFormatter());
                    //option.Filters.Insert(0, new CheckModelState());
                });
            services.AddApiVersioning((option) =>
            {
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddScoped<IExternalDataResolver>(_ => new SqlServerDataResolver()
            {
                ConnectionString = DecryptedConnection.ConnectionString,
            });
            services.AddRouting();
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Bearer Authentication 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var encryptionkey = Encoding.UTF8.GetBytes(TokenBO.BWATokenEncryptionKey);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero, //the default for this setting is 5 minutes
                        ValidIssuer = TokenBO.IssuerTitle,
                        ValidAudience = TokenBO.AudienceTitle,
                        IssuerSigningKey = JwtSecurityKey.Create(TokenBO.BWATokenSecretKey),
                        TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                                context.Response.Headers.Add("Provider", "alephba-system.ir");
                            }
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };

                    options.SaveToken = true;
                });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
            #endregion 
            services.AddSwaggerGen(opt =>
            {
                opt.OperationFilter<SwaggerDefaultValues>(); 
                //opt.OperationFilter<CultureAwareOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configuraton Startup pipelines
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseDefaultFiles();
                app.UseStaticFiles();
            }
            app.UseCors("CorsPolicy");
            var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("fa-IR"), new CultureInfo("ar-AE") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();

            app.UseMvc();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(
              options =>
              {
                  foreach (var description in provider.ApiVersionDescriptions)
                  {
                      options.SwaggerEndpoint(
                          $"/swagger/{description.GroupName}/swagger.json",
                          description.GroupName);
                      options.RoutePrefix = string.Empty;
                  }
              }); 
        }
    }
}
