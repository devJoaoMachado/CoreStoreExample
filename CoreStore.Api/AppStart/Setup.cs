using CoreStore.Application.CustomerModule.Command;
using CoreStore.Infra.Data.Data;
using CoreStore.Shared.Command;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace CoreStore.Api.AppStart
{
    public static class Setup
    {
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorCommand<,>));

            services.AddMediatR(typeof(Startup).Assembly,
                                typeof(CreateCustomerCommand).Assembly,
                                typeof(ValidatorCommand<,>).Assembly);
        }

        public static void ConfigureCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(
                options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
        }

        public static void ConfigureDataBaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSettings = new ConnectionSettings();

            configuration.GetSection("ConnectionSettings").Bind(connectionSettings);

            services.AddSingleton(connectionSettings);

            services.AddScoped(typeof(DatabaseConnection));
        }
    }
}
