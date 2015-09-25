CREATE TABLE [dbo].[Stations]
(
	[Id] INT NOT NULL  IDENTITY(1,1) PRIMARY KEY, 
    [Ecr] BIGINT NOT NULL, 
    [ExpressCode] BIGINT NOT NULL, 
    [StationName] NVARCHAR(50) NOT NULL, 
    [PositionId] INT NOT NULL, 
    [OpenStreetMapNode] BIGINT NULL, 
    [OpenStreetMapUrl] NVARCHAR(250) NULL, 
    [StationTypeId] INT NOT NULL, 
    [ImageId] BIGINT NULL, 
    CONSTRAINT [FK_Stations_ToPositions] FOREIGN KEY ([PositionId]) REFERENCES [Positions]([Id]),
	CONSTRAINT [FK_Stations_ToStationTypes] FOREIGN KEY ([StationTypeId]) REFERENCES [StationTypes]([Id]),
    CONSTRAINT [FK_Stations_ToImage] FOREIGN KEY ([ImageId]) REFERENCES [Images]([Id])


)
