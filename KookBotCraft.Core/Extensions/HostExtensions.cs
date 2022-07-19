using KaiHeiLa.Commands;
using KaiHeiLa.WebSocket;
using KookBotCraft.Core.Options;
using KookBotCraft.Core.Services;
using KookBotCraft.Database.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KookBotCraft.Core.Extensions {
    public static class HostExtensions {
        public static IHostBuilder AddBotCraft(this IHostBuilder builder) {

            return builder.UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(builder => {
                    builder.AddSimpleConsole(options => {
                        options.IncludeScopes = true;
                        options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                    }).AddFile(option => option.RootPath = AppContext.BaseDirectory);
                });
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services) {
            services.AddSingleton(new KaiHeiLaSocketClient(new KaiHeiLaSocketConfig {
                LogLevel = KaiHeiLa.LogSeverity.Verbose
            }))
                .AddSingleton(_ => new CommandService(new CommandServiceConfig {
                    LogLevel = KaiHeiLa.LogSeverity.Verbose
                }))
                .AddHostedService<BotHostService>()
                .AddHostedService<CommandHandleService>()
                .AddHostedService<BotMarketStatusService>()
                .AddDbContext<DefaultDbContext>(option => option.UseSqlite(context.Configuration.GetConnectionString("DefaultDbContext")));

            services.AddOptions<KookSettings>().Bind(context.Configuration.GetSection("Kook"));
            services.AddOptions<BotMarketSettings>().Bind(context.Configuration.GetSection("BotMarket"));
        }
    }
}
