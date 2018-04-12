/******** DMA Schema Migration Deployment Script      Script Date: 4/3/2018 6:12:10 PM ********/

/****** Object:  Table [dbo].[Geolocation]    Script Date: 4/3/2018 6:12:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Geolocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Geolocation](
	[locationId] [int] NOT NULL,
	[longitude] [decimal](8, 5) NULL,
	[latitude] [decimal](8, 5) NULL,
	[Description] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Color] [char](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK__Geolocat__30646B6E790C49F3] PRIMARY KEY CLUSTERED 
(
	[locationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Geolocati__Descr__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Geolocation] ADD  CONSTRAINT [DF__Geolocati__Descr__74AE54BC]  DEFAULT ('') FOR [Description]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Geolocati__Color__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Geolocation] ADD  CONSTRAINT [DF__Geolocati__Color__75A278F5]  DEFAULT ('#228B22') FOR [Color]
END

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [varchar](70) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDriver] [bit] NULL,
	[Gender] [char](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LocationId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[Age] [tinyint] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND name = N'UserNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__LastN__19DFD96B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__tmp_ms_xx__LastN__19DFD96B]  DEFAULT ('') FOR [LastName]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__First__1AD3FDA4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__tmp_ms_xx__First__1AD3FDA4]  DEFAULT ('') FOR [FirstName]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__IsDri__1BC821DD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__tmp_ms_xx__IsDri__1BC821DD]  DEFAULT ((0)) FOR [IsDriver]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__AspNetUse__IsAct__52E34C9D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__AspNetUse__IsAct__52E34C9D]  DEFAULT ((1)) FOR [IsActive]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__AspNetUsers__Age__5F7E2DAC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__AspNetUsers__Age__5F7E2DAC]  DEFAULT ((18)) FOR [Age]
END

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND name = N'RoleNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_RoleId')
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Schedule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Schedule](
	[ScheduleId] [int] NOT NULL,
	[CheckedSunday] [bit] NOT NULL,
	[CheckedMonday] [bit] NOT NULL,
	[CheckedTuesday] [bit] NOT NULL,
	[CheckedWednesday] [bit] NOT NULL,
	[CheckedThursday] [bit] NOT NULL,
	[CheckedFriday] [bit] NOT NULL,
	[CheckedSaturday] [bit] NOT NULL,
 CONSTRAINT [PK__Schedule__9C8A5B4979EE919A] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Schedule__Checke__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF__Schedule__Checke__76969D2E]  DEFAULT ((0)) FOR [CheckedSunday]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Schedule__Checke__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF__Schedule__Checke__778AC167]  DEFAULT ((0)) FOR [CheckedMonday]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Schedule__Checke__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF__Schedule__Checke__787EE5A0]  DEFAULT ((0)) FOR [CheckedTuesday]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Schedule__Checke__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF__Schedule__Checke__797309D9]  DEFAULT ((0)) FOR [CheckedWednesday]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Schedule__Checke__7A672E12]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF__Schedule__Checke__7A672E12]  DEFAULT ((0)) FOR [CheckedThursday]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Schedule__Checke__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF__Schedule__Checke__7B5B524B]  DEFAULT ((0)) FOR [CheckedFriday]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Schedule__Checke__7C4F7684]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF__Schedule__Checke__7C4F7684]  DEFAULT ((0)) FOR [CheckedSaturday]
END

GO
/****** Object:  Table [dbo].[Route]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Route]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Route](
	[DriverId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RouteNum] [int] NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[DestinationId] [int] NULL,
	[DepartTime] [time](0) NOT NULL,
	[ArriveTime] [time](0) NOT NULL,
	[MaxSeatCount] [int] NULL,
 CONSTRAINT [PK__tmp_ms_x__F6AB1F8A4C7EDC4C] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC,
	[RouteNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderKey] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RouteSlot]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteSlot]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RouteSlot](
	[DriverId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RouteNum] [int] NOT NULL,
	[RiderId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK__tmp_ms_x__97D66DE68FDC9777] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC,
	[RouteNum] ASC,
	[RiderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContextKey] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ChatThread]    Script Date: 4/3/2018 6:12:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatThread]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChatThread](
	[ThreadId] [bigint] NOT NULL,
	[User1Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[User2Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [datetime2](0) NOT NULL,
	[LastPostedOnDate] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_ChatThread] PRIMARY KEY CLUSTERED 
(
	[ThreadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ChatMessage]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChatMessage](
	[ThreadId] [bigint] NOT NULL,
	[MessageOrder] [int] NOT NULL,
	[FromUserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ToUserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateSent] [datetime2](0) NOT NULL,
	[IsBlocked] [bit] NOT NULL,
	[IsSent] [bit] NOT NULL,
	[IsFailed] [bit] NOT NULL,
	[MessageContent] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ChatMessage] PRIMARY KEY CLUSTERED 
(
	[ThreadId] ASC,
	[MessageOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__IsBlo__41B8C09B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChatMessage] ADD  CONSTRAINT [DF__tmp_ms_xx__IsBlo__41B8C09B]  DEFAULT ((0)) FOR [IsBlocked]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__IsSen__42ACE4D4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChatMessage] ADD  CONSTRAINT [DF__tmp_ms_xx__IsSen__42ACE4D4]  DEFAULT ((0)) FOR [IsSent]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__IsFai__43A1090D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChatMessage] ADD  CONSTRAINT [DF__tmp_ms_xx__IsFai__43A1090D]  DEFAULT ((0)) FOR [IsFailed]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__Messa__44952D46]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChatMessage] ADD  CONSTRAINT [DF__tmp_ms_xx__Messa__44952D46]  DEFAULT ('') FOR [MessageContent]
END

GO
/****** Object:  Table [dbo].[RideRequestStatus]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RideRequestStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RideRequestStatus](
	[RequestStatusId] [tinyint] NOT NULL,
	[RequestStatusName] [char](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_RequestStatusId] PRIMARY KEY CLUSTERED 
(
	[RequestStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProfilePicture]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProfilePicture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProfilePicture](
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsVehiclePicture] [bit] NOT NULL,
	[PictureContent] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_ProfilePicture] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[IsVehiclePicture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RideRequest]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RideRequest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RideRequest](
	[RiderId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RequestNum] [int] NOT NULL,
	[ScheduleId] [int] NULL,
	[PickupLocationId] [int] NULL,
	[DropoffLocationId] [int] NULL,
	[DepartingTime] [time](0) NOT NULL,
	[ArrivalTime] [time](0) NOT NULL,
	[RequestStatusId] [tinyint] NOT NULL,
 CONSTRAINT [PK_RideRequest] PRIMARY KEY CLUSTERED 
(
	[RiderId] ASC,
	[RequestNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculateGeolocationDistanceKm]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CalculateGeolocationDistanceKm]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[fn_CalculateGeolocationDistanceKm]
(
	@longitude1 DECIMAL(8,5),
	@latitude1 DECIMAL(8,5),
	@longitude2 DECIMAL(8,5),
	@latitude2 DECIMAL(8,5)
)
RETURNS DECIMAL(9,4)
AS
BEGIN
	-- Uses the haversine formula to get the distance between two objects
	-- Source: https://en.wikipedia.org/wiki/Haversine_formula at the "explicit inverse"

	-- Convert degrees into radians
	SET @longitude1 *= (PI() / 180.0);
	SET @longitude2 *= (PI() / 180.0);
	SET @latitude1 *= (PI() / 180.0);
	SET @latitude2 *= (PI() / 180.0);

	-- Apply haversine formula
	-- 1) Inner part
	DECLARE @calculation DECIMAL(8,5) = SQUARE(SIN((@latitude2 - @latitude1) / 2)) +
		(COS(@latitude1) * COS(@latitude2) *
		SQUARE(SIN((@longitude2 - @longitude1) / 2)));

	SET @calculation = SQRT(@calculation);

	-- 2) Outer part
	-- Average of earth''s radius in km is 6367.4445 (3956.54658047 in miles)
	SET @calculation = (2 * 6367.4445) * ASIN(@calculation);

	RETURN @calculation
END' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculateGeolocationDistanceMi]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CalculateGeolocationDistanceMi]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[fn_CalculateGeolocationDistanceMi]
(
	@longitude1 DECIMAL(8,5),
	@latitude1 DECIMAL(8,5),
	@longitude2 DECIMAL(8,5),
	@latitude2 DECIMAL(8,5)
)
RETURNS DECIMAL(9,4)
AS
BEGIN
	DECLARE @distKm DECIMAL(9,4) = [dbo].[fn_CalculateGeolocationDistanceKm](@longitude1, @latitude1, @longitude2, @latitude2);

	-- convert kilometers to miles
	RETURN @distKm * 0.62137119;
END' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[fn_UserGeolocation]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_UserGeolocation]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[fn_UserGeolocation]
(
	@userId NVARCHAR(128)
)
RETURNS @returntable TABLE
(
	UserLat DECIMAL(8,5) NULL,
	UserLong DECIMAL(8,5) NULL
)
AS
BEGIN
	INSERT @returntable
		SELECT TOP 1
			g.latitude,
			g.longitude
		FROM AspNetUsers AS au
			FULL OUTER JOIN Geolocation AS g ON g.locationId = au.LocationId
			WHERE au.Id = @userId;
	RETURN
END' 
END

GO
/****** Object:  View [dbo].[vw_UserLocations]    Script Date: 4/3/2018 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_UserLocations]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW vw_UserLocations AS
	SELECT
		g.LocationId,
		au.UserName,
		au.IsDriver,
		au.FirstName,
		au.LastName,
		g.Longitude,
		g.Latitude,
		g.Color,
		g.[Description]
	FROM [AspNetUsers] AS au
		RIGHT JOIN Geolocation AS g
		ON au.LocationId = g.LocationId
		WHERE au.IsActive = 1;' 
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUsers_LocationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUsers]'))
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Geolocation] ([locationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUsers_LocationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUsers]'))
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_LocationId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Route__Destinati__2CF2ADDF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Route]'))
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK__Route__Destinati__2CF2ADDF] FOREIGN KEY([DestinationId])
REFERENCES [dbo].[Geolocation] ([locationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Route__Destinati__2CF2ADDF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Route]'))
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK__Route__Destinati__2CF2ADDF]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Route__DriverId__2EDAF651]') AND parent_object_id = OBJECT_ID(N'[dbo].[Route]'))
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK__Route__DriverId__2EDAF651] FOREIGN KEY([DriverId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Route__DriverId__2EDAF651]') AND parent_object_id = OBJECT_ID(N'[dbo].[Route]'))
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK__Route__DriverId__2EDAF651]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Route__ScheduleI__2DE6D218]') AND parent_object_id = OBJECT_ID(N'[dbo].[Route]'))
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK__Route__ScheduleI__2DE6D218] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedule] ([ScheduleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Route__ScheduleI__2DE6D218]') AND parent_object_id = OBJECT_ID(N'[dbo].[Route]'))
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK__Route__ScheduleI__2DE6D218]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__RouteSlot__2FCF1A8A]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteSlot]'))
ALTER TABLE [dbo].[RouteSlot]  WITH CHECK ADD  CONSTRAINT [FK__RouteSlot__2FCF1A8A] FOREIGN KEY([DriverId], [RouteNum])
REFERENCES [dbo].[Route] ([DriverId], [RouteNum])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__RouteSlot__2FCF1A8A]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteSlot]'))
ALTER TABLE [dbo].[RouteSlot] CHECK CONSTRAINT [FK__RouteSlot__2FCF1A8A]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__RouteSlot__Rider__30C33EC3]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteSlot]'))
ALTER TABLE [dbo].[RouteSlot]  WITH CHECK ADD  CONSTRAINT [FK__RouteSlot__Rider__30C33EC3] FOREIGN KEY([RiderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__RouteSlot__Rider__30C33EC3]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteSlot]'))
ALTER TABLE [dbo].[RouteSlot] CHECK CONSTRAINT [FK__RouteSlot__Rider__30C33EC3]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatThread_User1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatThread]'))
ALTER TABLE [dbo].[ChatThread]  WITH CHECK ADD  CONSTRAINT [FK_ChatThread_User1Id] FOREIGN KEY([User1Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatThread_User1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatThread]'))
ALTER TABLE [dbo].[ChatThread] CHECK CONSTRAINT [FK_ChatThread_User1Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatThread_User2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatThread]'))
ALTER TABLE [dbo].[ChatThread]  WITH CHECK ADD  CONSTRAINT [FK_ChatThread_User2Id] FOREIGN KEY([User2Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatThread_User2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatThread]'))
ALTER TABLE [dbo].[ChatThread] CHECK CONSTRAINT [FK_ChatThread_User2Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatMessage_FromUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatMessage]'))
ALTER TABLE [dbo].[ChatMessage]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessage_FromUserId] FOREIGN KEY([FromUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatMessage_FromUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatMessage]'))
ALTER TABLE [dbo].[ChatMessage] CHECK CONSTRAINT [FK_ChatMessage_FromUserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatMessage_ThreadId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatMessage]'))
ALTER TABLE [dbo].[ChatMessage]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessage_ThreadId] FOREIGN KEY([ThreadId])
REFERENCES [dbo].[ChatThread] ([ThreadId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatMessage_ThreadId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatMessage]'))
ALTER TABLE [dbo].[ChatMessage] CHECK CONSTRAINT [FK_ChatMessage_ThreadId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatMessage_ToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatMessage]'))
ALTER TABLE [dbo].[ChatMessage]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessage_ToUserId] FOREIGN KEY([ToUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatMessage_ToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatMessage]'))
ALTER TABLE [dbo].[ChatMessage] CHECK CONSTRAINT [FK_ChatMessage_ToUserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfilePicture_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfilePicture]'))
ALTER TABLE [dbo].[ProfilePicture]  WITH CHECK ADD  CONSTRAINT [FK_ProfilePicture_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfilePicture_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfilePicture]'))
ALTER TABLE [dbo].[ProfilePicture] CHECK CONSTRAINT [FK_ProfilePicture_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_DropoffLocationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest]  WITH CHECK ADD  CONSTRAINT [FK_RideRequest_DropoffLocationId] FOREIGN KEY([DropoffLocationId])
REFERENCES [dbo].[Geolocation] ([locationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_DropoffLocationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest] CHECK CONSTRAINT [FK_RideRequest_DropoffLocationId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_PickupLocationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest]  WITH CHECK ADD  CONSTRAINT [FK_RideRequest_PickupLocationId] FOREIGN KEY([PickupLocationId])
REFERENCES [dbo].[Geolocation] ([locationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_PickupLocationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest] CHECK CONSTRAINT [FK_RideRequest_PickupLocationId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_RequestStatusId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest]  WITH CHECK ADD  CONSTRAINT [FK_RideRequest_RequestStatusId] FOREIGN KEY([RequestStatusId])
REFERENCES [dbo].[RideRequestStatus] ([RequestStatusId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_RequestStatusId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest] CHECK CONSTRAINT [FK_RideRequest_RequestStatusId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_RiderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest]  WITH CHECK ADD  CONSTRAINT [FK_RideRequest_RiderId] FOREIGN KEY([RiderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_RiderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest] CHECK CONSTRAINT [FK_RideRequest_RiderId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_ScheduleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest]  WITH CHECK ADD  CONSTRAINT [FK_RideRequest_ScheduleId] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedule] ([ScheduleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RideRequest_ScheduleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RideRequest]'))
ALTER TABLE [dbo].[RideRequest] CHECK CONSTRAINT [FK_RideRequest_ScheduleId]
GO

