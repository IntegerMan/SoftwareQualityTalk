CREATE TABLE [dbo].[ResumeKeywords]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Keyword] NVARCHAR(MAX) NOT NULL, 
    [Modifier] INT NOT NULL
)
GO

INSERT INTO [dbo].[ResumeKeywords] (Keyword, Modifier) VALUES 
('testing', 5),
('xunit', 5),
('nunit', 5),
('mstest', 2)
GO