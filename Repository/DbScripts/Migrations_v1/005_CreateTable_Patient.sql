create table dbo.Patient
(
    Document  nvarchar(50)  not null,
    name      nvarchar(500) not null,
    birthday  date          not null,
    email     nvarchar(100) not null,
    telephone nvarchar(50)  not null,
    id        int identity
        primary key,
    active    bit default 1
)
go

