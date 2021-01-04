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

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ds = @"192.168.1.14\SQLEXPRESS,1434";
            string user = textBox1.Text;
            string pass = textBox2.Text;
            string connetionString = $@"Data Source={ds};User ID={user};Password={pass}"; //use data source as login and inputs as credentials
            Program.cnn = new SqlConnection(connetionString); //use global sqlconnection for other form to stay connected
            try
            {
                Program.cnn.Open(); //check if you can login with given credentials
                Form f = new Form1(); //open form if login accepted
                f.Show();
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
