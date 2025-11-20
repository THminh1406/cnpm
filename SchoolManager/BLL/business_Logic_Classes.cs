using SchoolManager.DAL;
using SchoolManager.DTO;
using System.Collections.Generic;

namespace SchoolManager.BLL
{
    public class Business_Logic_Classes
    {
        private data_Access_Classes dal = new data_Access_Classes();

        public List<Classes> GetAllClasses()
        {
            // Gọi hàm DAL đã sửa (chữ G hoa)
            return dal.GetAllClasses();
        }
    }
}