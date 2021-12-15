using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
                listBox3.Items.Clear();
                using (var reader = new StringReader(output))
                {
                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        listBox3.Items.Add(line);
                        // Do something with the line
                    }
                }
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

        private void button6_Click(object sender, EventArgs e)
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

                String removeValues = listBox3.GetItemText(listBox3.SelectedItem); //get selected values
                if (removeValues == "")
                {
                    MessageBox.Show("Select a row");
                    return;
                }
                string[] words = removeValues.Split(' ');
                String site = words[2];
                String user = words[4];
                String pass = words[6];

                sql = $"Delete from {tablename} where CONVERT(NVARCHAR(MAX), Site) = N'{site}' and CONVERT(NVARCHAR(MAX), Username) = N'{user}' and CONVERT(NVARCHAR(MAX), Password) = N'{pass}';"; //query to delete all values from table
                cmd = new SqlCommand(sql, Program.cnn);
                cmd.ExecuteNonQuery();
                MessageBox.Show($"Succesfully deleted row from {tablename}");
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

        private void button7_Click(object sender, EventArgs e)
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

                String sitefilter = SiteFilter.Text;

                if (sitefilter == "")
                {
                    MessageBox.Show("Type a filter");
                    return;
                }


                sql = $"Select * from {tablename} where CONVERT(NVARCHAR(MAX), Site) = N'{sitefilter}';"; //query to get all columns from table that have same site
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
                listBox3.Items.Clear();
                using (var reader = new StringReader(output))
                {
                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        listBox3.Items.Add(line);
                        // Do something with the line
                    }
                }
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