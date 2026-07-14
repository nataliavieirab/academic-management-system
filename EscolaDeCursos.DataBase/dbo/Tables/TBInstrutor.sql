CREATE TABLE [dbo].[TBInstrutor] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Nome]     NVARCHAR (100)   NOT NULL,
    [Cpf]      NVARCHAR (14)    NOT NULL,
    [Telefone] NVARCHAR (15)    NOT NULL,
    [Email]    NVARCHAR (100)   NOT NULL
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_TBInstrutor_Email]
    ON [dbo].[TBInstrutor]([Email] ASC);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_TBInstrutor_Cpf]
    ON [dbo].[TBInstrutor]([Cpf] ASC);
GO

ALTER TABLE [dbo].[TBInstrutor]
    ADD CONSTRAINT [PK_TBInstrutor] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

