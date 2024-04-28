CREATE DATABASE PersonReferenceDB;
GO

USE PersonReferenceDB;
GO

create table dbo.Cities
(
    Id     int identity
        constraint PK_Cities
            primary key,
    Name   nvarchar(100) not null,
    NameEn nvarchar(100) not null
)
    go

create table dbo.Localizations
(
    ResourceKey    nvarchar(100) not null,
    LanguageCode   nvarchar(2)   not null,
    LocalizedValue nvarchar(100) not null
)
    go

create table dbo.Persons
(
    Id        int identity
        constraint PK_Persons
            primary key,
    Name      nvarchar(50)              not null,
    Surname   nvarchar(50)              not null,
    Gender    nvarchar(max)             not null,
    Pin       nvarchar(11)              not null,
    BirthDate date                      not null,
    Image     nvarchar(max) default N'' not null,
    CityId    int                       not null
        constraint FK_Persons_Cities_CityId
            references Cities
            on delete cascade,
    Status    nvarchar(20)              not null
)
    go

create table dbo.PersonRelationships
(
    Id               int identity
        constraint PK_PersonRelationships
            primary key,
    RelationshipType nvarchar(max) not null,
    PersonId         int           not null
        constraint FK_PersonRelationships_Persons_PersonId
            references Persons
            on delete cascade,
    RelatedPersonId  int           not null
        constraint FK_PersonRelationships_Persons_RelatedPersonId
            references Persons
)
    go

create index IX_PersonRelationships_PersonId
    on PersonRelationships (PersonId)
    go

create index IX_PersonRelationships_RelatedPersonId
    on PersonRelationships (RelatedPersonId)
    go

create index IX_Persons_CityId
    on Persons (CityId)
    go

create table dbo.PhoneNumbers
(
    Id       int identity
        constraint PK_PhoneNumbers
            primary key,
    Type     nvarchar(max) not null,
    Number   nvarchar(50)  not null,
    PersonId int
        constraint FK_PhoneNumbers_Persons_PersonId
            references Persons
)
    go

create index IX_PhoneNumbers_PersonId
    on PhoneNumbers (PersonId)
    go