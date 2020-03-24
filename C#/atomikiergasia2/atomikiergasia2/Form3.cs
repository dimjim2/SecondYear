using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atomikiergasia2
{
    //Δημητρίου Δημήτρης Π18036
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        String name;
        String lastName;
        //Create connectionString
        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatabaseAtomiki.mdb";
        OleDbConnection connection;
        private void button2_Click(object sender, EventArgs e)
        {
            //Clears everything fields
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            name = textBox1.Text;
            lastName = textBox2.Text;
            //Check if fields are empty or null
            if (textBox1.Text.Equals("") || textBox1.Text.Equals(null)|| textBox2.Text.Equals("")|| textBox2.Text.Equals(null))
            {
                
                MessageBox.Show("Fields are empty or null");
                return;
            }
            //Open connceion
            connection.Open();
            //Parameterized query to delete everything from contacts based on name and lastName
            String query="Delete from Contacts "+ "Where ContactName=? and ContactFullName=?";
            //Creation object to exexute query
            OleDbCommand command= new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("ContactName", name);
            command.Parameters.AddWithValue("ContactFullName", lastName);
            //Execute query
            int count = command.ExecuteNonQuery();
            //Close connection
            connection.Close();
            MessageBox.Show(count.ToString()+" row affected");

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //Connection object obtains reference and address based on constructor with connectionstring
            connection = new OleDbConnection(connectionString);
        }
    }
}
