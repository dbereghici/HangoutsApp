USE [master]
GO
/****** Object:  Database [Hangouts]    Script Date: 02-Jan-18 7:38:21 PM ******/
CREATE DATABASE [Hangouts]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hangouts', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Hangouts.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hangouts_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Hangouts_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Hangouts] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hangouts].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hangouts] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hangouts] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hangouts] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hangouts] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hangouts] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hangouts] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hangouts] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hangouts] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hangouts] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hangouts] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hangouts] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hangouts] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hangouts] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hangouts] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hangouts] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Hangouts] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hangouts] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hangouts] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hangouts] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hangouts] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hangouts] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Hangouts] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hangouts] SET RECOVERY FULL 
GO
ALTER DATABASE [Hangouts] SET  MULTI_USER 
GO
ALTER DATABASE [Hangouts] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hangouts] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hangouts] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hangouts] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hangouts] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Hangouts', N'ON'
GO
ALTER DATABASE [Hangouts] SET QUERY_STORE = OFF
GO
USE [Hangouts]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
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
USE [Hangouts]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 02-Jan-18 7:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Location] [nvarchar](max) NULL,
	[Longitude] [float] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Friendship]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friendship](
	[UserID1] [int] NOT NULL,
	[UserID2] [int] NOT NULL,
	[Status] [nvarchar](max) NULL,
	[ChatID] [int] NOT NULL,
 CONSTRAINT [PK_Friendship] PRIMARY KEY CLUSTERED 
(
	[UserID1] ASC,
	[UserID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AdminID] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChatID] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plan]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChatID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
	[ActivityID] [int] NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Plan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanUser]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanUser](
	[PlanID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_PlanUser] PRIMARY KEY CLUSTERED 
(
	[PlanID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[AddressID] [int] NOT NULL,
	[BirthDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserChat]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserChat](
	[ChatID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_UserChat] PRIMARY KEY CLUSTERED 
(
	[ChatID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 02-Jan-18 7:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[UserID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171109143531_InitialMigration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171109144318_FriendshipMigration1', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171109182653_GroupActivityMigration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171109212312_PlanUserMigration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171109213030_PlanChatMigration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171109214418_MessageMigration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171109215018_GroupPlanMigration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171110212844_GroupUserAdmin', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171111073619_ModifiedPlan', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171111074026_ModifiedUser', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171111094209_UserFriends', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171111140022_UserEmailMigration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171111153926_IgnoredUserFriendsProperty', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171111163102_FriendshipDeleteBehaviorCascade', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171113181351_ModifiedGroup', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171113200421_ModifiedGroup1', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171113205307_GenericRepModifiedGetByID', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171114121631_ModifiedGroupAdminOnetoMany', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171114195010_ModifiedGroup2', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171114204913_ModifiedUserGroup', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171118141110_test', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171123131346_ModifiedCascade', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171123135526_Test1', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171123141151_Test2', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171124075256_Test3', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171124075619_Test4', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171124082346_Test5', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171124083106_Test6', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171124141020_AddressAdminAdded', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171125072105_OnetoManyPlanActivity', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171130104338_OnetoOneChatFriendship', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171130114949_ModifiedRequiredProp', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171130170325_ModifiedRequiredProp1', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171130190801_ModifiedUserAge', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171130193559_ModifiedUserAge1', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171202165330_ModifiedPlan1', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171202170105_ModifiedPlan2', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171202214238_ModifiedActivity', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171203201607_AddedUserChat', N'2.0.0-rtm-26452')
SET IDENTITY_INSERT [dbo].[Activity] ON 

INSERT [dbo].[Activity] ([ID], [GroupID], [Description]) VALUES (31, 75, N'chill')
INSERT [dbo].[Activity] ([ID], [GroupID], [Description]) VALUES (32, 81, N'drinking')
INSERT [dbo].[Activity] ([ID], [GroupID], [Description]) VALUES (33, 81, N'camping')
INSERT [dbo].[Activity] ([ID], [GroupID], [Description]) VALUES (34, 81, N'video games')
INSERT [dbo].[Activity] ([ID], [GroupID], [Description]) VALUES (35, 77, N'art exhibition')
SET IDENTITY_INSERT [dbo].[Activity] OFF
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (71, 54.341262942939835, N'Н8645, Belarus', 29.086188431250037)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (72, 42.713615574883221, N'Emigrant Trail Rd, Casper, WY 82604, USA', -106.68285453749998)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (73, -33.102018484258359, N'Unnamed Road, Córdoba, Argentina', -62.385979537499949)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (74, 52.293272895765568, N'Hafenstraße 60, 48432 Rheine, Germany', 7.5080100254883178)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (75, 62.693612431747368, N'Isoahontie 40, 63210 Kuortane, Finland', 23.395270462500037)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (76, 39.164559896712717, N'Unnamed Road, 13109 Puebla de Don Rodrigo, Cdad. Real, Spain', -4.7453507228026979)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (77, 38.992390895649436, N'Unnamed Road, Agrafa 360 71, Greece', 21.637457962500037)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (78, 65.015864397535609, N'Sveitarfélagið Skagafjörður, Iceland', -18.968010787499963)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (79, 64.941516838343858, N'Skagafjarðarleið, Iceland', -18.792229537499963)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (80, 47.308003799999994, N'Unnamed Road, Petreşti, Moldova', 27.745856399999997)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (81, 56.095708048187475, N'Unnamed Road, Chirsha, Respublika Tatarstan, Russia, 422712', 49.235114212500036)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (82, 48.369838131349752, N'Unnamed Road, Pukanec, Slovakia', 18.660163040625037)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (83, 46.969845228669215, N'Strada Aeroportului, Chișinău, Moldova', 28.874701614843787)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (84, 52.2941128199617, N'Kobser Weg, 14793 Ziesar, Germany', 12.299079056250037)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (85, 60.951038906969529, N'Myllylahdenpolku 19, 47490 Iitti, Finland', 26.207770462500037)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (86, 51.589778468847243, N'An der Mönchlede 5, 37441 Bad Sachsa, Germany', 10.563239212500037)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (87, 46.7581129215703, N'Strada Republicii 96, Cluj-Napoca 400000, Romania', 23.593319411741675)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (88, 47.306141428015735, N'L391, Moldova', 28.057593460546912)
INSERT [dbo].[Address] ([ID], [Latitude], [Location], [Longitude]) VALUES (89, 50.596221549433352, N'Unnamed Road, 07422 Saalfelder Höhe, Germany', 11.266364212500037)
SET IDENTITY_INSERT [dbo].[Address] OFF
SET IDENTITY_INSERT [dbo].[Chat] ON 

INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (316, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (317, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (318, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (319, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (320, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (321, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (322, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (323, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (324, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (325, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (326, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (327, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (328, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (329, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (330, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (331, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (332, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (333, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (334, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (335, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (336, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (337, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (338, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (339, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (340, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (341, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (342, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (343, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (344, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (345, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (346, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (347, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (348, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (349, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (350, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (351, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (352, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (353, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (354, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (355, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (356, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (357, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (358, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (359, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (360, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (361, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (362, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (363, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (364, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (365, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (366, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (367, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (368, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (369, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (370, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (371, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (372, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (373, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (374, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (375, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (376, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (377, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (378, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (379, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (380, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (381, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (382, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (383, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (384, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (385, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (386, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (387, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (388, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (389, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (390, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (391, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (392, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (393, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (394, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (395, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (396, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (397, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (398, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (399, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (400, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (401, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (402, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (403, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (404, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (405, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (406, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (407, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (408, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (409, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (410, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (411, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (412, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Chat] ([ID], [CreatedAt]) VALUES (413, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Chat] OFF
GO
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 131, N'pending', 399)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 133, N'accepted', 318)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 134, N'accepted', 322)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 135, N'accepted', 324)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 136, N'accepted', 327)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 137, N'accepted', 331)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 140, N'pending', 357)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 141, N'pending', 358)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 142, N'accepted', 368)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 143, N'accepted', 384)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (130, 144, N'accepted', 385)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 132, N'accepted', 317)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 133, N'accepted', 320)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 135, N'accepted', 326)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 137, N'pending', 401)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 138, N'accepted', 339)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 139, N'accepted', 349)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 140, N'accepted', 351)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 141, N'accepted', 359)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 143, N'pending', 375)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (131, 144, N'pending', 395)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 133, N'accepted', 319)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 134, N'accepted', 321)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 135, N'accepted', 325)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 137, N'pending', 402)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 139, N'accepted', 348)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 140, N'accepted', 356)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 141, N'pending', 360)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 142, N'pending', 369)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 143, N'pending', 376)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (132, 144, N'accepted', 386)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 134, N'accepted', 323)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 136, N'accepted', 330)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 137, N'pending', 403)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 138, N'accepted', 338)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 139, N'accepted', 346)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 140, N'accepted', 350)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 143, N'accepted', 380)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (133, 144, N'pending', 387)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 131, N'pending', 398)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 136, N'pending', 329)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 137, N'pending', 332)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 138, N'pending', 336)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 139, N'pending', 345)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 140, N'pending', 353)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 141, N'pending', 361)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 143, N'pending', 382)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (134, 144, N'pending', 394)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 136, N'accepted', 328)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 137, N'accepted', 333)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 138, N'accepted', 340)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 139, N'accepted', 347)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 141, N'accepted', 364)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 142, N'pending', 371)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 143, N'pending', 381)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (135, 144, N'pending', 396)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (136, 137, N'pending', 334)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (136, 138, N'pending', 337)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (136, 139, N'pending', 341)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (136, 140, N'pending', 352)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (136, 141, N'pending', 367)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (136, 142, N'pending', 370)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (136, 144, N'pending', 389)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (137, 139, N'accepted', 342)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (137, 141, N'accepted', 363)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (137, 143, N'accepted', 377)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (137, 144, N'accepted', 388)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (138, 137, N'accepted', 404)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (138, 139, N'accepted', 343)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (138, 140, N'accepted', 355)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (138, 141, N'accepted', 362)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (138, 144, N'accepted', 392)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (139, 140, N'accepted', 354)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (139, 142, N'accepted', 372)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (139, 143, N'accepted', 378)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (139, 144, N'pending', 397)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (140, 137, N'accepted', 405)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (140, 141, N'pending', 365)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (140, 144, N'pending', 393)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (141, 142, N'accepted', 373)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (141, 143, N'pending', 379)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (141, 144, N'pending', 390)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (142, 137, N'pending', 407)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (142, 143, N'pending', 383)
INSERT [dbo].[Friendship] ([UserID1], [UserID2], [Status], [ChatID]) VALUES (143, 144, N'pending', 391)
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (75, 130, N'School friends')
INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (76, 130, N'University Friends')
INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (77, 131, N'University Friends')
INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (78, 133, N'Football friends')
INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (79, 136, N'Work Friends')
INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (80, 137, N'College Friends')
INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (81, 138, N'College Friends')
INSERT [dbo].[Group] ([ID], [AdminID], [Name]) VALUES (82, 142, N'Drinking Friends')
SET IDENTITY_INSERT [dbo].[Group] OFF
SET IDENTITY_INSERT [dbo].[Message] ON 

INSERT [dbo].[Message] ([ID], [ChatID], [Content], [CreatedAt], [UserID]) VALUES (51, 322, N'hi there :) ', CAST(N'2018-01-02T19:17:04.7015406' AS DateTime2), 130)
INSERT [dbo].[Message] ([ID], [ChatID], [Content], [CreatedAt], [UserID]) VALUES (52, 317, N'hi :) ', CAST(N'2018-01-02T19:19:33.5177493' AS DateTime2), 131)
INSERT [dbo].[Message] ([ID], [ChatID], [Content], [CreatedAt], [UserID]) VALUES (53, 348, N'hey', CAST(N'2018-01-02T19:26:43.5792153' AS DateTime2), 139)
INSERT [dbo].[Message] ([ID], [ChatID], [Content], [CreatedAt], [UserID]) VALUES (54, 348, N'how are you? :) ', CAST(N'2018-01-02T19:26:47.5446360' AS DateTime2), 139)
INSERT [dbo].[Message] ([ID], [ChatID], [Content], [CreatedAt], [UserID]) VALUES (55, 400, N'hi guys', CAST(N'2018-01-02T19:28:14.8983984' AS DateTime2), 139)
INSERT [dbo].[Message] ([ID], [ChatID], [Content], [CreatedAt], [UserID]) VALUES (56, 400, N'how are you? ', CAST(N'2018-01-02T19:28:19.4173932' AS DateTime2), 139)
SET IDENTITY_INSERT [dbo].[Message] OFF
SET IDENTITY_INSERT [dbo].[Plan] ON 

INSERT [dbo].[Plan] ([ID], [ChatID], [GroupID], [AddressID], [ActivityID], [EndTime], [StartTime]) VALUES (99, 400, 75, 86, 31, CAST(N'2018-02-02T17:02:00.0000000' AS DateTime2), CAST(N'2018-02-02T14:02:00.0000000' AS DateTime2))
INSERT [dbo].[Plan] ([ID], [ChatID], [GroupID], [AddressID], [ActivityID], [EndTime], [StartTime]) VALUES (100, 406, 80, 87, 31, CAST(N'2019-04-02T17:02:00.0000000' AS DateTime2), CAST(N'2018-04-02T02:02:00.0000000' AS DateTime2))
INSERT [dbo].[Plan] ([ID], [ChatID], [GroupID], [AddressID], [ActivityID], [EndTime], [StartTime]) VALUES (101, 408, 81, 80, 32, CAST(N'2018-10-05T01:00:00.0000000' AS DateTime2), CAST(N'2018-10-04T14:00:00.0000000' AS DateTime2))
INSERT [dbo].[Plan] ([ID], [ChatID], [GroupID], [AddressID], [ActivityID], [EndTime], [StartTime]) VALUES (102, 409, 75, 80, 31, CAST(N'2022-01-04T01:00:00.0000000' AS DateTime2), CAST(N'2018-01-03T01:00:00.0000000' AS DateTime2))
INSERT [dbo].[Plan] ([ID], [ChatID], [GroupID], [AddressID], [ActivityID], [EndTime], [StartTime]) VALUES (103, 410, 81, 88, 33, CAST(N'2018-05-07T01:00:00.0000000' AS DateTime2), CAST(N'2018-05-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Plan] ([ID], [ChatID], [GroupID], [AddressID], [ActivityID], [EndTime], [StartTime]) VALUES (104, 411, 81, 80, 34, CAST(N'2018-01-04T07:00:00.0000000' AS DateTime2), CAST(N'2018-01-04T01:00:00.0000000' AS DateTime2))
INSERT [dbo].[Plan] ([ID], [ChatID], [GroupID], [AddressID], [ActivityID], [EndTime], [StartTime]) VALUES (105, 413, 77, 89, 35, CAST(N'2018-01-04T16:00:00.0000000' AS DateTime2), CAST(N'2018-01-04T13:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Plan] OFF
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (99, 135)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (100, 137)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (101, 138)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (99, 139)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (102, 139)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (103, 140)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (101, 141)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (103, 141)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (104, 141)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (101, 143)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (103, 143)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (104, 143)
INSERT [dbo].[PlanUser] ([PlanID], [UserID]) VALUES (105, 144)
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (130, N'khaldrogo', N'khal', N'drogo', N'khaldrogo', N'khaldrogo@gmail.com', 71, CAST(N'1984-02-22T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (131, N'baelish', N'petyr', N'baelish', N'baelish', N'baelish@gmail.com', 72, CAST(N'1970-07-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (132, N'jamie4', N'jamie', N'lannister', N'jamie4', N'jamielannister@gmail.com', 73, CAST(N'1980-02-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (133, N'ramsay', N'ramsay', N'snow', N'ramsay', N'ramsay@gmail.com', 74, CAST(N'1986-02-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (134, N'brienne', N'brienne', N'of tarth', N'brienne', N'brienne@gmail.com', 75, CAST(N'1983-10-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (135, N'mormont', N'jorah', N'mormont', N'mormont', N'jorahmormont@gmail.com', 76, CAST(N'1977-08-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (136, N'jonsnow', N'jon', N'snow', N'jonsnow', N'jonsnow@gmail.com', 77, CAST(N'1985-07-23T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (137, N'daenerys', N'daenerys', N'targaryen', N'daenerys', N'daenerys@gmail.com', 78, CAST(N'1986-06-28T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (138, N'mountain', N'gregor', N'clegane', N'mountain', N'mountain@gmail.com', 79, CAST(N'1970-09-22T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (139, N'cersei', N'cersei', N'baratheon', N'cersei', N'cersei@gmail.com', 80, CAST(N'1972-02-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (140, N'aryastark', N'arya', N'stark', N'aryastark', N'aryastark@gmail.com', 81, CAST(N'1980-02-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (141, N'sansa4', N'sansa', N'stark', N'sansa4', N'sansastark@gmail.com', 82, CAST(N'1995-02-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (142, N'tyrion', N'tyrion', N'lannister', N'tyrion', N'tyrion@gmail.com', 83, CAST(N'1980-02-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (143, N'thehound', N'sandor', N'clegane', N'thehound', N'thehound@gmail.com', 84, CAST(N'1977-02-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[User] ([ID], [Username], [FirstName], [LastName], [Password], [Email], [AddressID], [BirthDate]) VALUES (144, N'daario', N'daario', N'naharis', N'daario', N'daario@gmail.com', 85, CAST(N'1977-01-18T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (316, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (318, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (322, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (324, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (327, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (331, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (335, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (344, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (357, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (358, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (368, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (374, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (384, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (385, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (399, 130)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (317, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (320, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (326, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (339, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (349, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (351, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (359, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (375, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (395, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (398, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (399, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (401, 131)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (316, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (317, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (319, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (321, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (325, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (348, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (356, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (360, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (369, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (376, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (386, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (402, 132)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (318, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (319, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (320, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (323, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (330, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (338, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (346, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (350, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (380, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (387, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (403, 133)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (321, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (322, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (323, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (329, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (332, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (336, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (345, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (353, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (361, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (382, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (394, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (398, 134)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (324, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (325, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (326, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (328, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (333, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (340, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (347, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (364, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (371, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (381, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (396, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (400, 135)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (327, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (328, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (329, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (330, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (334, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (337, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (341, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (352, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (366, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (367, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (370, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (389, 136)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (331, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (332, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (333, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (334, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (342, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (363, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (377, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (388, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (401, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (402, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (403, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (404, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (405, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (406, 137)
GO
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (407, 137)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (335, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (336, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (337, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (338, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (339, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (340, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (343, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (355, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (362, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (392, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (404, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (408, 138)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (341, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (342, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (343, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (344, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (345, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (346, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (347, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (348, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (349, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (354, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (372, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (378, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (397, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (400, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (409, 139)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (350, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (351, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (352, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (353, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (354, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (355, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (356, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (357, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (365, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (393, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (405, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (410, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (412, 140)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (358, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (359, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (360, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (361, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (362, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (363, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (364, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (365, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (366, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (367, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (373, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (379, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (390, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (408, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (410, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (411, 141)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (368, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (369, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (370, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (371, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (372, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (373, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (383, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (407, 142)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (374, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (375, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (376, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (377, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (378, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (379, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (380, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (381, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (382, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (383, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (384, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (391, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (408, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (410, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (411, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (412, 143)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (385, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (386, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (387, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (388, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (389, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (390, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (391, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (392, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (393, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (394, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (395, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (396, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (397, 144)
INSERT [dbo].[UserChat] ([ChatID], [UserID]) VALUES (413, 144)
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (130, 75, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (130, 76, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (130, 82, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (131, 75, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (131, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (131, 77, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (131, 81, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (132, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (132, 79, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (132, 81, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (133, 75, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (133, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (133, 78, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (133, 79, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (133, 80, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (133, 81, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (134, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (134, 78, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (135, 75, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (135, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (135, 77, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (135, 81, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (136, 75, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (136, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (136, 78, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (136, 79, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (137, 75, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (137, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (137, 78, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (137, 80, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (137, 81, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (138, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (138, 78, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (138, 80, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (138, 81, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (139, 75, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (139, 81, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (139, 82, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (140, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (140, 79, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (140, 80, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (140, 81, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (141, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (141, 78, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (141, 79, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (141, 80, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (141, 81, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (141, 82, N'received')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 75, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 77, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 78, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 79, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 80, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 81, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (142, 82, N'admin')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (143, 75, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (143, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (143, 79, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (143, 80, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (143, 81, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (143, 82, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (144, 75, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (144, 76, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (144, 77, N'member')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (144, 78, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (144, 79, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (144, 80, N'sent')
INSERT [dbo].[UserGroup] ([UserID], [GroupID], [Status]) VALUES (144, 81, N'member')
/****** Object:  Index [IX_Activity_GroupID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Activity_GroupID] ON [dbo].[Activity]
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Friendship_ChatID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Friendship_ChatID] ON [dbo].[Friendship]
(
	[ChatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Friendship_UserID2]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Friendship_UserID2] ON [dbo].[Friendship]
(
	[UserID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Group_AdminID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Group_AdminID] ON [dbo].[Group]
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Message_ChatID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Message_ChatID] ON [dbo].[Message]
(
	[ChatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Message_UserID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Message_UserID] ON [dbo].[Message]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Plan_ActivityID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Plan_ActivityID] ON [dbo].[Plan]
(
	[ActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Plan_AddressID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Plan_AddressID] ON [dbo].[Plan]
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Plan_ChatID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Plan_ChatID] ON [dbo].[Plan]
(
	[ChatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Plan_GroupID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Plan_GroupID] ON [dbo].[Plan]
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlanUser_UserID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_PlanUser_UserID] ON [dbo].[PlanUser]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_User_AddressID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_User_AddressID] ON [dbo].[User]
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserChat_UserID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserChat_UserID] ON [dbo].[UserChat]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserGroup_GroupID]    Script Date: 02-Jan-18 7:38:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserGroup_GroupID] ON [dbo].[UserGroup]
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chat] ADD  DEFAULT ('0001-01-01T00:00:00.000') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Friendship] ADD  DEFAULT ((0)) FOR [ChatID]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT ((0)) FOR [AdminID]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[Plan] ADD  DEFAULT ((0)) FOR [GroupID]
GO
ALTER TABLE [dbo].[Plan] ADD  DEFAULT ((0)) FOR [AddressID]
GO
ALTER TABLE [dbo].[Plan] ADD  DEFAULT ('0001-01-01T00:00:00.000') FOR [EndTime]
GO
ALTER TABLE [dbo].[Plan] ADD  DEFAULT ('0001-01-01T00:00:00.000') FOR [StartTime]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [AddressID]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ('0001-01-01T00:00:00.000') FOR [BirthDate]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Group_GroupID] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_Group_GroupID]
GO
ALTER TABLE [dbo].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK_Friendship_Chat_ChatID] FOREIGN KEY([ChatID])
REFERENCES [dbo].[Chat] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Friendship] CHECK CONSTRAINT [FK_Friendship_Chat_ChatID]
GO
ALTER TABLE [dbo].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK_Friendship_User_UserID1] FOREIGN KEY([UserID1])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Friendship] CHECK CONSTRAINT [FK_Friendship_User_UserID1]
GO
ALTER TABLE [dbo].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK_Friendship_User_UserID2] FOREIGN KEY([UserID2])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Friendship] CHECK CONSTRAINT [FK_Friendship_User_UserID2]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_User_AdminID] FOREIGN KEY([AdminID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_User_AdminID]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Chat_ChatID] FOREIGN KEY([ChatID])
REFERENCES [dbo].[Chat] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Chat_ChatID]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User_UserID]
GO
ALTER TABLE [dbo].[Plan]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Activity_ActivityID] FOREIGN KEY([ActivityID])
REFERENCES [dbo].[Activity] ([ID])
GO
ALTER TABLE [dbo].[Plan] CHECK CONSTRAINT [FK_Plan_Activity_ActivityID]
GO
ALTER TABLE [dbo].[Plan]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Address_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Plan] CHECK CONSTRAINT [FK_Plan_Address_AddressID]
GO
ALTER TABLE [dbo].[Plan]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Chat_ChatID] FOREIGN KEY([ChatID])
REFERENCES [dbo].[Chat] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Plan] CHECK CONSTRAINT [FK_Plan_Chat_ChatID]
GO
ALTER TABLE [dbo].[Plan]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Group_GroupID] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Plan] CHECK CONSTRAINT [FK_Plan_Group_GroupID]
GO
ALTER TABLE [dbo].[PlanUser]  WITH CHECK ADD  CONSTRAINT [FK_PlanUser_Plan_PlanID] FOREIGN KEY([PlanID])
REFERENCES [dbo].[Plan] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlanUser] CHECK CONSTRAINT [FK_PlanUser_Plan_PlanID]
GO
ALTER TABLE [dbo].[PlanUser]  WITH CHECK ADD  CONSTRAINT [FK_PlanUser_User_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[PlanUser] CHECK CONSTRAINT [FK_PlanUser_User_UserID]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Address_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Address_AddressID]
GO
ALTER TABLE [dbo].[UserChat]  WITH CHECK ADD  CONSTRAINT [FK_UserChat_Chat_ChatID] FOREIGN KEY([ChatID])
REFERENCES [dbo].[Chat] ([ID])
GO
ALTER TABLE [dbo].[UserChat] CHECK CONSTRAINT [FK_UserChat_Chat_ChatID]
GO
ALTER TABLE [dbo].[UserChat]  WITH CHECK ADD  CONSTRAINT [FK_UserChat_User_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserChat] CHECK CONSTRAINT [FK_UserChat_User_UserID]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Group_GroupID] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_Group_GroupID]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_User_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_User_UserID]
GO
USE [master]
GO
ALTER DATABASE [Hangouts] SET  READ_WRITE 
GO
