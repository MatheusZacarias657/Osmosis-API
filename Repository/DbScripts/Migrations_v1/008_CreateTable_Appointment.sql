create table dbo.Appointment
(
    id              int identity
        primary key,
    appointmentTime datetime not null,
    id_doctor       int      not null
        constraint id_doctor_appointments___fk
            references dbo.Professional,
    id_status       int      not null
        constraint id_status_Appointments___fk
            references dbo.AppointmentStatus,
    id_patient      int      not null
        constraint id_patient_Appointment___fk
            references dbo.Patient
)
go

