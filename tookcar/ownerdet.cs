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
    public partial class ownerdet : Form
    {
        public ownerdet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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
            dateTimePicker1.Text = null;


        }
        
        private void button4_Click_1(object sender, EventArgs e)
        {

            clear();
            MessageBox.Show("Record Clear...", "CLEAR", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (ownerupdate res1 = new ownerupdate())
            {
                res1.ShowDialog();
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

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search.Focus();
            }
        }

        private void textBox5_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void ownerdet_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tookcarDataSet1.ownerdet' table. You can move, or remove it, as needed.
            this.ownerdetTableAdapter.Fill(this.tookcarDataSet1.ownerdet);


            getautoid();

            textBox1.ReadOnly = true;
            textBox1.Enabled = false;
           

        }

        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");


        public void getautoid()
        {
            string newID = "W01";
            con1.Open();

            SqlCommand cmd = new SqlCommand("SELECT OwnerID FROM ownerdet ORDER BY OwnerID ASC", con1);
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> existingIDs = new List<int>();

            while (reader.Read())
            {
                string id = reader["OwnerID"].ToString().Substring(1);
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

            newID = "W" + nextID.ToString("D2");
            textBox1.Text = newID;
        }


        private void addkids_Click(object sender, EventArgs e)
        {



            getautoid();

            if (string.IsNullOrWhiteSpace(textBox1.Text)
               || string.IsNullOrWhiteSpace(textBox9.Text)
               || string.IsNullOrWhiteSpace(textBox2.Text)
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
            string cid = textBox9.Text;
            string name = textBox2.Text;
            string nic = textBox8.Text;
            string duration = textBox6.Text;
            string jdate = dateTimePicker1.Text;
            string mobilenb = textBox7.Text;
            string address = textBox4.Text;

            string query = $"insert into ownerdet (OwnerID,CarID,FullName,NIC,Duration,JoinedDate,MobileNumber,Address) values ('{id}','{cid}','{name}','{nic}','{duration}','{jdate}','{mobilenb}','{address}')";

            SqlCommand cmd1 = new SqlCommand(query, con1);


            try
            {
                con1.Open();


                cmd1.ExecuteNonQuery();
                MessageBox.Show("New Owner Details Add Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getautoid();
            }
            catch
            {

            }
            try
            {
                string query2 = "SELECT * FROM ownerdet";


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                SqlCommand cmd2 = new SqlCommand(query2, con1);

                sqlDataAdapter.SelectCommand = cmd2;
                DataSet dataSet = new DataSet();


                sqlDataAdapter.Fill(dataSet, "ownerdet");


                dataGridView1.DataSource = dataSet.Tables["ownerdet"];

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

            string query = "SELECT * FROM ownerdet";


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd1 = new SqlCommand(query, con1);

            sqlDataAdapter.SelectCommand = cmd1;
            DataSet dataSet = new DataSet();


            sqlDataAdapter.Fill(dataSet, "ownerdet");


            dataGridView1.DataSource = dataSet.Tables["ownerdet"];
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Empty Filed Please ENTER ID ?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand("Select OwnerID,CarID,FullName,NIC,Duration,JoinedDate,MobileNumber,Address from ownerdet where OwnerID =@parm1", con1);
            SqlCommand cmd2 = new SqlCommand("Select OwnerID,CarID,FullName,NIC,Duration,JoinedDate,MobileNumber,Address from ownerdet where CarID =@parm2", con1);


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

        private void button3_Click(object sender, EventArgs e)
        {

            //Init print datagridview
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Owner Details Backup Report";//Header
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (inovice res1 = new inovice())
            {
                res1.ShowDialog();
            }
        }
    }
    
}
