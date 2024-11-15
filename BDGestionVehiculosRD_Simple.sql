-- Crear la base de datos básica
USE [master]
GO
CREATE DATABASE [EstacionamientoDBS]
GO
USE [EstacionamientoDBS]
GO

-- Crear tabla TiposVehiculo
CREATE TABLE [dbo].[TiposVehiculo](
    [TipoID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Descripcion] NVARCHAR(50) NOT NULL,
    [TarifaPorMinuto] DECIMAL(10, 2) NOT NULL CHECK ([TarifaPorMinuto] >= 0)
);
GO

-- Crear tabla Vehiculos
CREATE TABLE [dbo].[Vehiculos](
    [VehiculoId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Placa] NVARCHAR(10) NOT NULL UNIQUE,
    [TipoID] INT NOT NULL,
    FOREIGN KEY ([TipoID]) REFERENCES [TiposVehiculo]([TipoID])
);
GO

-- Crear tabla RegistroEstancia
CREATE TABLE [dbo].[RegistroEstancia](
    [EstanciaID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [VehiculoID] INT NOT NULL,
    [FechaEntrada] DATETIME NOT NULL DEFAULT GETDATE(),
    [FechaSalida] DATETIME NULL,
    [Monto] DECIMAL(18, 2) NULL,
    FOREIGN KEY ([VehiculoID]) REFERENCES [Vehiculos]([VehiculoId])
);
GO

-- Crear vista para reporte de estacionamiento
CREATE VIEW [dbo].[ReporteEstacionamiento] AS
SELECT 
    v.Placa,
    te.Descripcion AS TipoVehiculo,
    re.FechaEntrada,
    re.FechaSalida,
    DATEDIFF(MINUTE, re.FechaEntrada, re.FechaSalida) AS TiempoEstacionadoMinutos,
    CASE
        WHEN te.TarifaPorMinuto = 0 THEN 0
        ELSE DATEDIFF(MINUTE, re.FechaEntrada, re.FechaSalida) * te.TarifaPorMinuto
    END AS CantidadAPagar
FROM 
    RegistroEstancia re
    INNER JOIN Vehiculos v ON re.VehiculoID = v.VehiculoID
    INNER JOIN TiposVehiculo te ON v.TipoID = te.TipoID
WHERE 
    re.FechaSalida IS NOT NULL;
GO

-- Insertar algunos tipos de vehículo
INSERT INTO [dbo].[TiposVehiculo] ([Descripcion], [TarifaPorMinuto]) 
VALUES (N'Oficial', 0.00), (N'Residente', 1.00), (N'No Residente', 3.00);
GO
