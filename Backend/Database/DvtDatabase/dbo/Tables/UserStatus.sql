CREATE TABLE [dbo].[UserStatus] (
    [UserStatusId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED ([UserStatusId] ASC)
);

