using DigitalSignage.BLL;
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
    /// <summary>
    /// Form de gestion de fuentes RSS
    /// </summary>
    public partial class RSSManageForm : Form
    {
        /// <summary>
        /// Instancia del servicio de fuentes RSS - FALTA IMPLEMENTAR CONTAINER IOC
        /// </summary>
        private RSSSourceService iRSSSourceService;

        /// <summary>
        /// Lista de fuentes RSS
        /// </summary>
        private IEnumerable<RSSSourceDTO> iRSSSources;

        public IEnumerable<RSSSourceDTO> RSSSources { get => iRSSSources; set => iRSSSources = value; }

        /// <summary>
        /// Constructor, Obtiene la lista de fuentes RSS y las carga en la vista
        /// </summary>
        public RSSManageForm()
        {
            InitializeComponent();
            this.iRSSSourceService = new RSSSourceService();
            this.RSSSources = this.iRSSSourceService.GetAll();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this.RSSSources;
        }

        /// <summary>
        /// Cierra el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Abre el form para creacion de una nueva fuente RSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            RSSEditForm rSSEditForm = new RSSEditForm(null);
            rSSEditForm.ShowDialog();
        }

        /// <summary>
        /// Abre form para edicion de una fuente RSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, EventArgs e)
        {
            RSSEditForm rSSEditForm = new RSSEditForm((RSSSourceDTO)this.dataGridView1.SelectedRows[0].DataBoundItem);
            rSSEditForm.ShowDialog();
        }
    }
}
