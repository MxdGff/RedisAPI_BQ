using RedisHelp;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace RedisAPI_BQ
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            RedisManager.SetIConfiguration(configuration);
            RedisManager.InitReidsConnectInfo();
            ServiceStackHelper.Patch();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<RedisAPIService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod()
                );
            });
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            //添加swagger配置
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Api Swagger Doc",
                    Description = "try to use swagger build api doc",
                    Version = "v1"
                });
                // xml文档绝对路径
                var file = Path.Combine(AppContext.BaseDirectory, "RedisAPI_BQ.xml");
                // xml文档绝对路径
                var path = Path.Combine(AppContext.BaseDirectory, file);
                // true : 显示控制器层注释
                options.IncludeXmlComments(path, true);
                // 对action的名称进行排序，如果有多个，就可以看见效果了。
                options.OrderActionsBy(o => o.RelativePath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //加入swagger中间件
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SWAGGER DOC");
                });
            }

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
