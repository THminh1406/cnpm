using SchoolManager.DAL;
using SchoolManager.DTO; // Dùng DTO
using System;
using System.Collections.Generic;

namespace SchoolManager.BLL
{
    public class Business_Logic_Manual_Roll_Call
    {
        private data_Access_Manual_Roll_Call dal = new data_Access_Manual_Roll_Call();

        // Đổi tên DTO và tên hàm
        public bool SaveRollCallRecord(List<Roll_Call_Records> list_Roll_Call)
        {
            // Gọi hàm DAL đã sửa
            return dal.SaveRollCallRecord(list_Roll_Call);
        }


        public List<Roll_Call_Records> GetRollCallRecords(int id_Class, DateTime date)
        {
            return dal.GetRollCallRecords(id_Class, date);
        }

    }
}