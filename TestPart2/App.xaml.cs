using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Configuration;
using System.Data;
using System.Windows;
using TestPart2.Services;
using TestPart2.ViewModels;

namespace TestPart2
{

    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Log.Logger = new LoggerConfiguration()
              .WriteTo.File($"test-sms-console-app-{DateTime.Now:yyyyMMdd}.log")
              .CreateLogger();
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ILogger>(Log.Logger);
            services.AddSingleton<IEnvironmentVariableService, EnvironmentVariableService>();

           
            services.AddSingleton<MainViewModel>();
            

            Services = services.BuildServiceProvider();

             
            var mainWindow = new MainWindow()
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };

            MainWindow = mainWindow;
            MainWindow.Show();
        }
    }
}
