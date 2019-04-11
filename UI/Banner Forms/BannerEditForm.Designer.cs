namespace DigitalSignage.UI.Banner_Forms
{
    partial class BannerEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BannerEditForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.endMinComboBox = new System.Windows.Forms.ComboBox();
            this.initMinComboBox = new System.Windows.Forms.ComboBox();
            this.endHourComboBox = new System.Windows.Forms.ComboBox();
            this.initHourComboBox = new System.Windows.Forms.ComboBox();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.initDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.initTimeLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.initDateLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.idValueLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.descTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textSourceTextBox = new System.Windows.Forms.TextBox();
            this.selectRSSButton = new System.Windows.Forms.Button();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rSSSourceLabel = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.cancelButton.CausesValidation = false;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(710, 482);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(125, 37);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(846, 49);
            this.panel3.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(336, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 23);
            this.label2.TabIndex = 35;
            this.label2.Text = "Carga de Banner";
            this.label2.Visible = false;
            // 
            // endMinComboBox
            // 
            this.endMinComboBox.FormattingEnabled = true;
            this.endMinComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.endMinComboBox.Location = new System.Drawing.Point(760, 262);
            this.endMinComboBox.Name = "endMinComboBox";
            this.endMinComboBox.Size = new System.Drawing.Size(52, 21);
            this.endMinComboBox.TabIndex = 45;
            this.endMinComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.endMinComboBox_Validating);
            // 
            // initMinComboBox
            // 
            this.initMinComboBox.FormattingEnabled = true;
            this.initMinComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.initMinComboBox.Location = new System.Drawing.Point(535, 262);
            this.initMinComboBox.Name = "initMinComboBox";
            this.initMinComboBox.Size = new System.Drawing.Size(52, 21);
            this.initMinComboBox.TabIndex = 43;
            this.initMinComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.initMinComboBox_Validating);
            // 
            // endHourComboBox
            // 
            this.endHourComboBox.FormattingEnabled = true;
            this.endHourComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.endHourComboBox.Location = new System.Drawing.Point(679, 262);
            this.endHourComboBox.Name = "endHourComboBox";
            this.endHourComboBox.Size = new System.Drawing.Size(52, 21);
            this.endHourComboBox.TabIndex = 44;
            this.endHourComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.EndHourComboBox_Validating);
            // 
            // initHourComboBox
            // 
            this.initHourComboBox.FormattingEnabled = true;
            this.initHourComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.initHourComboBox.Location = new System.Drawing.Point(449, 262);
            this.initHourComboBox.Name = "initHourComboBox";
            this.initHourComboBox.Size = new System.Drawing.Size(52, 21);
            this.initHourComboBox.TabIndex = 42;
            this.initHourComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.initHourComboBox_Validating);
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.CustomFormat = "";
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDateTimePicker.Location = new System.Drawing.Point(696, 168);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(116, 21);
            this.endDateTimePicker.TabIndex = 41;
            this.endDateTimePicker.Value = new System.DateTime(2019, 3, 12, 0, 15, 31, 0);
            this.endDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.endDateTimePicker_Validating);
            // 
            // initDateTimePicker
            // 
            this.initDateTimePicker.CustomFormat = "";
            this.initDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.initDateTimePicker.Location = new System.Drawing.Point(449, 168);
            this.initDateTimePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.initDateTimePicker.Name = "initDateTimePicker";
            this.initDateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.initDateTimePicker.Size = new System.Drawing.Size(116, 21);
            this.initDateTimePicker.TabIndex = 40;
            this.initDateTimePicker.Value = new System.DateTime(2019, 3, 11, 0, 14, 0, 0);
            this.initDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.initDateTimePicker_Validating);
            // 
            // endTimeLabel
            // 
            this.endTimeLabel.AutoSize = true;
            this.endTimeLabel.Location = new System.Drawing.Point(675, 231);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(75, 13);
            this.endTimeLabel.TabIndex = 39;
            this.endTimeLabel.Text = "Hora de fin:";
            // 
            // initTimeLabel
            // 
            this.initTimeLabel.AutoSize = true;
            this.initTimeLabel.Location = new System.Drawing.Point(446, 231);
            this.initTimeLabel.Name = "initTimeLabel";
            this.initTimeLabel.Size = new System.Drawing.Size(90, 13);
            this.initTimeLabel.TabIndex = 38;
            this.initTimeLabel.Text = "Hora de inicio:";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(699, 131);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(81, 13);
            this.endDateLabel.TabIndex = 37;
            this.endDateLabel.Text = "Fecha de fin:";
            // 
            // initDateLabel
            // 
            this.initDateLabel.AutoSize = true;
            this.initDateLabel.Location = new System.Drawing.Point(446, 131);
            this.initDateLabel.Name = "initDateLabel";
            this.initDateLabel.Size = new System.Drawing.Size(96, 13);
            this.initDateLabel.TabIndex = 36;
            this.initDateLabel.Text = "Fecha de inicio:";
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(48, 209);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(78, 13);
            this.descLabel.TabIndex = 35;
            this.descLabel.Text = "Descripción:";
            // 
            // idValueLabel
            // 
            this.idValueLabel.AutoSize = true;
            this.idValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idValueLabel.Location = new System.Drawing.Point(356, 65);
            this.idValueLabel.Name = "idValueLabel";
            this.idValueLabel.Size = new System.Drawing.Size(20, 24);
            this.idValueLabel.TabIndex = 34;
            this.idValueLabel.Text = "0";
            this.idValueLabel.Visible = false;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(47, 65);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(32, 24);
            this.idLabel.TabIndex = 33;
            this.idLabel.Text = "ID:";
            this.idLabel.Visible = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(48, 131);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(57, 13);
            this.nameLabel.TabIndex = 32;
            this.nameLabel.Text = "Nombre:";
            // 
            // descTextBox
            // 
            this.descTextBox.Location = new System.Drawing.Point(51, 231);
            this.descTextBox.Multiline = true;
            this.descTextBox.Name = "descTextBox";
            this.descTextBox.Size = new System.Drawing.Size(327, 102);
            this.descTextBox.TabIndex = 31;
            this.descTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.descTextBox_Validating);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(181, 131);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(198, 21);
            this.nameTextBox.TabIndex = 30;
            this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(446, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Tipo de fuente:";
            // 
            // textSourceTextBox
            // 
            this.textSourceTextBox.Location = new System.Drawing.Point(449, 377);
            this.textSourceTextBox.Multiline = true;
            this.textSourceTextBox.Name = "textSourceTextBox";
            this.textSourceTextBox.Size = new System.Drawing.Size(363, 80);
            this.textSourceTextBox.TabIndex = 47;
            this.textSourceTextBox.Visible = false;
            this.textSourceTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.textSourceTextBox_Validating);
            // 
            // selectRSSButton
            // 
            this.selectRSSButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.selectRSSButton.FlatAppearance.BorderSize = 0;
            this.selectRSSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectRSSButton.Location = new System.Drawing.Point(678, 350);
            this.selectRSSButton.Margin = new System.Windows.Forms.Padding(0);
            this.selectRSSButton.Name = "selectRSSButton";
            this.selectRSSButton.Size = new System.Drawing.Size(84, 25);
            this.selectRSSButton.TabIndex = 48;
            this.selectRSSButton.Text = "Seleccionar";
            this.selectRSSButton.UseVisualStyleBackColor = false;
            this.selectRSSButton.Click += new System.EventHandler(this.selectRSSButton_Click);
            this.selectRSSButton.Validating += new System.ComponentModel.CancelEventHandler(this.selectRSSButton_Validating);
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Location = new System.Drawing.Point(559, 353);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(105, 21);
            this.sourceComboBox.TabIndex = 49;
            this.sourceComboBox.SelectedIndexChanged += new System.EventHandler(this.sourceComboBox_SelectedIndexChanged);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(559, 482);
            this.saveButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(125, 37);
            this.saveButton.TabIndex = 50;
            this.saveButton.Text = "Guardar";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::DigitalSignage.UI.Properties.Resources._checked;
            this.pictureBox1.Location = new System.Drawing.Point(777, 353);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 51;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // rSSSourceLabel
            // 
            this.rSSSourceLabel.AutoSize = true;
            this.rSSSourceLabel.Font = new System.Drawing.Font("Verdana", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rSSSourceLabel.Location = new System.Drawing.Point(449, 412);
            this.rSSSourceLabel.Name = "rSSSourceLabel";
            this.rSSSourceLabel.Size = new System.Drawing.Size(0, 17);
            this.rSSSourceLabel.TabIndex = 52;
            this.rSSSourceLabel.Visible = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // BannerEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(846, 528);
            this.Controls.Add(this.rSSSourceLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.sourceComboBox);
            this.Controls.Add(this.selectRSSButton);
            this.Controls.Add(this.textSourceTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endMinComboBox);
            this.Controls.Add(this.initMinComboBox);
            this.Controls.Add(this.endHourComboBox);
            this.Controls.Add(this.initHourComboBox);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.initDateTimePicker);
            this.Controls.Add(this.endTimeLabel);
            this.Controls.Add(this.initTimeLabel);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.initDateLabel);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.idValueLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.descTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BannerEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BannerEditForm";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox endMinComboBox;
        private System.Windows.Forms.ComboBox initMinComboBox;
        private System.Windows.Forms.ComboBox endHourComboBox;
        private System.Windows.Forms.ComboBox initHourComboBox;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.DateTimePicker initDateTimePicker;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.Label initTimeLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.Label initDateLabel;
        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.Label idValueLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox descTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSourceTextBox;
        private System.Windows.Forms.Button selectRSSButton;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label rSSSourceLabel;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}