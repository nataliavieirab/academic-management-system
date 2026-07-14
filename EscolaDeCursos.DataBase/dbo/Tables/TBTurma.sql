CREATE TABLE [dbo].[TBTurma] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Nome]             NVARCHAR (100)   NOT NULL,
    [InstrutorId]      UNIQUEIDENTIFIER NULL,
    [CapacidadeMaxima] INT              NOT NULL,
    [DataInicio]       DATE             NOT NULL,
    [DataTermino]      DATE             NOT NULL
);
GO

CREATE NONCLUSTERED INDEX [IX_TBTurma_InstrutorId]
    ON [dbo].[TBTurma]([InstrutorId] ASC);
GO

ALTER TABLE [dbo].[TBTurma]
    ADD CONSTRAINT [PK_TBTurma] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[TBTurma]
    ADD CONSTRAINT [FK_TBTurma_TBInstrutor_InstrutorId] FOREIGN KEY ([InstrutorId]) REFERENCES [dbo].[TBInstrutor] ([Id]);
GO

