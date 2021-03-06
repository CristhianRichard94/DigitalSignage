﻿using DigitalSignage.BLL;
using DigitalSignage.BLL.RSSReader;
using DigitalSignage.DTO;
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

namespace DigitalSignage.UI.RSS_Forms
{
    /// <summary>
    /// Form de gestion de fuentes RSS
    /// </summary>
    public partial class RSSManageForm : Form
    {
        /// <summary>
        /// Instancia del servicio de fuentes RSS
        /// </summary>
        private readonly IRSSSourceService iRSSSourceService;

        private RSSSourceDTO iRSSSourceSelected;

        /// <summary>
        /// Lista de fuentes RSS
        /// </summary>
        private IEnumerable<RSSSourceDTO> iRSSSources;
        private StandardKernel kernel;

        public IEnumerable<RSSSourceDTO> RSSSources { get => iRSSSources; set => iRSSSources = value; }
        public RSSSourceDTO RSSSourceSelected { get => iRSSSourceSelected; set => iRSSSourceSelected = value; }

        /// <summary>
        /// Constructor, Obtiene la lista de fuentes RSS y las carga en la vista
        /// </summary>
        public RSSManageForm(IRSSSourceService pRSSSourceService, int pId)
        {
            InitializeComponent();
            this.iRSSSourceService = pRSSSourceService;

            // Opcion de mostrar todas las fuentes
            this.searchComboBox.SelectedIndex = 0;
            this.rSSDataGridView.AutoGenerateColumns = false;
            // Caso de que se inicie desde pantalla principal 
            if (pId == -1)
            {
                this.cancelButton.Visible = true;
                this.selectButton.Visible = false;
                this.cancelSelectButton.Visible = false;
                this.label1.Visible = false;
            }
            else // Caso de que se inicie desde seleccionar fuente en banner, sin una fuente previa
            {
                this.cancelButton.Visible = false;
                this.selectButton.Visible = true;
                this.cancelSelectButton.Visible = true;
                this.label1.Visible = true;
                if (pId > 0) // Caso de que se inicie desde seleccionar fuente en banner, con una fuente previa
                {
                    this.searchTextBox.Text = pId.ToString();
                    this.searchComboBox.SelectedIndex = 1;
                }
            }
            this.getSources();

        }

        /// <summary>
        /// Selecciona la fuente RSS para un banner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.RSSSourceSelected = (RSSSourceDTO)this.rSSDataGridView.SelectedRows[0].DataBoundItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception exc)
            {
                new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
            }

        }

        /// <summary>
        /// Abre el form para creacion de una nueva fuente RSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click(object sender, EventArgs e)
        {
            this.kernel = HomeForm.CreateKernel();
            this.kernel.Load(Assembly.GetExecutingAssembly());
            var rSSReader = this.kernel.Get<IRSSReader>();
            RSSEditForm rSSEditForm = new RSSEditForm(rSSReader, null);

            if (rSSEditForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.iRSSSourceService.Create(rSSEditForm.RSSSource);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha creado la fuente RSS", "Exito al crear la Fuente RSS").ShowDialog();
                    this.getSources();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error").ShowDialog();
                }
            }


        }

        /// <summary>
        /// Abre form para edicion de una fuente RSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, EventArgs e)
        {
            this.kernel = HomeForm.CreateKernel();
            this.kernel.Load(Assembly.GetExecutingAssembly());
            var rSSReader = this.kernel.Get<IRSSReader>();
            RSSEditForm rSSEditForm = new RSSEditForm(rSSReader, (RSSSourceDTO)this.rSSDataGridView.SelectedRows[0].DataBoundItem);
            if (rSSEditForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.iRSSSourceService.Update(rSSEditForm.RSSSource);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha modificado la fuente RSS", "Exito al crear la Fuente RSS").ShowDialog();
                    this.getSources();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al crear la fuente RSS").ShowDialog();
                }
            }
        }

        /// <summary>
        /// Obtiene las fuentes en base a la opcion y valor de busqueda ingresados
        /// </summary>
        public void getSources()
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todas las fuentes RSS":
                    this.RSSSources = this.iRSSSourceService.GetAll();
                    rSSDataGridView.DataSource = this.RSSSources;
                    if (this.RSSSources.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No hay fuentes cargadas en el sistema.", "No hay fuentes").ShowDialog();
                    }
                    break;
                case "Buscar por ID":
                    List<RSSSourceDTO> sources = new List<RSSSourceDTO>();
                    RSSSourceDTO resultSource = this.iRSSSourceService.Get(Convert.ToInt32(searchTextBox.Text));
                    if (resultSource != null)
                    {
                        sources.Add(resultSource);
                    }
                    else
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ninguna fuente con el ID ingresado.", "Error en la búsqueda").ShowDialog();
                    }
                    this.RSSSources = sources;
                    rSSDataGridView.DataSource = this.RSSSources;
                    break;
                case "Buscar por URL":
                    this.RSSSources = this.iRSSSourceService.GetSourcesByURL(searchTextBox.Text);
                    if (this.RSSSources.Count() == 0)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado ninguna fuente con la URL ingresada.", "Error en la búsqueda").ShowDialog();
                    }
                    rSSDataGridView.DataSource = this.RSSSources;
                    break;
            }
            checkRowCount();
        }

        
        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)searchComboBox.SelectedItem == "Mostrar todas las fuentes RSS")
            {
                searchTextBox.Enabled = false;
            }

            else
            {
                searchTextBox.Enabled = true;
            }
        }

        /// <summary>
        /// Ordena la busqueda de fuentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    getSources();
                }
                catch (Exception exc)
                {
                    new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
                }
            }
        }

        /// <summary>
        /// Solicita la eliminacion de la fuente seleccionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea eliminar la fuente seleccionada?",
                                    "Eliminar fuente");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                try
                {
                    var selectedSource = (RSSSourceDTO)this.rSSDataGridView.SelectedRows[0].DataBoundItem;
                    this.iRSSSourceService.Remove(selectedSource);
                    getSources();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al eliminar la fuente RSS").ShowDialog();
                }

            }
        }

        /// <summary>
        /// Cancela la seleccion de fuente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelSelectButton_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea salir sin seleccionar una fuente?",
                         "Cancelar");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Cierra el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


                        // Validaciones de campos


        private void searchTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            switch (searchComboBox.SelectedItem)
            {
                case "Buscar por URL":
                    if (searchTextBox.Text.Length == 0)
                    {
                        error = "Ingrese una parte de la URL de fuente a buscar.";
                        e.Cancel = true;
                    }
                    break;
                case "Buscar por ID":
                    if (searchTextBox.Text.Length == 0)
                    {
                        error = "Ingrese un ID de fuente a buscar.";
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


        /// <summary>
        /// Verifica si hay una fila seleccionada y sino deshabilita botones de editar y eliminar
        /// </summary>
        private void checkRowCount()
        {
            if (rSSDataGridView.RowCount == 0)
            {
                editButton.Enabled = false;
                deleteButton.Enabled = false;
                selectButton.Enabled = false;
            }
            else
            {
                editButton.Enabled = true;
                deleteButton.Enabled = true;
                selectButton.Enabled = true;
            }
        }
    }
}
