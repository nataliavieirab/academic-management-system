CREATE TABLE [dbo].[TBAluno] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Nome]     NVARCHAR (100)   NOT NULL,
    [Cpf]      NVARCHAR (14)    NOT NULL,
    [Telefone] NVARCHAR (15)    NOT NULL,
    [Email]    NVARCHAR (100)   NOT NULL,
    [Endereco] NVARCHAR (100)   NOT NULL
);
GO

ALTER TABLE [dbo].[TBAluno]
    ADD CONSTRAINT [PK_TBAluno] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_TBAluno_Email]
    ON [dbo].[TBAluno]([Email] ASC);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_TBAluno_Cpf]
    ON [dbo].[TBAluno]([Cpf] ASC);
GO

