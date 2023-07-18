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

namespace ZSMS.ACCOUNTS
{
    public partial class accountsPanel : Form
    {
        public accountsPanel()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            ACCOUNTS.addAccountPanel addAccountPanel = new addAccountPanel();
            addAccountPanel.ShowDialog();
        }

        public void CustomForm()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT accountID,CONCAT(accountFname, ' ' , accountMname , ' ' , accountLname) AS NAME, accountAddress,accountPosition,accountEntry,accountStatus FROM userAccountInfo WHERE accountStatus = 'ACTIVE';", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = dtbl;
        }

        private void accountsPanel_Load(object sender, EventArgs e)
        {
            CustomForm();
        }
    }
}

