using System;
using System.ComponentModel;
using System.Windows.Forms;
using SmallSharpTools.ImageResizer;

namespace WindowsApplication
{
    public partial class Form1 : Form
    {
        ImageProcessor processor = null;

        public delegate void UpdateProgressBarCallback(int completed, int total);
        public delegate void CancelProcessorCallback();
        
        public Form1()
        {
            InitializeComponent();
        }

        void processor_ProgressChanged(object sender, ProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(
                    new UpdateProgressBarCallback(UpdateProgressBar),
                    new object[] { e.Completed, e.Total }
                );
            }
        }
        
        private void  UpdateProgressBar(int completed, int total)
        {
            if (completed == 0)
            {
                progressBar1.Minimum = completed;
                progressBar1.Maximum = total;
                progressBar1.Value = 0;
                progressBar1.Step = 1;
            }
            else
            {
                progressBar1.PerformStep();
                toolStripStatusLabel1.Text = String.Format(
                    "Processing {0} of {1}...", completed, total);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            processor.Canceled = true;
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // only allow closing when canceled or copmleted
            if (!processor.Completed && !processor.Canceled)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            processor = ImageProcessor.Instance;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
            {
                string directory = args[1];
                processor.ProgressChanged += new EventHandler<ProgressEventArgs>(processor_ProgressChanged);
                processor.Run(directory);
            }
            processor.Completed = true;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
        
    }
}