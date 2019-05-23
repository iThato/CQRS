CREATE TABLE [dbo].[UserAccount] (
    [UserAccountId]   UNIQUEIDENTIFIER CONSTRAINT [DF_UserAccount_UserAccountId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [FirstName]       NVARCHAR (100)   NOT NULL,
    [LastName]        NVARCHAR (100)   NOT NULL,
    [KnownAs]         NVARCHAR (100)   NULL,
    [Username]        NVARCHAR (100)   NOT NULL,
    [Email]           NVARCHAR (100)   NOT NULL,
    [ContactNumber]   NVARCHAR (20)    NULL,
    [Password]        NVARCHAR (64)    NULL,
    [Salt]            NVARCHAR (64)    NULL,
    [AcceptedTerms]   BIT              NOT NULL,
    [UserStatusId]    INT              NOT NULL,
    [SystemProfileId] INT              NOT NULL,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED ([UserAccountId] ASC),
    CONSTRAINT [FK_UserAccount_SystemProfile] FOREIGN KEY ([SystemProfileId]) REFERENCES [dbo].[SystemProfile] ([SystemProfileId]),
    CONSTRAINT [FK_UserAccount_UserStatus] FOREIGN KEY ([UserStatusId]) REFERENCES [dbo].[UserStatus] ([UserStatusId])
);





