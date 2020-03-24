using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
namespace atomikiergasia2
{
    //Δημήτριου Δημήτρης Π18036
    public partial class Form2 : Form
    {
        String picture;
        string date;
        String name;
        String lname;
        String telephone;
        String country;
        String city;
        String email;
        String address;
        String song;
        int count1 = 0;
        //Create WindowsMediaPlayer object in order to play music
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        //Create connectionString
        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatabaseAtomiki.mdb";
        OleDbConnection connection;
        public Form2()
        {
            InitializeComponent();

        }
        
        //Create openfileDialog in order to choose picture.Filter png is included.
        private void button3_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null && !openFileDialog1.FileName.Equals(""))
            {
                picture = openFileDialog1.FileName;
            }
            pictureBox1.ImageLocation = picture;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Initial directories showed when the openfiledialogues open
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Pictures";
            openFileDialog2.InitialDirectory = Application.StartupPath + "\\Music";
            //Choose only one date on month calendar
            monthCalendar1.MaxSelectionCount = 1;
            //Create connection
            connection = new OleDbConnection(connectionString);
            //Stop music playing
            player.controls.stop();
        }
        /*Method to check whereas the email is valid or not using regular expression.
         * Also checks if textbox6 text is empty or null
         */
        public Boolean isValidEmail()
        {
            email = textBox6.Text;
            string pattern= "^[a-zA-Z0-9_+&*-]+(?:\\." +
                            "[a-zA-Z0-9_+&*-]+)*@" +
                            "(?:[a-zA-Z0-9-]+\\.)+[a-z" +
                            "A-Z]{2,7}$";
            if (email.Equals("") || email == null)
            {
                MessageBox.Show("Email null or empty");
                return false;
            }
            else if (!Regex.IsMatch(email, pattern))
            {
                MessageBox.Show("Email does not follows the standards");
                return false;
            }
            else
            {
                return true;
            }
        }
        /*Method to check whereas the telephone is valid(up to ten digits) or not using regular expression.
         * Also checks if textbox3 text is empty or null
         */
        public Boolean isValidTelephone()
        {
            telephone = textBox3.Text;
            string pattern = "^[0-9]{10}$";
            if (telephone.Equals("") || telephone == null)
            {
                MessageBox.Show("Empty or null telephone");
                return false;
            }
            else if (!Regex.IsMatch(telephone, pattern))
            {
                MessageBox.Show("Numbers total 10 needed to be valid !!!");
                return false;
            }
            else
            {
                return true;
            }
        }
        //checks if parameter given is empty or null
        public Boolean CheckIfNotNull(string something)
        {
            if (something.Equals("") || something.Equals(null))
            {
                count1 += 1;
                MessageBox.Show("Type something in textbox "+count1.ToString());
                return false;
            }
            else
            {
                return true;
            }
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            name = textBox1.Text;
            lname = textBox2.Text;
            country = textBox4.Text;
            city = textBox5.Text;
            address = textBox8.Text;
            bool ok1 = isValidTelephone();
            bool ok = isValidEmail();
            bool ok2 = CheckIfNotNull(name);
            bool ok3 = CheckIfNotNull(lname);
            bool ok4 = CheckIfNotNull(country);
            bool ok5 = CheckIfNotNull(address);
            bool ok6 = CheckIfNotNull(city);
            bool ok7 = string.IsNullOrEmpty(picture);
            bool ok8 = string.IsNullOrEmpty(song);
            bool ok9 = string.IsNullOrEmpty(date);
            /*If everything is not empty or null,email and telephone 
            is valid then lets do the insert procedure in database
            */
            if (ok && ok1 && ok2 && ok3 && ok4 && ok5 && ok6 && !ok7 && !ok8 && !ok9 && dateValid)
            {
                //Open connection
                connection.Open();
               //Query parameterized insertion to database
                String query = "Insert into Contacts(ContactName,ContactFullName,TelephoneNumber,EmailAddress,Country,City,Address,BirthDate,FavoriteSong,Picture) " +
                "values (?,?,?,?,?,?,?,?,?,?)";
                //Command creation in order to execute the query
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("ContactName", name);
                command.Parameters.AddWithValue("ContactFullName", lname);
                command.Parameters.AddWithValue("TelephoneNumber", telephone);
                command.Parameters.AddWithValue("EmailAddress", email);
                command.Parameters.AddWithValue("Country", country);
                command.Parameters.AddWithValue("City", city);
                command.Parameters.AddWithValue("Address", address);
                command.Parameters.AddWithValue("BirthDate", date);
                command.Parameters.AddWithValue("FavoriteSong", song);
                command.Parameters.AddWithValue("Picture", picture);
                //Execute query with the help of command
                int count = command.ExecuteNonQuery();
                //Close query
                connection.Close();
                MessageBox.Show(count.ToString() + " row affected!");
            }
            //Something is wrong or missing
            else
            {
                MessageBox.Show("Something is wrong." +Environment.NewLine+
                    "Procedure failed.Try again!!!");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Clear all values and data
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox8.Clear();
            picture = "";
            song = "";
            pictureBox1.ImageLocation = "";
        }
        //Shows in MessageBox the date you have chosen from the monthCalendar
        bool dateValid = true;
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime date1 = monthCalendar1.SelectionRange.Start;
            DateTime today = DateTime.Now;
            if(date1.Year==today.Year && date1.Month>=today.Month && date1.Day>today.Day)
            {
                dateValid = false;
                MessageBox.Show("Wrong date.Try again");
            }
            else if(date1.Year>today.Year)
            {
                dateValid = false;
                MessageBox.Show("Wrong date.Try again");
            }
            else
            {
                dateValid = true;
                date = monthCalendar1.SelectionRange.Start.ToShortDateString();
                MessageBox.Show("Date of birth " + date);
            }
        }
        //Create openfileDialog in order to choose song.Filter .mp3 is included.
        private void button4_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileName != null && !openFileDialog2.FileName.Equals(""))
            {
                song = openFileDialog2.FileName;
                //Play the song
                player.URL = song;
                player.controls.play();
            }
            
        }
        //When the form closes stop the music
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.controls.stop();
        }

        
    }
}
