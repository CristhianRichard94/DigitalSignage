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
        public IList<ImageDTO> Images { get => iImages; set => iImages = value; }

        /// <summary>
        /// Inicializa el servicio 
        /// asigna la campaña a editar o Inicializa una nueva campaña
        /// Asigna imagenes o genera una lista
        /// Agrega Imagenes a la lista
        /// </summary>
        /// <param name="pCampaign"></param>
        public CampaignEditForm(CampaignDTO pCampaign)
        {
            iCampaignService = new CampaignService();
            InitializeComponent();
            imgGridView.AutoGenerateColumns = false;
            if (pCampaign != null)
            {
                Campaign = pCampaign;
                loadCampaign();
                Images = pCampaign.Images;
                updateImagesGridView();
            }
            else
            {
                Campaign = new CampaignDTO();
                Campaign.Images = new List<ImageDTO>();
                Images = new List<ImageDTO>();
                Campaign.Name = "";
                Campaign.Description = "";
            }
            imgGridView.ForeColor = Color.Black;
        }


        /// <summary>
        /// Boton para volver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (!anyChange())
            {
                Close();
            }
            else
            {
                var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea cancelar las operaciones realizadas? se perderan los cambios",
                         "Cancelar");
                confirmResult.ShowDialog();
                if (confirmResult.DialogResult == DialogResult.Yes)
                {
                    Close();
                }
            }

        }

        /// <summary>
        /// Guarda la camapaña y cierra el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (compareTimes())
            {
                errorProvider.SetError(endMinComboBox, "El tiempo de inicio debe ser menor al de fin");
            }
            else
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    try
                    {
                        saveCampaign();
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    catch (Exception exc)
                    {
                        new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
                    }
                }
            }

        }

        /// <summary>
        /// Carga la campaña en la vista
        /// </summary>
        private void loadCampaign()
        {
            idLabel.Visible = true;
            idValueLabel.Visible = true;
            idValueLabel.Text = Campaign.Id.ToString();
            nameTextBox.Text = Campaign.Name;
            descTextBox.Text = Campaign.Description;
            initDateTimePicker.Value = Campaign.InitialDate;
            endDateTimePicker.Value = Campaign.EndDate;
            initHourComboBox.SelectedIndex = Campaign.InitialTime.Hours;
            initMinComboBox.SelectedIndex = Campaign.InitialTime.Minutes;
            endHourComboBox.SelectedIndex = Campaign.EndTime.Hours;
            endMinComboBox.SelectedIndex = Campaign.EndTime.Minutes;
        }

        /// <summary>
        /// Guarda la campaña de la vista al modelo
        /// </summary>
        private void saveCampaign()
        {
            Campaign.Name = nameTextBox.Text;
            Campaign.Description = descTextBox.Text;
            Campaign.InitialDate = initDateTimePicker.Value;
            Campaign.EndDate = endDateTimePicker.Value;
            Campaign.InitialTime = new TimeSpan(Convert.ToInt32(initHourComboBox.Text), Convert.ToInt32(initMinComboBox.Text), 0);
            Campaign.EndTime = new TimeSpan(Convert.ToInt32(endHourComboBox.Text), Convert.ToInt32(endMinComboBox.Text), 0);

            // Obtiene las imagenes del datagrid y las asigna a la campaña
            //List<ImageDTO> list = new List<ImageDTO>();
            //foreach (DataGridViewRow image in imgGridView.Rows)
            //{
            //    list.Add((ImageDTO)image.DataBoundItem);
            //}
            //Campaign.Images = list;
            Campaign.Images = Images;
        }


        /// <summary>
        /// Abre form para agregar una nueva imagen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addImageButton_Click(object sender, EventArgs e)
        {
            ImageForm imgform = new ImageForm(null, imgGridView.RowCount + 1);
            // Al cerrar el form
            if (imgform.ShowDialog(this) == DialogResult.OK)
            {
                try
                {

                    var newImage = imgform.Image;
                    var lastIndex = imgGridView.RowCount + 1;

                    // Si no tiene la ultima posición esta solapando otra imagen
                    if (newImage.Position != lastIndex)
                    {
                        // Busca la imagen solapada
                        var overLapImage = Images.Where(image => image.Position == newImage.Position).First();
                        overLapImage.Position = lastIndex;

                    }

                    // Agrega imagen a la lista, actualiza la lista de la vista y notifica
                    Images.Add(newImage);

                    updateImagesGridView();
                    new NotificationForm(MessageBoxButtons.OK, "Se ha añadido la imagen a la lista ", "Imagen añadida").ShowDialog();

                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
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
            ImageDTO img = (ImageDTO)imgGridView.SelectedRows[0].DataBoundItem;
            int oldImgPos = img.Position;
            ImageForm imgform = new ImageForm(img, Images.Count);
            if (imgform.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var updatedImage = imgform.Image;

                    // Si se modifico el orden de la imagen
                    if (updatedImage.Position != oldImgPos)
                    {
                        // Elimina la imagen anterior
                        var index = Images.IndexOf(img);
                        Images.RemoveAt(index);
                        // Ordena la lista
                        Images = Images.OrderBy(i => i.Position).ToList();
                        // Inserta la imagen modificada
                        Images.Insert(updatedImage.Position - 1, updatedImage);
                        // Acomoda las posiciones
                        int position = 1;
                        foreach (ImageDTO image in Images)
                        {
                            image.Position = position;
                            position++;
                        }

                    }
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

            // Luego de confirmado, elimina la imagen de la lista y actualiza la vista
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                var selectedImg = (ImageDTO)imgGridView.SelectedRows[0].DataBoundItem;
                // Verifica el orden de las imagenes
                if (selectedImg.Position != imgGridView.RowCount)
                {
                    Images = Images.OrderBy(i => i.Position).ToList();
                    var imgIndex = Images.IndexOf(selectedImg);
                    for (int i = imgIndex + 1; i < imgGridView.RowCount; i++)
                    {
                        Images[i].Position--;
                    }
                }
                var index = Images.IndexOf(selectedImg);
                Images.RemoveAt(index);
                updateImagesGridView();
            }


        }

        /// <summary>
        /// Ordena las imagenes por orden y actualiza la lista de la vista
        /// </summary>
        private void updateImagesGridView()
        {
            Images = Images.OrderBy(i => i.Position).ToList();
            imgGridView.DataSource = Images;
            imgGridView.Update();

        }

        // Validaciones de los datos

        private void nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (nameTextBox.Text.Length == 0)
            {
                error = "Ingrese nombre de la campaña.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void descTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (descTextBox.Text.Length == 0)
            {
                error = "Ingrese una descripción de la campaña.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void initDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (compareDates())
            {
                error = "La fecha de inicio debe ser menor a la de fin.";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }

        private void endDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (compareDates())
            {
                error = "La fecha de inicio debe ser menor a la de fin.";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }

        private void initHourComboBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (initHourComboBox.SelectedIndex == -1)
            {
                error = "Debe seleccionar una hora de inicio.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void initMinComboBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (initMinComboBox.SelectedIndex == -1)
            {
                error = "Debe seleccionar minutos de inicio.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void EndHourComboBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (endHourComboBox.SelectedIndex == -1)
            {
                error = "Debe seleccionar una hora de fin.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void endMinComboBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (endMinComboBox.SelectedIndex == -1)
            {
                error = "Debe seleccionar minutos de finalización.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void imgGridView_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (Images.Count == 0)
            {
                error = "Debe agregar al menos una imagen a la campaña";
                e.Cancel = true;
            }

            errorProvider.SetError(addImageButton, error);
        }



        /// FUNCIONES AUXILIARES DE COMPARACION

        private bool compareDates()
        {
            return initDateTimePicker.Value.CompareTo(endDateTimePicker.Value) > 0;
        }

        private bool compareTimes()
        {
            TimeSpan initTime = new TimeSpan(Convert.ToInt32(initHourComboBox.SelectedItem), Convert.ToInt32(initMinComboBox.SelectedItem), 0);

            TimeSpan endTime = new TimeSpan(Convert.ToInt32(endHourComboBox.SelectedItem), Convert.ToInt32(endMinComboBox.SelectedItem), 0);

            return initTime.CompareTo(endTime) > 0;
        }

        private bool anyChange()
        {
            bool change = false;
            change = (Campaign.Name != nameTextBox.Text) ? true : change;
            change = (Campaign.Description != descTextBox.Text) ? true : change;
            change = (Campaign.Images.Count != Images.Count) ? true : change;

            return change;
        }
    }
}
