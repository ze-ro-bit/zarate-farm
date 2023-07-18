using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZSMS.STOCKS
{
    public partial class stocksMonitoringPanel : Form
    {
        public stocksMonitoringPanel()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            addStocksPanel addStocksPanel = new addStocksPanel();
            addStocksPanel.ShowDialog();
        }

        public void CustomForm()
        {
            String date = DateTime.Now.ToString("yyyy-MM-dd");
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT * FROM stocksInfo WHERE stockStatus = 'AVAILABLE' AND dateEntry = '"+date+"'", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = dtbl;
        }

        public void Logs()
        {
            String date = DateTime.Now.ToString("yyyy-MM-dd");
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT * FROM stocksInfo", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = dtbl;
        }


        public void DailyOut()
        {
            String date = DateTime.Now.ToString("yyyy-MM-dd");
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT * FROM stocksInfo WHERE stockStatus = 'RELEASE'", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = dtbl;
        }

        private void stocksMonitoringPanel_Load(object sender, EventArgs e)
        {
            CustomForm();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            DailyOut();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            CustomForm();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Logs();
        }
    }
}
