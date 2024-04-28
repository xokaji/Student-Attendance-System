using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.VisualStyles;

namespace StudentAttendanceSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KAJI;Initial Catalog=StudentDB;Integrated Security=True;");

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd /MM/yyyy"; 
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                dateTimePicker1.CustomFormat = "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cnn = new SqlCommand("insert into studentab(studentid, studentname, section, status, datecreated) values(@studentid, @studentname, @section, @status, @datecreated)", con);
                cnn.Parameters.AddWithValue("@studentid", Convert.ToInt32(textBox1.Text));
                cnn.Parameters.AddWithValue("@studentname", textBox2.Text);
                cnn.Parameters.AddWithValue("@section", textBox3.Text);
                cnn.Parameters.AddWithValue("@status", textBox4.Text);
                cnn.Parameters.AddWithValue("@datecreated", dateTimePicker1.Value);
                cnn.ExecuteNonQuery();
                con.Close();
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Fill the Blanks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        void BindData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM studentab", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string studentIdString = textBox1.Text.Trim();

                if (!string.IsNullOrEmpty(studentIdString) && int.TryParse(studentIdString, out int studentId))
                {
                    con.Open();
                    SqlCommand cnn = new SqlCommand("DELETE  FROM studentab WHERE studentid = @studentid", con);
                    cnn.Parameters.AddWithValue("@studentid", studentId);

                    int rowsAffected = cnn.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Deleted Successfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                    }
                    else
                    {
                        MessageBox.Show("No record deleted. Student ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Student ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Text = "";



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
{
    try
    {
        string studentIdString = textBox1.Text.Trim();

        if (!string.IsNullOrEmpty(studentIdString) && int.TryParse(studentIdString, out int studentId))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE studentab SET studentname = @studentname, section = @section, status = @status, datecreated = @datecreated WHERE studentid = @studentid", con);
            cmd.Parameters.AddWithValue("@studentid", studentId);
            cmd.Parameters.AddWithValue("@studentname", textBox2.Text);
            cmd.Parameters.AddWithValue("@section", textBox3.Text);
            cmd.Parameters.AddWithValue("@status", textBox4.Text);
            cmd.Parameters.AddWithValue("@datecreated", Convert.ToDateTime(dateTimePicker1.Text));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                
                BindData();
            }
            else
            {
                MessageBox.Show("No record updated. Student ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Please enter a valid Student ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    
}


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}
