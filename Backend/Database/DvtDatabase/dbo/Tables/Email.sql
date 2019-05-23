CREATE TABLE [dbo].[Email] (
    [EmailId]    UNIQUEIDENTIFIER NOT NULL,
    [To]         VARCHAR (MAX)    NOT NULL,
    [Subject]    VARCHAR (MAX)    NOT NULL,
    [Body]       VARCHAR (MAX)    NOT NULL,
    [CreateDate] DATETIME         NOT NULL,
    [SentDate]   DATETIME         NULL,
    [Result]     VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED ([EmailId] ASC)
);



