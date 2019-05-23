CREATE TABLE [dbo].[TokenType] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TokenType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

