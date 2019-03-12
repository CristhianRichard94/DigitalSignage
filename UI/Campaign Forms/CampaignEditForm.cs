using DigitalSignage.BLL;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI.Campaign_Forms
{
    public partial class CampaignEditForm : Form
    {
    
        private CampaignService iCampaignService;
        private CampaignDTO iCampaign;
        private IEnumerable<ImageDTO> iImages;
        private bool editing = false;
        public CampaignEditForm(int pCampaignId)
        {
            this.iCampaignService = new CampaignService();
            InitializeComponent();

            //Editando
            if (pCampaignId!=-1)
            {
                editing = true;
                //Busca la campaña
                this.iCampaign = this.iCampaignService.Get(pCampaignId);
                //Inserta datos en los campos
                idLabel.Visible = true;
                idValueLabel.Visible = true;
                idValueLabel.Text = iCampaign.Id.ToString();
                initDateLabel.Text = 
                nameTextBox.Text = iCampaign.Name;
                descTextBox.Text = iCampaign.Description;
                initDateTimePicker.Value = iCampaign.InitialDate;
                endDateTimePicker.Value = iCampaign.EndDate;
                comboBox1.SelectedIndex = iCampaign.InitialTime.Hours;
                comboBox2.SelectedIndex = iCampaign.InitialTime.Minutes;
                comboBox3.SelectedIndex = iCampaign.InitialTime.Hours;
                comboBox4.SelectedIndex = iCampaign.InitialTime.Minutes;
                this.imgDataGridView.DataSource = iCampaign.Images;
            }
            //Creando
            else
            {
                this.iCampaign = new CampaignDTO();
                this.iImages = new List<ImageDTO>();
            }
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
                this.iCampaign.Name = nameTextBox.Text;
                this.iCampaign.Description = descTextBox.Text;
                this.iCampaign.InitialDate = initDateTimePicker.Value;
                this.iCampaign.EndDate = endDateTimePicker.Value;
                this.iCampaign.InitialTime = new TimeSpan(Convert.ToInt32(comboBox1.Text), Convert.ToInt32(comboBox2.Text), 0);
                this.iCampaign.EndTime = new TimeSpan(Convert.ToInt32(comboBox3.Text), Convert.ToInt32(comboBox4.Text), 0);
                //Guardar Imagenes



                //Usa el servicio para Guardar
                if (editing)
                {
                    this.iCampaignService.Update(this.iCampaign);
                    MessageBox.Show("Se ha modificado exitosamente la campaña");

                }
                else
                {
                    this.iCampaignService.Create(this.iCampaign);
                    MessageBox.Show("Se ha creado exitosamente la campaña");
                }
                this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
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
    }
}
