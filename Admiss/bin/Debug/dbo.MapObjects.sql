CREATE TABLE [dbo].[MapObjects] (
    [Id]        NVARCHAR (128) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [FName]     NVARCHAR (MAX) NULL,
    [Photo]     NVARCHAR (MAX) NULL,
    [Series]    INT            NULL,
    [Number]    INT            NULL,
    [Parent_Id] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.MapObjects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MapObjects_dbo.MapObjects_Parent_Id] FOREIGN KEY ([Parent_Id]) REFERENCES [dbo].[MapObjects] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Parent_Id]
    ON [dbo].[MapObjects]([Parent_Id] ASC);

