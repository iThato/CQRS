CREATE TABLE [dbo].[SystemFunctionGroup] (
    [SystemFunctionGroupId] INT            NOT NULL,
    [DisplayName]           NVARCHAR (100) NOT NULL,
    [Name]                  NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SystemFunctionGroup] PRIMARY KEY CLUSTERED ([SystemFunctionGroupId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SystemFunctionGroup]
    ON [dbo].[SystemFunctionGroup]([Name] ASC);

