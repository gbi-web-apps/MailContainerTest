using MailContainerTest.Abstractions;
using MailContainerTest.Data;
using MailContainerTest.Entities;
using MailContainerTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailContainerTest
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMailContainerTest(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(DataStoreTypeOptions.DataStoreTypeConfig);
            services.Configure<DataStoreTypeOptions>(options => section.Bind(options));

            services.AddScoped<IMailContainerDataStoreFactory, MailContainterDataStoreFactory>();
            services.AddScoped<IMailTransferService, MailTransferService>();

            return services;
        }
    }
}