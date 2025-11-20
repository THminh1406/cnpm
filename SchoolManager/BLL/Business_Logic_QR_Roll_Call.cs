using SchoolManager.DAL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchoolManager.BLL
{
    public class Business_Logic_QR_Roll_Call
    {
        private data_Access_QR_Roll_Call dal_QR = new data_Access_QR_Roll_Call();
        private Business_Logic_Students bll_Students = new Business_Logic_Students();

        /// <summary>
        /// Xử lý nghiệp vụ quét mã QR
        /// </summary>
        /// <returns>Tên học sinh nếu thành công, hoặc thông báo lỗi</returns>
        public string MarkStudentPresent(string studentCode, int id_Class_Expected, DateTime date)
        {
            // 1. Tìm học sinh bằng mã QR (studentCode)
            Students student = bll_Students.GetStudentByCode(studentCode);

            // 2. Kiểm tra lỗi
            if (student == null)
            {
                return "LỖI: Mã QR không hợp lệ!";
            }
            if (student.id_Class != id_Class_Expected)
            {
                return $"LỖI: {student.name_Student} không thuộc lớp này!";
            }

            // 3. Điểm danh
            bool success = dal_QR.MarkStudentAttendance(student.id_Student, date, "present", "qr");

            if (success)
            {
                return student.name_Student; // Thành công, trả về tên
            }
            else
            {
                return "LỖI: Không thể lưu điểm danh.";
            }
        }

        /// <summary>
        /// Lấy danh sách đã điểm danh hôm nay
        /// </summary>
        public List<Students> GetTodaysAttendance(int id_Class, DateTime date)
        {
            return dal_QR.GetTodaysAttendance(id_Class, date);
        }

        public bool ResetAttendance(int id_Class, DateTime date)
        {
            // Chỉ cần gọi DAL để xóa
            return dal_QR.DeleteAttendanceByClassAndDate(id_Class, date);

            // (Nếu bạn thêm hàm DAL ở file Manual_Roll_Call thì phải
            // khởi tạo dal_Manual và gọi dal_Manual.DeleteAttendanceByClassAndDate)
        }
    }
}