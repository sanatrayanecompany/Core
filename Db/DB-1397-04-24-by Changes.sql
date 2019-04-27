CREATE DATABASE MockTestDB COLLATE Persian_100_CI_AI;

GO

ALTER DATABASE MockTestDB SET MULTI_USER;


USE MockTestDB;

GO

/* Create schema -------------------- */

CREATE SCHEMA UserAccess;
GO
CREATE SCHEMA Test;
GO


/* Create tables -------------------- */

-- MenuType
CREATE TABLE [UserAccess].[tbMenuType]
(
    [Id] TINYINT PRIMARY KEY,
    [Title] NVARCHAR(100) NOT NULL
);

GO

-- Menu
CREATE TABLE [UserAccess].[tbMenu]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [ParentId] INT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbMenu] ([Id]),
    [TypeId] TINYINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbMenuType] ([Id]),
    [Name] VARCHAR(100) NOT NULL,
    [Title] NVARCHAR(100) NOT NULL,
    [Parameter] NVARCHAR(500) NULL,
    [Icon] VARCHAR(200) NULL,
    [Order] TINYINT NOT NULL,
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME
        DEFAULT GETDATE()
);

GO

-- Role
CREATE TABLE [UserAccess].[tbRole]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [Name] VARCHAR(100) NOT NULL,
    [Title] NVARCHAR(100) NOT NULL,
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME
        DEFAULT GETDATE()
);

GO

-- Permission
CREATE TABLE [UserAccess].[tbPermission]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [MenuId] INT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbMenu] ([Id]),
    [Name] VARCHAR(100) NOT NULL,
    [Title] NVARCHAR(100) NOT NULL,
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME
        DEFAULT GETDATE()
        UNIQUE
        (
            [MenuId],
            [Name]
        )
);

GO


--base table 
CREATE TABLE [Test].[tbProvinces]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [Code] INT,
    [Name] NVARCHAR(100)
);


--base table 
CREATE TABLE [Test].[tbCity]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [Code] INT,
    [Name] NVARCHAR(100),
    [ProvinceId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbProvinces] (Id)
);

--base table  
CREATE TABLE [Test].[tbBranch]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [CityId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbCity] (Id),
    [Code] INT,
    [Name] NVARCHAR(100),
    [Address] NVARCHAR(500),
    [Website] NVARCHAR(200),
    [Email] NVARCHAR(150),
    [Phone] CHAR(30),
    [Fax] CHAR(30),
);


-- User
CREATE TABLE [UserAccess].[tbUser]
(
    [Id] BIGINT IDENTITY PRIMARY KEY,
    [Username] VARCHAR(100) NOT NULL,
    [Password] VARBINARY(MAX) NOT NULL,
    [BranchId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbBranch] (Id),
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Mobile] CHAR(11) NULL,
    [Email] VARCHAR(100) NULL,
    [IsAdmin] BIT NOT NULL,
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME
        DEFAULT GETDATE()
        UNIQUE ([Username])
);

GO

-- UserRole
CREATE TABLE [UserAccess].[tbUserRole]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [RoleId] INT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbRole] ([Id]),
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME
        DEFAULT GETDATE()
        UNIQUE
        (
            [RoleId],
            [UserId]
        )
);

GO

-- RolePermission
CREATE TABLE [UserAccess].[tbRolePermission]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [RoleId] INT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbRole] ([Id]),
    [MenuId] INT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbMenu] ([Id]),
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME
        DEFAULT GETDATE()
        UNIQUE
        (
            [RoleId],
            [PermissionId]
        )
);


--table 1 متقاضیان
CREATE TABLE [Test].[tbApplicants]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [UserName] NVARCHAR(50),
    [Password] BINARY(64) NOT NULL,
    [NationalCode] CHAR(10) NOT NULL,
    [BranchId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbBranch] (Id),
    [IdNO] CHAR(10) NOT NULL,
    [Cellphone] CHAR(11) NULL,
    [Landline] CHAR(10) NULL,
    [LandlineCode] CHAR(10) NULL,
    [Address] NVARCHAR(300) NOT NULL,
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME
        DEFAULT GETDATE(),
    [ModifyDate] DATETIME
);



CREATE TABLE [Test].[tbApplicantTestRegistration]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [ApplicantId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbApplicants] (Id),
    [TestId] INT NOT NULL,
    [RegistrationDate] DATETIME
        DEFAULT GETDATE(),
    [ModifyDate] DATETIME,
    [Description] NVARCHAR(MAX)
);

--table 
CREATE TABLE [Test].[tbTestGroup]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
    [Code] INT,
    [Title] NVARCHAR(500) NULL,
    [Description] NVARCHAR(MAX)
);

--table 
CREATE TABLE [Test].[tbTest]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [TestGroupId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbTestGroup] (Id),
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
    [Code] INT NOT NULL,
    [Title] NVARCHAR(500) NULL,
    [Description] NVARCHAR(MAX)
);
--table 
CREATE TABLE [Test].[tbTestItem]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [TestId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbTest] (Id),
    [TestGroupId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbTestGroup] (Id),
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
    [BranchId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbBranch] (Id),
    [Code] INT NOT NULL,
    [Title] NVARCHAR(500) NULL,
    [HoldingDate] DATETIME NOT NULL, -- تاریخ برگذاری
    [Capacity] INT NOT NULL,         -- ظرفیت
    [EventPlace] NVARCHAR(MAX),      --محل برگزاری
    [Cost] DECIMAL(18) NOT NULL,     -- هزینه
    [HoldingTime] TIME(7) NOT NULL,
    [RegistrationDeadline] DATETIME,
    [DeadlineCancelling] DATETIME,
    [ModifyDate] DATETIME,
    [Description] NVARCHAR(MAX)
);

CREATE TABLE [Test].[tbApplicantTestRegistrationItems] -- آزمون متقاضیان ثبت نام شده
(
    [Id] INT IDENTITY PRIMARY KEY,
    [TestItemId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbTestItems] (Id),
    [StatusRegistrationId] INT NOT NULL,
    [RegistrationDate] DATETIME
        DEFAULT GETDATE(),
    [ModifyDate] DATETIME,
    [Description] NVARCHAR(MAX)
);


--table 
CREATE TABLE [Test].[tbRules]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
    [Title] NVARCHAR(250),
    [Text] NVARCHAR(MAX),
    [CreateDate] DATETIME
        DEFAULT GETDATE(),
    [ModifyDate] DATETIME,
    [Description] NVARCHAR(MAX)
);

CREATE TABLE [Test].[tbRuleAssignment]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [RuleId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbRules] (Id),
    [TestGroupId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbTestGroup] (Id),
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
);


CREATE TABLE [Test].[tbBank]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [Code] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX)
);

--table 11
--تکمیل نشده که با فیلدهای خروحی سرویس بانک باید تکمیل گردد 
CREATE TABLE [Test].[tbPaidBills]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [BankId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbBank] (Id),
    [TransactionId] NVARCHAR(500) NULL,
    [ApplicantTestRegistrationItemsId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbApplicantTestRegistrationItems] (Id),
    [Description] NVARCHAR(MAX)
);


--base table 
CREATE TABLE [Test].[tbStatusRegistration]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [Code] INT NOT NULL,
    [Title] NVARCHAR(500) NULL,
    [Description] NVARCHAR(MAX)
);


CREATE TABLE [Test].[tbReturnCashToApplicant]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
    [FromBankId] INT NOT NULL,                                                 --foreign key REFERENCES,
    [ToBankId] INT NOT NULL,                                                   --foreign key REFERENCES,
    [TransactionId] NVARCHAR(500) NULL,
    [ApplicantTestRegistrationItemsId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbApplicantTestRegistrationItems] (Id), --شناسه آزمون ثبت نام کننده
    [Description] NVARCHAR(MAX)
);

--base table 
CREATE TABLE [Test].[tbTestResultType]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [Code] INT NOT NULL,
    [Title] NVARCHAR(500) NULL,
    [Description] NVARCHAR(MAX)
);

--table 13
--تکمیل نشده
CREATE TABLE [Test].[tbTestResult]
(
    [Id] INT IDENTITY PRIMARY KEY,
    [UserId] BIGINT NOT NULL
        FOREIGN KEY REFERENCES [UserAccess].[tbUser] ([Id]),
    [ApplicantTestRegistrationItemsId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbApplicantTestRegistrationItems] (Id),
    [TestResultTypeId] INT NOT NULL
        FOREIGN KEY REFERENCES [Test].[tbTestResultType] (Id),
    [Description] NVARCHAR(MAX)
);


GO

CREATE PROCEDURE [UserAccess].[SP_User_Insert]
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
    (
        [Username],
        [Password],
        [BranchId],
        [FirstName],
        [LastName],
        [Mobile],
        [Email],
        [IsAdmin],
        [IsActive],
        [CreateDate]
    )
    VALUES
    (@Username, @Password, @BranchId, @Firstname, @Lastname, @Mobile, @Email, @IsAdmin, @IsActive, @CreateDate);

END;

GO

CREATE PROCEDURE [UserAccess].[SP_Authentication]
    @Username NVARCHAR(50),
    @Password VARBINARY(MAX)
AS
BEGIN

    SELECT ur.UserId,
           ur.RoleId
    FROM [UserAccess].[tbUser] u
        INNER JOIN [UserAccess].[tbUserRole] ur
            ON u.Id = ur.UserId
    WHERE u.Username = @Username
          AND u.[Password] = @Password; -- CAST(0 AS varbinary(MAX))

END;
GO
--------------------------------------


CREATE PROCEDURE [UserAccess].[SP_GetMenu] @UserId INT
AS
BEGIN

    SELECT m.[Id],
           m.[ParentId],
           m.[TypeId],
           m.[Name],
           m.[Title],
           m.[Parameter],
           m.[Icon],
           m.[Order],
           m.[IsActive]
    FROM [UserAccess].[tbUser] u
        INNER JOIN [UserAccess].[tbUserRole] ur
            ON u.Id = ur.UserId
        INNER JOIN [UserAccess].[tbRole] r
            ON r.Id = ur.RoleId
        INNER JOIN [UserAccess].[tbRolePermission] rp
            ON rp.RoleId = r.Id
        INNER JOIN [UserAccess].[tbMenu] m
            ON m.Id = rp.MenuId
    WHERE u.Id = @UserId;

END;



GO


CREATE PROCEDURE [Test].[SP_Branch_GetAll]
AS
BEGIN

    SELECT [CityId],
           [Code],
           [Name],
           [Address],
           [Website],
           [Email],
           [Phone],
           [Fax]
    FROM [Test].[tbBranch];

END;



GO


CREATE PROCEDURE [Test].[SP_Branch_GetAllView]
AS
BEGIN

    SELECT b.Id,
           b.[CityId],
           b.[Code],
           b.[Name] Name,
           b.[Address],
           b.[Website],
           b.[Email],
           b.[Phone],
           b.[Fax],
           c.[Name] CityName,
           p.[Name] ProvinceName,
           p.[Id] ProvinceId
    FROM [Test].[tbBranch] b
        INNER JOIN [Test].[tbCity] c
            ON b.CityId = c.Id
        INNER JOIN [Test].[tbProvinces] p
            ON c.ProvinceId = p.Id;

END;


GO

CREATE PROCEDURE [Test].[SP_Province_GetAll]
AS
BEGIN

    SELECT [Id],
           [Code],
           [Name]
    FROM [Test].[tbProvinces];

END;

GO

CREATE PROCEDURE [Test].[SP_City_GetAll]
AS
BEGIN

    SELECT [Id],
           [Code],
           [Name],
           [ProvinceId]
    FROM [Test].[tbCity];

END;


GO

CREATE PROCEDURE [Test].[SP_City_GetAllView]
AS
BEGIN

    SELECT c.[Id],
           c.[Code],
           c.[Name],
           c.[ProvinceId],
           p.[Name] ProvinceName
    FROM [Test].[tbCity] c
        INNER JOIN [Test].[tbProvinces] p
            ON c.ProvinceId = p.Id;

END;

---------Insert Regions----------------

GO
CREATE PROCEDURE [Test].[SP_Branch_Insert]
    @CityId INT,
    @Code INT,
    @Name NVARCHAR(100),
    @Address NVARCHAR(500),
    @Website NVARCHAR(200),
    @Email NVARCHAR(150),
    @Phone CHAR(30),
    @Fax CHAR(30)
AS
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM [Test].[tbBranch]
        WHERE [Name] = @Name
              AND [CityId] = @CityId
    )
        INSERT INTO [Test].[tbBranch]
        (
            [CityId],
            [Code],
            [Name],
            [Address],
            [Website],
            [Email],
            [Phone],
            [Fax]
        )
        OUTPUT INSERTED.Id
        VALUES
        (@CityId, @Code, @Name, @Address, @Website, @Email, @Phone, @Fax);
    ELSE
        RAISERROR('رکورد تکراری', 1, 1);

END;

GO
CREATE PROCEDURE [Test].[SP_Province_Insert]
    @Code INT = NULL,
    @Name NVARCHAR(100)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [Test].[tbProvinces] WHERE [Name] = @Name)
        INSERT INTO [Test].[tbProvinces]
        (
            [Code],
            [Name]
        )
        OUTPUT INSERTED.Id
        VALUES
        (   CASE
                WHEN @Code = 0 THEN
                    NULL
                ELSE
                    @Code
            END, @Name);
    ELSE
        RAISERROR('رکورد تکراری', 1, 1);

END;

GO
CREATE PROCEDURE [Test].[SP_City_Insert]
    @Code INT = NULL,
    @Name NVARCHAR(100),
    @ProvinceId INT
AS
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM [Test].[tbCity]
        WHERE [Name] = @Name
              AND [ProvinceId] = @ProvinceId
    )
        INSERT INTO [Test].[tbCity]
        (
            [Code],
            [Name],
            [ProvinceId]
        )
        OUTPUT INSERTED.Id
        VALUES
        (   CASE
                WHEN @Code = 0 THEN
                    NULL
                ELSE
                    @Code
            END, @Name, @ProvinceId);
    ELSE
        RAISERROR('رکورد تکراری', 1, 1);

END;



GO
CREATE PROCEDURE [Test].[SP_TestGroup_Insert]
    @UserId INT,
    @Code INT = NULL,
    @Title NVARCHAR(100),
    @ParentId INT,
    @Description NVARCHAR(MAX)
AS
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM [Test].[tbTestItem]
        WHERE [TestGroupId] = @ParentId
    )
        IF NOT EXISTS
        (
            SELECT 1
            FROM [Test].[tbTestGroup]
            WHERE [Title] = @Title
                  AND [ParentId] = @ParentId
        )
            INSERT INTO [Test].[tbTestGroup]
            (
                [UserId],
                [ParentId],
                [Code],
                [Title],
                [Description]
            )
            OUTPUT INSERTED.Id
            VALUES
            (   @UserId, @ParentId, CASE
                                        WHEN @Code = 0 THEN
                                            NULL
                                        ELSE
                                            @Code
                                    END, @Title, @Description);
        ELSE
            RAISERROR('رکورد تکراری', 1, 1);
    ELSE
        RAISERROR('امکان ثبت وجود ندارد بدلیل اینکه سرگروه انتخابی دارای زیرمجموعه آزمون می باشد.', 1, 1);

END;

GO

CREATE PROCEDURE [Test].[SP_TestGroup_GetAllView]
AS
BEGIN

    SELECT c.[Id],
           c.[UserId],
           c.[ParentId],
           c.[Code],
           c.[Title],
           c.[Description],
           ISNULL(p.[Title], N'گروه اصلی') ParentTitle,
           u.FirstName + ' ' + u.LastName UserFullName
    FROM [Test].[tbTestGroup] c
        LEFT JOIN [Test].[tbTestGroup] p
            ON c.ParentId = p.Id
        INNER JOIN [UserAccess].[tbUser] u
            ON c.UserId = u.Id;

END;

GO

CREATE PROCEDURE [Test].[SP_TestGroup_GetWithoutSubset]
AS
BEGIN

    SELECT [Id],
           [Title]
    FROM [Test].[tbTestGroup]
    WHERE Id NOT IN (
                        SELECT ISNULL([ParentId], 0) FROM [Test].[tbTestGroup]
                    );
END;


GO

CREATE PROCEDURE [Test].[SP_TestItem_GetAllView]
AS
BEGIN

    SELECT ti.[Id],
           ti.[TestGroupId],
           tg.Title [TestGroupTitle],
           ti.[BranchId],
           b.[Name] [BranchName],
           ti.[UserId],
           ti.[Code],
           ti.[Title],
           ti.[HoldingDate],
           CONVERT(VARCHAR(5), ti.[HoldingTime], 108) HoldingTime,
           ti.[Capacity],
           ti.[EventPlace],
           ti.[Cost],
           ti.[RegistrationDeadline],
           ti.[DeadlineCancelling],
           ti.[ModifyDate],
           ti.[Description]
    FROM [Test].[tbTestItem] ti
        INNER JOIN [Test].[tbTestGroup] tg
            ON tg.[Id] = ti.[TestGroupId]
        INNER JOIN [Test].[tbBranch] b
            ON b.[Id] = ti.[BranchId];


END;

GO

CREATE PROCEDURE [Test].[SP_InsertTestItem]
    @TestGroupId INT,
    @BranchId INT,
    @UserId INT,
    @Code INT = NULL,
    @Title NVARCHAR(500),
    @HoldingDate DATE,
    @HoldingTime TIME(7),
    @Capacity INT,
    @EventPlace NVARCHAR(MAX),
    @Cost DECIMAL(18, 0),
    @RegistrationDeadline DATE,
    @DeadlineCancelling DATE,
    @Description NVARCHAR(MAX)
AS
BEGIN

    IF EXISTS
    (
        SELECT 1
        FROM [Test].[tbTestGroup]
        WHERE Id NOT IN (
                            SELECT ISNULL([ParentId], 0) FROM [Test].[tbTestGroup]
                        )
              AND Id = @TestGroupId
    )
        INSERT INTO [Test].[tbTestItem]
        (
            [TestGroupId],
            [BranchId],
            [UserId],
            [Code],
            [Title],
            [HoldingDate],
            [Capacity],
            [EventPlace],
            [Cost],
            [HoldingTime],
            [RegistrationDeadline],
            [DeadlineCancelling],
            [Description]
        )
        OUTPUT INSERTED.Id
        VALUES
        (   @TestGroupId, @BranchId, @UserId, CASE
                                                  WHEN @Code = 0 THEN
                                                      NULL
                                                  ELSE
                                                      @Code
                                              END, @Title, @HoldingDate, @Capacity, @EventPlace, @Cost,
            CONVERT(VARCHAR(5), @HoldingTime, 108), @RegistrationDeadline, @DeadlineCancelling, @Description);

    ELSE
        RAISERROR('رکورد تکراری', 1, 1);
END;




GO

CREATE PROCEDURE [Test].[SP_Rule_Insert]
    @UserId INT ,
    @Title NVARCHAR(250),
    @Text NVARCHAR(MAX)
AS
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM [Test].[tbRules]
        WHERE [Title] = @Title
    )

        INSERT INTO [Test].[tbRules]
        (          
            [UserId],
            [Title],
            [Text],
			[CreateDate]
        )
		OUTPUT INSERTED.Id
        VALUES
        (   
			@UserId,
			@Title, 
			@Text,
			GETDATE() 
        )
        
    ELSE
        RAISERROR('رکورد تکراری', 1, 1);

END;


CREATE PROCEDURE [Test].[SP_Rule_GetAll]
AS
BEGIN

    SELECT  [UserId],
            [Title],
            [Text],
			[CreateDate],
			[ModifyDate]
    FROM [Test].[tbRule]

END;








-----------

CREATE PROCEDURE [Test].[SP_TestItem_GetAllView]
AS
BEGIN

    SELECT ti.[Id],
           ti.[TestGroupId],
           tg.Title [TestGroupTitle],
           ti.[BranchId],
           b.[Name] [BranchName],
           ti.[UserId],
           ti.[Code],
           ti.[Title],
           ti.[HoldingDate],
           CONVERT(VARCHAR(5), ti.[HoldingTime], 108) HoldingTime,
           ti.[Capacity],
           ti.[EventPlace],
           ti.[Cost],
           ti.[RegistrationDeadline],
           ti.[DeadlineCancelling],
           ti.[ModifyDate],
           ti.[Description]
    FROM [Test].[tbTestItem] ti
        INNER JOIN [Test].[tbTestGroup] tg
            ON tg.[Id] = ti.[TestGroupId]
        INNER JOIN [Test].[tbBranch] b
            ON b.[Id] = ti.[BranchId];


END;






GO
CREATE PROCEDURE [Test].[SP_Rule_Insert]
    @UserId INT ,
    @Name NVARCHAR(100),
    @ProvinceId INT
AS
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM [Test].[tbRules]
        WHERE [Name] = @Name
    AND [ProvinceId] = @ProvinceId
    )

        INSERT INTO [Test].[tbRules]
        (          
            [UserId],
            [Title],
            [CreateDate],
            [ModifyDate],
            [Text],
			[Description]
        )
		OUTPUT INSERTED.Id
        VALUES
        (   0,         -- UserId - bigint
            N'',       -- Text - nvarchar(max)
            GETDATE(), -- CreateDate - datetime
            GETDATE(), -- ModifyDate - datetime
            N''        -- Description - nvarchar(max)
            )
        
    ELSE
        RAISERROR('رکورد تکراری', 1, 1);

END;




--exec [Test].[SP_TestGroup_GetAllView]

--select * from useraccess.tbuser
---------------end insert regions--------------------------------------



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

SELECT *
FROM [UserAccess].[tbMenuType];
SELECT *
FROM [UserAccess].[tbMenu];
SELECT *
FROM [UserAccess].[tbRole];
SELECT *
FROM [UserAccess].[tbRolePermission];
SELECT *
FROM [UserAccess].[tbPermission];
SELECT *
FROM [UserAccess].[tbUser];
SELECT *
FROM [UserAccess].[tbUserRole];


INSERT INTO [UserAccess].[tbPermission]
VALUES
(1, 'Security', N'امنیت', 1, GETDATE());
--insert into [UserAccess].[tbPermission] values(	1		,'Security'	,N'امنیت	,	1,GETDATE())
INSERT INTO [UserAccess].[tbPermission]
VALUES
(2, 'UserAccessMenu', N'مدیریت دسترسی ها', 1, GETDATE());

INSERT INTO [UserAccess].[tbPermission]
VALUES
(3, 'ChangePassword', N'تغییر کلمه عبور', 1, GETDATE());

--insert into [UserAccess].[tbPermission] values(	4		,'BaseInformation'	,N'اطلاعات پایه'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	5		,'Rules'	,N'تعریف قوانین'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	6		,'Rules'	,N'قوانین'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	7		,'TestGroup'	,N'گروه آزمون'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	8		,'Test'	,N'آزمون'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	9		,'City'	,N'شهر'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	10		,'Province'	,N'استان'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	11		,'Branch'	,N'واحدها'	,	1,GETDATE())
--insert into [UserAccess].[tbPermission] values(	12		,'Users'	,N'کاربران'	,	1,GETDATE())



INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 1, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 2, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 3, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 4, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 5, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 6, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 7, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 8, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 9, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 10, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 11, 1, GETDATE());
INSERT INTO [UserAccess].[tbRolePermission]
VALUES
(1, 12, 1, GETDATE());



