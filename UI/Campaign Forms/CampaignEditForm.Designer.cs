﻿namespace DigitalSignage.UI.Campaign_Forms
{
    partial class CampaignEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampaignEditForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.logoPanel = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.descTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.idValueLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.initDateLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.initTimeLabel = new System.Windows.Forms.Label();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.initDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.initHourComboBox = new System.Windows.Forms.ComboBox();
            this.endHourComboBox = new System.Windows.Forms.ComboBox();
            this.initMinComboBox = new System.Windows.Forms.ComboBox();
            this.endMinComboBox = new System.Windows.Forms.ComboBox();
            this.imgGridView = new System.Windows.Forms.DataGridView();
            this.Data = new System.Windows.Forms.DataGridViewImageColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addImageButton = new System.Windows.Forms.Button();
            this.editImageButton = new System.Windows.Forms.Button();
            this.deleteImageButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPanel)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(0, 99);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(310, 50);
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
            this.panel3.Location = new System.Drawing.Point(310, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1060, 49);
            this.panel3.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(443, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 23);
            this.label2.TabIndex = 35;
            this.label2.Text = "Carga de Campaña";
            this.label2.Visible = false;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.title.Location = new System.Drawing.Point(75, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(172, 25);
            this.title.TabIndex = 0;
            this.title.Text = "Digital Signage";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.panel2.Controls.Add(this.logoPanel);
            this.panel2.Controls.Add(this.title);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(310, 49);
            this.panel2.TabIndex = 4;
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
            this.logoPanel.Size = new System.Drawing.Size(71, 49);
            this.logoPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPanel.TabIndex = 3;
            this.logoPanel.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(43)))), ((int)(((byte)(64)))));
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.saveButton);
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(310, 749);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(0, 49);
            this.saveButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(310, 50);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Guardar";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(519, 176);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(198, 21);
            this.nameTextBox.TabIndex = 9;
            this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
            // 
            // descTextBox
            // 
            this.descTextBox.Location = new System.Drawing.Point(390, 296);
            this.descTextBox.Multiline = true;
            this.descTextBox.Name = "descTextBox";
            this.descTextBox.Size = new System.Drawing.Size(327, 102);
            this.descTextBox.TabIndex = 11;
            this.descTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.descTextBox_Validating);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(386, 176);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(57, 13);
            this.nameLabel.TabIndex = 13;
            this.nameLabel.Text = "Nombre:";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(385, 110);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(32, 24);
            this.idLabel.TabIndex = 14;
            this.idLabel.Text = "ID:";
            this.idLabel.Visible = false;
            // 
            // idValueLabel
            // 
            this.idValueLabel.AutoSize = true;
            this.idValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idValueLabel.Location = new System.Drawing.Point(694, 110);
            this.idValueLabel.Name = "idValueLabel";
            this.idValueLabel.Size = new System.Drawing.Size(20, 24);
            this.idValueLabel.TabIndex = 15;
            this.idValueLabel.Text = "0";
            this.idValueLabel.Visible = false;
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(386, 254);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(78, 13);
            this.descLabel.TabIndex = 16;
            this.descLabel.Text = "Descripción:";
            // 
            // initDateLabel
            // 
            this.initDateLabel.AutoSize = true;
            this.initDateLabel.Location = new System.Drawing.Point(386, 442);
            this.initDateLabel.Name = "initDateLabel";
            this.initDateLabel.Size = new System.Drawing.Size(96, 13);
            this.initDateLabel.TabIndex = 17;
            this.initDateLabel.Text = "Fecha de inicio:";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(603, 442);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(81, 13);
            this.endDateLabel.TabIndex = 18;
            this.endDateLabel.Text = "Fecha de fin:";
            // 
            // initTimeLabel
            // 
            this.initTimeLabel.AutoSize = true;
            this.initTimeLabel.Location = new System.Drawing.Point(386, 542);
            this.initTimeLabel.Name = "initTimeLabel";
            this.initTimeLabel.Size = new System.Drawing.Size(90, 13);
            this.initTimeLabel.TabIndex = 19;
            this.initTimeLabel.Text = "Hora de inicio:";
            // 
            // endTimeLabel
            // 
            this.endTimeLabel.AutoSize = true;
            this.endTimeLabel.Location = new System.Drawing.Point(580, 542);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(75, 13);
            this.endTimeLabel.TabIndex = 20;
            this.endTimeLabel.Text = "Hora de fin:";
            // 
            // initDateTimePicker
            // 
            this.initDateTimePicker.CustomFormat = "";
            this.initDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.initDateTimePicker.Location = new System.Drawing.Point(390, 479);
            this.initDateTimePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.initDateTimePicker.Name = "initDateTimePicker";
            this.initDateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.initDateTimePicker.Size = new System.Drawing.Size(116, 21);
            this.initDateTimePicker.TabIndex = 23;
            this.initDateTimePicker.Value = new System.DateTime(2019, 3, 12, 0, 14, 26, 0);
            this.initDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.initDateTimePicker_Validating);
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.CustomFormat = "";
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDateTimePicker.Location = new System.Drawing.Point(601, 479);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(116, 21);
            this.endDateTimePicker.TabIndex = 24;
            this.endDateTimePicker.Value = new System.DateTime(2019, 3, 13, 0, 15, 0, 0);
            this.endDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.endDateTimePicker_Validating);
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
            this.initHourComboBox.Location = new System.Drawing.Point(390, 573);
            this.initHourComboBox.Name = "initHourComboBox";
            this.initHourComboBox.Size = new System.Drawing.Size(52, 21);
            this.initHourComboBox.TabIndex = 26;
            this.initHourComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.initHourComboBox_Validating);
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
            this.endHourComboBox.Location = new System.Drawing.Point(583, 573);
            this.endHourComboBox.Name = "endHourComboBox";
            this.endHourComboBox.Size = new System.Drawing.Size(52, 21);
            this.endHourComboBox.TabIndex = 28;
            this.endHourComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.EndHourComboBox_Validating);
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
            this.initMinComboBox.Location = new System.Drawing.Point(476, 573);
            this.initMinComboBox.Name = "initMinComboBox";
            this.initMinComboBox.Size = new System.Drawing.Size(52, 21);
            this.initMinComboBox.TabIndex = 27;
            this.initMinComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.initMinComboBox_Validating);
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
            this.endMinComboBox.Location = new System.Drawing.Point(665, 573);
            this.endMinComboBox.Name = "endMinComboBox";
            this.endMinComboBox.Size = new System.Drawing.Size(52, 21);
            this.endMinComboBox.TabIndex = 29;
            this.endMinComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.endMinComboBox_Validating);
            // 
            // imgGridView
            // 
            this.imgGridView.AllowUserToAddRows = false;
            this.imgGridView.AllowUserToDeleteRows = false;
            this.imgGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.imgGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imgGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data,
            this.Description,
            this.Duration,
            this.Position});
            this.imgGridView.GridColor = System.Drawing.Color.Silver;
            this.imgGridView.Location = new System.Drawing.Point(766, 99);
            this.imgGridView.Name = "imgGridView";
            this.imgGridView.RowHeadersVisible = false;
            this.imgGridView.RowTemplate.Height = 100;
            this.imgGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.imgGridView.Size = new System.Drawing.Size(592, 535);
            this.imgGridView.TabIndex = 30;
            this.imgGridView.Validating += new System.ComponentModel.CancelEventHandler(this.imgGridView_Validating);
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Data.DataPropertyName = "Data";
            dataGridViewCellStyle1.NullValue = "null";
            this.Data.DefaultCellStyle = dataGridViewCellStyle1;
            this.Data.HeaderText = "Imagen";
            this.Data.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Data.MinimumWidth = 170;
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Width = 170;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Descripción";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Duration
            // 
            this.Duration.DataPropertyName = "Duration";
            this.Duration.HeaderText = "Duración";
            this.Duration.Name = "Duration";
            this.Duration.ReadOnly = true;
            // 
            // Position
            // 
            this.Position.DataPropertyName = "Position";
            this.Position.HeaderText = "Posición";
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            // 
            // addImageButton
            // 
            this.addImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.addImageButton.FlatAppearance.BorderSize = 0;
            this.addImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addImageButton.Location = new System.Drawing.Point(766, 668);
            this.addImageButton.Margin = new System.Windows.Forms.Padding(0);
            this.addImageButton.Name = "addImageButton";
            this.addImageButton.Size = new System.Drawing.Size(145, 35);
            this.addImageButton.TabIndex = 6;
            this.addImageButton.Text = "Agregar Imagen";
            this.addImageButton.UseVisualStyleBackColor = false;
            this.addImageButton.Click += new System.EventHandler(this.addImageButton_Click);
            // 
            // editImageButton
            // 
            this.editImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.editImageButton.FlatAppearance.BorderSize = 0;
            this.editImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editImageButton.Location = new System.Drawing.Point(990, 668);
            this.editImageButton.Margin = new System.Windows.Forms.Padding(0);
            this.editImageButton.Name = "editImageButton";
            this.editImageButton.Size = new System.Drawing.Size(145, 35);
            this.editImageButton.TabIndex = 31;
            this.editImageButton.Text = "Editar Imagen";
            this.editImageButton.UseVisualStyleBackColor = false;
            this.editImageButton.Click += new System.EventHandler(this.editImageButton_Click);
            // 
            // deleteImageButton
            // 
            this.deleteImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(60)))), ((int)(((byte)(79)))));
            this.deleteImageButton.FlatAppearance.BorderSize = 0;
            this.deleteImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteImageButton.Location = new System.Drawing.Point(1213, 668);
            this.deleteImageButton.Margin = new System.Windows.Forms.Padding(0);
            this.deleteImageButton.Name = "deleteImageButton";
            this.deleteImageButton.Size = new System.Drawing.Size(145, 35);
            this.deleteImageButton.TabIndex = 32;
            this.deleteImageButton.Text = "Eliminar Imagen";
            this.deleteImageButton.UseVisualStyleBackColor = false;
            this.deleteImageButton.Click += new System.EventHandler(this.deleteImageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1119, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 24);
            this.label1.TabIndex = 33;
            this.label1.Text = "Imágenes";
            this.label1.Visible = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // CampaignEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteImageButton);
            this.Controls.Add(this.editImageButton);
            this.Controls.Add(this.imgGridView);
            this.Controls.Add(this.endMinComboBox);
            this.Controls.Add(this.initMinComboBox);
            this.Controls.Add(this.addImageButton);
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
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CampaignEditForm";
            this.Text = "CampaignEditForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPanel)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox logoPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox descTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label idValueLabel;
        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.Label initDateLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.Label initTimeLabel;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.DateTimePicker initDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.ComboBox initHourComboBox;
        private System.Windows.Forms.ComboBox endHourComboBox;
        private System.Windows.Forms.ComboBox initMinComboBox;
        private System.Windows.Forms.ComboBox endMinComboBox;
        private System.Windows.Forms.DataGridView imgGridView;
        private System.Windows.Forms.Button addImageButton;
        private System.Windows.Forms.Button editImageButton;
        private System.Windows.Forms.Button deleteImageButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewImageColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}