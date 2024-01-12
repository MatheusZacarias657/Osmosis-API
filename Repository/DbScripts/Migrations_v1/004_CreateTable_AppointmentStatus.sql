create table dbo.AppointmentStatus
(
    id                   int not null,
    name                 nvarchar(500) not null,
    createNewAppointment bit default 0 not null
)
go

INSERT INTO Osmosis.dbo.AppointmentStatus (name, id, createNewAppointment) VALUES (N'Active', 1, 0);
INSERT INTO Osmosis.dbo.AppointmentStatus (name, id, createNewAppointment) VALUES (N'Finished', 2, 0);
INSERT INTO Osmosis.dbo.AppointmentStatus (name, id, createNewAppointment) VALUES (N'Remarked', 3, 1);
INSERT INTO Osmosis.dbo.AppointmentStatus (name, id, createNewAppointment) VALUES (N'Canceled', 4, 0);
INSERT INTO Osmosis.dbo.AppointmentStatus (name, id, createNewAppointment) VALUES (N'NotAttend', 5, 0);

ALTER TABLE dbo.AppointmentStatus
ADD CONSTRAINT PK_AppointmentStatus_id PRIMARY KEY (id);