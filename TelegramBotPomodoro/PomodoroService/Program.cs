using NLog;
using NLog.Extensions.Logging;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Models;
using Shared.Services;
using Shared.Services.Rabbit;
using PomodoroService.Services;
using Shared.Services.Telegram;
using PomodoroService.Models;

/* TODO:
 * Localization Project
 * Lock on list write/read
 * Standart Telegram functions Project (change language, sponsorship, report bug)
 * Change language
 * Link to sponsorship
 * Report bug
 * 
 */

namespace PomodoroService
{
    internal class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var unused = new HostBuilder()
                // ReSharper disable once RedundantAssignment
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true);

                    Configuration = builder.Build();
                })
                .Build();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                    logging.AddNLog(LogManager.LoadConfiguration("NLog.config").Configuration))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
                    services.AddTransient<IConfig, Config>();
                    services.AddTransient<IPomodoroConfig, PomodoroConfig>();
                    services.AddSingleton<IMQService, RabbitMQService>();
                    services.AddTransient<IAnswerSender, AnswerSender>();
                    services.AddTransient<IIntervalController, IntervalController>();
                    services.AddSingleton<IRepository<WorkInterval>, WorkIntervalRepository>();
                    services.AddHostedService<Worker>();
                });
        }
    }
}