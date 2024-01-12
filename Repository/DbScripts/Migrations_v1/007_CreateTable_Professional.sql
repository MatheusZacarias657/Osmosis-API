create table dbo.Professional
(
    name          nvarchar(500) not null,
    specialty     nvarchar(500) not null,
    entryTime     time          not null,
    departureTime time          not null,
    active        bit default 1 not null,
    id            int identity
        primary key,
    servicePeriod time          not null,
    license       nvarchar(500) not null
)
go

CREATE TRIGGER [updateDailyServices]
ON [Professional]
WITH EXECUTE AS CALLER
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @status AS bit
    SELECT @status = active FROM INSERTED
    DECLARE @id_professional AS INT
    SELECT @id_professional = id FROM INSERTED

    IF @status = 0
    BEGIN
        DELETE FROM DailyService WHERE id_professional = @id_professional
    END
    ELSE
    BEGIN
        DECLARE @period AS TIME
        DECLARE @servicePeriod AS TIME
        SELECT @servicePeriod = servicePeriod FROM INSERTED
        DECLARE @departureTime AS TIME
        SELECT @departureTime = departureTime FROM INSERTED

        DECLARE @appointmentsCount AS INT
        SELECT @appointmentsCount = COUNT(DailyService.id)
        FROM DailyService
            INNER JOIN INSERTED ON INSERTED.id = DailyService.id_professional

        IF @appointmentsCount = 0
        BEGIN
            SELECT @period = entryTime FROM INSERTED
            WHILE @departureTime > dbo.calcRegisterDailyAppointments (@servicePeriod, @period)
            BEGIN
                INSERT INTO DailyService (startTime, id_professional) VALUES (@period, @id_professional)
                SET @period = dbo.calcRegisterDailyAppointments (@servicePeriod, @period)
            END
        END
        ELSE
        BEGIN
            DELETE FROM DailyService WHERE id_professional = @id_professional
            SELECT @period = entryTime FROM INSERTED
            WHILE @departureTime > dbo.calcRegisterDailyAppointments (@servicePeriod, @period)
            BEGIN
                INSERT INTO DailyService (startTime, id_professional) VALUES (@period, @id_professional)
                SET @period = dbo.calcRegisterDailyAppointments (@servicePeriod, @period)
            END
        END
    END
END
go

