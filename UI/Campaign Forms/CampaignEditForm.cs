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
        private IList<ImageDTO> iImages;

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
            else
            {
                this.dataGridView1.Columns["Data"].DefaultHeaderCellType = null;
            }
            dataGridView1.ForeColor = Color.Black;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("¿Está seguro que desea cancelar las operaciones realizadas? se perderan los cambios",
                                     "Cancelar",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
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
            this.Campaign.Name = nameTextBox.Text;
            this.Campaign.Description = descTextBox.Text;
            this.Campaign.InitialDate = initDateTimePicker.Value;
            this.Campaign.EndDate = endDateTimePicker.Value;
            this.Campaign.InitialTime = new TimeSpan(Convert.ToInt32(comboBox1.Text), Convert.ToInt32(comboBox2.Text), 0);
            this.Campaign.EndTime = new TimeSpan(Convert.ToInt32(comboBox3.Text), Convert.ToInt32(comboBox4.Text), 0);
            List<ImageDTO> list = new List<ImageDTO>();
            foreach (DataGridViewRow image in this.dataGridView1.Rows)
            {
                list.Add((ImageDTO)image.DataBoundItem);
            }
            this.Campaign.Images = list;
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
            ImageForm imgform = new ImageForm(null, this.dataGridView1.RowCount + 1);
            if (imgform.ShowDialog(this) == DialogResult.OK)
            {
                try
                {

                    var newImage = imgform.Image;
                    var lastIndex = 1 + this.dataGridView1.RowCount;

                    //Verificar que no ocupe el orden de otra imagen
                    if (newImage.Position != lastIndex)
                    {

                        var solapedImage = this.Campaign.Images.Where(image => image.Position == newImage.Position).First();
                        solapedImage.Position = lastIndex;

                    }

                    this.Campaign.Images.Add(imgform.Image);
                    this.updateImagesGridView();

                    MessageBox.Show(this, "Se ha añadido la imagen a la lista ", "Imagen añadida", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error: " + ex.Message, "Error al añadir imagen", MessageBoxButtons.OK);
                }
            }
        }

        private void editImageButton_Click(object sender, EventArgs e)
        {
            ImageDTO img = (ImageDTO)this.dataGridView1.SelectedRows[0].DataBoundItem;
            ImageForm imgform = new ImageForm(img, this.Campaign.Images.Count);
            if (imgform.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var updatedImage = imgform.Image;
                    var oldImage = this.Campaign.Images.Where(i => i.Id == updatedImage.Id).First();

                    //Verificar que no ocupe el orden de otra imagen
                    if (updatedImage.Position != oldImage.Position)
                    {

                        var editImg = this.Campaign.Images.Where(image => image.Position == updatedImage.Position).First();
                        editImg.Position = oldImage.Position;

                    }

                    this.Campaign.Images[this.Campaign.Images.IndexOf(oldImage)] = updatedImage;
                    updateImagesGridView();
                    MessageBox.Show(this, "Se ha editado correctamente la imagen", "Imagen editada", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error: " + ex.Message, "Error al añadir imagen", MessageBoxButtons.OK);
                }
            }
        }

        private void deleteImageButton_Click(object sender, EventArgs e)
        {
            var selectedImg = (ImageDTO)this.dataGridView1.SelectedRows[0].DataBoundItem;
            var index = this.Campaign.Images.IndexOf(selectedImg);
            this.Campaign.Images.RemoveAt(index);
            updateImagesGridView();

        }


        private void updateImagesGridView()
        {
            this.Campaign.Images = this.Campaign.Images.OrderBy(i => i.Position).ToList();
            this.dataGridView1.DataSource = this.Campaign.Images;
            this.dataGridView1.Update();

        }
    }
}
