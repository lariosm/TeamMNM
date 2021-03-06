-- Registered users table
CREATE TABLE [dbo].[SUPUsers]
(
    [Id]            INT IDENTITY (1,1)      NOT NULL,
    [FirstName]     NVARCHAR(100)           NOT NULL,
    [LastName]      NVARCHAR(100)           NOT NULL,
    [DateOfBirth]   DATE                    NOT NULL,
    [StreetAddress] NVARCHAR(128)           NOT NULL,
    [CityAddress]   NVARCHAR(128)           NOT NULL,
    [StateAddress]  NVARCHAR(128)           NOT NULL,
    [ZipCode]       NVARCHAR(128)           NOT NULL,
	[Lat]			FLOAT					NOT NULL,
	[Lng]			FLOAT					NOT NULL,
    [TimeStamp]     DATETIME                NOT NULL,
    [NetUserId]     NVARCHAR (128)          NULL

    CONSTRAINT [PK_dbo.SUPUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Item listings table
CREATE TABLE [dbo].[SUPItems]
(
    [Id]            INT IDENTITY (1,1)      NOT NULL,
    [ItemName]      NVARCHAR(100)           NOT NULL,
    [Description]   NVARCHAR(MAX)           NOT NULL,
    [TimeStamp]     DATETIME                NOT NULL,
    [IsAvailable]   BIT                     NOT NULL,
	[DailyPrice]	DECIMAL(38,2)			NOT NULL,
	[Lat]			FLOAT					NOT NULL,
	[Lng]			FLOAT					NOT NULL,
    [OwnerID]       INT FOREIGN KEY REFERENCES SUPUsers(Id) NOT NULL

	CONSTRAINT [PK_dbo.SUPItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Images table for either users or item listings
CREATE TABLE [dbo].[SUPImages]
(
	[Id]		INT IDENTITY(1,1)						NOT NULL,
	[Filename]	NVARCHAR(100)							NOT NULL,
	[Input]		VARBINARY(MAX)							NOT NULL,
	[ItemID]	INT FOREIGN KEY REFERENCES SUPItems(Id)	NULL,
	[UserID]	INT FOREIGN KEY REFERENCES SUPUsers(Id) NULL

	CONSTRAINT [PK_dbo.SUPImages] PRIMARY KEY CLUSTERED ([Id] Asc)
);

-- Item transactions table
CREATE TABLE [dbo].[SUPTransactions]
(
    [Id]            INT IDENTITY (1,1)      NOT NULL,
	[StartDate]     DATE	                NOT NULL,
	[EndDate]		DATE	                NOT NULL,
    [TimeStamp]     DATETIME                NOT NULL,
	[TotalPrice]	DECIMAL(38,2)					NOT NULL,
    [RenterID]      INT FOREIGN KEY REFERENCES SUPUsers(Id) NOT NULL,
	[OwnerID]		INT FOREIGN KEY REFERENCES SUPUsers(Id)	NOT NULL,
	[ItemID]		INT FOREIGN KEY REFERENCES SUPItems(Id)	NOT NULL

	CONSTRAINT [PK_dbo.SUPTransactions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Item requests table
-- NOTE: As of 2 June 2019, this table is DEPRECATED and scheduled for removal.
CREATE TABLE [dbo].[SUPRequests]
(
    [Id]            INT IDENTITY (1,1)      NOT NULL,
	[StartDate]     DATE	                NOT NULL,
	[EndDate]		DATE	                NOT NULL,
    [TimeStamp]     DATETIME                NOT NULL,
    [RenterID]      INT FOREIGN KEY REFERENCES SUPUsers(Id) NOT NULL,
	[ItemID]		INT FOREIGN KEY REFERENCES SUPItems(Id)	NOT NULL

	CONSTRAINT [PK_dbo.SUPRequests] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- User reviews table
CREATE TABLE [dbo].[SUPUserReviews]
(
	[Id]					INT IDENTITY (1,1)							NOT NULL,
	[Details]				NVARCHAR(MAX)								NOT NULL,
	[Rating]				INT											NOT NULL,
	[Timestamp]				DATETIME									NOT NULL,
	[UserDoingReviewID]		INT FOREIGN KEY REFERENCES SUPUsers(Id)		NOT NULL,
	[UserBeingReviewedID]	INT FOREIGN KEY REFERENCES SUPUsers(Id)		NOT NULL

	CONSTRAINT [PK_dbo.SUPUserReviews] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Item reviews table
CREATE TABLE [dbo].[SUPItemReviews]
(
	[Id]					INT IDENTITY (1,1)							NOT NULL,
	[Details]				NVARCHAR(MAX)								NOT NULL,
	[Rating]				INT											NOT NULL,
	[Timestamp]				DATETIME									NOT NULL,
	[UserDoingReviewID]		INT FOREIGN KEY REFERENCES SUPUsers(Id)		NOT NULL,
	[ItemBeingReviewedID]	INT FOREIGN KEY REFERENCES SUPItems(Id)		NOT NULL

	CONSTRAINT [PK_dbo.SUPItemReviews] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- *** ASP.NET Identity tables ***

-- ASP.NET users table
CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,

    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);

-- Roles
-- NOTE: As of 2 June 2019, this table is currently not in use.
CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

-- User logins
-- NOTE: As of 2 June 2019, this table is currently not in use.
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);

-- User claims
-- NOTE: As of 2 June 2019, this table is currently not in use.
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);

-- User Roles
-- NOTE: As of 2 June 2019, this table is currently not in use.
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);
