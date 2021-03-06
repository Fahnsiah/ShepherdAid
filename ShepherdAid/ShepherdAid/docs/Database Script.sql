USE [ShepherdAidDB]
GO
/****** Object:  StoredProcedure [dbo].[AddNewRole]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AddNewRole]
@RoleName nvarchar(20)

AS

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name])
     VALUES
           (NEWID(),
           @RoleName)

GO
/****** Object:  StoredProcedure [dbo].[GetAssignedRoles]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetAssignedRoles]
(
	@UserID nvarchar(50)
)

AS

SELECT 
	R.Id, 
	R.Name 
FROM 
	AspNetUserRoles UR 
	JOIN AspNetRoles R ON UR.RoleId = R.Id  
WHERE UR.UserId = @UserID
GO
/****** Object:  StoredProcedure [dbo].[GetAvailableRoles]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetAvailableRoles]
(
	@UserID nvarchar(50)
)

AS

SELECT 
	R.Id, 
	R.Name 
FROM 
	AspNetRoles R 
WHERE 
	R.Id NOT IN 
	(SELECT UR.RoleId FROM AspNetUserRoles UR 
		WHERE UR.UserId = @UserID)
GO
/****** Object:  StoredProcedure [dbo].[GetSystemUsers]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetSystemUsers]
(
	@UserID nvarchar(50),
	@InstitutionID int
)

AS

IF((SELECT COUNT(*) FROM AspNetUserRoles R WHERE R.UserId = @UserID AND R.RoleId = '9220F72C-A862-47EA-83B7-D31DF5F2180F') >0) BEGIN --System admin

	DECLARE @institutionGroupID int = (SELECT I.InstitutionGroupID FROM Institutions I WHERE I.ID = @InstitutionID)
	SELECT
		[Id]
		,[FirstName]
		,[MiddleName]
		,[LastName]
		,[UserName]
		,[Email]
		,[PhoneNumber]
		,[ClientID]
		,[InstitutionGroupID]
		,[InstitationID]
	FROM 
		vwSystemUsers U
	WHERE
		U.ClientID = (SELECT G.ClientID FROM InstitutionGroups G WHERE G.ID = @institutionGroupID)
END
ELSE IF((SELECT COUNT(*) FROM AspNetUserRoles R WHERE R.UserId = @UserID AND R.RoleId = '3dfe8e1a-350f-4dc3-989e-07a3f5038347') >0) BEGIN --admin

	SELECT
		[Id]
		,[FirstName]
		,[MiddleName]
		,[LastName]
		,[UserName]
		,[Email]
		,[PhoneNumber]
		,[ClientID]
		,[InstitutionGroupID]
		,[InstitationID]
	FROM 
		vwSystemUsers U
	WHERE
		U.InstitationID =  @institutionGroupID
END

ELSE IF((SELECT COUNT(*) FROM AspNetUserRoles R WHERE R.UserId = @UserID AND R.RoleId = 'd704556c-0624-4992-8882-15a5a710e1dc') > 0) BEGIN --super admin

	SELECT
		[Id]
		,[FirstName]
		,[MiddleName]
		,[LastName]
		,[UserName]
		,[Email]
		,[PhoneNumber]
		,[ClientID]
		,[InstitutionGroupID]
		,[InstitationID]
	FROM 
		vwSystemUsers U
	WHERE
		U.InstitationID =  @institutionGroupID
END

GO
/****** Object:  StoredProcedure [dbo].[RemoveUserFromRole]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RemoveUserFromRole]
(
	@UserID nvarchar(50),
	@RoleID nvarchar(50)
)

AS

DELETE FROM AspNetUserRoles 
WHERE 
	UserId = @UserID AND 
	RoleId =@RoleID
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](75) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](75) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Initial] [nvarchar](15) NULL,
	[OfficePhone] [nvarchar](50) NOT NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](50) NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Companies]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Initial] [nvarchar](15) NULL,
	[OfficePhone] [nvarchar](50) NOT NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](50) NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Counties]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](5) NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Counties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GenderTypes]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenderTypes](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[TypeName] [nvarchar](32) NOT NULL,
	[RecordedBy] [nvarchar](32) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_GenderTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InstitationUsers]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstitationUsers](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[InstitationID] [int] NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[StatusTypeID] [int] NOT NULL,
	[RecordedBy] [nvarchar](50) NULL,
	[DateRecorded] [datetime] NULL,
 CONSTRAINT [PK_InstitationUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InstitutionGroups]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstitutionGroups](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Initial] [nvarchar](15) NULL,
	[OfficePhone] [nvarchar](50) NOT NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](50) NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_InstitutionGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Institutions]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institutions](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[InstitutionGroupID] [int] NOT NULL,
	[InstitutionName] [nvarchar](50) NOT NULL,
	[Initial] [nvarchar](50) NULL,
	[OfficePhone] [nvarchar](50) NOT NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](50) NULL,
	[StatusTypeID] [int] NOT NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_Institutions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MaritalStatusTypes]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaritalStatusTypes](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[TypeName] [nvarchar](32) NOT NULL,
	[RecordedBy] [nvarchar](32) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_MaritalStatusTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Members]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[ID] [int] IDENTITY(100000000,1) NOT NULL,
	[InstitutionID] [int] NOT NULL,
	[MemberID] [nvarchar](20) NOT NULL,
	[SalutationTypeID] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[GenderTypeID] [int] NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[MaritalStatusTypeID] [int] NOT NULL,
	[ResidentAddress] [nvarchar](150) NOT NULL,
	[EmailAddress] [nvarchar](75) NULL,
	[MobilePhone] [nvarchar](15) NOT NULL,
	[OfficePhone] [nvarchar](15) NULL,
	[NationalityTypeID] [int] NOT NULL,
	[CountyID] [int] NULL,
	[Region] [nvarchar](50) NULL,
	[MemberTypeID] [int] NOT NULL,
	[StatusTypeID] [int] NOT NULL,
	[PhotoPath] [nvarchar](250) NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberTypes]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberTypes](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[TypeName] [nvarchar](32) NOT NULL,
	[RecordedBy] [nvarchar](32) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_MemberTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NationalityTypes]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NationalityTypes](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[TypeName] [nvarchar](32) NOT NULL,
	[Value] [nvarchar](50) NULL,
	[RecordedBy] [nvarchar](32) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_NationalityTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalutationTypes]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalutationTypes](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[TypeName] [nvarchar](32) NOT NULL,
	[RecordedBy] [nvarchar](32) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_SalutationTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusTypes]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemVariables]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemVariables](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
	[RecordedBy] [nvarchar](50) NOT NULL,
	[DateRecorded] [datetime] NOT NULL,
 CONSTRAINT [PK_SystemVariables] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Trackers]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trackers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[IpAddress] [nvarchar](50) NOT NULL,
	[AreaAccessed] [nvarchar](100) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[PreviousValues] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Trackers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[vwSystemUsers]    Script Date: 7/28/2017 3:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwSystemUsers]

AS

SELECT
	U.Id,
	U.FirstName,
	U.MiddleName,
	U.LastName,
	U.UserName,
	U.Email,
	U.PhoneNumber,
	IG.ClientID,
	I.InstitutionGroupID,
	IU.InstitationID
FROM 
	AspNetUsers U
	JOIN InstitationUsers IU ON U.Id = IU.UserID 
	JOIN Institutions I ON IU.InstitationID = I.ID 
	JOIN InstitutionGroups IG ON I.InstitutionGroupID = IG.ID
	JOIN Clients C ON IG.ClientID = C.ID
WHERE U.UserName <> 'superadmin'

GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'09d28441-d5aa-421e-a1f9-466fde0806fc', N'Top Management Report')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3dfe8e1a-350f-4dc3-989e-07a3f5038347', N'Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4666b8cd-040a-466a-9b9d-55e3e5bff9fe', N'General Report')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'79D30350-34E0-4F1C-B75F-447C915AFCF0', N'Group Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7ac33cc9-57ea-458d-9638-9539ab8efd8c', N'Data Entry')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9220F72C-A862-47EA-83B7-D31DF5F2180F', N'System Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a522778d-d30b-4295-b8f5-58a0b9dfdfa7', N'Data Delete')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'aa82ce48-02cb-4c24-adf6-3b964cd6b89d', N'Audit Trail Report')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'AB7F58D7-B670-4D2E-9D7E-AF6B103FAD5E', N'Users')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd704556c-0624-4992-8882-15a5a710e1dc', N'Super Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'DE4C4595-6D1A-4AE9-90CA-05BB8F71E250', N'Member')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f8146e57-585d-4d8b-a9c8-1509a72addac', N'Data Change')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0d02ab69-1c7a-416f-a4cc-981c0e7b57d0', N'3dfe8e1a-350f-4dc3-989e-07a3f5038347')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1a9098a0-2a11-4b83-a559-c9c004542d38', N'9220F72C-A862-47EA-83B7-D31DF5F2180F')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4d68ad4b-d243-4644-aa72-5c72bfcb328d', N'9220F72C-A862-47EA-83B7-D31DF5F2180F')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7645cc65-3b05-497e-a9c2-3e7857b6c085', N'7ac33cc9-57ea-458d-9638-9539ab8efd8c')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9387e9bc-ab3a-4e20-a73c-b5730fff0c4e', N'3dfe8e1a-350f-4dc3-989e-07a3f5038347')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bfa32b34-27ab-4468-9157-9f4888d2dda5', N'3dfe8e1a-350f-4dc3-989e-07a3f5038347')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c8c18175-403f-4e37-ab45-f8ef8c524291', N'AB7F58D7-B670-4D2E-9D7E-AF6B103FAD5E')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd3d9f87b-8ef6-4cda-b093-4f03bc01c7a4', N'd704556c-0624-4992-8882-15a5a710e1dc')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dd5f7ca2-fd39-408a-949d-85dc70bdb499', N'9220F72C-A862-47EA-83B7-D31DF5F2180F')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f3fc189d-6f0c-4bc9-8d88-3dae24577e1a', N'3dfe8e1a-350f-4dc3-989e-07a3f5038347')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f52c77ea-905b-41d5-9264-99e489c2a819', N'3dfe8e1a-350f-4dc3-989e-07a3f5038347')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f52c77ea-905b-41d5-9264-99e489c2a819', N'9220F72C-A862-47EA-83B7-D31DF5F2180F')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'0d02ab69-1c7a-416f-a4cc-981c0e7b57d0', N'siokoye@hip.org', 0, N'AEV4F8G8L5dvacc6B5vriIDqPCABQKEtUbOvB7SSyO/SP6/mrWdMi3UW88x2VvkF7A==', N'a5db410e-792c-4f40-ae4e-f2d5c3e24559', NULL, 0, 0, NULL, 0, 0, N'siokoye', N'Stephen', N'I', N'Okoye')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'1a9098a0-2a11-4b83-a559-c9c004542d38', N'ymdavies@yahoo.com', 0, N'AEfDAzQvb8BvNE6ecj+FbsAnVxyejnIZ2gDNyQWajn+KJhLw5UBH+FKwrA0MGxdZ2A==', N'6b28a64a-04b2-4877-a31e-5550589dd852', NULL, 0, 0, NULL, 0, 0, N'ymdavies', N'Yassah', N'Moore', N'Davies')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'4d68ad4b-d243-4644-aa72-5c72bfcb328d', N'pkkerkula@me.com', 0, N'AJPb/0eifnC1TUFv4Ol+TL8CetouiIrGIGa2oVdt06DtRAunzXlaW5JONq/xDNuaRw==', N'f2f3b03b-6098-403f-af79-d67b62e58ada', NULL, 0, 0, NULL, 0, 0, N'pkkerkula', N'Peter', N'K.', N'Kerkula')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'7645cc65-3b05-497e-a9c2-3e7857b6c085', N'owade@gmail.com', 0, N'ALCALeKvlsZNR53gsphQ96qtlmw84cJP7Mratd8XPkGo37kHqlefZOFW+u7nJZXm2Q==', N'88d0e56f-345f-451a-8d08-34e6783b46a6', NULL, 0, 0, NULL, 0, 0, N'owade', N'Ophelia', NULL, N'Wade')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'9387e9bc-ab3a-4e20-a73c-b5730fff0c4e', N'mnmah@hip.org', 0, N'AKt6EXoANU7HdgQHt3WG5kKpLt8+kLFwIs44z9Src1+ysUbRCp998bU+dCTkPlL90w==', N'f82d5f08-1eb1-466e-9204-a4eb83d3a6d7', NULL, 0, 0, NULL, 0, 0, N'mnmah', N'Michael', NULL, N'Nmah')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'bfa32b34-27ab-4468-9157-9f4888d2dda5', N'mkwilson@me.com', 0, N'AAZVm+kxDkiO6lAu0iqwXGZJ7CaPRTeYlDQEcbYf55Mun+TAoaoAk4mA5cZVMQ1ibQ==', N'8a0d5597-b85d-4f25-8e01-e64617716522', NULL, 0, 0, NULL, 0, 0, N'mkwilson', N'Mary', N'Koshie', N'Wilson')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'c8c18175-403f-4e37-ab45-f8ef8c524291', N'info@cabicol.org', 0, N'AKdTaLd/KSoiDaUx4xqFxygXRqWh6pHVSkA4RERrKb9CAx6q2vLceEOdmucocCM4aQ==', N'476dfb02-7a29-4f9a-bbb4-13b2662be81c', NULL, 0, 0, NULL, 0, 0, N'csuniversity', N'Columbia', N'southern', N'university')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'd3d9f87b-8ef6-4cda-b093-4f03bc01c7a4', N'info@riteliberia.org', 0, N'APSxCXRK+nWOwFGHBV/bB9dP0OswRk5P042Z5Q9DhUxHGu0+0v3TMVKbnz2X8d90RQ==', N'2f1d777f-4717-475a-8a60-5404e33e8341', N'+231776943923', 0, 0, NULL, 0, 0, N'superadmin', N'Super', NULL, N'Administrator')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'dd5f7ca2-fd39-408a-949d-85dc70bdb499', N'fkollie@me.com', 0, N'APH9XvPqRHQr14YLJi5QmlNDrTBGeZxXRk/EB3MUFlhYgf6+NEIANwdMHg69k5EIlg==', N'8fade0bf-5a0b-4e96-a844-df122c4a4894', NULL, 0, 0, NULL, 0, 0, N'fkollie', N'Francis', NULL, N'Kollie')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'f3fc189d-6f0c-4bc9-8d88-3dae24577e1a', N'hfwilson@mwetana.com.lr', 0, N'ADJgxwII1snyQyBEbOwzD5LIc+py/5V9xxpJJOUNn2Xo6Tgv8TZxr5lGB1+btZSObw==', N'9d2a6ad6-7de6-4ada-830f-f41210baae5a', NULL, 0, 0, NULL, 0, 0, N'hfwilson', N'Henry', N'Fahnsian', N'Wilson')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [MiddleName], [LastName]) VALUES (N'f52c77ea-905b-41d5-9264-99e489c2a819', N'zmoore@me.com', 0, N'AGnEC2zPSLEZLEcEAMSZYC/MmgyEBxvZwZswbFaD6e8zCR+8emHuqWqLDYx3r9Rbiw==', N'0bd68f4b-30fc-4c2a-85e2-a5093f43b437', NULL, 0, 0, NULL, 0, 0, N'zmoore', N'Zack', NULL, N'Moore')
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

GO
INSERT [dbo].[Clients] ([ID], [CompanyID], [Name], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [RecordedBy], [DateRecorded]) VALUES (100, 100, N'Catholic Bishops Conference of Liberia', N'CABICOL', N'+231...', N'+231...', N'info@cabicol.org', N'Monrovia, Liberia', N'www.cabicol.org', N'Developer', CAST(0x0000A7BA011E54C8 AS DateTime))
GO
INSERT [dbo].[Clients] ([ID], [CompanyID], [Name], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [RecordedBy], [DateRecorded]) VALUES (101, 100, N'United Methodist  in Liberia', N'UML', N'+231...', N'+231....', N'info@cabicol.org', N'Monrovia, Liberia', N'www.cabicol.org', N'superadmin', CAST(0x0000A7BA0129FEB8 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 

GO
INSERT [dbo].[Companies] ([ID], [Name], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [RecordedBy], [DateRecorded]) VALUES (100, N'RESEARCH INSTITUTE OF TECHNOLOGICAL ENTREPRENUERS', N'RITE', N'+231776943923', N'+231776943923', N'info@riteliberia.org', N'Monrovia, Liberia', NULL, N'Developer', CAST(0x0000A7BA011E49F5 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[Counties] ON 

GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (1, NULL, N'Bomi', CAST(0x0000A0440084BF56 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (2, NULL, N'Bong', CAST(0x0000A0440084C914 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (3, NULL, N'Gbanpolu', CAST(0x0000A0440084D33D AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (4, NULL, N'Grand Bassa', CAST(0x0000A0440084DD00 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (5, NULL, N'Grand Cape Mount', CAST(0x0000A04400852821 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (6, NULL, N'Grand Gedeh', CAST(0x0000A04400856B14 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (7, NULL, N'Grand Kru', CAST(0x0000A044008573DE AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (8, NULL, N'Lofa', CAST(0x0000A04400857FFC AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (9, NULL, N'Margibi', CAST(0x0000A0440085875C AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (10, NULL, N'Maryland', CAST(0x0000A04400859516 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (11, NULL, N'Montserrado', CAST(0x0000A04400859AE1 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (12, NULL, N'Nimba', CAST(0x0000A04400859FA3 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (13, NULL, N'River Cess', CAST(0x0000A0440085C30E AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (14, NULL, N'River Gee', CAST(0x0000A0440085E477 AS DateTime), N'Developer')
GO
INSERT [dbo].[Counties] ([ID], [Code], [Name], [DateRecorded], [RecordedBy]) VALUES (15, NULL, N'Sinoe', CAST(0x0000A04400863FA0 AS DateTime), N'Developer')
GO
SET IDENTITY_INSERT [dbo].[Counties] OFF
GO
SET IDENTITY_INSERT [dbo].[GenderTypes] ON 

GO
INSERT [dbo].[GenderTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (100, N'Female', N'hfwilson', CAST(0x0000A7BE00BC6209 AS DateTime))
GO
INSERT [dbo].[GenderTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (101, N'Male', N'hfwilson', CAST(0x0000A7BE00BC6B8C AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[GenderTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[InstitationUsers] ON 

GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (102, 100, N'd3d9f87b-8ef6-4cda-b093-4f03bc01c7a4', 1, N'superadmin', CAST(0x0000A7BA011E7EF9 AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (104, 100, N'f3fc189d-6f0c-4bc9-8d88-3dae24577e1a', 1, N'superadmin', CAST(0x0000A7BB011A13B5 AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (105, 105, N'bfa32b34-27ab-4468-9157-9f4888d2dda5', 1, N'superadmin', CAST(0x0000A7BB012100CE AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (107, 100, N'0d02ab69-1c7a-416f-a4cc-981c0e7b57d0', 1, N'superadmin', CAST(0x0000A7BC00188024 AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (108, 100, N'9387e9bc-ab3a-4e20-a73c-b5730fff0c4e', 1, N'superadmin', CAST(0x0000A7BC001E2A2F AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (110, 105, N'f52c77ea-905b-41d5-9264-99e489c2a819', 1, N'superadmin', CAST(0x0000A7BD00A71B38 AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (111, 107, N'dd5f7ca2-fd39-408a-949d-85dc70bdb499', 1, N'superadmin', CAST(0x0000A7BD00AB5E7D AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (112, 107, N'4d68ad4b-d243-4644-aa72-5c72bfcb328d', 1, N'superadmin', CAST(0x0000A7BD00AC0F92 AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (113, 107, N'1a9098a0-2a11-4b83-a559-c9c004542d38', 1, N'superadmin', CAST(0x0000A7BD0113B457 AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (114, 100, N'7645cc65-3b05-497e-a9c2-3e7857b6c085', 1, N'superadmin', CAST(0x0000A7BD01174E5E AS DateTime))
GO
INSERT [dbo].[InstitationUsers] ([ID], [InstitationID], [UserID], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (115, 100, N'c8c18175-403f-4e37-ab45-f8ef8c524291', 1, N'superadmin', CAST(0x0000A7BE00EAE1AE AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[InstitationUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[InstitutionGroups] ON 

GO
INSERT [dbo].[InstitutionGroups] ([ID], [ClientID], [Name], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [RecordedBy], [DateRecorded]) VALUES (100, 100, N'Catholic Archdiocese of Monrovia', N'', N'+231...', N'+231...', N'info@cabicol.org', N'Monrovia, Liberia', NULL, N'Developer', CAST(0x0000A7BA011E5DA6 AS DateTime))
GO
INSERT [dbo].[InstitutionGroups] ([ID], [ClientID], [Name], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [RecordedBy], [DateRecorded]) VALUES (101, 100, N'Catholic Diocese of Gbargna', NULL, N'+231...', N'+231....', N'info@cabicol.org', N'address', N'www.cabicol.org', N'superadmin', CAST(0x0000A7BA01347CBF AS DateTime))
GO
INSERT [dbo].[InstitutionGroups] ([ID], [ClientID], [Name], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [RecordedBy], [DateRecorded]) VALUES (102, 101, N'Methodist Diocese of Monrovia', N'MDM', N'+231...', N'+231....', N'info@cabicol.org', N'Monrovia, Liberia', N'www.cabicol.org', N'superadmin', CAST(0x0000A7BA01355448 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[InstitutionGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[Institutions] ON 

GO
INSERT [dbo].[Institutions] ([ID], [InstitutionGroupID], [InstitutionName], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (100, 100, N'Holy Innocents Parish', N'HIP', N'+231776943923', N'+231776943923', N'info@riteliberia.org', N'Monrovia, Liberia', NULL, 1, N'Developer', CAST(0x0000A7BA011E6D15 AS DateTime))
GO
INSERT [dbo].[Institutions] ([ID], [InstitutionGroupID], [InstitutionName], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (105, 102, N'S. T. Nable', N'STNUM', N'+231...', N'+231....', N'info@cabicol.org', N'Sinkor', N'www.shc.org', 1, N'superadmin', CAST(0x0000A7BA016A4C0C AS DateTime))
GO
INSERT [dbo].[Institutions] ([ID], [InstitutionGroupID], [InstitutionName], [Initial], [OfficePhone], [MobilePhone], [EmailAddress], [Address], [Website], [StatusTypeID], [RecordedBy], [DateRecorded]) VALUES (107, 101, N'St. Martin', N'SM', N'+231...', N'+231....', N'info@cabicol.org', N'address', N'www.cabicol.org', 1, N'superadmin', CAST(0x0000A7BB00DAF2B4 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Institutions] OFF
GO
SET IDENTITY_INSERT [dbo].[MaritalStatusTypes] ON 

GO
INSERT [dbo].[MaritalStatusTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (100, N'Single', N'developer', CAST(0x0000A7BE00B22D9B AS DateTime))
GO
INSERT [dbo].[MaritalStatusTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (101, N'Married', N'developer', CAST(0x0000A7BE00B38A1C AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[MaritalStatusTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Members] ON 

GO
INSERT [dbo].[Members] ([ID], [InstitutionID], [MemberID], [SalutationTypeID], [FirstName], [MiddleName], [LastName], [GenderTypeID], [DateOfBirth], [MaritalStatusTypeID], [ResidentAddress], [EmailAddress], [MobilePhone], [OfficePhone], [NationalityTypeID], [CountyID], [Region], [MemberTypeID], [StatusTypeID], [PhotoPath], [RecordedBy], [DateRecorded]) VALUES (100000000, 100, N'sdad', 100, N'Columbia', N'southern', N'university', 100, CAST(0x00007A6000000000 AS DateTime), 100, N'1347 chew street', N'info@cabicol.org', N'4847882111', N'4847882111', 100, 1, N'PENNSYLVANIA', 100, 1, NULL, N'hfwilson', CAST(0x0000A7BE00E8C260 AS DateTime))
GO
INSERT [dbo].[Members] ([ID], [InstitutionID], [MemberID], [SalutationTypeID], [FirstName], [MiddleName], [LastName], [GenderTypeID], [DateOfBirth], [MaritalStatusTypeID], [ResidentAddress], [EmailAddress], [MobilePhone], [OfficePhone], [NationalityTypeID], [CountyID], [Region], [MemberTypeID], [StatusTypeID], [PhotoPath], [RecordedBy], [DateRecorded]) VALUES (100000001, 100, N'mj9884', 100, N'Columbia', N'southern', N'university', 100, CAST(0x00007A6000000000 AS DateTime), 100, N'P.O. Box 3110', N'info@cabicol.org', N'4847882111', N'4847882111', 100, 3, N'al', 100, 1, NULL, N'hfwilson', CAST(0x0000A7BE00EAE0C9 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberTypes] ON 

GO
INSERT [dbo].[MemberTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (100, N'Registered Member', N'developer', CAST(0x0000A7A400000000 AS DateTime))
GO
INSERT [dbo].[MemberTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (101, N'Approved Member', N'superadmin', CAST(0x0000A7B6008981F4 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[MemberTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[NationalityTypes] ON 

GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (100, N'Afghanistan', N'Afghan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (101, N'Albania', N'Albanian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (102, N'Algeria', N'Algerian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (103, N'Andorra', N'Andorran', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (104, N'Angola', N'Angolan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (105, N'Argentina', N'Argentinian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (106, N'Armenia', N'Armenian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (107, N'Australia', N'Australian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (108, N'Austria', N'Austrian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (109, N'Azerbaijan', N'Azerbaijani', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (110, N'Bahamas', N'Bahamian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (111, N'Bahrain', N'Bahraini', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (112, N'Bangladesh', N'Bangladeshi', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (113, N'Barbados', N'Barbadian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (114, N'Belarus', N'Belarusian or Belarusan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (115, N'Belgium', N'Belgian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (116, N'Belize', N'Belizean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (117, N'Benin', N'Beninese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (118, N'Bhutan', N'Bhutanese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (119, N'Bolivia', N'Bolivian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (120, N'Bosnia-Herzegovina', N'Bosnian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (121, N'Botswana', N'Botswanan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (122, N'Brazil', N'Brazilian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (123, N'Britain', N'British', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (124, N'Brunei', N'Bruneian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (125, N'Bulgaria', N'Bulgarian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (126, N'Burkina', N'Burkinese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (127, N'Burma (official name Myanmar)', N'Burmese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (128, N'Burundi', N'Burundian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (129, N'Cambodia', N'Cambodian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (130, N'Cameroon', N'Cameroonian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (131, N'Canada', N'Canadian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (132, N'Cape Verde Islands', N'Cape Verdean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (133, N'Chad', N'Chadian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (134, N'Chile', N'Chilean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (135, N'China', N'Chinese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (136, N'Colombia', N'Colombian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (137, N'Congo', N'Congolese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (138, N'Costa Rica', N'Costa Rican', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (139, N'Croatia', N'Croat or Croatian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (140, N'Cuba', N'Cuban', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (141, N'Cyprus', N'Cypriot', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (142, N'Czech Republic', N'Czech', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (143, N'Denmark', N'Danish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (144, N'Djibouti', N'Djiboutian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (145, N'Dominica', N'Dominican', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (146, N'Dominican Republic', N'Dominican', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (147, N'Ecuador', N'Ecuadorean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (148, N'Egypt', N'Egyptian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (149, N'El Salvador', N'Salvadorean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (150, N'England', N'English', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (151, N'Eritrea', N'Eritrean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (152, N'Estonia', N'Estonian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (153, N'Ethiopia', N'Ethiopian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (154, N'Fiji', N'Fijian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (155, N'Finland', N'Finnish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (156, N'France', N'French', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (157, N'Gabon', N'Gabonese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (158, N'Gambia, the', N'Gambian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (159, N'Georgia', N'Georgian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (160, N'Germany', N'German', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (161, N'Ghana', N'Ghanaian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (162, N'Greece', N'Greek', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (163, N'Grenada', N'Grenadian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (164, N'Guatemala', N'Guatemalan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (165, N'Guinea', N'Guinean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (166, N'Guyana', N'Guyanese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (167, N'Haiti', N'Haitian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (168, N'Holland (also Netherlands)', N'Dutch', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (169, N'Honduras', N'Honduran', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (170, N'Hungary', N'Hungarian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (171, N'Iceland', N'Icelandic', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (172, N'India', N'Indian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (173, N'Indonesia', N'Indonesian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (174, N'Iran', N'Iranian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (175, N'Iraq', N'Iraqi', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (176, N'Ireland, Republic of', N'Irish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (177, N'Italy', N'Italian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (178, N'Jamaica', N'Jamaican', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (179, N'Japan', N'Japanese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (180, N'Jordan', N'Jordanian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (181, N'Kazakhstan', N'Kazakh', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (182, N'Kenya', N'Kenyan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (183, N'Kuwait', N'Kuwaiti', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (184, N'Laos', N'Laotian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (185, N'Latvia', N'Latvian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (186, N'Lebanon', N'Lebanese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (187, N'Liberia', N'Liberian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (188, N'Libya', N'Libyan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (189, N'Liechtenstein', N'-', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (190, N'Lithuania', N'Lithuanian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (191, N'Luxembourg', N'-', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (192, N'Macedonia', N'Macedonian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (193, N'Madagascar', N'Malagasy or Madagascan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (194, N'Malawi', N'Malawian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (195, N'Malaysia', N'Malaysian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (196, N'Maldives', N'Maldivian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (197, N'Mali', N'Malian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (198, N'Malta', N'Maltese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (199, N'Mauritania', N'Mauritanian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (200, N'Mauritius', N'Mauritian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (201, N'Mexico', N'Mexican', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (202, N'Moldova', N'Moldovan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (203, N'Monaco', N'Monégasque or Monacan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (204, N'Mongolia', N'Mongolian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (205, N'Montenegro', N'Montenegrin', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (206, N'Morocco', N'Moroccan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (207, N'Mozambique', N'Mozambican', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (208, N'Myanmar see Burma', N'-', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (209, N'Namibia', N'Namibian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (210, N'Nepal', N'Nepalese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (211, N'Netherlands, the (see Holland)', N'Dutch', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (212, N'Nicaragua', N'Nicaraguan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (213, N'Niger', N'Nigerien', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (214, N'Nigeria', N'Nigerian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (215, N'North Korea', N'North Korean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (216, N'Norway', N'Norwegian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (217, N'Oman', N'Omani', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (218, N'Pakistan', N'Pakistani', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (219, N'Panama', N'Panamanian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (220, N'Papua New Guinea', N'Papua New Guinean or Guinean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (221, N'Paraguay', N'Paraguayan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (222, N'Peru', N'Peruvian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (223, N'the Philippines', N'Philippine', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (224, N'Poland', N'Polish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (225, N'Portugal', N'Portuguese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (226, N'Qatar', N'Qatari', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (227, N'Romania', N'Romanian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (228, N'Russia', N'Russian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (229, N'Rwanda', N'Rwandan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (230, N'Saudi Arabia', N'Saudi Arabian or Saudi', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (231, N'Scotland', N'Scottish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (232, N'Senegal', N'Senegalese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (233, N'Serbia', N'Serb or Serbian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (234, N'Seychelles, the', N'Seychellois', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (235, N'Sierra Leone', N'Sierra Leonian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (236, N'Singapore', N'Singaporean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (237, N'Slovakia', N'Slovak', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (238, N'Slovenia', N'Slovene or Slovenian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (239, N'Solomon Islands', N'-', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (240, N'Somalia', N'Somali', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (241, N'South Africa', N'South African', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (242, N'South Korea', N'South Korean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (243, N'Spain', N'Spanish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (244, N'Sri Lanka', N'Sri Lankan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (245, N'Sudan', N'Sudanese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (246, N'Suriname', N'Surinamese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (247, N'Swaziland', N'Swazi', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (248, N'Sweden', N'Swedish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (249, N'Switzerland', N'Swiss', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (250, N'Syria', N'Syrian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (251, N'Taiwan', N'Taiwanese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (252, N'Tajikistan', N'Tajik or Tadjik', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (253, N'Tanzania', N'Tanzanian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (254, N'Thailand', N'Thai', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (255, N'Togo', N'Togolese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (256, N'Trinidad and Tobago', N'Trinidadian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (257, N'', N'Tobagan/Tobagonian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (258, N'Tunisia', N'Tunisian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (259, N'Turkey', N'Turkish', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (260, N'Turkmenistan', N'Turkmen or Turkoman', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (261, N'Tuvalu', N'Tuvaluan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (262, N'Uganda', N'Ugandan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (263, N'Ukraine', N'Ukrainian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (264, N'Uruguay', N'Uruguayan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (265, N'Uzbekistan', N'Uzbek', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (266, N'Vanuatu', N'Vanuatuan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (267, N'Vatican City', N'-', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (268, N'Venezuela', N'Venezuelan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (269, N'Vietnam', N'Vietnamese', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (270, N'Wales', N'Welsh', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (271, N'Western Samoa', N'Western Samoan', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (272, N'Yemen', N'Yemeni', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (273, N'Yugoslavia', N'Yugoslav', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (274, N'Zaire', N'Zaïrean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (275, N'Zambia', N'Zambian', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
INSERT [dbo].[NationalityTypes] ([ID], [TypeName], [Value], [RecordedBy], [DateRecorded]) VALUES (276, N'Zimbabwe', N'Zimbabwean', N'developer', CAST(0x0000A7BE00AE5BDA AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[NationalityTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[SalutationTypes] ON 

GO
INSERT [dbo].[SalutationTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (100, N'Mr.', N'hfwilson', CAST(0x0000A7BE00BA61C1 AS DateTime))
GO
INSERT [dbo].[SalutationTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (101, N'Mrs.', N'hfwilson', CAST(0x0000A7BE00BA6DC8 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[SalutationTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[StatusTypes] ON 

GO
INSERT [dbo].[StatusTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (1, N'Active', N'admin', CAST(0x0000A7B500000000 AS DateTime))
GO
INSERT [dbo].[StatusTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (2, N'Inactive', N'hfwilson', CAST(0x0000A7BE00B7DE07 AS DateTime))
GO
INSERT [dbo].[StatusTypes] ([ID], [TypeName], [RecordedBy], [DateRecorded]) VALUES (3, N'suspended', N'hfwilson', CAST(0x0000A7BE00B7ECC4 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[StatusTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemVariables] ON 

GO
INSERT [dbo].[SystemVariables] ([ID], [Name], [Value], [RecordedBy], [DateRecorded]) VALUES (100, N'Default Password', N'P@55w0rd', N'superadmin', CAST(0x0000A7BB01178058 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[SystemVariables] OFF
GO
SET IDENTITY_INSERT [dbo].[Trackers] ON 

GO
INSERT [dbo].[Trackers] ([ID], [UserName], [IpAddress], [AreaAccessed], [ActionDate], [PreviousValues]) VALUES (1, N'superadmin', N'::1', N'/MemberTypes/Edit/101', CAST(0x0000A7B60097815D AS DateTime), N'Modification: ID : 101 ; TypeName : Approved Member hh ; RecordedBy : superadmin ; DateRecorded : 7/20/2017 8:20:39 AM ; ')
GO
INSERT [dbo].[Trackers] ([ID], [UserName], [IpAddress], [AreaAccessed], [ActionDate], [PreviousValues]) VALUES (2, N'superadmin', N'::1', N'/MemberTypes/Delete/103', CAST(0x0000A7B60097BFBD AS DateTime), N'Delete: ID : 103 ; TypeName : ks kd lsk kd  ; RecordedBy : superadmin ; DateRecorded : 7/20/2017 9:12:22 AM ; ')
GO
SET IDENTITY_INSERT [dbo].[Trackers] OFF
GO
ALTER TABLE [dbo].[Counties] ADD  CONSTRAINT [DF_Counties_DateRecorded]  DEFAULT (getdate()) FOR [DateRecorded]
GO
ALTER TABLE [dbo].[Counties] ADD  CONSTRAINT [DF_Counties_RecordedBy]  DEFAULT (N'Developer') FOR [RecordedBy]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[InstitationUsers]  WITH CHECK ADD  CONSTRAINT [FK_InstitationUsers_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstitationUsers] CHECK CONSTRAINT [FK_InstitationUsers_AspNetUsers]
GO
ALTER TABLE [dbo].[InstitationUsers]  WITH CHECK ADD  CONSTRAINT [FK_InstitationUsers_Institutions] FOREIGN KEY([InstitationID])
REFERENCES [dbo].[Institutions] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstitationUsers] CHECK CONSTRAINT [FK_InstitationUsers_Institutions]
GO
ALTER TABLE [dbo].[InstitutionGroups]  WITH CHECK ADD  CONSTRAINT [FK_InstitutionGroups_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstitutionGroups] CHECK CONSTRAINT [FK_InstitutionGroups_Clients]
GO
ALTER TABLE [dbo].[Institutions]  WITH CHECK ADD  CONSTRAINT [FK_Institutions_InstitutionGroups] FOREIGN KEY([InstitutionGroupID])
REFERENCES [dbo].[InstitutionGroups] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Institutions] CHECK CONSTRAINT [FK_Institutions_InstitutionGroups]
GO
ALTER TABLE [dbo].[Institutions]  WITH CHECK ADD  CONSTRAINT [FK_Institutions_StatusTypes] FOREIGN KEY([StatusTypeID])
REFERENCES [dbo].[StatusTypes] ([ID])
GO
ALTER TABLE [dbo].[Institutions] CHECK CONSTRAINT [FK_Institutions_StatusTypes]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Counties] FOREIGN KEY([CountyID])
REFERENCES [dbo].[Counties] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Counties]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_GenderTypes] FOREIGN KEY([GenderTypeID])
REFERENCES [dbo].[GenderTypes] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_GenderTypes]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Institutions] FOREIGN KEY([InstitutionID])
REFERENCES [dbo].[Institutions] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Institutions]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_MaritalStatusTypes] FOREIGN KEY([MaritalStatusTypeID])
REFERENCES [dbo].[MaritalStatusTypes] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_MaritalStatusTypes]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_MemberTypes] FOREIGN KEY([MemberTypeID])
REFERENCES [dbo].[MemberTypes] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_MemberTypes]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_NationalityTypes] FOREIGN KEY([NationalityTypeID])
REFERENCES [dbo].[NationalityTypes] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_NationalityTypes]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_SalutationTypes] FOREIGN KEY([SalutationTypeID])
REFERENCES [dbo].[SalutationTypes] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_SalutationTypes]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_StatusTypes] FOREIGN KEY([StatusTypeID])
REFERENCES [dbo].[StatusTypes] ([ID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_StatusTypes]
GO
