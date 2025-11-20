using AForge.Video;
using AForge.Video.DirectShow;
using SchoolManager.BLL; // Bổ sung BLL
using SchoolManager.DTO; // Bổ sung DTO
using System;
using System.Collections.Generic; // Bổ sung
using System.Drawing;
using System.Windows.Forms;
using ZXing; // Bổ sung ZXing

using QRCoder; // Thư viện tạo QR
using iTextSharp.text; // Thư viện PDF
using iTextSharp.text.pdf; // Thư viện PDF
using System.IO; // Cần cho FileStream, MemoryStream
using System.Drawing.Imaging; // Cần cho ImageFormat

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class QR_Roll_Call : UserControl
    {
        // --- BỔ SUNG BLL VÀ ZXING ---
        private Business_Logic_Classes bll_Classes;
        private Business_Logic_QR_Roll_Call bll_QR_Attendance;
        private Business_Logic_Students bll_Students;
        private BarcodeReader zxingReader; // Đầu đọc QR

        // Biến camera (Giữ nguyên của bạn)
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private bool isCameraRunning = false;

        public QR_Roll_Call()
        {
            InitializeComponent();
            zxingReader = new BarcodeReader(); // Khởi tạo đầu đọc QR

            // SỬA: Dùng 'VisibleChanged' thay 'Leave' (an toàn hơn)
            this.VisibleChanged += new System.EventHandler(this.QR_Roll_Call_VisibleChanged);
        }

        // --- HÀM LOAD (ĐÃ SỬA) ---
        // (Chỉ tải BLL, không tìm camera ở đây)

        // Nút Bật/Tắt Camera (Giữ logic của bạn + Thêm Timer)
        private void btn_Turn_On_Camera_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isCameraRunning)
                {
                    // --- GIỮ LOGIC CỦA BẠN ---
                    // (Tìm camera khi nhấn nút)
                    if (videoDevices == null)
                    {
                        videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                    }

                    if (videoDevices.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thiết bị camera!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // --- GIỮ LOGIC CỦA BẠN ---
                    // (Dùng camera [1])
                    videoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                    videoSource.Start();

                    isCameraRunning = true;
                    btn_Turn_On_Camera.Text = "Tắt Camera";
                    // --- KẾT THÚC LOGIC CỦA BẠN ---

                    // --- BỔ SUNG: BẬT TIMER QUÉT QR ---
                    QR_Timer.Start();
                    real_Time_Roll_Call.Text = "Đang quét...";
                }
                else
                {
                    StopCamera();
                    btn_Turn_On_Camera.Text = "Bật Camera";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở camera: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StopCamera();
            }
        }

        // --- SỬA LỖI: XỬ LÝ ẢNH (THREAD-SAFE) ---
        // (Code này đảm bảo không bị crash ngẫu nhiên)
        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if (eventArgs.Frame == null)
                    return; // Không có frame

                // Clone một lần duy nhất
                Bitmap frameCopy = (Bitmap)eventArgs.Frame.Clone();

                // Cập nhật hình lên PictureBox an toàn đa luồng
                if (picture_Cam.InvokeRequired)
                {
                    picture_Cam.BeginInvoke(new Action(() =>
                    {
                        // Giải phóng ảnh cũ
                        if (picture_Cam.Image != null)
                        {
                            picture_Cam.Image.Dispose();
                            picture_Cam.Image = null;
                        }

                        picture_Cam.Image = frameCopy;
                    }));
                }
                else
                {
                    if (picture_Cam.Image != null)
                    {
                        picture_Cam.Image.Dispose();
                        picture_Cam.Image = null;
                    }

                    picture_Cam.Image = frameCopy;
                }
            }
            catch (ObjectDisposedException)
            {
                // Form bị đóng hoặc camera ngắt kết nối
            }
            catch (ArgumentException)
            {
                // Frame không hợp lệ, bỏ qua
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi Video_NewFrame: " + ex.Message);
            }
        }

        // --- BỔ SUNG: HÀM QUÉT QR (SỰ KIỆN 'TICK' CỦA QR_TIMER) ---
        private void QR_Timer_Tick(object sender, EventArgs e)
        {
            if (picture_Cam.Image == null) return; // Chưa có hình

            try
            {
                Bitmap bitmap = new Bitmap(picture_Cam.Image);
                Result result = zxingReader.Decode(bitmap);
                bitmap.Dispose();

                if (result != null && !string.IsNullOrEmpty(result.Text))
                {
                    string studentCode = result.Text;

                    if (real_Time_Roll_Call.Text.Contains(studentCode))
                    {
                        return; // Đã quét học sinh này rồi
                    }

                    int idLop = (int)cbo_Select_Class_QR.SelectedValue;
                    DateTime ngay = dtp_QR_Date.Value.Date;

                    // Gọi BLL điểm danh (Hàm LƯU)
                    string message = bll_QR_Attendance.MarkStudentPresent(studentCode, idLop, ngay);

                    // Hiển thị kết quả (an toàn đa luồng)
                    this.BeginInvoke(new Action(() => {
                        real_Time_Roll_Call.Text = message.StartsWith("LỖI:")
                            ? message
                            : $"Đã điểm danh: {message} ({studentCode})";

                        if (!message.StartsWith("LỖI:"))
                        {
                            RefreshAttendanceList(); // Tải lại bảng nếu thành công
                        }
                    }));
                }
            }
            catch (Exception)
            {
                // Bỏ qua lỗi
            }
        }

        // --- BỔ SUNG: HÀM TẢI LẠI BẢNG ---
        private void RefreshAttendanceList()
        {
            // 1. KIỂM TRA ĐẦU VÀO (Giống hàm cũ)
            if (cbo_Select_Class_QR.SelectedValue == null || !(cbo_Select_Class_QR.SelectedValue is int))
            {
                list_Of_Student.DataSource = null;

                // --- BỔ SUNG: Reset label về 0 ---
                lbl_Present.Text = "0"; // <-- Sửa tên label
                lbl_TotalStudents.Text = "0";       // <-- Sửa tên label
                lbl_Absent.Text = "0";        // <-- Sửa tên label
                return;
            }

            int idLop = (int)cbo_Select_Class_QR.SelectedValue;
            DateTime ngay = dtp_QR_Date.Value.Date;

            // --- BỔ SUNG: LẤY TỔNG SỐ HỌC SINH ---
            int totalStudents = 0;
            try
            {
                // Dùng BLL Students đã có của bạn
                var allStudents = bll_Students.GetStudentsByClassId(idLop);
                totalStudents = (allStudents != null) ? allStudents.Count : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy tổng số HS: " + ex.Message);
                totalStudents = 0;
            }

            // 2. LẤY DANH SÁCH ĐÃ ĐIỂM DANH (Giống hàm cũ)
            list_Of_Student.AutoGenerateColumns = false;

            // Lấy danh sách có mặt 1 lần
            var presentList = bll_QR_Attendance.GetTodaysAttendance(idLop, ngay);
            list_Of_Student.DataSource = presentList;

            // 3. --- BỔ SUNG: TÍNH TOÁN SỐ LIỆU ---
            int presentCount = (presentList != null) ? presentList.Count : 0;
            int absentCount = totalStudents - presentCount;

            // 4. --- BỔ SUNG: CẬP NHẬT 3 LABEL ---
            lbl_TotalStudents.Text = totalStudents.ToString(); // <-- Sửa tên label
            lbl_Present.Text = presentCount.ToString();       // <-- Sửa tên label
            lbl_Absent.Text = absentCount.ToString();        // <-- Sửa tên label
        }

        // Hàm StopCamera (Giữ của bạn) + Bổ sung
        private void StopCamera()
        {
            try
            {
                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                    // --- BỔ SUNG: HỦY ĐĂNG KÝ SỰ KIỆN ---
                    videoSource.NewFrame -= new NewFrameEventHandler(Video_NewFrame);
                    videoSource = null;
                    picture_Cam.Image?.Dispose();
                    picture_Cam.Image = null;
                }
                isCameraRunning = false;
                QR_Timer.Stop(); // Dừng Timer
                real_Time_Roll_Call.Text = "Sẵn sàng"; // Cập nhật trạng thái
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tắt camera: " + ex.Message);
            }
        }

        // --- SỬA: DÙNG 'VISIBLECHANGED' THAY 'LEAVE' ---
        private void QR_Roll_Call_VisibleChanged(object sender, EventArgs e)
        {
            // Khi UserControl bị ẩn đi (ví dụ: đổi tab)
            if (!this.Visible)
            {
                StopCamera();
            }
        }

        private void QR_Roll_Call_Load_1(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return; // Dừng lại, không chạy code CSDL
            }
            bll_Classes = new Business_Logic_Classes();
            bll_QR_Attendance = new Business_Logic_QR_Roll_Call();
            bll_Students = new Business_Logic_Students();
            // 1. Tải danh sách lớp
            cbo_Select_Class_QR.DataSource = bll_Classes.GetAllClasses();
            cbo_Select_Class_QR.DisplayMember = "name_Class";
            cbo_Select_Class_QR.ValueMember = "id_Class";

            // 2. Tải danh sách điểm danh (ban đầu rỗng)
            RefreshAttendanceList();

            // --- BỔ SUNG: KẾT NỐI SỰ KIỆN TẢI LẠI ---
            // (Khi đổi lớp hoặc đổi ngày, gọi hàm 'Selection_Changed')
            this.cbo_Select_Class_QR.SelectedIndexChanged += new System.EventHandler(this.Selection_Changed);
            this.dtp_QR_Date.ValueChanged += new System.EventHandler(this.Selection_Changed);
        }

        private void Selection_Changed(object sender, EventArgs e)
        {
            // Khi chọn lớp hoặc ngày, tải lại bảng
            RefreshAttendanceList();
        }

        private void btn_Export_PDF_Click(object sender, EventArgs e)
        {
            // 1. Lấy lớp và danh sách học sinh (từ các control đã có)
            if (cbo_Select_Class_QR.SelectedValue == null || !(cbo_Select_Class_QR.SelectedValue is int))
            {
                MessageBox.Show("Vui lòng chọn một lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idLop = (int)cbo_Select_Class_QR.SelectedValue;
            string tenLop = cbo_Select_Class_QR.Text;

            // Dùng BLL đã có để lấy danh sách
            List<Students> dsHocSinh = bll_Students.GetStudentsByClassId(idLop);

            if (dsHocSinh.Count == 0)
            {
                MessageBox.Show("Lớp này không có học sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Hỏi người dùng muốn lưu tệp ở đâu
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF file|*.pdf";
            saveDialog.Title = "Lưu danh sách mã QR";
            saveDialog.FileName = $"Danh_sach_QR_Lop_{tenLop}.pdf"; // Tên tệp mặc định

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveDialog.FileName;
                try
                {
                    // 3. Bắt đầu tạo tệp PDF
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30); // Lề
                    PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                    document.Open();

                    // Tiêu đề
                    // (Bạn cần thêm font hỗ trợ tiếng Việt nếu muốn)
                    // Font font = ... (Tạm thời dùng font mặc định)
                    document.Add(new Paragraph($"DANH SACH MA QR - LOP: {tenLop}"));
                    document.Add(new Paragraph("--------------------------------------------------"));

                    // 4. Khởi tạo bộ tạo QR (chỉ 1 lần)
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();

                    // 5. Lặp qua từng học sinh để thêm vào PDF
                    foreach (Students hocSinh in dsHocSinh)
                    {
                        // Thêm Tên và Mã HS (student_code)
                        document.Add(new Paragraph($"Ten: {hocSinh.name_Student}"));
                        document.Add(new Paragraph($"Ma HS: {hocSinh.code_Student}"));

                        // Tạo ảnh QR
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(hocSinh.code_Student, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);

                        // Chuyển ảnh QR từ System.Drawing.Bitmap sang iTextSharp.text.Image
                        using (Bitmap qrBitmap = qrCode.GetGraphic(5)) // Kích thước (pixel-per-module)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                qrBitmap.Save(ms, ImageFormat.Png);
                                byte[] bitmapBytes = ms.ToArray();
                                iTextSharp.text.Image qrImage = iTextSharp.text.Image.GetInstance(bitmapBytes);

                                qrImage.ScalePercent(75f); // Thu nhỏ ảnh QR lại 75%
                                document.Add(qrImage); // Thêm ảnh vào PDF
                            }
                        }

                        document.Add(new Paragraph("--------------------")); // Dòng kẻ ngăn cách
                    }

                    // 6. Đóng tệp
                    document.Close();
                    MessageBox.Show("Xuất tệp PDF thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void QR_Timer_Tick_1(object sender, EventArgs e)
        {
            QR_Timer.Tick += QR_Timer_Tick;
            QR_Timer.Interval = 5000; //
        }

        private void btn_Save_QR_Roll_Call_Click(object sender, EventArgs e)
        {
            StopCamera();
            btn_Turn_On_Camera.Text = "Bật Camera"; // Reset lại nút chính
            picture_Cam.Image = null; // Xóa hình camera
            MessageBox.Show("Đã hoàn tất và lưu điểm danh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_Reset_QR_Roll_Call_Click(object sender, EventArgs e)
        {
            if (cbo_Select_Class_QR.SelectedValue == null || !(cbo_Select_Class_QR.SelectedValue is int))
            {
                MessageBox.Show("Vui lòng chọn lớp cần làm lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idLop = (int)cbo_Select_Class_QR.SelectedValue;
            DateTime ngay = dtp_QR_Date.Value.Date;
            string tenLop = cbo_Select_Class_QR.Text;

            // 2. HIỂN THỊ CẢNH BÁO (Rất quan trọng)
            string canhBao = $"Bạn có chắc chắn muốn XÓA TOÀN BỘ dữ liệu điểm danh \ncủa lớp {tenLop}\ntrong ngày {ngay.ToShortDateString()} không?\n\nHành động này không thể hoàn tác.";

            DialogResult confirm = MessageBox.Show(canhBao, "Xác nhận Xóa",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            // 3. Nếu người dùng chọn "Yes"
            if (confirm == DialogResult.Yes)
            {
                // 4. Gọi BLL để xóa
                bool success = bll_QR_Attendance.ResetAttendance(idLop, ngay);

                if (success)
                {
                    MessageBox.Show("Đã xóa dữ liệu cũ thành công. \nBạn có thể bắt đầu điểm danh lại.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Tải lại bảng (bây giờ sẽ bị trống)
                    RefreshAttendanceList();

                    // 6. Xóa dòng trạng thái
                    real_Time_Roll_Call.Text = "Sẵn sàng (Đã reset)";
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Change_Manual_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}