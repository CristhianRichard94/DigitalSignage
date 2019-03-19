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
    public partial class CampaignManageForm : Form
    {
        private CampaignService iCampaignService;
        private IEnumerable<CampaignDTO> iCampaigns;
        public CampaignManageForm()
        {
            iCampaigns = new List<CampaignDTO>();
            this.iCampaignService = new CampaignService();
            InitializeComponent();
            campaignsGridView.AutoGenerateColumns = false;

            // Opcion de mostrar todas las campañas
            searchComboBox.SelectedIndex = 0;

            getCampaigns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CampaignForm_Load(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            CampaignEditForm cef = new CampaignEditForm((CampaignDTO)campaignsGridView.SelectedRows[0].DataBoundItem);
            if (cef.ShowDialog(this) == DialogResult.OK)
            {
                try
                {

                    this.iCampaignService.Update(cef.Campaign);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha modificado la campaña", "Exito al modificar la campaña").ShowDialog();
                    getCampaigns();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al modificar la campaña").ShowDialog();
                }
            }
        }

        private void campaignsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void createButton_Click(object sender, EventArgs e)
        {
            CampaignEditForm cef = new CampaignEditForm(null);
            if (cef.ShowDialog(this) == DialogResult.OK)
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



        public void getCampaigns()
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todas las campañas":
                    this.iCampaigns = this.iCampaignService.GetAll();
                    campaignsGridView.DataSource = this.iCampaigns;
                    break;
                case "Buscar por nombre":
                    try
                    {
                        IEnumerable<CampaignDTO> resultCampaigns = this.iCampaignService.getCampaignsByName(searchTextBox.Text);
                        if (resultCampaigns.Count() == 0)
                        {
                            new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ninguna campaña con el nombre ingresado.", "Error en la búsqueda").ShowDialog();
                        }
                        this.iCampaigns = resultCampaigns;
                        campaignsGridView.DataSource = this.iCampaigns;
                    }
                    catch (Exception exc)
                    {
                        new NotificationForm(MessageBoxButtons.OK,exc.Message, "Error").ShowDialog();
                    }

                    break;
                case "Buscar por fecha":
                    // Implementar Buscar por fecha
                    //campaignsGridView.DataSource = this.iCampaignService.GetCampaignsActiveInDate(searchDateTimePicker.Value);
                    break;
                case "Buscar por ID":
                    List<CampaignDTO> campaigns = new List<CampaignDTO>();
                    CampaignDTO resultCampaign = this.iCampaignService.Get(Convert.ToInt32(searchTextBox.Text));
                    if (resultCampaign.Name == null)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado la campaña con el ID ingresado.", "Error en la búsqueda").ShowDialog();

                    }
                    else
                    {
                        campaigns.Add(resultCampaign);
                    }
                    this.iCampaigns = campaigns;
                    campaignsGridView.DataSource = this.iCampaigns;
                    break;
            }
        }

        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(searchComboBox.SelectedItem)
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
        
        private void searchButton_Click(object sender, EventArgs e)
        {
            //Comprobaciones del campo de busqueda
            getCampaigns();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea eliminar la imagen seleccionada?",
                                    "Cancelar");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                var selectedCampaign = (CampaignDTO)this.campaignsGridView.SelectedRows[0].DataBoundItem;
                this.iCampaignService.Remove(selectedCampaign);
                getCampaigns();
            }
        }
    }
}
