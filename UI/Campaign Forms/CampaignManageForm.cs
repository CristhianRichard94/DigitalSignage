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

        public CampaignManageForm()
        {
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
            new CampaignEditForm(Convert.ToInt32(this.campaignsGridView.CurrentRow.Cells["Id"].Value.ToString())).ShowDialog();
        }

        private void campaignsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void createButton_Click(object sender, EventArgs e)
        {
            new CampaignEditForm(-1).ShowDialog();
        }



        public void getCampaigns()
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todas las campañas":
                    campaignsGridView.DataSource = this.iCampaignService.GetAll();
                    break;
                case "Buscar por nombre":
                    try
                    {
                        IEnumerable<CampaignDTO> resultCampaigns = this.iCampaignService.getCampaignsByName(searchTextBox.Text);
                        if (resultCampaigns.Count() == 0)
                        {
                            MessageBox.Show("No se ha encontrado ninguna campaña con el nombre ingresado.");
                        }
                        campaignsGridView.DataSource = resultCampaigns;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error: "+ exc.Message);
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
                        MessageBox.Show("No se ha encontrado la campaña con el ID ingresado.");
                    }
                    else
                    {
                        campaigns.Add(resultCampaign);
                    }
                    campaignsGridView.DataSource = campaigns;
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
    }
}
