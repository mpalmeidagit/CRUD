USE [CRUD]
GO

CREATE TABLE [dbo].[tab_cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](100) NULL,
	[email] [varchar](50) NULL,
	[cpf] [varchar](20) NULL,
	[telefone] [varchar](20) NULL,
	[cep] [varchar](12) NULL,
	[estado] [varchar](50) NULL,
	[cidade] [varchar](50) NULL,
	[bairro] [varchar](50) NULL,
	[endereco] [varchar](150) NULL,
	[ativo] [bit] NULL,
 CONSTRAINT [PK_tab_cliente] PRIMARY KEY([id])
 )
GO

CREATE TABLE [dbo].[tab_recibo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data] [date] NOT NULL,
	[servico_id] [int] NOT NULL,
	[cliente_id] [int] NOT NULL,
	[ativo] [bit] NULL,
 CONSTRAINT [PK_tab_recibo] PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[tab_servico](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](100) NULL,
	[valor] [decimal](10, 2) NULL,
	[ativo] [bit] NULL,
 CONSTRAINT [PK_tab_servico] PRIMARY KEY ([id])
 )
GO

CREATE TABLE [dbo].[tab_perfil](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](100) NULL,
	[ativo] [bit] NULL,
 CONSTRAINT [PK_tab_perfil] PRIMARY KEY ([id])
 )
 GO

  CREATE TABLE [dbo].[tab_usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NULL,
	[senha] [varchar](32) NULL,
	[nome] [varchar](100) NULL,
	[email] [varchar](100) NULL,
	[id_perfil] [int]  NOT NULL,
	[ativo] [bit] NULL,	
 CONSTRAINT [PK_tab_usuario] PRIMARY KEY ([id])
 )
 GO
 ALTER TABLE [dbo].[tab_usuario]  WITH CHECK ADD FOREIGN KEY([id_perfil]) REFERENCES [dbo].[tab_perfil] ([id])
GO
ALTER TABLE [dbo].[tab_recibo]  WITH CHECK ADD FOREIGN KEY([cliente_id]) REFERENCES [dbo].[tab_cliente] ([id])
GO
ALTER TABLE [dbo].[tab_recibo]  WITH CHECK ADD FOREIGN KEY([servico_id]) REFERENCES [dbo].[tab_servico] ([id])
GO

