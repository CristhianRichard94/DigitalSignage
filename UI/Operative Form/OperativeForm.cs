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
        private string WHITE_SPACE = new String(' ', 80);


        /// <summary>
        /// Constructor, obtiene instancias de servicios de banner y campaña
        /// Se subscribe a los mismos
        /// Ordena el inicio de tareas de actualizacion 
        /// </summary>
        /// <param name="campaignService"></param>
        /// <param name="bannerService"></param>
        public OperativeForm(ICampaignService campaignService, IBannerService bannerService)
        {
            InitializeComponent();
            this.iCampaignService = campaignService;
            this.iBannerService = bannerService;
            this.bannerText.Text = "";
            iBannerUnsubscriber = (Unsubscriber<string>)iBannerService.Subscribe(this);
            iCampaignUnsubscriber = (Unsubscriber<byte[]>)this.iCampaignService.Subscribe(this);
            iBannerService.StartAsyncTasks();
            iCampaignService.StartAsyncTasks();


            // Timmer para mover el banner de texto
            this.timer.Interval = 100;
            this.timer.Start();
        }

        /// <summary>
        /// ordena el cancelamiento de las tareas de actualización y cierra el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.iCampaignService.CancelAsyncTasks();
            this.iBannerService.CancelAsyncTasks();
            this.Close();
        }


        /// <summary>
        /// Funcion auxiliar para convertir los datos del modelo en una imagen para la vista
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Mueve el texto del banner dentro en la pantalla
        /// </summary>
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

        /// <summary>
        /// Recibe una nueva imagen del servicio 
        /// </summary>
        /// <param name="value"></param>
        public void OnNext(byte[] value)
        {
            imgBox.Image = byteArrayToImage(value);
        }

        /// <summary>
        /// Recibe nuevo texto del servicio del banners
        /// </summary>
        /// <param name="text"></param>
        public void OnNext(string text)
        {
            iCurrentText = WHITE_SPACE + text;
        }

        /// <summary>
        /// Actualiza la posicion del banner y la hora actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                // No esta en el hilo de UI, Reingresar ahi... 
                this.BeginInvoke(new EventHandler(timer_Tick), sender, e);
            }
            else
            {
                lock (timer)
                {
                    // Solo funciona cuando no es reingresada mientras ya esta en proceso
                    if (this.timer.Enabled)
                    {
                        // Detiene el timer
                        this.timer.Stop();

                        // Mover el texto del banner
                        moveBannerText();
                        updateTimeLabel();
                        // Reinicia el timer
                        this.timer.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Muestra la fecha y hora actual
        /// </summary>
        void updateTimeLabel()
        {
            this.timeLabel.Text = DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
