namespace ImageHandler
{
    partial class frmImageHandler
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImageHandler));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmToGray = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBrightness = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmResize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCutImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgbProcessing = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDraw = new System.Windows.Forms.ToolStripButton();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboLineWeight = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cboDrawStyle = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.說明LToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.picMain = new System.Windows.Forms.PictureBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnForeColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.picThumbnail = new System.Windows.Forms.PictureBox();
            this.menuMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmEdit,
            this.tsmAbout});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(657, 24);
            this.menuMain.TabIndex = 11;
            this.menuMain.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNew,
            this.tsmOpen,
            this.tsmSave,
            this.tsmReload,
            this.tsmSaveAs});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(38, 20);
            this.tsmFile.Text = "&File";
            // 
            // tsmNew
            // 
            this.tsmNew.Name = "tsmNew";
            this.tsmNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmNew.Size = new System.Drawing.Size(193, 22);
            this.tsmNew.Text = "New";
            this.tsmNew.Click += new System.EventHandler(this.tsmNew_Click);
            // 
            // tsmOpen
            // 
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmOpen.Size = new System.Drawing.Size(193, 22);
            this.tsmOpen.Text = "&Open";
            this.tsmOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // tsmSave
            // 
            this.tsmSave.Enabled = false;
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(193, 22);
            this.tsmSave.Text = "&Save";
            this.tsmSave.Click += new System.EventHandler(this.tsmSave_Click);
            // 
            // tsmReload
            // 
            this.tsmReload.Enabled = false;
            this.tsmReload.Name = "tsmReload";
            this.tsmReload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsmReload.Size = new System.Drawing.Size(193, 22);
            this.tsmReload.Text = "&Reload";
            this.tsmReload.Click += new System.EventHandler(this.tsmReload_Click);
            // 
            // tsmSaveAs
            // 
            this.tsmSaveAs.Name = "tsmSaveAs";
            this.tsmSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tsmSaveAs.Size = new System.Drawing.Size(193, 22);
            this.tsmSaveAs.Text = "Save &As";
            this.tsmSaveAs.Click += new System.EventHandler(this.tsmSaveAs_Click);
            // 
            // tsmEdit
            // 
            this.tsmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCut,
            this.tsmCopy,
            this.tsmPaste,
            this.tsmUndo,
            this.toolStripMenuItem1,
            this.tsmToGray,
            this.tsmRotate,
            this.tsmBrightness,
            this.tsmResize,
            this.tsmCutImage});
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(41, 20);
            this.tsmEdit.Text = "&Edit";
            // 
            // tsmCut
            // 
            this.tsmCut.Name = "tsmCut";
            this.tsmCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmCut.Size = new System.Drawing.Size(148, 22);
            this.tsmCut.Text = "Cut";
            this.tsmCut.Click += new System.EventHandler(this.tsmCut_Click);
            // 
            // tsmCopy
            // 
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmCopy.Size = new System.Drawing.Size(148, 22);
            this.tsmCopy.Text = "Copy";
            this.tsmCopy.Click += new System.EventHandler(this.tsmCopy_Click);
            // 
            // tsmPaste
            // 
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmPaste.Size = new System.Drawing.Size(148, 22);
            this.tsmPaste.Text = "Paste";
            this.tsmPaste.Click += new System.EventHandler(this.tsmPaste_Click);
            // 
            // tsmUndo
            // 
            this.tsmUndo.Enabled = false;
            this.tsmUndo.Name = "tsmUndo";
            this.tsmUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmUndo.Size = new System.Drawing.Size(148, 22);
            this.tsmUndo.Text = "Undo";
            this.tsmUndo.Click += new System.EventHandler(this.tsmUndo_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            this.toolStripMenuItem1.Tag = "";
            // 
            // tsmToGray
            // 
            this.tsmToGray.Enabled = false;
            this.tsmToGray.Name = "tsmToGray";
            this.tsmToGray.Size = new System.Drawing.Size(148, 22);
            this.tsmToGray.Text = "Grayscale";
            this.tsmToGray.Click += new System.EventHandler(this.tsmToGray_Click);
            // 
            // tsmRotate
            // 
            this.tsmRotate.Enabled = false;
            this.tsmRotate.Name = "tsmRotate";
            this.tsmRotate.Size = new System.Drawing.Size(148, 22);
            this.tsmRotate.Text = "Rotate";
            this.tsmRotate.Click += new System.EventHandler(this.tsmRotate_Click);
            // 
            // tsmBrightness
            // 
            this.tsmBrightness.Enabled = false;
            this.tsmBrightness.Name = "tsmBrightness";
            this.tsmBrightness.Size = new System.Drawing.Size(148, 22);
            this.tsmBrightness.Text = "Brightness";
            this.tsmBrightness.Click += new System.EventHandler(this.tsmBrightness_Click);
            // 
            // tsmResize
            // 
            this.tsmResize.Enabled = false;
            this.tsmResize.Name = "tsmResize";
            this.tsmResize.Size = new System.Drawing.Size(148, 22);
            this.tsmResize.Text = "Resize";
            this.tsmResize.Click += new System.EventHandler(this.tsmResize_Click);
            // 
            // tsmCutImage
            // 
            this.tsmCutImage.Enabled = false;
            this.tsmCutImage.Name = "tsmCutImage";
            this.tsmCutImage.Size = new System.Drawing.Size(148, 22);
            this.tsmCutImage.Text = "Cut Image";
            this.tsmCutImage.Click += new System.EventHandler(this.tsmCutImage_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(54, 20);
            this.tsmAbout.Text = "&About";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pgbProcessing,
            this.toolStripStatusLabel1,
            this.lblInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 490);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(657, 22);
            this.statusStrip.TabIndex = 12;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 17);
            this.lblStatus.Text = "Ready";
            // 
            // pgbProcessing
            // 
            this.pgbProcessing.Name = "pgbProcessing";
            this.pgbProcessing.Size = new System.Drawing.Size(100, 16);
            this.pgbProcessing.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(599, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(125, 175);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDraw,
            this.btnSelect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 49);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(56, 441);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDraw
            // 
            this.btnDraw.AutoSize = false;
            this.btnDraw.CheckOnClick = true;
            this.btnDraw.Image = ((System.Drawing.Image)(resources.GetObject("btnDraw.Image")));
            this.btnDraw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(55, 20);
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.CheckOnClick = true;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(53, 19);
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 20);
            this.toolStripLabel1.Text = "Line Weight";
            // 
            // cboLineWeight
            // 
            this.cboLineWeight.AutoSize = false;
            this.cboLineWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLineWeight.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cboLineWeight.Name = "cboLineWeight";
            this.cboLineWeight.Size = new System.Drawing.Size(40, 23);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnOpen,
            this.btnPrint,
            this.toolStripSeparator,
            this.toolStripLabel1,
            this.cboLineWeight,
            this.toolStripLabel2,
            this.cboDrawStyle,
            this.toolStripSeparator2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.toolStripSeparator1,
            this.說明LToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 24);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(1);
            this.toolStrip2.Size = new System.Drawing.Size(657, 25);
            this.toolStrip2.TabIndex = 14;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 20);
            this.btnNew.Text = "New(&N)";
            this.btnNew.Click += new System.EventHandler(this.tsmNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 20);
            this.btnSave.Text = "Save(&S)";
            this.btnSave.Click += new System.EventHandler(this.tsmSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 20);
            this.btnOpen.Text = "Open(&O)";
            this.btnOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 20);
            this.btnPrint.Text = "Print(&P)";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(66, 20);
            this.toolStripLabel2.Text = "Draw Style";
            // 
            // cboDrawStyle
            // 
            this.cboDrawStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrawStyle.Items.AddRange(new object[] {
            "Pen",
            "Line",
            "Rectangle",
            "Circle"});
            this.cboDrawStyle.Name = "cboDrawStyle";
            this.cboDrawStyle.Size = new System.Drawing.Size(85, 23);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 20);
            this.btnCut.Text = "Cut(&U)";
            this.btnCut.Click += new System.EventHandler(this.tsmCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 20);
            this.btnCopy.Text = "Copy(&C)";
            this.btnCopy.Click += new System.EventHandler(this.tsmCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 20);
            this.btnPaste.Text = "Paste(&P)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // 說明LToolStripButton
            // 
            this.說明LToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.說明LToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("說明LToolStripButton.Image")));
            this.說明LToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.說明LToolStripButton.Name = "說明LToolStripButton";
            this.說明LToolStripButton.Size = new System.Drawing.Size(23, 20);
            this.說明LToolStripButton.Text = "About(&A)";
            this.說明LToolStripButton.Click += new System.EventHandler(this.tsmAbout_Click);
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.Color.Transparent;
            this.picMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMain.Location = new System.Drawing.Point(0, 0);
            this.picMain.Margin = new System.Windows.Forms.Padding(0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(594, 441);
            this.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMain.TabIndex = 13;
            this.picMain.TabStop = false;
            this.picMain.Paint += new System.Windows.Forms.PaintEventHandler(this.picMain_Paint);
            this.picMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseDown);
            this.picMain.MouseLeave += new System.EventHandler(this.picMain_MouseLeave);
            this.picMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseMove);
            this.picMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseUp);
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelMain.Controls.Add(this.picMain);
            this.panelMain.Location = new System.Drawing.Point(63, 49);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(594, 441);
            this.panelMain.TabIndex = 0;
            // 
            // btnForeColor
            // 
            this.btnForeColor.BackColor = System.Drawing.SystemColors.Control;
            this.btnForeColor.Location = new System.Drawing.Point(6, 419);
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.Size = new System.Drawing.Size(36, 36);
            this.btnForeColor.TabIndex = 14;
            this.btnForeColor.UseVisualStyleBackColor = false;
            this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            this.btnForeColor.Paint += new System.Windows.Forms.PaintEventHandler(this.btnForeColor_Paint);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Location = new System.Drawing.Point(21, 434);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(36, 36);
            this.btnBackColor.TabIndex = 15;
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            this.btnBackColor.Paint += new System.Windows.Forms.PaintEventHandler(this.btnBackColor_Paint);
            // 
            // picThumbnail
            // 
            this.picThumbnail.Location = new System.Drawing.Point(-14, 232);
            this.picThumbnail.Name = "picThumbnail";
            this.picThumbnail.Size = new System.Drawing.Size(100, 50);
            this.picThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picThumbnail.TabIndex = 14;
            this.picThumbnail.TabStop = false;
            this.picThumbnail.Visible = false;
            // 
            // frmImageHandler
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 512);
            this.Controls.Add(this.picThumbnail);
            this.Controls.Add(this.btnForeColor);
            this.Controls.Add(this.btnBackColor);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmImageHandler";
            this.Text = "ImageHandler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImageHandler_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmImageHandler_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmImageHandler_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmImageHandler_DragOver);
            this.DragLeave += new System.EventHandler(this.frmImageHandler_DragLeave);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImageHandler_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmImageHandler_KeyUp);
            this.Resize += new System.EventHandler(this.frmImageHandler_Resize);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmReload;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmToGray;
        private System.Windows.Forms.ToolStripMenuItem tsmRotate;
        private System.Windows.Forms.ToolStripMenuItem tsmBrightness;
        private System.Windows.Forms.ToolStripMenuItem tsmResize;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmNew;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboLineWeight;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnDraw;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cboDrawStyle;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmCut;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.Button btnForeColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.ToolStripMenuItem tsmCutImage;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton 說明LToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAs;
        private System.Windows.Forms.PictureBox picThumbnail;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripProgressBar pgbProcessing;
    }
}

