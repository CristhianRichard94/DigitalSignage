using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI
{
    /// <summary>
    /// Form de mensajes personalizado, despliega notificaciones o confirmaciones
    /// </summary>
    public partial class NotificationForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pMessageBoxButtons">tipo de botones, usado para modificar los botones mostrados</param>
        /// <param name="pText"> Informacion a mostrar</param>
        /// <param name="pTitle">Titulo del form</param>
        public NotificationForm(MessageBoxButtons pMessageBoxButtons, string pText, string pTitle)
        {
            InitializeComponent();

            this.titleLabel.Text = pTitle;
            this.infoLabel.Text = pText;

            // Verifica si es un mensaje de informacion o confirmacion
            if (pMessageBoxButtons == MessageBoxButtons.YesNo)
            {
                this.acceptButton.Visible = true;
                this.cancelButton.Visible = true;
                this.okButton.Visible = false;
            } else
            {
                this.acceptButton.Visible = false;
                this.cancelButton.Visible = false;
                this.okButton.Visible = true;
            }
        }

        /// <summary>
        /// Accion de aceptar el mensaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        /// <summary>
        /// Cierra el mensaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Rechaza/Cancela el mensaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
