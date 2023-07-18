using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZSMS.ADMIN
{
    public partial class adminDashboard : Form
    {
        bool sidebarExpand;
        public adminDashboard()
        {
            InitializeComponent();
        }

        private void sideTimer_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            sideTimer.Start();
        }

        private void sideTimer_Tick_1(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sideContainer.Width -= 10;
                if (sideContainer.Width == sideContainer.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sideTimer.Stop();
                }
            }
            else
            {
                sideContainer.Width += 10;
                if (sideContainer.Width == sideContainer.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sideTimer.Stop();
                }
            }
        }


        public void nav(Form form, Panel panel)
        {
            form.TopLevel = false;
            form.Size = panel.Size;
            panel.Controls.Clear();
            panel.Controls.Add(form);
            form.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            DASHBOARD.dashboardPanel dashboardPanel = new DASHBOARD.dashboardPanel();
            nav(dashboardPanel, main);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            ACCOUNTS.accountsPanel accountsPanel = new ACCOUNTS.accountsPanel();
            nav(accountsPanel, main);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            CUSTOMER.customerPanel customerPanel = new CUSTOMER.customerPanel();
            nav(customerPanel, main);
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            STOCKS.stocksMonitoringPanel stocksMonitoringPanel = new STOCKS.stocksMonitoringPanel();
            nav(stocksMonitoringPanel, main);
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            ORDERS.ordersPanel ordersPanel = new ORDERS.ordersPanel();
            nav(ordersPanel, main);
        }

    }
}
