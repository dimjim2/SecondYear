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
    public partial class Form1 : Form
    {
        DateTime date;
        String today;
        /*Create connectionstring
        The connection string contains the information that the provider need to know 
        to be able to establish a connection to the database or the data file.
        */
        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatabaseAtomiki.mdb";
        OleDbConnection connection;
        //Make a list of strings
        List<String> birthdays = new List<string>();
        int count = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 f1 = new Form3();
            f1.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form4 f2 = new Form4();
            f2.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form5 f3 = new Form5();
            f3.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form6 f4 = new Form6();
            f4.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //Current date
            date = DateTime.Now;
            //string of todays date
            today = date.Day.ToString()+"/"+date.Month.ToString()+"/"+date.Year.ToString();
            //Open connection
            connection.Open();
            //Query creation,select everything from database
            String query = "Select * from Contacts";
            //Command creation in order to execute the query
            OleDbCommand command = new OleDbCommand(query, connection);
            //Run query and object to save results
            OleDbDataReader reader = command.ExecuteReader();
            //Create stringbuilder object which are like String objects, except that they can be modified
            StringBuilder builder = new StringBuilder();
            while (reader.Read())
            {
                //Covert date of birth from database to DateTime
                DateTime d = Convert.ToDateTime(reader.GetString(8));
                //If todays date is same except of year of course ,then maybe somebody has birthday
                if (date.Day.ToString().Equals(d.Day.ToString()) && date.Month.ToString().Equals(d.Month.ToString()))
                {
                    int age = date.Year - d.Year;
                    MessageBox.Show("Today is "+today+Environment.NewLine+"Birthday for " + reader.GetString(1) + " " + reader.GetString(2)+Environment.NewLine+"And becomes "+age +" year old."+"Dont forget to say happy birthday!!!");
                    count += 1;
                }
            }
            if (count == 0)
            {
                MessageBox.Show("None contacts birthdays today!!!");
            }
            //Close connection
            connection.Close();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connection object obtains reference and address based on constructor with connectionstring
            connection = new OleDbConnection(connectionString);
        }
    }
}
