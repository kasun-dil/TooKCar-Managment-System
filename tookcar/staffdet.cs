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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace tookcar
{
    public partial class staffdet : Form
    {
        public staffdet()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (staffupdate res1 = new staffupdate())
            {
                res1.ShowDialog();
            }
        }

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox6.Clear();
            dateTimePicker1.Text = null;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            MessageBox.Show("Record Clear...", "CLEAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void panel5_Paint(object sender, PaintEventArgs e) 
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
                textBox2.Focus();
            }
        }



        private void textBox2_KeyDown(object sender, KeyEventArgs e)
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
                textBox8.Focus();
            }

        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
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
                dateTimePicker1.Focus();
            }

        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
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

        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");


       

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search.Focus();
            }
        }


        private void staffdet_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tookcarDataSet2.staffdet' table. You can move, or remove it, as needed.
            this.staffdetTableAdapter.Fill(this.tookcarDataSet2.staffdet);

            getautoid();

            textBox1.ReadOnly = true;
            textBox1.Enabled = false;



        }

        public void getautoid()
        {
            string newID = "S01";
            con1.Open();

            SqlCommand cmd = new SqlCommand("SELECT StaffID FROM staffdet ORDER BY StaffID ASC", con1);
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> existingIDs = new List<int>();

            while (reader.Read())
            {
                string id = reader["StaffID"].ToString().Substring(1);
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

            newID = "S" + nextID.ToString("D2");
            textBox1.Text = newID;
        }


        private void addkids_Click(object sender, EventArgs e)
        {
            getautoid();

            if (string.IsNullOrWhiteSpace(textBox1.Text)
               || string.IsNullOrWhiteSpace(textBox2.Text)
               || string.IsNullOrWhiteSpace(textBox3.Text)
               || string.IsNullOrWhiteSpace(textBox8.Text)
               || string.IsNullOrWhiteSpace(textBox6.Text)
               || string.IsNullOrWhiteSpace(dateTimePicker1.Text)
               || string.IsNullOrWhiteSpace(textBox7.Text)
               || string.IsNullOrWhiteSpace(textBox4.Text))

            {
                MessageBox.Show("Enter the Complete Data...?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            string id = textBox1.Text;
            string fname = textBox2.Text;
            string sname = textBox3.Text;
            string nic = textBox8.Text;
            string job = textBox6.Text;
            string jdate = dateTimePicker1.Text;
            string mobilenb = textBox7.Text;
            string address = textBox4.Text;

            string query = $"insert into staffdet (StaffID,FirstName,SecondName,NIC,JobRoll,JoinedDate,MobileNumber,Address) values ('{id}','{fname}','{sname}','{nic}','{job}','{jdate}','{mobilenb}','{address}')";

            SqlCommand cmd1 = new SqlCommand(query, con1);


            try
            {
                con1.Open();


                cmd1.ExecuteNonQuery();
                MessageBox.Show("New Staff Member Details Add Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getautoid();
            }
            catch
            {

            }
            try
            {
                string query2 = "SELECT * FROM staffdet";


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                SqlCommand cmd2 = new SqlCommand(query2, con1);

                sqlDataAdapter.SelectCommand = cmd2;
                DataSet dataSet = new DataSet();


                sqlDataAdapter.Fill(dataSet, "staffdet");


                dataGridView1.DataSource = dataSet.Tables["staffdet"];

                con1.Close();
                clear();
            }
            catch
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            getautoid();

            string query = "SELECT * FROM staffdet";


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd1 = new SqlCommand(query, con1);

            sqlDataAdapter.SelectCommand = cmd1;
            DataSet dataSet = new DataSet();


            sqlDataAdapter.Fill(dataSet, "staffdet");


            dataGridView1.DataSource = dataSet.Tables["staffdet"];
        }

        private void search_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Empty Filed Please ENTER ID ?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            SqlCommand cmd = new SqlCommand("Select StaffID,FirstName,SecondName,NIC,JobRoll,JoinedDate,MobileNumber,Address from staffdet where StaffID =@parm1", con1);
            SqlCommand cmd2 = new SqlCommand("Select StaffID,FirstName,SecondName,NIC,JobRoll,JoinedDate,MobileNumber,Address from staffdet where FirstName =@parm2", con1);


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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Init print datagridview
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Staff Details Backup Report";//Header
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
