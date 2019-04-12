using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalSignage.UI.Banner_Forms;
using DigitalSignage.UI.Campaign_Forms;
using DigitalSignage.UI.Properties;
using DigitalSignage.UI.Operative_Form;
using DigitalSignage.UI.RSS_Forms;
using Ninject;
using System.Reflection;
using DigitalSignage.BLL;
using Ninject.Modules;

namespace DigitalSignage.UI
{
    /// <summary>
    /// Form de inicio, da acceso a las demas vistas
    /// </summary>
    public partial class HomeForm : Form
    {
        /// <summary>
        /// Booleano para el dropdown del boton de gestion
        /// </summary>
        private bool isCollapsed = true;

        /// <summary>
        /// instancia del kernel que carga los servicios 
        /// </summary>
        private StandardKernel kernel;

        public HomeForm()
        {
            this.kernel = CreateKernel();
            // load the bindings from the executing assembly
            kernel.Load(Assembly.GetExecutingAssembly());

            InitializeComponent();
        }

        /// <summary>
        /// Cierra la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Timer usado para desplegar y comprimir la lista de opciones de gestion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Despliega lista
            if (isCollapsed)
            {
                manageOptions.Image = Resources.dropdownarrow2;
                dropdownPanel.Height += 10;
                if (dropdownPanel.Size == dropdownPanel.MaximumSize)
                {
                    timer1.Stop();
                    this.isCollapsed = false;
                }
            }
            // Colapsa lista
            else
            {
                manageOptions.Image = Resources.dropdownarrow1;
                dropdownPanel.Height -= 10;
                if (dropdownPanel.Size == dropdownPanel.MinimumSize)
                {
                    timer1.Stop();
                    this.isCollapsed = true;
                }
            }
        }

        /// <summary>
        /// Dispara el timer para desplegar o comprimir la lista de opciones de gestion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manageOptions_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        /// <summary>
        /// Abre form de la vista operativa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void operativeScreen_Click(object sender, EventArgs e)
        {
                var campaignService = kernel.Get<ICampaignService>();
                var bannerService = kernel.Get<IBannerService>();
                new OperativeForm(campaignService, bannerService).ShowDialog();

        }

        /// <summary>
        /// Abre form de gestion de campañas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void campManage_Click(object sender, EventArgs e)
        {
            var campaignService = kernel.Get<ICampaignService>();
            CampaignManageForm campaignManageForm = new CampaignManageForm(campaignService);
            campaignManageForm.ShowDialog();

        }

        /// <summary>
        /// Abre form de gestion de banners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bannersManage_Click(object sender, EventArgs e)
        {
            var bannerService = kernel.Get<IBannerService>();
            BannerManageForm bannerManageForm = new BannerManageForm(bannerService);
            bannerManageForm.ShowDialog();

        }


        /// <summary>
        /// Abre form de gestion de fuentes RSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rssManage_Click(object sender, EventArgs e)
        {
            var rSSSourceService = kernel.Get<IRSSSourceService>();
            RSSManageForm rSSManageForm = new RSSManageForm(rSSSourceService,-1);
            rSSManageForm.ShowDialog();

        }

        /// <summary>
        /// Inicializa el kernel que gestiona los servicios
        /// </summary>
        /// <returns></returns>
        public static StandardKernel CreateKernel()
        {
            var modules = new INinjectModule[]
                              {
                              new Bindings()
                              };

            var kernel = new StandardKernel(modules);
            return kernel;
        }
    }
}
