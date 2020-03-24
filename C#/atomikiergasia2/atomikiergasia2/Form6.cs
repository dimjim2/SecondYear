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
    public partial class Form6 : Form
    {
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        /*Create connectionstring
       The connection string contains the information that the provider need to know 
       to be able to establish a connection to the database or the data file.
       */
        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatabaseAtomiki.mdb";
        //Declaration of connection
        OleDbConnection connection;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            //Creation of connection
            connection = new OleDbConnection(connectionString);
            //Open conection
            connection.Open();
            label2.Text = "";
            //Select only ContactName query
            String query = "Select ContactName from Contacts";
            //Creation of object to execute query
            OleDbCommand command = new OleDbCommand(query, connection);
            //Run query and object to save results
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Add ContactName to combobox 
                comboBox1.Items.Add(reader.GetString(0));

            }
            //Close connection
            connection.Close();
            //Stop the music
            player.controls.stop();

        }
        //Occurs when you click to a value of the combobox
        //Changes the selected index value
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.controls.stop();
            MessageBox.Show(comboBox1.SelectedItem.ToString());
            connection = new OleDbConnection(connectionString);
            //Creation of connection
            connection.Open();
            //Parameterized query select based on ContactName
            String query="Select * from Contacts Where ContactName=?";
            //Creation of object to execute query
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("ContactName", comboBox1.SelectedItem.ToString());
            //Run query and object to save results
            OleDbDataReader reader = command.ExecuteReader();
            //Create stringbuilder object which are like String objects, except that they can be modified
            StringBuilder builder = new StringBuilder();
            while (reader.Read())
            {
                //Apend to builder string
                builder.AppendLine("Name  "+reader.GetString(1)+ 
                    Environment.NewLine +"Last Name  "+ reader.GetString(2) + Environment.NewLine +"Telephone Number  "+ reader.GetString(3) 
                    + Environment.NewLine +"EmailAddress  "+ reader.GetString(4) + Environment.NewLine + 
                    "Country "+ reader.GetString(5) + Environment.NewLine +"City  " + reader.GetString(6) + 
                    Environment.NewLine + "Address  "+reader.GetString(7) + Environment.NewLine +"Birth Date  "+ reader.GetString(8));
                String  song = reader.GetString(9);
                player.URL = song;
                //Play the song
                player.controls.play();
                String picture = reader.GetString(10);
                //Image location 
                pictureBox1.ImageLocation = picture;

            }
            //Label text is builder contents
            label2.Text = builder.ToString();
            //Close connection
            connection.Close();
        }
        //Stops the music when we close the form
        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.controls.stop();
        }
    }
}
