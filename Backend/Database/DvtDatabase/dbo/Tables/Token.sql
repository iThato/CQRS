CREATE TABLE [dbo].[Token] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Value]       UNIQUEIDENTIFIER NOT NULL,
    [ExpiryDate]  DATETIME         NOT NULL,
    [CreatedDate] DATETIME         CONSTRAINT [DF_Token_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [TokenTypeId] INT              NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Token_TokenType] FOREIGN KEY ([TokenTypeId]) REFERENCES [dbo].[TokenType] ([Id]),
    CONSTRAINT [FK_Token_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserAccount] ([UserAccountId])
);

