USE master;

--Delete the minasDB database if it exists.  
IF EXISTS(SELECT * from sys.databases WHERE name='seguimientoMinas')  
BEGIN  
    DROP DATABASE seguimientoMinas; 
END  

--Create a new database called seguimientoMinas.  
CREATE DATABASE seguimientoMinas; 

USE seguimientoMinas; 

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'Formulario')
	BEGIN
		DROP TABLE Formulario;
	END

	CREATE TABLE dbo.Formulario  
		   (idFormulario int identity(1,1) NOT NULL,
			GUIDFormulario varchar(40) unique Not NULL,
			nombreFormulario varchar(50) NOT NULL,  
			estado bit Not NULL,
			CONSTRAINT PK_Formulario PRIMARY KEY (idFormulario))

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'TipoDetalle')
	BEGIN
		DROP TABLE TipoDetalle;
	END

	CREATE TABLE dbo.TipoDetalle  
		   (idTipoDetalle int identity(1,1) NOT NULL,
			nombreTipoDetalle varchar(25) Not NULL,
			CONSTRAINT PK_TipoDetalle PRIMARY KEY (idTipoDetalle))

	insert into TipoDetalle(nombreTipoDetalle) VALUES ('Dato');
	insert into TipoDetalle(nombreTipoDetalle) VALUES ('Imágen');
	insert into TipoDetalle(nombreTipoDetalle) VALUES ('Video');

	
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'TipoConexion')
	BEGIN
		DROP TABLE TipoConexion;
	END

	CREATE TABLE dbo.TipoConexion  
		   (idTipoConexion int identity(1,1) NOT NULL,
			nombreTipoConexion varchar(20) unique Not NULL,
			CONSTRAINT PK_TipoConexion PRIMARY KEY (idTipoConexion))

	insert into TipoConexion(nombreTipoConexion) VALUES ('Formulario');
	insert into TipoConexion(nombreTipoConexion) VALUES ('Imágen');
	insert into TipoConexion(nombreTipoConexion) VALUES ('Video');


IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'Job')
	BEGIN
		DROP TABLE Job;
	END

	CREATE TABLE dbo.Job  
		   (idJob int identity(1,1) NOT NULL,
			nombre varchar(40) Not Null,
			CONSTRAINT PK_Job PRIMARY KEY (idJob))

	insert into Job(nombre) VALUES ('JOB1');
	insert into Job(nombre) VALUES ('JOB2');
	insert into Job(nombre) VALUES ('JOB3');
	insert into Job(nombre) VALUES ('JOB4');
	insert into Job(nombre) VALUES ('JOB5');

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'Conexion')
	BEGIN
		DROP TABLE Conexion;
	END

	CREATE TABLE dbo.Conexion  
		   (idConexion int identity(1,1) NOT NULL,
			idTipoConexion int Not NULL,
			idFormulario int unique Not Null,
			idJob int NOT NULL,
			nombreConexion varchar(120) unique NOT NULL,
			periodoSincronizacion int NOT NULL,
			descripcion varchar(255) NOT NULL,
			fechaActualizacion datetime,
			CONSTRAINT PK_Conexion PRIMARY KEY (idConexion),
			CONSTRAINT FK_TipoConexion FOREIGN KEY (idTipoConexion) REFERENCES TipoConexion(idTipoConexion),
			CONSTRAINT DF_FechaActualizacion DEFAULT GETDATE() FOR fechaActualizacion,
			CONSTRAINT FK_FormularioConexion FOREIGN KEY (idFormulario) REFERENCES Formulario(idFormulario),
			CONSTRAINT FK_JobConexion FOREIGN KEY (idJob) REFERENCES Job(idJob))

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'Detalle')
	BEGIN
		DROP TABLE Detalle;
	END

	CREATE TABLE dbo.Detalle  
		   (idDetalle bigint identity(1,1) NOT NULL,
			idConexion int Not NULL,
			idActividad VARCHAR(25) NULL,
			nombreActividad varchar(50) NULL,
			urlDetalle varchar(150) NOT NULL,  
			descripcion varchar(max) NULL, 
			idTipoDetalle int not NULL,
			fechaCreacion date default GETDATE(),
			CONSTRAINT PK_Detalle PRIMARY KEY (idDetalle),
			CONSTRAINT FK_TipoDetalle FOREIGN KEY (idTipoDetalle) REFERENCES TipoDetalle(idTipoDetalle),
			CONSTRAINT FK_FormularioDetalle FOREIGN KEY (idConexion) REFERENCES Conexion(idConexion))

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'Usuario')
	BEGIN
		DROP TABLE Usuario;
	END

	CREATE TABLE dbo.Usuario  
		   (idUsuario bigint identity(1,1) NOT NULL,
			GUIDUsuario varchar(40) Not Null,
			nombre varchar(50) Not Null,
			apellido varchar(50),
			CONSTRAINT PK_Usuario PRIMARY KEY (idUsuario))

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'TipoLocacion')
	BEGIN
		DROP TABLE TipoLocacion;
	END

	CREATE TABLE dbo.TipoLocacion  
		   (idTipoLocacion int identity(1,1) NOT NULL,
			GUIDTipoLocation varchar(40) Not Null,
			nombre varchar(50) Not Null,
			estado bit Not Null,
			CONSTRAINT PK_TipoLocacion PRIMARY KEY (idTipoLocacion))

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE XTYPE='U' AND NAME = 'Locacion')
	BEGIN
		DROP TABLE Locacion;
	END

	CREATE TABLE dbo.Locacion  
		   (idLocacion bigint identity(1,1) NOT NULL,
			GUIDLocation varchar(40) Not Null,
			nombre varchar(30) Not Null,
			nombreContacto varchar(30) Not Null,
			email varchar(50) Not Null,
			telefono varchar(50) Not Null,
			fax varchar(50) Not Null,
			direccion varchar(50) Not Null,
			ciudad varchar(20) Not Null,
			departamento varchar(20) Not Null,
			pais varchar(20) Not Null,
			latitud varchar(20) Not Null,
			longitud varchar(20) Not Null,
			tagUID varchar(20) Not Null,
			idTipoLocacion int Not Null,
			fechaActualizacion datetime Not Null,
			CONSTRAINT PK_Locacion PRIMARY KEY (idLocacion),
			CONSTRAINT FK_TipoLocacion FOREIGN KEY (idTipoLocacion) REFERENCES TipoLocacion(idTipoLocacion))

-----------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[physical_table](
	[IdActividad] [varchar](max) NULL,
	[NombreFormulario] [varchar](max) NULL,
	[LocationName] [varchar](max) NOT NULL,
	[LocationGUID] [varchar](max) NULL,
	[FechaCreacion] [varchar](max) NULL,
	[FechaActualizacion] [varchar](max) NULL,
	[Usuario] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


 ----------------------------------------------------Procedimientos almacenadois------------------------------------------------------
CREATE PROCEDURE [dbo].[ActualizarFechaConexion]
  @GuidFormulario varchar(100)

AS
BEGIN

UPDATE C    
 SET fechaActualizacion = CONVERT (datetime, GETDATE()) 
 FROM Conexion C
  INNER JOIN Formulario F ON C.idFormulario = F.idFormulario
  WHERE F.GUIDFormulario = @GuidFormulario
END
GO
------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[AddColumna]
	 @NombreTabla varchar(128),
	 @NombreColumna varchar(128),
	 @TipoColumna varchar(15),
	 @Obligatorio varchar(15)

AS
BEGIN
	
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'ALTER TABLE '+ @NombreTabla + ' ADD '+@NombreColumna+' '+@TipoColumna+' '+@Obligatorio+' ;'
   EXEC (@SQLString)
END
GO
--------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[AddMedia]

     @idConexion int,
	 @UrlDetalle varchar(150),
	 @idTipoDetalle int,
	 @idActividad varchar(25),
	 @descripcion varchar(max),
	 @NombreActividad varchar(50)

AS
BEGIN
 INSERT INTO [dbo].[Detalle] 
   (
   idConexion,
   UrlDetalle,
   idTipoDetalle,
   idActividad,
   Descripcion,
   NombreActividad,
   FechaCreacion
   )
   VALUES(
	@idConexion,
	@UrlDetalle,
	@idTipoDetalle,
	@idActividad,
	@descripcion ,
	@NombreActividad,
	GETDATE()
   )		
END 
GO
---------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[AddRegistro]
     @NombreTabla nvarchar(max),
	 @Values nvarchar(max),
	 @Columns nvarchar(max)
AS
BEGIN	
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'INSERT INTO '+ @NombreTabla +  +'('+@Columns+') VALUES('+@Values+') ;'
   EXEC (@SQLString)		
END
GO
-------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[AddRegla]
	 @NombreTabla varchar(128),
	 @NombreColumna varchar(128),
	 @TablaReferencia varchar(128),
	 @NombreColumnaReferencia varchar(128)

AS
BEGIN	
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'ALTER TABLE '+ @NombreTabla + '  WITH CHECK ADD FOREIGN KEY( ['+@NombreColumna+']) REFERENCES [dbo].['+@TablaReferencia+'] (['+@NombreColumnaReferencia+']) ;'
   EXEC (@SQLString)		
END
GO
-----------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ChangeNameColumn]	
	 @TableName varchar(128),
	 @OldColumnName varchar(128),
	 @NewColumnName varchar(128)

AS
BEGIN
	DECLARE @StringChange varchar(50);
	set @StringChange =  @TableName+'.'+@OldColumnName;
	EXEC sp_RENAME   @StringChange , @NewColumnName, 'COLUMN'		
END
GO
-----------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ColumnaClavePrimaria]
	 @NombreTabla varchar(128),
	 @NombreColumna varchar(128)
AS
BEGIN
	SELECT TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME
	FROM information_schema.key_column_usage
	WHERE  TABLE_NAME =  @NombreTabla and COLUMN_NAME = @NombreColumna and CONSTRAINT_NAME LIKE 'PK%'	
END
GO
-----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ColumnaExiste]
	 @NombreTabla varchar(128),
	 @NombreColumna varchar(128)

AS
BEGIN
	Select * from( 
	SELECT OBJ.id AS id_tabla, 
        ROW_NUMBER() OVER (ORDER BY COL.colid) AS id_columna, 
        COL.name AS columna, 
        TYP.name AS Tipo, 
        --Por algun motivo los nvarchar dan el doble de la longitud
        Longitud = CASE TYP.name 
            WHEN 'nvarchar' THEN COL.LENGTH/2
            WHEN 'varchar' THEN COL.LENGTH/2
            ELSE COL.LENGTH
            END,
        COL.xprec AS PRECISION, 
        COL.xscale AS escala, 
        COL.isnullable AS Isnullable, 
        FK.constid AS id_fk, 
        OBJ2.name AS table_derecha, 
        COL2.name 
        --COL.*
    FROM dbo.syscolumns COL
    JOIN dbo.sysobjects OBJ ON OBJ.id = COL.id
    JOIN dbo.systypes TYP ON TYP.xusertype = COL.xtype
    --left join dbo.sysconstraints CON on CON.colid = COL.colid
    LEFT JOIN dbo.sysforeignkeys FK ON FK.fkey = COL.colid AND FK.fkeyid=OBJ.id
    LEFT JOIN dbo.sysobjects OBJ2 ON OBJ2.id = FK.rkeyid
    LEFT JOIN dbo.syscolumns COL2 ON COL2.colid = FK.rkey AND COL2.id = OBJ2.id
    WHERE OBJ.name = @NombreTabla  AND (OBJ.xtype='U' OR OBJ.xtype='V')
	) as TableColumn

	where columna = @NombreColumna	
END
GO
---------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ConexionesDisponiblesSincronizarConsultar]
 @idJob int

AS
BEGIN
	SELECT 
              idConexion,
              idTipoConexion,
              conexion.idFormulario,
              idJob,
              nombreConexion,
              periodoSincronizacion,
              descripcion,
              fechaActualizacion,
              Formulario.GUIDFormulario
        FROM [dbo].[Conexion] inner join [dbo].[Formulario] on(Conexion.idFormulario = Formulario.idFormulario)
        Where periodoSincronizacion <= (select DATEDIFF ( minute , fechaActualizacion , GetDate() ) ) and idJob = @idJob
END 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ConsultarActividades]	 
	 @Formulario varchar(25),
	 @FechaInicio  varchar(25),
	 @FechaFin  varchar(25)
AS
BEGIN
		DECLARE @StringSQL NVARCHAR(MAX);
		SET @StringSQL = RTRIM('	
							INSERT INTO physical_table 
							
							
							SELECT 

							ID as IdActividad,
						    Title as NombreFormulario, 
							LocationName,
							LocationGUID, 
							CreatedOn as FechaCreacion, 
							UpdatedOn as FechaActualizacion, 
							UserName as Usuario
							FROM ' + @Formulario + ' WHERE (CONVERT (date, CreatedOn) >=  CONVERT (date,'  + '''' + @FechaInicio + ''''+ ')  AND CONVERT (date, CreatedOn) <= CONVERT (date,'+ '''' + @FechaFin + ''''+'))'								   							
							)
							EXEC (@StringSQL)
END
GO
-------------------------------------------------------------------------------------------------------------------------------
Create PROCEDURE [dbo].[ConsultarActividadesTemporales]	 
AS
BEGIN

    Select

		 IdActividad,
		 NombreFormulario, 
		 LocationName,
		 LocationGUID, 
		 FechaCreacion, 
		 FechaActualizacion, 
		 Usuario 		 
		 from  physical_table
		 DELETE physical_table
END
GO 
----------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ConsultarDisponibilidadJob]

AS
BEGIN
	
	SELECT TOP(1) J.idJob,Count(C.idJob) AS cantidad FROM [dbo].[Job] J
              left join [dbo].[Conexion] C ON J.[idJob] = C.[idJob]
	GROUP BY J.idJob,C.idJob ORDER BY cantidad,J.idJob ASC
END
GO
------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[CrearTabla]
	 @NombreTabla nvarchar(128)

AS
BEGIN
	declare @SQLString nvarchar(max);
	set @SQLString = 'CREATE TABLE [dbo].' + @NombreTabla +'(
						[Codigo] [int] IDENTITY(1,1) NOT NULL,
					PRIMARY KEY CLUSTERED 
					(
						[Codigo] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY]';
   EXEC (@SQLString)
END
GO
---------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[EliminarDetalleActividad]
	 @NombreTabla varchar(128),
	 @IdActividad varchar(50)	
AS
BEGIN
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'DELETE FROM '+@NombreTabla+ ' WHERE IdActividad='+@IdActividad+';'
   EXEC (@SQLString)	
END
GO
------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[Prueba]
	 @NombreTabla varchar(128),
	 @NombreColumna varchar(128),
	 @TipoColumna varchar(15),
	 @Obligatorio varchar(15)
AS
BEGIN
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'ALTER TABLE '+ @NombreTabla + ' ADD '+@NombreColumna+' '+@TipoColumna+' '+@Obligatorio+' ;'
   EXEC (@SQLString)	
END
GO
-------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[RegistroExiste]
	 @NombreTabla varchar(128),
	 @NombreColumna varchar(128),
	 @IdPK varchar(25)
AS
BEGIN
	
   Declare @Result int
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'SELECT @Result = Count(*) FROM '+@NombreTabla+' WHERE '+@NombreColumna+' = '+@IdPK+';'
   execute sp_executesql @SQLString, N'@Result int OUTPUT', @Result output
   select @Result as Resultado
END
GO
------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[TablaExiste]
	 @NombreTabla varchar(128)
AS
BEGIN
	
	SELECT OBJ.id AS id_tabla, 
        ROW_NUMBER() OVER (ORDER BY COL.colid) AS id_columna, 
        COL.name AS columna, 
        TYP.name AS Tipo, 
        --Por algun motivo los nvarchar dan el doble de la longitud
        Longitud = CASE TYP.name 
            WHEN 'nvarchar' THEN COL.LENGTH/2
            WHEN 'varchar' THEN COL.LENGTH/2
            ELSE COL.LENGTH
            END,
        COL.xprec AS PRECISION, 
        COL.xscale AS escala, 
        COL.isnullable AS Isnullable, 
        FK.constid AS id_fk, 
        OBJ2.name AS table_derecha, 
        COL2.name 
        --COL.*
    FROM dbo.syscolumns COL
    JOIN dbo.sysobjects OBJ ON OBJ.id = COL.id
    JOIN dbo.systypes TYP ON TYP.xusertype = COL.xtype
    --left join dbo.sysconstraints CON on CON.colid = COL.colid
    LEFT JOIN dbo.sysforeignkeys FK ON FK.fkey = COL.colid AND FK.fkeyid=OBJ.id
    LEFT JOIN dbo.sysobjects OBJ2 ON OBJ2.id = FK.rkeyid
    LEFT JOIN dbo.syscolumns COL2 ON COL2.colid = FK.rkey AND COL2.id = OBJ2.id
    WHERE OBJ.name = @NombreTabla AND (OBJ.xtype='U' OR OBJ.xtype='V')		
END
GO
-----------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[TablasConsulta]
	 @NombreTabla varchar(128)

AS
BEGIN
	SELECT TABLE_NAME from Information_Schema.Tables
	where (TABLE_NAME = @NombreTabla or @NombreTabla is null)		
END
GO
-------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[UpdateRegistro]

     @NombreTabla nvarchar(128),
	 @script nvarchar(max),
	 @IdActividad nvarchar(max)
AS
BEGIN	
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'UPDATE '+ @NombreTabla + ' SET ' + @script + ' WHERE ID = '+ @IdActividad + ';'
   EXEC (@SQLString)	
END
GO
---------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ValidarRegistroActualizar]
	 @NombreTabla nvarchar(128),
	 @UpdateOn nvarchar(40),
	 @IdActividad nvarchar(25)
AS
BEGIN
   Declare @Result int
   DECLARE @SQLString NVARCHAR(MAX)
   SET @SQLString = 'SELECT @Result = Count(*) FROM '+@NombreTabla+' WHERE ( CreatedOn < '+@UpdateOn+' AND ID = '+ @IdActividad +' );'
   execute sp_executesql @SQLString, N'@Result int OUTPUT', @Result output
   select @Result as Resultado
END
GO
------------------------------------------------------------------------------------------------------------------------------------


