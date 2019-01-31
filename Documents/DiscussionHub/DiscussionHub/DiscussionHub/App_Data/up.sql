CREATE TABLE [dbo].[SUPUsers]
(
    [ID]            INT IDENTITY (1,1)      NOT NULL,
    [Name]          NVARCHAR(100)           NOT NULL,
    [Username]      NVARCHAR(100)           NOT NULL,
    [Email]         NVARCHAR(100)           NOT NULL,

    CONSTRAINT [PK_dbo.SUPUsers] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[Discussions]
(
    [ID]            INT IDENTITY (1,1)					 NOT NULL,
    [Title]         NVARCHAR(100)						 NOT NULL,
    [Description]   NVARCHAR(MAX)						 NOT NULL,
    [URL]           NVARCHAR(256)						 NOT NULL,
    [UserID]        INT FOREIGN KEY REFERENCES SUPUsers(ID) NOT NULL

    CONSTRAINT [PK_dbo.Discussions] PRIMARY KEY CLUSTERED ([ID] ASC)
)

INSERT INTO [dbo].[SUPUsers] (Name, Username, Email) VALUES
    ('Jim Jones', 'jjones1', 'jjones@gmail.com'),
    ('Bill Brown', 'bbrown1', 'bbrown@gmail.com'),
    ('Sam Smith', 'ssmisth1', 'ssmith@gmail.com')

INSERT INTO [dbo].[Discussions] (Title, Description, URL, UserID) VALUES
    ('Some Problem', 'Something bad happened.', 'badstuff.com/ohno', 3),
    ('Some Solution', 'Something got fixed.', 'solutions.com/ohyeah', 1),
    ('Some other thing', 'Something is boring.', 'generic.com/boring', 2)

SELECT * FROM dbo.SUPUsers;
SELECT * FROM dbo.Discussions;