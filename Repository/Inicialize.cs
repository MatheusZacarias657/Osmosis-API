using DbUp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces.Appointment;
using Repository.Interfaces.Base;
using Repository.Interfaces.Customer;
using Repository.Interfaces.Patient;
using Repository.Interfaces.Professional;
using Repository.Repositories.Appointment;
using Repository.Repositories.Base;
using Repository.Repositories.Customer;
using Repository.Repositories.Patient;
using Repository.Repositories.Professional;

namespace Repository
{
    public static class Inicialize
    {
        public static IServiceCollection InicializeRepository(this IServiceCollection services, IConfiguration configuration)
        {
            //Add SQL Connection and Start DB
            string sqlConnection = configuration.GetConnectionString("SQLConnection");
            ChechDBIntegrity(sqlConnection, configuration["Settings:MigrationVersion"]);
            services.AddDbContext<DataContext>(options => options.UseSqlServer(sqlConnection));

            //Add BaseRepository
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Add Customer Repositories
            services.AddTransient<IActiveGuidRepository, ActiveGuidRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerRoleRepository, CustomerRoleRepository>();

            //Add Professional Repositories
            services.AddTransient<IProfessionalRepository, ProfessionalRepository>();
            services.AddTransient<IProfessionalValidRepository, ProfessionalRepository>();

            //Add Appointments Repositories
            services.AddTransient<IAppointmentProfessionalRepository, AppointmentRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IAppointmentStatusRepository, AppointmentStatusRepository>();
            services.AddTransient<IDailyAppointmentRepository, DailyAppointmentRepository>();

            //Add Patient Repositories
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPatientValidRepository, PatientRepository>();

            return services;
        }

        private static void ChechDBIntegrity(string sqlConnection, string migrationVersion)
        {
            Console.WriteLine("Chekin DB Integrity...");
            EnsureDatabase.For.SqlDatabase(sqlConnection);

            var upgrader = DeployChanges.To
                .SqlDatabase(sqlConnection)
                .WithScriptsFromFileSystem($"../Repository/DbScripts/Migrations_{migrationVersion}")
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DB was atualized");
            Console.ResetColor();
        }
    }
}
