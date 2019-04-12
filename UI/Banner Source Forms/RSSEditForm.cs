using DigitalSignage.BLL.RSSReader;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI.RSS_Forms
{
    public partial class RSSEditForm : Form
    {
        private RSSSourceDTO iRSSSource;
        private IRSSReader iRSSReader;

        public RSSEditForm(IRSSReader rSSReader,RSSSourceDTO pRSSSource)
        {
            InitializeComponent();

            this.RSSReader = rSSReader;

            if (pRSSSource == null)
            {
                this.RSSSource = new RSSSourceDTO();
                this.RSSSource.RSSItems = new List<RSSItemDTO>();
            }
            else
            {
                this.RSSSource = pRSSSource;
                this.loadSource();
            }
        }
        
        public RSSSourceDTO RSSSource { get => iRSSSource; set => iRSSSource = value; }
        public IRSSReader RSSReader { get => iRSSReader; set => iRSSReader = value; }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (!anyChange())
            {
                Close();
            }
            else
            {
                if (BwRSSReader.IsBusy)
                {
                    var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "Se estan obteniendo feeds ¿Está seguro que desea cancelar el proceso y salir sin guardar?",
                                        "Cancelar");
                    confirmResult.ShowDialog();
                    if (confirmResult.DialogResult == DialogResult.Yes)
                    {
                        BwRSSReader.CancelAsync();
                        this.Close();
                    }
                }
                else
                {
                    var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea cancelar las operaciones realizadas? se perderan los cambios",
                                          "Cancelar");
                    confirmResult.ShowDialog();
                    if (confirmResult.DialogResult == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            { 
                try
                {
                    this.saveSource();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception exc)
                {
                    new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
                }
        }
        }


        void loadSource()
        {
            this.urlTextBox.Text = this.RSSSource.Url;
            this.descriptionTextBox.Text = this.RSSSource.Description;
            this.rSSItemsGridView.DataSource = this.RSSSource.RSSItems;
        }

        void saveSource()
        {
            this.RSSSource.Url = this.urlTextBox.Text;
            this.RSSSource.Description = this.descriptionTextBox.Text;

            // Obtiene los items del datagrid y los asigna a la fuente
            List<RSSItemDTO> list = new List<RSSItemDTO>();
            foreach (DataGridViewRow item in this.rSSItemsGridView.Rows)
            {
                list.Add((RSSItemDTO)item.DataBoundItem);
            }
            this.RSSSource.RSSItems = list;
        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            Uri uri;

            if (!Uri.TryCreate(urlTextBox.Text.Trim(), UriKind.Absolute, out uri))
            {
                new NotificationForm(MessageBoxButtons.OK, "La URL ingresada no es válida.", "URL Erronea").ShowDialog();
            } else
            {
                verifyButton.Enabled = false;
                saveButton.Enabled = false;
                BwRSSReader.RunWorkerAsync(uri);
                this.loadPictureBox.Visible = true;
            }

        }

        private void BwRSSReader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = iRSSReader.Read((Uri)e.Argument);

                BwRSSReader.ReportProgress(1, e.Result);
                if (BwRSSReader.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al leer feeds").ShowDialog();

            }

        }

        private void BwRSSReader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            rSSItemsGridView.DataSource = e.UserState;
        }

        private void BwRSSReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                new NotificationForm(MessageBoxButtons.OK, "Error: "+ e.Error.Message, "Error al obtener feeds.").ShowDialog();

            }
            else if (!e.Cancelled)
            {

                var items = (List<RSSItemDTO>)e.Result;
                rSSItemsGridView.DataSource = items;
                iRSSSource.RSSItems = items;
            }

            verifyButton.Enabled = true;
            saveButton.Enabled = true;
            loadPictureBox.Visible = false;
            checkPictureBox.Visible = true;

        }

        private void urlTextBox_TextChanged(object sender, EventArgs e)
        {
            rSSItemsGridView.DataSource = null;
        }

        private void urlTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (urlTextBox.Text.Length == 0)
            {
                errorProvider.SetError(urlTextBox, "Debe ingresar una url");
                e.Cancel = true;

            }
            else
            {
                Uri uri;

                if (!Uri.TryCreate(urlTextBox.Text.Trim(), UriKind.Absolute, out uri))
                {

                    errorProvider.SetError(urlTextBox, "La Url ingresada no es válida.");
                    e.Cancel = true;

                }
            }

            errorProvider.SetError(urlTextBox, null);
        }

        private void descriptionTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (descriptionTextBox.Text.Length == 0)
            {
                error = "Ingrese una descripción de la fuente";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void rSSItemsGridView_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (rSSItemsGridView.RowCount == 0)
            {
                error = "Verifique que pueden obtenerse feeds de la Url";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }


        private bool anyChange()
        {
            bool change = false;
            change = (RSSSource.Url != urlTextBox.Text) ? true : change;
            change = (RSSSource.Description != descriptionTextBox.Text) ? true : change;
            change = (RSSSource.RSSItems != (List<RSSItemDTO>)rSSItemsGridView.DataSource) ? true : change;
            return change;
        }
    }
}
