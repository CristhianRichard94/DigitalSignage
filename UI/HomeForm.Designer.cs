namespace DigitalSignage.UI
{
    partial class HomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.operativeScreen = new System.Windows.Forms.Button();
            this.dropdownPanel = new System.Windows.Forms.Panel();
            this.rssManage = new System.Windows.Forms.Button();
            this.bannersManage = new System.Windows.Forms.Button();
            this.campManage = new System.Windows.Forms.Button();
            this.manageOptions = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.logoPanel = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dropdownPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPanel)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(363, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Bienvenido";
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.exit.Cursor = System.Windows.Forms.Cursors.Default;
            this.exit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.exit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exit.FlatAppearance.BorderSize = 0;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.exit.Location = new System.Drawing.Point(0, 100);
            this.exit.Margin = new System.Windows.Forms.Padding(0);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(266, 50);
            this.exit.TabIndex = 89;
            this.exit.TabStop = false;
            this.exit.Text = "Salir";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // operativeScreen
            // 
            this.operativeScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.operativeScreen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operativeScreen.FlatAppearance.BorderSize = 0;
            this.operativeScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.operativeScreen.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.operativeScreen.Location = new System.Drawing.Point(0, 50);
            this.operativeScreen.Margin = new System.Windows.Forms.Padding(0);
            this.operativeScreen.Name = "operativeScreen";
            this.operativeScreen.Size = new System.Drawing.Size(266, 50);
            this.operativeScreen.TabIndex = 4;
            this.operativeScreen.Text = "Vista Operativa";
            this.operativeScreen.UseVisualStyleBackColor = false;
            this.operativeScreen.Click += new System.EventHandler(this.operativeScreen_Click);
            // 
            // dropdownPanel
            // 
            this.dropdownPanel.Controls.Add(this.rssManage);
            this.dropdownPanel.Controls.Add(this.bannersManage);
            this.dropdownPanel.Controls.Add(this.campManage);
            this.dropdownPanel.Controls.Add(this.manageOptions);
            this.dropdownPanel.Location = new System.Drawing.Point(0, 0);
            this.dropdownPanel.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.dropdownPanel.MaximumSize = new System.Drawing.Size(266, 145);
            this.dropdownPanel.MinimumSize = new System.Drawing.Size(266, 50);
            this.dropdownPanel.Name = "dropdownPanel";
            this.dropdownPanel.Size = new System.Drawing.Size(266, 50);
            this.dropdownPanel.TabIndex = 90;
            // 
            // rssManage
            // 
            this.rssManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.rssManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.rssManage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rssManage.FlatAppearance.BorderSize = 0;
            this.rssManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rssManage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rssManage.Location = new System.Drawing.Point(0, 114);
            this.rssManage.Margin = new System.Windows.Forms.Padding(0);
            this.rssManage.Name = "rssManage";
            this.rssManage.Size = new System.Drawing.Size(266, 32);
            this.rssManage.TabIndex = 8;
            this.rssManage.Text = "Fuentes RSS";
            this.rssManage.UseVisualStyleBackColor = false;
            this.rssManage.Click += new System.EventHandler(this.rssManage_Click);
            // 
            // bannersManage
            // 
            this.bannersManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.bannersManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.bannersManage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bannersManage.FlatAppearance.BorderSize = 0;
            this.bannersManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bannersManage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.bannersManage.Location = new System.Drawing.Point(0, 82);
            this.bannersManage.Margin = new System.Windows.Forms.Padding(0);
            this.bannersManage.Name = "bannersManage";
            this.bannersManage.Size = new System.Drawing.Size(266, 32);
            this.bannersManage.TabIndex = 7;
            this.bannersManage.Text = "Banners";
            this.bannersManage.UseVisualStyleBackColor = false;
            this.bannersManage.Click += new System.EventHandler(this.bannersManage_Click);
            // 
            // campManage
            // 
            this.campManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.campManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.campManage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.campManage.FlatAppearance.BorderSize = 0;
            this.campManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.campManage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.campManage.Location = new System.Drawing.Point(0, 50);
            this.campManage.Margin = new System.Windows.Forms.Padding(0);
            this.campManage.Name = "campManage";
            this.campManage.Size = new System.Drawing.Size(266, 32);
            this.campManage.TabIndex = 6;
            this.campManage.Text = "Campañas";
            this.campManage.UseVisualStyleBackColor = false;
            this.campManage.Click += new System.EventHandler(this.campManage_Click);
            // 
            // manageOptions
            // 
            this.manageOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.manageOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.manageOptions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.manageOptions.FlatAppearance.BorderSize = 0;
            this.manageOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.manageOptions.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.manageOptions.Image = global::DigitalSignage.UI.Properties.Resources.dropdownarrow1;
            this.manageOptions.Location = new System.Drawing.Point(0, 0);
            this.manageOptions.Margin = new System.Windows.Forms.Padding(0);
            this.manageOptions.Name = "manageOptions";
            this.manageOptions.Size = new System.Drawing.Size(266, 50);
            this.manageOptions.TabIndex = 5;
            this.manageOptions.Text = "Administración";
            this.manageOptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.manageOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.manageOptions.UseVisualStyleBackColor = false;
            this.manageOptions.Click += new System.EventHandler(this.manageOptions_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(43)))), ((int)(((byte)(64)))));
            this.flowLayoutPanel1.Controls.Add(this.dropdownPanel);
            this.flowLayoutPanel1.Controls.Add(this.operativeScreen);
            this.flowLayoutPanel1.Controls.Add(this.exit);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 49);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(266, 700);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1366, 49);
            this.panel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.panel2.Controls.Add(this.logoPanel);
            this.panel2.Controls.Add(this.title);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(266, 49);
            this.panel2.TabIndex = 1;
            // 
            // logoPanel
            // 
            this.logoPanel.BackColor = System.Drawing.Color.Transparent;
            this.logoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.logoPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.logoPanel.Image = ((System.Drawing.Image)(resources.GetObject("logoPanel.Image")));
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Padding = new System.Windows.Forms.Padding(3);
            this.logoPanel.Size = new System.Drawing.Size(61, 49);
            this.logoPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPanel.TabIndex = 3;
            this.logoPanel.TabStop = false;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.title.Location = new System.Drawing.Point(64, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(172, 25);
            this.title.TabIndex = 0;
            this.title.Text = "Digital Signage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SeaShell;
            this.label2.Location = new System.Drawing.Point(85, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(703, 59);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sistema de cartelería digital";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ink Free", 89.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.label3.Location = new System.Drawing.Point(21, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(848, 148);
            this.label3.TabIndex = 4;
            this.label3.Text = "Digital Signage";
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = global::DigitalSignage.UI.Properties.Resources.NY_Signage__1_;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(367, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(895, 486);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // HomeForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1366, 749);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Digital Signage ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.dropdownPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPanel)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button operativeScreen;
        private System.Windows.Forms.Panel dropdownPanel;
        private System.Windows.Forms.Button rssManage;
        private System.Windows.Forms.Button bannersManage;
        private System.Windows.Forms.Button campManage;
        private System.Windows.Forms.Button manageOptions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox logoPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}