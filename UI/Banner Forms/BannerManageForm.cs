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

        public IEnumerable<BannerDTO> Banners { get => iBanners; set => iBanners = value; }

        public BannerManageForm(IBannerService pBannerService)
        {
            this.iBannerService = pBannerService;
            InitializeComponent();

            Banners = new List<BannerDTO>();
            bannersGridView.AutoGenerateColumns = false;

            // Opcion de mostrar todas las campañas
            searchComboBox.SelectedIndex = 0;
            try
            {
                getBanners();

            }
            catch (Exception ex)
            {
                new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            BannerEditForm bef = new BannerEditForm((BannerDTO)bannersGridView.SelectedRows[0].DataBoundItem);
            if (bef.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.iBannerService.Update(bef.Banner);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha modificado el banner", "Exito al modificar el banner").ShowDialog();
                    getBanners();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
                }
            }
        }

        private void createButton_Click(object sender, EventArgs e)
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
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
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
                try
                {
                    var selectedBanner = (BannerDTO)bannersGridView.SelectedRows[0].DataBoundItem;
                    this.iBannerService.Remove(selectedBanner);
                    getBanners();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
                }
            }
        }

        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todos los banners":
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
                case "Mostrar todos los banners":
                    Banners = this.iBannerService.GetAll();
                    bannersGridView.DataSource = Banners;
                    if (Banners.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No hay banners cargados en el sistema.", "No hay banners").ShowDialog();
                    }
                    break;
                case "Buscar por nombre":
                    Banners = this.iBannerService.getBannersByName(searchTextBox.Text);
                    bannersGridView.DataSource = Banners;
                    if (Banners.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se encontraron banners con el nombre ingresado.", "No hay banners").ShowDialog();
                    }
                    break;
                case "Buscar por fecha":

                    Banners = this.iBannerService.getBannersActiveInDate(searchDateTimePicker.Value);
                    bannersGridView.DataSource = Banners;
                    if (Banners.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se encontraron banners para la fecha especificada.", "No hay banners").ShowDialog();
                    }
                    break;
                case "Buscar por ID":
                    List<BannerDTO> banners = new List<BannerDTO>();
                    BannerDTO resultBanner = this.iBannerService.Get(Convert.ToInt32(searchTextBox.Text));
                    if (resultBanner != null)
                    {
                        banners.Add(resultBanner);
                    }
                    else
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se encontraron banners para el ID especificado.", "No hay banners").ShowDialog();
                    }
                    Banners = banners;
                    bannersGridView.DataSource = Banners;
                    break;
            }
        }

        private void searchTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            switch (searchComboBox.SelectedItem)
            {
                case "Buscar por nombre":
                    if (searchTextBox.Text.Length == 0)
                    {
                        error = "Ingrese un nombre de banner a buscar.";
                        e.Cancel = true;
                    }
                    break;
                case "Buscar por ID":
                    if (searchTextBox.Text.Length == 0)
                    {
                        error = "Ingrese un ID de banner a buscar.";
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    getBanners();
                }
                catch (Exception exc)
                {
                    new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
                }
            }
        }
    }
}
