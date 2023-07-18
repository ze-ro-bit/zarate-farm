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

namespace ZSMS
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "SELECT * FROM userAccountInfo WHERE accountID= '" + txtLogin.Text + "'  AND accountPassword = '" + txtPassword.Text + "' COLLATE Latin1_General_CS_AS";
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dat = new DataTable();
            adap.Fill(dat);


            if (dat.Rows.Count > 0)
            {
                for (int i = 0; i < dat.Rows.Count; i++)
                {
                    if (dat.Rows[i]["accountPosition"].ToString() == "ADMIN")
                    {
                        ADMIN.adminDashboard frm = new ADMIN.adminDashboard();
                        this.Hide();
                        frm.ShowDialog();

                    }
                    if (dat.Rows[i]["accountPosition"].ToString() == "STAFF")
                    {
                        if (dat.Rows[i]["accountStatus"].ToString() == "ACTIVE")
                        {
                            SECRETARY.secDashboard frm = new SECRETARY.secDashboard();
                            this.Hide();
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("USER INACTIVE");
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("INVALID ACCOUNT");
            }
        }
    }
}
