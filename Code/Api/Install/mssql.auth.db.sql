
CREATE TABLE [dbo].[token](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[date_create] [datetime] NOT NULL,
	[date_end] [datetime] NULL,
	[jwt] [nvarchar](max) NOT NULL,
	[user_id] [bigint] NOT NULL
 CONSTRAINT [PK_api_session] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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



