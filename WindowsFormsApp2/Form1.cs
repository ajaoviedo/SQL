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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                SqlDataReader dr;
                String sql, outp = "";
                String tablename = listBox2.GetItemText(listBox2.SelectedItem); //get selected table

                if (tablename == "")
                {
                    MessageBox.Show("Select a table");
                    return;
                }
                sql = $"Select * from {tablename}"; //query to get all columns from table
                cmd = new SqlCommand(sql, Program.cnn);
                dr = cmd.ExecuteReader();
                int i = 1;
                while (dr.Read())
                {
                    outp += i + ". Site: " + dr.GetValue(0) + " Username: " + dr.GetValue(1) + " Password: " + dr.GetValue(2) + "\n";
                    i++;
                }
                dr.Close();
                String output = $"Table: {tablename}\n\n" + outp;
                richTextBox1.Text = output;
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                String sql = "";
                String tablename = listBox2.GetItemText(listBox2.SelectedItem); //get selected table
                String site = Site.Text;
                String user = Username.Text;
                String pass = Password.Text;
                if (site == "" || user == "" || pass == "")
                {
                    MessageBox.Show("Fill in all fields");
                    return;
                }
                sql = $"Insert into {tablename} values('{site}','{user}','{pass}');"; //query to add row to table
                cmd = new SqlCommand(sql, Program.cnn);
                cmd.ExecuteNonQuery();
                MessageBox.Show($"Succesfully added row to {tablename}");
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                String sql = "";
                String tablename = listBox2.GetItemText(listBox2.SelectedItem); //get selected table

                if (tablename == "")
                {
                    MessageBox.Show("Select a table");
                    return;
                }
                sql = $"Delete from {tablename};"; //query to delete all values from table
                cmd = new SqlCommand(sql, Program.cnn);
                cmd.ExecuteNonQuery();
                MessageBox.Show($"Succesfully deleted all values from {tablename}");
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                SqlDataReader dr;
                String sql = "";
                string db = listBox1.GetItemText(listBox1.SelectedItem);

                if (db == "")
                {
                    MessageBox.Show("Select a database");
                    return;
                }
                sql = $"USE {db}; SELECT * FROM sys.Tables;"; //query to see all tables in database
                cmd = new SqlCommand(sql, Program.cnn);
                dr = cmd.ExecuteReader();
                listBox2.Items.Clear(); //clear previous items from listbox
                while (dr.Read())
                {
                    listBox2.Items.Add(dr.GetValue(0)); //add each table
                }
                dr.Close();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                SqlDataReader dr;
                String sql = "";

                sql = $"SELECT name FROM sys.databases;"; //query to see all databases in the server
                cmd = new SqlCommand(sql, Program.cnn);
                dr = cmd.ExecuteReader();
                listBox1.Items.Clear(); //clear previous items from listbox
                while (dr.Read())
                {
                    listBox1.Items.Add(dr.GetValue(0)); //add each database
                }
                dr.Close();
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