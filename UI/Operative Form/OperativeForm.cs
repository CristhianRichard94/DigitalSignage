using DigitalSignage.BLL;
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

namespace DigitalSignage.UI.Operative_Form
{
    public partial class OperativeForm : Form, IObserver<string>, IObserver<byte[]>
    {

        // Instancia del servicio de banners
        private readonly IBannerService iBannerService;

        // Instancia del servicio de campañas
        private readonly ICampaignService iCampaignService;

        // Variable para eliminar subscripcion del servicio de banner
        private Unsubscriber<string> iBannerUnsubscriber;

        // Variable para eliminar subscripcion del servicio de campaña
        private Unsubscriber<byte[]> iCampaignUnsubscriber;

        // Variable para guardar el texto actual en bannerText
        private string iCurrentText;

        // Guarda el carater que se esta quitando de iCurrentText
        private int iCurrentCharacter = 0;

        // Variable de texto con espacio para que el texto de banner entre por la izquierda
        private string WHITE_SPACE = new String(' ', 40);


        public OperativeForm(ICampaignService campaignService, IBannerService bannerService)
        {
            InitializeComponent();
            this.iCampaignService = campaignService;
            this.iBannerService = bannerService;
            this.bannerText.Text = WHITE_SPACE + "No hay ningun banner activo en este momento";
            iCampaignUnsubscriber = (Unsubscriber<byte[]>)this.iCampaignService.Subscribe(this);
            iBannerUnsubscriber = (Unsubscriber<string>)iBannerService.Subscribe(this);


            // Timmer para mover el banner de texto
            this.timer1.Interval = 100;
            this.timer1.Start();
        }

        private void OperativeForm_Load(object sender, EventArgs e)
        {

        }

        private void OperativeForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {

                // Minimizar pantalla
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;

            }
            if (e.KeyCode == Keys.F11)
            {

                // Maximizar pantalla
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;

            }
            if (e.KeyCode == Keys.F5)
            {

                // Actualizar banners y campañas
                iBannerService.RefreshActiveBanners();
                iCampaignService.RefreshActiveCampaigns();

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void moveBannerText()
        {            
            if (string.IsNullOrEmpty(bannerText.Text))
            {
                bannerText.Text = iCurrentText;
                return;
            }

            // Ya mostro todo el texto
            if (iCurrentCharacter == bannerText.Text.Length)
            {

                bannerText.Text = iCurrentText;
                iCurrentCharacter = 0;

            }

            bannerText.Text = bannerText.Text.Substring(1) + bannerText.Text[0];
            iCurrentCharacter++;

        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(byte[] value)
        {
            imgBox.Image = byteArrayToImage(value);
        }

        public void OnNext(string text)
        {
            iCurrentText = WHITE_SPACE + text;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                // No esta en el hilo de UI, Reingresar ahi... 
                this.BeginInvoke(new EventHandler(timer1_Tick), sender, e);
            }
            else
            {
                lock (timer1)
                {
                    // Solo funciona cuando no es reingresada mientras ya esta en proceso
                    if (this.timer1.Enabled)
                    {
                        // Detiene el timer
                        this.timer1.Stop();

                        // Mover el texto del banner
                        moveBannerText();
                        updateTimeLabel();
                        // Reinicia el timer
                        this.timer1.Start();
                    }
                }
            }
        }

        void updateTimeLabel()
        {
            this.timeLabel.Text = DateTime.Now.ToShortDateString() + " - "+ DateTime.Now.ToShortTimeString();
        }
    }
}
