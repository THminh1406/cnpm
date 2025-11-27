using Guna.UI2.WinForms;
using SchoolManager.BLL;
using SchoolManager.DTO;
using SchoolManager.Presentations.Forms; // Để dùng Form_EditStudent
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class UC_Student_Management : UserControl
    {
        // Các biến logic
        private Business_Logic_Classes bll_Classes;
        private Business_Logic_Students bll_Students;
        private int currentClassId = 0;
        private Random rng = new Random();

        public UC_Student_Management()
        {
            InitializeComponent();
        }

        // === SỰ KIỆN LOAD FORM ===
        private void UC_Student_Management_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            // 1. Khởi tạo BLL
            bll_Classes = new Business_Logic_Classes();
            bll_Students = new Business_Logic_Students();

            // 2. Cấu hình DataGridView
            dgv_ClassList.AutoGenerateColumns = false;
            // Gắn sự kiện vẽ STT (nếu chưa gắn trong Designer)
            this.dgv_ClassList.RowPostPaint += dgv_ClassList_RowPostPaint;

            // 3. Tải danh sách lớp
            LoadClassesToComboBox();

            if (txt_Search != null)
            {
                txt_Search.TextChanged -= txt_Search_TextChanged;
                txt_Search.TextChanged += txt_Search_TextChanged;
            }
        }

        // === TẢI COMBOBOX LỚP ===
        private void LoadClassesToComboBox()
        {
            try
            {
                List<Classes> listClass = bll_Classes.GetAllClasses();
                // Thêm mục mặc định
                listClass.Insert(0, new Classes { id_Class = 0, name_Class = "-- Chọn lớp --" });

                // Tìm ComboBox trong form (để đảm bảo an toàn nếu tên biến bị ẩn)
                var cboObj = this.Controls.Find("cbo_SelectClass", true).FirstOrDefault();
                if (cboObj is Guna2ComboBox cbo)
                {
                    cbo.DataSource = listClass;
                    cbo.DisplayMember = "name_Class";
                    cbo.ValueMember = "id_Class";

                    // Gắn sự kiện chọn
                    cbo.SelectedIndexChanged -= cbo_SelectClass_SelectedIndexChanged; // Tránh gán trùng
                    cbo.SelectedIndexChanged += cbo_SelectClass_SelectedIndexChanged;

                    // Chọn mặc định lớp đầu tiên có dữ liệu (index 1)
                    if (listClass.Count > 1) cbo.SelectedIndex = 1;
                }
                else
                {
                    // Dự phòng nếu không tìm thấy ComboBox, load thẳng lớp đầu tiên
                    if (listClass.Count > 1) LoadStudentList(listClass[1].id_Class);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp: " + ex.Message);
            }
        }

        // === SỰ KIỆN CHỌN LỚP ===
        private void cbo_SelectClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is Guna2ComboBox cbo && cbo.SelectedValue != null)
            {
                if (int.TryParse(cbo.SelectedValue.ToString(), out int classId) && classId > 0)
                {
                    LoadStudentList(classId);
                    if (lbl_Class != null) lbl_Class.Text = "Lớp: " + cbo.Text;
                }
                else
                {
                    // Nếu chọn "-- Chọn lớp --"
                    dgv_ClassList.DataSource = null;
                    if (lbl_Class != null) lbl_Class.Text = "Vui lòng chọn lớp";
                }
            }
        }

        // === TẢI DANH SÁCH HỌC SINH ===
        public void LoadStudentList(int classId)
        {
            this.currentClassId = classId;
            try
            {
                List<Students> students = bll_Students.GetStudentsByClassId(classId);
                dgv_ClassList.DataSource = students;

                // --- MAP CỘT DỮ LIỆU (Khớp với DTO mới) ---
                if (dgv_ClassList.Columns.Contains("student_Code"))
                    dgv_ClassList.Columns["student_Code"].DataPropertyName = "code_Student";

                if (dgv_ClassList.Columns.Contains("name_Student"))
                    dgv_ClassList.Columns["name_Student"].DataPropertyName = "name_Student";

                if (dgv_ClassList.Columns.Contains("birthday"))
                    dgv_ClassList.Columns["birthday"].DataPropertyName = "birthday";

                if (dgv_ClassList.Columns.Contains("gender"))
                    dgv_ClassList.Columns["gender"].DataPropertyName = "GenderText"; // "Nam"/"Nữ"

                if (dgv_ClassList.Columns.Contains("ethnicity"))
                    dgv_ClassList.Columns["ethnicity"].DataPropertyName = "ethnicity";

                // --- ẨN CÁC CỘT THỪA ---
                // Không còn address, parent... nên không cần ẩn chúng nữa
                string[] hiddenCols = { "id_Student", "id_Class", "GenderText" };
                foreach (string col in hiddenCols)
                {
                    if (dgv_ClassList.Columns.Contains(col)) dgv_ClassList.Columns[col].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        // === SỐ THỨ TỰ (STT) ===
        private void dgv_ClassList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dgv_ClassList.Columns.Contains("STT"))
            {
                dgv_ClassList.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
            }
        }

        // === VẼ ICON SỬA & XÓA (CĂN GIỮA) ===
        private void guna2DataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Kiểm tra cột thao tác "action"
            if (e.RowIndex >= 0 && e.ColumnIndex == dgv_ClassList.Columns["action"].Index)
            {
                // 1. Vẽ nền mặc định
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                int iconSize = 32;
                int padding = 12;

                // 2. Tính toán căn giữa cụm 2 icon
                int totalWidth = (iconSize * 2) + padding;
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

                // Vị trí icon thứ 2 (Xóa)
                int x_Delete = startX + iconSize + padding;

                // 3. Vẽ Icon SỬA
                if (Properties.Resources.edit != null)
                    e.Graphics.DrawImage(Properties.Resources.edit, new Rectangle(startX, y, iconSize, iconSize));

                // 4. Vẽ Icon XÓA
                if (Properties.Resources.delete != null)
                    e.Graphics.DrawImage(Properties.Resources.delete, new Rectangle(x_Delete, y, iconSize, iconSize));

                e.Handled = true;
            }
        }

        // === XỬ LÝ CLICK (SỬA & XÓA) ===
        private void guna2DataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Kiểm tra cột "action"
            if (e.RowIndex >= 0 && e.ColumnIndex == dgv_ClassList.Columns["action"].Index)
            {
                int iconSize = 32;
                int padding = 12;
                int totalWidth = (iconSize * 2) + padding;
                int startX = (dgv_ClassList.Columns[e.ColumnIndex].Width - totalWidth) / 2;
                int clickX = e.X;

                // Lấy dữ liệu học sinh
                Students currentStudent = (Students)dgv_ClassList.Rows[e.RowIndex].DataBoundItem;
                if (currentStudent == null) return;

                // --- CLICK NÚT SỬA (Trái) ---
                if (clickX >= startX && clickX <= startX + iconSize)
                {
                    Form_EditStudent frm = new Form_EditStudent(currentStudent);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadStudentList(this.currentClassId); // Tải lại nếu sửa thành công
                    }
                }
                // --- CLICK NÚT XÓA (Phải) ---
                else if (clickX >= startX + iconSize + padding && clickX <= startX + iconSize * 2 + padding)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa học sinh [{currentStudent.name_Student}] không?",
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (bll_Students.DeleteStudent(currentStudent.id_Student))
                        {
                            MessageBox.Show("Đã xóa thành công!");
                            LoadStudentList(this.currentClassId);
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại.");
                        }
                    }
                }
            }
        }

        // === CÁC NÚT CHUYỂN TRANG ===
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            index mainForm = this.FindForm() as index;
            if (mainForm != null) mainForm.showUC(mainForm.uC_Add_New_Student1);
            mainForm.uC_Add_New_Student1.Reload();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            index mainForm = this.FindForm() as index;
            if (mainForm != null) mainForm.showUC(mainForm.uC_Import_File_Excel1);
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa chọn lớp thì không tìm
            if (this.currentClassId <= 0) return;

            string keyword = txt_Search.Text.Trim();

            // Gọi BLL để tìm kiếm
            List<Students> result = bll_Students.SearchStudents(this.currentClassId, keyword);

            // Cập nhật lại bảng
            dgv_ClassList.DataSource = result;
        }

        private async void btn_Random_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem lưới có dữ liệu không
            if (dgv_ClassList.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách học sinh đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Khóa nút để tránh bấm liên tục khi đang quay
            btn_Random.Enabled = false;

            try
            {
                int totalRows = dgv_ClassList.Rows.Count;
                int loops = 30; // Tổng số lần nhảy (hiệu ứng quay)
                int delay = 30; // Tốc độ ban đầu (ms) - càng nhỏ càng nhanh

                // 2. Vòng lặp tạo hiệu ứng quay số
                for (int i = 0; i < loops; i++)
                {
                    // Chọn ngẫu nhiên một dòng index
                    int randomIndex = rng.Next(totalRows);

                    // Bỏ chọn các dòng cũ
                    dgv_ClassList.ClearSelection();

                    // Chọn dòng mới
                    dgv_ClassList.Rows[randomIndex].Selected = true;

                    // Tự động cuộn lưới đến dòng đang chọn (để người dùng thấy)
                    dgv_ClassList.FirstDisplayedScrollingRowIndex = randomIndex;

                    // --- LOGIC LÀM CHẬM DẦN ---
                    // 5 lần nhảy cuối cùng sẽ chậm dần lại để tạo kịch tính
                    if (i > loops - 10) delay += 20;
                    if (i > loops - 5) delay += 50;

                    // Dừng một chút trước khi nhảy tiếp
                    await Task.Delay(delay);
                }

                // 3. Lấy thông tin người chiến thắng (Dòng đang được chọn cuối cùng)
                if (dgv_ClassList.SelectedRows.Count > 0)
                {
                    var row = dgv_ClassList.SelectedRows[0];

                    // Lấy tên học sinh (Thay "full_name" bằng Tên Cột thực tế trong Grid của bạn)
                    // Nếu bạn không nhớ tên cột, hãy thử dùng index: row.Cells[1].Value.ToString()
                    string name = row.Cells["name_Student"].Value != null ? row.Cells["name_Student"].Value.ToString() : "Không tên";
                    string code = row.Cells["student_Code"].Value != null ? row.Cells["student_Code"].Value.ToString() : "";

                    // Hiển thị kết quả
                    MessageBox.Show($"Người được gọi là:\n\n{name}\n({code})", "Kết quả Random", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Mở lại nút sau khi quay xong
                btn_Random.Enabled = true;
            }
        }
    }
}