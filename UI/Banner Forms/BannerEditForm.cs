using DigitalSignage.DTO;
using DigitalSignage.UI.RSS_Forms;
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
    public partial class BannerEditForm : Form
    {
        private BannerDTO iBanner;
        private RSSSourceDTO iRSSSource;

        //constantes para los tipos de fuentes
        const string RSS_SOURCE = "Fuente RSS";
        const string TEXT_SOURCE = "Fuente de texto";


        public BannerEditForm(BannerDTO pBanner)
        {
            InitializeComponent();
            comboBox5.Items.Add(RSS_SOURCE);
            comboBox5.Items.Add(TEXT_SOURCE);
            if (pBanner != null)
            {
                this.Banner = pBanner;
                this.loadBanner();
            }
            else
            {
                this.Banner = new BannerDTO();
                // Opcion de cargar fuente de texto
                comboBox5.SelectedIndex = 0;

            }
        }

        public BannerDTO Banner { get => iBanner; set => iBanner = value; }
        public RSSSourceDTO RSSSource { get => iRSSSource; set => iRSSSource = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea cancelar las operaciones realizadas? se perderan los cambios",
                                     "Cancelar");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void BannerEditForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox5.SelectedItem)
            {
                case TEXT_SOURCE:
                    this.textBox1.Visible = true;
                    this.button2.Visible = false;
                    break;
                case RSS_SOURCE:
                    this.textBox1.Visible = false;
                    this.button2.Visible = true;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Banner.Source != null)
            {
                RSSManageForm rSSManageForm = new RSSManageForm(this.Banner.Source.Id);

                if (rSSManageForm.ShowDialog() == DialogResult.OK)
                {
                    this.RSSSource = rSSManageForm.RSSSourceSelected;
                }
            } else
            {
                RSSManageForm rSSManageForm = new RSSManageForm(0);

                if (rSSManageForm.ShowDialog() == DialogResult.OK)
                {
                    this.RSSSource = rSSManageForm.RSSSourceSelected;
                }
            }

        }

        private void loadBanner()
        {
            idLabel.Visible = true;
            idValueLabel.Visible = true;
            idValueLabel.Text = this.Banner.Id.ToString();
            nameTextBox.Text = this.Banner.Name;
            descTextBox.Text = this.Banner.Description;
            initDateTimePicker.Value = this.Banner.InitialDate;
            endDateTimePicker.Value = this.Banner.EndDate;
            comboBox1.SelectedIndex = this.Banner.InitialTime.Hours;
            comboBox2.SelectedIndex = this.Banner.InitialTime.Minutes;
            comboBox3.SelectedIndex = this.Banner.EndTime.Hours;
            comboBox4.SelectedIndex = this.Banner.EndTime.Minutes;
            if (this.Banner.GetType() == typeof(TextSourceDTO))
            {
                TextSourceDTO text = (TextSourceDTO)this.Banner.Source;
                this.textBox1.Text = text.Data;
                this.pictureBox1.Visible = false;

            }

            if (this.Banner.Source.GetType() == typeof(RSSSourceDTO))
            {
                comboBox5.SelectedItem = RSS_SOURCE;
                this.comboBox5_SelectedIndexChanged(comboBox5, EventArgs.Empty);

                RSSSourceDTO rssSource = (RSSSourceDTO)this.Banner.Source;
                this.pictureBox1.Visible = true;

            }
        }

        private void saveBanner()
        {
            this.Banner.Name = nameTextBox.Text;
            this.Banner.Description = descTextBox.Text;
            this.Banner.InitialDate = initDateTimePicker.Value;
            this.Banner.EndDate = endDateTimePicker.Value;
            this.Banner.InitialTime = new TimeSpan(Convert.ToInt32(comboBox1.Text), Convert.ToInt32(comboBox2.Text), 0);
            this.Banner.EndTime = new TimeSpan(Convert.ToInt32(comboBox3.Text), Convert.ToInt32(comboBox4.Text), 0);
            switch (comboBox5.SelectedItem)
            {
                case TEXT_SOURCE:

                    TextSourceDTO text = new TextSourceDTO();
                    text.Data = this.textBox1.Text;
                    this.Banner.Source = text;
                    break;
                case RSS_SOURCE:
                    this.Banner.Source = this.RSSSource;
                    break;
            }
        }
    }
}
