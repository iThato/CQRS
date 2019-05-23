CREATE TABLE [dbo].[SystemFunction] (
    [SystemFunctionId]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (100) NOT NULL,
    [DisplayName]           NVARCHAR (100) NOT NULL,
    [SystemFunctionGroupId] INT            NOT NULL,
    CONSTRAINT [PK_SystemFunction] PRIMARY KEY CLUSTERED ([SystemFunctionId] ASC),
    CONSTRAINT [FK_SystemFunction_SystemFunctionGroup] FOREIGN KEY ([SystemFunctionGroupId]) REFERENCES [dbo].[SystemFunctionGroup] ([SystemFunctionGroupId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SystemFunction]
    ON [dbo].[SystemFunction]([Name] ASC);

