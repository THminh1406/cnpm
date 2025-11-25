-- Updated stored procedures for teacher subject support
-- Run these on your SQL Server (use the database that contains dbo.teachers)

CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllTeachers]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        id_teacher,
        full_name,
        username,
        email,
        phone_number,
        user_role,
        subject,
        is_active,
        IsManualDeactivated,
        created_at
    FROM dbo.teachers
    WHERE is_active = 1;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetPendingRegistrations]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        id_teacher,
        full_name,
        username,
        email,
        phone_number,
        user_role,
        subject,
        created_at
    FROM dbo.teachers
    WHERE is_active = 0 AND IsManualDeactivated = 0;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetTeacherById]
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        id_teacher,
        full_name,
        username,
        email,
        phone_number,
        user_role,
        subject,
        created_at
    FROM dbo.teachers
    WHERE id_teacher = @id;
END
GO

-- New stored procedure to set or clear the subject for a teacher
CREATE OR ALTER PROCEDURE [dbo].[sp_SetTeacherSubject]
    @id INT,
    @subject NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.teachers
    SET subject = @subject
    WHERE id_teacher = @id;

    -- return number of affected rows for calling code to verify success
    SELECT @@ROWCOUNT AS affected_rows;
END
GO
