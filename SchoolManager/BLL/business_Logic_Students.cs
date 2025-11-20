using SchoolManager.DAL;
using SchoolManager.DTO;
using System.Collections.Generic;

namespace SchoolManager.BLL
{
    public class Business_Logic_Students
    {
        private data_Access_Students dal = new data_Access_Students();

        public List<Students> GetStudentsByClassId(int id_Class)
        {
            return dal.GetStudentsByClassId(id_Class);
        }

        public Students GetStudentByCode(string studentCode)
        {
            return dal.GetStudentByCode(studentCode);
        }

        // === THÊM HÀM NÀY ===
        public bool DeleteStudent(int id)
        {
            return dal.DeleteStudent(id);
        }

        public bool UpdateStudent(Students s)
        {
            // Có thể thêm kiểm tra dữ liệu nếu cần (ví dụ: Mã số không được để trống)
            if (string.IsNullOrEmpty(s.code_Student)) return false;

            return dal.UpdateStudent(s);
        }

        public bool ImportStudentList(List<Students> list)
        {
            if (list == null || list.Count == 0) return false;
            return dal.ImportStudentList(list);
        }
    }
}