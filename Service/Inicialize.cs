using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces.Appointment;
using Service.Interfaces.Customer;
using Service.Interfaces.HttpFactory;
using Service.Interfaces.OAuth;
using Service.Interfaces.Patient;
using Service.Interfaces.Professional;
using Service.Services.Appointment;
using Service.Services.Customer;
using Service.Services.HttpFactory;
using Service.Services.OAuth;
using Service.Services.Patient;
using Service.Services.Professional;

namespace Service
{
    public static class Inicialize
    {
        public static IServiceCollection InicializeService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Factory
            services.AddSingleton<IHttpFactoryRequests, HttpFactoryRequests>();

            //Add Customer Services
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IActiveGuidService, ActiveGuidService>();
            services.AddTransient<ICustomerRoleService, CustomerRoleService>();

            //Add OAuth Services
            services.AddTransient<ITemporayPasswordGenerator, TemporayPasswordGenerator>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IOAuthService, OAuthService>();
            services.AddTransient<ICustomAuthenticationService, CustomAuthenticationService>();

            //Add Professional Services
            services.AddTransient<IProfessionalService, ProfessionalService>();

            //Add Appointment Services
            services.AddTransient <IAppointmentService, AppointmentService>();

            //Add Patient Services
            services.AddTransient<IPatientService, PatientService>();

            return services;
        }
    }
}
