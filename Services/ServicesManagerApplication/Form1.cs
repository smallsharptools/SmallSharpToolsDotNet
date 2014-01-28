using System;
using System.Windows.Forms;
using SmallSharpTools.Services.ServicesManagerClient;

namespace SmallSharpTools.Services.ServicesManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindHostedServices();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Client.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BindHostedServices()
        {
            dataGridView1.DataSource = Client.GetHostedServices();
        }

        private ServicesManagerClient.ServicesManagerClient _client = null;
        public ServicesManagerClient.ServicesManagerClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new ServicesManagerClient.ServicesManagerClient();
                }
                return _client;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostedService hostedService = dataGridView1.CurrentRow.DataBoundItem as HostedService;
            if (hostedService != null)
            {
                if (hostedService.IsActive)
                {
                    toggleServiceToolStripMenuItem.Text = "Stop Service";
                }
                else
                {
                    toggleServiceToolStripMenuItem.Text = "Start Service";
                }
            }

        }

        private void toggleServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleService();
        }

        private void ToggleService()
        {
            HostedService hostedService = dataGridView1.CurrentRow.DataBoundItem as HostedService;
            if (hostedService != null)
            {
                if (hostedService.IsActive)
                {
                    if (hostedService.IsManager)
                    {
                        MessageBox.Show("Unable to stop manager", "Info");
                    }
                    else
                    {
                        Client.StopService(hostedService.Name);
                    }
                }
                else
                {
                    Client.StartService(hostedService.Name);
                }
                BindHostedServices();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ToggleService();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.Show();
        }

        private AboutBox1 _aboutBox1 = null;
        private AboutBox1 AboutBox
        {
            get
            {
                if (_aboutBox1 == null)
                {
                    _aboutBox1 = new AboutBox1();
                }
                return _aboutBox1;
            }
        }
    }
}