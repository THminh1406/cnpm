using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManager.Presentations
{
    public static class ThemeHelper
    {
        public static bool IsDarkMode { get; set; } = false;

        // --- CẤU HÌNH MÀU DARK MODE ---
        private static Color DarkBackground = Color.FromArgb(32, 33, 36); // Nền chính
        private static Color DarkSurface = Color.FromArgb(80, 80, 80);    // Nền Panel/Input
        private static Color DarkText = Color.FromArgb(230, 230, 230);
        private static Color DarkBorder = Color.FromArgb(100, 100, 100);

        // --- CẤU HÌNH MÀU LIGHT MODE ---
        private static Color LightBackground = Color.FromArgb(242, 245, 250); // Xanh nhạt
        private static Color LightSurface = Color.White; // Trắng
        private static Color LightText = Color.Black;
        private static Color LightBorder = Color.LightGray;

        public static void ApplyTheme(Control container)
        {
            Color backColor = IsDarkMode ? DarkBackground : LightBackground;
            Color surfaceColor = IsDarkMode ? DarkSurface : LightSurface;
            Color textColor = IsDarkMode ? DarkText : LightText;
            Color borderColor = IsDarkMode ? DarkBorder : LightBorder;

            // Đổi màu nền container chính (Form/UC lớn)
            if (container.Tag == null || container.Tag.ToString() != "NoTheme")
            {
                container.BackColor = backColor;
            }

            foreach (Control c in container.Controls)
            {
                UpdateControl(c, backColor, surfaceColor, textColor, borderColor);
            }
        }

        private static void UpdateControl(Control c, Color bg, Color surface, Color text, Color border)
        {
            // 1. NẾU GẶP TAG "NoTheme" -> BỎ QUA NGAY LẬP TỨC
            if (c.Tag != null && c.Tag.ToString() == "NoTheme") return;

            // --- 2. XỬ LÝ USERCONTROL (Trang con) ---
            // UserControl con phải ăn theo màu nền chính (Background) để hòa nhập vào Form cha
            if (c is UserControl)
            {
                c.BackColor = bg;
            }

            // --- 3. XỬ LÝ PANEL (Khối nội dung) ---
            else if (c is Guna2Panel || c is Panel)
            {
                // Kiểm tra Tag Transparent (Nếu muốn trong suốt thì bỏ qua việc tô màu)

                    // Nếu không có tag Transparent -> Tô màu nổi (Surface)
                    if (IsDarkMode)
                    {
                        // Dark Mode: Panel màu xám sáng (80,80,80) để nổi trên nền đen (32,33,36)
                        c.BackColor = Color.Transparent;
                        if (c is Guna2Panel p) p.FillColor = surface;
                    }
                    else
                    {
                        // Light Mode: Panel màu Trắng (White) để nổi trên nền xanh nhạt (242,245,250)
                        c.BackColor = Color.Transparent;
                        if (c is Guna2Panel p) p.FillColor = Color.White;
                    }
            }

            // --- 4. XỬ LÝ GROUPBOX ---
            else if (c is GroupBox || c is Guna2GroupBox)
            {
                c.BackColor = bg;
                c.ForeColor = text;
                if (c is Guna2GroupBox gb)
                {
                    gb.FillColor = bg;
                    gb.CustomBorderColor = IsDarkMode ? surface : Color.WhiteSmoke;
                    gb.ForeColor = text;
                }
            }

            // --- 5. XỬ LÝ TEXTBOX (Input) ---
            else if (c is Guna2TextBox txt)
            {
                // Dark Mode: Màu surface (Xám sáng)
                // Light Mode: Màu Trắng (White) - Luôn là trắng dù panel màu gì
                txt.FillColor = IsDarkMode ? surface : Color.White;

                txt.ForeColor = text;
                txt.BorderColor = border;
                txt.PlaceholderForeColor = IsDarkMode ? Color.Silver : Color.Gray;

                txt.DisabledState.FillColor = IsDarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(226, 226, 226);
                txt.DisabledState.ForeColor = IsDarkMode ? Color.Gray : Color.FromArgb(138, 138, 138);
            }

            // --- 6. XỬ LÝ COMBOBOX / DATEPICKER ---
            else if (c is Guna2ComboBox cbo)
            {
                cbo.FillColor = IsDarkMode ? surface : Color.White;
                cbo.ForeColor = text;
                cbo.BorderColor = border;
                cbo.ItemsAppearance.BackColor = IsDarkMode ? surface : Color.White;
                cbo.ItemsAppearance.ForeColor = text;
            }
            else if (c is Guna2DateTimePicker dtp)
            {
                dtp.FillColor = IsDarkMode ? surface : Color.White;
                dtp.ForeColor = text;
                dtp.BorderColor = border;
            }

            // --- 7. XỬ LÝ CHỮ (LABEL) ---
            else if (c is Label || c is Guna2HtmlLabel)
            {
                // Label nên trong suốt để ăn theo màu của Panel/UserControl chứa nó
                c.BackColor = Color.Transparent;
                c.ForeColor = text;
            }
            else if (c is Guna2CheckBox chk) chk.ForeColor = text;
            else if (c is Guna2RadioButton rad) rad.ForeColor = text;

            // --- 8. XỬ LÝ BUTTON ---
            else if (c is Guna2Button btn)
            {
                // Nếu là nút Action (Xanh/Đỏ - Màu đặc biệt) -> Giữ nguyên nền, chỉ đổi chữ
                if (btn.FillColor != Color.White && btn.FillColor != Color.FloralWhite && btn.FillColor != Color.Transparent && btn.FillColor != DarkSurface)
                {
                    btn.ForeColor = Color.White;
                }
                else
                {
                    // Nút Menu (Màu nhạt/Trong suốt)
                    if (IsDarkMode)
                    {
                        btn.FillColor = surface; // Nền xám cho nút menu
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.FillColor = Color.Transparent; // Trả về trong suốt
                        btn.ForeColor = Color.Black;
                    }
                }
            }

            // --- 9. XỬ LÝ BẢNG (GRID) ---
            else if (c is Guna2DataGridView dgv)
            {
                ApplyGridTheme(dgv, bg, surface, text);
            }

            // === QUAN TRỌNG: ĐỆ QUY QUÉT SÂU VÀO TRONG ===
            if (c.HasChildren)
            {
                foreach (Control child in c.Controls)
                {
                    // Gọi đệ quy để xử lý các thành phần con
                    UpdateControl(child, bg, surface, text, border);
                }
            }
        }

        private static void ApplyGridTheme(Guna2DataGridView dgv, Color bg, Color surface, Color text)
        {
            if (IsDarkMode)
            {
                // --- DARK MODE ---
                dgv.BackgroundColor = bg;
                dgv.GridColor = Color.FromArgb(100, 100, 100);

                // Header
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);

                // Row chính
                dgv.DefaultCellStyle.BackColor = surface;
                dgv.DefaultCellStyle.ForeColor = text;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 110, 120);
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;

                // Row xen kẽ
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(70, 70, 70);
                dgv.AlternatingRowsDefaultCellStyle.ForeColor = text;
                dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 110, 120);
                dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

                // Guna Override
                dgv.ThemeStyle.RowsStyle.BackColor = surface;
                dgv.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(70, 70, 70);
            }
            else
            {
                // --- LIGHT MODE ---
                // Bảng BlueTheme (Học sinh)
                if (dgv.Tag != null && dgv.Tag.ToString() == "BlueTheme")
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.GridColor = Color.White;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 245, 250);
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
                }
                // Bảng Thường
                else
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.GridColor = Color.LightGray;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }
    }
}