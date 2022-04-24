using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using src.SqlLiteReaderModel.Model;
using src.SqlLiteReaderModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace src.SqlLiteReaderConsole
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            ServiceProvider services = new ServiceCollection()
                                            .AddSingleton<IDatabaseConnection, SqlLiteConnection>()
                                            .AddSingleton<IDatabaseInformation, SqlLiteDatabaseInformation>()
                                            .AddSingleton<ICareerInformation, SqlLiteCareerInformation>()
                                            .BuildServiceProvider();


            IDatabaseConnection connection = services.GetRequiredService<IDatabaseConnection>();
            connection.SetConnectionString(@"E:\Downloads\20220421_Backup_cp.db");

            IDatabaseInformation information = services.GetRequiredService<IDatabaseInformation>();
            ICareerInformation career = services.GetRequiredService<ICareerInformation>();
            Console.WriteLine(string.Concat("IL-2 Career Editor Tool V", ""));
            Console.WriteLine(string.Concat("Database Version ", information.GetDatabaseVersion()));
            Console.WriteLine(string.Empty);

            foreach (Pilot pilot in career.GetPilots())
            {
                Console.WriteLine(string.Format("=== Pilot entry for pilot number: {0} ===", pilot.PilotId));
                Console.WriteLine(string.Format("Name:         {0}", pilot.FirstName));
                Console.WriteLine(string.Format("Last name:    {0}", pilot.LastName));
                Console.WriteLine(string.Format("State:        {0}", pilot.Alive ? "Alive" : "Missed or KIA"));
                Console.WriteLine(string.Format("Airfield:     {0}", pilot.currentAirfield));
                Console.WriteLine(string.Format("=== Pilot entry end ===", pilot.LastName, pilot.FirstName));
                Console.WriteLine(string.Empty);
            }
        }
    }
}