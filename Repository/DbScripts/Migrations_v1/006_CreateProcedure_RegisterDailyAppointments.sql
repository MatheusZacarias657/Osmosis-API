CREATE FUNCTION dbo.calcRegisterDailyAppointments(@startTime TIME, @appointmentPeriod TIME)
RETURNS TIME
AS
BEGIN
    DECLARE @period AS DATETIME
    SET @period = DATEADD(HOUR, DATEPART(hh,@appointmentPeriod), @startTime)
    SET @period = DATEADD(MINUTE, DATEPART(mi,@appointmentPeriod), @period)
    SET @period = DATEADD(SECOND, DATEPART(ss,@appointmentPeriod), @period)

    RETURN CAST(@period AS time)
END
go

