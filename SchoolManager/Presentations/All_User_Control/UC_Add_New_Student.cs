using Guna.UI2.WinForms;
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

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class UC_Add_New_Student : UserControl
    {

        Business_Logic_Classes bll_Classes;
        Business_Logic_Students bll_Students;

        // Danh sách 54 dân tộc chuẩn
        string[] ethnicList = {
            "Kinh", "Tày", "Thái", "Mường", "Khmer", "Hoa", "Nùng", "H'Mông", "Dao", "Gia Rai",
            "Ê Đê", "Ba Na", "Sán Chay", "Chăm", "Cơ Ho", "Xơ Đăng", "Sán Dìu", "Hrê", "Ra Glai",
            "Mnông", "Thổ", "Stiêng", "Khơ Mú", "Bru - Vân Kiều", "Cơ Tu", "Giáy", "Tà Ôi", "Mạ",
            "Giẻ-Triêng", "Co", "Chơ Ro", "Xinh Mun", "Hà Nhì", "Chu Ru", "Lào", "La Chí", "Kháng",
            "Phù Lá", "La Hủ", "La Ha", "Pà Thẻn", "Lự", "Ngái", "Chứt", "Lô Lô", "Mảng", "Cơ Lao",
            "Bố Y", "Cống", "Si La", "Pu Péo", "Rơ Măm", "Brâu", "Ơ Đu"
        };
        public UC_Add_New_Student()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void UC_Add_New_Student_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            bll_Classes = new Business_Logic_Classes();
            bll_Students = new Business_Logic_Students();
            LoadInitData();
        }

        public void Reload()
        {
            LoadInitData();

            // Xóa sạch bảng tạm thời
            dgv_StudentAdded.DataSource = null;
            dgv_StudentAdded.Rows.Clear();

            // Xóa sạch ô nhập
            ClearInputs(false);
        }

        private void LoadInitData()
        {
            // 1. Load Lớp (Dùng cbo_Class hoặc cbo_SeclectClass tùy design của bạn)
            // Trong file design bạn có 2 cái cbo lớp, tôi dùng cbo_Class
            cbo_Class.DataSource = bll_Classes.GetAllClasses();
            cbo_Class.DisplayMember = "name_Class";
            cbo_Class.ValueMember = "id_Class";

            // 2. Load Dân tộc (guna2ComboBox4)
            cbo_Nation.Items.Clear();
            cbo_Nation.Items.AddRange(ethnicList);
            cbo_Nation.SelectedIndex = 0; // Mặc định Kinh

            // 3. Load Giới tính (txt_Gender là ComboBox)
            if (txt_Gender.Items.Count == 0)
            {
                txt_Gender.Items.Add("Nam");
                txt_Gender.Items.Add("Nữ");
            }
            txt_Gender.SelectedIndex = 0;

            // 4. Cấu hình DataGridView hiển thị tạm
            SetupGridView();
        }

        private void SetupGridView()
        {
            dgv_StudentAdded.AutoGenerateColumns = false;
            dgv_StudentAdded.Columns.Clear();

            // Thêm cột thủ công
            dgv_StudentAdded.Columns.Add("colCode", "Mã HS");
            dgv_StudentAdded.Columns.Add("colName", "Họ Tên");
            dgv_StudentAdded.Columns.Add("colClass", "Lớp");
            dgv_StudentAdded.Columns.Add("colGender", "Giới tính");
            dgv_StudentAdded.Columns.Add("colEth", "Dân tộc");
            dgv_StudentAdded.Columns.Add("colDob", "Ngày sinh");
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_NameStudent.Text) || string.IsNullOrWhiteSpace(txt_StudentCode.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Tên và Mã số.", "Thông báo");
                return;
            }

            if (cbo_Class.SelectedValue == null) return;

            // Xử lý giới tính để khớp với DTO logic (male/female)
            string genderSelection = txt_Gender.SelectedItem.ToString();
            string genderDB = (genderSelection == "Nam") ? "male" : "female";

            // Tạo DTO
            Students s = new Students
            {
                code_Student = txt_StudentCode.Text.Trim(),
                name_Student = txt_NameStudent.Text.Trim(),
                id_Class = (int)cbo_Class.SelectedValue,
                gender = genderDB, // Lưu male/female vào DB
                ethnicity = cbo_Nation.SelectedItem.ToString(),
                birthday = dtp_Birthday.Value
            };

            try
            {
                // 1. Lưu vào CSDL
                if (bll_Students.AddStudent(s))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo");

                    // 2. Hiện lên bảng tạm (Chỉ hiện, không load lại từ DB)
                    dgv_StudentAdded.Rows.Add(
                        s.code_Student,
                        s.name_Student,
                        cbo_Class.Text, // Tên lớp
                        genderSelection, // Hiện "Nam"/"Nữ" cho đẹp
                        s.ethnicity,
                        s.birthday.ToString("dd/MM/yyyy")
                    );

                    // 3. Xóa ô nhập để nhập tiếp (Giữ lại lớp)
                    ClearInputs(true);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại.", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // === NÚT HỦY (Làm mới ô nhập) ===
        // Bạn nhớ gán sự kiện Click cho nút Hủy (ví dụ btn_Cancel)
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearInputs(false); // false nghĩa là xóa sạch, không giữ lại lớp
        }

        private void ClearInputs(bool keepClass)
        {
            txt_StudentCode.Clear();
            txt_NameStudent.Clear();
            dtp_Birthday.Value = DateTime.Now;
            txt_Gender.SelectedIndex = 0;
            if (cbo_Nation.Items.Count > 0) cbo_Nation.SelectedIndex = 0;

            if (!keepClass && cbo_Class.Items.Count > 0) cbo_Class.SelectedIndex = 0;

            txt_StudentCode.Focus();
        }
    }
}
