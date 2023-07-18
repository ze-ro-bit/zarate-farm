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
    public partial class addStocksPanel : Form
    {
        public addStocksPanel()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (txtType.Text.Equals("") || txtMname.Text.Equals("") || txtkilos.Text.Equals(""))
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
                    cmd.CommandText = "Insert into stocksInfo VALUES('" + txtGen.selectedValue + "','" + txtMname.Text + "','" + txtType.Text + "','" + txtkilos.Text + "','" + date + "','AVAILABLE')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("STOCKS ADDED TO SYSTEM SUCCESFULLY");
                    this.Hide();

                    txtType.Text = "";
                    txtMname.Text = "";
                    txtkilos.Text = "";
                }
            }
        }
    }
}
