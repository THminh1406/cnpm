using Guna.UI2.WinForms;
using SchoolManager.BLL;
using SchoolManager.DTO; // Dùng DTO
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

// Đảm bảo namespace này đúng với vị trí file của bạn
namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Manual_Roll_Call : UserControl
    {
        // Khởi tạo BLL
        private Business_Logic_Classes bll_Classes;
        private Business_Logic_Students bll_Students;
        private Business_Logic_Manual_Roll_Call bll_Attendance;

        private const string present = "present";
        private const string late = "late";
        private const string absent_P = "absent_permitted";
        private const string absent_UP = "absent_unpermitted";

        private bool isUpdatingCheckboxes = false;



        public uc_Manual_Roll_Call()
        {
            InitializeComponent();
        }

        private void uc_Manual_Roll_Call_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return; // Dừng lại, không chạy code CSDL
            }

            bll_Classes = new Business_Logic_Classes();
            bll_Students = new Business_Logic_Students();
            bll_Attendance = new Business_Logic_Manual_Roll_Call();

            cbo_Select_Class.DataSource = bll_Classes.GetAllClasses();
            cbo_Select_Class.DisplayMember = "name_Class";
            cbo_Select_Class.ValueMember = "id_Class";

            this.cbo_Select_Class.SelectedIndexChanged += new System.EventHandler(this.cbo_Select_Class_SelectedIndexChanged);

            this.list_Of_Student.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.List_Of_Student_CellContentClick);

            this.dtp_Select_Date.ValueChanged += new System.EventHandler(this.Control_ValueChanged);

            //load_List_Of_Student();
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            // Khi chọn Lớp hoặc Ngày, tải lại danh sách
            load_List_Of_Student();
        }

        private void load_List_Of_Student()
        {
            if (cbo_Select_Class.SelectedValue == null || !(cbo_Select_Class.SelectedValue is int))
            {
                list_Of_Student.DataSource = null;
                UpdateAllManualAttendanceLabels();
                return;
            }

            int id_Class = (int)cbo_Select_Class.SelectedValue;
            DateTime date = dtp_Select_Date.Value.Date;
            List<Students> studentsList = bll_Students.GetStudentsByClassId(id_Class);
            List<Roll_Call_Records> ListRollCallRecords = bll_Attendance.GetRollCallRecords(id_Class, date);
            Dictionary<int, Roll_Call_Records> map = ListRollCallRecords.ToDictionary(r => r.id_Student, r => r);
            list_Of_Student.AutoGenerateColumns = false;
            list_Of_Student.DataSource = studentsList;

            foreach (DataGridViewRow row in list_Of_Student.Rows)
            {
                int id_Student = (int)row.Cells["STT"].Value;
                if (map.ContainsKey(id_Student))
                {
                    Roll_Call_Records record = map[id_Student];
                    row.Cells[COL_PRESENT.Name].Value = (record.status == present);
                    row.Cells[COL_LATE.Name].Value = (record.status == late);
                    row.Cells[COL_ABSENT_P.Name].Value = (record.status == absent_P);
                    row.Cells[COL_ABSENT_UP.Name].Value = (record.status == absent_UP);
                    row.Cells["notes"].Value = record.notes;
                }
            }
            UpdateAllManualAttendanceLabels();
        }

        private void UpdateAllManualAttendanceLabels()
        {
            // === 1. LẤY TỔNG SỐ HỌC SINH (Từ BLL) ===
            int totalStudents = 0;
            if (cbo_Select_Class.SelectedValue != null && cbo_Select_Class.SelectedValue is int)
            {
                int idLop = (int)cbo_Select_Class.SelectedValue;
                try
                {
                    var allStudents = bll_Students.GetStudentsByClassId(idLop);
                    totalStudents = (allStudents != null) ? allStudents.Count : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi lấy tổng số HS: " + ex.Message);
                }
            }

            // === 2. ĐẾM TỪ DATAGRIDVIEW (Với luật mới: Phép = Vắng) ===
            int presentCount = 0;
            int lateCount = 0;
            int absentCount = 0; // Sẽ bao gồm cả Vắng Phép và Không Phép

            foreach (DataGridViewRow row in list_Of_Student.Rows)
            {
                // Logic đếm này phải khớp với logic của btn_Save
                if (Convert.ToBoolean(row.Cells[COL_LATE.Name].Value) == true)
                {
                    lateCount++;
                }
                // *** QUY TẮC MỚI: Cả hai loại vắng đều cộng vào absentCount ***
                else if (Convert.ToBoolean(row.Cells[COL_ABSENT_P.Name].Value) == true)
                {
                    absentCount++; // Vắng có phép
                }
                else if (Convert.ToBoolean(row.Cells[COL_ABSENT_UP.Name].Value) == true)
                {
                    absentCount++; // Vắng không phép
                }
                else // Mặc định (check 'present' hoặc không check gì)
                {
                    presentCount++;
                }
            }

            // === 3. CẬP NHẬT 4 LABEL ===
            // (Hãy sửa tên label cho đúng với file thiết kế của bạn)
            lbl_TotalStudents.Text = totalStudents.ToString();
            lbl_Present.Text = presentCount.ToString();
            lbl_Late.Text = lateCount.ToString();
            lbl_Absent.Text = absentCount.ToString();
        }

        private void List_Of_Student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //xử lý vẽ
            if (list_Of_Student.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                bool current = Convert.ToBoolean(list_Of_Student.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? false);
                list_Of_Student.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !current;
            }


            if (isUpdatingCheckboxes || e.RowIndex < 0) return;
            string colName = list_Of_Student.Columns[e.ColumnIndex].Name;
            List<string> checkCols = new List<string> { COL_PRESENT.Name, COL_LATE.Name, COL_ABSENT_P.Name, COL_ABSENT_UP.Name };
            if (checkCols.Contains(colName))
            {
                isUpdatingCheckboxes = true;

                foreach(string name in checkCols)
                {
                    if (name != colName)
                    {
                        list_Of_Student.Rows[e.RowIndex].Cells[name].Value = false;
                    }
                }
                list_Of_Student.Rows[e.RowIndex].Cells[colName].Value = true;
                list_Of_Student.EndEdit();
                isUpdatingCheckboxes = false;

                UpdateAllManualAttendanceLabels();
            }

        }

        private void SetAllStatus(String statusCheckbox)
        {
            isUpdatingCheckboxes = true;
            foreach (DataGridViewRow row in list_Of_Student.Rows)
            {
                row.Cells[COL_PRESENT.Name].Value = (statusCheckbox == present);
                row.Cells[COL_LATE.Name].Value = (statusCheckbox == late);
                row.Cells[COL_ABSENT_P.Name].Value = (statusCheckbox == absent_P);
                row.Cells[COL_ABSENT_UP.Name].Value = (statusCheckbox == absent_UP);
            }
            isUpdatingCheckboxes = false;
            UpdateAllManualAttendanceLabels();
        }

        private void cbo_Select_Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_List_Of_Student();
        }


        private void btn_Save_Click_Click(object sender, EventArgs e)
        {
            if(list_Of_Student.Rows.Count == 0)
            {
                return;
            }
            DateTime currentDate = dtp_Select_Date.Value.Date;
            List<Roll_Call_Records> rollCallRecords = new List<Roll_Call_Records>();

            foreach(DataGridViewRow row in list_Of_Student.Rows)
            {
                if (row.Cells["student_Code"].Value == null) continue;
                String status = present; // Mặc định là có mặt

                if (Convert.ToBoolean(row.Cells[COL_LATE.Name].Value) == true){
                    status = late;
                }
                else if (Convert.ToBoolean(row.Cells[COL_ABSENT_P.Name].Value) == true){
                    status = absent_P;
                }
                else if (Convert.ToBoolean(row.Cells[COL_ABSENT_UP.Name].Value) == true){
                    status = absent_UP;
                }
                rollCallRecords.Add(new Roll_Call_Records
                {
                    id_Student = (int)row.Cells["STT"].Value,
                    id_Class = (int)cbo_Select_Class.SelectedValue,
                    date = currentDate,
                    status = status,
                    notes = row.Cells["notes"].Value?.ToString() ?? string.Empty
                });
            }

            if(bll_Attendance.SaveRollCallRecord(rollCallRecords))
            {
                MessageBox.Show("Attendance records saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save attendance records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_All_Present_Click(object sender, EventArgs e)
        {
            SetAllStatus(present);
        }

        private void btn_All_Absent_Click(object sender, EventArgs e)
        {
            SetAllStatus(absent_UP);
        }

        private void list_Of_Student_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && list_Of_Student.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                // Vẽ lại nền của ô
                e.PaintBackground(e.CellBounds, true);

                // Lấy giá trị checkbox hiện tại
                bool isChecked = false;
                if (e.Value != null && e.Value != DBNull.Value)
                    isChecked = (bool)e.Value;

                // Kích thước checkbox muốn hiển thị
                int size = 35; // 👈 chỉnh to nhỏ tại đây
                int x = e.CellBounds.Left + (e.CellBounds.Width - size) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - size) / 2;

                // Vẽ checkbox
                ControlPaint.DrawCheckBox(e.Graphics, new Rectangle(x, y, size, size),
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                e.Handled = true;
            }
        }
    }
}