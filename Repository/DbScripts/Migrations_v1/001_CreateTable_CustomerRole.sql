create table dbo.CustomerRole
(
    name nvarchar(500) not null,
    id   int not null
)
go

INSERT INTO Osmosis.dbo.CustomerRole (name, id) VALUES (N'Master', 1);
INSERT INTO Osmosis.dbo.CustomerRole (name, id) VALUES (N'Admin', 2);
INSERT INTO Osmosis.dbo.CustomerRole (name, id) VALUES (N'User', 3);

ALTER TABLE dbo.CustomerRole
ADD CONSTRAINT PK_CustomerRole_id PRIMARY KEY (id);