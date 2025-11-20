using SchoolManager.BLL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager.Presentations.Forms
{
    public partial class Form_EditStudent : Form
    {
        private Students currentStudent;
        private Business_Logic_Students bll = new Business_Logic_Students();
        public Form_EditStudent(Students s)
        {
            InitializeComponent();
            this.currentStudent = s;
        }

        private void Form_EditStudent_Load(object sender, EventArgs e)
        {
            // Cấu hình ComboBox
            cbo_Gender.Items.Clear();
            cbo_Gender.Items.Add("Nam");
            cbo_Gender.Items.Add("Nữ");

            // Đổ dữ liệu hiện tại lên Form
            txt_Name.Text = currentStudent.name_Student;      

            txt_Code.Text = currentStudent.code_Student;       // SỬA MÃ SỐ
            dtp_Birthday.Value = currentStudent.birthday;      // SỬA NGÀY SINH      // SỬA ĐỊA CHỈ

            // Xử lý Giới tính (Database lưu 'male'/'female' -> Form hiện 'Nam'/'Nữ')
            if (currentStudent.gender == "male") cbo_Gender.SelectedItem = "Nam";
            else if (currentStudent.gender == "female") cbo_Gender.SelectedItem = "Nữ";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu mới từ Form gán vào Object
            currentStudent.name_Student = txt_Name.Text;
            currentStudent.code_Student = txt_Code.Text;
            currentStudent.birthday = dtp_Birthday.Value;

            // Chuyển đổi Giới tính về tiếng Anh để lưu DB
            if (cbo_Gender.SelectedItem != null)
            {
                currentStudent.gender = cbo_Gender.SelectedItem.ToString() == "Nam" ? "male" : "female";
            }

            // 2. Gọi BLL để cập nhật
            if (bll.UpdateStudent(currentStudent))
            {
                MessageBox.Show("Cập nhật thành công!");
                this.DialogResult = DialogResult.OK; // Báo thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật. Vui lòng kiểm tra lại mã số (có thể bị trùng).");
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
