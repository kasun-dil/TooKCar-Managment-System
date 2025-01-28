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
    public partial class garagedet : Form
    {
        public garagedet()
        {
            InitializeComponent();
        }

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            MessageBox.Show("Record Clear...", "CLEAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button6_Click(object sender, EventArgs e)
        {
            using (garageupdate res1 = new garageupdate())
            {
                res1.ShowDialog();
            }
        }

       

        private void garagedet_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tookcarDataSet.garagedet' table. You can move, or remove it, as needed.
            this.garagedetTableAdapter.Fill(this.tookcarDataSet.garagedet);

            getautoid();

            textBox1.ReadOnly = true;
            textBox1.Enabled = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");

        public void getautoid()
        {
            string newID = "G01"; // Default starting ID

            // Open SQL connection
            con1.Open();

            // Query to fetch all GarageIDs, ordered numerically
            SqlCommand cmd = new SqlCommand("SELECT GarageID FROM garagedet ORDER BY GarageID ASC", con1);
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> existingIDs = new List<int>();

            while (reader.Read())
            {
                // Extract the numeric part of each GarageID (e.g., "G01" -> 1)
                string id = reader["GarageID"].ToString().Substring(1);
                if (int.TryParse(id, out int numericID))
                {
                    existingIDs.Add(numericID);
                }
            }

            con1.Close();

            // Find the first missing numeric ID in the sequence
            int nextID = 1; // Start checking from 1
            while (existingIDs.Contains(nextID))
            {
                nextID++; // Increment until a gap is found
            }

            // Format the new ID with the prefix "G"
            newID = "G" + nextID.ToString("D2"); // Ensures two-digit format (e.g., G02)

            // Set the new ID in the TextBox
            textBox1.Text = newID;




        }

        private void addkids_Click(object sender, EventArgs e)
        {

            getautoid();

            if (string.IsNullOrWhiteSpace(textBox1.Text)
               || string.IsNullOrWhiteSpace(textBox2.Text)
               || string.IsNullOrWhiteSpace(textBox3.Text)
               || string.IsNullOrWhiteSpace(textBox7.Text)
               || string.IsNullOrWhiteSpace(textBox4.Text))

            {
                MessageBox.Show("Enter the Complete Data...?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            

            string id = textBox1.Text;
            string name = textBox2.Text;
            string description = textBox3.Text;
            string mobilenb = textBox7.Text;
            string address = textBox4.Text;


            string query = $"insert into garagedet (GarageID,Name,Description,MobileNumber,Address) values ('{id}','{name}','{description}','{mobilenb}','{address}')";

            SqlCommand cmd1 = new SqlCommand(query, con1);


            try
            {
                con1.Open();

                cmd1.ExecuteNonQuery();
                MessageBox.Show("New Garage Details Add Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getautoid();
            }
            catch
            {

            }

            string query2 = "SELECT * FROM garagedet";


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd2 = new SqlCommand(query2, con1);

            sqlDataAdapter.SelectCommand = cmd2;
            DataSet dataSet = new DataSet();


            sqlDataAdapter.Fill(dataSet, "garagedet");


            garagegrid.DataSource = dataSet.Tables["garagedet"];

            con1.Close();
            clear();
            


        }

        private void button2_Click(object sender, EventArgs e)
        {

            getautoid();

            string query = "SELECT * FROM garagedet";


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd1 = new SqlCommand(query, con1);

            sqlDataAdapter.SelectCommand = cmd1;
            DataSet dataSet = new DataSet();


            sqlDataAdapter.Fill(dataSet, "garagedet");


            garagegrid.DataSource = dataSet.Tables["garagedet"];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Empty Filed Please ENTER ID ?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand("Select GarageID,Name,Description,MobileNumber,Address from garagedet where GarageID =@parm1", con1);
            SqlCommand cmd2 = new SqlCommand("Select GarageID,Name,Description,MobileNumber,Address from garagedet where Name =@parm2", con1);


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
        garagegrid.DataSource = dt;
        garagegrid.AllowUserToAddRows = false;

        

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox7.Focus();
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                search.Focus();
            }

        }

        private void addkids_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void garagegrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Init print datagridview
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Garage Details Backup Report";//Header
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
            printer.PrintDataGridView(garagegrid);
            MessageBox.Show("Backup PDF Create Succesfull", "SUCCESFULL", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
    
}
