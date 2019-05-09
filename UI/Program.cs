using DigitalSignage.DAL.EntityFramework;
using DigitalSignage.Domain;
using DigitalSignage.DTO;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DigitalSignage.UI
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .WriteTo.File("C:\\DigitalSignage_Logs\\Logs.txt", rollingInterval: RollingInterval.Day)
                   .CreateLogger();

            Log.Information("Registering Mappings");
            AutoMapperConfig.RegisterMappings();


            Application.Run(new HomeForm());
        }


    }
}






