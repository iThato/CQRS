CREATE TABLE [dbo].[SystemProfileFunction] (
    [SystemFunctionId] INT NOT NULL,
    [SystemProfileId]  INT NOT NULL,
    CONSTRAINT [PK_SystemProfileFunction] PRIMARY KEY CLUSTERED ([SystemFunctionId] ASC, [SystemProfileId] ASC),
    CONSTRAINT [FK_SystemProfileFunction_SystemFunction] FOREIGN KEY ([SystemFunctionId]) REFERENCES [dbo].[SystemFunction] ([SystemFunctionId]),
    CONSTRAINT [FK_SystemProfileFunction_SystemProfile] FOREIGN KEY ([SystemProfileId]) REFERENCES [dbo].[SystemProfile] ([SystemProfileId])
);

