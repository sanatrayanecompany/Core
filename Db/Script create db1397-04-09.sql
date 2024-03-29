USE [master]
GO
/****** Object:  Database [MockTestDB]    Script Date: 04/04/1397 08:55:02 ق.ظ ******/
CREATE DATABASE [MockTestDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MockTestDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\MockTestDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MockTestDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\MockTestDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MockTestDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MockTestDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MockTestDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MockTestDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MockTestDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MockTestDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MockTestDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MockTestDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MockTestDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MockTestDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MockTestDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MockTestDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MockTestDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MockTestDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MockTestDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MockTestDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MockTestDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MockTestDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MockTestDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MockTestDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MockTestDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MockTestDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MockTestDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MockTestDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MockTestDB] SET RECOVERY FULL 
GO
ALTER DATABASE [MockTestDB] SET  MULTI_USER 
GO
ALTER DATABASE [MockTestDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MockTestDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MockTestDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MockTestDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MockTestDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MockTestDB', N'ON'
GO
ALTER DATABASE [MockTestDB] SET QUERY_STORE = OFF
GO
USE [MockTestDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [MockTestDB]
GO
/****** Object:  Schema [Test]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
CREATE SCHEMA [Test]
GO
/****** Object:  Schema [UserAccess]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
CREATE SCHEMA [UserAccess]
GO
/****** Object:  Table [tbApplicants]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbApplicants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [binary](64) NOT NULL,
	[NationalCode] [char](10) NOT NULL,
	[BranchId] [int] NOT NULL,
	[IdNO] [char](10) NOT NULL,
	[Cellphone] [char](11) NULL,
	[Landline] [char](10) NULL,
	[LandlineCode] [char](10) NULL,
	[Address] [nvarchar](300) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbApplicantTestRegistration]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbApplicantTestRegistration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicantId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[RegistrationDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbApplicantTestRegistrationItems]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbApplicantTestRegistrationItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestItemId] [int] NOT NULL,
	[StatusRegistrationId] [int] NOT NULL,
	[RegistrationDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbBank]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbBank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbBranch]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbBranch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityId] [int] NOT NULL,
	[Code] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Address] [nvarchar](500) NULL,
	[Website] [nvarchar](200) NULL,
	[Email] [nvarchar](150) NULL,
	[Phone] [char](30) NULL,
	[Fax] [char](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbCity]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbCity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[ProvinceId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbPaidBills]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbPaidBills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankId] [int] NOT NULL,
	[TransactionId] [nvarchar](500) NULL,
	[ApplicantTestRegistrationItemsId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbProvinces]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbProvinces](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbReturnCashToApplicant]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbReturnCashToApplicant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[FromBankId] [int] NOT NULL,
	[ToBankId] [int] NOT NULL,
	[TransactionId] [nvarchar](500) NULL,
	[ApplicantTestRegistrationItemsId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbRuleAssign]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbRuleAssign](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RuleId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[TestGroupId] [int] NOT NULL,
	[UserId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbRules]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbRules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbStatusRegistration]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbStatusRegistration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbTest]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestGroupId] [int] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Code] [int] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbTestGroup]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbTestGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] INT,
	[UserId] [bigint] NOT NULL,
	[Code] [int] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbTestItems]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbTestItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[BranchId] INT NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Code] [int] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[HoldingDate] [datetime] NOT NULL,
	[Capacity] [int] NOT NULL,
	[EventPlace] [nvarchar](max) NULL,
	[Cost] [decimal](18, 0) NOT NULL,
	[SeveralNo] [int] NOT NULL,
	[RegistrationDeadline] [datetime] NULL,
	[DeadlineCancelling] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbTestResult]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbTestResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[ApplicantTestRegistrationItemsId] [int] NOT NULL,
	[TestResultTypeId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbTestResultType]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbTestResultType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbMenu]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[TypeId] [tinyint] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Parameter] [nvarchar](500) NULL,
	[Icon] [varchar](200) NULL,
	[Order] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbMenuType]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbMenuType](
	[Id] [tinyint] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbPermission]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbRole]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbRolePermission]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbRolePermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tbUser]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [varbinary](max) NOT NULL,
	[BranchId] [int] NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Mobile] [char](11) NULL,
	[Email] [varchar](100) NULL,
	[IsAdmin] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK__tbUser__3214EC07EF32CBE5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tbUserRole]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tbUserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [tbMenu] ON 
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (1, NULL, 1, N'Security', N'امنیت', NULL, N'fa fa-lock', 1, 1, CAST(N'2018-05-30T06:32:10.757' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (2, 1, 2, N'UserAccessManagement', N'مدیریت دسترسی ها', NULL, N'fa fa-file', 2, 1, CAST(N'2018-05-30T06:46:15.280' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (3, 1, 2, N'ChangePassword', N'تغییر کلمه عبور', NULL, N'fa fa-file', 3, 1, CAST(N'2018-05-30T06:46:15.283' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (4, NULL, 1, N'BaseInformation', N'اطلاعات پایه', NULL, N'fa fa-list-alt', 2, 1, CAST(N'2018-05-30T07:14:13.383' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (5, 4, 2, N'Rules', N'تعریف قوانین', NULL, N'fa fa-file', 1, 1, CAST(N'2018-05-30T07:16:31.050' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (6, 4, 2, N'Rules', N'قوانین', NULL, N'fa fa-file', 2, 1, CAST(N'2018-05-30T07:27:39.587' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (7, 4, 2, N'TestGroup', N'گروه آزمون', NULL, N'fa fa-file', 3, 1, CAST(N'2018-05-30T07:27:39.587' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (8, 4, 2, N'Test', N'آزمون', NULL, N'fa fa-file', 4, 1, CAST(N'2018-05-30T07:27:39.590' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (9, 4, 2, N'City', N'شهر', NULL, N'fa fa-file', 6, 1, CAST(N'2018-05-30T07:27:39.590' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (10, 4, 2, N'Province', N'استان', NULL, N'fa fa-file', 5, 1, CAST(N'2018-05-30T07:27:39.590' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (11, 4, 2, N'Branch', N'واحدها', NULL, N'fa fa-file', 7, 1, CAST(N'2018-05-30T07:27:39.590' AS DateTime))
GO
INSERT [tbMenu] ([Id], [ParentId], [TypeId], [Name], [Title], [Parameter], [Icon], [Order], [IsActive], [CreateDate]) VALUES (12, 1, 2, N'UsersManagement', N'مدیریت کاربران', NULL, N'fa fa-file', 1, 1, CAST(N'2018-05-30T07:35:51.160' AS DateTime))
GO
SET IDENTITY_INSERT [tbMenu] OFF
GO
INSERT [tbMenuType] ([Id], [Title]) VALUES (1, N'منو')
GO
INSERT [tbMenuType] ([Id], [Title]) VALUES (2, N'زیر منو')
GO
SET IDENTITY_INSERT [tbPermission] ON 
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (1, 1, N'Security', N'امنیت', 1, CAST(N'2018-06-11T05:59:08.350' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (2, 2, N'UserAccessManagement', N'مدیریت دسترسی ها', 1, CAST(N'2018-06-11T05:59:08.510' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (3, 3, N'ChangePassword', N'تغییر کلمه عبور', 1, CAST(N'2018-06-11T05:59:08.523' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (4, 4, N'BaseInformation', N'اطلاعات پایه', 1, CAST(N'2018-06-11T05:59:08.523' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (5, 5, N'Rules', N'تعریف قوانین', 1, CAST(N'2018-06-11T05:59:08.540' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (6, 6, N'Rules', N'قوانین', 1, CAST(N'2018-06-11T05:59:08.540' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (7, 7, N'TestGroup', N'گروه آزمون', 1, CAST(N'2018-06-11T05:59:08.540' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (8, 8, N'Test', N'آزمون', 1, CAST(N'2018-06-11T05:59:08.543' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (9, 9, N'City', N'شهر', 1, CAST(N'2018-06-11T05:59:08.543' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (10, 10, N'Province', N'استان', 1, CAST(N'2018-06-11T05:59:08.543' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (11, 11, N'Branch', N'واحدها', 1, CAST(N'2018-06-11T05:59:08.547' AS DateTime))
GO
INSERT [tbPermission] ([Id], [MenuId], [Name], [Title], [IsActive], [CreateDate]) VALUES (12, 12, N'Users', N'کاربران', 1, CAST(N'2018-06-11T05:59:08.547' AS DateTime))
GO
SET IDENTITY_INSERT [tbPermission] OFF
GO
SET IDENTITY_INSERT [tbRole] ON 
GO
INSERT [tbRole] ([Id], [Name], [Title], [IsActive], [CreateDate]) VALUES (1, N'Admin', N'مدیریت سیستم', 1, CAST(N'2018-05-30T06:34:52.960' AS DateTime))
GO
INSERT [tbRole] ([Id], [Name], [Title], [IsActive], [CreateDate]) VALUES (2, N'User Level1', N'کاربران سطح1', 1, CAST(N'2018-05-30T06:36:15.137' AS DateTime))
GO
SET IDENTITY_INSERT [tbRole] OFF
GO
SET IDENTITY_INSERT [tbRolePermission] ON 
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (1, 1, 1, 1, CAST(N'2018-06-11T06:19:40.533' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (2, 1, 2, 1, CAST(N'2018-06-11T06:19:40.577' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (3, 1, 3, 1, CAST(N'2018-06-11T06:19:40.600' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (4, 1, 4, 1, CAST(N'2018-06-11T06:19:40.603' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (5, 1, 5, 1, CAST(N'2018-06-11T06:19:40.603' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (6, 1, 6, 1, CAST(N'2018-06-11T06:19:40.603' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (7, 1, 7, 1, CAST(N'2018-06-11T06:19:40.603' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (8, 1, 8, 1, CAST(N'2018-06-11T06:19:40.603' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (9, 1, 9, 1, CAST(N'2018-06-11T06:19:40.603' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (10, 1, 10, 1, CAST(N'2018-06-11T06:19:40.613' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (11, 1, 11, 1, CAST(N'2018-06-11T06:19:40.613' AS DateTime))
GO
INSERT [tbRolePermission] ([Id], [RoleId], [MenuId], [IsActive], [CreateDate]) VALUES (12, 1, 12, 1, CAST(N'2018-06-11T06:19:40.613' AS DateTime))
GO
SET IDENTITY_INSERT [tbRolePermission] OFF
GO
SET IDENTITY_INSERT [tbUser] ON 
GO
INSERT [tbUser] ([Id], [Username], [Password], [BranchId], [FirstName], [LastName], [Mobile], [Email], [IsAdmin], [IsActive], [CreateDate]) VALUES (1, N'Admin', 0x202CB962AC59075B964B07152D234B70, NULL, N'مدير کل سايت', NULL, NULL, NULL, 1, 1, CAST(N'2018-06-02T06:59:05.937' AS DateTime))
GO
SET IDENTITY_INSERT [tbUser] OFF
GO
SET IDENTITY_INSERT [tbUserRole] ON 
GO
INSERT [tbUserRole] ([Id], [RoleId], [UserId], [IsActive], [CreateDate]) VALUES (1, 1, 1, 1, CAST(N'2018-06-09T06:58:50.587' AS DateTime))
GO
SET IDENTITY_INSERT [tbUserRole] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tbPermis__AEA98A7E05525198]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
ALTER TABLE [tbPermission] ADD UNIQUE NONCLUSTERED 
(
	[MenuId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ__tbRolePe__6400A1A98507D66C]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
ALTER TABLE [tbRolePermission] ADD UNIQUE NONCLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tbUser__536C85E4DCC4B211]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
ALTER TABLE [tbUser] ADD  CONSTRAINT [UQ__tbUser__536C85E4DCC4B211] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ__tbUserRo__5B8242DF6924ADDA]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
ALTER TABLE [tbUserRole] ADD UNIQUE NONCLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [tbApplicants] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbApplicantTestRegistration] ADD  DEFAULT (getdate()) FOR [RegistrationDate]
GO
ALTER TABLE [tbApplicantTestRegistrationItems] ADD  DEFAULT (getdate()) FOR [RegistrationDate]
GO
ALTER TABLE [tbRules] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbMenu] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbPermission] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbRole] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbRolePermission] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbUser] ADD  CONSTRAINT [DF__tbUser__CreateDa__3B75D760]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbUserRole] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [tbApplicants]  WITH CHECK ADD FOREIGN KEY([BranchId])
REFERENCES [tbBranch] ([Id])
GO
ALTER TABLE [tbApplicantTestRegistration]  WITH CHECK ADD FOREIGN KEY([ApplicantId])
REFERENCES [tbApplicants] ([Id])
GO
ALTER TABLE [tbApplicantTestRegistrationItems]  WITH CHECK ADD FOREIGN KEY([TestItemId])
REFERENCES [tbTestItems] ([Id])
GO
ALTER TABLE [tbBranch]  WITH CHECK ADD FOREIGN KEY([CityId])
REFERENCES [tbCity] ([Id])
GO
ALTER TABLE [tbCity]  WITH CHECK ADD FOREIGN KEY([ProvinceId])
REFERENCES [tbProvinces] ([Id])
GO
ALTER TABLE [tbPaidBills]  WITH CHECK ADD FOREIGN KEY([ApplicantTestRegistrationItemsId])
REFERENCES [tbApplicantTestRegistrationItems] ([Id])
GO
ALTER TABLE [tbPaidBills]  WITH CHECK ADD FOREIGN KEY([BankId])
REFERENCES [tbBank] ([Id])
GO
ALTER TABLE [tbReturnCashToApplicant]  WITH CHECK ADD FOREIGN KEY([ApplicantTestRegistrationItemsId])
REFERENCES [tbApplicantTestRegistrationItems] ([Id])
GO
ALTER TABLE [tbReturnCashToApplicant]  WITH CHECK ADD  CONSTRAINT [FK__tbReturnC__UserI__70DDC3D8] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbReturnCashToApplicant] CHECK CONSTRAINT [FK__tbReturnC__UserI__70DDC3D8]
GO
ALTER TABLE [tbRuleAssign]  WITH CHECK ADD FOREIGN KEY([BranchId])
REFERENCES [tbBranch] ([Id])
GO
ALTER TABLE [tbRuleAssign]  WITH CHECK ADD FOREIGN KEY([RuleId])
REFERENCES [tbRules] ([Id])
GO
ALTER TABLE [tbRuleAssign]  WITH CHECK ADD FOREIGN KEY([TestGroupId])
REFERENCES [tbTestGroup] ([Id])
GO
ALTER TABLE [tbRuleAssign]  WITH CHECK ADD  CONSTRAINT [FK__tbRuleAss__UserI__66603565] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbRuleAssign] CHECK CONSTRAINT [FK__tbRuleAss__UserI__66603565]
GO
ALTER TABLE [tbRules]  WITH CHECK ADD  CONSTRAINT [FK__tbRules__UserId__5FB337D6] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbRules] CHECK CONSTRAINT [FK__tbRules__UserId__5FB337D6]
GO
ALTER TABLE [tbTest]  WITH CHECK ADD FOREIGN KEY([TestGroupId])
REFERENCES [tbTestGroup] ([Id])
GO
ALTER TABLE [tbTest]  WITH CHECK ADD  CONSTRAINT [FK__tbTest__UserId__5535A963] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbTest] CHECK CONSTRAINT [FK__tbTest__UserId__5535A963]
GO
ALTER TABLE [tbTestGroup]  WITH CHECK ADD  CONSTRAINT [FK__tbTestGro__UserI__5165187F] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbTestGroup] CHECK CONSTRAINT [FK__tbTestGro__UserI__5165187F]
GO
ALTER TABLE [tbTestItems]  WITH CHECK ADD FOREIGN KEY([TestId])
REFERENCES [tbTest] ([Id])
GO
ALTER TABLE [tbTestItems]  WITH CHECK ADD  CONSTRAINT [FK__tbTestIte__UserI__59063A47] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbTestItems] CHECK CONSTRAINT [FK__tbTestIte__UserI__59063A47]
GO
ALTER TABLE [tbTestResult]  WITH CHECK ADD FOREIGN KEY([ApplicantTestRegistrationItemsId])
REFERENCES [tbApplicantTestRegistrationItems] ([Id])
GO
ALTER TABLE [tbTestResult]  WITH CHECK ADD FOREIGN KEY([TestResultTypeId])
REFERENCES [tbTestResultType] ([Id])
GO
ALTER TABLE [tbTestResult]  WITH CHECK ADD  CONSTRAINT [FK__tbTestRes__UserI__76969D2E] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbTestResult] CHECK CONSTRAINT [FK__tbTestRes__UserI__76969D2E]
GO
ALTER TABLE [tbMenu]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [tbMenu] ([Id])
GO
ALTER TABLE [tbMenu]  WITH CHECK ADD FOREIGN KEY([TypeId])
REFERENCES [tbMenuType] ([Id])
GO
ALTER TABLE [tbPermission]  WITH CHECK ADD FOREIGN KEY([MenuId])
REFERENCES [tbMenu] ([Id])
GO
ALTER TABLE [tbRolePermission]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [tbRole] ([Id])
GO
ALTER TABLE [tbRolePermission]  WITH CHECK ADD  CONSTRAINT [FK_tbRolePermission_tbMenu] FOREIGN KEY([MenuId])
REFERENCES [tbMenu] ([Id])
GO
ALTER TABLE [tbRolePermission] CHECK CONSTRAINT [FK_tbRolePermission_tbMenu]
GO
ALTER TABLE [tbUser]  WITH CHECK ADD  CONSTRAINT [FK__tbUser__BranchId__3A81B327] FOREIGN KEY([BranchId])
REFERENCES [tbBranch] ([Id])
GO
ALTER TABLE [tbUser] CHECK CONSTRAINT [FK__tbUser__BranchId__3A81B327]
GO
ALTER TABLE [tbUserRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [tbRole] ([Id])
GO
ALTER TABLE [tbUserRole]  WITH CHECK ADD  CONSTRAINT [FK__tbUserRol__UserI__403A8C7D] FOREIGN KEY([UserId])
REFERENCES [tbUser] ([Id])
GO
ALTER TABLE [tbUserRole] CHECK CONSTRAINT [FK__tbUserRol__UserI__403A8C7D]
GO
/****** Object:  StoredProcedure [SP_User_Insert]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SP_User_Insert]
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
GO
/****** Object:  StoredProcedure [SP_Authentication]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SP_Authentication]
    @Username NVARCHAR(50),
    @Password VARBINARY(MAX)
AS
BEGIN

    select ur.UserId,ur.RoleId from [UserAccess].[tbUser] u
	inner join [UserAccess].[tbUserRole] ur on u.id = ur.UserId
	where u.Username = @Username 
	AND u.[Password] = @Password -- CAST(0 AS varbinary(MAX))

END;
GO
/****** Object:  StoredProcedure [SP_GetMenu]    Script Date: 04/04/1397 08:55:03 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SP_GetMenu]
    @UserId int
AS
BEGIN

    select  m.[Id]
		   ,m.[ParentId]
		   ,m.[TypeId]
		   ,m.[Name]
		   ,m.[Title]
		   ,m.[Parameter]
		   ,m.[Icon]
		   ,m.[Order]
		   ,m.[IsActive]
	from [UserAccess].[tbUser] u
	inner join [UserAccess].[tbUserRole] ur on u.id = ur.UserId

	inner join [UserAccess].[tbRole] r on r.Id = ur.RoleId
	inner join [UserAccess].[tbRolePermission] rp on rp.RoleId = r.Id
	inner join [UserAccess].[tbMenu] m on m.Id = rp.MenuId
	where u.Id = @UserId 

END;
GO
USE [master]
GO
ALTER DATABASE [MockTestDB] SET  READ_WRITE 
GO
