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

namespace ZSMS.ORDERS
{
    public partial class ordersPanel : Form
    {
        public ordersPanel()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            ORDERS.orderFormPanel orderFormPanel = new orderFormPanel();
            orderFormPanel.ShowDialog();
        }

        public void CustomForm()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT orderSummaryInfo.referenceID,orderSummaryInfo.customerID,CONCAT(customerInfo.customerFname, ' ' , customerInfo.customerMname , ' ' , customerInfo.customerLname) AS NAME, orderSummaryInfo.totalPayment,orderSummaryInfo.purchaseDate,orderSummaryInfo.orderStatus FROM orderSummaryInfo INNER JOIN customerInfo ON customerInfo.customerID = orderSummaryInfo.customerID", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = dtbl;
        }

        private void ordersPanel_Load(object sender, EventArgs e)
        {
            CustomForm();
        }
    }
}
