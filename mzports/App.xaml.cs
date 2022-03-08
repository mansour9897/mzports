using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using mzports.Services.TCM;

namespace mzports
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ServiceProvider? serviceProvider;
        public static ServiceProvider? Service
        {
            get => serviceProvider;
            set
            {
                serviceProvider = value;
            }
        }

        public App()
        {

        }

        private void ConfigureServices(ServiceCollection services)
        {
            //Serices
            _ = services.AddScoped<TemperatureControllerModule>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
    }
}
