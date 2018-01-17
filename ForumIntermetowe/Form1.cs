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

namespace ForumIntermetowe
{
    public partial class Form1 : Form
    {
        String strConnection = Properties.Settings.Default.ForumConnectionString;
        SqlConnection _con;

        public Form1()
        {
            _con = new SqlConnection(strConnection);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'forumDataSet.Tematy' table. You can move, or remove it, as needed.
            this.tematyTableAdapter.Fill(this.forumDataSet.Tematy);
            
            tabPage1_Click(sender, e);
            tabPage2_Click(sender, e);
            tabPage3_Click(sender, e);
            tabPage4_Click(sender, e);
            tabPage5_Click(sender, e);
            tabPage6_Click(sender, e);
            tabPage7_Click(sender, e);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            string queryStatement = "SELECT Kategorie.Kategoria_Temat, Kategorie.Kategoria_Opis, Tematy.Temat_Nazwa, Tematy.Temat_Opis, Tematy.Temat_Data_dodania, Tematy.Temat_Data_edycji FROM Kategorie INNER JOIN Tematy ON Kategorie.Kategoria_ID = Tematy.Kategoria_ID";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);

            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView1.DataSource = table;
            // dataGridView1.Columns[dataGridView1.DisplayedColumnCount(true) - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            string queryStatement = "SELECT Tematy.Temat_Nazwa, Posty.Post_Tresc, Posty.Post_Data_dodania, Posty.Post_Data_edycji, Uzytkownicy.Uzyt_Login FROM Posty INNER JOIN Tematy ON Posty.Temat_ID = Tematy.Temat_ID INNER JOIN Uzytkownicy ON Posty.Uzyt_ID = Uzytkownicy.Uzyt_ID";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);

            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView2.DataSource = table;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            string queryStatement = "SELECT Tematy.Temat_Nazwa, Tematy.Temat_ID, Uzytkownicy.Uzyt_Login FROM Tematy INNER JOIN Uzytkownicy ON Tematy.Uzyt_ID = Uzytkownicy.Uzyt_ID";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);

            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView4.DataSource = table;

            queryStatement = "SELECT Uzyt_Login, Uzyt_ID FROM Uzytkownicy";
            _cmd = new SqlCommand(queryStatement, _con);

            _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView6.DataSource = table;

            dataGridView4.Columns[1].Visible = false;
            dataGridView6.Columns[1].Visible = false;
            dataGridView4.Columns[0].ReadOnly = true;
            dataGridView4.Columns[2].ReadOnly = true;
            dataGridView6.Columns[0].ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedUserIndex = dataGridView6.SelectedCells[0].RowIndex;
            int selectedUserID = (int)dataGridView6.Rows[selectedUserIndex].Cells[1].Value;
            int selectedTopicIndex = dataGridView4.SelectedCells[0].RowIndex;
            int selectedTopicID = (int)dataGridView4.Rows[selectedTopicIndex].Cells[1].Value;

            string queryStatement = "SELECT MAX(Post_ID) FROM Posty";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);
            _con.Open();
            int maxIDuzytkownika = (int)_cmd.ExecuteScalar();
            _con.Close();

            queryStatement = "INSERT INTO Posty (Post_ID, Temat_ID, Uzyt_ID, Post_Data_edycji, Post_Tresc, Post_Data_dodania) VALUES (" + (maxIDuzytkownika + 1) + ", " + selectedTopicID + ", " + selectedUserID + ", '" + DateTime.UtcNow + "', '" + textBox3.Text + "', '" + DateTime.UtcNow + "')";
            _cmd = new SqlCommand(queryStatement, _con);
            _con.Open();
            _cmd.ExecuteReader();
            _con.Close();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            string queryStatement = "SELECT Kategoria_Temat, Kategoria_ID FROM Kategorie";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);

            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView5.DataSource = table;

            queryStatement = "SELECT Uzyt_Login, Uzyt_ID FROM Uzytkownicy";
            _cmd = new SqlCommand(queryStatement, _con);

            _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView3.DataSource = table;

            dataGridView3.Columns[1].Visible = false;
            dataGridView5.Columns[1].Visible = false;
            dataGridView3.Columns[0].ReadOnly = true;
            dataGridView5.Columns[0].ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedUserIndex = dataGridView3.SelectedCells[0].RowIndex;
            int selectedUserID = (int)dataGridView3.Rows[selectedUserIndex].Cells[1].Value;
            int selectedCategoryIndex = dataGridView5.SelectedCells[0].RowIndex;
            int selectedCategoryID = (int)dataGridView5.Rows[selectedCategoryIndex].Cells[1].Value;

            string queryStatement = "SELECT MAX(Temat_ID) FROM Tematy";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);
            _con.Open();
            int maxIDuzytkownika = (int)_cmd.ExecuteScalar();
            _con.Close();

            // dataGridView6.Rows[selectedUser].Cells[1].Value + ", '" + DateTime.UtcNow 
            queryStatement = "INSERT INTO Tematy (Temat_ID, Uzyt_ID, Kategoria_ID, Temat_Nazwa, Temat_Opis, Temat_Data_dodania, Temat_Data_edycji) VALUES (" + (maxIDuzytkownika+1) + ", "+ selectedUserID + ", "+selectedCategoryID+", '" +textBox4.Text+"', '"+textBox5.Text+"', '" + DateTime.UtcNow + "', '" + DateTime.UtcNow + "')";
            _cmd = new SqlCommand(queryStatement, _con);
            _con.Open();
            _cmd.ExecuteReader();
            _con.Close();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {
            string queryStatement = "SELECT Temat_Nazwa, Temat_ID FROM Tematy";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);

            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView7.DataSource = table;

            dataGridView7.Columns[1].Visible = false;
            dataGridView7.Columns[0].ReadOnly = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectedTopicIndex = dataGridView7.SelectedCells[0].RowIndex;
            int selectedTopicID = (int)dataGridView7.Rows[selectedTopicIndex].Cells[1].Value;
            //"POST, DATA1, DATA2, USER, ID POSTU, ID USERA, ID TEMATU"
            string queryStatement = "SELECT Posty.Post_Tresc, Posty.Post_Data_dodania, Posty.Post_Data_edycji, Uzytkownicy.Uzyt_Login, Posty.Post_ID, Uzytkownicy.Uzyt_ID, Tematy.Temat_ID FROM Posty, Tematy, Uzytkownicy WHERE Posty.Temat_ID = Tematy.Temat_ID AND Posty.Uzyt_ID = Uzytkownicy.Uzyt_ID AND Posty.Temat_ID LIKE "+ selectedTopicID;
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);
            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView8.DataSource = table;

            dataGridView8.Columns[4].Visible = false;
            dataGridView8.Columns[5].Visible = false;
            dataGridView8.Columns[6].Visible = false;

            dataGridView8.Columns[0].ReadOnly = true;
            dataGridView8.Columns[1].ReadOnly = true;
            dataGridView8.Columns[2].ReadOnly = true;
            dataGridView8.Columns[3].ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //"POST, DATA1, DATA2, USER, ID POSTU, ID USERA, ID TEMATU"
            int selectedUserIndex = dataGridView8.SelectedCells[0].RowIndex;
            int selectedPostID = (int)dataGridView8.Rows[selectedUserIndex].Cells[4].Value;
            int selectedUserID = (int)dataGridView8.Rows[selectedUserIndex].Cells[5].Value;
            int selectedTopicID = (int)dataGridView8.Rows[selectedUserIndex].Cells[6].Value;
            //DateTime.UtcNow
            string queryStatement = "DELETE FROM Posty WHERE Posty.Post_ID LIKE "+selectedPostID+" AND Posty.Temat_ID LIKE "+selectedTopicID+" AND Posty.Uzyt_ID LIKE "+selectedUserID;
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);
            _con.Open();
            _cmd.ExecuteReader();
            _con.Close();
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
            string queryStatement = "SELECT Temat_Nazwa, Temat_ID FROM Tematy";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);

            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView9.DataSource = table;

            dataGridView9.Columns[1].Visible = false;
            dataGridView9.Columns[0].ReadOnly = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedTopicIndex = dataGridView9.SelectedCells[0].RowIndex;
            int selectedTopicID = (int)dataGridView9.Rows[selectedTopicIndex].Cells[1].Value;
            //"POST, DATA1, DATA2, USER, ID POSTU, ID USERA, ID TEMATU"
            string queryStatement = "SELECT Posty.Post_Tresc, Posty.Post_Data_dodania, Posty.Post_Data_edycji, Uzytkownicy.Uzyt_Login, Posty.Post_ID, Uzytkownicy.Uzyt_ID, Tematy.Temat_ID FROM Posty, Tematy, Uzytkownicy WHERE Posty.Temat_ID = Tematy.Temat_ID AND Posty.Uzyt_ID = Uzytkownicy.Uzyt_ID AND Posty.Temat_ID LIKE " + selectedTopicID;
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);
            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView10.DataSource = table;

            dataGridView10.Columns[4].Visible = false;
            dataGridView10.Columns[5].Visible = false;
            dataGridView10.Columns[6].Visible = false;

            dataGridView10.Columns[0].ReadOnly = true;
            dataGridView10.Columns[1].ReadOnly = true;
            dataGridView10.Columns[2].ReadOnly = true;
            dataGridView10.Columns[3].ReadOnly = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //"POST, DATA1, DATA2, USER, ID POSTU, ID USERA, ID TEMATU"
            int selectedUserIndex = dataGridView10.SelectedCells[0].RowIndex;
            int selectedPostID = (int)dataGridView10.Rows[selectedUserIndex].Cells[4].Value;
            int selectedUserID = (int)dataGridView10.Rows[selectedUserIndex].Cells[5].Value;
            int selectedTopicID = (int)dataGridView10.Rows[selectedUserIndex].Cells[6].Value;
            //DateTime.UtcNow
            string queryStatement = "UPDATE Posty SET Post_Tresc = '"+textBox7.Text+ "', Post_Data_edycji = '"+ DateTime.UtcNow + "' WHERE Posty.Post_ID LIKE " + selectedPostID + " AND Posty.Temat_ID LIKE " + selectedTopicID + " AND Posty.Uzyt_ID LIKE " + selectedUserID;
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);
            _con.Open();
            _cmd.ExecuteReader();
            _con.Close();
            textBox7.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int selectedUserIndex = dataGridView10.SelectedCells[0].RowIndex;
            textBox7.Text = dataGridView10.Rows[selectedUserIndex].Cells[0].Value.ToString();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            string queryStatement = "SELECT Uzyt_Login, Uzyt_Haslo, Uzyt_Mail, Uzyt_Uprawnienia, Uzyt_ID FROM Uzytkownicy";
            SqlCommand _cmd = new SqlCommand(queryStatement, _con);

            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
            _con.Open();
            DataTable table = new DataTable();
            _dap.Fill(table);
            _con.Close();

            dataGridView11.DataSource = table;

            dataGridView11.Columns[4].Visible = false;
            dataGridView11.Columns[0].ReadOnly = true;
            dataGridView11.Columns[1].ReadOnly = true;
            dataGridView11.Columns[2].ReadOnly = true;
            dataGridView11.Columns[3].ReadOnly = true;
        }
    }
}
