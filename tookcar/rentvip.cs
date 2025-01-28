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
    public partial class rentvip : Form
    {
        public rentvip()
        {
            InitializeComponent();
        }

        private void rentvip_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tookcarDataSet5.rentvip' table. You can move, or remove it, as needed.
            this.rentvipTableAdapter.Fill(this.tookcarDataSet5.rentvip);
 
            getautoid();

            textBox1.ReadOnly = true;
            textBox1.Enabled = false;
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

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                textBox10.Focus();
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
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
                addkids.Focus();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        void clear()
        {
            textBox1.Clear();
            textBox9.Clear();
            textBox8.Clear();
            textBox3.Clear();
            textBox6.Clear();
            textBox10.Clear();
            textBox7.Clear();
            textBox4.Clear();
            textBox11.Clear();
            dateTimePicker1.Text = null;
            dateTimePicker2.Text = null;
            comboBox1.Text = null;
            comboBox2.Text = null;


        }
        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            MessageBox.Show("Record Clear...", "CLEAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");


        public void getautoid()
        {
            string newID = "V001";
            con1.Open();

            SqlCommand cmd = new SqlCommand("SELECT RefNumber FROM rentvip ORDER BY RefNumber ASC", con1);
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> existingIDs = new List<int>();

            while (reader.Read())
            {
                string id = reader["RefNumber"].ToString().Substring(1);
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

            newID = "V" + nextID.ToString("D3");
            textBox1.Text = newID;
        }

        private void addkids_Click(object sender, EventArgs e)
        {

            getautoid();


            if (string.IsNullOrWhiteSpace(textBox1.Text)
               || string.IsNullOrWhiteSpace(comboBox1.Text)
               || string.IsNullOrWhiteSpace(textBox9.Text)
               || string.IsNullOrWhiteSpace(textBox8.Text)
               || string.IsNullOrWhiteSpace(textBox3.Text)
               || string.IsNullOrWhiteSpace(textBox6.Text)
               || string.IsNullOrWhiteSpace(textBox10.Text)
               || string.IsNullOrWhiteSpace(dateTimePicker1.Text)
               || string.IsNullOrWhiteSpace(textBox7.Text))

            {
                MessageBox.Show("Enter the Complete Data...?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string refnb = textBox1.Text;
            string vehiclenb = comboBox1.Text;
            string name = textBox9.Text;
            string nic = textBox8.Text;
            string licnb = textBox3.Text;
            string duration = textBox6.Text;
            string advance = textBox10.Text;
            string obdate = dateTimePicker1.Text;
            string km = textBox7.Text;


            string delydate = dateTimePicker2.Text;
            string totalkm = textBox4.Text;
            string fullpay = textBox11.Text;
            string staffid = comboBox2.Text;


            string query = $"insert into rentvip (RefNumber,VehicleNumber,FullName,NIC,LisenceNumber,Duration,AdvancePay,DateObtained,KMs,DelivaryDate,TotalKMs,FullPayment,VerifyStaff_ID) values ('{refnb}','{vehiclenb}','{name}','{nic}','{licnb}','{duration}','{advance}','{obdate}','{km}','{delydate}','{totalkm}','{fullpay}','{staffid}')";

            SqlCommand cmd1 = new SqlCommand(query, con1);


            try
            {
                con1.Open();


                cmd1.ExecuteNonQuery();
                MessageBox.Show("New VIP Reference Details Add Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getautoid();
            }
            catch
            {

            }
            try
            {

                string query2 = "SELECT * FROM rentvip";


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                SqlCommand cmd2 = new SqlCommand(query2, con1);

                sqlDataAdapter.SelectCommand = cmd2;
                DataSet dataSet = new DataSet();


                sqlDataAdapter.Fill(dataSet, "rentvip");


                dataGridView1.DataSource = dataSet.Tables["rentvip"];

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

            string query2 = "SELECT * FROM rentvip";


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd2 = new SqlCommand(query2, con1);

            sqlDataAdapter.SelectCommand = cmd2;
            DataSet dataSet = new DataSet();


            sqlDataAdapter.Fill(dataSet, "rentvip");


            dataGridView1.DataSource = dataSet.Tables["rentvip"];
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Empty Filed Please ENTER ID ?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand("Select RefNumber,VehicleNumber,FullName,NIC,LisenceNumber,Duration,AdvancePay,DateObtained,KMs,DelivaryDate,TotalKMs,FullPayment,VerifyStaff_ID from rentdet where RefNumber =@parm1", con1);
            SqlCommand cmd2 = new SqlCommand("Select RefNumber,VehicleNumber,FullName,NIC,LisenceNumber,Duration,AdvancePay,DateObtained,KMs,DelivaryDate,TotalKMs,FullPayment,VerifyStaff_ID from rentdet where VehicleNumber =@parm2", con1);
            SqlCommand cmd3 = new SqlCommand("select RefNumber,VehicleNumber,FullName,NIC,LisenceNumber,Duration,AdvancePay,DateObtained,KMs,DelivaryDate,TotalKMs,FullPayment,VerifyStaff_ID from rentdet where NIC =@parm3", con1);


            cmd.Parameters.AddWithValue("@parm1", textBox5.Text);
            cmd2.Parameters.AddWithValue("@parm2", textBox5.Text);
            cmd3.Parameters.AddWithValue("@parm3", textBox5.Text);


            SqlDataAdapter da = new SqlDataAdapter();
            SqlDataAdapter da2 = new SqlDataAdapter();
            SqlDataAdapter da3 = new SqlDataAdapter();


            da.SelectCommand = cmd;
            da2.SelectCommand = cmd2;
            da3.SelectCommand = cmd3;

            DataTable dt = new DataTable();

            dt.Clear();

            da.Fill(dt);
            da2.Fill(dt);
            da3.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (vipupdate res1 = new vipupdate())
            {
                res1.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Init print datagridview
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "VIP Vehicle Rental Details Backup Report";//Header
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

        private void button7_Click(object sender, EventArgs e)
        {
            using (inovice res= new inovice())
            {
                res.ShowDialog();
            }
        }
    }
}
