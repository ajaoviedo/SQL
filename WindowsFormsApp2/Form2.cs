using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            //string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            //Console.WriteLine(hostName);
            //GET IP Address
            //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            //MessageBox.Show("My IP Address is :" + myIP);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ds = @"10.140.177.176\SQLEXPRESS,1434"; //192.168.1.5 home
            string user = textBox1.Text;
            string pass = textBox2.Text;
            string connetionString = $@"Data Source={ds};User ID={user};Password={pass}"; //use data source as login and inputs as credentials
            Program.cnn = new SqlConnection(connetionString); //use global sqlconnection for other form to stay connected
            try
            {
                Program.cnn.Open(); //check if you can login with given credentials
                Form f = new Form1(); //open form if login accepted
                f.Show();
                this.Hide();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Cannot open connection! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot open connection! Error: {ex.Message}");
            }
        }
    }
}
