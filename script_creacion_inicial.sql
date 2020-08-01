use AGUSTIN

/* Eliminación de los objetos preexistentes */

IF OBJECT_ID('dbo.Addresses','U') IS NOT NULL
	DROP TABLE dbo.Addresses;

IF OBJECT_ID('dbo.Phones','U') IS NOT NULL
	DROP TABLE dbo.Phones;

IF OBJECT_ID('dbo.RolesByUsers','U') IS NOT NULL
	DROP TABLE dbo.RolesByUsers;

IF OBJECT_ID('dbo.Users','U') IS NOT NULL
	DROP TABLE dbo.Users;

IF OBJECT_ID('dbo.PhoneTypes','U') IS NOT NULL
	DROP TABLE dbo.PhoneTypes;

IF OBJECT_ID('dbo.AddressTypes','U') IS NOT NULL
	DROP TABLE dbo.AddressTypes;

IF OBJECT_ID('dbo.Cities','U') IS NOT NULL
	DROP TABLE dbo.Cities;

IF OBJECT_ID('dbo.RolesByFuncionalities','U') IS NOT NULL
	DROP TABLE dbo.RolesByFuncionalities;

IF OBJECT_ID('dbo.Roles','U') IS NOT NULL
	DROP TABLE dbo.Roles;

IF OBJECT_ID('dbo.Funcionalities','U') IS NOT NULL
	DROP TABLE dbo.Funcionalities;

IF OBJECT_ID('dbo.SP_GetUsuarioByUserName', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_GetUsuarioByUserName;

IF OBJECT_ID('dbo.SP_InsertUser', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_InsertUser;
	
IF OBJECT_ID('dbo.SP_GetRolFuncionalities', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_GetRolFuncionalities;
	
IF OBJECT_ID('dbo.SP_GetFuncionalities', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_GetFuncionalities;
	
IF OBJECT_ID('dbo.SP_GetRoles', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_GetRoles;			
	
IF OBJECT_ID('dbo.SP_GetRolByDescription', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_GetRolByDescription;		
	
IF OBJECT_ID('dbo.SP_InsertUsuarioRol', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_InsertUsuarioRol;	
	
IF OBJECT_ID('dbo.SP_GetRolesByUser', 'P') IS NOT NULL
	DROP PROCEDURE dbo.SP_GetRolesByUser;			

-------------------- Creación de tablas ---------------------------

create table dbo.Users (
	user_id int not null,
	user_username varchar(50) not null,
	user_password varchar(255) not null,
	user_count_failed_attempts int default 0,
	user_mail varchar(50),
	user_active bit default 1
);

create table dbo.Phones (
	phon_id int not null,
	phon_user int not null,
	phon_number int not null,
	phon_type int not null
);

create table dbo.PhoneTypes (
	phty_id int not null,
	phty_description varchar(50) not null
);

create table dbo.Addresses (
	addr_id int not null,
	addr_street varchar(50) not null,
	addr_street_number int not null,
	addr_floor int default 0,
	addr_city int not null,
	addr_type int not null,
	addr_user int not null
);

create table dbo.AddressTypes (
	adty_id int not null,
	adty_description varchar(50) not null
);

create table dbo.Cities (
	city_id int not null,
	city_description varchar(50) not null,
	city_postal_code int not null
);

create table dbo.Roles (
	role_id int not null,
	role_description varchar(50) not null,
	role_active bit default 1
);

create table dbo.Funcionalities (
	func_id int not null,
	func_description varchar(50) not null
);

create table dbo.RolesByFuncionalities (
	func_id int not null,
	role_id int not null
);

create table dbo.RolesByUsers (
	user_id int not null,
	role_id int not null
);

-------------------- Creación de primary keys ---------------------------

ALTER TABLE dbo.Users 
ADD CONSTRAINT PK_Users PRIMARY KEY (user_id);

ALTER TABLE dbo.Phones 
ADD CONSTRAINT PK_Phones PRIMARY KEY (phon_id);

ALTER TABLE dbo.PhoneTypes 
ADD CONSTRAINT PK_PhoneTypes PRIMARY KEY (phty_id);

ALTER TABLE dbo.Addresses 
ADD CONSTRAINT PK_Addresses PRIMARY KEY (addr_id);

ALTER TABLE dbo.AddressTypes 
ADD CONSTRAINT PK_AddressTypes PRIMARY KEY (adty_id);

ALTER TABLE dbo.Cities 
ADD CONSTRAINT PK_Cities PRIMARY KEY (city_id);

ALTER TABLE dbo.Roles 
ADD CONSTRAINT PK_Roles PRIMARY KEY (role_id);

ALTER TABLE dbo.Funcionalities 
ADD CONSTRAINT PK_Funcionalities PRIMARY KEY (func_id);

ALTER TABLE dbo.RolesByFuncionalities 
ADD CONSTRAINT PK_RolesByFuncionalities PRIMARY KEY (role_id, func_id);

ALTER TABLE dbo.RolesByUsers 
ADD CONSTRAINT PK_RolesByUsers PRIMARY KEY (user_id, role_id);

-------------------- Creación de foreign keys ---------------------------

ALTER TABLE dbo.Phones ADD CONSTRAINT FK_Phones_Users 
FOREIGN KEY (phon_user) REFERENCES dbo.Users(user_id);

ALTER TABLE dbo.Phones ADD CONSTRAINT FK_Phones_PhoneTypes 
FOREIGN KEY (phon_type) REFERENCES dbo.PhoneTypes(phty_id);

ALTER TABLE dbo.Addresses ADD CONSTRAINT FK_Addresses_Cities
FOREIGN KEY (addr_city) REFERENCES dbo.Cities(city_id);

ALTER TABLE dbo.Addresses ADD CONSTRAINT FK_Addresses_Users 
FOREIGN KEY (addr_user) REFERENCES dbo.Users(user_id);

ALTER TABLE dbo.Addresses ADD CONSTRAINT FK_Addresses_AddressTypes 
FOREIGN KEY (addr_type) REFERENCES dbo.AddressTypes(adty_id);

ALTER TABLE dbo.RolesByFuncionalities ADD CONSTRAINT FK_RolesByFuncionalities_Funcionalities 
FOREIGN KEY (func_id) REFERENCES dbo.Funcionalities(func_id);

ALTER TABLE dbo.RolesByFuncionalities ADD CONSTRAINT FK_RolesByFuncionalities_Roles 
FOREIGN KEY (role_id) REFERENCES dbo.Roles(role_id);

ALTER TABLE dbo.RolesByUsers ADD CONSTRAINT FK_RolesByUsers_Users 
FOREIGN KEY (user_id) REFERENCES dbo.Users(user_id);

ALTER TABLE dbo.RolesByUsers ADD CONSTRAINT FK_RolesByUsers_Roles 
FOREIGN KEY (role_id) REFERENCES dbo.Roles(role_id);

go

-------------------- Creación de stored procedures ---------------------------

create procedure dbo.SP_GetUsuarioByUserName (@username varchar(50)) as
begin
	
	select user_id, user_active, user_count_failed_attempts, user_mail, user_password, user_username
	from dbo.Users
	where user_username = @username and user_active = 1

end
go

create procedure dbo.SP_InsertUser (@username varchar(50), @password varchar(50), @countFailedAttemps int, @isActive bit, @mail varchar(50), @Id int output) as
begin

	insert into dbo.Users values (
		(select isnull(max(user_id),0) + 1 from dbo.Users),
		@username,
		@password,
		@countFailedAttemps,
		@mail,
		@isActive		
	);

	set @Id = (select top 1 user_id from dbo.Users where user_username = @username)

end
go

create procedure dbo.SP_GetRolFuncionalities (@rolId int) as
begin

	select f.func_id, f.func_description
	from dbo.RolesByFuncionalities rf
	join dbo.Funcionalities f on f.func_id = rf.func_id
	join dbo.Roles r on r.role_id = rf.role_id
	where rf.role_id = @rolId and r.role_active = 1

end
go

create procedure dbo.SP_GetFuncionalities as
begin

	select func_id, func_description
	from dbo.Funcionalities

end
go

create procedure dbo.SP_GetRoles as
begin

	select role_id, role_description, role_active
	from dbo.Roles

end
go

create procedure dbo.SP_InsertUsuarioRol (@userId int, @rolId int) as
begin

	insert into dbo.RolesByUsers values (@userId, @rolId)

end
go

create procedure dbo.SP_GetRolByDescription (@description varchar(50)) as
begin

	select role_id, role_description, role_active
	from dbo.Roles
	where role_description = @description

end
go

create procedure dbo.SP_GetRolesByUser (@userId int) as
begin

	select r.role_id, r.role_description, r.role_active
	from dbo.RolesByUsers rbu
	join dbo.Roles r on rbu.role_id = r.role_id
	where user_id = @userId

end
go

---------------------Inserción de datos----------------------------

insert into dbo.Roles values
(1, 'ADMINISTRADOR', 1),
(2, 'CONSULTOR', 1),
(3, 'SUPERVISOR', 1);

insert into dbo.Funcionalities values
(1, 'USUARIO - ALTA'),
(2, 'USUARIO - BAJA'),
(3, 'USUARIO - MODIFICACIÓN'),
(4, 'ROL - ALTA'),
(5, 'ROL - BAJA'),
(6, 'ROL - MODIFICACIÓN');

insert into dbo.RolesByFuncionalities values
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(5, 1),
(6, 1),
(1, 3),
(2, 3),
(3, 3);