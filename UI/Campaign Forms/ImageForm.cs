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
    public partial class ImageForm : Form
    {
        private ImageDTO iImage;

        public ImageDTO Image { get => iImage; set => iImage = value; }

        public ImageForm(ImageDTO pImage, int pImageListLength)
        {
            InitializeComponent();
            for (int i = 1; i <= pImageListLength; i++)
            {
                comboBox2.Items.Add(i);
            }

            if (pImage != null)
            {
                this.Image = pImage;
                this.pictureBox1.Image = byteArrayToImage(this.Image.Data);
                this.textBox1.Text = this.Image.Description;
                this.textBox2.Text = this.Image.Duration.ToString();
                this.comboBox2.Text = this.Image.Position.ToString();
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Abre dialogo para seleccionar imagen  
            OpenFileDialog open = new OpenFileDialog();
            // Añade Filtro de extensiones de imagen
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {  
                pictureBox1.Image = new Bitmap(open.FileName);
                imagePath.Text = open.FileName;
                imagePath.ForeColor = Color.WhiteSmoke;
                imagePath.Font = new Font(Font.FontFamily, 10);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Controlar campos vacíos
                //Guardar Imagen
                DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
        }

        public void saveImage()
        {
            //VALIDAR CAMPOS
            this.Image.Data = this.imageToByteArray(pictureBox1.Image);
            this.Image.Description = this.textBox1.Text;
            this.Image.Duration = Convert.ToInt32(this.textBox2.Text);
            this.Image.Position = Convert.ToInt32(this.comboBox2.Text);
        }

        // Funciones auxiliares para convertir imagenes
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

      
    }
}
