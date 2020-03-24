using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
namespace atomikiergasia2
{
    //Δημητρίου Δημήτρης Π18036
    public partial class Form5 : Form
    {
        //Create connectionString
        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatabaseAtomiki.mdb";
        OleDbConnection connection;
        String editName;
        String editFullName;
        String song;
        String picture;
        String name;
        String lname;
        String telephone;
        String country;
        String city;
        String email;
        String address;
        String date;
        int count1 = 0;
        //Create WindowsMediaPlayer object in order to play music
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //Create connection
            //Initial directories showed when the openfiledialogues open
            connection = new OleDbConnection(connectionString);
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Pictures";
            openFileDialog2.InitialDirectory = Application.StartupPath + "\\Music";
            //Choose only one date on month calendar
            monthCalendar1.MaxSelectionCount = 1;
            //Stop music playing
            player.controls.stop();
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Bool variable to check if search method is done
            bool f = false;
            //Stop the music
            player.controls.stop();
            editName = textBox8.Text;
            editFullName = textBox9.Text;
            //Boolean variable  false or true if textboxes are null or empty or not respectively
            bool z;
            //Check if textboxes are empty or null
            if (editName.Equals("") || editName.Equals(null) ||editFullName.Equals("")||editFullName.Equals(null))
            {
                
                z = false;
                MessageBox.Show("Type something in textboxes in order to find the contact to edit");
            }
            else
            {
                z = true;
            }
            if (z)
            {
                //Open connection
                connection.Open();
                //Query selction based on name and lastName
                String query = "Select ContactName,ContactFullName,TelephoneNumber,EmailAddress,Country,City,Address,BirthDate,FavoriteSong,Picture from Contacts Where ContactName=? and ContactFullName=?";
                //Create command to execute query
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("ContactName", editName);
                command.Parameters.AddWithValue("ContactName", editFullName);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    f = true;
                    //Get data from reader
                    textBox1.Text = reader.GetString(0);
                    textBox2.Text = reader.GetString(1);
                    textBox3.Text = reader.GetString(2);
                    textBox6.Text = reader.GetString(3);
                    textBox5.Text = reader.GetString(5);
                    textBox4.Text = reader.GetString(4);
                    textBox7.Text = reader.GetString(6);
                    date = reader.GetString(7);
                    //Convert string to date and show that to monthCalendar
                    DateTime date1 = Convert.ToDateTime(reader.GetString(7));
                    monthCalendar1.SetDate(date1);
                    //Get the song path and start playing it
                    song = reader.GetString(8);
                    player.URL = song;
                    player.controls.play();
                    picture = reader.GetString(9);
                    //Picturebox imagelocation
                    pictureBox1.ImageLocation = picture;
                }
                //Close connection
                connection.Close();
            }
            if (!f)
            {
                //clear data
                Clear();
            }
        }
        //Create openfileDialog in order to choose song.Filter .mp3 is included.
        private void button1_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileName != null && !openFileDialog2.FileName.Equals(""))
            {
                song = openFileDialog2.FileName;
                player.URL = song;
                player.controls.play();
            }
        }
        //When the form closes stop the music
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.controls.stop();
        }
        //Create openfileDialog in order to choose picture.Filter png is included.
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null && !openFileDialog1.FileName.Equals(""))
            {
                picture = openFileDialog1.FileName;

            }
            pictureBox1.ImageLocation = picture;
        }
        //Checks using regular expressions whether the email is valid or not
        public Boolean isValidEmail()
        {
            email = textBox6.Text;
            string pattern = "^[a-zA-Z0-9_+&*-]+(?:\\." +
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
        //Check something if it is null,empty or valid
        public Boolean CheckIfNotNull(string something)
        {
            if (something.Equals("") || something.Equals(null))
            {
                count1 += 1;
                MessageBox.Show("Type something in textbox " + count1.ToString());
                return false;
            }
            else
            {
                return true;
            }

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            lname = textBox2.Text;
            country = textBox4.Text;
            city = textBox5.Text;
            address = textBox7.Text;
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
            is valid then lets do the update procedure in database
            */
            if (ok1 && ok2 && ok3 && ok4 && ok5 && ok6 && !ok7 && !ok8 && !ok9)
            {
                //Open connection
                connection.Open();
                //Update is done based on name and full name of the contact
                //Query parameterized insertion to database
                String query = "UPDATE Contacts SET ContactName = ?,ContactFullName = ?,TelephoneNumber = ?,EmailAddress = ?,Country = ?,City = ?,Address = ?,BirthDate = ?,FavoriteSong = ?,Picture = ? WHERE ContactName=@CID and ContactFullName=@CID1";
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
                command.Parameters.AddWithValue("@CID", editName);
                command.Parameters.AddWithValue("@CID", editFullName);
                //Execute query with the help of command
                int count = command.ExecuteNonQuery();
                //Closes connection
                connection.Close();
                MessageBox.Show(count.ToString() + " row affected!");
            }
            else
            {
                MessageBox.Show("Process of update fail");
            }
            
        }
        bool dateValid;
        //Shows in MessageBox the date you have chosen from the monthCalendar
        //Checks if the date is Valid
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime date1 = monthCalendar1.SelectionRange.Start;
            DateTime today = DateTime.Now;
            if (date1.Year == today.Year && date1.Month >= today.Month && date1.Day > today.Day)
            {
                dateValid = false;
                MessageBox.Show("Wrong date.Try again");
            }
            else if (date1.Year > today.Year)
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
        //Clear selected fields
        private void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            picture = "";
            song = "";
            date = "";
            monthCalendar1.SetDate(DateTime.Now);
            pictureBox1.ImageLocation = "";
        }
        //Calls clear method to clear the above selected fields and cleras also textnox8,textbox9 and stop also the music
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            Clear();
            textBox9.Clear();
            textBox8.Clear();
        }
    }
}
