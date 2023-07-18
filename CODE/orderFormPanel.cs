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
    public partial class orderFormPanel : Form
    {
        public orderFormPanel()
        {
            InitializeComponent();
        }

        public void TableForm()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT * FROM stocksInfo WHERE stockStatus = 'AVAILABLE';", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtbl;
        }
        private int CalculateColumnSum()
        {
            int sum = 0;

            // Iterate over the rows of the DataGridView
            foreach (DataGridViewRow row in dataTable.Rows)
            {
                // Check if the row is not a new row or a header row
                if (!row.IsNewRow && row.Index != -1)
                {
                    // Retrieve the value from the desired column (assuming it's the first column, change the index if needed)
                    int cellValue;
                    if (int.TryParse(row.Cells[4].Value?.ToString(), out cellValue))
                    {
                        // Add the value to the sum
                        sum += cellValue;
                    }
                }
            }

            return sum;
        }
        public void CustomForm()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataAdapter sqlDATA = new SqlDataAdapter("SELECT orderStatusInfo.referenceID,orderStatusInfo.customerID,CONCAT(customerInfo.customerFname, ' ' , customerInfo.customerMname , ' ' , customerInfo.customerLname) AS NAME,orderStatusInfo.livestockID, orderStatusInfo.serviceFeeAdded,orderStatusInfo.dateOrder,orderStatusInfo.orderStatus FROM orderStatusInfo INNER JOIN customerInfo ON customerInfo.customerID = orderStatusInfo.customerID WHERE orderStatusInfo.customerID = '"+ cmb1.Text + "' AND orderStatus = 'PENDING PAYMENT' ", con);
            DataTable dtbl = new DataTable();
            sqlDATA.Fill(dtbl);
            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = dtbl;
        }

        private void orderFormPanel_Load(object sender, EventArgs e)
        {
            
            TableForm();
            bunifuCards1.Visible = false;
            String date = DateTime.Now.ToString("yyyyMMdd");
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            con.Open();
            cmd1.CommandText = "SELECT COUNT(*) FROM orderSummaryInfo;";

            int num1 = Convert.ToInt32(cmd1.ExecuteScalar());
            int res = num1 + 1;

            txtacc.Text = "REF-"+ date + "-" +"00"+ + res;

            using (SqlConnection con1 = new SqlConnection())
            {
                con1.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT customerID,CONCAT(customerFname,' ',customerMname,' ',customerLname) AS NAME FROM customerInfo", con1))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "Please select";
                    dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.
                    cmb1.DataSource = dt;
                    cmb1.DisplayMember = "customerID";

                }
            }


            
        }

        private void txtFname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void cmb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT customerID,customerFname,customerMname,customerLname,customerAddress FROM customerInfo where customerID = '" + cmb1.Text + "';";


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                txtFname.Text = Convert.ToString(sdr.GetValue(1).ToString());
                CustomForm();
                txtTotalPurhcase.Text = "0";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    txtTotalPurhcase.Text = Convert.ToString(double.Parse(txtTotalPurhcase.Text) + double.Parse(dataTable.Rows[i].Cells[4].Value.ToString()));
                }
            }



            con.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmb1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void cmb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(txtTotal.Text);
            double num2 = Convert.ToDouble(txtService.Text);

            double totalFinal = num1 + num2;

            txtTotalPay.Text = totalFinal.ToString();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            bunifuCards1.Visible = true;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            bunifuCards1.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                bunifuCards1.Visible = true;
                int v = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[v];

                txtInventory.Text = row.Cells[0].Value.ToString();
                txtWeight.Text = row.Cells[1].Value.ToString();
                txtKilo.Text = row.Cells[3].Value.ToString();


                double num1 = Convert.ToDouble(txtWeight.Text);
                double num2 = Convert.ToDouble(txtKilo.Text);

                double totalFinal = num1 * num2;

                txtTotal.Text = totalFinal.ToString();

                bunifuCards1.Visible = false;
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (txtFname.Text.Equals("") || txtInventory.Text.Equals("") || txtWeight.Text.Equals("") || txtKilo.Text.Equals(""))
            {
                MessageBox.Show("Please Fill In all the fields");
            }
            else
            {
                DialogResult result = MessageBox.Show("Proceed to order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    String date = DateTime.Now.ToString("yyyy-MM-dd");
                    //String date1 = DateTime.Now.ToString("yyyy");
                    Random rand = new Random();
                    int number = rand.Next(0, 100);
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                    SqlCommand cmd = new SqlCommand();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd.Connection = con;
                    cmd1.Connection = con;
                    con.Open();
                    cmd.CommandText = "Insert into orderStatusInfo VALUES('" + txtacc.Text + "','" + cmb1.Text + "','" + txtInventory.Text + "','" + txtTotalPay.Text + "','" + date + "','PENDING PAYMENT')";
                    cmd1.CommandText = "Update stocksInfo set stockStatus = 'RELEASE' WHERE livestockID = '" + txtInventory.Text + "';";
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("ADDED to ORDER LIST");
                    TableForm();
                    CustomForm();
                    txtTotalPurhcase.Text = "0";
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        txtTotalPurhcase.Text = Convert.ToString(double.Parse(txtTotalPurhcase.Text) + double.Parse(dataTable.Rows[i].Cells[4].Value.ToString()));
                    }
                    txtFname.Text = "";
                    txtInventory.Text = "";
                    txtWeight.Text = "";
                    txtKilo.Text = "";
                }
            }
        }

        private void txtService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double num1 = Convert.ToDouble(txtTotal.Text);
                double num2 = Convert.ToDouble(txtService.Text);

                double totalFinal = num1 + num2;

                txtTotalPay.Text = totalFinal.ToString();
            }
        }
        

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Proceed wiht this purhcase?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                String date = DateTime.Now.ToString("yyyy-MM-dd");
                Random rand = new Random();
                int number = rand.Next(0, 100);
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd1 = new SqlCommand();
                cmd.Connection = con;
                cmd1.Connection = con;
                con.Open();
                cmd.CommandText = "Insert into orderSummaryInfo VALUES('" + txtacc.Text + "','" + cmb1.Text + "','" + txtTotalPurhcase.Text + "','" + date + "','PENDING PAYMENT')";
                cmd1.CommandText = "Update orderStatusInfo set orderStatus = 'WAITING' WHERE customerID = '" + cmb1.Text + "' AND referenceID = '"+txtacc.Text+"'";
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ADDED to ORDER LIST");
                CustomForm();
                txtFname.Text = "";
                txtInventory.Text = "";
                txtWeight.Text = "";
                txtKilo.Text = "";
            }
        }

        private void dataTable_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            txtTotalPurhcase.Text = (from DataGridViewRow row in dataTable.Rows where row.Cells[4].FormattedValue.ToString() != string.Empty select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum().ToString();
        }

        private void dataTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            txtTotalPurhcase.Text = (from DataGridViewRow row in dataTable.Rows where row.Cells[4].FormattedValue.ToString() != string.Empty select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum().ToString();
        }
    }
}
