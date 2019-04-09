using DigitalSignage.BLL;
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
        /// Instancia del servicio de fuentes RSS - FALTA IMPLEMENTAR CONTAINER IOC
        /// </summary>
        private readonly IRSSSourceService iRSSSourceService;

        private RSSSourceDTO iRSSSourceSelected;

        /// <summary>
        /// Lista de fuentes RSS
        /// </summary>
        private IEnumerable<RSSSourceDTO> iRSSSources;
        string ALL_SOURCES = "Mostrar todas las Fuentes";
        string ID_SOURCE = "Buscar por ID";
        private StandardKernel kernel;

        public IEnumerable<RSSSourceDTO> RSSSources { get => iRSSSources; set => iRSSSources = value; }
        public RSSSourceDTO RSSSourceSelected { get => iRSSSourceSelected; set => iRSSSourceSelected = value; }

        /// <summary>
        /// Constructor, Obtiene la lista de fuentes RSS y las carga en la vista
        /// </summary>
        public RSSManageForm(IRSSSourceService pRSSSourceService ,int pId)
        {
            InitializeComponent();
            this.iRSSSourceService = pRSSSourceService;
            this.searchComboBox.Items.Add(ALL_SOURCES);
            this.searchComboBox.Items.Add(ID_SOURCE);

            // Opcion de mostrar todas las fuentes
            this.searchComboBox.SelectedIndex = 0;
            this.rSSGridView1.AutoGenerateColumns = false;
            // Caso de que se inicie desde pantalla principal 
            if (pId == -1)
            {
                this.button2.Visible = true;
                this.button1.Visible = false;
                this.button3.Visible = false;
                this.label1.Visible = false;
            } else // Caso de que se inicie desde seleccionar fuente en banner, sin una fuente previa
            {
                this.button2.Visible = false;
                this.button1.Visible = true;
                this.button3.Visible = true;
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
        /// Cierra el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.RSSSourceSelected = (RSSSourceDTO)this.rSSGridView1.SelectedRows[0].DataBoundItem;
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
        private void button2_Click(object sender, EventArgs e)
        {
            this.kernel = HomeForm.CreateKernel();
            this.kernel.Load(Assembly.GetExecutingAssembly());
            var rSSReader = this.kernel.Get<IRSSReader>();
            RSSEditForm rSSEditForm = new RSSEditForm(rSSReader,null);

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
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al crear la fuente RSS").ShowDialog();
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
            RSSEditForm rSSEditForm = new RSSEditForm(rSSReader,(RSSSourceDTO)this.rSSGridView1.SelectedRows[0].DataBoundItem);
            if (rSSEditForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.iRSSSourceService.Create(rSSEditForm.RSSSource);
                    new NotificationForm(MessageBoxButtons.OK, "Se ha modificado la fuente RSS", "Exito al crear la Fuente RSS").ShowDialog();
                    this.getSources();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al crear la fuente RSS").ShowDialog();
                }
            }
        }

        public void getSources()
        {
            switch (searchComboBox.SelectedItem)
            {
                case "Mostrar todas las Fuentes":
                    this.RSSSources = this.iRSSSourceService.GetAll();
                    rSSGridView1.DataSource = this.RSSSources;
                    break;
                case "Buscar por ID":
                    List<RSSSourceDTO> sources= new List<RSSSourceDTO>();
                    RSSSourceDTO resultSource = this.iRSSSourceService.Get(Convert.ToInt32(searchTextBox.Text));
                    if (resultSource.Url == null)
                    {
                        new NotificationForm(MessageBoxButtons.OK, "No se ha encontrado la campaña con el ID ingresado.", "Error en la búsqueda").ShowDialog();

                    }
                    else
                    {
                        sources.Add(resultSource);
                    }
                    this.RSSSources = sources;
                    rSSGridView1.DataSource = this.RSSSources;
                    break;
            }
        }

        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)searchComboBox.SelectedItem == "Mostrar todas las fuentes")
            {
                searchTextBox.Enabled = false;
            }

            else
            {
                    searchTextBox.Enabled = true;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            getSources();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea eliminar la fuente seleccionada?",
                                    "Eliminar fuente");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                try
                {
                    var selectedSource = (RSSSourceDTO)this.rSSGridView1.SelectedRows[0].DataBoundItem;
                    this.iRSSSourceService.Remove(selectedSource);
                    getSources();
                }
                catch (Exception ex)
                {
                    new NotificationForm(MessageBoxButtons.OK, "Error: " + ex.Message, "Error al eliminar la fuente RSS").ShowDialog();
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea salir sin seleccionar una fuente?",
                         "Cancelar");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rSSGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
