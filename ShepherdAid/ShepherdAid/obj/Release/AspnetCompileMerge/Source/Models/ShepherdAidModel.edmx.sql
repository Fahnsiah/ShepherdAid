
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/28/2017 14:14:00
-- Generated from EDMX file: W:\CLIENTS\CHURCH\2017\ShepherdAid\ShepherdAid\Models\ShepherdAidModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ShepherdAidDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AspNetGroupRoles_AspNetGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetGroupRoles] DROP CONSTRAINT [FK_AspNetGroupRoles_AspNetGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetGroupRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetGroupRoles] DROP CONSTRAINT [FK_AspNetGroupRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitationUsers_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstitationUsers] DROP CONSTRAINT [FK_InstitationUsers_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitationUsers_Institutions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstitationUsers] DROP CONSTRAINT [FK_InstitationUsers_Institutions];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionGroups_Clients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstitutionGroups] DROP CONSTRAINT [FK_InstitutionGroups_Clients];
GO
IF OBJECT_ID(N'[dbo].[FK_Institutions_InstitutionGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Institutions] DROP CONSTRAINT [FK_Institutions_InstitutionGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_Institutions_StatusTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Institutions] DROP CONSTRAINT [FK_Institutions_StatusTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberDocuments_DocumentTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberDocuments] DROP CONSTRAINT [FK_MemberDocuments_DocumentTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberDocuments_Members]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberDocuments] DROP CONSTRAINT [FK_MemberDocuments_Members];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_Counties]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_Counties];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_GenderTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_GenderTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_Institutions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_Institutions];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_MaritalStatusTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_MaritalStatusTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_MemberTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_MemberTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_NationalityTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_NationalityTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_SalutationTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_SalutationTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_StatusTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_StatusTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_MembersObligations_Members]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MembersObligations] DROP CONSTRAINT [FK_MembersObligations_Members];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberTypes_Institutions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberTypes] DROP CONSTRAINT [FK_MemberTypes_Institutions];
GO
IF OBJECT_ID(N'[dbo].[FK_Obligations_CurrencyTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Obligations] DROP CONSTRAINT [FK_Obligations_CurrencyTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Obligations_ObligationStatusTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Obligations] DROP CONSTRAINT [FK_Obligations_ObligationStatusTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Obligations_ObligationTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Obligations] DROP CONSTRAINT [FK_Obligations_ObligationTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Obligations_RecurranceTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Obligations] DROP CONSTRAINT [FK_Obligations_RecurranceTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_ObligationTypes_Institutions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObligationTypes] DROP CONSTRAINT [FK_ObligationTypes_Institutions];
GO
IF OBJECT_ID(N'[dbo].[FK_SalutationTypes_Institutions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalutationTypes] DROP CONSTRAINT [FK_SalutationTypes_Institutions];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetGroupRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetGroupRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetGroups];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Counties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Counties];
GO
IF OBJECT_ID(N'[dbo].[CurrencyTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CurrencyTypes];
GO
IF OBJECT_ID(N'[dbo].[DocumentTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentTypes];
GO
IF OBJECT_ID(N'[dbo].[GenderTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GenderTypes];
GO
IF OBJECT_ID(N'[dbo].[InstitationUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstitationUsers];
GO
IF OBJECT_ID(N'[dbo].[InstitutionGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstitutionGroups];
GO
IF OBJECT_ID(N'[dbo].[Institutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Institutions];
GO
IF OBJECT_ID(N'[dbo].[MaritalStatusTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MaritalStatusTypes];
GO
IF OBJECT_ID(N'[dbo].[MemberDocuments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberDocuments];
GO
IF OBJECT_ID(N'[dbo].[Members]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Members];
GO
IF OBJECT_ID(N'[dbo].[MembersObligations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MembersObligations];
GO
IF OBJECT_ID(N'[dbo].[MemberTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberTypes];
GO
IF OBJECT_ID(N'[dbo].[NationalityTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NationalityTypes];
GO
IF OBJECT_ID(N'[dbo].[Obligations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Obligations];
GO
IF OBJECT_ID(N'[dbo].[ObligationStatusTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObligationStatusTypes];
GO
IF OBJECT_ID(N'[dbo].[ObligationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObligationTypes];
GO
IF OBJECT_ID(N'[dbo].[RecurranceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecurranceTypes];
GO
IF OBJECT_ID(N'[dbo].[SalutationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalutationTypes];
GO
IF OBJECT_ID(N'[dbo].[StatusTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatusTypes];
GO
IF OBJECT_ID(N'[dbo].[SystemVariables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemVariables];
GO
IF OBJECT_ID(N'[dbo].[Trackers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trackers];
GO
IF OBJECT_ID(N'[ShepherdAidDBModelStoreContainer].[vwSystemUsers]', 'U') IS NOT NULL
    DROP TABLE [ShepherdAidDBModelStoreContainer].[vwSystemUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'InstitationUsers'
CREATE TABLE [dbo].[InstitationUsers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InstitationID] int  NOT NULL,
    [UserID] nvarchar(128)  NOT NULL,
    [StatusTypeID] int  NOT NULL,
    [RecordedBy] nvarchar(50)  NULL,
    [DateRecorded] datetime  NULL
);
GO

-- Creating table 'MemberTypes'
CREATE TABLE [dbo].[MemberTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(32)  NOT NULL,
    [RecordedBy] nvarchar(32)  NOT NULL,
    [DateRecorded] datetime  NOT NULL,
    [InstitutionID] int  NULL
);
GO

-- Creating table 'Trackers'
CREATE TABLE [dbo].[Trackers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [IpAddress] nvarchar(50)  NOT NULL,
    [AreaAccessed] nvarchar(100)  NOT NULL,
    [ActionDate] datetime  NOT NULL,
    [PreviousValues] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CompanyID] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Initial] nvarchar(15)  NULL,
    [OfficePhone] nvarchar(50)  NOT NULL,
    [MobilePhone] nvarchar(50)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Website] nvarchar(50)  NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Initial] nvarchar(15)  NULL,
    [OfficePhone] nvarchar(50)  NOT NULL,
    [MobilePhone] nvarchar(50)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Website] nvarchar(50)  NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'InstitutionGroups'
CREATE TABLE [dbo].[InstitutionGroups] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ClientID] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Initial] nvarchar(15)  NULL,
    [OfficePhone] nvarchar(50)  NOT NULL,
    [MobilePhone] nvarchar(50)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Website] nvarchar(50)  NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'SystemVariables'
CREATE TABLE [dbo].[SystemVariables] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(25)  NOT NULL,
    [Value] nvarchar(50)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'vwSystemUsers'
CREATE TABLE [dbo].[vwSystemUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(75)  NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(75)  NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [ClientID] int  NOT NULL,
    [InstitutionGroupID] int  NOT NULL,
    [InstitationID] int  NOT NULL
);
GO

-- Creating table 'GenderTypes'
CREATE TABLE [dbo].[GenderTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(32)  NOT NULL,
    [RecordedBy] nvarchar(32)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'MaritalStatusTypes'
CREATE TABLE [dbo].[MaritalStatusTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(32)  NOT NULL,
    [RecordedBy] nvarchar(32)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'SalutationTypes'
CREATE TABLE [dbo].[SalutationTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(32)  NOT NULL,
    [RecordedBy] nvarchar(32)  NOT NULL,
    [DateRecorded] datetime  NOT NULL,
    [InstitutionID] int  NOT NULL
);
GO

-- Creating table 'NationalityTypes'
CREATE TABLE [dbo].[NationalityTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(32)  NOT NULL,
    [Value] nvarchar(50)  NULL,
    [RecordedBy] nvarchar(32)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'Counties'
CREATE TABLE [dbo].[Counties] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(5)  NULL,
    [Name] nvarchar(100)  NOT NULL,
    [DateRecorded] datetime  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'DocumentTypes'
CREATE TABLE [dbo].[DocumentTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(32)  NOT NULL,
    [RecordedBy] nvarchar(32)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'MemberDocuments'
CREATE TABLE [dbo].[MemberDocuments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [DocumentTypeID] int  NOT NULL,
    [DocumentPath] nvarchar(250)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'Members'
CREATE TABLE [dbo].[Members] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InstitutionID] int  NOT NULL,
    [MemberID] nvarchar(20)  NOT NULL,
    [SalutationTypeID] int  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [GenderTypeID] int  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [MaritalStatusTypeID] int  NOT NULL,
    [ResidentAddress] nvarchar(150)  NOT NULL,
    [EmailAddress] nvarchar(75)  NULL,
    [MobilePhone] nvarchar(15)  NOT NULL,
    [OfficePhone] nvarchar(15)  NULL,
    [NationalityTypeID] int  NOT NULL,
    [CountyID] int  NULL,
    [Region] nvarchar(50)  NULL,
    [MemberTypeID] int  NOT NULL,
    [StatusTypeID] int  NOT NULL,
    [PhotoPath] nvarchar(250)  NULL,
    [UserID] nvarchar(1280)  NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'ObligationTypes'
CREATE TABLE [dbo].[ObligationTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ObligationName] nvarchar(50)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL,
    [InstitutionID] int  NOT NULL
);
GO

-- Creating table 'StatusTypes'
CREATE TABLE [dbo].[StatusTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StatusName] nvarchar(50)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'MembersObligations'
CREATE TABLE [dbo].[MembersObligations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [ObligationID] int  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'CurrencyTypes'
CREATE TABLE [dbo].[CurrencyTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CurrencyName] nvarchar(25)  NOT NULL,
    [ShortName] nvarchar(10)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'Obligations'
CREATE TABLE [dbo].[Obligations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ObligationTypeID] int  NOT NULL,
    [CurrencyTypeID] int  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [DateStarted] datetime  NOT NULL,
    [DateAdjusted] datetime  NOT NULL,
    [AdjustedBalance] decimal(19,4)  NOT NULL,
    [RecurranceTypeID] int  NOT NULL,
    [ObligationStatusTypeID] int  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'ObligationStatusTypes'
CREATE TABLE [dbo].[ObligationStatusTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StatusName] nvarchar(50)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'RecurranceTypes'
CREATE TABLE [dbo].[RecurranceTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Recurrance] nvarchar(50)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'AspNetGroups'
CREATE TABLE [dbo].[AspNetGroups] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'AspNetGroupRoles'
CREATE TABLE [dbo].[AspNetGroupRoles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GroupID] int  NOT NULL,
    [RoleID] nvarchar(128)  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'Institutions'
CREATE TABLE [dbo].[Institutions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InstitutionGroupID] int  NOT NULL,
    [InstitutionName] nvarchar(50)  NOT NULL,
    [Initial] nvarchar(50)  NULL,
    [OfficePhone] nvarchar(50)  NOT NULL,
    [MobilePhone] nvarchar(50)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Website] nvarchar(50)  NULL,
    [StatusTypeID] int  NOT NULL,
    [RecordedBy] nvarchar(50)  NOT NULL,
    [DateRecorded] datetime  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [FirstName] nvarchar(75)  NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(75)  NULL,
    [LoginHint] nvarchar(50)  NULL,
    [UserGroupID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'InstitationUsers'
ALTER TABLE [dbo].[InstitationUsers]
ADD CONSTRAINT [PK_InstitationUsers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MemberTypes'
ALTER TABLE [dbo].[MemberTypes]
ADD CONSTRAINT [PK_MemberTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Trackers'
ALTER TABLE [dbo].[Trackers]
ADD CONSTRAINT [PK_Trackers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'InstitutionGroups'
ALTER TABLE [dbo].[InstitutionGroups]
ADD CONSTRAINT [PK_InstitutionGroups]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SystemVariables'
ALTER TABLE [dbo].[SystemVariables]
ADD CONSTRAINT [PK_SystemVariables]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id], [UserName], [ClientID], [InstitutionGroupID], [InstitationID] in table 'vwSystemUsers'
ALTER TABLE [dbo].[vwSystemUsers]
ADD CONSTRAINT [PK_vwSystemUsers]
    PRIMARY KEY CLUSTERED ([Id], [UserName], [ClientID], [InstitutionGroupID], [InstitationID] ASC);
GO

-- Creating primary key on [ID] in table 'GenderTypes'
ALTER TABLE [dbo].[GenderTypes]
ADD CONSTRAINT [PK_GenderTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MaritalStatusTypes'
ALTER TABLE [dbo].[MaritalStatusTypes]
ADD CONSTRAINT [PK_MaritalStatusTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SalutationTypes'
ALTER TABLE [dbo].[SalutationTypes]
ADD CONSTRAINT [PK_SalutationTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'NationalityTypes'
ALTER TABLE [dbo].[NationalityTypes]
ADD CONSTRAINT [PK_NationalityTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Counties'
ALTER TABLE [dbo].[Counties]
ADD CONSTRAINT [PK_Counties]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'DocumentTypes'
ALTER TABLE [dbo].[DocumentTypes]
ADD CONSTRAINT [PK_DocumentTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MemberDocuments'
ALTER TABLE [dbo].[MemberDocuments]
ADD CONSTRAINT [PK_MemberDocuments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [PK_Members]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ObligationTypes'
ALTER TABLE [dbo].[ObligationTypes]
ADD CONSTRAINT [PK_ObligationTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'StatusTypes'
ALTER TABLE [dbo].[StatusTypes]
ADD CONSTRAINT [PK_StatusTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MembersObligations'
ALTER TABLE [dbo].[MembersObligations]
ADD CONSTRAINT [PK_MembersObligations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CurrencyTypes'
ALTER TABLE [dbo].[CurrencyTypes]
ADD CONSTRAINT [PK_CurrencyTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Obligations'
ALTER TABLE [dbo].[Obligations]
ADD CONSTRAINT [PK_Obligations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ObligationStatusTypes'
ALTER TABLE [dbo].[ObligationStatusTypes]
ADD CONSTRAINT [PK_ObligationStatusTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RecurranceTypes'
ALTER TABLE [dbo].[RecurranceTypes]
ADD CONSTRAINT [PK_RecurranceTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AspNetGroups'
ALTER TABLE [dbo].[AspNetGroups]
ADD CONSTRAINT [PK_AspNetGroups]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AspNetGroupRoles'
ALTER TABLE [dbo].[AspNetGroupRoles]
ADD CONSTRAINT [PK_AspNetGroupRoles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Institutions'
ALTER TABLE [dbo].[Institutions]
ADD CONSTRAINT [PK_Institutions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CompanyID] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [FK_Clients_Companies]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[Companies]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Clients_Companies'
CREATE INDEX [IX_FK_Clients_Companies]
ON [dbo].[Clients]
    ([CompanyID]);
GO

-- Creating foreign key on [ClientID] in table 'InstitutionGroups'
ALTER TABLE [dbo].[InstitutionGroups]
ADD CONSTRAINT [FK_InstitutionGroups_Clients]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[Clients]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionGroups_Clients'
CREATE INDEX [IX_FK_InstitutionGroups_Clients]
ON [dbo].[InstitutionGroups]
    ([ClientID]);
GO

-- Creating foreign key on [DocumentTypeID] in table 'MemberDocuments'
ALTER TABLE [dbo].[MemberDocuments]
ADD CONSTRAINT [FK_MemberDocuments_DocumentTypes]
    FOREIGN KEY ([DocumentTypeID])
    REFERENCES [dbo].[DocumentTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberDocuments_DocumentTypes'
CREATE INDEX [IX_FK_MemberDocuments_DocumentTypes]
ON [dbo].[MemberDocuments]
    ([DocumentTypeID]);
GO

-- Creating foreign key on [CountyID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_Counties]
    FOREIGN KEY ([CountyID])
    REFERENCES [dbo].[Counties]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_Counties'
CREATE INDEX [IX_FK_Members_Counties]
ON [dbo].[Members]
    ([CountyID]);
GO

-- Creating foreign key on [GenderTypeID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_GenderTypes]
    FOREIGN KEY ([GenderTypeID])
    REFERENCES [dbo].[GenderTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_GenderTypes'
CREATE INDEX [IX_FK_Members_GenderTypes]
ON [dbo].[Members]
    ([GenderTypeID]);
GO

-- Creating foreign key on [MaritalStatusTypeID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_MaritalStatusTypes]
    FOREIGN KEY ([MaritalStatusTypeID])
    REFERENCES [dbo].[MaritalStatusTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_MaritalStatusTypes'
CREATE INDEX [IX_FK_Members_MaritalStatusTypes]
ON [dbo].[Members]
    ([MaritalStatusTypeID]);
GO

-- Creating foreign key on [MemberID] in table 'MemberDocuments'
ALTER TABLE [dbo].[MemberDocuments]
ADD CONSTRAINT [FK_MemberDocuments_Members]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberDocuments_Members'
CREATE INDEX [IX_FK_MemberDocuments_Members]
ON [dbo].[MemberDocuments]
    ([MemberID]);
GO

-- Creating foreign key on [MemberTypeID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_MemberTypes]
    FOREIGN KEY ([MemberTypeID])
    REFERENCES [dbo].[MemberTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_MemberTypes'
CREATE INDEX [IX_FK_Members_MemberTypes]
ON [dbo].[Members]
    ([MemberTypeID]);
GO

-- Creating foreign key on [NationalityTypeID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_NationalityTypes]
    FOREIGN KEY ([NationalityTypeID])
    REFERENCES [dbo].[NationalityTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_NationalityTypes'
CREATE INDEX [IX_FK_Members_NationalityTypes]
ON [dbo].[Members]
    ([NationalityTypeID]);
GO

-- Creating foreign key on [SalutationTypeID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_SalutationTypes]
    FOREIGN KEY ([SalutationTypeID])
    REFERENCES [dbo].[SalutationTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_SalutationTypes'
CREATE INDEX [IX_FK_Members_SalutationTypes]
ON [dbo].[Members]
    ([SalutationTypeID]);
GO

-- Creating foreign key on [StatusTypeID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_StatusTypes]
    FOREIGN KEY ([StatusTypeID])
    REFERENCES [dbo].[StatusTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_StatusTypes'
CREATE INDEX [IX_FK_Members_StatusTypes]
ON [dbo].[Members]
    ([StatusTypeID]);
GO

-- Creating foreign key on [MemberID] in table 'MembersObligations'
ALTER TABLE [dbo].[MembersObligations]
ADD CONSTRAINT [FK_MembersObligations_Members]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembersObligations_Members'
CREATE INDEX [IX_FK_MembersObligations_Members]
ON [dbo].[MembersObligations]
    ([MemberID]);
GO

-- Creating foreign key on [CurrencyTypeID] in table 'Obligations'
ALTER TABLE [dbo].[Obligations]
ADD CONSTRAINT [FK_Obligations_CurrencyTypes]
    FOREIGN KEY ([CurrencyTypeID])
    REFERENCES [dbo].[CurrencyTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Obligations_CurrencyTypes'
CREATE INDEX [IX_FK_Obligations_CurrencyTypes]
ON [dbo].[Obligations]
    ([CurrencyTypeID]);
GO

-- Creating foreign key on [ObligationStatusTypeID] in table 'Obligations'
ALTER TABLE [dbo].[Obligations]
ADD CONSTRAINT [FK_Obligations_ObligationStatusTypes]
    FOREIGN KEY ([ObligationStatusTypeID])
    REFERENCES [dbo].[ObligationStatusTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Obligations_ObligationStatusTypes'
CREATE INDEX [IX_FK_Obligations_ObligationStatusTypes]
ON [dbo].[Obligations]
    ([ObligationStatusTypeID]);
GO

-- Creating foreign key on [ObligationTypeID] in table 'Obligations'
ALTER TABLE [dbo].[Obligations]
ADD CONSTRAINT [FK_Obligations_ObligationTypes]
    FOREIGN KEY ([ObligationTypeID])
    REFERENCES [dbo].[ObligationTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Obligations_ObligationTypes'
CREATE INDEX [IX_FK_Obligations_ObligationTypes]
ON [dbo].[Obligations]
    ([ObligationTypeID]);
GO

-- Creating foreign key on [RecurranceTypeID] in table 'Obligations'
ALTER TABLE [dbo].[Obligations]
ADD CONSTRAINT [FK_Obligations_RecurranceTypes]
    FOREIGN KEY ([RecurranceTypeID])
    REFERENCES [dbo].[RecurranceTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Obligations_RecurranceTypes'
CREATE INDEX [IX_FK_Obligations_RecurranceTypes]
ON [dbo].[Obligations]
    ([RecurranceTypeID]);
GO

-- Creating foreign key on [GroupID] in table 'AspNetGroupRoles'
ALTER TABLE [dbo].[AspNetGroupRoles]
ADD CONSTRAINT [FK_AspNetGroupRoles_AspNetGroups]
    FOREIGN KEY ([GroupID])
    REFERENCES [dbo].[AspNetGroups]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetGroupRoles_AspNetGroups'
CREATE INDEX [IX_FK_AspNetGroupRoles_AspNetGroups]
ON [dbo].[AspNetGroupRoles]
    ([GroupID]);
GO

-- Creating foreign key on [RoleID] in table 'AspNetGroupRoles'
ALTER TABLE [dbo].[AspNetGroupRoles]
ADD CONSTRAINT [FK_AspNetGroupRoles_AspNetRoles]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetGroupRoles_AspNetRoles'
CREATE INDEX [IX_FK_AspNetGroupRoles_AspNetRoles]
ON [dbo].[AspNetGroupRoles]
    ([RoleID]);
GO

-- Creating foreign key on [InstitationID] in table 'InstitationUsers'
ALTER TABLE [dbo].[InstitationUsers]
ADD CONSTRAINT [FK_InstitationUsers_Institutions]
    FOREIGN KEY ([InstitationID])
    REFERENCES [dbo].[Institutions]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitationUsers_Institutions'
CREATE INDEX [IX_FK_InstitationUsers_Institutions]
ON [dbo].[InstitationUsers]
    ([InstitationID]);
GO

-- Creating foreign key on [InstitutionGroupID] in table 'Institutions'
ALTER TABLE [dbo].[Institutions]
ADD CONSTRAINT [FK_Institutions_InstitutionGroups]
    FOREIGN KEY ([InstitutionGroupID])
    REFERENCES [dbo].[InstitutionGroups]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Institutions_InstitutionGroups'
CREATE INDEX [IX_FK_Institutions_InstitutionGroups]
ON [dbo].[Institutions]
    ([InstitutionGroupID]);
GO

-- Creating foreign key on [StatusTypeID] in table 'Institutions'
ALTER TABLE [dbo].[Institutions]
ADD CONSTRAINT [FK_Institutions_StatusTypes]
    FOREIGN KEY ([StatusTypeID])
    REFERENCES [dbo].[StatusTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Institutions_StatusTypes'
CREATE INDEX [IX_FK_Institutions_StatusTypes]
ON [dbo].[Institutions]
    ([StatusTypeID]);
GO

-- Creating foreign key on [InstitutionID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_Institutions]
    FOREIGN KEY ([InstitutionID])
    REFERENCES [dbo].[Institutions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_Institutions'
CREATE INDEX [IX_FK_Members_Institutions]
ON [dbo].[Members]
    ([InstitutionID]);
GO

-- Creating foreign key on [InstitutionID] in table 'MemberTypes'
ALTER TABLE [dbo].[MemberTypes]
ADD CONSTRAINT [FK_MemberTypes_Institutions]
    FOREIGN KEY ([InstitutionID])
    REFERENCES [dbo].[Institutions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberTypes_Institutions'
CREATE INDEX [IX_FK_MemberTypes_Institutions]
ON [dbo].[MemberTypes]
    ([InstitutionID]);
GO

-- Creating foreign key on [InstitutionID] in table 'ObligationTypes'
ALTER TABLE [dbo].[ObligationTypes]
ADD CONSTRAINT [FK_ObligationTypes_Institutions]
    FOREIGN KEY ([InstitutionID])
    REFERENCES [dbo].[Institutions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ObligationTypes_Institutions'
CREATE INDEX [IX_FK_ObligationTypes_Institutions]
ON [dbo].[ObligationTypes]
    ([InstitutionID]);
GO

-- Creating foreign key on [InstitutionID] in table 'SalutationTypes'
ALTER TABLE [dbo].[SalutationTypes]
ADD CONSTRAINT [FK_SalutationTypes_Institutions]
    FOREIGN KEY ([InstitutionID])
    REFERENCES [dbo].[Institutions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalutationTypes_Institutions'
CREATE INDEX [IX_FK_SalutationTypes_Institutions]
ON [dbo].[SalutationTypes]
    ([InstitutionID]);
GO

-- Creating foreign key on [UserID] in table 'InstitationUsers'
ALTER TABLE [dbo].[InstitationUsers]
ADD CONSTRAINT [FK_InstitationUsers_AspNetUsers]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitationUsers_AspNetUsers'
CREATE INDEX [IX_FK_InstitationUsers_AspNetUsers]
ON [dbo].[InstitationUsers]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------