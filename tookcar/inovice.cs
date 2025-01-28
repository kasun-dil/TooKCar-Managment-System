using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace tookcar
{
    public partial class inovice : Form
    {
        public inovice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToLongDateString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void inovice_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
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

        SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\OneDrive\Desktop\tookcar\tookcar\tookcar.mdf;Integrated Security=True");


        private void search_Click_1(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox5.Text))

            {
                MessageBox.Show("Empty Filed Please ENTER ID ?", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ownerid.Text = data["OwnerID"].ToString();
                    name.Text = data["FullName"].ToString();
                    nic.Text = data["NIC"].ToString();
                    duration.Text = data["Duration"].ToString();
                    jdate.Text = data["JoinedDate"].ToString();
                    carid2.Text = data["CarID"].ToString();



                }

                con1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                search.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Print(this.inovicepanal);
        }

        private void Print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            inovicepanal = pnl; // Store the panel to be printed
            getprintarea(pnl); // Capture the panel's content as an image
            printPreviewDialog1.Document = printDocument1; // Link the document to the preview dialog
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage); // Event handler for printing
            printPreviewDialog1.ShowDialog(); // Show print preview
            MessageBox.Show("PDF Create Succesfull", "SUCCESFULL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            



        }

        private Bitmap memoryimg; // Used to store the image of the panel

        private void getprintarea(Panel pnl)
        {
            memoryimg = new Bitmap(pnl.Width, pnl.Height); // Create a bitmap with the panel's dimensions
            pnl.DrawToBitmap(memoryimg, new Rectangle(0, 0, pnl.Width, pnl.Height)); // Render the panel into the bitmap
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memoryimg, (pagearea.Width / 2) - (this.inovicepanal.Width / 2), this.inovicepanal.Location.Y );

        }


    }
}
