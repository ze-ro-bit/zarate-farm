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
    public partial class addCustomerPanel : Form
    {
        public addCustomerPanel()
        {
            InitializeComponent();
        }

        private void addCustomerPanel_Load(object sender, EventArgs e)
        {
            String date = DateTime.Now.ToString("yyyyMM");
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            con.Open();
            cmd1.CommandText = "SELECT COUNT(*) FROM customerInfo;";

            int num1 = Convert.ToInt32(cmd1.ExecuteScalar());
            int res = num1 + 1;

            txtacc.Text = date + "-" + "00" + res;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (txtFname.Text.Equals("") || txtMname.Text.Equals("") || txtLname.Text.Equals("") || txtAddress.Text.Equals(""))
            {
                MessageBox.Show("Please Fill In all the fields");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to save the data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    String date = DateTime.Now.ToString("yyyy-MM-dd");
                    //String date1 = DateTime.Now.ToString("yyyy");
                    Random rand = new Random();
                    int number = rand.Next(0, 100);
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "Insert into customerInfo VALUES('" + txtacc.Text + "','" + txtFname.Text + "','" + txtMname.Text + "','" + txtLname.Text + "','" + txtAddress.Text + "','" + date + "','ACTIVE')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("CUSTOMER ADDED TO SYSTEM SUCCESFULLY");
                    this.Hide();

                    txtFname.Text = "";
                    txtLname.Text = "";
                    txtMname.Text = "";
                    txtAddress.Text = "";
                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
