SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContextKey] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[id] [int] NOT NULL,
	[make] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[model_name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[model_trim] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[model_year] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[body_style] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_position] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_cc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_num_cyl] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_type] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_valves_per_cyl] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_power_ps] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_power_rpm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_torque_nm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_torque_rpm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_bore_mm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_stroke_mm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_compression] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[engine_fuel] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[top_speed_kph] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[zero_to_100_kph] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[drive_type] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[transmission_type] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[seats] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[doors] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[weight_kg] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[length_mm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[width_mm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[height_mm] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[wheelbase] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lkm_hwy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lkm_mixed] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lkm_city] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[fuel_capacity_l] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[sold_in_us] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[co2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[make_display] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [Makes] ON [dbo].[Cars]
(
	[make] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [Models] ON [dbo].[Cars]
(
	[model_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [Trims] ON [dbo].[Cars]
(
	[model_trim] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [Years] ON [dbo].[Cars]
(
	[model_year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[getCarsByYearMakeModelTrim]
@model_year nvarchar(10)='', @make nvarchar(50)='', @model_name nvarchar(50)='', @model_trim nvarchar(128)=''
as
select * from dbo.Cars with(nolock)
where (
(@model_year is null or @model_year =' ' or model_year = @model_year) and
(@make is null or @make =' ' or make = @make) and
(@model_name is null or @model_name =' ' or model_name = @model_name) and
(@model_trim is null or @model_trim =' ' or model_trim = @model_trim))



GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[getMakeByYear]
@model_year nvarchar(10)=''
as
select distinct make from dbo.Cars with(nolock)
where model_year = @model_year
order by make asc;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[getModelByYearMake]
@model_year nvarchar(10)='', @make nvarchar(50)=''
as
select distinct model_name from dbo.Cars with(nolock)
where model_year = @model_year and make = @make
order by model_name asc;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[getTrimByYearMakeModel]
@model_year nvarchar(10)='', @make nvarchar(50)='', @model_name nvarchar(50)=''
as
select distinct model_trim from dbo.Cars with(nolock)
where model_trim != '' and model_trim is not null and model_year = @model_year and make = @make and model_name = @model_name
order by model_trim asc

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[getYears]
as
select distinct model_year from dbo.Cars with (nolock)
order by model_year desc;
GO
