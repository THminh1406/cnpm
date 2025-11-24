using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Imaging;

namespace SchoolManager.Presentations
{
    public partial class uc_Drawing_Board : UserControl
    {

        // === 1. BIẾN TRẠNG THÁI ===

        // --- Logic Công cụ ---
        private enum DrawingTool { Pen, Eraser }
        private DrawingTool currentTool = DrawingTool.Pen;
        private Color currentColor = Color.Black;
        private int currentPenSize = 5;
        private bool isDrawing = false;
        private Point lastPoint = Point.Empty;

        // --- Logic Trang (Pages) ---
        // 'Dictionary' để lưu trữ các trang. 
        // Key (int) là ID của nút (Button) trang, Value (Bitmap) là hình ảnh của trang đó.
        private Dictionary<int, Bitmap> pages = new Dictionary<int, Bitmap>();
        private int currentPageButtonID = 0; // ID của nút trang đang được chọn
        private int pageButtonCounter = 1; // Số đếm để tạo ID/Tên nút

        // --- Logic Lịch sử (Undo/Redo) ---
        // Chúng ta cần một 'Stack' cho MỖI trang.
        // Key (int) là ID của nút trang, Value là Stack Lịch sử của trang đó.
        private Dictionary<int, Stack<Bitmap>> undoHistory = new Dictionary<int, Stack<Bitmap>>();
        private Dictionary<int, Stack<Bitmap>> redoHistory = new Dictionary<int, Stack<Bitmap>>();

        public uc_Drawing_Board()
        {
            InitializeComponent();

            // Tối ưu hóa việc vẽ, tránh bị răng cưa
            this.DoubleBuffered = true;
        }

        private void uc_Drawing_Board_Load(object sender, EventArgs e)
        {
            // --- SỬA LỖI TẠI ĐÂY ---

            // 1. Gán giá trị của thanh trượt BẰNG giá trị mặc định của bút
            // (Giả sử thanh trượt của bạn tên là 'trackBar')
            trackBar.Value = currentPenSize;

            // 2. Thêm trang đầu tiên (như cũ)
            AddNewPage(true);
        }

        private void panel_DrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = e.Location;

                // --- Chuẩn bị cho Undo ---
                // Lấy trang (Bitmap) hiện tại
                Bitmap currentPage = GetCurrentPage();
                // Lấy Stack 'Undo' của trang hiện tại
                Stack<Bitmap> currentUndoStack = GetCurrentUndoStack();

                // Lưu 1 bản sao (Clone) của trạng thái HIỆN TẠI vào Undo
                currentUndoStack.Push(new Bitmap(currentPage));

                // Khi bắt đầu vẽ 1 nét mới, xóa toàn bộ lịch sử Redo
                GetCurrentRedoStack().Clear();
            }
        }

        // (Thay thế hàm MouseMove cũ của bạn)

        private void panel_DrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || lastPoint.IsEmpty) return;

            Bitmap currentPageBitmap = GetCurrentPage();
            if (currentPageBitmap == null) return; // Thêm kiểm tra an toàn

            // Tạo đối tượng Graphics từ Bitmap (Đã đúng)
            using (Graphics g = Graphics.FromImage(currentPageBitmap))
            {
                // Xác định màu vẽ (Đã đúng)
                Color drawColor = (currentTool == DrawingTool.Eraser)
                                        ? panel_DrawingSurface.BackColor
                                        : currentColor;

                using (Pen pen = new Pen(drawColor, currentPenSize))
                {
                    // --- SỬA LỖI LOGIC TẠI ĐÂY ---
                    if (currentTool == DrawingTool.Pen)
                    {
                        // Nếu là Bút, dùng đầu tròn VÀ Làm mượt
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                    }
                    else if (currentTool == DrawingTool.Eraser)
                    {
                        // Nếu là Tẩy, dùng đầu vuông VÀ TẮT làm mượt
                        g.SmoothingMode = SmoothingMode.None; // <-- TẮT LÀM MƯỢT
                        pen.StartCap = LineCap.Square;
                        pen.EndCap = LineCap.Square;
                    }
                    // --- KẾT THÚC SỬA LỖI ---

                    // Vẽ đường thẳng (Đã đúng)
                    g.DrawLine(pen, lastPoint, e.Location);
                }
            }

            // Cập nhật điểm cuối (Đã đúng)
            lastPoint = e.Location;

            // Yêu cầu Panel 'vẽ lại' (Đã đúng)
            panel_DrawingSurface.Invalidate();
        }

        private void panel_DrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
                lastPoint = Point.Empty;
            }
        }

        private void panel_DrawingSurface_Paint(object sender, PaintEventArgs e)
        {
            // Sự kiện 'Paint' là nơi chúng ta hiển thị Bitmap lên Panel
            Bitmap currentPage = GetCurrentPage();
            if (currentPage != null)
            {
                // Vẽ Bitmap (đã được cập nhật ở MouseMove) lên Panel
                e.Graphics.DrawImage(currentPage, Point.Empty);
            }
        }

        // === 3. LOGIC CÔNG CỤ (TOOLS) ===

        private void btn_Tool_Click(object sender, EventArgs e)
        {
            // 'btn_Pen_Tool' là Bút
            if (sender == btn_Pen_Tool)
            {
                currentTool = DrawingTool.Pen;
                // Đặt lại con trỏ chuột về mặc định
                panel_DrawingSurface.Cursor = Cursors.Default;
            }
            // 'btn_Eraser_Tool' là Tẩy
            else if (sender == btn_Eraser_Tool)
            {
                currentTool = DrawingTool.Eraser;
                // Gọi hàm mới để tạo con trỏ chuột hình vuông
                UpdateEraserCursor();
            }
            // 'btn_Ruler_Tool' là Thước
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            // === ĐÃ SỬA: Dùng Guna2Button ===

            // Lấy cái nút đã được nhấn (sender) và đổi kiểu (cast)
            // sang Guna2Button (thay vì Guna2GradientButton)
            Guna.UI2.WinForms.Guna2Button clickedButton = sender as Guna.UI2.WinForms.Guna2Button;

            if (clickedButton == null) return;

            // Lấy giá trị Tag (đang là kiểu string, ví dụ: "Red")
            string colorTag = clickedButton.Tag?.ToString();

            if (string.IsNullOrEmpty(colorTag))
            {
                // Nếu bạn quên đặt Tag, báo lỗi
                MessageBox.Show("Nút này chưa được gán Tag màu!");
                return;
            }

            // Chuyển đổi chuỗi (string) trong Tag thành Màu (Color)
            try
            {
                // Ví dụ: Chuyển chuỗi "Red" thành đối tượng Color.Red
                currentColor = Color.FromName(colorTag);
            }
            catch
            {
                // Nếu bạn gõ sai Tag (ví dụ: "Redd"), nó sẽ dùng màu Đen
                currentColor = Color.Black;
            }

            // Tự động chuyển về Bút
            currentTool = DrawingTool.Pen;
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            currentPenSize = trackBar.Value;

            // BỔ SUNG: Nếu đang ở chế độ Tẩy, cập nhật con trỏ
            if (currentTool == DrawingTool.Eraser)
            {
                UpdateEraserCursor();
            }
        }

        private void UpdateEraserCursor()
        {
            try
            {
                int size = currentPenSize; // Lấy kích thước từ thanh trượt

                // Kích thước con trỏ chuột có giới hạn (thường là 32x32)
                // Nếu quá lớn, chúng ta chỉ hiển thị viền (an toàn hơn)
                if (size < 2) size = 2;
                if (size > 32) size = 32; // Giới hạn kích thước con trỏ

                // 1. Tạo một Bitmap (ảnh) nhỏ
                Bitmap cursorBitmap = new Bitmap(size, size);
                using (Graphics g = Graphics.FromImage(cursorBitmap))
                {
                    // 2. Vẽ một hình vuông (viền đen) lên ảnh
                    g.DrawRectangle(Pens.Black, 0, 0, size - 1, size - 1);
                }

                // 3. Tạo con trỏ chuột từ ảnh
                IntPtr hIcon = cursorBitmap.GetHicon();
                Cursor customCursor = new Cursor(hIcon);

                // 4. Đặt con trỏ cho bảng vẽ
                panel_DrawingSurface.Cursor = customCursor;

                // 5. Giải phóng tài nguyên
                cursorBitmap.Dispose();
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, quay về con trỏ mặc định
                panel_DrawingSurface.Cursor = Cursors.Default;
                Console.WriteLine("Lỗi tạo con trỏ: " + ex.Message);
            }
        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            Stack<Bitmap> currentUndoStack = GetCurrentUndoStack();
            Stack<Bitmap> currentRedoStack = GetCurrentRedoStack();
            Bitmap currentPage = GetCurrentPage();

            if (currentUndoStack.Count > 0)
            {
                // 1. Lưu trạng thái HIỆN TẠI vào Redo
                currentRedoStack.Push(new Bitmap(currentPage));

                // 2. Lấy trạng thái TRƯỚC ĐÓ từ Undo
                Bitmap previousState = currentUndoStack.Pop();

                // 3. Cập nhật trang (Bitmap) hiện tại (Hủy tham chiếu cũ, dùng cái mới)
                Bitmap oldPage = pages[currentPageButtonID];
                pages[currentPageButtonID] = previousState;
                oldPage.Dispose(); // Giải phóng bộ nhớ của ảnh cũ

                // 4. Vẽ lại
                panel_DrawingSurface.Invalidate();
            }
        }

        private void btn_Redo_Click(object sender, EventArgs e)
        {
            Stack<Bitmap> currentUndoStack = GetCurrentUndoStack();
            Stack<Bitmap> currentRedoStack = GetCurrentRedoStack();
            Bitmap currentPage = GetCurrentPage();

            if (currentRedoStack.Count > 0)
            {
                // 1. Lưu trạng thái HIỆN TẠI vào Undo
                currentUndoStack.Push(new Bitmap(currentPage));

                // 2. Lấy trạng thái TIẾP THEO từ Redo
                Bitmap nextState = currentRedoStack.Pop();

                // 3. Cập nhật trang
                Bitmap oldPage = pages[currentPageButtonID];
                pages[currentPageButtonID] = nextState;
                oldPage.Dispose();

                // 4. Vẽ lại
                panel_DrawingSurface.Invalidate();
            }
        }

        // (XÓA HÀM CŨ VÀ THAY BẰNG HÀM NÀY)

        private void btn_ClearPage_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa trang này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (pages.Count == 1)
            {
                // --- LOGIC 1: LÀ TRANG CUỐI CÙNG -> CHỈ LÀM TRẮNG ---
                GetCurrentUndoStack().Push(new Bitmap(GetCurrentPage()));
                GetCurrentRedoStack().Clear();
                using (Graphics g = Graphics.FromImage(GetCurrentPage()))
                {
                    g.Clear(panel_DrawingSurface.BackColor);
                }
                panel_DrawingSurface.Invalidate();
            }
            else
            {
                // --- LOGIC 2: CÓ NHIỀU TRANG -> XÓA HẲN ---

                int pageIDToDelete = currentPageButtonID;

                // 2a. Tìm Panel nút trang (Guna2GradientPanel) cần xóa
                Guna.UI2.WinForms.Guna2GradientPanel panelToDelete =
                    guna2Panel7.Controls.OfType<Guna.UI2.WinForms.Guna2GradientPanel>()
                               .FirstOrDefault(p => (int)p.Tag == pageIDToDelete);

                // 2b. Tính toán trang (index) sẽ chuyển đến
                List<int> pageIDs = new List<int>(pages.Keys);
                pageIDs.Sort();
                int currentIndex = pageIDs.IndexOf(pageIDToDelete);

                // Luôn chuyển về trang có index 0 (nếu xóa trang đầu)
                // hoặc chuyển về trang phía trước (nếu xóa trang sau)
                int newIndexToSwitchTo = (currentIndex > 0) ? (currentIndex - 1) : 0;

                // 2c. Xóa dữ liệu (Bitmap, History,...)
                if (pages.ContainsKey(pageIDToDelete))
                {
                    pages[pageIDToDelete].Dispose(); // Giải phóng Bitmap
                    pages.Remove(pageIDToDelete);
                    undoHistory.Remove(pageIDToDelete);
                    redoHistory.Remove(pageIDToDelete);
                }

                // 2d. Xóa Nút (Control) khỏi giao diện
                if (panelToDelete != null)
                {
                    guna2Panel7.Controls.Remove(panelToDelete);
                    panelToDelete.Dispose();
                }

                // 2e. Sắp xếp lại (Re-order và Re-number) tất cả các nút
                // Đây là hàm quan trọng sẽ sửa lỗi "Trang 4"
                ReorderPageButtons();

                // 2f. Chuyển sang trang mới dựa trên INDEX đã tính
                // (Vì ID đã bị đổi, chúng ta phải tìm ID mới tại Index đó)
                List<int> finalPageIDs = new List<int>(pages.Keys);
                finalPageIDs.Sort();
                int finalIDToSwitchTo = finalPageIDs[newIndexToSwitchTo];

                SwitchToPage(finalIDToSwitchTo);
            }
        }

        // --- HÀM MỚI: SẮP XẾP LẠI VỊ TRÍ, TÊN VÀ ID CÁC NÚT TRANG ---

        private void ReorderPageButtons()
        {
            // Lấy tất cả các Panel trang còn lại
            var panels = guna2Panel7.Controls.OfType<Guna.UI2.WinForms.Guna2GradientPanel>()
                                   .OrderBy(p => (int)p.Tag) // Sắp xếp theo ID (Tag) cũ
                                   .ToList();

            // Tạo các Dictionary (bộ nhớ) tạm thời
            Dictionary<int, Bitmap> newPages = new Dictionary<int, Bitmap>();
            Dictionary<int, Stack<Bitmap>> newUndoHistory = new Dictionary<int, Stack<Bitmap>>();
            Dictionary<int, Stack<Bitmap>> newRedoHistory = new Dictionary<int, Stack<Bitmap>>();

            int newPageIndex = 1; // Bộ đếm ID mới (bắt đầu từ 1)
            int verticalSpacing = 10;
            int controlHeight = 96;
            int newX = 43; // Vị trí X (đã sửa)

            foreach (var panel in panels)
            {
                int oldID = (int)panel.Tag;
                int newID = newPageIndex; // ID mới (ví dụ: Trang 3 cũ giờ là Trang 2)

                // 1. Cập nhật Dữ liệu: Chuyển dữ liệu từ ID cũ sang ID mới
                if (pages.ContainsKey(oldID)) newPages[newID] = pages[oldID];
                if (undoHistory.ContainsKey(oldID)) newUndoHistory[newID] = undoHistory[oldID];
                if (redoHistory.ContainsKey(oldID)) newRedoHistory[newID] = redoHistory[oldID];

                // 2. Cập nhật Control (Giao diện)

                // 2a. Cập nhật Panel (vỏ ngoài)
                panel.Tag = newID; // Gán Tag ID mới
                int newY = (controlHeight + verticalSpacing) * (newPageIndex - 1) + verticalSpacing; // (newPageIndex - 1) vì index 0-based
                panel.Location = new Point(newX, newY);
                panel.TabIndex = 19 + newPageIndex;

                // 2b. Cập nhật Label (Tên)
                Guna.UI2.WinForms.Guna2HtmlLabel lbl = panel.Controls.OfType<Guna.UI2.WinForms.Guna2HtmlLabel>().FirstOrDefault();
                if (lbl != null)
                {
                    lbl.Text = $"Trang {newID}"; // <-- Sửa lỗi hiển thị
                    lbl.Tag = newID;
                }

                // 2c. Cập nhật Nút bấm (ẩn)
                Guna.UI2.WinForms.Guna2GradientButton btn = panel.Controls.OfType<Guna.UI2.WinForms.Guna2GradientButton>().FirstOrDefault();
                if (btn != null)
                {
                    btn.Tag = newID;
                }

                newPageIndex++; // Tăng bộ đếm cho trang tiếp theo
            }

            // 3. Thay thế Dữ liệu (Dictionaries) cũ bằng Dữ liệu tạm
            // (Phải dọn dẹp Bitmap cũ để tránh rò rỉ bộ nhớ, nhưng
            // vì chúng ta chỉ chuyển tham chiếu (reference), nên không cần Dispose)
            pages = newPages;
            undoHistory = newUndoHistory;
            redoHistory = newRedoHistory;

            // 4. SỬA LỖI QUAN TRỌNG NHẤT:
            // Đặt lại bộ đếm toàn cục
            pageButtonCounter = newPageIndex; // (Ví dụ: Nếu còn 2 trang, counter sẽ là 3)
        }

        private void btn_AddPage_Click(object sender, EventArgs e)
        {
            AddNewPage(false);
        }

        // Hàm tạo trang mới
        // (Xóa hàm AddNewPage cũ của bạn và thay bằng hàm này)

        private Guna.UI2.WinForms.Guna2GradientPanel CreatePageButton_UI(int buttonID)
        {
            // --- Tạo Label (Bên trong) ---
            Guna.UI2.WinForms.Guna2HtmlLabel pageLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pageLabel.BackColor = System.Drawing.Color.Transparent;
            pageLabel.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            pageLabel.ForeColor = System.Drawing.Color.Black;
            pageLabel.Location = new System.Drawing.Point(95, 28);
            pageLabel.Name = $"lbl_Page_{buttonID}";
            pageLabel.Size = new System.Drawing.Size(93, 39);
            pageLabel.TabIndex = 9;
            pageLabel.Text = $"Trang {buttonID}";
            pageLabel.Tag = buttonID;
            pageLabel.Click += PageControl_Click;

            // --- Tạo Nút Bấm (Bên trong, đè lên) ---
            Guna.UI2.WinForms.Guna2GradientButton pageClickButton = new Guna.UI2.WinForms.Guna2GradientButton();
            pageClickButton.BackColor = System.Drawing.Color.Transparent;
            pageClickButton.BorderColor = System.Drawing.Color.Transparent;
            pageClickButton.BorderRadius = 25;
            pageClickButton.FillColor = System.Drawing.Color.Transparent;
            pageClickButton.FillColor2 = System.Drawing.Color.Transparent;
            pageClickButton.FocusedColor = System.Drawing.Color.Transparent;
            pageClickButton.Font = new System.Drawing.Font("Segoe UI", 10.875F, System.Drawing.FontStyle.Bold);
            pageClickButton.ForeColor = System.Drawing.Color.Black;
            try
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Drawing_Board));
                pageClickButton.Image = ((System.Drawing.Image)(resources.GetObject("guna2GradientButton11.Image")));
            }
            catch { /* Bỏ qua nếu lỗi ảnh */ }
            pageClickButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            pageClickButton.ImageOffset = new System.Drawing.Point(-10, 0);
            pageClickButton.ImageSize = new System.Drawing.Size(100, 100);
            pageClickButton.Location = new System.Drawing.Point(0, 0);
            pageClickButton.Name = $"btn_PageClick_{buttonID}";
            pageClickButton.Size = new System.Drawing.Size(206, 96);
            pageClickButton.TabIndex = 11;
            pageClickButton.Tag = buttonID; // QUAN TRỌNG: Gán ID vào Tag
            pageClickButton.Click += PageButton_Click; // Gắn sự kiện Click chính
            pageClickButton.UseTransparentBackground = true;

            // --- Tạo Panel (Vỏ bọc ngoài) ---
            Guna.UI2.WinForms.Guna2GradientPanel pagePanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            pagePanel.BackColor = System.Drawing.Color.Transparent;
            pagePanel.BorderColor = System.Drawing.Color.Black;
            pagePanel.BorderRadius = 25;
            pagePanel.BorderThickness = 1;
            pagePanel.Controls.Add(pageClickButton); // Thêm Nút vào Panel
            pagePanel.Controls.Add(pageLabel); // Thêm Label vào Panel
            pagePanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            pagePanel.FillColor2 = System.Drawing.Color.Silver;
            pagePanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            pagePanel.Name = $"panel_Page_{buttonID}";
            pagePanel.Size = new System.Drawing.Size(206, 96);
            pagePanel.Tag = buttonID; // Gán ID vào Tag của Panel

            // Trả về Nút Panel (View) đã hoàn thiện
            return pagePanel;
        }
        private void AddNewPage(bool isFirstPage)
        {
            // === PHẦN 1: XỬ LÝ DỮ LIỆU (LOGIC) ===

            // 1. Tạo Bitmap mới cho trang
            Bitmap newPageBitmap = new Bitmap(panel_DrawingSurface.Width, panel_DrawingSurface.Height);
            using (Graphics g = Graphics.FromImage(newPageBitmap))
            {
                g.Clear(panel_DrawingSurface.BackColor);
            }

            // 2. Tạo ID và lưu dữ liệu
            int buttonID = pageButtonCounter++; // Lấy ID từ bộ đếm
            pages.Add(buttonID, newPageBitmap);
            undoHistory.Add(buttonID, new Stack<Bitmap>());
            redoHistory.Add(buttonID, new Stack<Bitmap>());


            // === PHẦN 2: XỬ LÝ GIAO DIỆN (VIEW) ===

            // 3. Gọi hàm giao diện (đã tách) để tạo nút
            Guna.UI2.WinForms.Guna2GradientPanel pagePanel = CreatePageButton_UI(buttonID);

            // 4. Tính toán vị trí Y (Logic định vị)
            int verticalSpacing = 10;
            int newX = 43; // Căn lề phải (đã sửa)

            // Tìm control cuối cùng (theo VỊ TRÍ Y)
            Guna.UI2.WinForms.Guna2GradientPanel lastPagePanel = guna2Panel7.Controls
                .OfType<Guna.UI2.WinForms.Guna2GradientPanel>()
                .OrderByDescending(p => p.Location.Y)
                .FirstOrDefault();

            int newY;
            int newTabIndex;

            if (lastPagePanel == null)
            {
                // Đây là trang đầu tiên
                newY = verticalSpacing;
                newTabIndex = 19;
            }
            else
            {
                // Đây là trang tiếp theo
                newY = lastPagePanel.Bottom + verticalSpacing;
                newTabIndex = lastPagePanel.TabIndex + 1;
            }

            pagePanel.Location = new System.Drawing.Point(newX, newY);
            pagePanel.TabIndex = newTabIndex;

            // 5. Thêm nút (View) vào Panel chứa
            guna2Panel7.Controls.Add(pagePanel);

            // 6. Tự động cuộn xuống
            guna2Panel7.ScrollControlIntoView(pagePanel);


            // === PHẦN 3: CẬP NHẬT TRẠNG THÁI (LOGIC) ===
            if (!isFirstPage)
            {
                SwitchToPage(buttonID);
            }
            else
            {
                currentPageButtonID = buttonID;
                UpdatePageButtonUI();
            }
        }

        // --- HÀM MỚI: ĐỂ CLICK XUYÊN (TÙY CHỌN) ---
        // (Hàm này giúp khi nhấn vào Label cũng là nhấn vào Nút)
        private void PageControl_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control?.Parent != null)
            {
                // Kích hoạt sự kiện Click của control cha (Panel)
                // (Hoặc tìm Guna2GradientButton bên trong và gọi Click)
                Guna.UI2.WinForms.Guna2GradientButton btn = control.Parent.Controls.OfType<Guna.UI2.WinForms.Guna2GradientButton>().FirstOrDefault();
                if (btn != null)
                {
                    PageButton_Click(btn, e); // Gọi hàm Click chính
                }
            }
        }

        // (Thay thế hàm PageButton_Click cũ)

        private void PageButton_Click(object sender, EventArgs e)
        {
            // SỬA LỖI: Phải ép kiểu (cast) về Guna2GradientButton
            // (Vì đây là kiểu nút bạn đã tạo trong AddNewPage)
            Guna.UI2.WinForms.Guna2GradientButton clickedButton = sender as Guna.UI2.WinForms.Guna2GradientButton;

            if (clickedButton != null)
            {
                // Lấy ID từ Tag (đã đúng)
                int pageID = (int)clickedButton.Tag;
                SwitchToPage(pageID);
            }
        }

        private void SwitchToPage(int pageID)
        {
            if (currentPageButtonID == pageID) return; // Đang ở trang đó rồi

            currentPageButtonID = pageID;

            // Cập nhật giao diện (làm nổi bật nút trang được chọn)
            UpdatePageButtonUI();

            // Yêu cầu bảng vẽ (guna2Panel12) tự vẽ lại (sẽ gọi sự kiện Paint)
            panel_DrawingSurface.Invalidate();
        }

        // Cập nhật giao diện (highlight) các nút trang
        private void UpdatePageButtonUI()
        {
            // 'guna2Panel7' là Panel chứa các nút trang
            foreach (Control ctrl in guna2Panel7.Controls)
            {
                // SỬA LỖI: Tìm Guna2GradientPanel (thay vì Guna2Button)
                if (ctrl is Guna.UI2.WinForms.Guna2GradientPanel)
                {
                    Guna.UI2.WinForms.Guna2GradientPanel btnPanel = ctrl as Guna.UI2.WinForms.Guna2GradientPanel;
                    int pageID = (int)btnPanel.Tag; // Lấy ID từ Tag của Panel

                    // Lấy Label bên trong để đổi màu chữ
                    Guna.UI2.WinForms.Guna2HtmlLabel lbl = btnPanel.Controls.OfType<Guna.UI2.WinForms.Guna2HtmlLabel>().FirstOrDefault();

                    // Nếu là nút của trang hiện tại -> làm nổi bật
                    if (pageID == currentPageButtonID)
                    {
                        btnPanel.FillColor = Color.DodgerBlue;
                        btnPanel.FillColor2 = Color.DarkBlue; // Bạn có thể đổi màu tùy ý
                        if (lbl != null) lbl.ForeColor = Color.White;
                    }
                    else // Nếu là nút trang khác -> mờ đi
                    {
                        // Dùng màu mặc định (từ code AddNewPage của bạn)
                        btnPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        btnPanel.FillColor2 = System.Drawing.Color.Silver;
                        if (lbl != null) lbl.ForeColor = Color.Black;
                    }
                }
            }
        }

        private Bitmap GetCurrentPage()
        {
            if (pages.ContainsKey(currentPageButtonID))
            {
                return pages[currentPageButtonID];
            }
            return null;
        }

        private Stack<Bitmap> GetCurrentUndoStack()
        {
            if (undoHistory.ContainsKey(currentPageButtonID))
            {
                return undoHistory[currentPageButtonID];
            }
            return new Stack<Bitmap>(); // Trả về Stack rỗng (an toàn)
        }

        private Stack<Bitmap> GetCurrentRedoStack()
        {
            if (redoHistory.ContainsKey(currentPageButtonID))
            {
                return redoHistory[currentPageButtonID];
            }
            return new Stack<Bitmap>(); // Trả về Stack rỗng (an toàn)
        }

        private void btn_Save_Export_PDF_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có trang nào để lưu không
            if (pages.Count == 0)
            {
                MessageBox.Show("Không có trang nào để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Hỏi người dùng muốn lưu tệp ở đâu
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF file|*.pdf";
            saveDialog.Title = "Lưu Bảng vẽ thành PDF";
            saveDialog.FileName = "BangVeCuaToi.pdf"; // Tên tệp mặc định

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveDialog.FileName;
                try
                {
                    // 3. Bắt đầu tạo tệp PDF
                    // Lấy kích thước của bảng vẽ làm kích thước PDF
                    // (Dùng 'panel_DrawingSurface' từ code của bạn)
                    iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(panel_DrawingSurface.Width, panel_DrawingSurface.Height);

                    // Đặt lề (margins) là 0
                    Document document = new Document(pageSize, 0, 0, 0, 0);

                    PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                    document.Open();

                    // 4. Lặp qua các trang (theo thứ tự ID)
                    List<int> pageIDs = new List<int>(pages.Keys);
                    pageIDs.Sort(); // Sắp xếp để đảm bảo Trang 1, Trang 2...

                    foreach (int pageID in pageIDs)
                    {
                        // Thêm trang mới vào PDF (trừ trang đầu tiên)
                        if (pageID != pageIDs[0])
                        {
                            document.NewPage();
                        }

                        Bitmap pageBitmap = pages[pageID]; // Lấy ảnh Bitmap của trang

                        // 5. Chuyển đổi Bitmap (System.Drawing) sang Image (iTextSharp)
                        iTextSharp.text.Image pdfImage;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pageBitmap.Save(ms, ImageFormat.Png); // Lưu ảnh vào RAM
                            byte[] bitmapBytes = ms.ToArray();
                            pdfImage = iTextSharp.text.Image.GetInstance(bitmapBytes);
                        }

                        // 6. Căn chỉnh ảnh vừa khít trang PDF
                        pdfImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                        pdfImage.SetAbsolutePosition(0, 0); // Đặt ở góc (0,0)

                        document.Add(pdfImage); // Thêm ảnh vào PDF
                    }

                    // 7. Đóng tệp
                    document.Close();
                    MessageBox.Show("Đã xuất PDF thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
