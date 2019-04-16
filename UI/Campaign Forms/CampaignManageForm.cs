using DigitalSignage.BLL;
using DigitalSignage.Domain;
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

namespace DigitalSignage.UI.Campaign_Forms
{
    /// <summary>
    /// Pantalla de gestion de campañas
    /// </summary>
    public partial class CampaignManageForm : Form
    {
        /// <summary>
        /// instancia del servicio de campañas
        /// </summary>
        private readonly ICampaignService iCampaignService;

        /// <summary>
        /// Lista de campañas
        /// </summary>
        private IEnumerable<CampaignDTO> iCampaigns;

        public IEnumerable<CampaignDTO> Campaigns { get => iCampaigns; set => iCampaigns = value; }


        /// <summary>
        /// Constructor, obtiene todas las campañas y las muestra
        /// </summary>
        public CampaignManageForm(ICampaignService pCampaignService)
        {
            iCampaignService = pCampaignService;
            Campaigns = new List<CampaignDTO>();
            InitializeComponent();
            campaignsDataGridView.AutoGenerateColumns = false;

            // Opcion de mostrar todas las campañas
            searchComboBox.SelectedIndex = 0;
            try
            {
                getCampaigns();
            }
            catch (Exception exc)
            {
                new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
            }
        }

        /// <summary>
        /// Cierra el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Abre form de edicion de una campaña, edita una campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, EventArgs e)
        {
            CampaignEditForm cef = new CampaignEditForm((CampaignDTO)campaignsDataGridView.SelectedRows[0].DataBoundItem);
            if (cef.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.iCampaignService.Update(cef.Campaign);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha modificado la campaña", "Exito al modificar la campaña").ShowDialog();
                    getCampaigns();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
                }
            }
        }

        /// <summary>
        /// Abre form de creacion de una campaña, crea una campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click(object sender, EventArgs e)
        {
            CampaignEditForm cef = new CampaignEditForm(null);
            if (cef.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    this.iCampaignService.Create(cef.Campaign);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha creado la campaña", "Exito al crear la campaña").ShowDialog();

                    getCampaigns();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al crear la campaña").ShowDialog();
                }
            }
        }


        /// <summary>
        /// Obtiene campañas dependiendo de la opcion seleccionada en searchComboBox y el texto en searchTextBox
        /// </summary>
        public void getCampaigns()
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todas las campañas":
                    Campaigns = this.iCampaignService.GetAll();
                    campaignsDataGridView.DataSource = Campaigns;
                    if (Campaigns.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No hay campañas cargadas en el sistema.", "No hay campañas").ShowDialog();
                    }
                    break;
                case "Buscar por nombre":
                    IEnumerable<CampaignDTO> resultCampaigns = this.iCampaignService.getCampaignsByName(searchTextBox.Text);
                    if (resultCampaigns.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ninguna campaña con el nombre ingresado.", "No hay camapñas").ShowDialog();
                    }
                    Campaigns = resultCampaigns;
                    campaignsDataGridView.DataSource = Campaigns;

                    break;
                case "Buscar por fecha":
                    resultCampaigns = this.iCampaignService.GetCampaignsActiveInDate(searchDateTimePicker.Value);
                    if (resultCampaigns.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ninguna campaña en la fecha ingresada.", "No hay camapñas").ShowDialog();
                    }
                    Campaigns = resultCampaigns;
                    campaignsDataGridView.DataSource = Campaigns;
                    break;
                case "Buscar por ID":
                    List<CampaignDTO> campaigns = new List<CampaignDTO>();
                    CampaignDTO resultCampaign = this.iCampaignService.Get(Convert.ToInt32(searchTextBox.Text));
                    if (resultCampaign != null)
                    {
                        campaigns.Add(resultCampaign);
                    } else
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ninguna campaña con el ID especificado.", "No hay camapñas").ShowDialog();

                    }
                    Campaigns = campaigns;
                    campaignsDataGridView.DataSource = Campaigns;
                    break;
            }
        }

        /// <summary>
        /// Cambiar el modo de ingreso de valor de busqueda segun searchCombobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todas las campañas":
                    searchTextBox.Visible = true;
                    searchTextBox.Enabled = false;
                    searchDateTimePicker.Visible = false;
                    break;
                case "Buscar por nombre":
                    searchTextBox.Visible = true;
                    searchTextBox.Enabled = true;
                    searchDateTimePicker.Visible = false;
                    break;
                case "Buscar por fecha":
                    searchTextBox.Visible = false;
                    searchDateTimePicker.Visible = true;
                    break;
                case "Buscar por ID":
                    searchTextBox.Visible = true;
                    searchTextBox.Enabled = true;
                    searchDateTimePicker.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Dispara la busqueda de campañas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    getCampaigns();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, ex.Message, "Error").ShowDialog();
                }
            }
        }


        /// <summary>
        ///  Elimina una campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea eliminar la campaña seleccionada?",
                                    "Eliminar Campaña");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                try
                {
                    var selectedCampaign = (CampaignDTO)campaignsDataGridView.SelectedRows[0].DataBoundItem;
                    this.iCampaignService.Remove(selectedCampaign);
                    getCampaigns();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, ex.Message, "Error").ShowDialog();
                }

            }
        }

        // Validacion del campo de busqueda
        private void searchTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            switch (searchComboBox.SelectedItem)
            {
                case "Buscar por nombre":
                    if (searchTextBox.Text.Length == 0)
                    {
                        error = "Ingrese un nombre de campaña a buscar.";
                        e.Cancel = true;
                    }
                    break;
                case "Buscar por ID":
                    if (searchTextBox.Text.Length == 0)
                    {
                        error = "Ingrese un ID de campaña a buscar.";
                        e.Cancel = true;
                    }
                    else
                    {
                        int result;
                        if (!int.TryParse(searchTextBox.Text, out result))
                        {
                            error = "El ID debe ser un valor numérico.";
                            e.Cancel = true;
                        }
                    }
                    break;
            }
            errorProvider.SetError((Control)sender, error);
        }
    }
}
