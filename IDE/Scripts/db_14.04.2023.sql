USE [FastApi]
GO
/****** Object:  Table [dbo].[api]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[api](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](250) NOT NULL,
	[description] [nvarchar](max) NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_app_active]  DEFAULT ((1)),
	[host] [nvarchar](max) NULL,
	[user_id] [bigint] NOT NULL,
	[is_private] [bit] NOT NULL CONSTRAINT [DF_api_is_private]  DEFAULT ((1)),
	[login_process] [nvarchar](max) NULL,
	[logout_process] [nvarchar](max) NULL,
	[jwt_issuer] [nvarchar](max) NULL,
	[jwt_audience] [nvarchar](max) NULL,
	[jwt_key] [nvarchar](max) NULL,
	[uid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_api_uid]  DEFAULT (newid()),
	[api_code] [bigint] NOT NULL,
 CONSTRAINT [PK_app] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[api_doc]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[api_doc](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_api_doc] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[api_session]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[api_session](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[session_uid] [uniqueidentifier] NOT NULL,
	[date_create] [datetime] NOT NULL,
	[date_end] [datetime] NULL,
	[jwt] [nvarchar](max) NULL,
 CONSTRAINT [PK_api_session] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[api_token]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[api_token](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[uid] [uniqueidentifier] NOT NULL,
	[active] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[con]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[con](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[connection] [nvarchar](500) NULL,
	[con_type_id] [int] NOT NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_con_active]  DEFAULT ((1)),
	[user_id] [bigint] NULL,
	[db_source] [nvarchar](max) NULL,
	[db_user] [nvarchar](max) NULL,
	[db_password] [nvarchar](max) NULL,
	[db_name] [nvarchar](max) NULL,
	[db_port] [int] NULL,
	[db_schema] [nvarchar](50) NULL,
	[comment] [nvarchar](max) NULL,
	[api_id] [bigint] NULL,
 CONSTRAINT [PK_con] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[con_table]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[con_table](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[description] [nvarchar](50) NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_con_table_active]  DEFAULT ((1)),
	[con_id] [bigint] NOT NULL,
 CONSTRAINT [PK_con_table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[con_type]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[con_type](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[description] [nvarchar](max) NULL,
	[con_str_format] [nvarchar](max) NULL,
 CONSTRAINT [PK_con_type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[data_type]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[data_type](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_data_type] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[lang]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lang](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[culture_code] [nvarchar](50) NULL,
 CONSTRAINT [PK_lang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[process]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[process](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[description] [nvarchar](max) NULL,
	[sql] [nvarchar](max) NULL,
	[con_id] [int] NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_process_active]  DEFAULT ((1)),
	[request] [nvarchar](max) NULL,
	[response] [nvarchar](max) NULL,
 CONSTRAINT [PK_process] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[process_param]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[process_param](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[data_type] [nvarchar](10) NOT NULL,
	[direction] [int] NOT NULL,
	[priority] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_process_parameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](250) NOT NULL,
	[last_name] [nvarchar](250) NOT NULL,
	[e_mail] [nvarchar](250) NOT NULL,
	[status] [int] NOT NULL CONSTRAINT [DF_user_approved]  DEFAULT ((0)),
	[password] [nvarchar](max) NOT NULL,
	[uid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_user_uid]  DEFAULT (newid()),
	[wrong_try] [int] NOT NULL CONSTRAINT [DF_user_wrong_try]  DEFAULT ((0)),
	[temp_code] [nvarchar](50) NULL,
	[jwt_token] [nvarchar](max) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[word]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[word](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[word] [nvarchar](max) NULL,
	[value] [nvarchar](max) NULL,
	[language] [nchar](10) NULL,
 CONSTRAINT [PK_word] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[v_primary_keys]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_primary_keys] AS
SELECT 
    Top 1000 KU.table_name as TABLENAME
    ,column_name as PRIMARYKEYCOLUMN
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC 

INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
    ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' 
    AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME 
ORDER BY 
     KU.TABLE_NAME
    ,KU.ORDINAL_POSITION
; 

GO
/****** Object:  View [dbo].[v_columns]    Script Date: 14.04.2023 10:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_columns] AS
SELECT
  DB_NAME() AS [Database_Name],
  s.name AS [Schema_Name],
  t.name AS [Table_Name],
  c.name AS [Column_Name],
  ty.name AS [Column_Data_Type],
  uty.name AS [Column_System_Type],
  c.max_length AS [Column_Maximum_Length],
  c.precision AS [Column_Precision],
  c.scale AS [Column_Scale],
  CASE
    pk.PRIMARYKEYCOLUMN
    when c.name Then 'Yes'
    else 'No'
  END AS [Column_Has_Primary_Key],
  CASE
    c.is_nullable
    WHEN 1 THEN 'Yes'
    WHEN 0 THEN 'No'
  END AS [Column_Is_Nullable],
  CASE
    c.is_identity
    WHEN 1 THEN 'Yes'
    WHEN 0 THEN 'No'
  END AS [Column_Has_Identity],
  CASE
    c.is_computed
    WHEN 1 THEN 'Yes'
    WHEN 0 THEN 'No'
  END AS [Column_Is_Computed],
  cc.definition AS [Computed_Column_Definition],
  c.column_id as [Column_Id],
  Case
  	When t.name IN (
    'sysdiagrams',
    '__MigrationHistory',
    'process',
    'api_doc',
    'con_type',
    'con'
  ) Then
  	1
ELSE
	0
END As Is_System_Table
FROM
  sys.tables AS t
  INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id
  INNER JOIN sys.columns AS c ON t.object_id = c.object_id
  INNER JOIN sys.types AS ty ON ty.user_type_id = c.user_type_id
  INNER JOIN sys.types AS uty ON ty.system_type_id = uty.user_type_id
  LEFT JOIN sys.computed_columns AS cc ON t.object_id = cc.object_id
  AND cc.column_id = c.column_id
  LEFT JOIN v_primary_keys AS pk ON t.name = pk.TABLENAME
  AND pk.PRIMARYKEYCOLUMN = c.name
UNION ALL
SELECT
  DB_NAME() AS DatabaseName,
  s.name AS SchemaName,
  t.name AS TableName,
  c.name AS ColumnName,
  ty.name AS ColumnDataType,
  ty.name AS ColumnSystemTypeName,
  c.max_length AS ColumnMaximumLength,
  c.precision AS ColumnPrecision,
  c.scale AS ColumnScale,
  CASE
    c.is_nullable
    WHEN 1 THEN 'Yes'
    WHEN 0 THEN 'No'
  END AS ColumnIsNullable,
  CASE
    c.is_identity
    WHEN 1 THEN 'Yes'
    WHEN 0 THEN 'No'
  END AS ColumnHasIdentity,
  CASE
    pk.PRIMARYKEYCOLUMN
    when c.name Then 'Yes'
    else 'No'
  END AS [Column_Has_Primary_Key],
  CASE
    c.is_computed
    WHEN 1 THEN 'Yes'
    WHEN 0 THEN 'No'
  END AS ColumnIsComputed,
  cc.definition AS [Computed Column Definition],
  c.column_id as [Column_Id],
  Case
  	When t.name IN (
    'sysdiagrams',
    '__MigrationHistory',
    'process',
    'api_doc',
    'con_type',
    'con'
  ) Then
  	1
ELSE
	0
END As Is_System_Table
FROM
  sys.tables AS t
  INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id
  INNER JOIN sys.columns AS c ON t.object_id = c.object_id
  INNER JOIN sys.types AS ty ON ty.user_type_id = c.user_type_id
  AND ty.is_assembly_type = 1
  LEFT JOIN sys.computed_columns AS cc ON t.object_id = cc.object_id
  AND cc.column_id = c.column_id
  LEFT JOIN v_primary_keys AS pk ON t.name = pk.TABLENAME
  AND pk.PRIMARYKEYCOLUMN = c.name;

GO
SET IDENTITY_INSERT [dbo].[api] ON 

GO
INSERT [dbo].[api] ([ID], [name], [description], [active], [host], [user_id], [is_private], [login_process], [logout_process], [jwt_issuer], [jwt_audience], [jwt_key], [uid], [api_code]) VALUES (1, N'api', N'api', 1, N'api.latestapi.com', 1, 1, NULL, NULL, NULL, NULL, NULL, N'88ddc565-7805-4e3d-bcd6-65a141c246a9', 123456)
GO
INSERT [dbo].[api] ([ID], [name], [description], [active], [host], [user_id], [is_private], [login_process], [logout_process], [jwt_issuer], [jwt_audience], [jwt_key], [uid], [api_code]) VALUES (2, N'test', N'test', 1, N'test.latestapi.com', 1, 1, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', 973856)
GO
SET IDENTITY_INSERT [dbo].[api] OFF
GO
SET IDENTITY_INSERT [dbo].[api_doc] ON 

GO
INSERT [dbo].[api_doc] ([ID], [name], [description]) VALUES (1, N't:con', N'con comment')
GO
INSERT [dbo].[api_doc] ([ID], [name], [description]) VALUES (2, N'c:con.name', N'Define name of connection.')
GO
INSERT [dbo].[api_doc] ([ID], [name], [description]) VALUES (3, N'c:con.connection', N'Connection string info.')
GO
INSERT [dbo].[api_doc] ([ID], [name], [description]) VALUES (4, N'ct:ApiReturn', N'Api return objesidir. Eğer veri döndüren bir api çağırırsanız veriyi ''data'' içerisinde sunar.')
GO
INSERT [dbo].[api_doc] ([ID], [name], [description]) VALUES (5, N'cc:ApiReturn.status', N'Durum bilgisini tutar. ''success'' ise sorun yoktur. ')
GO
SET IDENTITY_INSERT [dbo].[api_doc] OFF
GO
SET IDENTITY_INSERT [dbo].[con] ON 

GO
INSERT [dbo].[con] ([ID], [name], [connection], [con_type_id], [active], [user_id], [db_source], [db_user], [db_password], [db_name], [db_port], [db_schema], [comment], [api_id]) VALUES (1, N'emailSignDesigner', N'.\sqlexpress;Initial Catalog=FastApi;User Id=sa;Password=1;TrustServerCertificate=True;', 2, 1, NULL, N'.\sqlexpress', N'sa', N'1', N'emailSignDesigner', NULL, N'dbo', NULL, 1)
GO
INSERT [dbo].[con] ([ID], [name], [connection], [con_type_id], [active], [user_id], [db_source], [db_user], [db_password], [db_name], [db_port], [db_schema], [comment], [api_id]) VALUES (2, N'world', N'.\sqlexpress;Initial Catalog=FastApi;User Id=sa;Password=1;TrustServerCertificate=True;', 3, 1, NULL, N'116.203.183.236', N'world', N'earge1234!', N'world', 3306, N'world', NULL, 1)
GO
INSERT [dbo].[con] ([ID], [name], [connection], [con_type_id], [active], [user_id], [db_source], [db_user], [db_password], [db_name], [db_port], [db_schema], [comment], [api_id]) VALUES (3, N'test', NULL, 1, 1, NULL, N'116.203.183.236', N'postgres', N'p4p8P4DEQb', N'test', 5433, N'dbo', NULL, 1)
GO
INSERT [dbo].[con] ([ID], [name], [connection], [con_type_id], [active], [user_id], [db_source], [db_user], [db_password], [db_name], [db_port], [db_schema], [comment], [api_id]) VALUES (5, N'enibraApi', N'Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 116.203.183.236)(PORT = 1522))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XE)));User Id= MURAT;Password=1', 4, 1, NULL, N'116.203.183.236', N'MURAT', N'1', N'XE', 1522, N'MURAT', NULL, 1)
GO
INSERT [dbo].[con] ([ID], [name], [connection], [con_type_id], [active], [user_id], [db_source], [db_user], [db_password], [db_name], [db_port], [db_schema], [comment], [api_id]) VALUES (6, N'sqlLiteFastApi', NULL, 5, 1, NULL, N'C:\Scriptingo\FastApi\Code\Api\Data\FastApi', NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[con] OFF
GO
SET IDENTITY_INSERT [dbo].[con_table] ON 

GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (1, N'EmailProgram', NULL, 1, 1)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (2, N'Language', NULL, 1, 1)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (3, N'Setting', NULL, 1, 1)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (4, N'SignTemplate', NULL, 1, 1)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (5, N'Translate', NULL, 1, 1)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (6, N'city', NULL, 1, 2)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (7, N'country', NULL, 1, 2)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (8, N'countrylanguage', NULL, 1, 2)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (9, N'test', NULL, 1, 3)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (10, N'APP', NULL, 1, 5)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (11, N'PROCESS', NULL, 1, 5)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (12, N'PROCESS_LOG', NULL, 1, 5)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (13, N'TOKEN', NULL, 1, 5)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (14, N'api', NULL, 1, 6)
GO
INSERT [dbo].[con_table] ([ID], [name], [description], [active], [con_id]) VALUES (15, N'api_doc', NULL, 1, 6)
GO
SET IDENTITY_INSERT [dbo].[con_table] OFF
GO
SET IDENTITY_INSERT [dbo].[con_type] ON 

GO
INSERT [dbo].[con_type] ([ID], [name], [description], [con_str_format]) VALUES (1, N'postgre', N'Postgre', N'Server={db_source};Port={db_port};Database={db_name};User Id={db_user};Password={db_password};')
GO
INSERT [dbo].[con_type] ([ID], [name], [description], [con_str_format]) VALUES (2, N'mssql', N'Ms-SQL', N'Data Source={db_source};Initial Catalog={db_name};User Id={db_user};Password={db_password};TrustServerCertificate=True;')
GO
INSERT [dbo].[con_type] ([ID], [name], [description], [con_str_format]) VALUES (3, N'mysql', N'MySql ', N'server={db_source};Port={db_port};uid={db_user};pwd={db_password};database={db_name}')
GO
INSERT [dbo].[con_type] ([ID], [name], [description], [con_str_format]) VALUES (4, N'oracle', N'ORACLE', N'Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {db_source})(PORT = {db_port}))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = {db_name})));User Id= {db_user};Password={db_password}')
GO
INSERT [dbo].[con_type] ([ID], [name], [description], [con_str_format]) VALUES (5, N'sqlite', N'Sql Lite', N'Data Source={db_source}')
GO
SET IDENTITY_INSERT [dbo].[con_type] OFF
GO
SET IDENTITY_INSERT [dbo].[data_type] ON 

GO
INSERT [dbo].[data_type] ([ID], [name]) VALUES (3, N'decimal')
GO
INSERT [dbo].[data_type] ([ID], [name]) VALUES (2, N'int')
GO
INSERT [dbo].[data_type] ([ID], [name]) VALUES (1, N'string')
GO
SET IDENTITY_INSERT [dbo].[data_type] OFF
GO
SET IDENTITY_INSERT [dbo].[lang] ON 

GO
INSERT [dbo].[lang] ([ID], [name], [culture_code]) VALUES (1, N'Türkçe', N'tr-TR')
GO
INSERT [dbo].[lang] ([ID], [name], [culture_code]) VALUES (2, N'English', N'en-US')
GO
INSERT [dbo].[lang] ([ID], [name], [culture_code]) VALUES (3, N'Deutch', N'de-DE')
GO
SET IDENTITY_INSERT [dbo].[lang] OFF
GO
SET IDENTITY_INSERT [dbo].[process] ON 

GO
INSERT [dbo].[process] ([ID], [name], [description], [sql], [con_id], [active], [request], [response]) VALUES (1, N'delete', N'Test tablosundan veri siler.', N'delete from test where id = @id', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[process] ([ID], [name], [description], [sql], [con_id], [active], [request], [response]) VALUES (2, N'GetAllCountryList', N'Country listesini getirir.', N'select * from Country;', 2, 1, NULL, NULL)
GO
INSERT [dbo].[process] ([ID], [name], [description], [sql], [con_id], [active], [request], [response]) VALUES (3, N'create', N'Yeni bir test kaydı oluşturur.', N'insert into test(name,description) values(@name,@description) ; insert into test(name,description) values(@name,@description);', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[process] ([ID], [name], [description], [sql], [con_id], [active], [request], [response]) VALUES (4, N'to_ok', N'test tablolarının name alanını ok e çevirir.', NULL, NULL, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[process] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

GO
INSERT [dbo].[user] ([ID], [first_name], [last_name], [e_mail], [status], [password], [uid], [wrong_try], [temp_code], [jwt_token]) VALUES (1, N'Murat', N'Uysal', N'opendev.com.tr@gmail.com', 1, N'123456', N'3eef0b27-925b-4179-8f04-24e3a86d462a', 0, N'49f5b80653e449818c44086d6d73d290', NULL)
GO
SET IDENTITY_INSERT [dbo].[user] OFF
GO
SET IDENTITY_INSERT [dbo].[word] ON 

GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (112, N'Dashboard', N'Dashboard', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (113, N'Dashboard', N'Dashboard', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (114, N'Dashboard', N'Dashboard', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (115, N'Welcome to latestapi.com api world. Develop your api with source code in minutes. <a target=''_blank'' href=''{0}''>How?</a>', N'Welcome to latestapi.com api world. Develop your api with source code in minutes. <a target=''_blank'' href=''{0}''>How?</a>', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (116, N'Welcome to latestapi.com api world. Develop your api with source code in minutes. <a target=''_blank'' href=''{0}''>How?</a>', N'Welcome to latestapi.com api world. Develop your api with source code in minutes. <a target=''_blank'' href=''{0}''>How?</a>', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (117, N'Welcome to latestapi.com api world. Develop your api with source code in minutes. <a target=''_blank'' href=''{0}''>How?</a>', N'Welcome to latestapi.com api world. Develop your api with source code in minutes. <a target=''_blank'' href=''{0}''>How?</a>', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (118, N'Add', N'Add', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (119, N'Add', N'Add', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (120, N'Add', N'Add', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (121, N'Menu', N'Menu', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (122, N'Menu', N'Menu', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (123, N'Menu', N'Menu', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (124, N'Api List', N'Api List', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (125, N'Api List', N'Api List', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (126, N'Api List', N'Api List', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (127, N'Api Code', N'Api Code', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (128, N'Api Code', N'Api Code', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (129, N'Api Code', N'Api Code', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (130, N'Api Name', N'Api Name', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (131, N'Api Name', N'Api Name', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (132, N'Api Name', N'Api Name', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (133, N'Description', N'Description', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (134, N'Description', N'Description', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (135, N'Description', N'Description', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (136, N'Host', N'Host', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (137, N'Host', N'Host', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (138, N'Host', N'Host', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (139, N'Api Uid', N'Api Uid', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (140, N'Api Uid', N'Api Uid', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (141, N'Api Uid', N'Api Uid', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (142, N'Active', N'Active', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (143, N'Active', N'Active', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (144, N'Active', N'Active', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (145, N'Manage', N'Manage', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (146, N'Manage', N'Manage', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (147, N'Manage', N'Manage', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (148, N'You can create and manage your apis in api list page.', N'You can create and manage your apis in api list page.', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (149, N'You can create and manage your apis in api list page.', N'You can create and manage your apis in api list page.', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (150, N'You can create and manage your apis in api list page.', N'You can create and manage your apis in api list page.', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (151, N'PageTitle.ProcessList', N'PageTitle.ProcessList', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (152, N'PageTitle.ProcessList', N'PageTitle.ProcessList', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (153, N'PageTitle.ProcessList', N'PageTitle.ProcessList', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (154, N'Api Create', N'Api Create', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (155, N'Api Create', N'Api Create', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (156, N'Api Create', N'Api Create', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (157, N'Api Edit', N'Api Edit', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (158, N'Api Edit', N'Api Edit', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (159, N'Api Edit', N'Api Edit', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (160, N'Search...', N'Search...', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (161, N'Search...', N'Search...', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (162, N'Search...', N'Search...', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (163, N'Your api updated successfully.', N'Your api updated successfully.', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (164, N'Your api updated successfully.', N'Your api updated successfully.', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (165, N'Your api updated successfully.', N'Your api updated successfully.', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (166, N'Is Private', N'Is Private', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (167, N'Is Private', N'Is Private', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (168, N'Is Private', N'Is Private', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (169, N'Edit', N'Edit', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (170, N'Edit', N'Edit', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (171, N'Edit', N'Edit', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (172, N'Your requrested api is not exist or you are not owner.', N'Your requrested api is not exist or you are not owner.', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (173, N'Your requrested api is not exist or you are not owner.', N'Your requrested api is not exist or you are not owner.', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (174, N'Your requrested api is not exist or you are not owner.', N'Your requrested api is not exist or you are not owner.', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (175, N'Databases', N'Databases', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (176, N'Databases', N'Databases', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (177, N'Databases', N'Databases', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (178, N'Databases Processes', N'Databases Processes', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (179, N'Databases Processes', N'Databases Processes', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (180, N'Databases Processes', N'Databases Processes', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (181, N'Configurations', N'Configurations', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (182, N'Configurations', N'Configurations', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (183, N'Configurations', N'Configurations', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (184, N'Api Details', N'Api Details', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (185, N'Api Details', N'Api Details', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (186, N'Api Details', N'Api Details', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (187, N'Api Manage', N'Api Manage', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (188, N'Api Manage', N'Api Manage', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (189, N'Api Manage', N'Api Manage', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (190, N'Configuration', N'Configuration', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (191, N'Configuration', N'Configuration', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (192, N'Configuration', N'Configuration', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (193, N'You can manage configurations.', N'You can manage configurations.', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (194, N'You can manage configurations.', N'You can manage configurations.', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (195, N'You can manage configurations.', N'You can manage configurations.', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (196, N'Processes', N'Processes', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (197, N'Processes', N'Processes', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (198, N'Processes', N'Processes', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (199, N'You can manage processes.', N'You can manage processes.', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (200, N'You can manage processes.', N'You can manage processes.', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (201, N'You can manage processes.', N'You can manage processes.', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (202, N'You can manage databases.', N'You can manage databases.', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (203, N'You can manage databases.', N'You can manage databases.', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (204, N'You can manage databases.', N'You can manage databases.', N'de-DE     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (205, N'Manage Api', N'Manage Api', N'tr-TR     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (206, N'Manage Api', N'Manage Api', N'en-US     ')
GO
INSERT [dbo].[word] ([ID], [word], [value], [language]) VALUES (207, N'Manage Api', N'Manage Api', N'de-DE     ')
GO
SET IDENTITY_INSERT [dbo].[word] OFF
GO
/****** Object:  Index [UK_api_code]    Script Date: 14.04.2023 10:10:08 ******/
ALTER TABLE [dbo].[api] ADD  CONSTRAINT [UK_api_code] UNIQUE NONCLUSTERED 
(
	[api_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_api_name]    Script Date: 14.04.2023 10:10:08 ******/
ALTER TABLE [dbo].[api] ADD  CONSTRAINT [UK_api_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[api_session] ADD  CONSTRAINT [DF_api_session_session_uid]  DEFAULT (newid()) FOR [session_uid]
GO
ALTER TABLE [dbo].[api_session] ADD  CONSTRAINT [DF_api_session_date_create]  DEFAULT (getdate()) FOR [date_create]
GO
ALTER TABLE [dbo].[api_token] ADD  CONSTRAINT [DF_api_token_uid]  DEFAULT (newid()) FOR [uid]
GO
ALTER TABLE [dbo].[api_token] ADD  CONSTRAINT [DF_api_token_active]  DEFAULT ((1)) FOR [active]
GO
