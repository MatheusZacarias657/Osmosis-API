use Osmosis
go

create table dbo.DailyAppointment
(
    startTime       time not null,
    id              int identity
        primary key,
    id_professional int  not null
        constraint id_professional_dailyService___fk
            references dbo.Professional
)
go

