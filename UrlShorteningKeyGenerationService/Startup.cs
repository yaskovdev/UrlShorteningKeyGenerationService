using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UrlShorteningKeyGenerationService.Services;

namespace UrlShorteningKeyGenerationService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRandomKeyCreationService, RandomKeyCreationService>();
            services.AddSingleton<ICosmosDbService>(
                InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<IRandomKeyGenerator, RandomKeyGenerator>();

            services.AddHostedService<BackgroundKeyGenerator>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "UrlShorteningKeyGenerationService", Version = "v1"}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlShorteningKeyGenerationService v1"));
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfiguration config)
        {
            string databaseName = config.GetSection("DatabaseName").Value;
            string containerName = config.GetSection("ContainerName").Value;
            string account = config.GetSection("Account").Value;
            string key = config.GetSection("Key").Value;
            CosmosClient client = new(account, key, new CosmosClientOptions {AllowBulkExecution = true});
            CosmosDbService cosmosDbService = new(client, databaseName, containerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            return cosmosDbService;
        }
    }
}
