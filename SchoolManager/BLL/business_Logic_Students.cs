using SchoolManager.DAL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool AddStudent(Students s)
        {
            if (string.IsNullOrWhiteSpace(s.name_Student) || string.IsNullOrWhiteSpace(s.code_Student))
                return false;

            if (dal.CheckStudentCodeExists(s.code_Student))
                throw new Exception("Mã học sinh đã tồn tại!");

            return dal.InsertStudent(s);
        }

        public List<Students> SearchStudents(int classId, string keyword)
        {
            // 1. Lấy toàn bộ danh sách học sinh của lớp đó
            List<Students> allStudents = GetStudentsByClassId(classId);

            // 2. Nếu từ khóa rỗng, trả về tất cả
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return allStudents;
            }

            // 3. Lọc theo Tên hoặc Mã học sinh (Không phân biệt hoa thường)
            string lowerKeyword = keyword.ToLower();

            var filteredList = allStudents.Where(s =>
                s.name_Student.ToLower().Contains(lowerKeyword) ||
                s.code_Student.ToLower().Contains(lowerKeyword)
            ).ToList();

            return filteredList;
        }
    }
}