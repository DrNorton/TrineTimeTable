﻿CREATE TABLE [dbo].[Images]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ThumbUrl] NVARCHAR(250) NULL,
	[FullImageUrl] NVARCHAR(250) NULL
)
