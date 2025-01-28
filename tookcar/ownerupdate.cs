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
    public partial class ownerupdate : Form
    {
        public ownerupdate()
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

        private void ownerupdate_Load(object sender, EventArgs e)
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

        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");

        private void search_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox5.Text))
             
            {
                MessageBox.Show("Enter the Delete ID in Search Bar and search?", "ENTER ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string ID = textBox5.Text;
            string query3 = $"SELECT * FROM ownerdet WHERE OwnerID = '{ID}'";

            SqlCommand cmd3 = new SqlCommand(query3, con1);

            try
            {
                con1.Open();
                SqlDataReader data = cmd3.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    textBox1.Text = data["OwnerID"].ToString();
                    textBox9.Text = data["CarID"].ToString();
                    textBox2.Text = data["FullName"].ToString();
                    textBox8.Text = data["NIC"].ToString();
                    textBox6.Text = data["Duration"].ToString();
                    dateTimePicker1.Text = data["JoinedDate"].ToString();
                    textBox7.Text = data["MobileNumber"].ToString();
                    textBox4.Text = data["Address"].ToString();

                }

                else
                {
                    MessageBox.Show("Search Data Not Found", "NOT FOUND", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                con1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void addkids_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)
              || string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Enter the Delete ID in Search Bar and search?", "ENTER ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are You Sure Update This Record", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string id = textBox1.Text;
                string cid = textBox9.Text;
                string name = textBox2.Text;
                string nic = textBox8.Text;
                string duration = textBox6.Text;
                string jdate = dateTimePicker1.Text;
                string mobilenb = textBox7.Text;
                string address = textBox4.Text;


                string query2 = $"UPDATE ownerdet SET OwnerID='{id}', CarID ='{cid}', FullName='{name}',NIC='{nic}',Duration='{duration}',JoinedDate='{jdate}',MobileNumber='{mobilenb}',Address='{address}' WHERE OwnerID = '{id}' ";

                SqlCommand cmd2 = new SqlCommand(query2, con1);

                try
                {
                    con1.Open();
                    cmd2.ExecuteNonQuery();
                    con1.Close();
                    MessageBox.Show("Reocrd is Update", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                string query3 = $"DELETE ownerdet WHERE OwnerID = '{ID}'";

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
