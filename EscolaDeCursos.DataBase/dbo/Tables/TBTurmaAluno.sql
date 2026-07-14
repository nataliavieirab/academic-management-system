CREATE TABLE [dbo].[TBTurmaAluno] (
    [AlunosId] UNIQUEIDENTIFIER NOT NULL,
    [TurmaId]  UNIQUEIDENTIFIER NOT NULL
);
GO

CREATE NONCLUSTERED INDEX [IX_TBTurmaAluno_TurmaId]
    ON [dbo].[TBTurmaAluno]([TurmaId] ASC);
GO

ALTER TABLE [dbo].[TBTurmaAluno]
    ADD CONSTRAINT [FK_TBTurmaAluno_TBTurma_TurmaId] FOREIGN KEY ([TurmaId]) REFERENCES [dbo].[TBTurma] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[TBTurmaAluno]
    ADD CONSTRAINT [FK_TBTurmaAluno_TBAluno_AlunosId] FOREIGN KEY ([AlunosId]) REFERENCES [dbo].[TBAluno] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[TBTurmaAluno]
    ADD CONSTRAINT [PK_TBTurmaAluno] PRIMARY KEY CLUSTERED ([AlunosId] ASC, [TurmaId] ASC);
GO

