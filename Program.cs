using HuskyBot;
using HuskyBot.Data;
using HuskyBot.Interfaces.IServices;
using HuskyBot.Services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync(args);
    public async Task MainAsync(string[] args)
    {
        await InitHost(args);
    }
        private static async Task InitHost(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                var discordBotService = services.GetRequiredService<IDiscordBotService>();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Something went wrong starting up applicaton " + e);
                throw;
            }

            await host.RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
}