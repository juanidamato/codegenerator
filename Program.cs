using codegenerator.BLL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace codegenerator
{
    internal static class Program
    {

       

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            ConfigureServices(services);
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(form1);
            }

            //Application.Run(new MainForm());
        }



        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging();
            
            services.AddSingleton<DatabaseUtilities>();
            services.AddTransient<ODBCConnect>();
            services.AddTransient<MainForm>();
           
        }
    }
}