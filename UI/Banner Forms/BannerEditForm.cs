using DigitalSignage.BLL;
using DigitalSignage.DTO;
using DigitalSignage.UI.RSS_Forms;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI.Banner_Forms
{
    /// <summary>
    /// Form de creación y edición de banners
    /// </summary>
    public partial class BannerEditForm : Form
    {
        private BannerDTO iBanner;
        private RSSSourceDTO iRSSSource;
        private StandardKernel kernel;

        //constantes para los tipos de fuentes
        const string RSS_SOURCE = "Fuente RSS";
        const string TEXT_SOURCE = "Fuente de texto";


        public BannerEditForm(BannerDTO pBanner)
        {
            InitializeComponent();
            sourceComboBox.Items.Add(RSS_SOURCE);
            sourceComboBox.Items.Add(TEXT_SOURCE);
            // Editando
            if (pBanner != null)
            {
                Banner = pBanner;
                loadBanner();
            }
            // Creando un banner
            else
            {
                Banner = new BannerDTO();
                Banner.Source = new TextSourceDTO();
                // Opcion de cargar fuente de texto
                sourceComboBox.SelectedItem = TEXT_SOURCE;
                initHourComboBox.SelectedItem = 0;
                initMinComboBox.SelectedItem = 0;
                endHourComboBox.SelectedItem = 23;
                endMinComboBox.SelectedItem = 59;

            }
        }

        /// <summary>
        /// Instancia del banner a creando/modificando
        /// </summary>
        public BannerDTO Banner { get => iBanner; set => iBanner = value; }

        /// <summary>
        /// Instancia de la fuente RSS, en caso de que exista
        /// </summary>
        public RSSSourceDTO RSSSource { get => iRSSSource; set => iRSSSource = value; }


        /// <summary>
        /// Verifica si se realizaron cambios en el form y cancela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (anyChange())
            {
                var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea cancelar las operaciones realizadas? se perderan los cambios",
                         "Cancelar");
                confirmResult.ShowDialog();
                if (confirmResult.DialogResult == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }

        }

        /// <summary>
        /// Cambio en el selector de fuente de banner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (sourceComboBox.SelectedItem)
            {
                case TEXT_SOURCE:
                    textSourceTextBox.Visible = true;
                    selectRSSButton.Visible = false;
                    pictureBox1.Visible = false;
                    rSSSourceLabel.Visible = false;
                    break;
                case RSS_SOURCE:
                    textSourceTextBox.Visible = false;
                    selectRSSButton.Visible = true;
                    rSSSourceLabel.Visible = true;
                    rSSSourceLabel.Text = string.Format(
                        "Id: {1}{0} Descripción: {2}{0} Url: {3}{0}",
                        Environment.NewLine,
                        RSSSource.Id,
                        RSSSource.Description,
                        RSSSource.Url
                    );
                    break;
            }
        }

        /// <summary>
        /// Botón de selección de fuente RSS despliega pantalla de gestion de fuentes RSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectRSSButton_Click(object sender, EventArgs e)
        {
            kernel = HomeForm.CreateKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var rSSSourceService = kernel.Get<IRSSSourceService>();

            if (Banner.Source != null && Banner.Source.GetType() == typeof(RSSSourceDTO))
            {
                RSSManageForm rSSManageForm = new RSSManageForm(rSSSourceService, Banner.Source.Id);

                if (rSSManageForm.ShowDialog() == DialogResult.OK)
                {
                    RSSSource = rSSManageForm.RSSSourceSelected;
                    pictureBox1.Visible = true;
                }
            }
            else
            {
                RSSManageForm rSSManageForm = new RSSManageForm(rSSSourceService, 0);

                if (rSSManageForm.ShowDialog() == DialogResult.OK)
                {
                    RSSSource = rSSManageForm.RSSSourceSelected;
                    pictureBox1.Visible = true;
                    rSSSourceLabel.Text = string.Format(
                                            "Id: {1}{0} Descripción: {2}{0} Url: {3}{0}",
                                            Environment.NewLine,
                                            RSSSource.Id,
                                            RSSSource.Description,
                                            RSSSource.Url
                                        );
                }
            }

        }


        /// <summary>
        /// Carga el banner del modelo en el form
        /// </summary>
        private void loadBanner()
        {
            idLabel.Visible = true;
            idValueLabel.Visible = true;
            idValueLabel.Text = Banner.Id.ToString();
            nameTextBox.Text = Banner.Name;
            descTextBox.Text = Banner.Description;
            initDateTimePicker.Value = Banner.InitialDate;
            endDateTimePicker.Value = Banner.EndDate;
            initHourComboBox.SelectedIndex = Banner.InitialTime.Hours;
            initMinComboBox.SelectedIndex = Banner.InitialTime.Minutes;
            endHourComboBox.SelectedIndex = Banner.EndTime.Hours;
            endMinComboBox.SelectedIndex = Banner.EndTime.Minutes;
            if (Banner.Source.GetType() == typeof(TextSourceDTO))
            {
                sourceComboBox.SelectedItem = TEXT_SOURCE;

                TextSourceDTO text = (TextSourceDTO)Banner.Source;
                textSourceTextBox.Text = text.Data;
                pictureBox1.Visible = false;
                rSSSourceLabel.Visible = false;

            }

            if (Banner.Source.GetType() == typeof(RSSSourceDTO))
            {
                RSSSource = (RSSSourceDTO)Banner.Source;
                sourceComboBox.SelectedItem = RSS_SOURCE;
                pictureBox1.Visible = true;
                rSSSourceLabel.Visible = true;
                rSSSourceLabel.Text = string.Format(
                    "Id: {1}{0} Descripción: {2}{0} Url: {3}{0}",
                    Environment.NewLine,
                    RSSSource.Id,
                    RSSSource.Description,
                    RSSSource.Url
                );
            }
            sourceComboBox_SelectedIndexChanged(sourceComboBox, EventArgs.Empty);

        }


        /// <summary>
        /// Guarda el banner con los valores especificados en la vista al modelo
        /// </summary>
        private void saveBanner()
        {
            Banner.Name = nameTextBox.Text;
            Banner.Description = descTextBox.Text;
            Banner.InitialDate = initDateTimePicker.Value;
            Banner.EndDate = endDateTimePicker.Value;
            Banner.InitialTime = new TimeSpan(Convert.ToInt32(initHourComboBox.Text), Convert.ToInt32(initMinComboBox.Text), 0);
            Banner.EndTime = new TimeSpan(Convert.ToInt32(endHourComboBox.Text), Convert.ToInt32(endMinComboBox.Text), 0);
            switch (sourceComboBox.SelectedItem)
            {
                case TEXT_SOURCE:

                    TextSourceDTO text = new TextSourceDTO();
                    text.Data = textSourceTextBox.Text;
                    Banner.Source = text;
                    break;
                case RSS_SOURCE:
                    Banner.Source = new RSSSourceDTO();
                    Banner.Source = RSSSource;
                    break;
            }
        }

        /// <summary>
        /// Guarda el banner de la vista si se cumplen las validaciones
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
                        saveBanner();
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    catch (Exception ex)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al guardar").ShowDialog();
                    }

                }
            }

        }

                //  Validaciones de campos


        private void nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (nameTextBox.Text.Length == 0)
            {
                error = "Ingrese nombre del banner.";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);
        }

        private void descTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (descTextBox.Text.Length == 0)
            {
                error = "Ingrese una descripción del banner.";
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

        private void selectRSSButton_Validating(object sender, CancelEventArgs e)
        {
            string error = null;

            if (sourceComboBox.SelectedItem.ToString() == RSS_SOURCE && Banner.Source == null)
            {
                error = "Debe seleccionar una fuente RSS";
                e.Cancel = true;
            }

            errorProvider.SetError((Control)sender, error);

        }

        private void textSourceTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (textSourceTextBox.Visible == true)
            {
                string error = null;

                if (textSourceTextBox.Text.Length == 0)
                {
                    error = "Debe Ingresar un texto para el banner";
                    e.Cancel = true;
                }

                errorProvider.SetError((Control)sender, error);
            }

        }

        // Funciones auxiliares de comparacion


        /// <summary>
        /// Verifica si la fecha de inicio es anterior a la hora de fin
        /// </summary>
        /// <returns></returns>
        private bool compareDates()
        {
            return initDateTimePicker.Value.CompareTo(endDateTimePicker.Value) > 0;
        }



        /// <summary>
        /// Verifica si la hora de inicio es anterior a la hora de fin
        /// </summary>
        /// <returns></returns>
        private bool compareTimes()
        {
            TimeSpan initTime = new TimeSpan(Convert.ToInt32(initHourComboBox.SelectedItem), Convert.ToInt32(initMinComboBox.SelectedItem), 0);

            TimeSpan endTime = new TimeSpan(Convert.ToInt32(endHourComboBox.SelectedItem), Convert.ToInt32(endMinComboBox.SelectedItem), 0);

            return initTime.CompareTo(endTime) > 0;
        }


        /// <summary>
        /// Verifica si se modifico el form para obtener confirmación al cerrar
        /// </summary>
        /// <returns></returns>
        private bool anyChange()
        {
            bool change = false;
            change = (Banner.Name != nameTextBox.Text) ? true : change;
            change = (Banner.Description != descTextBox.Text) ? true : change;
            change = (Banner.InitialDate != initDateTimePicker.Value) ? true : change;
            change = (Banner.EndDate != endDateTimePicker.Value) ? true : change;
            change = (Banner.InitialTime != new TimeSpan(Convert.ToInt32(initHourComboBox.SelectedItem), Convert.ToInt32(initMinComboBox.SelectedItem), 0)) ? true : change;
            change = (Banner.EndTime != new TimeSpan(Convert.ToInt32(endHourComboBox.SelectedItem), Convert.ToInt32(endMinComboBox.SelectedItem), 0)) ? true : change;
            if (Banner.Source is TextSourceDTO)
            {
                TextSourceDTO textSource = (TextSourceDTO)Banner.Source;
                change = (textSource.Data != textSourceTextBox.Text) ? true : change;
            }
            if (Banner.Source is RSSSourceDTO)
            {
                RSSSourceDTO source = (RSSSourceDTO)Banner.Source;
                change = (source.Id != RSSSource.Id) ? true : change;
            }
            return change;
        }

    }

}
