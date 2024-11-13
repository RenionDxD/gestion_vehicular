USE [master]
GO
/****** Object:  Database [EstacionamientoDB]    Script Date: 13/11/2024 11:08:41 a. m. ******/
CREATE DATABASE [EstacionamientoDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EstacionamientoDB', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EstacionamientoDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EstacionamientoDB_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EstacionamientoDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EstacionamientoDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EstacionamientoDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EstacionamientoDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EstacionamientoDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EstacionamientoDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EstacionamientoDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EstacionamientoDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET RECOVERY FULL 
GO
ALTER DATABASE [EstacionamientoDB] SET  MULTI_USER 
GO
ALTER DATABASE [EstacionamientoDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EstacionamientoDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EstacionamientoDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EstacionamientoDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EstacionamientoDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EstacionamientoDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EstacionamientoDB', N'ON'
GO
ALTER DATABASE [EstacionamientoDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [EstacionamientoDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EstacionamientoDB]
GO
/****** Object:  Table [dbo].[TiposVehiculo]    Script Date: 13/11/2024 11:08:42 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposVehiculo](
	[TipoID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
	[TarifaPorMinuto] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculos]    Script Date: 13/11/2024 11:08:42 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculos](
	[VehiculoId] [int] IDENTITY(1,1) NOT NULL,
	[Placa] [nvarchar](10) NOT NULL,
	[TipoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VehiculoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroEstancia]    Script Date: 13/11/2024 11:08:42 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroEstancia](
	[EstanciaID] [int] IDENTITY(1,1) NOT NULL,
	[VehiculoID] [int] NOT NULL,
	[FechaEntrada] [datetime] NOT NULL,
	[FechaSalida] [datetime] NULL,
	[Monto] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[EstanciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ReporteEstacionamiento]    Script Date: 13/11/2024 11:08:42 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Vista para reportes de entradas y salidas, con cálculos de tiempo estacionado y cobro
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
    re.FechaSalida IS NOT NULL; -- Solo mostrar registros con salida

GO
SET IDENTITY_INSERT [dbo].[TiposVehiculo] ON 

INSERT [dbo].[TiposVehiculo] ([TipoID], [Descripcion], [TarifaPorMinuto]) VALUES (1, N'Oficial', CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[TiposVehiculo] ([TipoID], [Descripcion], [TarifaPorMinuto]) VALUES (2, N'Residente', CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[TiposVehiculo] ([TipoID], [Descripcion], [TarifaPorMinuto]) VALUES (3, N'No Residente', CAST(3.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[TiposVehiculo] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vehiculo__8310F99DB4E24E71]    Script Date: 13/11/2024 11:08:42 a. m. ******/
ALTER TABLE [dbo].[Vehiculos] ADD UNIQUE NONCLUSTERED 
(
	[Placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RegistroEstancia] ADD  DEFAULT (getdate()) FOR [FechaEntrada]
GO
ALTER TABLE [dbo].[RegistroEstancia]  WITH CHECK ADD FOREIGN KEY([VehiculoID])
REFERENCES [dbo].[Vehiculos] ([VehiculoId])
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD FOREIGN KEY([TipoId])
REFERENCES [dbo].[TiposVehiculo] ([TipoID])
GO
ALTER TABLE [dbo].[TiposVehiculo]  WITH CHECK ADD CHECK  (([TarifaPorMinuto]>=(0)))
GO
USE [master]
GO
ALTER DATABASE [EstacionamientoDB] SET  READ_WRITE 
GO
