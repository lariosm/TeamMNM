CREATE TABLE [dbo].[SUPUsers]
(
    [ID]            INT IDENTITY (1,1)    NOT NULL,
    [FirstName]     NVARCHAR(100)           NOT NULL,
    [LastName]      NVARCHAR(100)           NOT NULL,
    [NetUserId]     NVARCHAR (128)          NULL,

    CONSTRAINT [PK_dbo.SUPUsers] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[SUPDiscussions]
(
    [ID]            INT IDENTITY (1,1)      NOT NULL,
    [Title]         NVARCHAR(100)           NOT NULL,
    [Description]   NVARCHAR(MAX)           NOT NULL,
    [URL]           NVARCHAR(100)           NOT NULL,
    [TIMESTAMP]		DATETIME			    NOT NULL,
    [UserID]        INT FOREIGN     KEY REFERENCES SUPUsers(ID)    NOT NULL

    CONSTRAINT [PK_dbo.SUPDiscussions] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[SUPComments]
(
    [ID]            INT IDENTITY (1,1)      NOT NULL,
    [Title]         NVARCHAR(100)           NOT NULL,
    [Details]       NVARCHAR(MAX)           NOT NULL,
    [TIMESTAMP]		DATETIME			    NOT NULL,
    [UserID]        INT FOREIGN     KEY REFERENCES SUPUsers(ID)    NOT NULL,
    [DiscussionID]  INT FOREIGN     KEY REFERENCES SUPDiscussions(ID)   NOT NULL

    CONSTRAINT [PK_dbo.SUPComments] PRIMARY KEY CLUSTERED ([ID] ASC)
)

INSERT INTO [dbo].[SUPUsers] (FirstName, LastName) VALUES
	('Jim', 'Jones'),
	('Sam', 'Smith'),
	('Bob', 'Brown')

INSERT INTO [dbo].[SUPDiscussions] (Title, Description, URL, TIMESTAMP, UserID) VALUES
    ('Some Problem', 'Something bad happened.', 'badstuff.com/ohno', '01/18/2012 01:37:38', 3),
    ('Some Solution', 'Something got fixed.', 'solutions.com/ohyeah', '02/18/2012 01:37:38', 1),
    ('Some other thing', 'Something is boring.', 'generic.com/boring', '03/18/2012 01:37:38', 2)

INSERT INTO [dbo].[SUPComments] (Title, Details, TIMESTAMP, UserID, DiscussionID) VALUES
    ('I like this', 'This article is interesting.', '04/18/2012 01:37:38', 1, 2),
    ('I dont like this', 'This article is not interesting.', '05/18/2012 01:37:38', 2, 1),
    ('What is this?', 'It looks like fake news to me.', '06/18/2012 01:37:38', 3, 3)



-- Identity tables


-- Users

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

CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);


-- User logins

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
