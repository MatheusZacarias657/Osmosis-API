create table dbo.ActiveGuid
(
    guid           nvarchar(500) not null,
    id             int identity
        primary key,
    creationDate   datetime      not null,
    expirationDate datetime,
    id_customer    int           not null
        constraint ActiveGuid_Customer_id_fk
            references dbo.Customer,
    browser        nvarchar(50)  not null
)
go

CREATE TRIGGER [updateExpirationDate]
ON [ActiveGuid]
WITH EXECUTE AS CALLER
AFTER INSERT
AS
BEGIN
  DECLARE @validade AS int
  SET @validade = 7

  UPDATE ActiveGuid
  SET ActiveGuid.expirationDate = DATEADD(DAY, @validade, INSERTED.[creationDate])
  FROM INSERTED
END
go

