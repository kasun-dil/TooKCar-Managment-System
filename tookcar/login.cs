using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace tookcar
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (useridbox.Text == "" && passwordbox.Text == "")
            {
                MessageBox.Show("Missing Information", "MISSING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (useridbox.Text == ("kasun") && passwordbox.Text == "kasun1113")
            {
                dashboard1 obj = new dashboard1();
                obj.Show();
                this.Hide();
          

                MessageBox.Show("Login Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (useridbox.Text == (" ") && passwordbox.Text == " ")
            {
                dashboard1 obj = new dashboard1();
                obj.Show();
                this.Hide();
               

                MessageBox.Show("Login Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (useridbox.Text == ("admin") && passwordbox.Text == "admin123")
            {
                dashboard1 obj = new dashboard1();
                obj.Show();
                this.Hide();
              

                MessageBox.Show("Login Succesful...", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Enter The Correct User ID and Password", "INCORRECT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                passwordbox.UseSystemPasswordChar = false;
            }
            else
            {
                passwordbox.UseSystemPasswordChar = true;
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void clear()

        {
            passwordbox.Clear();
            useridbox.Text = null;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void useridbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passwordbox.Focus();
            }
        }

        private void passwordbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.Focus();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
