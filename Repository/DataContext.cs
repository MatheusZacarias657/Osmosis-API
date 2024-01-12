using Microsoft.EntityFrameworkCore;
using Repository.Entities.Appointment;
using Repository.Entities.Customer;
using Repository.Entities.Meeting;
using Repository.Entities.Patient;
using Repository.Entities.Professional;

namespace Repository
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveGuidEntity>().ToTable(tb => tb.HasTrigger("updateExpirationDate"));
            modelBuilder.Entity<ProfessionalEntity>().ToTable(tb => tb.HasTrigger("updateDailyAppointments"));
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //Add Customer Tables
        public DbSet<CustomerEntity> Customer => Set<CustomerEntity>();
        public DbSet<CustomerRoleEntity> CustomerRole => Set<CustomerRoleEntity>();
        public DbSet<ActiveGuidEntity> ActiveGuid => Set<ActiveGuidEntity>();

        //Add Professional Tables
        public DbSet<ProfessionalEntity> Professional => Set<ProfessionalEntity>();

        //Add Appointment Tables
        public DbSet<DailyAppointmentEntity> DailyAppointment => Set<DailyAppointmentEntity>();
        public DbSet<AppointmentEntity> Appointment => Set<AppointmentEntity>();
        public DbSet<AppointmentStatusEntity> AppointmentStatus => Set<AppointmentStatusEntity>();

        //Add Patient Tables
        public DbSet<PatientEntity> Patient => Set<PatientEntity>();
    }
}
