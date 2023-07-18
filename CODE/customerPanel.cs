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

namespace ZSMS.CUSTOMER
{
    public partial class customerPanel : Form
    {
        public customerPanel()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            CUSTOMER.addCustomerPanel addCustomerPanel = new addCustomerPanel();
            addCustomerPanel.ShowDialog();
        }

        public void CustomForm()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT customerInfo.customerID,CONCAT(customerInfo.customerFname, ' ' , customerInfo.customerMname , ' ' , customerInfo.customerLname) AS NAME, customerInfo.customerAddress,customerInfo.customerEntry,customerInfo.customerStatus FROM customerInfo WHERE customerInfo.customerStatus = 'ACTIVE';", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = dtbl;
        }
        private void customerPanel_Load(object sender, EventArgs e)
        {
            CustomForm();
        }
    }
}
