﻿CREATE TABLE [dbo].[Images]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Thumb] VARBINARY(MAX) NULL,
	[FullImage] VARBINARY(MAX) NULL
)