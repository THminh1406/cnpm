USE [master]
GO
/****** Object:  Database [management_db]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE DATABASE [management_db]

go
use management_db
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [management_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [management_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [management_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [management_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [management_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [management_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [management_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [management_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [management_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [management_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [management_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [management_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [management_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [management_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [management_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [management_db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [management_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [management_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [management_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [management_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [management_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [management_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [management_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [management_db] SET RECOVERY FULL 
GO
ALTER DATABASE [management_db] SET  MULTI_USER 
GO
ALTER DATABASE [management_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [management_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [management_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [management_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [management_db] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'management_db', N'ON'
GO
ALTER DATABASE [management_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [management_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200)
GO
USE [management_db]
GO
/****** Object:  Table [dbo].[attendance]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[attendance](
	[id_attendance] [int] IDENTITY(1,1) NOT NULL,
	[id_student] [int] NOT NULL,
	[attendance_date] [date] NOT NULL,
	[attendance_status] [nvarchar](20) NOT NULL,
	[attendance_method] [nvarchar](10) NOT NULL,
	[attendance_notes] [nvarchar](max) NULL,
	[recorded_at] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_attendance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[audit_log]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[audit_log](
	[id_log] [int] IDENTITY(1,1) NOT NULL,
	[log_table_name] [nvarchar](50) NOT NULL,
	[log_record_id] [int] NOT NULL,
	[log_action] [nvarchar](10) NOT NULL,
	[log_old_value] [nvarchar](max) NULL,
	[log_new_value] [nvarchar](max) NULL,
	[log_user_id] [int] NULL,
	[log_timestamp] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_log] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[id_category] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](100) NOT NULL,
	[id_teacher] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[classes]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[classes](
	[id_class] [int] IDENTITY(1,1) NOT NULL,
	[class_name] [nvarchar](100) NOT NULL,
	[id_teacher] [int] NULL,
	[class_description] [nvarchar](max) NULL,
	[created_at] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[grades]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grades](
	[id_grade] [int] IDENTITY(1,1) NOT NULL,
	[id_student] [int] NOT NULL,
	[id_subject] [int] NOT NULL,
	[grade_period] [nvarchar](20) NOT NULL,
	[grade_score] [decimal](5, 2) NULL,
	[grade_date] [date] NOT NULL,
	[semester] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_grade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notes]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notes](
	[id_note] [int] IDENTITY(1,1) NOT NULL,
	[id_student] [int] NOT NULL,
	[id_teacher] [int] NOT NULL,
	[note_content] [nvarchar](max) NOT NULL,
	[note_type] [nvarchar](20) NOT NULL,
	[created_at] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_note] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz_Content]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz_Content](
	[id_quiz] [int] NOT NULL,
	[id_vocabulary] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_quiz] ASC,
	[id_vocabulary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quizzes]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quizzes](
	[id_quiz] [int] IDENTITY(1,1) NOT NULL,
	[id_teacher] [int] NOT NULL,
	[id_class] [int] NOT NULL,
	[quiz_title] [nvarchar](255) NOT NULL,
	[quiz_type] [nvarchar](50) NOT NULL,
	[quiz_config_json] [nvarchar](max) NULL,
	[created_at] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_quiz] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[student_notes]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student_notes](
	[id_note] [int] IDENTITY(1,1) NOT NULL,
	[id_student] [int] NOT NULL,
	[note_content] [nvarchar](max) NULL,
	[note_type] [nvarchar](50) NULL,
	[priority] [nvarchar](20) NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_note] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[students]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id_student] [int] IDENTITY(1,1) NOT NULL,
	[student_code] [nvarchar](50) NOT NULL,
	[full_name] [nvarchar](255) NOT NULL,
	[date_of_birth] [date] NOT NULL,
	[gender] [nvarchar](10) NULL,
	[id_class] [int] NOT NULL,
	[created_at] [datetime2](7) NULL,
	[ethnicity] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_student] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_students_student_code] UNIQUE NONCLUSTERED 
(
	[student_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subjects]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subjects](
	[id_subject] [int] IDENTITY(1,1) NOT NULL,
	[subject_name] [nvarchar](100) NOT NULL,
	[subject_code] [nvarchar](20) NULL,
    [id_teacher] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_subject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_subjects_subject_code] UNIQUE NONCLUSTERED 
(
	[subject_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[teachers]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[teachers](
	[id_teacher] [int] IDENTITY(1,1) NOT NULL,
	[full_name] [nvarchar](255) NOT NULL,
	[username] [nvarchar](100) NOT NULL,
	[password_hash] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NULL,
	[user_role] [nvarchar](20) NOT NULL,
	[phone_number] [nvarchar](10) NOT NULL,
	[otp_code] [nvarchar](6) NULL,
	[is_active] [bit] NOT NULL,
	[IsManualDeactivated] [bit] NOT NULL,
	[created_at] [datetime2](7) NULL,
	[remember_token] [nvarchar](255) NULL,
	[remember_expires] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_teacher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_teachers_email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_teachers_username] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vocabulary]    Script Date: 25/11/2025 12:49:30 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vocabulary](
	[id_vocabulary] [int] IDENTITY(1,1) NOT NULL,
	[WordText] [nvarchar](100) NOT NULL,
	[WordImage] [varbinary](max) NULL,
	[id_category] [int] NULL,
	[id_teacher] [int] NULL,
	[created_at] [datetime2](7) NULL,
	[VocabType] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_vocabulary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [idx_attendance_date]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_attendance_date] ON [dbo].[attendance]
(
	[attendance_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_id_student_date]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_id_student_date] ON [dbo].[attendance]
(
	[id_student] ASC,
	[attendance_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_log_table_action]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_log_table_action] ON [dbo].[audit_log]
(
	[log_table_name] ASC,
	[log_action] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_log_timestamp]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_log_timestamp] ON [dbo].[audit_log]
(
	[log_timestamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_class_name]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_class_name] ON [dbo].[classes]
(
	[class_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_id_teacher]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_id_teacher] ON [dbo].[classes]
(
	[id_teacher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_grade_date]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_grade_date] ON [dbo].[grades]
(
	[grade_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_id_student_subject_period]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_id_student_subject_period] ON [dbo].[grades]
(
	[id_student] ASC,
	[id_subject] ASC,
	[grade_period] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_id_student]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_id_student] ON [dbo].[notes]
(
	[id_student] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_id_teacher_date]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_id_teacher_date] ON [dbo].[notes]
(
	[id_teacher] ASC,
	[created_at] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_date_of_birth]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_date_of_birth] ON [dbo].[students]
(
	[date_of_birth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_full_name]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_full_name] ON [dbo].[students]
(
	[full_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_id_class]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_id_class] ON [dbo].[students]
(
	[id_class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_student_code]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_student_code] ON [dbo].[students]
(
	[student_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_subject_name]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_subject_name] ON [dbo].[subjects]
(
	[subject_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_user_role]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_user_role] ON [dbo].[teachers]
(
	[user_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_username]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [idx_username] ON [dbo].[teachers]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_teachers_remember_token]    Script Date: 25/11/2025 12:49:30 CH ******/
CREATE NONCLUSTERED INDEX [IX_teachers_remember_token] ON [dbo].[teachers]
(
	[remember_token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[attendance] ADD  CONSTRAINT [DF_attendance_method]  DEFAULT ('manual') FOR [attendance_method]
GO
ALTER TABLE [dbo].[attendance] ADD  DEFAULT (getdate()) FOR [recorded_at]
GO
ALTER TABLE [dbo].[audit_log] ADD  DEFAULT (getdate()) FOR [log_timestamp]
GO
ALTER TABLE [dbo].[classes] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[grades] ADD  DEFAULT ('HocKy1') FOR [semester]
GO
ALTER TABLE [dbo].[notes] ADD  CONSTRAINT [DF_notes_type]  DEFAULT ('general') FOR [note_type]
GO
ALTER TABLE [dbo].[notes] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Quizzes] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[student_notes] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[teachers] ADD  DEFAULT ('teacher') FOR [user_role]
GO
ALTER TABLE [dbo].[teachers] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[teachers] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Vocabulary] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Vocabulary] ADD  DEFAULT ('Word') FOR [VocabType]
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [FK_attendance_id_student] FOREIGN KEY([id_student])
REFERENCES [dbo].[students] ([id_student])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [FK_attendance_id_student]
GO
ALTER TABLE [dbo].[classes]  WITH CHECK ADD  CONSTRAINT [FK_classes_id_teacher] FOREIGN KEY([id_teacher])
REFERENCES [dbo].[teachers] ([id_teacher])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[classes] CHECK CONSTRAINT [FK_classes_id_teacher]
GO
ALTER TABLE [dbo].[grades]  WITH CHECK ADD  CONSTRAINT [FK_grades_id_student] FOREIGN KEY([id_student])
REFERENCES [dbo].[students] ([id_student])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[grades] CHECK CONSTRAINT [FK_grades_id_student]
GO
ALTER TABLE [dbo].[grades]  WITH CHECK ADD  CONSTRAINT [FK_grades_id_subject] FOREIGN KEY([id_subject])
REFERENCES [dbo].[subjects] ([id_subject])
GO

ALTER TABLE [dbo].[subjects] 
WITH CHECK ADD CONSTRAINT [FK_subjects_id_teacher] FOREIGN KEY([id_teacher])
REFERENCES [dbo].[teachers] ([id_teacher])
ON DELETE SET NULL; -- Nếu xóa giáo viên, id_teacher trong môn học sẽ về NULL
GO
ALTER TABLE [dbo].[subjects] CHECK CONSTRAINT [FK_subjects_id_teacher];
GO

ALTER TABLE [dbo].[grades] CHECK CONSTRAINT [FK_grades_id_subject]
GO
ALTER TABLE [dbo].[notes]  WITH CHECK ADD  CONSTRAINT [FK_notes_id_student] FOREIGN KEY([id_student])
REFERENCES [dbo].[students] ([id_student])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[notes] CHECK CONSTRAINT [FK_notes_id_student]
GO
ALTER TABLE [dbo].[notes]  WITH CHECK ADD  CONSTRAINT [FK_notes_id_teacher] FOREIGN KEY([id_teacher])
REFERENCES [dbo].[teachers] ([id_teacher])
GO
ALTER TABLE [dbo].[notes] CHECK CONSTRAINT [FK_notes_id_teacher]
GO
ALTER TABLE [dbo].[Quiz_Content]  WITH CHECK ADD FOREIGN KEY([id_quiz])
REFERENCES [dbo].[Quizzes] ([id_quiz])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quiz_Content]  WITH CHECK ADD FOREIGN KEY([id_vocabulary])
REFERENCES [dbo].[Vocabulary] ([id_vocabulary])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quizzes]  WITH CHECK ADD FOREIGN KEY([id_class])
REFERENCES [dbo].[classes] ([id_class])
GO
ALTER TABLE [dbo].[student_notes]  WITH CHECK ADD FOREIGN KEY([id_student])
REFERENCES [dbo].[students] ([id_student])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[students]  WITH CHECK ADD  CONSTRAINT [FK_students_id_class] FOREIGN KEY([id_class])
REFERENCES [dbo].[classes] ([id_class])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[students] CHECK CONSTRAINT [FK_students_id_class]
GO
ALTER TABLE [dbo].[Vocabulary]  WITH CHECK ADD FOREIGN KEY([id_category])
REFERENCES [dbo].[Categories] ([id_category])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [CK_attendance_method] CHECK  (([attendance_method]='manual' OR [attendance_method]='qr'))
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [CK_attendance_method]
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [CK_attendance_status] CHECK  (([attendance_status]='late' OR [attendance_status]='absent_unpermitted' OR [attendance_status]='absent_permitted' OR [attendance_status]='present'))
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [CK_attendance_status]
GO
ALTER TABLE [dbo].[audit_log]  WITH CHECK ADD  CONSTRAINT [CK_audit_log_action] CHECK  (([log_action]='DELETE' OR [log_action]='UPDATE' OR [log_action]='INSERT'))
GO
ALTER TABLE [dbo].[audit_log] CHECK CONSTRAINT [CK_audit_log_action]
GO
ALTER TABLE [dbo].[grades]  WITH CHECK ADD  CONSTRAINT [CK_grades_period] CHECK  (([grade_period]='Final' OR [grade_period]='Mid'))
GO
ALTER TABLE [dbo].[grades] CHECK CONSTRAINT [CK_grades_period]
GO
ALTER TABLE [dbo].[notes]  WITH CHECK ADD  CONSTRAINT [CK_notes_type] CHECK  (([note_type]='general' OR [note_type]='academic' OR [note_type]='behavior'))
GO
ALTER TABLE [dbo].[notes] CHECK CONSTRAINT [CK_notes_type]
GO
ALTER TABLE [dbo].[students]  WITH CHECK ADD  CONSTRAINT [CK_students_gender] CHECK  (([gender]='other' OR [gender]='female' OR [gender]='male'))
GO
ALTER TABLE [dbo].[students] CHECK CONSTRAINT [CK_students_gender]
GO
ALTER TABLE [dbo].[teachers]  WITH CHECK ADD CHECK  (([user_role]='admin' OR [user_role]='teacher'))
GO
/****** Object:  StoredProcedure [dbo].[sp_ApproveTeacher]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ApproveTeacher] @id INT AS BEGIN SET NOCOUNT ON; UPDATE dbo.teachers SET is_active = 1, IsManualDeactivated = 0 WHERE id_teacher = @id; END 
GO
/****** Object:  StoredProcedure [dbo].[sp_AssignTeacherToClass]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Procedure: assign a teacher to a class
CREATE OR ALTER PROCEDURE [dbo].[sp_AssignTeacherToClass]
    @classId INT,
    @teacherId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Clear previous assignment(s) for this teacher (except the target class)
        UPDATE dbo.classes
        SET id_teacher = NULL
        WHERE id_teacher = @teacherId AND id_class <> @classId;

        -- Assign teacher to requested class
        UPDATE dbo.classes
        SET id_teacher = @teacherId
        WHERE id_class = @classId;

        COMMIT TRANSACTION;
        SELECT @@ROWCOUNT AS rowsAffected;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckStudentCodeExists]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CheckStudentCodeExists]
    @code NVARCHAR(50)
AS
BEGIN
    SELECT COUNT(*) FROM dbo.students WHERE student_code = @code;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteAttendanceByClassDate]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteAttendanceByClassDate]
    @classId INT,
    @date DATE
AS
BEGIN
    
    DELETE A
    FROM dbo.attendance A
    JOIN dbo.students S ON A.id_student = S.id_student
    WHERE S.id_class = @classId 
      AND A.attendance_date = @date;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteCategory]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteCategory]
    @Id INT
AS
BEGIN
    
    DELETE FROM dbo.Categories WHERE id_category = @Id;
END


-- Nguyen
-- 1. Lấy bảng điểm thô cho GridView nhập điểm
IF OBJECT_ID('dbo.sp_GetRawGrades', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetRawGrades;
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteNote]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteNote]
    @id INT
AS
BEGIN
    
    DELETE FROM dbo.student_notes WHERE id_note = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteNotesByFilter]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteNotesByFilter]
    @classId INT,
    @noteType NVARCHAR(50)
AS
BEGIN

    DELETE n 
    FROM dbo.student_notes n
    JOIN dbo.students s ON n.id_student = s.id_student
    WHERE s.id_class = @classId
      AND (@noteType = N'Tất cả' OR n.note_type = @noteType);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteQuiz]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteQuiz]
    @id INT
AS
BEGIN
    
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Bước 1: Xóa nội dung chi tiết trước
        DELETE FROM dbo.Quiz_Content WHERE id_quiz = @id;
        
        -- Bước 2: Xóa header game
        DELETE FROM dbo.Quizzes WHERE id_quiz = @id;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        -- Ném lỗi ra để C# bắt được
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteStudent]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteStudent]
    @id INT
AS
BEGIN
    DELETE FROM dbo.students WHERE id_student = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteTeacher]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteTeacher] @id INT AS BEGIN  
    DELETE dbo.teachers WHERE id_teacher = @id; END 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteVocabulary]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteVocabulary]
    @id INT
AS
BEGIN
    
    DELETE FROM dbo.Vocabulary WHERE id_vocabulary = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EmailExists]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EmailExists]
    @email NVARCHAR(256)
AS
BEGIN
    SELECT COUNT(1) FROM dbo.teachers WHERE email = @email;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAcademicRawData]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAcademicRawData]
    @from DATE,
    @to DATE
AS
BEGIN
    SELECT 
        s.id_student,
        c.class_name, 
        s.gender,
        s.ethnicity,
        sub.subject_name,
        g.grade_score
    FROM dbo.students s
    JOIN dbo.classes c ON s.id_class = c.id_class
    JOIN dbo.grades g ON s.id_student = g.id_student
    JOIN dbo.subjects sub ON g.id_subject = sub.id_subject
    WHERE g.grade_period = 'Final' 
      AND g.grade_date BETWEEN @from AND @to;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetActivationState]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetActivationState]
    @username NVARCHAR(256)
AS
BEGIN
    SELECT is_active, IsManualDeactivated
    FROM dbo.teachers
    WHERE username = @username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllCategories]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllCategories]
    @OnlyShowNonEmpty BIT = 0
AS
BEGIN

    IF @OnlyShowNonEmpty = 1
    BEGIN
        SELECT C.id_category, C.category_name 
        FROM dbo.Categories C 
        WHERE EXISTS (SELECT 1 FROM dbo.Vocabulary V WHERE V.id_category = C.id_category)
        ORDER BY C.category_name;
    END
    ELSE
    BEGIN
        SELECT id_category, category_name 
        FROM dbo.Categories 
        ORDER BY category_name;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllClasses]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Procedure: return all classes
CREATE PROCEDURE [dbo].[sp_GetAllClasses]
AS
BEGIN
    SELECT id_class, class_name, id_teacher
    FROM dbo.classes;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllQuizzes]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllQuizzes]
AS
BEGIN
    
    SELECT id_quiz, quiz_title, quiz_type 
    FROM dbo.Quizzes;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllSubjects]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllSubjects]
AS
BEGIN
    SELECT * FROM dbo.subjects;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllTeachers]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetAllTeachers]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        t.id_teacher,
        t.full_name,
        t.username,
        t.email,
        t.phone_number,
        t.user_role,
        s.subject_name AS subject, -- Lấy tên môn từ bảng subjects
        t.is_active,
        t.IsManualDeactivated,
        t.created_at
    FROM dbo.teachers t
    LEFT JOIN dbo.subjects s ON t.id_teacher = s.id_teacher
    WHERE t.is_active = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAssignedClassId]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAssignedClassId] @id INT AS BEGIN 
    SELECT TOP 1 id_class FROM dbo.classes WHERE id_teacher = @id; END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAttendanceByClassDate]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAttendanceByClassDate]
    @classId INT,
    @date DATE
AS
BEGIN

    SELECT 
        B.id_attendance,
        B.id_student, 
        A.id_class, 
        B.attendance_date, 
        B.attendance_status, 
        B.attendance_notes
    FROM dbo.students A
    JOIN dbo.attendance B ON A.id_student = B.id_student
    WHERE A.id_class = @classId 
      AND B.attendance_date = @date;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAttendanceReportByClass]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAttendanceReportByClass]
    @classId INT,
    @startDate DATE,
    @endDate DATE
AS
BEGIN

    SELECT 
        a.id_attendance, 
        s.id_student,     
        s.id_class,       
        s.full_name,      
        a.attendance_date, 
        a.attendance_status, 
        a.attendance_notes 
    FROM dbo.attendance AS a
    INNER JOIN dbo.students AS s ON a.id_student = s.id_student
    WHERE s.id_class = @classId
      AND a.attendance_date BETWEEN @startDate AND @endDate
    ORDER BY s.full_name, a.attendance_date;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCategoryIdForVocab]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetCategoryIdForVocab]
    @id INT
AS
BEGIN
    
    SELECT id_category FROM dbo.Vocabulary WHERE id_vocabulary = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmailCount]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetEmailCount]
    @email NVARCHAR(256)
AS
BEGIN
    SELECT COUNT(1) FROM dbo.teachers WHERE email = @email;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetNotesByClass]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetNotesByClass]
    @classId INT,
    @noteType NVARCHAR(50) -- Truyền 'Tất cả' hoặc loại cụ thể
AS
BEGIN

    SELECT n.*, s.full_name 
    FROM dbo.student_notes n
    JOIN dbo.students s ON n.id_student = s.id_student
    WHERE s.id_class = @classId
      AND (@noteType = N'Tất cả' OR n.note_type = @noteType)
    ORDER BY n.created_at DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetNotesForExport]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetNotesForExport]
    @classId INT,
    @from DATETIME,
    @to DATETIME
AS
BEGIN

    SELECT n.*, s.full_name 
    FROM dbo.student_notes n
    JOIN dbo.students s ON n.id_student = s.id_student
    WHERE s.id_class = @classId 
      AND n.created_at BETWEEN @from AND @to
    ORDER BY s.id_student ASC, n.created_at DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPasswordHashByUsername]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetPasswordHashByUsername]
    @username NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT password_hash
    FROM dbo.teachers
    WHERE username = @username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPendingRegistrations]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetPendingRegistrations]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        t.id_teacher,
        t.full_name,
        t.username,
        t.email,
        t.phone_number,
        t.user_role,
        s.subject_name AS subject, -- Lấy môn học nếu đã được gán (thường là null)
        t.created_at
    FROM dbo.teachers t
    LEFT JOIN dbo.subjects s ON t.id_teacher = s.id_teacher
    WHERE t.is_active = 0 AND t.IsManualDeactivated = 0;
END
GO


/****** Object:  StoredProcedure [dbo].[sp_GetQuizContentIds]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetQuizContentIds]
    @id_quiz INT
AS
BEGIN
    
    SELECT id_vocabulary 
    FROM dbo.Quiz_Content 
    WHERE id_quiz = @id_quiz;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRawDataByClass]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRawDataByClass]
    @classId INT,
    @from DATE,
    @to DATE
AS
BEGIN
    SELECT 
        s.id_student,
        c.class_name, 
        s.gender,
        s.ethnicity,
        sub.subject_name,
        g.grade_score
    FROM dbo.students s
    JOIN dbo.classes c ON s.id_class = c.id_class
    JOIN dbo.grades g ON s.id_student = g.id_student
    JOIN dbo.subjects sub ON g.id_subject = sub.id_subject
    WHERE s.id_class = @classId
      AND g.grade_period = 'Final' 
      AND g.grade_date BETWEEN @from AND @to;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRawGrades]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRawGrades]
    @classId INT,
    @semester NVARCHAR(50)
AS
BEGIN
    SELECT 
        s.id_student, 
        s.full_name, 
        sub.subject_name, 
        g.grade_period, 
        g.grade_score
    FROM dbo.students s
    LEFT JOIN dbo.grades g ON s.id_student = g.id_student AND g.semester = @semester
    LEFT JOIN dbo.subjects sub ON g.id_subject = sub.id_subject
    WHERE s.id_class = @classId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStudentByCode]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetStudentByCode]
    @code NVARCHAR(50)
AS
BEGIN
    SELECT id_student, student_code, full_name, date_of_birth, gender, ethnicity, id_class 
    FROM dbo.students 
    WHERE student_code = @code;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStudentsByClassId]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetStudentsByClassId]
    @classId INT
AS
BEGIN
    SELECT id_student, student_code, full_name, date_of_birth, gender, ethnicity, id_class 
    FROM dbo.students 
    WHERE id_class = @classId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetSubjectIdByName]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetSubjectIdByName]
    @name NVARCHAR(255)
AS
BEGIN
    SELECT id_subject FROM dbo.subjects WHERE subject_name = @name;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTeacherById]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[sp_GetTeacherById]
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        t.id_teacher,
        t.full_name,
        t.username,
        t.email,
        t.phone_number,
        t.user_role,
        s.subject_name AS subject, -- Lấy tên môn
        t.created_at
    FROM dbo.teachers t
    LEFT JOIN dbo.subjects s ON t.id_teacher = s.id_teacher
    WHERE t.id_teacher = @id;
END
GO


/****** Object:  StoredProcedure [dbo].[sp_GetTodaysAttendance]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetTodaysAttendance]
    @id_Class INT,
    @date DATE
AS
BEGIN

    SELECT 
        s.id_student AS id_Student, 
        s.student_code AS code_Student, 
        s.full_name AS name_Student,
        a.attendance_status,
        a.attendance_method
    FROM dbo.students s
    JOIN dbo.attendance a ON s.id_student = a.id_student
    WHERE s.id_class = @id_Class 
      AND a.attendance_date = @date;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUsernameCount]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUsernameCount]
    @username NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM dbo.teachers WHERE username = @username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetVocabularyByCategory]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetVocabularyByCategory]
    @categoryId INT,
    @vocabType NVARCHAR(50)
AS
BEGIN

    SELECT id_vocabulary, WordText, WordImage, id_category, VocabType 
    FROM dbo.Vocabulary 
    WHERE id_category = @categoryId AND VocabType = @vocabType;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetVocabularyByIds]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetVocabularyByIds]
    @ids NVARCHAR(MAX) -- Chuỗi ID ngăn cách bởi dấu phẩy (VD: "1,5,10")
AS
BEGIN

    SELECT id_vocabulary, WordText, WordImage, id_category, VocabType 
    FROM dbo.Vocabulary 
    WHERE id_vocabulary IN (SELECT value FROM STRING_SPLIT(@ids, ','));
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetVocabularyCountByCategory]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetVocabularyCountByCategory]
    @id INT
AS
BEGIN
    
    SELECT COUNT(*) FROM dbo.Vocabulary WHERE id_category = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetVocabularyList]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetVocabularyList]
    @categoryId INT = 0,
    @searchTerm NVARCHAR(255) = NULL
AS
BEGIN

    SELECT 
        V.id_vocabulary, 
        V.WordText AS Word, 
        V.WordImage AS WordImage, 
        C.category_name AS CategoryName,
        V.VocabType
    FROM dbo.Vocabulary V
    JOIN dbo.Categories C ON V.id_category = C.id_category
    WHERE (@categoryId = 0 OR V.id_category = @categoryId)
      AND (@searchTerm IS NULL OR @searchTerm = '' OR V.WordText LIKE N'%' + @searchTerm + N'%');
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertCategory]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertCategory]
    @Name NVARCHAR(255)
AS
BEGIN
    
    INSERT INTO dbo.Categories (category_name) 
    VALUES (@Name);
    
    SELECT SCOPE_IDENTITY(); 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertManualAttendance]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertManualAttendance]
    @studentId INT,
    @date DATE,
    @status NVARCHAR(50),
    @notes NVARCHAR(MAX) = NULL
AS
BEGIN

    INSERT INTO dbo.attendance 
    (id_student, attendance_date, attendance_status, attendance_method, attendance_notes) 
    VALUES 
    (@studentId, @date, @status, 'manual', @notes);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertNote]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertNote]
    @sid INT,
    @content NVARCHAR(MAX),
    @type NVARCHAR(50),
    @priority NVARCHAR(50)
AS
BEGIN

    INSERT INTO dbo.student_notes 
    (id_student, note_content, note_type, priority, created_at) 
    VALUES 
    (@sid, @content, @type, @priority, GETDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertQuiz]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertQuiz]
    @id_teacher INT,
    @id_class INT,
    @quiz_title NVARCHAR(255),
    @quiz_type NVARCHAR(50),
    @quiz_config_json NVARCHAR(MAX)
AS
BEGIN
    
    INSERT INTO dbo.Quizzes (id_teacher, id_class, quiz_title, quiz_type, quiz_config_json)
    VALUES (@id_teacher, @id_class, @quiz_title, @quiz_type, @quiz_config_json);
    
    -- Trả về ID vừa tạo
    SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertQuizContent]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertQuizContent]
    @id_quiz INT,
    @id_vocabulary INT
AS
BEGIN
    
    INSERT INTO dbo.Quiz_Content (id_quiz, id_vocabulary)
    VALUES (@id_quiz, @id_vocabulary);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertStudent]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertStudent]
    @code NVARCHAR(50),
    @name NVARCHAR(255),
    @dob DATE,
    @gender NVARCHAR(10),
    @ethnicity NVARCHAR(50),
    @classId INT
AS
BEGIN
    INSERT INTO dbo.students (student_code, full_name, date_of_birth, gender, ethnicity, id_class) 
    VALUES (@code, @name, @dob, @gender, @ethnicity, @classId);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertVocabulary]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertVocabulary]
    @WordText NVARCHAR(255),
    @WordImage VARBINARY(MAX) = NULL,
    @id_category INT,
    @VocabType NVARCHAR(50)
AS
BEGIN
    
    INSERT INTO dbo.Vocabulary (WordText, WordImage, id_category, VocabType)
    VALUES (@WordText, @WordImage, @id_category, @VocabType);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_LockTeacher]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_LockTeacher] @id INT AS BEGIN SET NOCOUNT ON; UPDATE dbo.teachers SET IsManualDeactivated = 1 WHERE id_teacher = @id; END 
GO
/****** Object:  StoredProcedure [dbo].[sp_LoginTeacher]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_LoginTeacher]
    @username NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        id_teacher,
        full_name,
        username,
        user_role,
        password_hash,
        is_active,
        IsManualDeactivated
    FROM dbo.teachers
    WHERE username = @username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_LoginWithToken]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_LoginWithToken]
    @tokenRaw NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    IF @tokenRaw IS NULL
    BEGIN
        RETURN;
    END

    DECLARE @tokenHashBase64 NVARCHAR(MAX);
    DECLARE @hb VARBINARY(32) = HASHBYTES('SHA2_256', CONVERT(VARBINARY(MAX), @tokenRaw));
    SET @tokenHashBase64 = CAST(N'' AS XML).value('xs:base64Binary(sql:variable("@hb"))', 'NVARCHAR(MAX)');

    SELECT id_teacher, full_name, username, user_role
    FROM dbo.teachers
    WHERE remember_token = @tokenHashBase64
      AND remember_expires > GETDATE()
      AND is_active = 1
      AND (IsManualDeactivated = 0 OR IsManualDeactivated IS NULL);
END;

-- 12 :	sp_SaveRememberToken

IF OBJECT_ID('dbo.sp_SaveRememberToken', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_SaveRememberToken;
GO
/****** Object:  StoredProcedure [dbo].[sp_MarkStudentAttendance]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MarkStudentAttendance]
    @id_Student INT,
    @date DATE,
    @status NVARCHAR(50),
    @method NVARCHAR(50)
AS
BEGIN

    MERGE INTO dbo.attendance AS T
    USING (SELECT @id_Student AS id_student, @date AS attendance_date) AS S
    ON T.id_student = S.id_student AND T.attendance_date = S.attendance_date
    WHEN MATCHED THEN
        UPDATE SET 
            attendance_status = @status, 
            attendance_method = @method
    WHEN NOT MATCHED THEN
        INSERT (id_student, attendance_date, attendance_status, attendance_method)
        VALUES (@id_Student, @date, @status, @method);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegisterTeacher]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegisterTeacher]
    @name NVARCHAR(256),
    @username NVARCHAR(256),
    @hash NVARCHAR(MAX),
    @email NVARCHAR(256) = NULL,
    @phone NVARCHAR(50) = NULL,
    @role NVARCHAR(50) = 'teacher'
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.teachers (full_name, username, password_hash, email, user_role, phone_number, is_active, IsManualDeactivated, created_at)
    VALUES (@name, @username, @hash, @email, @role, ISNULL(@phone, ''), 0, 0, GETDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RejectTeacher]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RejectTeacher] @id INT AS BEGIN SET NOCOUNT ON; UPDATE dbo.teachers SET IsManualDeactivated = 0, is_active = 0 WHERE id_teacher = @id; END 
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveRememberToken]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SaveRememberToken]
    @id INT,
    @tokenRaw NVARCHAR(MAX) = NULL,
    @expires DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @tokenHashBase64 NVARCHAR(MAX) = NULL;

    IF @tokenRaw IS NOT NULL
    BEGIN
        DECLARE @hb VARBINARY(32) = HASHBYTES('SHA2_256', CONVERT(VARBINARY(MAX), @tokenRaw));
        SET @tokenHashBase64 = CAST(N'' AS XML).value('xs:base64Binary(sql:variable("@hb"))', 'NVARCHAR(MAX)');
    END

    UPDATE dbo.teachers
    SET remember_token = @tokenHashBase64,
        remember_expires = @expires
    WHERE id_teacher = @id;
END;

-- 13 :	sp_GetPendingRegistrations

IF OBJECT_ID('dbo.sp_GetPendingRegistrations', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetPendingRegistrations;
GO
/****** Object:  StoredProcedure [dbo].[sp_SetPasswordHashById]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SetPasswordHashById]
    @id INT,
    @hash NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.teachers
    SET password_hash = @hash
    WHERE id_teacher = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UnlockTeacher]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UnlockTeacher] @id INT AS BEGIN SET NOCOUNT ON; UPDATE dbo.teachers SET IsManualDeactivated = 0, is_active = 1 WHERE id_teacher = @id; END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateNoteContent]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateNoteContent]
    @id INT,
    @content NVARCHAR(MAX)
AS
BEGIN

    UPDATE dbo.student_notes 
    SET note_content = @content 
    WHERE id_note = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePasswordByEmail]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdatePasswordByEmail]
    @email NVARCHAR(256),
    @hash NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.teachers
    SET password_hash = @hash
    WHERE email = @email;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePasswordHashById]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdatePasswordHashById]
    @id INT,
    @hash NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.teachers
    SET password_hash = @hash
    WHERE id_teacher = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateStudent]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateStudent]
    @id INT,
    @code NVARCHAR(50),
    @name NVARCHAR(255),
    @dob DATE,
    @gender NVARCHAR(10),
    @ethnicity NVARCHAR(50)
AS
BEGIN
    UPDATE dbo.students 
    SET student_code = @code, 
        full_name = @name, 
        date_of_birth = @dob, 
        gender = @gender, 
        ethnicity = @ethnicity
    WHERE id_student = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTeacherInfo]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateTeacherInfo]
    @id INT,
    @name NVARCHAR(256) = NULL,
    @email NVARCHAR(256) = NULL,
    @phone NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.teachers
    SET full_name = @name, email = @email, phone_number = @phone
    WHERE id_teacher = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateVocabulary]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateVocabulary]
    @id INT,
    @WordText NVARCHAR(255),
    @WordImage VARBINARY(MAX) = NULL,
    @id_category INT,
    @VocabType NVARCHAR(50)
AS
BEGIN

    -- Nếu có ảnh truyền vào (khác NULL) -> Cập nhật cả ảnh
    IF @WordImage IS NOT NULL
    BEGIN
        UPDATE dbo.Vocabulary 
        SET WordText = @WordText, 
            WordImage = @WordImage, 
            id_category = @id_category, 
            VocabType = @VocabType 
        WHERE id_vocabulary = @id;
    END
    ELSE
    BEGIN
        -- Nếu ảnh là NULL -> Giữ nguyên ảnh cũ, chỉ cập nhật thông tin khác
        UPDATE dbo.Vocabulary 
        SET WordText = @WordText, 
            id_category = @id_category, 
            VocabType = @VocabType 
        WHERE id_vocabulary = @id;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpsertGrade]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpsertGrade]
    @sid INT,
    @subId INT,
    @sem NVARCHAR(50),
    @type NVARCHAR(50), -- 'Mid' or 'Final'
    @score FLOAT
AS
BEGIN

    -- Check exist
    IF EXISTS (SELECT 1 FROM dbo.grades 
               WHERE id_student = @sid 
                 AND id_subject = @subId 
                 AND semester = @sem 
                 AND grade_period = @type)
    BEGIN
        -- Update
        UPDATE dbo.grades 
        SET grade_score = @score, 
            grade_date = GETDATE()
        WHERE id_student = @sid 
          AND id_subject = @subId 
          AND semester = @sem 
          AND grade_period = @type;
    END
    ELSE
    BEGIN
        -- Insert
        INSERT INTO dbo.grades (id_student, id_subject, semester, grade_period, grade_score, grade_date)
        VALUES (@sid, @subId, @sem, @type, @score, GETDATE());
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UsernameExists]    Script Date: 25/11/2025 12:49:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UsernameExists]
    @username NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM dbo.teachers WHERE username = @username;
END
GO


