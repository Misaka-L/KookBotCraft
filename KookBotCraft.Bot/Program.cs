using KookBotCraft.Application.Services;
using KookBotCraft.Core.Extensions;
using KookBotCraft.Database.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KookBotCraft.Bot {
    internal class Program {
        static Task Main(string[] args) => new Program().MainAsync(args);

        public async Task MainAsync(string[] args) {
            var host = Host.CreateDefaultBuilder(args)
                .AddBotCraft()
                .ConfigureServices(ConfigureServices);

            await host.RunConsoleAsync();
        }

        public void ConfigureServices(HostBuilderContext context, IServiceCollection services) {
            services.AddTransient<MessageService>();
        }
    }
}