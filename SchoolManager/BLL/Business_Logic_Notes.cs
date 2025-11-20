using SchoolManager.DAL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.BLL
{
    public class Business_Logic_Notes
    {
        // Khởi tạo lớp truy cập dữ liệu (DAL)
        private data_Access_Notes dal = new data_Access_Notes();

        // 1. Lấy danh sách ghi chú (Có lọc theo lớp và loại)
        public List<NoteDTO> GetNotes(int classId, string noteTypeFilter)
        {
            // Kiểm tra classId hợp lệ (chặn nếu chưa chọn lớp)
            if (classId <= 0) return new List<NoteDTO>();

            return dal.GetNotes(classId, noteTypeFilter);
        }

        // 2. Thêm ghi chú mới
        public bool AddNote(NoteDTO note)
        {
            // --- Kiểm tra Logic nghiệp vụ ---

            // Không cho phép đối tượng null
            if (note == null) return false;

            // Bắt buộc phải có nội dung (không được để trống hoặc chỉ toàn khoảng trắng)
            if (string.IsNullOrWhiteSpace(note.NoteContent)) return false;

            // Bắt buộc phải chọn học sinh
            if (note.StudentId <= 0) return false;

            // Nếu mọi thứ OK thì gọi DAL để lưu
            return dal.AddNote(note);
        }

        // 3. Xóa 1 ghi chú cụ thể
        public bool DeleteNote(int noteId)
        {
            if (noteId <= 0) return false;
            return dal.DeleteNote(noteId);
        }

        // 4. Xóa tất cả ghi chú theo bộ lọc (Dùng cho nút "Xóa tất cả")
        public bool DeleteNotesByFilter(int classId, string noteTypeFilter)
        {
            if (classId <= 0) return false;

            // Gọi DAL để xóa hàng loạt
            return dal.DeleteNotesByFilter(classId, noteTypeFilter);
        }

        public bool UpdateNoteContent(int noteId, string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent)) return false; // Không cho lưu rỗng
            return dal.UpdateNoteContent(noteId, newContent);
        }

        public List<NoteDTO> GetNotesForExport(int classId, DateTime fromDate, DateTime toDate)
        {
            // Xử lý giờ phút để lấy trọn vẹn ngày cuối
            DateTime endOfDay = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);
            return dal.GetNotesForExport(classId, fromDate, endOfDay);
        }
    }
}