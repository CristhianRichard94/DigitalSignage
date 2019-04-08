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

        /// <summary>
        /// Instancia del servicio de banners
        /// </summary>
        private readonly IBannerService iBannerService;

        /// <summary>
        /// Instancia del servicio de campañas
        /// </summary>
        private readonly ICampaignService iCampaignService;

        /// <summary>
        /// Variable para eliminar subscripcion del servicio de banner
        /// </summary>
        private Unsubscriber<string> iBannerUnsubscriber;

        /// <summary>
        /// Variable para eliminar subscripcion del servicio de campaña
        /// </summary>
        private Unsubscriber<byte[]> iCampaignUnsubscriber;

        /// <summary>
        /// Variable para guardar el texto actual en bannerText
        /// </summary>
        private string iCurrentText;

        /// <summary>
        /// Guarda el carater que se esta quitando de iCurrentText
        /// </summary>
        private int iCurrentCharacter = 0;

        /// <summary>
        /// Variable de texto con espacio para que el texto de banner entre por la izquierda
        /// </summary>
        private string WHITE_SPACE = new String(' ', 40);


        public OperativeForm(ICampaignService campaignService, IBannerService bannerService)
        {
            InitializeComponent();
            this.iCampaignService = campaignService;
            this.iBannerService = bannerService;
            this.bannerText.Text = WHITE_SPACE+ "Cargando Banners";
            iBannerUnsubscriber = (Unsubscriber<string>)iBannerService.Subscribe(this);
            iCampaignUnsubscriber = (Unsubscriber<byte[]>)this.iCampaignService.Subscribe(this);


            // Timmer para mover el banner de texto
            this.timer1.Interval = 100;
            this.timer1.Start();
        }

        private void OperativeForm_Load(object sender, EventArgs e)
        {

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
