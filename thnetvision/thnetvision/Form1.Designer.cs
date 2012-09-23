namespace thnetvision
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Chat_SubmitBtn = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cardDetailPB = new System.Windows.Forms.PictureBox();
            this.awayField = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ゲーム開始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadCardlistiniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CardMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.アクティブにするToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.スリープにするToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.表向きにするToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.裏向きにするToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.homeLibrary = new System.Windows.Forms.PictureBox();
            this.LibraryMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.手札に加えるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.場に裏向きカードを1枚出すToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.山札をシャッフルするToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.awayLibrary = new System.Windows.Forms.PictureBox();
            this.homeNodeSleep = new System.Windows.Forms.PictureBox();
            this.homeNodeActive = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.homeHades = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.awayHand = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.awayHades = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.awayNodeSleep = new System.Windows.Forms.PictureBox();
            this.awayNodeActive = new System.Windows.Forms.PictureBox();
            this.awayLifePicture = new System.Windows.Forms.PictureBox();
            this.homeHand = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.homeField = new System.Windows.Forms.Panel();
            this.HomeFieldMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全てアクティブにToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.homeLife = new System.Windows.Forms.NumericUpDown();
            this.homeLifePicture = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cardDetailAbility = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cardDetailAtkDef = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.デッキを読み込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.cardDetailPB)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.CardMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.homeLibrary)).BeginInit();
            this.LibraryMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.awayLibrary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeNodeSleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeNodeActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeHades)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.awayHades)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.awayNodeSleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.awayNodeActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.awayLifePicture)).BeginInit();
            this.homeHand.SuspendLayout();
            this.HomeFieldMenuStrip.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.homeLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeLifePicture)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(7, 839);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "デッキ読込";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(81, 839);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Chat_SubmitBtn
            // 
            this.Chat_SubmitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Chat_SubmitBtn.Location = new System.Drawing.Point(222, 811);
            this.Chat_SubmitBtn.Name = "Chat_SubmitBtn";
            this.Chat_SubmitBtn.Size = new System.Drawing.Size(65, 24);
            this.Chat_SubmitBtn.TabIndex = 4;
            this.Chat_SubmitBtn.Text = "送信";
            this.Chat_SubmitBtn.UseVisualStyleBackColor = true;
            this.Chat_SubmitBtn.Click += new System.EventHandler(this.Chat_SubmitBtn_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(151, 839);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(136, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "カード一覧表示";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cardDetailPB
            // 
            this.cardDetailPB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cardDetailPB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cardDetailPB.Location = new System.Drawing.Point(7, 29);
            this.cardDetailPB.Name = "cardDetailPB";
            this.cardDetailPB.Size = new System.Drawing.Size(280, 400);
            this.cardDetailPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cardDetailPB.TabIndex = 6;
            this.cardDetailPB.TabStop = false;
            this.cardDetailPB.Paint += new System.Windows.Forms.PaintEventHandler(this.cardDetailPB_Paint);
            // 
            // awayField
            // 
            this.awayField.AllowDrop = true;
            this.awayField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(68)))));
            this.awayField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.awayField.Location = new System.Drawing.Point(138, 120);
            this.awayField.Margin = new System.Windows.Forms.Padding(0);
            this.awayField.Name = "awayField";
            this.awayField.Size = new System.Drawing.Size(704, 301);
            this.awayField.TabIndex = 7;
            this.awayField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1274, 26);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.デッキを読み込みToolStripMenuItem,
            this.ゲーム開始ToolStripMenuItem,
            this.ReadCardlistiniToolStripMenuItem,
            this.終了ToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.ファイルFToolStripMenuItem.Text = "ファイル";
            // 
            // ゲーム開始ToolStripMenuItem
            // 
            this.ゲーム開始ToolStripMenuItem.Name = "ゲーム開始ToolStripMenuItem";
            this.ゲーム開始ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.ゲーム開始ToolStripMenuItem.Text = "ゲーム開始";
            this.ゲーム開始ToolStripMenuItem.Click += new System.EventHandler(this.ゲーム開始ToolStripMenuItem_Click);
            // 
            // ReadCardlistiniToolStripMenuItem
            // 
            this.ReadCardlistiniToolStripMenuItem.Name = "ReadCardlistiniToolStripMenuItem";
            this.ReadCardlistiniToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.ReadCardlistiniToolStripMenuItem.Text = "cardlist.iniの再読込";
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            // 
            // CardMenuStrip1
            // 
            this.CardMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アクティブにするToolStripMenuItem,
            this.スリープにするToolStripMenuItem,
            this.表向きにするToolStripMenuItem,
            this.裏向きにするToolStripMenuItem});
            this.CardMenuStrip1.Name = "contextMenuStrip1";
            this.CardMenuStrip1.Size = new System.Drawing.Size(173, 92);
            // 
            // アクティブにするToolStripMenuItem
            // 
            this.アクティブにするToolStripMenuItem.Name = "アクティブにするToolStripMenuItem";
            this.アクティブにするToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.アクティブにするToolStripMenuItem.Text = "アクティブにする";
            this.アクティブにするToolStripMenuItem.Click += new System.EventHandler(this.アクティブにするToolStripMenuItem_Click);
            // 
            // スリープにするToolStripMenuItem
            // 
            this.スリープにするToolStripMenuItem.Name = "スリープにするToolStripMenuItem";
            this.スリープにするToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.スリープにするToolStripMenuItem.Text = "スリープにする";
            this.スリープにするToolStripMenuItem.Click += new System.EventHandler(this.スリープにするToolStripMenuItem_Click);
            // 
            // 表向きにするToolStripMenuItem
            // 
            this.表向きにするToolStripMenuItem.Name = "表向きにするToolStripMenuItem";
            this.表向きにするToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.表向きにするToolStripMenuItem.Text = "表向きにする";
            this.表向きにするToolStripMenuItem.Click += new System.EventHandler(this.表向きにするToolStripMenuItem_Click);
            // 
            // 裏向きにするToolStripMenuItem
            // 
            this.裏向きにするToolStripMenuItem.Name = "裏向きにするToolStripMenuItem";
            this.裏向きにするToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.裏向きにするToolStripMenuItem.Text = "裏向きにする";
            this.裏向きにするToolStripMenuItem.Click += new System.EventHandler(this.裏向きにするToolStripMenuItem_Click);
            // 
            // homeLibrary
            // 
            this.homeLibrary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.homeLibrary.ContextMenuStrip = this.LibraryMenuStrip2;
            this.homeLibrary.Location = new System.Drawing.Point(27, 20);
            this.homeLibrary.Name = "homeLibrary";
            this.homeLibrary.Size = new System.Drawing.Size(85, 120);
            this.homeLibrary.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeLibrary.TabIndex = 0;
            this.homeLibrary.TabStop = false;
            this.homeLibrary.Paint += new System.Windows.Forms.PaintEventHandler(this.homeLibrary_Paint);
            this.homeLibrary.DoubleClick += new System.EventHandler(this.homeLibrary_DoubleClick);
            this.homeLibrary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.homeLibrary_MouseDown);
            this.homeLibrary.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Card_MouseUp);
            // 
            // LibraryMenuStrip2
            // 
            this.LibraryMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.手札に加えるToolStripMenuItem,
            this.場に裏向きカードを1枚出すToolStripMenuItem,
            this.山札をシャッフルするToolStripMenuItem});
            this.LibraryMenuStrip2.Name = "contextMenuStrip2";
            this.LibraryMenuStrip2.Size = new System.Drawing.Size(221, 70);
            // 
            // 手札に加えるToolStripMenuItem
            // 
            this.手札に加えるToolStripMenuItem.Name = "手札に加えるToolStripMenuItem";
            this.手札に加えるToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.手札に加えるToolStripMenuItem.Text = "手札に加える";
            // 
            // 場に裏向きカードを1枚出すToolStripMenuItem
            // 
            this.場に裏向きカードを1枚出すToolStripMenuItem.Name = "場に裏向きカードを1枚出すToolStripMenuItem";
            this.場に裏向きカードを1枚出すToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.場に裏向きカードを1枚出すToolStripMenuItem.Text = "場に裏向きでカードを出す";
            this.場に裏向きカードを1枚出すToolStripMenuItem.Click += new System.EventHandler(this.場に裏向きカードを1枚出すToolStripMenuItem_Click);
            // 
            // 山札をシャッフルするToolStripMenuItem
            // 
            this.山札をシャッフルするToolStripMenuItem.Name = "山札をシャッフルするToolStripMenuItem";
            this.山札をシャッフルするToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.山札をシャッフルするToolStripMenuItem.Text = "山札をシャッフルする";
            // 
            // awayLibrary
            // 
            this.awayLibrary.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.awayLibrary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.awayLibrary.Location = new System.Drawing.Point(26, 275);
            this.awayLibrary.Name = "awayLibrary";
            this.awayLibrary.Size = new System.Drawing.Size(85, 120);
            this.awayLibrary.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.awayLibrary.TabIndex = 10;
            this.awayLibrary.TabStop = false;
            this.awayLibrary.Paint += new System.Windows.Forms.PaintEventHandler(this.awayLibrary_Paint);
            this.awayLibrary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.awayLibrary_MouseDown);
            this.awayLibrary.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Card_MouseUp);
            // 
            // homeNodeSleep
            // 
            this.homeNodeSleep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.homeNodeSleep.ContextMenuStrip = this.LibraryMenuStrip2;
            this.homeNodeSleep.Location = new System.Drawing.Point(8, 240);
            this.homeNodeSleep.Name = "homeNodeSleep";
            this.homeNodeSleep.Size = new System.Drawing.Size(108, 77);
            this.homeNodeSleep.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeNodeSleep.TabIndex = 2;
            this.homeNodeSleep.TabStop = false;
            this.homeNodeSleep.Paint += new System.Windows.Forms.PaintEventHandler(this.homeNodeSleep_Paint);
            this.homeNodeSleep.MouseDown += new System.Windows.Forms.MouseEventHandler(this.homeNodeSleep_MouseDown);
            this.homeNodeSleep.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Card_MouseUp);
            // 
            // homeNodeActive
            // 
            this.homeNodeActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.homeNodeActive.ContextMenuStrip = this.LibraryMenuStrip2;
            this.homeNodeActive.Location = new System.Drawing.Point(9, 113);
            this.homeNodeActive.Name = "homeNodeActive";
            this.homeNodeActive.Size = new System.Drawing.Size(77, 108);
            this.homeNodeActive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeNodeActive.TabIndex = 1;
            this.homeNodeActive.TabStop = false;
            this.homeNodeActive.Paint += new System.Windows.Forms.PaintEventHandler(this.homeNodeActive_Paint);
            this.homeNodeActive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.homeNodeActive_MouseDown);
            this.homeNodeActive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Card_MouseUp);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "冥界";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(10, 362);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 50);
            this.button6.TabIndex = 4;
            this.button6.Text = "ターン終了";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(27, 146);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "除外カード";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // homeHades
            // 
            this.homeHades.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.homeHades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.homeHades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.homeHades.Location = new System.Drawing.Point(27, 192);
            this.homeHades.Name = "homeHades";
            this.homeHades.Size = new System.Drawing.Size(85, 120);
            this.homeHades.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeHades.TabIndex = 2;
            this.homeHades.TabStop = false;
            this.homeHades.Paint += new System.Windows.Forms.PaintEventHandler(this.homeHades_Paint);
            this.homeHades.DoubleClick += new System.EventHandler(this.homeHades_DoubleClick);
            this.homeHades.MouseDown += new System.Windows.Forms.MouseEventHandler(this.homeHades_MouseDown);
            this.homeHades.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Card_MouseUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "山札";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Location = new System.Drawing.Point(7, 597);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(280, 210);
            this.textBox1.TabIndex = 13;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // awayHand
            // 
            this.awayHand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.awayHand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.awayHand.Location = new System.Drawing.Point(138, 0);
            this.awayHand.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.awayHand.Name = "awayHand";
            this.awayHand.Size = new System.Drawing.Size(704, 119);
            this.awayHand.TabIndex = 14;
            this.awayHand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(10)))), ((int)(((byte)(8)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Controls.Add(this.awayHand, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.awayField, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.homeHand, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.homeField, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 2, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(291, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(982, 844);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panel5.Controls.Add(this.awayHades);
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.awayLibrary);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.panel5.Name = "panel5";
            this.tableLayoutPanel1.SetRowSpan(this.panel5, 2);
            this.panel5.Size = new System.Drawing.Size(137, 421);
            this.panel5.TabIndex = 16;
            this.panel5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // awayHades
            // 
            this.awayHades.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.awayHades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.awayHades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.awayHades.Location = new System.Drawing.Point(26, 103);
            this.awayHades.Name = "awayHades";
            this.awayHades.Size = new System.Drawing.Size(85, 120);
            this.awayHades.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.awayHades.TabIndex = 14;
            this.awayHades.TabStop = false;
            this.awayHades.Paint += new System.Windows.Forms.PaintEventHandler(this.awayHades_Paint);
            this.awayHades.MouseDown += new System.Windows.Forms.MouseEventHandler(this.awayHades_MouseDown);
            this.awayHades.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Card_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(27, 246);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 23);
            this.button3.TabIndex = 12;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Paint += new System.Windows.Forms.PaintEventHandler(this.button3_Paint);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.awayNodeSleep);
            this.panel8.Controls.Add(this.awayNodeActive);
            this.panel8.Controls.Add(this.awayLifePicture);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(843, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.panel8.Name = "panel8";
            this.tableLayoutPanel1.SetRowSpan(this.panel8, 2);
            this.panel8.Size = new System.Drawing.Size(139, 421);
            this.panel8.TabIndex = 19;
            this.panel8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(6, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 10;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // awayNodeSleep
            // 
            this.awayNodeSleep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.awayNodeSleep.ContextMenuStrip = this.LibraryMenuStrip2;
            this.awayNodeSleep.Location = new System.Drawing.Point(20, 73);
            this.awayNodeSleep.Name = "awayNodeSleep";
            this.awayNodeSleep.Size = new System.Drawing.Size(108, 77);
            this.awayNodeSleep.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.awayNodeSleep.TabIndex = 9;
            this.awayNodeSleep.TabStop = false;
            this.awayNodeSleep.Paint += new System.Windows.Forms.PaintEventHandler(this.awayNodeSleep_Paint);
            // 
            // awayNodeActive
            // 
            this.awayNodeActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.awayNodeActive.ContextMenuStrip = this.LibraryMenuStrip2;
            this.awayNodeActive.Location = new System.Drawing.Point(51, 161);
            this.awayNodeActive.Name = "awayNodeActive";
            this.awayNodeActive.Size = new System.Drawing.Size(77, 108);
            this.awayNodeActive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.awayNodeActive.TabIndex = 8;
            this.awayNodeActive.TabStop = false;
            this.awayNodeActive.Paint += new System.Windows.Forms.PaintEventHandler(this.awayNodeActive_Paint);
            // 
            // awayLifePicture
            // 
            this.awayLifePicture.BackColor = System.Drawing.Color.DarkSlateGray;
            this.awayLifePicture.Cursor = System.Windows.Forms.Cursors.Default;
            this.awayLifePicture.Location = new System.Drawing.Point(6, 346);
            this.awayLifePicture.Name = "awayLifePicture";
            this.awayLifePicture.Size = new System.Drawing.Size(128, 64);
            this.awayLifePicture.TabIndex = 7;
            this.awayLifePicture.TabStop = false;
            this.awayLifePicture.Paint += new System.Windows.Forms.PaintEventHandler(this.awayLifePicture_Paint);
            // 
            // homeHand
            // 
            this.homeHand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.homeHand.Controls.Add(this.label10);
            this.homeHand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeHand.Location = new System.Drawing.Point(138, 724);
            this.homeHand.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.homeHand.Name = "homeHand";
            this.homeHand.Size = new System.Drawing.Size(704, 120);
            this.homeHand.TabIndex = 15;
            this.homeHand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(657, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "0";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // homeField
            // 
            this.homeField.AllowDrop = true;
            this.homeField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(68)))));
            this.homeField.ContextMenuStrip = this.HomeFieldMenuStrip;
            this.homeField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeField.ForeColor = System.Drawing.SystemColors.ControlText;
            this.homeField.Location = new System.Drawing.Point(138, 422);
            this.homeField.Margin = new System.Windows.Forms.Padding(0);
            this.homeField.Name = "homeField";
            this.homeField.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.homeField.Size = new System.Drawing.Size(704, 301);
            this.homeField.TabIndex = 20;
            this.homeField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // HomeFieldMenuStrip
            // 
            this.HomeFieldMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全てアクティブにToolStripMenuItem});
            this.HomeFieldMenuStrip.Name = "HomeFieldMenuStrip";
            this.HomeFieldMenuStrip.Size = new System.Drawing.Size(173, 26);
            // 
            // 全てアクティブにToolStripMenuItem
            // 
            this.全てアクティブにToolStripMenuItem.Name = "全てアクティブにToolStripMenuItem";
            this.全てアクティブにToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.全てアクティブにToolStripMenuItem.Text = "全てアクティブに";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panel6.Controls.Add(this.homeNodeSleep);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.homeNodeActive);
            this.panel6.Controls.Add(this.homeLife);
            this.panel6.Controls.Add(this.homeLifePicture);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 422);
            this.panel6.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.panel6.Name = "panel6";
            this.tableLayoutPanel1.SetRowSpan(this.panel6, 2);
            this.panel6.Size = new System.Drawing.Size(137, 422);
            this.panel6.TabIndex = 17;
            this.panel6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(4, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Player1";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // homeLife
            // 
            this.homeLife.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.homeLife.Location = new System.Drawing.Point(85, 51);
            this.homeLife.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.homeLife.Name = "homeLife";
            this.homeLife.Size = new System.Drawing.Size(47, 20);
            this.homeLife.TabIndex = 5;
            this.homeLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.homeLife.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.homeLife.ValueChanged += new System.EventHandler(this.homeLife_ValueChanged);
            // 
            // homeLifePicture
            // 
            this.homeLifePicture.BackColor = System.Drawing.Color.DarkSlateGray;
            this.homeLifePicture.Location = new System.Drawing.Point(4, 7);
            this.homeLifePicture.Name = "homeLifePicture";
            this.homeLifePicture.Size = new System.Drawing.Size(128, 64);
            this.homeLifePicture.TabIndex = 8;
            this.homeLifePicture.TabStop = false;
            this.homeLifePicture.Paint += new System.Windows.Forms.PaintEventHandler(this.homeLifePicture_Paint);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.homeHades);
            this.panel7.Controls.Add(this.button6);
            this.panel7.Controls.Add(this.homeLibrary);
            this.panel7.Controls.Add(this.button5);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(843, 422);
            this.panel7.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.panel7.Name = "panel7";
            this.tableLayoutPanel1.SetRowSpan(this.panel7, 2);
            this.panel7.Size = new System.Drawing.Size(139, 422);
            this.panel7.TabIndex = 18;
            this.panel7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            // 
            // cardDetailAbility
            // 
            this.cardDetailAbility.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cardDetailAbility.Location = new System.Drawing.Point(17, 326);
            this.cardDetailAbility.Multiline = true;
            this.cardDetailAbility.Name = "cardDetailAbility";
            this.cardDetailAbility.ReadOnly = true;
            this.cardDetailAbility.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cardDetailAbility.Size = new System.Drawing.Size(260, 62);
            this.cardDetailAbility.TabIndex = 17;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox2.Location = new System.Drawing.Point(8, 436);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(279, 155);
            this.textBox2.TabIndex = 18;
            // 
            // cardDetailAtkDef
            // 
            this.cardDetailAtkDef.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cardDetailAtkDef.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cardDetailAtkDef.Location = new System.Drawing.Point(205, 388);
            this.cardDetailAtkDef.Name = "cardDetailAtkDef";
            this.cardDetailAtkDef.Size = new System.Drawing.Size(72, 19);
            this.cardDetailAtkDef.TabIndex = 19;
            this.cardDetailAtkDef.Text = "/";
            this.cardDetailAtkDef.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox3.Location = new System.Drawing.Point(7, 813);
            this.textBox3.MaxLength = 10000;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(209, 20);
            this.textBox3.TabIndex = 20;
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
            // 
            // デッキを読み込みToolStripMenuItem
            // 
            this.デッキを読み込みToolStripMenuItem.Name = "デッキを読み込みToolStripMenuItem";
            this.デッキを読み込みToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.デッキを読み込みToolStripMenuItem.Text = "デッキの読込";
            this.デッキを読み込みToolStripMenuItem.Click += new System.EventHandler(this.デッキを読み込みToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 872);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cardDetailAtkDef);
            this.Controls.Add(this.cardDetailAbility);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Chat_SubmitBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cardDetailPB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "東方NETVISION VC#2010";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cardDetailPB)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.CardMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.homeLibrary)).EndInit();
            this.LibraryMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.awayLibrary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeNodeSleep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeNodeActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeHades)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.awayHades)).EndInit();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.awayNodeSleep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.awayNodeActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.awayLifePicture)).EndInit();
            this.homeHand.ResumeLayout(false);
            this.HomeFieldMenuStrip.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.homeLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeLifePicture)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Chat_SubmitBtn;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox cardDetailPB;
        private System.Windows.Forms.Panel awayField;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip CardMenuStrip1;
        private System.Windows.Forms.PictureBox homeLibrary;
        private System.Windows.Forms.PictureBox awayLibrary;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel awayHand;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel homeHand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox homeHades;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReadCardlistiniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ToolStripMenuItem スリープにするToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem アクティブにするToolStripMenuItem;
        private System.Windows.Forms.Panel homeField;
        private System.Windows.Forms.ToolStripMenuItem ゲーム開始ToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown homeLife;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem 裏向きにするToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 表向きにするToolStripMenuItem;
        private System.Windows.Forms.TextBox cardDetailAbility;
        private System.Windows.Forms.ContextMenuStrip LibraryMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 場に裏向きカードを1枚出すToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手札に加えるToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 山札をシャッフルするToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label cardDetailAtkDef;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox awayHades;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox awayLifePicture;
        private System.Windows.Forms.PictureBox homeLifePicture;
        private System.Windows.Forms.PictureBox homeNodeSleep;
        private System.Windows.Forms.PictureBox homeNodeActive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip HomeFieldMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 全てアクティブにToolStripMenuItem;
        private System.Windows.Forms.PictureBox awayNodeSleep;
        private System.Windows.Forms.PictureBox awayNodeActive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem デッキを読み込みToolStripMenuItem;
    }
}

