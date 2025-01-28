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
    public partial class vipupdate : Form
    {
        public vipupdate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search.Focus();
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
                textBox11.Focus();
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7.Focus();
            }
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

        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");

        private void search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Empty Filed Please ENTER ID ?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ID = textBox5.Text;
            string query3 = $"SELECT * FROM rentvip WHERE RefNumber = '{ID}'";

            SqlCommand cmd3 = new SqlCommand(query3, con1);

            con1.Open();
            SqlDataReader data = cmd3.ExecuteReader();
            if (data.HasRows)
            {
                data.Read();
                textBox1.Text = data["RefNumber"].ToString();
                comboBox1.Text = data["VehicleNumber"].ToString();
                textBox9.Text = data["FullName"].ToString();
                textBox8.Text = data["NIC"].ToString();
                textBox3.Text = data["LisenceNumber"].ToString();
                textBox6.Text = data["Duration"].ToString();
                textBox10.Text = data["AdvancePay"].ToString();
                dateTimePicker1.Text = data["DateObtained"].ToString();
                textBox7.Text = data["KMs"].ToString();

                //dateTimePicker2.Text = Convert.ToString(data["DeliveryDate"]);
                textBox4.Text = Convert.ToString(data["TotalKMs"]);
                textBox11.Text = Convert.ToString(data["FullPayment"]);
                comboBox2.Text = Convert.ToString(data["VerifyStaff_ID"]);

                con1.Close();

            }
            else
            {
                MessageBox.Show("Search Data Not Found", "NOT FOUND", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)
              || string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Enter the Delete ID in Search Bar and search?", "ENTER ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are You Sure Update This Record", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
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


                string query2 = $"UPDATE rentvip SET RefNumber='{refnb}', VehicleNumber ='{vehiclenb}', FullName='{name}',NIC='{nic}',LisenceNumber='{licnb}',Duration='{duration}', AdvancePay='{advance}', DateObtained='{obdate}', KMs='{km}', DelivaryDate='{delydate}', TotalKMs='{totalkm}',FullPayment='{fullpay}',VerifyStaff_ID='{staffid}' WHERE RefNumber = '{refnb}' ";

                SqlCommand cmd2 = new SqlCommand(query2, con1);

                try
                {
                    con1.Open();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Reocrd is Update", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    con1.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)
              || string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Enter the Delete ID in Search Bar and search?", "ENTER ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are You Sure Delete This Record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {



                string ID = textBox5.Text;
                string query3 = $"DELETE rentvip WHERE RefNumber = '{ID}'";

                SqlCommand cmd3 = new SqlCommand(query3, con1);


                try
                {
                    con1.Open();
                    cmd3.ExecuteNonQuery();
                    con1.Close();

                    MessageBox.Show("Reocrd is Delete", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
