USE [master]
GO
/****** Object:  Database [SISAP-DEV]    Script Date: 16/06/2021 14:34:12 ******/
CREATE DATABASE [SISAP-DEV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SISAP-DEV', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SISAP-DEV.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SISAP-DEV_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SISAP-DEV_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SISAP-DEV] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SISAP-DEV].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SISAP-DEV] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SISAP-DEV] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SISAP-DEV] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SISAP-DEV] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SISAP-DEV] SET ARITHABORT OFF 
GO
ALTER DATABASE [SISAP-DEV] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SISAP-DEV] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SISAP-DEV] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SISAP-DEV] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SISAP-DEV] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SISAP-DEV] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SISAP-DEV] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SISAP-DEV] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SISAP-DEV] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SISAP-DEV] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SISAP-DEV] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SISAP-DEV] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SISAP-DEV] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SISAP-DEV] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SISAP-DEV] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SISAP-DEV] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SISAP-DEV] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SISAP-DEV] SET RECOVERY FULL 
GO
ALTER DATABASE [SISAP-DEV] SET  MULTI_USER 
GO
ALTER DATABASE [SISAP-DEV] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SISAP-DEV] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SISAP-DEV] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SISAP-DEV] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SISAP-DEV] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SISAP-DEV] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SISAP-DEV', N'ON'
GO
ALTER DATABASE [SISAP-DEV] SET QUERY_STORE = OFF
GO
USE [SISAP-DEV]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[CategoriaId] [int] IDENTITY(1,1) NOT NULL,
	[TipoCategoria] [varchar](100) NULL,
	[ClaseId] [int] NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciclos]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciclos](
	[CiclosId] [int] IDENTITY(1,1) NOT NULL,
	[Annio] [int] NULL,
	[Mes] [int] NULL,
	[LecturaInicial] [datetime] NULL,
	[LecturaFinal] [datetime] NULL,
	[EmisionInicial] [datetime] NULL,
	[EmisionFinal] [datetime] NULL,
	[DistribucionInicial] [datetime] NULL,
	[DistribucionFinal] [datetime] NULL,
	[CorteInicial] [datetime] NULL,
	[CorteFinal] [datetime] NULL,
 CONSTRAINT [PK_Ciclos] PRIMARY KEY CLUSTERED 
(
	[CiclosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clase]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clase](
	[ClaseId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_Clase] PRIMARY KEY CLUSTERED 
(
	[ClaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioCreacion] [varchar](50) NULL,
	[CodigoCliente] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[DNI] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Direccion] [varchar](200) NULL,
	[UrbanizacionId] [int] NULL,
	[ManzanaId] [int] NULL,
	[ServicioId] [int] NULL,
	[CategoriaId] [int] NULL,
	[NumeroMedidor] [varchar](50) NULL,
	[EstadoServicioId] [int] NULL,
	[Complemento] [varchar](100) NULL,
	[Observaciones] [varchar](500) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConsumoServicio]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsumoServicio](
	[ConsumoServicioId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[CategoriaId] [int] NULL,
	[ServicioId] [int] NULL,
	[PagoId] [int] NULL,
 CONSTRAINT [PK_ConsumoServicio] PRIMARY KEY CLUSTERED 
(
	[ConsumoServicioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleEstadoPago]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleEstadoPago](
	[DetalleEstadoPagoId] [int] IDENTITY(1,1) NOT NULL,
	[DetalleEstadoPago] [int] NOT NULL,
 CONSTRAINT [PK_DetalleEstadoPago] PRIMARY KEY CLUSTERED 
(
	[DetalleEstadoPagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[DetalleFacturaId] [int] IDENTITY(1,1) NOT NULL,
	[ConsumoServicioId] [int] NOT NULL,
	[ServicioId] [int] NOT NULL,
	[UsuarioCreacion] [varchar](50) NULL,
	[Cantidad] [varchar](50) NULL,
	[CostoCubo] [varchar](50) NULL,
	[CostoRefacturacion] [varchar](50) NULL,
	[CostoIGV] [varchar](50) NULL,
	[CostoTotal] [varchar](50) NULL,
 CONSTRAINT [PK_DetalleFactura] PRIMARY KEY CLUSTERED 
(
	[DetalleFacturaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direccion](
	[DireccionId] [int] IDENTITY(1,1) NOT NULL,
	[Manzana] [varchar](50) NULL,
	[NumeroLote] [varchar](50) NULL,
	[Asignacion] [varchar](50) NULL,
 CONSTRAINT [PK_Direccion] PRIMARY KEY CLUSTERED 
(
	[DireccionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DireccionDato]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DireccionDato](
	[DireccionDatoId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[DireccionId] [int] NULL,
	[UrbanizacionId] [int] NULL,
 CONSTRAINT [PK_DireccionDato] PRIMARY KEY CLUSTERED 
(
	[DireccionDatoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[EstadoId] [int] IDENTITY(1,1) NOT NULL,
	[EstadoMedidor] [int] NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[EstadoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoPago]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoPago](
	[EstadoPagoId] [int] IDENTITY(1,1) NOT NULL,
	[EstadoNombre] [varchar](50) NULL,
 CONSTRAINT [PK_EstadoPago] PRIMARY KEY CLUSTERED 
(
	[EstadoPagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoServicio]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoServicio](
	[EstadoServicioId] [int] IDENTITY(1,1) NOT NULL,
	[EstadoDescripcion] [varchar](50) NULL,
 CONSTRAINT [PK_EstadoServicio] PRIMARY KEY CLUSTERED 
(
	[EstadoServicioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[FacturaId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[Annio] [int] NULL,
	[Mes] [int] NULL,
	[SubTotal] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[FacturaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturacion]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturacion](
	[FacturacionId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[Annio] [int] NULL,
	[Mes] [int] NULL,
	[SubTotal] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
	[NroBoleta] [int] NULL,
	[FechaEmision] [datetime] NULL,
 CONSTRAINT [PK_Facturacion] PRIMARY KEY CLUSTERED 
(
	[FacturacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lectura]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lectura](
	[LecturaId] [int] IDENTITY(1,1) NOT NULL,
	[Annio] [int] NULL,
	[Mes] [int] NULL,
	[CantidadLectura] [decimal](18, 4) NULL,
	[Consumo] [decimal](18, 4) NULL,
	[Promedio] [decimal](18, 4) NULL,
	[Alerta] [varchar](100) NULL,
	[ClienteId] [int] NULL,
	[FechaRegistro] [datetime] NULL,
	[CiclosId] [int] NULL,
	[CantidadLecturaAntigua] [decimal](18, 4) NULL,
	[Procesado] [int] NULL,
	[Actualizado] [int] NULL,
 CONSTRAINT [PK_Lectura] PRIMARY KEY CLUSTERED 
(
	[LecturaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manzana]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manzana](
	[ManzanaId] [int] IDENTITY(1,1) NOT NULL,
	[UrbanizacionId] [int] NULL,
	[NombreManzana] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medidor]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medidor](
	[MeidorId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[EstadoId] [int] NULL,
	[LecturaId] [int] NULL,
	[NumeroMedidor] [int] NULL,
 CONSTRAINT [PK_Medidor] PRIMARY KEY CLUSTERED 
(
	[MeidorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opcion]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opcion](
	[OpcionId] [int] IDENTITY(1,1) NOT NULL,
	[Etiqueta] [varchar](50) NULL,
	[Url] [varchar](50) NULL,
	[ParentId] [int] NULL,
	[Orden] [int] NULL,
	[Icono] [varchar](20) NULL,
	[CreadoPor] [varchar](50) NULL,
	[FechaCreacion] [datetime2](7) NULL,
	[ModificadoPor] [varchar](50) NULL,
	[FechaModificacion] [datetime2](7) NULL,
 CONSTRAINT [PK_Opcion] PRIMARY KEY CLUSTERED 
(
	[OpcionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[PagoId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[Estado] [int] NULL,
	[Total] [decimal](18, 2) NULL,
	[PeriodoAnnio] [int] NULL,
	[PeriodoMes] [int] NULL,
	[FechaPago] [datetime] NULL,
	[ServicioId] [int] NULL,
	[Observaciones] [varchar](150) NULL,
	[FacturacionId] [int] NULL,
	[EstadoPDesc] [varchar](100) NULL,
 CONSTRAINT [PK_Pago] PRIMARY KEY CLUSTERED 
(
	[PagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rangos]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rangos](
	[RangosId] [int] IDENTITY(1,1) NOT NULL,
	[CategoriaId] [int] NULL,
	[RangoMin] [decimal](18, 2) NULL,
	[RangoMax] [decimal](18, 2) NULL,
	[TarifaAgua] [decimal](18, 2) NULL,
	[TarifaAlcantarillado] [decimal](18, 2) NULL,
	[CargoFijo] [decimal](18, 2) NULL,
	[ClaseId] [int] NULL,
 CONSTRAINT [PK_Rangos] PRIMARY KEY CLUSTERED 
(
	[RangosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicio](
	[ServicioId] [int] IDENTITY(1,1) NOT NULL,
	[ServicioNombre] [varchar](100) NULL,
 CONSTRAINT [PK_Servicio] PRIMARY KEY CLUSTERED 
(
	[ServicioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tarifario]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarifario](
	[TarifarioId] [int] IDENTITY(1,1) NOT NULL,
	[CategoriaId] [int] NULL,
	[RangoMin] [decimal](18, 2) NULL,
	[RangoMax] [decimal](18, 2) NULL,
	[TarifaAgua] [decimal](18, 2) NULL,
	[TarifaAlcantarillado] [decimal](18, 2) NULL,
	[CargoFijo] [decimal](18, 2) NULL,
	[ClaseId] [int] NULL,
 CONSTRAINT [PK_Tarifario] PRIMARY KEY CLUSTERED 
(
	[TarifarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoCategoria]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCategoria](
	[TipoCategoriaId] [int] IDENTITY(1,1) NOT NULL,
	[TipoCategoria] [int] NULL,
 CONSTRAINT [PK_TipoCategoria] PRIMARY KEY CLUSTERED 
(
	[TipoCategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Urbanizacion]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Urbanizacion](
	[UrbanizacionId] [int] IDENTITY(1,1) NOT NULL,
	[NombreUrbanizacion] [varchar](100) NULL,
	[Codigo] [int] NULL,
 CONSTRAINT [PK_Urbanizacion] PRIMARY KEY CLUSTERED 
(
	[UrbanizacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 16/06/2021 14:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Rol] [int] NULL,
	[Estado] [int] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [SISAP-DEV] SET  READ_WRITE 
GO
