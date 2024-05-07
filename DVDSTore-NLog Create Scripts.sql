USE dvdstore

go

/****** Object:  Table [dbo].[Logs]    Script Date: 5/6/2024 7:48:12 PM ******/
SET ansi_nulls ON

go

SET quoted_identifier ON

go

CREATE TABLE [dbo].[logs]
  (
     [logid]          [INT] IDENTITY(1, 1) NOT NULL,
     [level]          [VARCHAR](max) NOT NULL,
     [callsite]       [VARCHAR](max) NOT NULL,
     [type]           [VARCHAR](max) NOT NULL,
     [message]        [VARCHAR](max) NOT NULL,
     [stacktrace]     [VARCHAR](max) NOT NULL,
     [innerexception] [VARCHAR](max) NOT NULL,
     [additionalinfo] [VARCHAR](max) NOT NULL,
     [loggedondate]   [DATETIME] NOT NULL,
     CONSTRAINT [pk_logs] PRIMARY KEY CLUSTERED ( [logid] ASC )WITH (pad_index =
     OFF, statistics_norecompute = OFF, ignore_dup_key = OFF, allow_row_locks =
     on, allow_page_locks = on, optimize_for_sequential_key = OFF) ON [PRIMARY]
  )
ON [PRIMARY]
textimage_on [PRIMARY]

go

ALTER TABLE [dbo].[logs]
  ADD CONSTRAINT [df_logs_loggedondate] DEFAULT (Getdate()) FOR [LoggedOnDate]

go

USE dvdstore

go

/****** Object:  StoredProcedure [dbo].[InsertLog]    Script Date: 5/6/2024 7:37:12 PM ******/
SET ansi_nulls ON

go

SET quoted_identifier ON

go

/*
                Create InsertLog stored procedure
*/
CREATE PROCEDURE [dbo].[Insertlog] (@level          VARCHAR(max),
                                    @callSite       VARCHAR(max),
                                    @type           VARCHAR(max),
                                    @message        VARCHAR(max),
                                    @stackTrace     VARCHAR(max),
                                    @innerException VARCHAR(max),
                                    @additionalInfo VARCHAR(max))
AS
    INSERT INTO dbo.logs
                ([level],
                 callsite,
                 [type],
                 [message],
                 stacktrace,
                 innerexception,
                 additionalinfo)
    VALUES      ( @level,
                  @callSite,
                  @type,
                  @message,
                  @stackTrace,
                  @innerException,
                  @additionalInfo )

go 