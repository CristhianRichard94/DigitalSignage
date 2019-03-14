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
        
        public ImageForm(ImageDTO pImage, int pImageListLength)
        {
            InitializeComponent();
            for (int i = 1; i <= pImageListLength; i++)
            {
                comboBox2.Items.Add(i);
            }

            if (pImage != null)
            {
                this.iImage = pImage;
                this.pictureBox1.Image = byteArrayToImage(this.iImage.Data);
                this.textBox1.Text = this.iImage.Description;
                this.textBox2.Text = this.iImage.Duration.ToString();
                this.comboBox2.Text = this.iImage.Position.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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



        // Image conversion aux functions
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
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
