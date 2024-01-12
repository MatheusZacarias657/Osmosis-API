create table dbo.Customer
(
    id       int identity
        primary key,
    name     nvarchar(500)  not null,
    login    nvarchar(500)  not null,
    password nvarchar(1000) not null,
    email    nvarchar(100)  not null,
    id_role  int            not null
        constraint id_role_Users___fk
            references dbo.CustomerRole,
    active   bit default 1  not null
)
go