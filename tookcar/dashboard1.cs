using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace tookcar
{
    public partial class dashboard1 : Form
    {
        public dashboard1()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("Https://youtube.com");
        }

        private void button6_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("Https://Facebook.com");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/kasun-dilshan/");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
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


        private void dashboard1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToLongDateString();
            time.Text = DateTime.Now.ToLongTimeString();
        }

      

       

        private void garegedet_Click(object sender, EventArgs e)
        {
            using (garagedet res1 = new garagedet())
            {
                res1.ShowDialog();
            }
        }

        private void staffdet_Click(object sender, EventArgs e)
        {
            using (staffdet res1 = new staffdet())
            {
                res1.ShowDialog();
            }
        }

        private void luxurycar_Click(object sender, EventArgs e)
        {
            using (rentvip res1 = new rentvip())
            {
                res1.ShowDialog();
            }
        }

        private void rentcar_Click(object sender, EventArgs e)
        {
            using (rentdet res1 = new rentdet())
            {
                res1.ShowDialog();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dashboard1_Load_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
       
        }

        private void date_Click(object sender, EventArgs e)
        {

        }

        private void cardet_Click(object sender, EventArgs e)
        {
            using(cardet res1 = new cardet())
            {
                res1.ShowDialog();
            }
        }

        private void ownerdet_Click(object sender, EventArgs e)
        {
            using (ownerdet res1 = new ownerdet())
            {
                res1.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quikly Contact Developer : 076 4694 845", "EMARGENCY", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

     
}
