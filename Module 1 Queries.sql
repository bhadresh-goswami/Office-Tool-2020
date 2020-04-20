
--module 1 
--tables
Create table LocationMaster
(
	LocationId int primary key identity,
	LocationName varchar(100) Unique not null
)

Create table TechMaster
(
	TechId int primary key identity,
	TechName varchar(100) not null unique
)

create table ExpertMaster
(
	ExpertId int primary key identity,
	ExpertName varchar(100) not null unique,
	ExpertPassword varchar(20) not null,
	ExpertEmailid varchar(200) not null unique,
	ExpertMobile varchar(12) not null unique,
	Designation varchar(50) not null,
	RefLocationId int foreign key references LocationMaster(LocationId)
)

Create table TechExpertMaster
(
	TechExpertId int primary key identity,
	RefTechId int not null references TechMaster(TechId),
	RefExpertId int not null references ExpertMaster(ExpertId),
	TechExpertIsEnabled bit not null
)