using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tookcar
{
    public partial class cardet : Form
    {
        public cardet()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox9.Clear();
            textBox4.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox6.Clear();
            textBox3.Clear();
            dateTimePicker1.Text = null;
            comboBox1.Text = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            MessageBox.Show("Record Clear...", "CLEAR", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (carupdate res1 = new carupdate())
            {
                res1.ShowDialog();
            }
        }

        

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                search.Focus();
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox9.Focus();
            }

        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox8.Focus();
            }

        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox6.Focus();
            }

        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox7.Focus();
            }

        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker1.Focus();
            }

        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker2.Focus();
            }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                addkids.Focus();
            }
        }

        private void cardet_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tookcarDataSet3.cardet' table. You can move, or remove it, as needed.
            this.cardetTableAdapter.Fill(this.tookcarDataSet3.cardet);

            // getautoid();

            // textBox1.ReadOnly = true;
            // textBox1.Enabled = false;

          

        }

        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");


        private void search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Empty Filed Please ENTER ID ?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand("Select VehicleNumber,VehicleType,BrandName,Model,BodyColor,TotalKMs,EngineNumber,EngineCapacity,InsuranceEXD,CO2_EXD,FuelType from cardet where VehicleNumber =@parm1", con1);
            SqlCommand cmd2 = new SqlCommand("Select VehicleNumber,VehicleType,BrandName,Model,BodyColor,TotalKMs,EngineNumber,EngineCapacity,InsuranceEXD,CO2_EXD,FuelType from cardet where BrandName =@parm2", con1);


            cmd.Parameters.AddWithValue("@parm1", textBox5.Text);
            cmd2.Parameters.AddWithValue("@parm2", textBox5.Text);

            SqlDataAdapter da = new SqlDataAdapter();
            SqlDataAdapter da2 = new SqlDataAdapter();

            da.SelectCommand = cmd;
            da2.SelectCommand = cmd2;

            DataTable dt = new DataTable();

            dt.Clear();

            da.Fill(dt);
            da2.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
        }

        /* public void getautoid()
        {
            string newID = "C01";
            con1.Open();

            SqlCommand cmd = new SqlCommand("SELECT VehicleNumber FROM cardet ORDER BY VehicleNumber ASC", con1);
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> existingIDs = new List<int>();

            while (reader.Read())
            {
                string id = reader["VehicleNumber"].ToString().Substring(1);
                if (int.TryParse(id, out int numericID))
                {
                    existingIDs.Add(numericID);
                }
            }

            con1.Close();

            int nextID = 1;
            while (existingIDs.Contains(nextID))
            {
                nextID++;
            }

            newID = "C" + nextID.ToString("D2");
            textBox1.Text = newID;
        } */


        private void addkids_Click(object sender, EventArgs e)
        {
            // getautoid();

            if (string.IsNullOrWhiteSpace(textBox1.Text)
               || string.IsNullOrWhiteSpace(comboBox1.Text)
               || string.IsNullOrWhiteSpace(textBox9.Text)
               || string.IsNullOrWhiteSpace(textBox2.Text)
               || string.IsNullOrWhiteSpace(textBox8.Text)
               || string.IsNullOrWhiteSpace(textBox3.Text)
               || string.IsNullOrWhiteSpace(textBox6.Text)
               || string.IsNullOrWhiteSpace(textBox7.Text)
               || string.IsNullOrWhiteSpace(dateTimePicker1.Text)
               || string.IsNullOrWhiteSpace(dateTimePicker2.Text)
               || string.IsNullOrWhiteSpace(textBox4.Text))

            {
                MessageBox.Show("Enter the Complete Data...?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            string id = textBox1.Text;
            string type = comboBox1.Text;
            string brand = textBox9.Text;
            string model = textBox2.Text;
            string color = textBox8.Text;
            string km = textBox3.Text;
            string enginenb = textBox6.Text;
            string capacity = textBox7.Text;
            string ins_exp = dateTimePicker1.Text;
            string co2_exp = dateTimePicker2.Text;
            string fueltype = textBox4.Text;

            string query = $"insert into cardet (VehicleNumber,VehicleType,BrandName,Model,BodyColor,TotalKMs,EngineNumber,EngineCapacity,InsuranceEXD,CO2_EXD,FuelType) values ('{id}','{type}','{brand}','{model}','{color}','{km}','{enginenb}','{capacity}','{ins_exp}','{co2_exp}','{fueltype}')";

            SqlCommand cmd1 = new SqlCommand(query, con1);


            try
            {
                con1.Open();


                cmd1.ExecuteNonQuery();
                MessageBox.Show("New Vehicle Details Add Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //getautoid();
            }
            catch
            {

            }
            try
            {
                string query2 = "SELECT * FROM cardet";


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                SqlCommand cmd2 = new SqlCommand(query2, con1);

                sqlDataAdapter.SelectCommand = cmd2;
                DataSet dataSet = new DataSet();


                sqlDataAdapter.Fill(dataSet, "cardet");


                dataGridView1.DataSource = dataSet.Tables["cardet"];

                con1.Close();
                clear();
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //getautoid();

            try
            {
                string query2 = "SELECT * FROM cardet";


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                SqlCommand cmd2 = new SqlCommand(query2, con1);

                sqlDataAdapter.SelectCommand = cmd2;
                DataSet dataSet = new DataSet();


                sqlDataAdapter.Fill(dataSet, "cardet");


                dataGridView1.DataSource = dataSet.Tables["cardet"];

                con1.Close();
                clear();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Init print datagridview
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Car Details Backup Report";//Header
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "TookCar - Car Rental Service";//Footer
            printer.FooterSpacing = 15;
            printer.SubTitleSpacing = 10;
            //Print landscape mode
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dataGridView1);
            MessageBox.Show("Backup PDF Create Succesfull", "SUCCESFULL", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
