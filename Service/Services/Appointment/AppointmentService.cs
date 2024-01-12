using Repository.Entities.Appointment;
using Repository.Entities.Meeting;
using Repository.Interfaces.Appointment;
using Repository.Interfaces.Patient;
using Repository.Interfaces.Professional;
using Service.DTOs.Appointments;
using Service.Interfaces.Appointment;
using Service.Utils.Enums;
using Service.Utils.Tools;

namespace Service.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IDailyAppointmentRepository dailyAppointmentRepository;
        private readonly IProfessionalValidRepository professionalRepository;
        private readonly IAppointmentStatusRepository statusRepository;
        private readonly IPatientValidRepository patientRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IDailyAppointmentRepository dailyAppointmentRepository, IProfessionalValidRepository professionalRepository, IAppointmentStatusRepository statusRepository, IPatientValidRepository patientRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.dailyAppointmentRepository = dailyAppointmentRepository;
            this.professionalRepository = professionalRepository;
            this.statusRepository = statusRepository;
            this.patientRepository = patientRepository;
        }

        public bool Add(AppointmentRegister appointment)
        {
            try
            {
                if (!professionalRepository.CheckIfProfesionalExist(appointment.id_professional))
                    throw new Exception("The Profisional doesn't exist");

                if (!patientRepository.CheckIfPatientExist(appointment.id_patient))
                    throw new Exception("The Pacient doesn't exist");

                if (!ValidAppointment(appointment))
                    return false;

                AppointmentEntity entity = new AppointmentEntity();
                ObjectTools.CopyProperties(entity, appointment, true);
                appointmentRepository.Add(entity);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AppointmentRegister> SearchAppointments(Dictionary<string, string>? filters)
        {
            try
            {
                List<AppointmentEntity> entities = new List<AppointmentEntity>();
                if (filters.Count == 0 || filters == null)
                    entities = appointmentRepository.GetAllAppointments();
                else
                    entities = appointmentRepository.FindByCustomFilter(filters);

                List<AppointmentRegister> appointments = new List<AppointmentRegister>();

                foreach (AppointmentEntity entity in entities)
                {
                    AppointmentRegister appointment = new AppointmentRegister();
                    ObjectTools.CopyProperties(appointment, entity, false);
                    appointments.Add(appointment);
                }

                return appointments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DateTime> FindAppointmentsAvailable(int profissionalId, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                if (dateEnd <= dateStart)
                    throw new Exception("Invalid Period");

                if (!professionalRepository.CheckIfProfesionalExist(profissionalId))
                    throw new Exception("The Profisional doesn't exist");

                List<AppointmentEntity> activeAppointments = appointmentRepository.FindAppointmentsByProfessionalId(profissionalId, dateStart, dateEnd);
                List<DailyAppointmentEntity> dailyAppointments = dailyAppointmentRepository.FindDailyAppointmentByProfessionalId(profissionalId);
                List<DateTime> availableAppointments = new List<DateTime>();

                if (activeAppointments.Count == 0)
                {
                    for (DateTime i = dateStart; i <= dateEnd; i = i.AddDays(1))
                    {
                        foreach (DailyAppointmentEntity dailyAppointment in dailyAppointments)
                        {
                            DateTime appointmentTime = new DateTime(i.Year, i.Month, i.Day, dailyAppointment.startTime.Hours, dailyAppointment.startTime.Minutes, dailyAppointment.startTime.Seconds);
                            availableAppointments.Add(appointmentTime);
                        }
                    }
                }
                else
                {
                    for (DateTime i = dateStart; i <= dateEnd; i = i.AddDays(1))
                    {
                        List<AppointmentEntity> dayAppointments = activeAppointments.Where(activeAppointment => activeAppointment.appointmentTime.Date == i.Date).ToList();
                        foreach (DailyAppointmentEntity dailyAppointment in dailyAppointments)
                        {
                            if (!dayAppointments.Exists(dayAppointment => dayAppointment.appointmentTime.TimeOfDay == dailyAppointment.startTime))
                            {
                                DateTime appointmentTime = new DateTime(i.Year, i.Month, i.Day, dailyAppointment.startTime.Hours, dailyAppointment.startTime.Minutes, dailyAppointment.startTime.Seconds);
                                availableAppointments.Add(appointmentTime);
                            }
                        }
                    }
                }

                return availableAppointments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(AppointmentUpdate appointment)
        {
            try
            {
                if (!Enum.IsDefined(typeof(AppointmentStatus), appointment.id_status))
                    throw new Exception("The status doesn't exist to be modified");

                AppointmentEntity oldAppointment = appointmentRepository.FindById(appointment.id);

                if (oldAppointment == null)
                    throw new Exception("The appointment doesn't exist to be modified");

                AppointmentStatusEntity statusEntity = statusRepository.FindById(appointment.id_status);

                if (statusEntity == null)
                    throw new Exception("Incosistence on AppointmentStatus");

                if (statusEntity.createNewAppointment && appointment.appointment == null)
                    throw new Exception("The new Infos are missing to create new Appointment");

                //Create the new appointment
                if (statusEntity.createNewAppointment)
                {
                    bool registerStates = Add(appointment.appointment);

                    if (!registerStates)
                        throw new Exception($"error on register new Appointment");
                }

                //Change the oldAppointment
                appointmentRepository.ChangeStatus(oldAppointment.id, statusEntity.id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ValidAppointment(AppointmentRegister appointment)
        {
            try
            {
                DateTime now = NormalizeDate.GetCurrentDateTime();

                if (appointment.appointmentTime != default(DateTime) && appointment.appointmentTime < now)
                    return false;

                List<DailyAppointmentEntity> dailyAppointments = dailyAppointmentRepository.FindDailyAppointmentByProfessionalId(appointment.id_professional);

                if (!dailyAppointments.Exists(dailyAppointment => appointment.appointmentTime.TimeOfDay == dailyAppointment.startTime))
                    return false;

                if (!CheckIfAppointmentAlreadyExists(appointment))
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CheckIfAppointmentAlreadyExists(AppointmentRegister appointment)
        {
            try
            {
                Dictionary<string, string> filters = new()
                {
                    {"id_professional", appointment.id_professional.ToString() },
                    {"appointmentTime", appointment.appointmentTime.ToString() },
                    {"id_status", AppointmentStatus.Active.ToString() }
                };

                return appointmentRepository.FindByCustomFilter(filters).Count == 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
