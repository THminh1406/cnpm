using System;

namespace SchoolManager.DTO
{
    public class Students
    {
        public int id_Student { get; set; }
        public int id_Class { get; set; }

        // 5 Thông tin cốt lõi
        public string code_Student { get; set; }
        public string name_Student { get; set; }
        public DateTime birthday { get; set; }
        public string gender { get; set; }
        public string ethnicity { get; set; } // Dân tộc

        // Thuộc tính phụ để hiển thị (Không lưu trong DB)
        public string GenderText
        {
            get
            {
                if (gender == "male") return "Nam";
                if (gender == "female") return "Nữ";
                return "";
            }
        }
    }
}