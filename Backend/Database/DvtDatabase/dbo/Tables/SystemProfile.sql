CREATE TABLE [dbo].[SystemProfile] (
    [SystemProfileId] INT            IDENTITY (1, 1) NOT NULL,
    [DisplayName]     NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SystemProfile] PRIMARY KEY CLUSTERED ([SystemProfileId] ASC)
);

