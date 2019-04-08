using DigitalSignage.DTO;
using DigitalSignage.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI.Banner_Forms
{
    public partial class BannerManageForm : Form
    {

        private readonly IBannerService iBannerService;
        private IEnumerable<BannerDTO> iBanners;

        public BannerManageForm(IBannerService pBannerService)
        {
            this.iBannerService = pBannerService;
            InitializeComponent();

            iBanners = new List<BannerDTO>();
            bannersGridView.AutoGenerateColumns = false;

            // Opcion de mostrar todas las campañas
            searchComboBox.SelectedIndex = 0;

            this.getBanners();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BannerEditForm bef = new BannerEditForm((BannerDTO)bannersGridView.SelectedRows[0].DataBoundItem);
            if(bef.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.iBannerService.Update(bef.Banner);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha modificado el banner", "Exito al modificar el banner").ShowDialog();
                    getBanners();
            }
                catch (Exception ex)
            {
                new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al modificar el banner").ShowDialog();
            }
        }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BannerEditForm bef = new BannerEditForm(null);
            if (bef.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.iBannerService.Create(bef.Banner);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha creado el banner", "Exito al crear el banner").ShowDialog();
                    getBanners();
            }
                catch (Exception ex)
            {
                new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al crear el banner").ShowDialog();
            }
        }
        }



        private void deleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea eliminar el banner seleccionado?",
                                    "Eliminar Banner");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                var selectedBanner = (BannerDTO)this.bannersGridView.SelectedRows[0].DataBoundItem;
                this.iBannerService.Remove(selectedBanner);
                getBanners();
            }
        }

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
        public void getBanners()
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todas las campañas":
                    this.iBanners = this.iBannerService.GetAll();
                    bannersGridView.DataSource = this.iBanners;
                    break;
                case "Buscar por nombre":
                    try
                    {
                        IEnumerable<BannerDTO> resultBanners= this.iBannerService.getBannersByName(searchTextBox.Text);
                        if (resultBanners.Count() == 0)
                        {
                            new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ninguna campaña con el nombre ingresado.", "Error en la búsqueda").ShowDialog();
                        }
                        this.iBanners = resultBanners;
                        bannersGridView.DataSource = this.iBanners;
                    }
                    catch (Exception exc)
                    {
                        new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
                    }

                    break;
                case "Buscar por fecha":
                    try
                    {
                        IEnumerable<BannerDTO> resultCampaigns = this.iBannerService.getBannersActiveInDate(this.searchDateTimePicker.Value);
                        if (resultCampaigns.Count() == 0)
                        {
                            new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ningun banner en la fecha ingresada.", "Error en la búsqueda").ShowDialog();
                        }
                        this.iBanners = resultCampaigns;
                        this.bannersGridView.DataSource = this.iBanners;
                    }
                    catch (Exception exc)
                    {
                        new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
                    }
                    break;
                case "Buscar por ID":
                    List<BannerDTO> banners = new List<BannerDTO>();
                    BannerDTO resultBanner = this.iBannerService.Get(Convert.ToInt32(searchTextBox.Text));
                    if (resultBanner.Name == null)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ningún banner con el ID ingresado.", "Error en la búsqueda").ShowDialog();
                    }
                    else
                    {
                        banners.Add(resultBanner);
                    }
                    this.iBanners = banners;
                    bannersGridView.DataSource = this.iBanners;
                    break;
            }
        }
    }
}
