
-- ========================================
-- PHẦN 1: TẠO DATABASE CHÍNH 
-- Mục đích: Tạo database chính với tên rõ ràng, hỗ trợ collation cho tiếng Việt/Anh (yêu cầu quốc tế hóa).
-- Lưu ý: Script này đã được tối ưu cho SQL Server (T-SQL), tích hợp tất cả trong một script duy nhất, idempotent (có thể chạy nhiều lần mà không lỗi).
-- Sử dụng COLLATE Vietnamese_CI_AS cho hỗ trợ tiếng Việt.
-- ========================================
-- Kiểm tra và tạo database nếu chưa tồn tại.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'management_db')
BEGIN
    CREATE DATABASE  management_db
    COLLATE Vietnamese_CI_AS;  -- Collation hỗ trợ tiếng Việt (utf8 tương đương).
END
GO
-- Kiểm tra lại và chuyển sang sử dụng database.
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'management_db')
BEGIN
    USE management_db;
END
ELSE
BEGIN
    PRINT 'Database management_db could not be created. Please check permissions.';
    RETURN;
END
GO

-- ========================================
-- PHẦN 2: BẢNG  TEACHERS (QUẢN LÝ GIÁO VIÊN) - REQ-17-19
-- Mục đích: Lưu thông tin giáo viên, hỗ trợ đa tài khoản và phân quyền RBAC (dễ quản lý qua prefix  ).
-- Idempotent: Kiểm tra bảng tồn tại trước khi tạo; drop/add constraints/indexes nếu cần.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'teachers')
BEGIN
    -- Tạo bảng  teachers với khóa chính IDENTITY (tương đương AUTO_INCREMENT).
    CREATE TABLE  teachers (
        -- id_teacher: Khóa chính tự động tăng (IDENTITY(1,1)).
        id_teacher INT IDENTITY(1,1) PRIMARY KEY,
        -- full_name: Họ tên đầy đủ giáo viên, bắt buộc, độ dài tối đa 255 ký tự.
        full_name NVARCHAR(255) NOT NULL,  -- NVARCHAR cho Unicode (tiếng Việt).
        -- username: Tên đăng nhập duy nhất, dùng cho login hệ thống.
        username NVARCHAR(100) NOT NULL,
        -- password_hash: Mật khẩu đã mã hóa (sử dụng bcrypt hoặc AES ở app layer cho bảo mật REQ-7).
        password_hash NVARCHAR(255) NOT NULL,
        -- email: Email liên lạc duy nhất, tùy chọn cho thông báo (e.g., báo cáo).
        email NVARCHAR(255) NULL,
        -- user_role: Phân quyền dựa trên vai trò (teacher hoặc admin), mặc định teacher để hỗ trợ RBAC.
        user_role NVARCHAR(20) NOT NULL DEFAULT 'teacher' CHECK (user_role IN ('teacher', 'admin')),
        -- is_active: Tài khoảng có được vô hiệu hóa hay không (1 là hoạt động , 0 là bị vô hiệu hóa).
        is_active BIT NOT NULL DEFAULT 1,
        -- created_at: Thời gian tạo tài khoản, tự động (DATETIME2 thay TIMESTAMP).
        created_at DATETIME2 DEFAULT GETDATE()
    );
END
-- Thêm unique constraints cho username và email nếu chưa tồn tại.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'UQ_teachers_username' AND type = 'UQ')
BEGIN
    ALTER TABLE teachers ADD CONSTRAINT UQ_teachers_username UNIQUE (username);
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'UQ_teachers_email' AND type = 'UQ')
BEGIN
    ALTER TABLE teachers ADD CONSTRAINT UQ_teachers_email UNIQUE (email);
END
-- Index trên username để tìm kiếm login nhanh (hiệu suất cho 100 user đồng thời).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_username' AND object_id = OBJECT_ID('teachers'))
BEGIN
    CREATE INDEX idx_username ON  teachers (username);
END
-- Index trên user_role để lọc theo quyền (e.g., admin xem tất cả lớp).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_user_role' AND object_id = OBJECT_ID('teachers'))
BEGIN
    CREATE INDEX idx_user_role ON  teachers (user_role);
END
GO

-- ========================================
-- PHẦN 3: BẢNG CLASSES (QUẢN LÝ LỚP HỌC) - REQ-1,3
-- Mục đích: Phân loại học sinh theo lớp, liên kết với giáo viên (prefix   dễ phân biệt module).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'classes')
BEGIN
    -- Tạo bảng classes với khóa chính IDENTITY.
    CREATE TABLE classes (
        -- id_class: Khóa chính tự động tăng.
        id_class INT IDENTITY(1,1) PRIMARY KEY,
        -- class_name: Tên lớp (e.g., "10A1"), bắt buộc.
        class_name NVARCHAR(100) NOT NULL,
        -- id_teacher: Khóa ngoại liên kết với  teachers, chỉ định giáo viên phụ trách.
        id_teacher INT NOT NULL,
        -- class_description: Mô tả lớp, tùy chọn (e.g., số lượng HS hoặc ghi chú).
        class_description NVARCHAR(MAX),  -- NVARCHAR(MAX) cho TEXT dài.
        -- created_at: Thời gian tạo lớp, tự động.
        created_at DATETIME2 DEFAULT GETDATE()
    );
END
-- Foreign key: Nếu xóa giáo viên, xóa cascade các lớp liên kết (an toàn dữ liệu).
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_classes_id_teacher')
BEGIN
    ALTER TABLE classes ADD CONSTRAINT FK_classes_id_teacher FOREIGN KEY (id_teacher) REFERENCES  teachers(id_teacher) ON DELETE CASCADE;
END
-- Index trên id_teacher để query lớp theo giáo viên nhanh (REQ-3).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_teacher' AND object_id = OBJECT_ID('classes'))
BEGIN
    CREATE INDEX idx_id_teacher ON  classes (id_teacher);
END
-- Index trên class_name để tìm kiếm lớp dễ dàng.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_class_name' AND object_id = OBJECT_ID('classes'))
BEGIN
    CREATE INDEX idx_class_name ON  classes (class_name);
END
GO

-- ========================================
-- PHẦN 4: BẢNG  SUBJECTS (QUẢN LÝ MÔN HỌC) - REQ-1,8
-- Mục đích: Phân loại điểm số và quiz theo môn học (tên rõ ràng để dễ quản lý khi mở rộng).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'subjects')
BEGIN
    -- Tạo bảng subjects với khóa chính IDENTITY.
    CREATE TABLE subjects (
        -- id_subject: Khóa chính tự động tăng.
        id_subject INT IDENTITY(1,1) PRIMARY KEY,
        -- subject_name: Tên môn (e.g., "Toán"), bắt buộc.
        subject_name NVARCHAR(100) NOT NULL,
        -- subject_code: Mã môn duy nhất (e.g., "MATH101"), tùy chọn cho tìm kiếm.
        subject_code NVARCHAR(20) NULL,
        -- subject_description: Mô tả môn, tùy chọn.
        subject_description NVARCHAR(MAX)
    );
END
-- Unique constraint cho subject_code.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'UQ_subjects_subject_code' AND type = 'UQ')
BEGIN
    ALTER TABLE  subjects ADD CONSTRAINT UQ_subjects_subject_code UNIQUE (subject_code);
END
-- Index trên subject_name để tìm kiếm môn nhanh.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_subject_name' AND object_id = OBJECT_ID('subjects'))
BEGIN
    CREATE INDEX idx_subject_name ON subjects (subject_name);
END
GO

-- ========================================
-- PHẦN 5: BẢNG STUDENTS (QUẢN LÝ HỒ SƠ HỌC SINH) - REQ-1-7
-- Mục đích: Lưu thông tin cá nhân HS, hỗ trợ nhập từ Excel, tìm kiếm, bảo mật (dễ quản lý qua prefix).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'students')
BEGIN
    -- Tạo bảng students với khóa chính IDENTITY.
    CREATE TABLE students (
        -- id_student: Khóa chính tự động tăng.
        id_student INT IDENTITY(1,1) PRIMARY KEY,
        -- student_code: Mã HS duy nhất, bắt buộc (dùng cho tìm kiếm REQ-4).
        student_code NVARCHAR(50) NOT NULL,
        -- full_name: Họ tên HS, bắt buộc.
        full_name NVARCHAR(255) NOT NULL,
        -- date_of_birth: Ngày sinh, định dạng DATE (hỗ trợ quốc tế hóa, REQ-1).
        date_of_birth DATE NOT NULL,
        -- gender: Giới tính, enum thay bằng CHECK constraint.
        gender NVARCHAR(10) NULL,
        -- hometown: Quê quán, tùy chọn (REQ-1).
        hometown NVARCHAR(255) NULL,
        -- id_class: Khóa ngoại liên kết với classes.
        id_class INT NOT NULL,
        -- created_at: Thời gian tạo hồ sơ, tự động.
        created_at DATETIME2 DEFAULT GETDATE()
    );
END
-- Unique constraint cho student_code.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'UQ_students_student_code' AND type = 'UQ')
BEGIN
    ALTER TABLE  students ADD CONSTRAINT UQ_students_student_code UNIQUE (student_code);
END
-- CHECK constraint cho gender.
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_students_gender')
BEGIN
    ALTER TABLE  students ADD CONSTRAINT CK_students_gender CHECK (gender IN ('male', 'female', 'other'));
END
-- Foreign key: Nếu xóa lớp, xóa cascade HS liên kết.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_students_id_class')
BEGIN
    ALTER TABLE students ADD CONSTRAINT FK_students_id_class FOREIGN KEY (id_class) REFERENCES  classes(id_class) ON DELETE CASCADE;
END
-- Index trên student_code cho tìm kiếm mã HS nhanh (REQ-4).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_student_code' AND object_id = OBJECT_ID('students'))
BEGIN
    CREATE INDEX idx_student_code ON  students (student_code);
END
-- Index trên full_name cho tìm kiếm theo tên (REQ-4).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_full_name' AND object_id = OBJECT_ID('students'))
BEGIN
    CREATE INDEX idx_full_name ON  students (full_name);
END
-- Index trên id_class để query HS theo lớp (REQ-3).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_class' AND object_id = OBJECT_ID('students'))
BEGIN
    CREATE INDEX idx_id_class ON  students (id_class);
END
-- Index trên date_of_birth cho báo cáo theo tuổi (e.g., thống kê REQ-22).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_date_of_birth' AND object_id = OBJECT_ID('students'))
BEGIN
    CREATE INDEX idx_date_of_birth ON  students (date_of_birth);
END
GO

-- ========================================
-- PHẦN 6: BẢNG  ATTENDANCE (QUẢN LÝ ĐIỂM DANH) - REQ-11-12
-- Mục đích: Lưu trạng thái điểm danh (QR/thủ công), hỗ trợ báo cáo chuyên cần (tên dễ query).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'attendance')
BEGIN
    -- Tạo bảng attendance với khóa chính IDENTITY.
    CREATE TABLE attendance (
        -- id_attendance: Khóa chính tự động tăng.
        id_attendance INT IDENTITY(1,1) PRIMARY KEY,
        -- id_student: Khóa ngoại liên kết HS.
        id_student INT NOT NULL,
        -- attendance_date: Ngày điểm danh, bắt buộc.
        attendance_date DATE NOT NULL,
        -- attendance_status: Trạng thái (có phép/không phép/vắng muộn), enum thay CHECK.
        attendance_status NVARCHAR(20) NOT NULL,
        -- attendance_method: Phương thức (QR hoặc thủ công), mặc định thủ công (REQ-11).
        attendance_method NVARCHAR(10) NOT NULL,
        -- attendance_notes: Ghi chú vắng mặt, tùy chọn.
        attendance_notes NVARCHAR(MAX) NULL,
        -- recorded_at: Thời gian ghi nhận điểm danh, tự động.
        recorded_at DATETIME2 DEFAULT GETDATE()
    );
END
-- CHECK constraint cho attendance_status.
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_attendance_status')
BEGIN
    ALTER TABLE  attendance ADD CONSTRAINT CK_attendance_status CHECK (attendance_status IN ('present', 'absent_permitted', 'absent_unpermitted', 'late'));
END
-- Default và CHECK cho attendance_method.
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE name = 'DF_attendance_method')
BEGIN
    ALTER TABLE  attendance ADD CONSTRAINT DF_attendance_method DEFAULT 'manual' FOR attendance_method;
END
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_attendance_method')
BEGIN
    ALTER TABLE  attendance ADD CONSTRAINT CK_attendance_method CHECK (attendance_method IN ('qr', 'manual'));
END
-- Foreign key: Nếu xóa HS, xóa cascade điểm danh liên kết.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_attendance_id_student')
BEGIN
    ALTER TABLE attendance ADD CONSTRAINT FK_attendance_id_student FOREIGN KEY (id_student) REFERENCES  students(id_student) ON DELETE CASCADE;
END
-- Composite index trên id_student và attendance_date cho báo cáo theo HS/ngày nhanh (REQ-12).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_student_date' AND object_id = OBJECT_ID('attendance'))
BEGIN
    CREATE INDEX idx_id_student_date ON  attendance (id_student, attendance_date);
END
-- Index trên attendance_date cho thống kê theo tháng/học kỳ (REQ-12).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_attendance_date' AND object_id = OBJECT_ID('attendance'))
BEGIN
    CREATE INDEX idx_attendance_date ON  attendance (attendance_date);
END
GO

-- ========================================
-- PHẦN 7: BẢNG  GRADES (QUẢN LÝ KẾT QUẢ HỌC TẬP) - REQ-8-10
-- Mục đích: Lưu điểm/nhận xét theo tháng/học kỳ, hỗ trợ thống kê (prefix   dễ quản lý báo cáo).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'grades')
BEGIN
    -- Tạo bảng  grades với khóa chính IDENTITY.
    CREATE TABLE  grades (
        -- id_grade: Khóa chính tự động tăng.
        id_grade INT IDENTITY(1,1) PRIMARY KEY,
        -- id_student: Khóa ngoại HS.
        id_student INT NOT NULL,
        -- id_subject: Khóa ngoại môn học.
        id_subject INT NOT NULL,
        -- grade_period: Loại kỳ (tháng/học kỳ I/II), enum thay CHECK (REQ-8).
        grade_period NVARCHAR(20) NOT NULL,
        -- grade_score: Điểm số (e.g., 8.50), độ chính xác 2 chữ số thập phân.
        grade_score DECIMAL(5,2) NULL,
        -- grade_comment: Nhận xét học lực/thái độ (REQ-9).
        grade_comment NVARCHAR(MAX) NULL,
        -- grade_date: Ngày nhập điểm, bắt buộc.
        grade_date DATE NOT NULL
    );
END
-- CHECK constraint cho grade_period.
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_grades_period')
BEGIN
    ALTER TABLE  grades ADD CONSTRAINT CK_grades_period CHECK (grade_period IN ('monthly', 'semester1', 'semester2'));
END
-- Foreign key HS: Xóa cascade nếu xóa HS.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_grades_id_student')
BEGIN
    ALTER TABLE grades ADD CONSTRAINT FK_grades_id_student FOREIGN KEY (id_student) REFERENCES  students(id_student) ON DELETE CASCADE;
END
-- Foreign key môn: Không cho xóa nếu có điểm (NO ACTION tương đương RESTRICT).
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_grades_id_subject')
BEGIN
    ALTER TABLE  grades ADD CONSTRAINT FK_grades_id_subject FOREIGN KEY (id_subject) REFERENCES  subjects(id_subject) ON DELETE NO ACTION;
END
-- Composite index cho thống kê theo HS/môn/kỳ (REQ-10).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_student_subject_period' AND object_id = OBJECT_ID('grades'))
BEGIN
    CREATE INDEX idx_id_student_subject_period ON  grades (id_student, id_subject, grade_period);
END
-- Index trên grade_date cho lịch sử theo thời gian.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_grade_date' AND object_id = OBJECT_ID('grades'))
BEGIN
    CREATE INDEX idx_grade_date ON  grades (grade_date);
END
GO

-- ========================================
-- PHẦN 8: BẢNG  NOTES (QUẢN LÝ GHI CHÚ) - REQ-9,16
-- Mục đích: Lưu ghi chú pop-up nhanh, gắn ngày giờ vào hồ sơ HS (tên rõ ràng cho module ghi chú).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'notes')
BEGIN
    -- Tạo bảng  notes với khóa chính IDENTITY.
    CREATE TABLE  notes (
        -- id_note: Khóa chính tự động tăng.
        id_note INT IDENTITY(1,1) PRIMARY KEY,
        -- id_student: Khóa ngoại HS.
        id_student INT NOT NULL,
        -- id_teacher: Khóa ngoại GV ghi chú.
        id_teacher INT NOT NULL,
        -- note_content: Nội dung ghi chú, bắt buộc.
        note_content NVARCHAR(MAX) NOT NULL,
        -- note_type: Loại ghi chú (hành vi/học tập/chung), mặc định chung (REQ-9).
        note_type NVARCHAR(20) NOT NULL,
        -- created_at: Thời gian tạo ghi chú, tự động (gắn ngày giờ REQ-16).
        created_at DATETIME2 DEFAULT GETDATE()
    );
END
-- Default và CHECK cho note_type.
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE name = 'DF_notes_type')
BEGIN
    ALTER TABLE  notes ADD CONSTRAINT DF_notes_type DEFAULT 'general' FOR note_type;
END
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_notes_type')
BEGIN
    ALTER TABLE  notes ADD CONSTRAINT CK_notes_type CHECK (note_type IN ('behavior', 'academic', 'general'));
END
-- Foreign key HS: Xóa cascade nếu xóa HS.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_notes_id_student')
BEGIN
    ALTER TABLE notes ADD CONSTRAINT FK_notes_id_student FOREIGN KEY (id_student) REFERENCES  students(id_student) ON DELETE CASCADE;
END
-- Foreign key GV: NO ACTION để tránh cycle/multiple paths.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_notes_id_teacher')
BEGIN
    ALTER TABLE  notes ADD CONSTRAINT FK_notes_id_teacher FOREIGN KEY (id_teacher) REFERENCES  teachers(id_teacher) ON DELETE NO ACTION;
END
-- Index trên id_student để query ghi chú theo HS nhanh.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_student' AND object_id = OBJECT_ID('notes'))
BEGIN
    CREATE INDEX idx_id_student ON  notes (id_student);
END
-- Composite index theo id_teacher và created_at để theo dõi hoạt động GV.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_teacher_date' AND object_id = OBJECT_ID('notes'))
BEGIN
    CREATE INDEX idx_id_teacher_date ON  notes (id_teacher, created_at);
END
GO

-- ========================================
-- PHẦN 9: BẢNG  QUIZZES (QUẢN LÝ QUIZ/TRÒ CHƠI) - REQ-13-14
-- Mục đích: Lưu quiz/trò chơi (flashcard, ghép chữ,...), dùng JSON cho linh hoạt (dễ quản lý nội dung).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'quizzes')
BEGIN
    -- Tạo bảng  quizzes với khóa chính IDENTITY.
    CREATE TABLE  quizzes (
        -- id_quiz: Khóa chính tự động tăng.
        id_quiz INT IDENTITY(1,1) PRIMARY KEY,
        -- id_teacher: GV tạo quiz.
        id_teacher INT NOT NULL,
        -- id_class: Lớp áp dụng quiz.
        id_class INT NOT NULL,
        -- quiz_title: Tiêu đề quiz (e.g., "Quiz Toán Tháng 9"), bắt buộc.
        quiz_title NVARCHAR(255) NOT NULL,
        -- quiz_type: Loại (quiz/flashcard/match,...), enum thay CHECK (REQ-14).
        quiz_type NVARCHAR(20) NOT NULL,
        -- quiz_questions: Câu hỏi lưu dạng JSON (linh hoạt cho nhiều loại trò chơi, SQL Server hỗ trợ JSON).
        quiz_questions NVARCHAR(MAX) NULL,  -- Lưu JSON dưới dạng string.
        -- quiz_duration: Thời gian giới hạn (phút), tùy chọn.
        quiz_duration INT NULL,
        -- created_at: Thời gian tạo quiz, tự động.
        created_at DATETIME2 DEFAULT GETDATE()
    );
END
-- CHECK constraint cho quiz_type.
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_quizzes_type')
BEGIN
    ALTER TABLE quizzes ADD CONSTRAINT CK_quizzes_type CHECK (quiz_type IN ('quiz', 'flashcard', 'match', 'fill_blank', 'other'));
END
-- Foreign key GV: Xóa cascade nếu xóa GV.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_quizzes_id_teacher')
BEGIN
    ALTER TABLE quizzes ADD CONSTRAINT FK_quizzes_id_teacher FOREIGN KEY (id_teacher) REFERENCES  teachers(id_teacher) ON DELETE CASCADE;
END
-- Foreign key lớp: NO ACTION để tránh multiple paths.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_quizzes_id_class')
BEGIN
    ALTER TABLE quizzes ADD CONSTRAINT FK_quizzes_id_class FOREIGN KEY (id_class) REFERENCES  classes(id_class) ON DELETE NO ACTION;
END
-- Index theo id_teacher và id_class để query quiz theo lớp nhanh.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_teacher_class' AND object_id = OBJECT_ID(' quizzes'))
BEGIN
    CREATE INDEX idx_id_teacher_class ON  quizzes (id_teacher, id_class);
END
GO

-- ========================================
-- PHẦN 10: BẢNG QUESTIONS (QUẢN LÝ CÂU HỎI TRONG QUIZ) 
-- Mục đích: Tạo câu hỏi và kết quả cho quiz/trò chơi (flashcard, ghép chữ,...).
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'questions')
BEGIN
    -- Tạo bảng questions với khóa chính IDENTITY.
    CREATE TABLE questions (
        -- id_questions: Khóa chính tự động tăng.
        id_questions INT IDENTITY(1,1) PRIMARY KEY,
        -- id_quiz: Khóa ngoại quiz.
        id_quiz INT NOT NULL,
        -- question_text: Câu hỏi.
        question_text NVARCHAR(MAX) NULL,
        -- answer_right: Câu trả lời đúng.
        answer_right NVARCHAR(MAX) NULL,
        -- answer_wrong: Câu trả lời sai .
        answer_wrong NVARCHAR(MAX) NULL
    );
END
-- Foreign key questions với quizzes: NO ACTION để tránh multiple paths.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_questions_quizzes')
BEGIN
    ALTER TABLE questions ADD CONSTRAINT FK_questions_quizzes FOREIGN KEY (id_quiz) REFERENCES  quizzes(id_quiz) ON DELETE NO ACTION;
END


-- ========================================
-- PHẦN 11: BẢNG  QUIZ_RESULTS (QUẢN LÝ KẾT QUẢ QUIZ) - REQ-13
-- Mục đích: Lưu kết quả HS tham gia quiz, hỗ trợ đánh giá và thống kê (tên phân biệt rõ với  quizzes).
-- Idempotent: Kiểm tra bảng/constraints/indexes tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'quiz_results')
BEGIN
    -- Tạo bảng  quiz_results với khóa chính IDENTITY.
    CREATE TABLE  quiz_results (
        -- id_quiz_result: Khóa chính tự động tăng.
        id_quiz_result INT IDENTITY(1,1) PRIMARY KEY,
        -- id_student: HS tham gia.
        id_student INT NOT NULL,
        -- id_quiz: Quiz liên kết.
        id_quiz INT NOT NULL,
        -- quiz_score: Điểm quiz của HS.
        quiz_score DECIMAL(5,2) NULL,
        -- quiz_answers: Câu trả lời lưu dạng JSON.
        quiz_answers NVARCHAR(MAX) NULL,  -- Lưu JSON dưới dạng string.
        -- completed_at: Thời gian hoàn thành quiz, tự động.
        completed_at DATETIME2 DEFAULT GETDATE()
    );
END
-- Foreign key HS: Xóa cascade nếu xóa HS.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_quiz_results_id_student')
BEGIN
    ALTER TABLE  quiz_results ADD CONSTRAINT FK_quiz_results_id_student FOREIGN KEY (id_student) REFERENCES  students(id_student) ON DELETE CASCADE;
END
-- Foreign key quiz: NO ACTION để tránh multiple paths.
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_quiz_results_id_quiz')
BEGIN
    ALTER TABLE  quiz_results ADD CONSTRAINT FK_quiz_results_id_quiz FOREIGN KEY (id_quiz) REFERENCES  quizzes(id_quiz) ON DELETE NO ACTION;
END
-- Composite index theo id_student và id_quiz cho thống kê cá nhân nhanh.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_student_quiz' AND object_id = OBJECT_ID('quiz_results'))
BEGIN
    CREATE INDEX idx_id_student_quiz ON  quiz_results (id_student, id_quiz);
END
-- Index trên id_quiz cho thống kê kết quả theo quiz (REQ-13).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_id_quiz' AND object_id = OBJECT_ID('quiz_results'))
BEGIN
    CREATE INDEX idx_id_quiz ON  quiz_results (id_quiz);
END
GO

-- ========================================
-- PHẦN 12: TRIGGER VÀ BẢNG  AUDIT_LOG (BẢO MẬT - THEO DÕI THAY ĐỔI)
-- Mục đích: Tự động ghi log thay đổi dữ liệu (REQ bảo mật: audit trail, dễ quản lý log qua prefix).
-- Idempotent: Kiểm tra bảng/constraints/indexes/trigger tồn tại.
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'audit_log')
BEGIN
    -- Tạo bảng  audit_log trước để trigger sử dụng.
    CREATE TABLE  audit_log (
        -- id_log: Khóa chính tự động tăng.
        id_log INT IDENTITY(1,1) PRIMARY KEY,
        -- log_table_name: Tên bảng bị thay đổi (e.g., 'students').
        log_table_name NVARCHAR(50) NOT NULL,
        -- log_record_id: ID record bị ảnh hưởng.
        log_record_id INT NOT NULL,
        -- log_action: Hành động (INSERT/UPDATE/DELETE), enum thay CHECK.
        log_action NVARCHAR(10) NOT NULL,
        -- log_old_value: Giá trị cũ dạng JSON (cho audit chi tiết).
        log_old_value NVARCHAR(MAX) NULL,
        -- log_new_value: Giá trị mới dạng JSON.
        log_new_value NVARCHAR(MAX) NULL,
        -- log_user_id: ID GV thực hiện thay đổi (liên kết  teachers.id_teacher).
        log_user_id INT NULL,
        -- log_timestamp: Thời gian sự kiện, tự động.
        log_timestamp DATETIME2 DEFAULT GETDATE()
    );
END
-- CHECK constraint cho log_action.
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_audit_log_action')
BEGIN
    ALTER TABLE  audit_log ADD CONSTRAINT CK_audit_log_action CHECK (log_action IN ('INSERT', 'UPDATE', 'DELETE'));
END
-- Index theo log_table_name và log_action để query log theo bảng/hành động nhanh.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_log_table_action' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX idx_log_table_action ON  audit_log (log_table_name, log_action);
END
-- Index theo log_timestamp để xem lịch sử theo thời gian (hỗ trợ báo cáo).
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_log_timestamp' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX idx_log_timestamp ON  audit_log (log_timestamp);
END
GO

-- Tạo trigger sau khi UPDATE bảng  students: Ghi log thay đổi (ví dụ: thay đổi tên HS).
-- Idempotent: Drop trigger nếu tồn tại trước khi tạo.
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'tr_students_after_update' AND parent_id = OBJECT_ID('students'))
BEGIN
    DROP TRIGGER tr_students_after_update;
END
-- SQL Server trigger syntax: Sử dụng IF UPDATE() để kiểm tra cột; sử dụng inserted/deleted tables.
--CREATE TRIGGER tr_students_after_update ON  students
--AFTER UPDATE
--AS
--BEGIN
    --- Chỉ trigger nếu full_name thay đổi (có thể mở rộng cho các cột khác).
    --IF UPDATE(full_name)
    --BEGIN
        --INSERT INTO  audit_log (log_table_name, log_record_id, log_action, log_old_value, log_new_value, log_user_id, log_timestamp)
        --SELECT 
            --' students' AS log_table_name,
            --i.id_student AS log_record_id,
            --'UPDATE' AS log_action,
            --(SELECT full_name FROM deleted d WHERE d.id_student = i.id_student FOR JSON PATH) AS log_old_value,  -- Old value
            --(SELECT full_name FROM inserted i_inner WHERE i_inner.id_student = i.id_student FOR JSON PATH) AS log_new_value,  -- New value
            --NULL AS log_user_id,  -- Có thể thay bằng SUSER_ID() nếu cần user ID
            --GETDATE() AS log_timestamp
        --FROM inserted i
        --INNER JOIN deleted d ON i.id_student = d.id_student;
    --END
--END;


/* Đảm bảo bạn đang sử dụng đúng database */
USE management_db;
GO

/* ========================================
PHẦN 1: THÊM GIÁO VIÊN (BẮT BUỘC)
(Vì bảng 'classes' yêu cầu 'id_teacher')
========================================
*/

-- Thêm giáo viên 1 (nếu chưa tồn tại)
IF NOT EXISTS (SELECT 1 FROM teachers WHERE username = 'teacher_nva')
BEGIN
    INSERT INTO teachers (full_name, username, password_hash, email, user_role, is_active)
    VALUES (N'Nguyễn Văn A', 'teacher_nva', 'placeholder_hash_123', 'nva@example.com', 'teacher', 1);
    PRINT 'Đã thêm giáo viên Nguyễn Văn A';
END

-- Thêm giáo viên 2 (nếu chưa tồn tại)
IF NOT EXISTS (SELECT 1 FROM teachers WHERE username = 'teacher_btm')
BEGIN
    INSERT INTO teachers (full_name, username, password_hash, email, user_role, is_active)
    VALUES (N'Bùi Thị M', 'teacher_btm', 'placeholder_hash_456', 'btm@example.com', 'teacher', 1);
    PRINT 'Đã thêm giáo viên Bùi Thị M';
END
GO

/* ========================================
PHẦN 2: THÊM LỚP HỌC (CLASSES)
(Script này sẽ tự động tìm ID giáo viên đã tạo ở trên)
========================================
*/

-- Khai báo biến để giữ ID
DECLARE @TeacherID_NVA INT;
DECLARE @TeacherID_BTM INT;

-- Lấy ID của giáo viên
SELECT @TeacherID_NVA = id_teacher FROM teachers WHERE username = 'teacher_nva';
SELECT @TeacherID_BTM = id_teacher FROM teachers WHERE username = 'teacher_btm';

-- Thêm Lớp 10A1 (do GV Nguyễn Văn A phụ trách)
IF NOT EXISTS (SELECT 1 FROM classes WHERE class_name = '10A1')
BEGIN
    INSERT INTO classes (class_name, id_teacher, class_description)
    VALUES (N'10A1', @TeacherID_NVA, N'Lớp 10A1 (GVCN: Nguyễn Văn A)');
    PRINT 'Đã thêm lớp 10A1';
END

-- Thêm Lớp 11B2 (do GV Bùi Thị M phụ trách)
IF NOT EXISTS (SELECT 1 FROM classes WHERE class_name = '11B2')
BEGIN
    INSERT INTO classes (class_name, id_teacher, class_description)
    VALUES (N'11B2', @TeacherID_BTM, N'Lớp 11B2 (GVCN: Bùi Thị M)');
    PRINT 'Đã thêm lớp 11B2';
END
GO

/* ========================================
PHẦN 3: THÊM HỌC SINH (STUDENTS)
(Script này sẽ tự động tìm ID lớp học đã tạo ở trên)
========================================
*/

-- Khai báo biến để giữ ID lớp
DECLARE @ClassID_10A1 INT;
DECLARE @ClassID_11B2 INT;

-- Lấy ID của lớp
SELECT @ClassID_10A1 = id_class FROM classes WHERE class_name = '10A1';
SELECT @ClassID_11B2 = id_class FROM classes WHERE class_name = '11B2';

-- Thêm 2 học sinh vào lớp 10A1
IF NOT EXISTS (SELECT 1 FROM students WHERE student_code = 'HS10A1_001')
BEGIN
    INSERT INTO students (student_code, full_name, date_of_birth, gender, hometown, id_class)
    VALUES (N'HS10A1_001', N'Trần Thị B', '2008-05-10', 'female', N'Hà Nội', @ClassID_10A1);
    PRINT 'Đã thêm học sinh Trần Thị B (HS10A1_001)';
END

IF NOT EXISTS (SELECT 1 FROM students WHERE student_code = 'HS10A1_002')
BEGIN
    INSERT INTO students (student_code, full_name, date_of_birth, gender, hometown, id_class)
    VALUES (N'HS10A1_002', N'Lê Văn C', '2008-09-22', 'male', N'Đà Nẵng', @ClassID_10A1);
    PRINT 'Đã thêm học sinh Lê Văn C (HS10A1_002)';
END

-- Thêm 1 học sinh vào lớp 11B2
IF NOT EXISTS (SELECT 1 FROM students WHERE student_code = 'HS11B2_001')
BEGIN
    INSERT INTO students (student_code, full_name, date_of_birth, gender, hometown, id_class)
    VALUES (N'HS11B2_001', N'Phạm Thu D', '2007-01-15', 'female', N'TP. Hồ Chí Minh', @ClassID_11B2);
    PRINT 'Đã thêm học sinh Phạm Thu D (HS11B2_001)';
END
GO



-- ========= BẢNG 1: CATEGORIES (QUẢN LÝ CHỦ ĐỀ) =========
-- Mục đích: Lưu tên các chủ đề (ví dụ: "Bài 5", "Động vật", "Trái cây")
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE Categories (
        -- Khóa chính tự động tăng
        id_category INT IDENTITY(1,1) PRIMARY KEY,
        
        -- Tên chủ đề (phải là NVARCHAR để hỗ trợ Tiếng Việt)
        category_name NVARCHAR(100) NOT NULL,
        
        -- (Tùy chọn) ID giáo viên tạo chủ đề này
        id_teacher INT NULL 
    );
    PRINT 'Đã tạo bảng Categories.'
END
GO


-- ========= BẢNG 2: VOCABULARY (KHO NGUYÊN LIỆU) =========
-- Mục đích: Lưu trữ TẤT CẢ các cặp Từ vựng-Hình ảnh
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vocabulary')
BEGIN
    CREATE TABLE Vocabulary (
        -- Khóa chính tự động tăng
        id_vocabulary INT IDENTITY(1,1) PRIMARY KEY,
        
        -- Từ vựng (Tiếng Việt hoặc Tiếng Anh)
        WordText NVARCHAR(100) NOT NULL,
        
        -- Dữ liệu hình ảnh (lưu trực tiếp vào CSDL)
        WordImage VARBINARY(MAX) NOT NULL,
        
        -- Khóa ngoại: Từ vựng này thuộc chủ đề nào?
        id_category INT NULL, 
        
        -- (Tùy chọn) ID giáo viên đã upload từ này
        id_teacher INT NULL,
        created_at DATETIME2 DEFAULT GETDATE(),

        -- Tạo liên kết đến bảng Categories
        -- Nếu xóa 1 Chủ đề, các từ vựng thuộc chủ đề đó sẽ bị SET NULL (chứ không bị xóa)
        FOREIGN KEY (id_category) REFERENCES Categories(id_category) ON DELETE SET NULL
    );
    PRINT 'Đã tạo bảng Vocabulary.'
END
GO


-- ========= BẢNG 3: QUIZZES (CÔNG THỨC GAME) =========
-- Mục đích: Lưu cấu hình của một game (ví dụ: "Game Lật Thẻ Bài 5")
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Quizzes')
BEGIN
    CREATE TABLE Quizzes (
        -- Khóa chính tự động tăng
        id_quiz INT IDENTITY(1,1) PRIMARY KEY,
        
        -- Game này của giáo viên nào
        id_teacher INT NOT NULL,
        
        -- Game này dành cho lớp nào (Dựa trên bảng 'classes' bạn đã có)
        id_class INT NOT NULL,
        
        -- Tên game mà giáo viên đặt
        quiz_title NVARCHAR(255) NOT NULL,
        
        -- Loại game (để code C# biết load UC nào)
        quiz_type NVARCHAR(50) NOT NULL, -- 'MemoryFlip', 'WordMatch', 'FillInBlank'
        
        -- Cấu hình (độ khó, thời gian) lưu dạng JSON
        quiz_config_json NVARCHAR(MAX) NULL, -- Ví dụ: "{ 'difficulty': '4x4' }"
        
        created_at DATETIME2 DEFAULT GETDATE(),

        -- Tạo liên kết đến bảng 'classes' (bảng bạn đã có)
        FOREIGN KEY (id_class) REFERENCES classes(id_class)
        
        -- (Bạn có thể thêm khóa ngoại cho id_teacher nếu có bảng Teachers)
        -- FOREIGN KEY (id_teacher) REFERENCES Teachers(id_teacher)
    );
    PRINT 'Đã tạo bảng Quizzes.'
END
GO


-- ========= BẢNG 4: QUIZ_CONTENT (BẢNG LIÊN KẾT NỘI DUNG) =========
-- Mục đích: Chỉ định Game A sẽ dùng Từ vựng X, Y, Z...
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Quiz_Content')
BEGIN
    CREATE TABLE Quiz_Content (
        -- Khóa ngoại 1: Game nào?
        id_quiz INT NOT NULL,
        
        -- Khóa ngoại 2: Từ vựng nào?
        id_vocabulary INT NOT NULL,
        
        -- Khóa chính kép: Đảm bảo 1 game không thể chứa 1 từ vựng 2 lần
        PRIMARY KEY (id_quiz, id_vocabulary), 
        
        -- Tạo liên kết đến bảng Quizzes
        -- Nếu XÓA 1 GAME (Quiz), tất cả nội dung liên kết của nó TỰ ĐỘNG BỊ XÓA
        FOREIGN KEY (id_quiz) REFERENCES Quizzes(id_quiz) ON DELETE CASCADE,
        
        -- Tạo liên kết đến bảng Vocabulary
        -- Nếu XÓA 1 TỪ VỰNG (trong Kho), nó cũng tự động bị XÓA khỏi các game
        FOREIGN KEY (id_vocabulary) REFERENCES Vocabulary(id_vocabulary) ON DELETE CASCADE
    );
    PRINT 'Đã tạo bảng Quiz_Content.'
END
GO

PRINT '--- Hoàn tất tạo CSDL cho Game ---'

ALTER TABLE Vocabulary
ADD VocabType NVARCHAR(50) NOT NULL DEFAULT 'Word';

ALTER TABLE Vocabulary
ALTER COLUMN WordImage VARBINARY(MAX) NULL;