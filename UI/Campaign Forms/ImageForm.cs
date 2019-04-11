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
    /// Form de creacion/edicion de imagen
    /// </summary>
    public partial class ImageForm : Form
    {
        /// <summary>
        /// Imagen a crear/editar
        /// </summary>
        private ImageDTO iImage;

        public ImageDTO Image { get => iImage; set => iImage = value; }

        /// <summary>
        /// Constructor, carga la imagen en la vista o inicializa la imagen 
        /// </summary>
        /// <param name="pImage">Imagen a editar, si se esta creando recibe null</param>
        /// <param name="pImageListLength">Cantidad de imagenes en la lista, usado para determinar el rango de posiciones</param>
        public ImageForm(ImageDTO pImage, int pImageListLength)
        {
            InitializeComponent();
            for (int i = 1; i <= pImageListLength; i++)
            {
                positionComboBox.Items.Add(i);
            }
            positionComboBox.SelectedIndex = positionComboBox.Items.Count - 1;

            if (pImage != null)
            {
                Image = pImage;
                imgBox.Image = byteArrayToImage(Image.Data);
                descriptionTextBox.Text = Image.Description;
                durationTextBox.Text = Image.Duration.ToString();
                positionComboBox.Text = Image.Position.ToString();
            }
            else
            {
                Image = new ImageDTO();
                Image.Position = pImageListLength;
            }
        }

        /// <summary>
        /// Boton para cancelar cambios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (!anyChange())
            {
                Close();
            } else
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
        /// Abre dialogo de seleccion de imagen y la carga en la vista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadImg_Click(object sender, EventArgs e)
        {
            // Abre dialogo para seleccionar imagen  
            OpenFileDialog open = new OpenFileDialog();
            // Añade Filtro de extensiones de imagen
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                imgBox.Image = new Bitmap(open.FileName);
                imagePath.Text = open.FileName;
                imagePath.ForeColor = Color.WhiteSmoke;
                imagePath.Font = new Font(Font.FontFamily, 10);
            }
        }

        /// <summary>
        /// Guarda los cambios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            //Controlar campos vacíos
            saveImage();
            DialogResult = DialogResult.OK;

            Close();
        }

        /// <summary>
        /// Guarda la imagen de la vista en el modelo
        /// </summary>
        public void saveImage()
        {
            //VALIDAR CAMPOS
            Image.Data = imageToByteArray(imgBox.Image);
            Image.Description = descriptionTextBox.Text;
            Image.Duration = Convert.ToInt32(durationTextBox.Text);
            Image.Position = Convert.ToInt32(positionComboBox.Text);
        }

        // Funciones auxiliares para convertir imagenes

        /// <summary>
        /// Convierte bytes en imagen para mostrar
        /// </summary>
        /// <param name="byteArrayIn">Bytes a convertir</param>
        /// <returns></returns>
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Convierte imagen en bytes para guardar
        /// </summary>
        /// <param name="imageIn">Imagen a convertir</param>
        /// <returns></returns>
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void imgBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (imgBox == null || imgBox.Image == null)
            {
                error = "Debe ingresar una imagen.";
                e.Cancel = true;
            }

            errorProvider.SetError(loadImgButton, error);
        }

        private void descriptionTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (descriptionTextBox.Text.Length == 0)
            {
                error = "Debe ingresar una descripción.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void durationTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (durationTextBox.Text.Length == 0)
            {
                error = "Debe ingresar una duración.";
                e.Cancel = true;
            } else
            {
                int duration;
                if (!int.TryParse(durationTextBox.Text, out duration))
                {
                    error = "La duración debe ser un valor numérico.";
                    e.Cancel = true;
                }
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void positionComboBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (positionComboBox.SelectedIndex == -1)
            {
                error = "Debe seleccionar una posición.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private bool anyChange()
        {
            bool change = false;
            change = (Image.Description != descriptionTextBox.Text) ? true : change;
            int result;
            if (int.TryParse(durationTextBox.Text, out result))
            {
                change = (Image.Duration != result) ? true : change;

            }
            change = (Image.Position != Convert.ToInt32(positionComboBox.SelectedItem)) ? true : change;

            return change;
        }
    }
}
