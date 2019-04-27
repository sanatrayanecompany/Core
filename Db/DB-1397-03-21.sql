create database MockTestDB collate Persian_100_CI_AI

GO

ALTER DATABASE MockTestDB
SET MULTI_USER;


use MockTestDB

go

/* Create schema -------------------- */

create schema UserAccess
go
create schema Test
go


/* Create tables -------------------- */

-- MenuType
create table [UserAccess].[tbMenuType]
(
	[Id] tinyint primary key,
	[Title] nvarchar(100) not null
)

go

-- Menu
create table [UserAccess].[tbMenu]
(
	[Id] int identity primary key,
	[ParentId] int null foreign key references [UserAccess].[tbMenu]([Id]),
	[TypeId] tinyint not null foreign key references [UserAccess].[tbMenuType]([Id]),
	[Name] varchar(100) not null,
	[Title] nvarchar(100) not null,
	[Parameter] nvarchar(500) null,
	[Icon] varchar(200) null,
	[Order] tinyint not null,
	[IsActive] bit not null,
	[CreateDate] datetime default getdate()
)

go

-- Role
create table [UserAccess].[tbRole]
(
	[Id] int identity primary key,
	[Name] varchar(100) not null,
	[Title] nvarchar(100) not null,
	[IsActive] bit not null,
	[CreateDate] datetime default getdate()
)

go

-- Permission
create table [UserAccess].[tbPermission]
(
	[Id] int identity primary key,
	[MenuId] int not null foreign key references [UserAccess].[tbMenu]([Id]),
	[Name] varchar(100) not null,
	[Title] nvarchar(100) not null,
	[IsActive] bit not null,
	[CreateDate] datetime default getdate()
	unique([MenuId], [Name])
)

go


--base table 
create table [Test].[tbProvinces]
(
	[Id] int identity primary key,
	[Code] INT,
	[Name] nvarchar(100)
)


--base table 
create table [Test].[tbCity]
(
	[Id] int identity primary key,
	[Code] INT,
	[Name] nvarchar(100),
	[ProvinceId] INT NOT NULL FOREIGN KEY REFERENCES [Test].[tbProvinces](Id)
)

--base table  
create table [Test].[tbBranch]
(
	[Id] int identity primary key,
	[CityId] INT NOT NULL FOREIGN KEY REFERENCES [Test].[tbCity](Id),
	[Code] INT,
	[Name] nvarchar(100),
	[Address] NVARCHAR(500),
	[Website] NVARCHAR(200),
	[Email] NVARCHAR(150),
	[Phone] CHAR(30),
	[Fax] CHAR(30),
)


-- User
create table [UserAccess].[tbUser]
(
	[Id] bigint identity primary key,
	[Username] varchar(100) not null,
	[Password] varbinary(max) not null,
	[BranchId] INT NOT NULL FOREIGN KEY REFERENCES  [Test].[tbBranch](Id),
	[FirstName] nvarchar(100) not null,
	[LastName] nvarchar(100) not null,
	[Mobile] char(11) null,
	[Email] varchar(100) null,
	[IsAdmin] bit not null,
	[IsActive] bit not null,
	[CreateDate] datetime default getdate()
	unique([Username])
)

go

-- UserRole
create table [UserAccess].[tbUserRole]
(
	[Id] int identity primary key,
	[RoleId] int not null foreign key references [UserAccess].[tbRole]([Id]),
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
	[IsActive] bit not null,
	[CreateDate] datetime default getdate()
	unique([RoleId], [UserId])
)

go

-- RolePermission
create table [UserAccess].[tbRolePermission]
(
	[Id] int identity primary key,
	[RoleId] int not null foreign key references [UserAccess].[tbRole]([Id]),
	[MenuId] int not null foreign key references [UserAccess].[tbMenu]([Id]),
	[IsActive] bit not null,
	[CreateDate] datetime default getdate()
	unique([RoleId], [PermissionId])
)


--table 1 متقاضیان
create table [Test].[tbApplicants]
(
	[Id] int identity primary key,
	[UserName] nvarchar(50) ,
	[Password] binary(64) not null,
	[NationalCode] char(10) not null,
	[BranchId] INT NOT NULL FOREIGN KEY REFERENCES  [Test].[tbBranch](Id),
	[IdNO] char(10) not null,
	[Cellphone] char(11) null,
	[Landline] char(10) null,
	[LandlineCode] char(10) null,
	[Address] nvarchar(300) not null,
	[IsActive] bit not null,
	[CreateDate] datetime default getdate(),
	[ModifyDate] datetime 
)



create table [Test].[tbApplicantTestRegistration]
(
	[Id] int identity primary key,
	[ApplicantId] int not null foreign key REFERENCES [Test].[tbApplicants](Id),
	[TestId] int not null,
	[RegistrationDate] datetime default getdate(),
	[ModifyDate] datetime,
	[Description] nvarchar(max)
)

--table 
create table [Test].[tbTestGroup]
(
	[Id] int identity primary key,
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
	[Code] int not null,
	[Title] nvarchar(500) null,
	[Description] nvarchar(max)
)

--table 
create table [Test].[tbTest]
(
	[Id] int identity primary key,
	[TestGroupId] int not null foreign key REFERENCES [Test].[tbTestGroup](Id),
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
	[Code] int not null,
	[Title] nvarchar(500) null ,
	[Description] nvarchar(max)
)
--table 
create table [Test].[tbTestItems]
(
	[Id] int identity primary key,
	[TestId] int not null foreign key REFERENCES [Test].[tbTest](Id),
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
	[Code] int not null,
	[Title] nvarchar(500) null ,
	[HoldingDate] datetime not null, -- تاریخ برگذاری
	[Capacity] int not null, -- ظرفیت
	[EventPlace] nvarchar(max),--محل برگزاری
	[Cost] decimal(18) not NULL, -- هزینه
	[SeveralNo] int not null,
	[RegistrationDeadline] datetime,
	[DeadlineCancelling] datetime,
	[ModifyDate] datetime,
	[Description] nvarchar(max)
)

create table [Test].[tbApplicantTestRegistrationItems] -- آزمون متقاضیان ثبت نام شده
(
	[Id] int identity primary key,
	[TestItemId]INT not null foreign key REFERENCES [Test].[tbTestItems](Id) ,
	[StatusRegistrationId] int not null,
	[RegistrationDate] datetime default getdate(),
	[ModifyDate] datetime,
	[Description] nvarchar(max)
)


--table 
create table [Test].[tbRules]
(
	[Id] int identity primary key,
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
	[Text] nvarchar(max),
	[CreateDate] datetime default getdate(),
	[ModifyDate] datetime,
	[Description] nvarchar(max)
)

CREATE TABLE [Test].[tbRuleAssign]
(
	[Id] int identity primary key,
	[RuleId] INT NOT NULL FOREIGN KEY REFERENCES [Test].[tbRules](Id),
	[BranchId] INT NOT NULL FOREIGN KEY REFERENCES  [Test].[tbBranch](Id),
	[TestGroupId] INT NOT NULL FOREIGN KEY REFERENCES [Test].[tbTestGroup](Id),
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
)


create table [Test].[tbBank]
(
	[Id] int identity primary key,
	[Code] int not null,
	[Name] NVARCHAR(100) not null,
	[Description] nvarchar(max)
)

--table 11
--تکمیل نشده که با فیلدهای خروحی سرویس بانک باید تکمیل گردد 
create table [Test].[tbPaidBills]
(
	[Id] int identity primary key,
	[BankId] int not null foreign key REFERENCES [Test].[tbBank](Id),
	[TransactionId] nvarchar(500) null ,
	[ApplicantTestRegistrationItemsId] int not null foreign key REFERENCES [Test].[tbApplicantTestRegistrationItems](Id),
	[Description] nvarchar(max)
)


--base table 
create table [Test].[tbStatusRegistration]
(
	[Id] int identity primary key,
	[Code] int not null,
	[Title] nvarchar(500) null ,
	[Description] nvarchar(max)
)


create table [Test].[tbReturnCashToApplicant]
(
	[Id] int identity primary key,
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
	[FromBankId] int not NULL, --foreign key REFERENCES,
    [ToBankId] int not NULL, --foreign key REFERENCES,
	[TransactionId] nvarchar(500) null ,
	[ApplicantTestRegistrationItemsId] int not null foreign key REFERENCES [Test].[tbApplicantTestRegistrationItems](Id), --شناسه آزمون ثبت نام کننده
	[Description] nvarchar(max)
)

--base table 
create table [Test].[tbTestResultType]
(
	[Id] int identity primary key,
	[Code] int not null,
	[Title] nvarchar(500) null ,
	[Description] nvarchar(max)
)

--table 13
--تکمیل نشده
create table [Test].[tbTestResult]
(
	[Id] int identity primary key,
	[UserId] bigint not null foreign key references [UserAccess].[tbUser]([Id]),
	[ApplicantTestRegistrationItemsId] int not null foreign key REFERENCES [Test].[tbApplicantTestRegistrationItems](Id),
	[TestResultTypeId] int NOT NULL FOREIGN KEY REFERENCES [Test].[tbTestResultType](Id),
	[Description] nvarchar(max)
)


GO

CREATE PROCEDURE [Test].[SP_User_Insert]
    @Username NVARCHAR(50),
    @Password VARBINARY(MAX),
    @Firstname NVARCHAR(50),
    @Lastname NVARCHAR(50),
    @IsAdmin BIT,
    @Mobile CHAR(20),
    @IsActive BIT,
    @Email NVARCHAR(50),
    @CreateDate DATETIME,
    @BranchId INT
AS
BEGIN

    INSERT INTO [UserAccess].[tbUser]
           ([Username]
           ,[Password]
           ,[BranchId]
           ,[FirstName]
           ,[LastName]
           ,[Mobile]
           ,[Email]
           ,[IsAdmin]
           ,[IsActive]
           ,[CreateDate])
     VALUES
           (@Username
           ,@Password
           ,@BranchId
           ,@FirstName
           ,@LastName
           ,@Mobile
           ,@Email
           ,@IsAdmin
           ,@IsActive
           ,@CreateDate)

END;


CREATE PROCEDURE [UserAccess].[SP_Authentication]
    @Username NVARCHAR(50),
    @Password VARBINARY(MAX)
AS
BEGIN

    select ur.UserId,ur.RoleId from [UserAccess].[tbUser] u
	inner join [UserAccess].[tbUserRole] ur on u.id = ur.UserId

END;

GO

------ Menu
----execute [UserAccess].[Menu_Insert] null, 1, 'UserAccessTab', N'مدیریت دسترسی ها', null, null, 1, 1

----execute [UserAccess].[Menu_Insert] 1, 2, 'UserAccessTab_MenuGroup', N'مدیریت منوها', null, null, 1, 1
----execute [UserAccess].[Menu_Insert] 2, 3, 'UserAccessTab_MenuGroup_Menu', N'تعریف منو', null, null, 1, 1
----execute [UserAccess].[Menu_Insert] 2, 3, 'UserAccessTab_MenuGroup_Permission', N'تعریف دسترسی', null, null, 2, 1

----execute [UserAccess].[Menu_Insert] 1, 2, 'UserAccessTab_RoleGroup', N'مدیریت نقش ها', null, null, 2, 1
----execute [UserAccess].[Menu_Insert] 5, 3, 'UserAccessTab_Role', N'تعریف نقش', null, null, 1, 1
----execute [UserAccess].[Menu_Insert] 5, 3, 'UserAccessTab_RolePermission', N'تعریف دسترسی', null, null, 2, 1

----execute [UserAccess].[Menu_Insert] 1, 2, 'UserAccessTab_UserGroup', N'مدیریت کاربران', null, null, 3, 1
----execute [UserAccess].[Menu_Insert] 8, 3, 'UserAccessTab_UserGroup_User', N'تعریف کاربران', null, null, 1, 1
----execute [UserAccess].[Menu_Insert] 8, 3, 'UserAccessTab_UserRole', N'تعریف دسترسی', null, null, 2, 1


----go

-- Role


--INSERT INTO  [UserAccess].[tbMenuType] VALUES(1, N'منو')
--INSERT INTO  [UserAccess].[tbMenuType] VALUES(2, N'زیر منو')

--INSERT INTO  [UserAccess].[tbMenu] VALUES( NULL,1, 'Security', N'امنیت', null, null,1, 1, GETDATE())
----UPDATE UserAccess.tbMenu SET [Name]='Security' , Title = N'امنیت' WHERE id =1
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 1,2, 'UserAccessMenu', N'مدیریت دسترسی ها', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 1,2, 'ChangePassword', N'تغییر کلمه عبور', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 1,2, 'Users', N'کاربران', null, null,1, 1, GETDATE())

--INSERT INTO  [UserAccess].[tbMenu] VALUES( NULL,1, 'BaseInformation', N'اطلاعات پایه', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 4,2, 'Rules', N'قوانین', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 4,2, 'TestGroup', N'گروه آزمون', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 4,2, 'Test', N'آزمون', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 4,2, 'City', N'شهر', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 4,2, 'Province', N'استان', null, null,1, 1, GETDATE())
--INSERT INTO  [UserAccess].[tbMenu] VALUES( 4,2, 'Branch', N'واحدها', null, null,1, 1, GETDATE())


--INSERT INTO   [UserAccess].[tbRole] VALUES('Admin', N'مدیریت سیستم',1, GETDATE())
--INSERT INTO   [UserAccess].[tbRole] VALUES('User Level1', N'کاربران سطح1',1, GETDATE())
--insert into [MockTestDB].[UserAccess].[tbUserRole] values(
--  1,
--  1,
--  1,
--  getdate()
--  )

SELECT * FROM  [UserAccess].[tbMenuType]
SELECT * FROM  [UserAccess].[tbMenu]
SELECT * FROM  [UserAccess].[tbRole]
SELECT * FROM  [UserAccess].[tbRolePermission]
SELECT * FROM  [UserAccess].[tbPermission]
SELECT * FROM  [UserAccess].[tbUser]
SELECT * FROM  [UserAccess].[tbUserRole]


insert into [UserAccess].[tbPermission] values(1,'Security',N'امنیت',1,getdate())
--insert into [UserAccess].[tbPermission] values(	1		,'Security'	,N'امنیت	,	1,GETDATE())
insert into [UserAccess].[tbPermission] values(	2,'UserAccessMenu',N'مدیریت دسترسی ها'	,	1,GETDATE())

insert into [UserAccess].[tbPermission] 
values(	3,'ChangePassword'	,N'تغییر کلمه عبور'	,	1,GETDATE())

--insert into [UserAccess].[tbPermission] values(	4		,'BaseInformation'	,N'اطلاعات پایه'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	5		,'Rules'	,N'تعریف قوانین'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	6		,'Rules'	,N'قوانین'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	7		,'TestGroup'	,N'گروه آزمون'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	8		,'Test'	,N'آزمون'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	9		,'City'	,N'شهر'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	10		,'Province'	,N'استان'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	11		,'Branch'	,N'واحدها'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	12		,'Users'	,N'کاربران'	,	1,GETDATE())



insert into [UserAccess].[tbRolePermission] values(1,1,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,2,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,3,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,4,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,5,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,6,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,7,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,8,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,9,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,10,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,11,1,getdate())
insert into [UserAccess].[tbRolePermission] values(1,12,1,getdate())



