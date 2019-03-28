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
    /// <summary>
    /// Form de creacion y edicion de una campaña 
    /// </summary>
    public partial class CampaignEditForm : Form
    {
        /// <summary>
        /// instancia del servicio de campañas
        /// </summary>
        private CampaignService iCampaignService;

        /// <summary>
        /// Campaña siendo creada/modificada
        /// </summary>
        private CampaignDTO iCampaign = new CampaignDTO();
        
        /// <summary>
        /// Lista de imagenes
        /// </summary>
        private IList<ImageDTO> iImages;

        public CampaignDTO Campaign { get => iCampaign; set => iCampaign = value; }

        /// <summary>
        /// Inicializa el servicio 
        /// asigna la campaña a editar o Inicializa una nueva campaña
        /// Asigna imagenes o genera una lista
        /// Agrega Imagenes a la lista
        /// </summary>
        /// <param name="pCampaign"></param>
        public CampaignEditForm(CampaignDTO pCampaign)
        {
            this.iCampaignService = new CampaignService();
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            if (pCampaign != null)
            {
                this.Campaign = pCampaign;
                this.loadCampaign();
                this.iImages = pCampaign.Images;
                this.dataGridView1.DataSource = pCampaign.Images;
            }
            else
            {
                this.Campaign = new CampaignDTO();
                this.Campaign.Images = new List<ImageDTO>();
            }
            dataGridView1.ForeColor = Color.Black;
        }
        // FALTA REACOMODAR ORDENES DE IMAGENES, CUANDO UNA SOLAPA A OTRA SE COPIAN, 
        // CUANDO SE ELIMINA UNA DE ORDEN INTERMEDIO SE ROMPE TODO
        // Falta validar cambio en los campos para el boton de volver
        // Falta agregar un error provider para guardar

            /// <summary>
            /// Boton para volver
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo,"¿Está seguro que desea cancelar las operaciones realizadas? se perderan los cambios",
                                     "Cancelar");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Guarda la camapaña y cierra el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                new NotificationForm(MessageBoxButtons.OK,exc.Message,"Error").ShowDialog();
            }
        }

        /// <summary>
        /// Carga la campaña en la vista
        /// </summary>
        private void loadCampaign()
        {
            idLabel.Visible = true;
            idValueLabel.Visible = true;
            idValueLabel.Text = this.Campaign.Id.ToString();
            nameTextBox.Text = this.Campaign.Name;
            descTextBox.Text = this.Campaign.Description;
            initDateTimePicker.Value = this.Campaign.InitialDate;
            endDateTimePicker.Value = this.Campaign.EndDate;
            comboBox1.SelectedIndex = this.Campaign.InitialTime.Hours;
            comboBox2.SelectedIndex = this.Campaign.InitialTime.Minutes;
            comboBox3.SelectedIndex = this.Campaign.EndTime.Hours;
            comboBox4.SelectedIndex = this.Campaign.EndTime.Minutes;
        }

        /// <summary>
        /// Guarda la campaña de la vista al modelo
        /// </summary>
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

        /// <summary>
        /// Comprueba si el tiempo de inicio esta antes del de fin
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Comprueba si la fecha de inicio esta antes de la de fin
        /// </summary>
        /// <returns></returns>
        private bool initDateIsBeforeEndDate()
        {
            return initDateTimePicker.Value.CompareTo(endDateTimePicker.Value) < 0;
        }

        /// <summary>
        /// Abre form para agregar una nueva imagen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addImageButton_Click(object sender, EventArgs e)
        {
            ImageForm imgform = new ImageForm(null, this.dataGridView1.RowCount + 1);
            // Al cerrar el form
            if (imgform.ShowDialog(this) == DialogResult.OK)
            {
                try
                { 

                    var newImage = imgform.Image;
                    var lastIndex = 1 + this.dataGridView1.RowCount;

                    //Verificar que no ocupe el orden de otra imagen              - falta arreglar
                    if (newImage.Position != lastIndex)
                    {

                        var solapedImage = this.Campaign.Images.Where(image => image.Position == newImage.Position).First();
                        solapedImage.Position = lastIndex;

                    }
                    // Agrega imagen a la lista, actualiza la lista de la vista y notifica
                    this.Campaign.Images.Add(imgform.Image);
                    this.updateImagesGridView();
                    new NotificationForm(MessageBoxButtons.OK, "Se ha añadido la imagen a la lista ", "Imagen añadida").ShowDialog();

                }
                catch (Exception ex)
            {
                    new NotificationForm(MessageBoxButtons.OK, "Error: "+ ex.Message, "Error").ShowDialog();
                }
        }
        }

        /// <summary>
        /// Abre form para editar una imagen existente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    new NotificationForm(MessageBoxButtons.OK, "Se ha editado correctamente la imagen", "Imagen editada").ShowDialog();

                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
                }
            }
        }

        /// <summary>
        /// Elimina una imagen de la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteImageButton_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea eliminar la imagen seleccionada?",
                                    "Cancelar");
            confirmResult.ShowDialog();

            // Luego de confirmado elimina la imagen de la lista de la campaña y actualiza la lista de la vista
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                var selectedImg = (ImageDTO)this.dataGridView1.SelectedRows[0].DataBoundItem;
                var index = this.Campaign.Images.IndexOf(selectedImg);
                this.Campaign.Images.RemoveAt(index);
                updateImagesGridView();
            }


        }

        /// <summary>
        /// Ordena las imagenes por orden y actualiza la lista de la vista
        /// </summary>
        private void updateImagesGridView()
        {
            this.Campaign.Images = this.Campaign.Images.OrderBy(i => i.Position).ToList();
            this.dataGridView1.DataSource = this.Campaign.Images;
            this.dataGridView1.Update();

        }
    }
}
