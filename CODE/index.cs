using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZARATESYSTEM
{
    public partial class Form1 : Form
    {
        int startpoint = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 5;
            bunifuCircleProgressbar1.Value = startpoint;
            if(bunifuCircleProgressbar1.Value == 100)
            {
                bunifuCircleProgressbar1.Value = 0;
                timer1.Stop();
                login login = new login();
                this.Hide();
                login.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }
    }
}
