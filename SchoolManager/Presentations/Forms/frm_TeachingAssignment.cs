using System;
using System.Windows.Forms;
using SchoolManager.BLL;

namespace SchoolManager.Presentations
{
    public partial class frm_TeachingAssignment : Form
    {
        private int _teacherId;
        private Business_Logic_ApproveRegistration bll = new Business_Logic_ApproveRegistration();

        public frm_TeachingAssignment(int teacherId, string teacherName)
        {
            InitializeComponent();
            _teacherId = teacherId;
            this.Text = "Phân công giảng dạy - " + teacherName;
            this.Load += Frm_TeachingAssignment_Load;
        }

        private void Frm_TeachingAssignment_Load(object sender, EventArgs e)
        {
            LoadCombos();
            LoadGrid();
        }

        private void LoadCombos()
        {
            // Load Lớp
            cboClass.DataSource = bll.GetAllClasses(); // Hàm này bạn đã có sẵn
            cboClass.DisplayMember = "name_Class";
            cboClass.ValueMember = "id_Class";

            // Load Môn
            cboSubject.DataSource = bll.GetAllSubjects();
            cboSubject.DisplayMember = "subject_name";
            cboSubject.ValueMember = "id_subject";

            // Set default HocKy (nếu chưa set trong Designer)
            if (cboSemester.Items.Count == 0)
            {
                cboSemester.Items.Add("HocKy1");
                cboSemester.Items.Add("HocKy2");
                cboSemester.SelectedIndex = 0;
            }
        }

        private void LoadGrid()
        {
            dgvAssignments.DataSource = bll.GetTeachingAssignments(_teacherId);
            // Ẩn cột ID nếu cần
            if (dgvAssignments.Columns["id_assignment"] != null)
                dgvAssignments.Columns["id_assignment"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra dữ liệu đầu vào trước
                if (cboClass.SelectedValue == null || cboSubject.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Lớp và Môn học.");
                    return;
                }

                int classId = Convert.ToInt32(cboClass.SelectedValue);
                int subId = Convert.ToInt32(cboSubject.SelectedValue);
                string sem = cboSemester.Text;

                // 2. Gọi hàm (Lúc này nếu lỗi, nó sẽ nhảy xuống catch bên dưới)
                int result = bll.AddTeachingAssignment(classId, subId, _teacherId, sem);

                if (result == 1)
                {
                    MessageBox.Show("Đã thêm phân công!");
                    LoadGrid();
                }
                else if (result == -1)
                {
                    MessageBox.Show("Lớp này môn này học kỳ này ĐÃ CÓ người dạy rồi!");
                }
                else if (result == -2)
                {
                    MessageBox.Show("Giáo viên này đã được phân công dạy môn này ở lớp này rồi!");
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống (Kết quả trả về 0).");
                }
            }
            catch (Exception ex)
            {
                // 3. ĐÂY LÀ NƠI SẼ HIỆN LỖI THỰC SỰ
                MessageBox.Show("Chi tiết lỗi: " + ex.Message);
            }
        }

        // Sự kiện xóa (Bạn cần thêm cột Button vào Grid hoặc double click để xóa)
        private void dgvAssignments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (MessageBox.Show("Hủy phân công này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idAssign = Convert.ToInt32(dgvAssignments.Rows[e.RowIndex].Cells["id_assignment"].Value);
                    bll.DeleteTeachingAssignment(idAssign);
                    LoadGrid();
                }
            }
        }
    }
}