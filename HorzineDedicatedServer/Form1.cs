using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorzineDedicatedServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 100)
            {
                progressBar1.Value++;
            }
            else
            {
                timer1.Stop();
                progressBar1.Value = 0;

                this.Hide();

                frmServer form2 = new frmServer();

                form2.Show();
            }
        }


    }
}
