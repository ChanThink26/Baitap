using WinFormsApp1.Models;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    private static readonly Color Navy = Color.FromArgb(25, 69, 108);
    private static readonly Color Blue = Color.FromArgb(40, 112, 169);
    private static readonly Color LightBlue = Color.FromArgb(230, 241, 249);
    private readonly List<LopHoc> _classes = [];
    private readonly List<SinhVien> _students = [];
    private readonly TextBox _txtMaSV = new();
    private readonly TextBox _txtHoTen = new();
    private readonly DateTimePicker _dtpNgaySinh = new();
    private readonly RadioButton _radNam = new();
    private readonly RadioButton _radNu = new();
    private readonly TextBox _txtEmail = new();
    private readonly TextBox _txtDienThoai = new();
    private readonly NumericUpDown _numDiem = new();
    private readonly ComboBox _cmbLopHoc = new();
    private readonly ComboBox _cmbTrangThai = new();
    private readonly TextBox _txtTuKhoa = new();
    private readonly ComboBox _cmbLocLop = new();
    private readonly NumericUpDown _numDiemTu = new();
    private readonly DataGridView _grid = new();
    private readonly Button _btnThem = new();
    private readonly Button _btnSua = new();
    private readonly Button _btnXoa = new();
    private readonly Button _btnLamMoi = new();
    private readonly Label _lblTongSo = new();
    private bool _isPopulating;
    private bool _isEditing;
    private bool _isRefreshingGrid;

    public Form1()
    {
        InitializeComponent();
        BuildInterface();
        Load += Form1_Load;
    }

    private void BuildInterface()
    {
        Text = "Ứng dụng quản lý sinh viên";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(900, 650);
        ClientSize = new Size(1180, 760);
        BackColor = Color.FromArgb(244, 247, 250);
        Font = new Font("Segoe UI", 9F);

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 6,
            Padding = new Padding(12, 0, 12, 8),
            BackColor = BackColor
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 198));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        Controls.Add(root);

        var banner = new Panel { Dock = DockStyle.Fill, BackColor = Navy, Padding = new Padding(12, 0, 12, 0) };
        banner.Controls.Add(new Label
        {
            Text = "▣  Ứng dụng quản lý sinh viên",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(4, 11)
        });
        root.Controls.Add(banner, 0, 0);

        var heading = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1
        };
        heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
        heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
        heading.Controls.Add(new Label
        {
            Text = "QUẢN LÝ SINH VIÊN",
            ForeColor = Navy,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Fill
        }, 0, 0);
        heading.Controls.Add(new Label
        {
            Text = "Bài tập Windows Forms • Quan hệ Lớp học 1 — n Sinh viên",
            ForeColor = Color.FromArgb(80, 96, 112),
            TextAlign = ContentAlignment.MiddleRight,
            AutoEllipsis = true,
            Dock = DockStyle.Fill
        }, 1, 0);
        root.Controls.Add(heading, 0, 1);

        var details = new GroupBox
        {
            Text = "▏ Thông tin sinh viên",
            Dock = DockStyle.Fill,
            ForeColor = Blue,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Padding = new Padding(10, 8, 10, 8),
            BackColor = Color.White
        };
        var detailsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 6,
            RowCount = 4,
            Padding = new Padding(0, 8, 0, 0),
            BackColor = Color.White,
            Font = new Font("Segoe UI", 9F)
        };
        detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
        detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
        detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
        detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
        detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
        detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23));
        detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3F));
        detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3F));
        detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.4F));
        detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        details.Controls.Add(detailsLayout);
        root.Controls.Add(details, 0, 2);

        ConfigureInput(_txtMaSV, 0);
        ConfigureInput(_txtHoTen, 1);
        ConfigureInput(_txtEmail, 6);
        ConfigureInput(_txtDienThoai, 7);
        ConfigureInput(_dtpNgaySinh, 3);
        _dtpNgaySinh.Format = DateTimePickerFormat.Custom;
        _dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
        _dtpNgaySinh.MaxDate = DateTime.Today;
        ConfigureInput(_cmbLopHoc, 2);
        _cmbLopHoc.DropDownStyle = ComboBoxStyle.DropDownList;
        ConfigureInput(_cmbTrangThai, 8);
        _cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        ConfigureInput(_numDiem, 5);
        _numDiem.DecimalPlaces = 1;
        _numDiem.Increment = 0.1M;
        _numDiem.Maximum = 10;
        _numDiem.Minimum = 0;
        _numDiem.TextAlign = HorizontalAlignment.Left;

        AddField(detailsLayout, "Mã sinh viên", _txtMaSV, 0, 0);
        AddField(detailsLayout, "Họ và tên", _txtHoTen, 0, 2);
        AddField(detailsLayout, "Lớp học", _cmbLopHoc, 0, 4);
        AddField(detailsLayout, "Ngày sinh", _dtpNgaySinh, 1, 0);
        AddField(detailsLayout, "Giới tính", BuildGenderControl(), 1, 2);
        AddField(detailsLayout, "Điểm", _numDiem, 1, 4);
        AddField(detailsLayout, "Email", _txtEmail, 2, 0);
        AddField(detailsLayout, "Điện thoại", _txtDienThoai, 2, 2);
        AddField(detailsLayout, "Trạng thái", _cmbTrangThai, 2, 4);

        var actions = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false,
            Padding = new Padding(0, 5, 0, 0)
        };
        StyleButton(_btnLamMoi, "↻  Làm mới", Color.FromArgb(113, 131, 148));
        StyleButton(_btnXoa, "Xóa", Color.FromArgb(207, 78, 86));
        StyleButton(_btnSua, "✎  Sửa", Blue);
        StyleButton(_btnThem, "Thêm", Color.FromArgb(37, 145, 112));
        actions.Controls.AddRange([_btnLamMoi, _btnXoa, _btnSua, _btnThem]);
        detailsLayout.Controls.Add(actions, 0, 3);
        detailsLayout.SetColumnSpan(actions, 6);

        var searchPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            WrapContents = false,
            BackColor = Color.White,
            Padding = new Padding(8, 11, 6, 4),
            Margin = new Padding(0, 4, 0, 4),
            AutoScroll = true
        };
        searchPanel.Controls.Add(MakeLabel("Từ khóa", 55));
        ConfigureInput(_txtTuKhoa, 0);
        _txtTuKhoa.Width = 245;
        _txtTuKhoa.Dock = DockStyle.None;
        _txtTuKhoa.PlaceholderText = "Mã, họ tên, email hoặc điện thoại";
        searchPanel.Controls.Add(_txtTuKhoa);
        searchPanel.Controls.Add(MakeLabel("Lớp", 32));
        ConfigureInput(_cmbLocLop, 0);
        _cmbLocLop.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbLocLop.Width = 180;
        _cmbLocLop.Dock = DockStyle.None;
        searchPanel.Controls.Add(_cmbLocLop);
        searchPanel.Controls.Add(MakeLabel("Điểm từ", 55));
        ConfigureInput(_numDiemTu, 0);
        _numDiemTu.Width = 75;
        _numDiemTu.DecimalPlaces = 1;
        _numDiemTu.Increment = 0.5M;
        _numDiemTu.Maximum = 10;
        _numDiemTu.Minimum = 0;
        _numDiemTu.Dock = DockStyle.None;
        searchPanel.Controls.Add(_numDiemTu);
        var btnTim = new Button();
        StyleButton(btnTim, "⌕  Tìm kiếm", Blue);
        searchPanel.Controls.Add(btnTim);
        var btnTatCa = new Button();
        StyleButton(btnTatCa, "Hiển thị tất cả", Color.FromArgb(225, 239, 249), Navy);
        searchPanel.Controls.Add(btnTatCa);
        root.Controls.Add(searchPanel, 0, 3);

        var gridPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(8) };
        var gridHeader = new Panel { Dock = DockStyle.Top, Height = 30 };
        gridHeader.Controls.Add(new Label
        {
            Text = "Danh sách sinh viên",
            ForeColor = Navy,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(2, 4)
        });
        _lblTongSo.Text = "Tổng số: 0 sinh viên";
        _lblTongSo.ForeColor = Color.FromArgb(70, 95, 108);
        _lblTongSo.Dock = DockStyle.Right;
        _lblTongSo.Width = 220;
        _lblTongSo.TextAlign = ContentAlignment.MiddleRight;
        gridHeader.Controls.Add(_lblTongSo);
        gridPanel.Controls.Add(_grid);
        gridPanel.Controls.Add(gridHeader);
        ConfigureGrid();
        root.Controls.Add(gridPanel, 0, 4);

        var footer = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = Color.FromArgb(234, 239, 243)
        };
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
        footer.Controls.Add(new Label
        {
            Text = "Bài tập: xây dựng Windows Forms quản lý sinh viên theo lớp",
            ForeColor = Color.FromArgb(83, 99, 112),
            TextAlign = ContentAlignment.MiddleLeft,
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Padding = new Padding(8, 0, 0, 0)
        }, 0, 0);
        footer.Controls.Add(new Label
        {
            Text = "Thêm • Sửa • Xóa • Tìm kiếm • Lọc theo lớp",
            ForeColor = Color.FromArgb(83, 99, 112),
            TextAlign = ContentAlignment.MiddleRight,
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Padding = new Padding(0, 0, 8, 0)
        }, 1, 0);
        root.Controls.Add(footer, 0, 5);

        _txtMaSV.TextChanged += TxtMaSV_TextChanged;
        _btnThem.Click += (_, _) => AddStudent();
        _btnSua.Click += (_, _) => EditStudent();
        _btnXoa.Click += (_, _) => DeleteStudent();
        _btnLamMoi.Click += (_, _) => ResetForm();
        btnTim.Click += (_, _) => RefreshGrid(applyFilters: true);
        _txtTuKhoa.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshGrid(applyFilters: true);
                e.SuppressKeyPress = true;
            }
        };
        btnTatCa.Click += (_, _) =>
        {
            _txtTuKhoa.Clear();
            _cmbLocLop.SelectedIndex = 0;
            _numDiemTu.Value = 0;
            RefreshGrid(applyFilters: false);
        };
        _grid.SelectionChanged += Grid_SelectionChanged;
        _grid.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0)
            {
                var code = _grid.Rows[e.RowIndex].Cells[nameof(StudentGridRow.MaSV)].Value?.ToString();
                if (code is not null)
                {
                    _txtMaSV.Text = code;
                    if (_btnSua.Enabled)
                    {
                        EditStudent();
                        _txtHoTen.Focus();
                    }
                }
            }
        };
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        SeedData();
        _cmbLopHoc.DataSource = _classes;
        _cmbLopHoc.DisplayMember = nameof(LopHoc.TenLop);
        _cmbLocLop.Items.Add("Tất cả lớp");
        _cmbLocLop.Items.AddRange(_classes.Select(lop => (object)lop.TenLop).ToArray());
        _cmbLocLop.SelectedIndex = 0;
        _cmbTrangThai.Items.AddRange(["Đang học", "Bảo lưu", "Đã tốt nghiệp"]);
        _cmbTrangThai.SelectedIndex = 0;
        RefreshGrid(applyFilters: false);
        ResetForm();
    }

    private void SeedData()
    {
        var class1 = new LopHoc { MaLop = "CNTT01", TenLop = "Kỹ thuật phần mềm 01" };
        var class2 = new LopHoc { MaLop = "CNTT02", TenLop = "Trí tuệ nhân tạo 01" };
        var class3 = new LopHoc { MaLop = "CNTT03", TenLop = "Khoa học dữ liệu 01" };
        _classes.AddRange([class1, class2, class3]);
        AddSeedStudent("SV000123", "Nguyễn Văn An", new DateTime(2005, 8, 16), "Nam", "an.ngv@edu.vn", "0912345678", 8.5M, class1);
        AddSeedStudent("SV000124", "Trần Minh Anh", new DateTime(2006, 1, 22), "Nữ", "anh.tm@edu.vn", "0987654321", 9.0M, class2);
        AddSeedStudent("SV000125", "Lê Hoàng Bình", new DateTime(2005, 9, 5), "Nam", "binh.lh@edu.vn", "0355555677", 7.4M, class1);
        AddSeedStudent("SV000126", "Đỗ Thị Hồng", new DateTime(2006, 11, 30), "Nữ", "hong.dt@edu.vn", "0777668899", 8.1M, class3);
    }

    private void AddSeedStudent(string code, string name, DateTime birthDate, string gender, string email, string phone, decimal score, LopHoc lopHoc)
    {
        var student = new SinhVien
        {
            MaSV = code,
            HoTen = name,
            NgaySinh = birthDate,
            GioiTinh = gender,
            Email = email,
            DienThoai = phone,
            Diem = score,
            TrangThai = "Đang học",
            LopHoc = lopHoc
        };
        _students.Add(student);
        lopHoc.SinhViens.Add(student);
    }

    private static void ConfigureInput(Control control, int tabIndex)
    {
        control.TabIndex = tabIndex;
        control.Margin = new Padding(3, 4, 8, 4);
        control.Dock = DockStyle.Fill;
        control.Font = new Font("Segoe UI", 9F);
    }

    private static void AddField(TableLayoutPanel layout, string caption, Control input, int row, int labelColumn)
    {
        var label = new Label
        {
            Text = caption,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleRight,
            ForeColor = Color.FromArgb(51, 70, 90),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Margin = new Padding(2, 2, 6, 2),
            TabStop = false
        };
        layout.Controls.Add(label, labelColumn, row);
        layout.Controls.Add(input, labelColumn + 1, row);
    }

    private Control BuildGenderControl()
    {
        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Margin = new Padding(3, 4, 8, 4),
            Padding = new Padding(3, 4, 0, 0)
        };
        _radNam.Text = "Nam";
        _radNam.Checked = true;
        _radNam.AutoSize = true;
        _radNam.TabIndex = 4;
        _radNu.Text = "Nữ";
        _radNu.AutoSize = true;
        _radNu.TabIndex = 4;
        panel.Controls.AddRange([_radNam, _radNu]);
        return panel;
    }

    private static Label MakeLabel(string text, int width) => new()
    {
        Text = text,
        Width = width,
        Height = 28,
        TextAlign = ContentAlignment.MiddleRight,
        ForeColor = Color.FromArgb(51, 70, 90),
        Margin = new Padding(5, 0, 4, 0),
        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
    };

    private static void StyleButton(Button button, string text, Color color, Color? foreground = null)
    {
        button.Text = text;
        button.AutoSize = true;
        button.MinimumSize = new Size(84, 30);
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = color;
        button.ForeColor = foreground ?? Color.White;
        button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        button.Margin = new Padding(4, 0, 4, 0);
        button.Cursor = Cursors.Hand;
    }

    private void ConfigureGrid()
    {
        _grid.Dock = DockStyle.Fill;
        _grid.Top = 30;
        _grid.ReadOnly = true;
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        _grid.MultiSelect = false;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.AutoGenerateColumns = false;
        _grid.RowHeadersVisible = false;
        _grid.BackgroundColor = Color.White;
        _grid.BorderStyle = BorderStyle.None;
        _grid.EnableHeadersVisualStyles = false;
        _grid.ColumnHeadersDefaultCellStyle.BackColor = LightBlue;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = Navy;
        _grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _grid.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F);
        _grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(205, 229, 244);
        _grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(27, 48, 66);
        _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 253);
        _grid.RowTemplate.Height = 28;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        AddGridColumn(nameof(StudentGridRow.MaSV), "Mã SV", 85);
        AddGridColumn(nameof(StudentGridRow.HoTen), "Họ và tên", 145);
        AddGridColumn(nameof(StudentGridRow.NgaySinh), "Ngày sinh", 92);
        AddGridColumn(nameof(StudentGridRow.GioiTinh), "Giới tính", 75);
        AddGridColumn(nameof(StudentGridRow.Email), "Email", 150);
        AddGridColumn(nameof(StudentGridRow.DienThoai), "Điện thoại", 115);
        AddGridColumn(nameof(StudentGridRow.Diem), "Điểm", 60);
        AddGridColumn(nameof(StudentGridRow.TenLop), "Lớp", 170);
        AddGridColumn(nameof(StudentGridRow.TrangThai), "Trạng thái", 110);
    }

    private void AddGridColumn(string property, string header, int width) => _grid.Columns.Add(new DataGridViewTextBoxColumn
    {
        DataPropertyName = property,
        HeaderText = header,
        Name = property,
        Width = width,
        SortMode = DataGridViewColumnSortMode.NotSortable
    });

    private void TxtMaSV_TextChanged(object? sender, EventArgs e)
    {
        if (_isPopulating)
        {
            return;
        }

        var student = _students.FirstOrDefault(s => string.Equals(s.MaSV, _txtMaSV.Text.Trim(), StringComparison.OrdinalIgnoreCase));
        if (student is null)
        {
            ClearStudentFields(keepCode: true);
            SetMode(isExisting: false, isEditing: false);
        }
        else
        {
            DisplayStudent(student);
            SetMode(isExisting: true, isEditing: false);
        }
    }

    private void DisplayStudent(SinhVien student)
    {
        _isPopulating = true;
        _txtMaSV.Text = student.MaSV;
        _txtHoTen.Text = student.HoTen;
        _dtpNgaySinh.Value = student.NgaySinh.Date > DateTime.Today ? DateTime.Today : student.NgaySinh;
        _radNam.Checked = student.GioiTinh == "Nam";
        _radNu.Checked = student.GioiTinh == "Nữ";
        _txtEmail.Text = student.Email;
        _txtDienThoai.Text = student.DienThoai;
        _numDiem.Value = Math.Clamp(student.Diem, _numDiem.Minimum, _numDiem.Maximum);
        _cmbLopHoc.SelectedItem = student.LopHoc;
        _cmbTrangThai.SelectedItem = student.TrangThai;
        _isPopulating = false;
    }

    private void ClearStudentFields(bool keepCode)
    {
        _isPopulating = true;
        if (!keepCode)
        {
            _txtMaSV.Clear();
        }
        _txtHoTen.Clear();
        _dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
        _radNam.Checked = true;
        _txtEmail.Clear();
        _txtDienThoai.Clear();
        _numDiem.Value = 0;
        if (_cmbLopHoc.Items.Count > 0)
        {
            _cmbLopHoc.SelectedIndex = 0;
        }
        if (_cmbTrangThai.Items.Count > 0)
        {
            _cmbTrangThai.SelectedIndex = 0;
        }
        _isPopulating = false;
    }

    private void SetMode(bool isExisting, bool isEditing)
    {
        _isEditing = isEditing;
        var inputsEnabled = !isExisting || isEditing;
        _txtMaSV.Enabled = !isExisting;
        _txtHoTen.Enabled = inputsEnabled;
        _dtpNgaySinh.Enabled = inputsEnabled;
        _radNam.Enabled = inputsEnabled;
        _radNu.Enabled = inputsEnabled;
        _txtEmail.Enabled = inputsEnabled;
        _txtDienThoai.Enabled = inputsEnabled;
        _numDiem.Enabled = inputsEnabled;
        _cmbLopHoc.Enabled = inputsEnabled;
        _cmbTrangThai.Enabled = inputsEnabled;
        _btnThem.Enabled = !isExisting && !isEditing;
        _btnSua.Enabled = isExisting;
        _btnXoa.Enabled = isExisting && !isEditing;
        _btnSua.Text = isEditing ? "Lưu sửa" : "✎  Sửa";
        _grid.Enabled = !isEditing;
    }

    private void AddStudent()
    {
        var student = ReadStudentFromForm();
        if (!ValidateStudent(student))
        {
            return;
        }
        if (_students.Any(s => string.Equals(s.MaSV, student.MaSV, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Mã sinh viên đã tồn tại.", "Không thể thêm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtMaSV.Focus();
            return;
        }
        if (student.LopHoc!.SinhViens.Count >= student.LopHoc.SiSoToiDa)
        {
            MessageBox.Show("Lớp đã đạt sĩ số tối đa.", "Không thể thêm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _students.Add(student);
        student.LopHoc.SinhViens.Add(student);
        RefreshGrid(applyFilters: false);
        DisplayStudent(student);
        SetMode(isExisting: true, isEditing: false);
        MessageBox.Show("Đã thêm sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void EditStudent()
    {
        var current = _students.FirstOrDefault(s => string.Equals(s.MaSV, _txtMaSV.Text.Trim(), StringComparison.OrdinalIgnoreCase));
        if (current is null)
        {
            return;
        }
        if (!_isEditing)
        {
            SetMode(isExisting: true, isEditing: true);
            _txtHoTen.Focus();
            return;
        }

        var updated = ReadStudentFromForm();
        if (!ValidateStudent(updated))
        {
            return;
        }
        if (!ReferenceEquals(current.LopHoc, updated.LopHoc) && updated.LopHoc!.SinhViens.Count >= updated.LopHoc.SiSoToiDa)
        {
            MessageBox.Show("Lớp đã đạt sĩ số tối đa.", "Không thể sửa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (MessageBox.Show("Bạn có chắc muốn lưu thay đổi của sinh viên này?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        current.LopHoc!.SinhViens.Remove(current);
        current.HoTen = updated.HoTen;
        current.NgaySinh = updated.NgaySinh;
        current.GioiTinh = updated.GioiTinh;
        current.Email = updated.Email;
        current.DienThoai = updated.DienThoai;
        current.Diem = updated.Diem;
        current.TrangThai = updated.TrangThai;
        current.LopHoc = updated.LopHoc;
        current.LopHoc!.SinhViens.Add(current);
        RefreshGrid(applyFilters: false);
        DisplayStudent(current);
        SetMode(isExisting: true, isEditing: false);
        MessageBox.Show("Đã cập nhật sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void DeleteStudent()
    {
        if (_isEditing)
        {
            MessageBox.Show("Hãy lưu hoặc làm mới trước khi xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var student = _students.FirstOrDefault(s => string.Equals(s.MaSV, _txtMaSV.Text.Trim(), StringComparison.OrdinalIgnoreCase));
        if (student is null)
        {
            return;
        }
        if (MessageBox.Show($"Bạn có chắc muốn xóa sinh viên {student.HoTen} ({student.MaSV})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            return;
        }

        _students.Remove(student);
        student.LopHoc!.SinhViens.Remove(student);
        ResetForm();
        RefreshGrid(applyFilters: false);
        MessageBox.Show("Đã xóa sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private SinhVien ReadStudentFromForm() => new()
    {
        MaSV = _txtMaSV.Text.Trim(),
        HoTen = _txtHoTen.Text.Trim(),
        NgaySinh = _dtpNgaySinh.Value.Date,
        GioiTinh = _radNam.Checked ? "Nam" : "Nữ",
        Email = _txtEmail.Text.Trim(),
        DienThoai = _txtDienThoai.Text.Trim(),
        Diem = _numDiem.Value,
        TrangThai = _cmbTrangThai.SelectedItem?.ToString() ?? string.Empty,
        LopHoc = _cmbLopHoc.SelectedItem as LopHoc
    };

    private bool ValidateStudent(SinhVien student)
    {
        if (student.IsValid(out var errors))
        {
            return true;
        }
        var message = string.Join(Environment.NewLine, errors.Select(error => $"• {error.ErrorMessage}"));
        MessageBox.Show(message, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        var firstInvalidProperty = errors.SelectMany(error => error.MemberNames).FirstOrDefault();
        Control? control = firstInvalidProperty switch
        {
            nameof(SinhVien.MaSV) => _txtMaSV,
            nameof(SinhVien.HoTen) => _txtHoTen,
            nameof(SinhVien.NgaySinh) => _dtpNgaySinh,
            nameof(SinhVien.GioiTinh) => _radNam,
            nameof(SinhVien.Email) => _txtEmail,
            nameof(SinhVien.DienThoai) => _txtDienThoai,
            nameof(SinhVien.Diem) => _numDiem,
            nameof(SinhVien.LopHoc) => _cmbLopHoc,
            nameof(SinhVien.TrangThai) => _cmbTrangThai,
            _ => null
        };
        control?.Focus();
        return false;
    }

    private void ResetForm()
    {
        _isPopulating = true;
        _txtMaSV.Clear();
        _isPopulating = false;
        ClearStudentFields(keepCode: true);
        SetMode(isExisting: false, isEditing: false);
        _txtMaSV.Focus();
        _grid.CurrentCell = null;
        _grid.ClearSelection();
    }

    private void RefreshGrid(bool applyFilters)
    {
        IEnumerable<SinhVien> results = _students;
        if (applyFilters)
        {
            var keyword = _txtTuKhoa.Text.Trim();
            if (keyword.Length > 0)
            {
                results = results.Where(s => s.MaSV.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || s.HoTen.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
                    || s.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || s.DienThoai.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }
            if (_cmbLocLop.SelectedIndex > 0 && _cmbLocLop.SelectedIndex <= _classes.Count)
            {
                var selectedClass = _classes[_cmbLocLop.SelectedIndex - 1];
                results = results.Where(s => ReferenceEquals(s.LopHoc, selectedClass));
            }
            if (_numDiemTu.Value > 0)
            {
                results = results.Where(s => s.Diem >= _numDiemTu.Value);
            }
        }

        var rows = results.Select(s => new StudentGridRow(
            s.MaSV,
            s.HoTen,
            s.NgaySinh.ToString("dd/MM/yyyy"),
            s.GioiTinh,
            s.Email,
            s.DienThoai,
            s.Diem.ToString("0.0"),
            s.LopHoc?.TenLop ?? string.Empty,
            s.TrangThai)).ToList();
        _isRefreshingGrid = true;
        _grid.DataSource = rows;
        _isRefreshingGrid = false;
        _lblTongSo.Text = $"Tổng số: {_students.Count} sinh viên";
    }

    private void Grid_SelectionChanged(object? sender, EventArgs e)
    {
        if (!_isRefreshingGrid && _grid.CurrentRow?.DataBoundItem is StudentGridRow row && !_isEditing)
        {
            var student = _students.FirstOrDefault(s => s.MaSV == row.MaSV);
            if (student is not null)
            {
                DisplayStudent(student);
                SetMode(isExisting: true, isEditing: false);
            }
        }
    }

    private sealed record StudentGridRow(string MaSV, string HoTen, string NgaySinh, string GioiTinh, string Email, string DienThoai, string Diem, string TenLop, string TrangThai);
}
