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
    public partial class Form4 : Form
    {
        //Create connectionString
        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatabaseAtomiki.mdb";
        //Create WindowsMediaPlayer object in order to play music
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        OleDbConnection connection;
        public string rchecked;
        String data;
        public Form4()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            data = textBox1.Text;
            bool ok = CheckRadio();
            //Stringbuilder object creation
            StringBuilder builder= new StringBuilder();
            if (ok)
            {
                player.controls.stop();
                pictureBox1.ImageLocation = "";
                connection.Open();
                //If the user chooses the first radiobutton
                if (rchecked.Equals("radiobutton1"))
                {
                    //Query select based on name

                    String query = "Select ContactName,ContactFullName,TelephoneNumber,EmailAddress,Country,City,Address,BirthDate,FavoriteSong,Picture from Contacts Where ContactName=?";
                    //Command creation in order to execute the query
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("ContactName", data);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                     //Initialize picturebox imagelocation,player.Url songs path and add data to stringBuilder
                        pictureBox1.ImageLocation = reader.GetString(9);
                        builder.AppendLine("Name  " + reader.GetString(0) +
                        Environment.NewLine + "Last Name  " + reader.GetString(1) + Environment.NewLine + "Telephone Number  " + reader.GetString(2)
                         + Environment.NewLine + "EmailAddress  " + reader.GetString(3) + Environment.NewLine +
                        "Country " + reader.GetString(4) + Environment.NewLine + "City  " + reader.GetString(5) +
                        Environment.NewLine + "Address  " + reader.GetString(6) + Environment.NewLine + "Birth Date  "
                        + reader.GetString(7) + Environment.NewLine + "Favorite song " + reader.GetString(8)
                        + Environment.NewLine + "Picture " + reader.GetString(9) + Environment.NewLine + "------------------");
                        String song = reader.GetString(8);
                        player.URL = song;
                        player.controls.play();
                    }
                }
                else if (rchecked.Equals("radiobutton2"))
                {
                    //Query select based on Lastname
                    String query = "Select ContactName,ContactFullName,TelephoneNumber,EmailAddress,Country,City,Address,BirthDate,FavoriteSong,Picture from Contacts Where ContactFullName=?";
                    //Command creation in order to execute the query
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("ContactFullName", data);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //Initialize picturebox imagelocation,player.Url songs path and add data to stringBuilder
                        pictureBox1.ImageLocation = reader.GetString(9);
                        builder.AppendLine("Name  " + reader.GetString(0) +
                        Environment.NewLine + "Last Name  " + reader.GetString(1) + Environment.NewLine + "Telephone Number  " + reader.GetString(2)
                        + Environment.NewLine + "EmailAddress  " + reader.GetString(3) + Environment.NewLine +
                        "Country " + reader.GetString(4) + Environment.NewLine + "City  " + reader.GetString(5) +
                        Environment.NewLine + "Address  " + reader.GetString(6) + Environment.NewLine + "Birth Date  "
                        + reader.GetString(7)+ Environment.NewLine+"Favorite song " +reader.GetString(8)
                        + Environment.NewLine+"Picture " +reader.GetString(9)+Environment.NewLine+"------------------");
                        String song = reader.GetString(8);
                        player.URL = song;
                        player.controls.play();
                    }
                }
                else if (rchecked.Equals("radiobutton3"))
                {
                    //Query select based on telephoneNumber
                    String query = "Select ContactName,ContactFullName,TelephoneNumber,EmailAddress,Country,City,Address,BirthDate,FavoriteSong,Picture from Contacts Where TelephoneNumber=?";
                    //Initialize picturebox imagelocation,player.Url songs path and add data to stringBuilder
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("TelephoneNumber", data);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //Initialize picturebox imagelocation,player.Url songs path and add data to stringBuilder
                        pictureBox1.ImageLocation = reader.GetString(9);
                        builder.AppendLine("Name  " + reader.GetString(0) +
                        Environment.NewLine + "Last Name  " + reader.GetString(1) + Environment.NewLine + "Telephone Number  " + reader.GetString(2)
                        + Environment.NewLine + "EmailAddress  " + reader.GetString(3) + Environment.NewLine +
                        "Country " + reader.GetString(4) + Environment.NewLine + "City  " + reader.GetString(5) +
                        Environment.NewLine + "Address  " + reader.GetString(6) + Environment.NewLine + "Birth Date  "
                        + reader.GetString(7) + Environment.NewLine + "Favorite song " + reader.GetString(8)
                        + Environment.NewLine + "Picture " + reader.GetString(9) + Environment.NewLine + "------------------");
                        String song = reader.GetString(8);
                        player.URL = song;
                        player.controls.play();
                    }
                }
                //close connection
                connection.Close();
                richTextBox1.Clear();
                //Add stringbuilder contents to richtextbox and show
                richTextBox1.AppendText(builder.ToString());
                //If stringbuilder is empty then the procdures above failed
                if (builder.ToString().Equals(""))
                {
                    MessageBox.Show("The contact based on fields you gave does not exist!!!");
                }
            }
        }
        //Check if radioButtons are checked 
        private bool CheckRadio()
        {

            if (radioButton1.Checked == true)
            {
                rchecked = "radiobutton1";
                return true;
            }
            else if (radioButton2.Checked == true)
            {
                rchecked = "radiobutton2";
                return true;
            }
            else if (radioButton3.Checked == true)
            {
                rchecked = "radiobutton3";
                return true;
            }
            else
            {
                MessageBox.Show("Choose a search method!!!");
                return false;
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            //Create connection
            connection = new OleDbConnection(connectionString);
            //Stop the music
            player.controls.stop();
        }
        //Stop the music player
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.controls.stop();
        }
    }
}
