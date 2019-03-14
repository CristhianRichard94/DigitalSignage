using DigitalSignage.BLL;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI.Campaign_Forms
{
    public partial class CampaignEditForm : Form
    {
    
        private CampaignService iCampaignService;
        private CampaignDTO iCampaign = new CampaignDTO();
        private IEnumerable<ImageDTO> iImages;

        public CampaignDTO Campaign { get => iCampaign; set => iCampaign = value; }

        public CampaignEditForm(CampaignDTO pCampaign)
        {
            this.iCampaignService = new CampaignService();
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            if (pCampaign != null)
            {
                this.iCampaign = pCampaign;
                this.loadCampaign();
                this.iImages = pCampaign.Images;
                this.dataGridView1.DataSource = pCampaign.Images;

            }
            dataGridView1.ForeColor = Color.Black;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CampaignEditForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Controlar campos vacíos y length de imagenes
                this.saveCampaign();
                //Guardar Imagenes
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
        }

        private void loadCampaign()
        {
            idLabel.Visible = true;
            idValueLabel.Visible = true;
            idValueLabel.Text = iCampaign.Id.ToString();
            nameTextBox.Text = iCampaign.Name;
            descTextBox.Text = iCampaign.Description;
            initDateTimePicker.Value = iCampaign.InitialDate;
            endDateTimePicker.Value = iCampaign.EndDate;
            comboBox1.SelectedIndex = iCampaign.InitialTime.Hours;
            comboBox2.SelectedIndex = iCampaign.InitialTime.Minutes;
            comboBox3.SelectedIndex = iCampaign.EndTime.Hours;
            comboBox4.SelectedIndex = iCampaign.EndTime.Minutes;
        }

        private void saveCampaign()
        {
            this.iCampaign.Name = nameTextBox.Text;
            this.iCampaign.Description = descTextBox.Text;
            this.iCampaign.InitialDate = initDateTimePicker.Value;
            this.iCampaign.EndDate = endDateTimePicker.Value;
            this.iCampaign.InitialTime = new TimeSpan(Convert.ToInt32(comboBox1.Text), Convert.ToInt32(comboBox2.Text), 0);
            this.iCampaign.EndTime = new TimeSpan(Convert.ToInt32(comboBox3.Text), Convert.ToInt32(comboBox4.Text), 0);
        }

 
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool initTimeIsBeforeEndTime()
        {
            int hours = int.Parse(comboBox1.SelectedItem.ToString());
            int minutes = int.Parse(comboBox2.SelectedItem.ToString());
            var initTimespan = new TimeSpan(hours, minutes, 0);

            hours = int.Parse(comboBox3.SelectedItem.ToString());
            minutes = int.Parse(comboBox4.SelectedItem.ToString());
            var endTimespan = new TimeSpan(hours, minutes, 0);

            return initTimespan.CompareTo(endTimespan) < 0;
        }
        
        private bool initDateIsBeforeEndDate()
        {
            return initDateTimePicker.Value.CompareTo(endDateTimePicker.Value) < 0;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addImageButton_Click(object sender, EventArgs e)
        {

        }

        private void editImageButton_Click(object sender, EventArgs e)
        {

        }
    }
}
