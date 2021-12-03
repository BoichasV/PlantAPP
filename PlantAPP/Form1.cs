using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace PlantAPP
{
    public partial class Form1 : Form
    {
        public static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/varax/source/repos/PlantAPP/User.accdb;";
        public OleDbConnection myConnection;

        public bool a = false;
        public string log;
        public string idp;
        public string login;
        public string cell;
        public int rowindex;
        public int columnindex;

        Label lbl;
        Panel pnl;
        TextBox log_txt;
        TextBox pass_txt;
        TextBox passConf_txt;
        TextBox name_txt;
        TextBox age_txt;
        TextBox search_txt;
        CueTextBox logu_txt;
        CueTextBox uppass_txt;
        CueTextBox passConfu_txt;
        CueTextBox ageu_txt;
        Button btn_log;
        Button btn_reg;
        Button btn_reg_conf;
        Button btn_search;
        Button btn_add;
        Button btn_back;
        Button btn_del;
        Button btn_logout;
        Button btn_upn;
        Button btn_upa;
        Button btn_upps;
        Button btn_clos;
        MenuStrip menu;
        DataGridView data;

        class CueTextBox : TextBox
        {
            [Localizable(true)]
            public string Cue
            {
                get { return mCue; }
                set { mCue = value; updateCue(); }
            }

            private void updateCue()
            {
                if (this.IsHandleCreated && mCue != null)
                {
                    SendMessage(this.Handle, 0x1501, (IntPtr)1, mCue);
                }
            }
            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                updateCue();
            }
            private string mCue;

            // PInvoke
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);
        }
        public Form1()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectionString);
            myConnection.Open();
            this.Load += LoadEvent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //start window
        private void LoadEvent(object sender, EventArgs e)
        {
            Controls.Clear();
            pnl = new Panel();
            pnl.Location = new Point(0, 0);
            pnl.Width = 1920;
            pnl.Height = 1080;
            Controls.Add(pnl);
            lbl = new Label();
            lbl.Text = "Login";
            lbl.Visible = true;
            lbl.Location = new Point(90, 50);
            pnl.Controls.Add(lbl);
            log_txt = new TextBox();
            log_txt.Visible = true;
            log_txt.Location = new Point(60, 80);
            pnl.Controls.Add(log_txt);
            lbl = new Label();
            lbl.Text = "Password";
            lbl.Visible = true;
            lbl.Location = new Point(80, 120);
            pnl.Controls.Add(lbl);
            pass_txt = new TextBox();
            pass_txt.PasswordChar = '*';
            pass_txt.Visible = true;
            pass_txt.Location = new Point(60, 150);
            pnl.Controls.Add(pass_txt);
            btn_log = new Button();
            btn_log.Text = "Log in";
            btn_log.Visible = true;
            btn_log.Location = new Point(70, 200);
            pnl.Controls.Add(btn_log);
            btn_log.Click += new EventHandler(btn_log_Click);
            lbl = new Label();
            lbl.Text = "OR";
            lbl.Visible = true;
            lbl.Location = new Point(95, 235);
            pnl.Controls.Add(lbl);
            btn_reg = new Button();
            btn_reg.Text = "Registration";
            btn_reg.Visible = true;
            btn_reg.Location = new Point(70, 260);
            pnl.Controls.Add(btn_reg);
            btn_reg.Click += new EventHandler(btn_reg_Click);

        }
        //login
        private void btn_log_Click(object sender, EventArgs e)
        {
            string query = "SELECT id, Login, User_pass, User_name FROM Users";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader dbReader = command.ExecuteReader();
            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Error!");
            }
            else
            {
                while (dbReader.Read())
                {
                    if (log_txt.Text == dbReader["Login"].ToString() && pass_txt.Text == dbReader["User_pass"].ToString())
                    {
                        a = true;
                        log = dbReader["Login"].ToString();
                        login = dbReader["id"].ToString();
                        this.Load += SuccEvent;
                        SuccEvent(log, null);
                    }
                }
                if (!a)
                {
                    MessageBox.Show("Wrong login/password!");
                }
            }
        }
        //main window
        private void SuccEvent(object sender, EventArgs e)
        {
            var prof = new System.Windows.Forms.ToolStripLabel();
            var plan = new System.Windows.Forms.ToolStripLabel();
            var exp = new System.Windows.Forms.ToolStripLabel();
            Controls.Clear();
            pnl = new Panel();
            pnl.Location = new Point(0, 0);
            pnl.Width = 1920;
            pnl.Height = 1080;
            Controls.Add(pnl);
            prof.Visible = true;
            prof.Text = "My profile";
            prof.Click += new EventHandler(prof_Click);
            plan.Visible = true;
            plan.Text = "My Plant";
            plan.Click += new EventHandler(plan_Click);
            exp.Visible = true;
            exp.Text = "Explore";
            exp.Click += new EventHandler(exp_Click);
            menu = new MenuStrip();
            menu.Location = new Point(10, 50);
            menu.Items.Add(prof);
            menu.Items.Add(plan);
            menu.Items.Add(exp);
            pnl.Controls.Add(menu);
            lbl = new Label();
            lbl.Text = "Welcome, " + log;
            lbl.Visible = true;
            lbl.Location = new Point(70, 150);
            pnl.Controls.Add(lbl);


        }
        //explore
        private void exp_Click(object sender, EventArgs e)
        {
            pnl.Controls.Clear();
            search_txt = new TextBox();
            search_txt.Visible = true;
            search_txt.Location = new Point(10, 10);
            search_txt.Width = 150;
            search_txt.Height = 40;
            pnl.Controls.Add(search_txt);
            btn_search = new Button();
            btn_search.Text = "Search: ";
            btn_search.Visible = true;
            btn_search.Location = new Point(165, 10);
            btn_search.Width = 50;
            btn_search.Click += new EventHandler(btn_search_Click);
            pnl.Controls.Add(btn_search);
            data = new DataGridView();
            data.Visible = true;
            data.Location = new Point(10, 50);
            data.Width = 195;
            data.Height = 200;
            pnl.Controls.Add(data);
            data.Rows.Clear();
            string query1 = "SELECT Plant FROM Plants";
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query1, myConnection);
            adapter.Fill(ds);
            data.DataSource = ds.Tables[0];
            btn_add = new Button();
            btn_add.Text = "Add";
            btn_add.Visible = true;
            btn_add.Location = new Point(70, 270);
            pnl.Controls.Add(btn_add);
            btn_add.Click += new EventHandler(btn_add_Click);
            btn_back = new Button();
            btn_back.Text = "On Main";
            btn_back.Visible = true;
            btn_back.Location = new Point(70, 300);
            pnl.Controls.Add(btn_back);
            btn_back.Click += new EventHandler(btn_back_Click);


        }
        //go to main window
        private void btn_back_Click(object sender, EventArgs e)
        {
            SuccEvent(null, null);
        }
        //add plant to profile
        private void btn_add_Click(object sender, EventArgs e)
        {
            rowindex = data.CurrentCell.RowIndex;
            columnindex = data.CurrentCell.ColumnIndex;
            string query = $"SELECT id_plant FROM Plants WHERE Plant = '{ data.Rows[rowindex].Cells[columnindex].Value.ToString()}'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader dbReader = command.ExecuteReader();
            while (dbReader.Read())
            {
                idp = dbReader["id_plant"].ToString();
            }
            string query_add = $"INSERT INTO Test (id_user, id_plant) VALUES ('{login}', {idp})";
            OleDbCommand command_ins = new OleDbCommand(query_add, myConnection);
            int result = command_ins.ExecuteNonQuery();
            if (result != 0)
            {
                MessageBox.Show("Plant succesfull added!");
                SuccEvent(null, null);

            }
            else
            {
                MessageBox.Show("Error!");
            }

        }
        //filter
        private void btn_search_Click(object sender, EventArgs e)
        {
            (data.DataSource as DataTable).DefaultView.RowFilter = $"Plant LIKE '%{search_txt.Text}%'";
        }
        //list of plants on profile
        private void plan_Click(object sender, EventArgs e)
        {
            pnl.Controls.Clear();
            lbl = new Label();
            lbl.Text = log + "`s garden";
            lbl.Visible = true;
            lbl.Location = new Point(10, 10);
            pnl.Controls.Add(lbl);
            data = new DataGridView();
            data.Visible = true;
            data.Location = new Point(10, 50);
            data.Width = 195;
            data.Height = 200;
            pnl.Controls.Add(data);
            data.Rows.Clear();
            string query2 = $"SELECT Plant, Spec FROM Plants LEFT JOIN Test ON Plants.id_plant = Test.id_plant WHERE Test.id_user = {login}";
            DataSet ds1 = new DataSet();
            OleDbDataAdapter adapter1 = new OleDbDataAdapter(query2, myConnection);
            adapter1.Fill(ds1);
            data.DataSource = ds1.Tables[0];
            btn_back = new Button();
            btn_back.Text = "On Main";
            btn_back.Visible = true;
            btn_back.Location = new Point(70, 300);
            pnl.Controls.Add(btn_back);
            btn_back.Click += new EventHandler(btn_back_Click);
            btn_del = new Button();
            btn_del.Text = "Delete";
            btn_del.Visible = true;
            btn_del.Location = new Point(70, 270);
            pnl.Controls.Add(btn_del);
            btn_del.Click += new EventHandler(btn_del_Click);
        }
        //delete plant from profile
        private void btn_del_Click(object sender, EventArgs e)
        {
            rowindex = data.CurrentCell.RowIndex;
            columnindex = data.CurrentCell.ColumnIndex;
            string query = $"SELECT id_plant FROM Plants WHERE Plant = '{ data.Rows[rowindex].Cells[columnindex].Value.ToString()}'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader dbReader = command.ExecuteReader();
            while (dbReader.Read())
            {
                idp = dbReader["id_plant"].ToString();
            }
            string query_del = $"Delete * FROM Test WHERE id_user = {login} AND id_plant = {idp}";
            OleDbCommand command_ins = new OleDbCommand(query_del, myConnection);
            int result = command_ins.ExecuteNonQuery();
            if (result != 0)
            {
                MessageBox.Show("Plant succesfull deleted!");
                SuccEvent(null, null);
            }
            else
            {
                MessageBox.Show("Error!");
            }

        }
        //profile
        private void prof_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Users";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader dbReader = command.ExecuteReader();
            pnl.Controls.Clear();
            lbl = new Label();
            lbl.Text = "Hello, " + log;
            lbl.Visible = true;
            lbl.Location = new Point(10, 10);
            pnl.Controls.Add(lbl);
            logu_txt = new CueTextBox();
            logu_txt.Cue = ("Enter new Name");
            logu_txt.Visible = true;
            logu_txt.Location = new Point(10, 40);
            pnl.Controls.Add(logu_txt);
            btn_back = new Button();
            btn_back.Text = "On Main";
            btn_back.Visible = true;
            btn_back.Location = new Point(70, 300);
            pnl.Controls.Add(btn_back);
            btn_back.Click += new EventHandler(btn_back_Click);
            btn_logout = new Button();
            btn_logout.Text = "Log Out";
            btn_logout.Visible = true;
            btn_logout.Location = new Point(70, 260);
            pnl.Controls.Add(btn_logout);
            btn_logout.Click += new EventHandler(btn_logout_Click);
            btn_upn = new Button();
            btn_upn.Text = "Update";
            btn_upn.Visible = true;
            btn_upn.Location = new Point(120, 38);
            pnl.Controls.Add(btn_upn);
            btn_upn.Click += new EventHandler(btn_upn_Click);
            ageu_txt = new CueTextBox();
            ageu_txt.Cue = ("Enter new Age");
            ageu_txt.Visible = true;
            ageu_txt.Location = new Point(10, 70);
            pnl.Controls.Add(ageu_txt);
            btn_upa = new Button();
            btn_upa.Text = "Update";
            btn_upa.Visible = true;
            btn_upa.Location = new Point(120, 68);
            pnl.Controls.Add(btn_upa);
            btn_upa.Click += new EventHandler(btn_upa_Click);
            uppass_txt = new CueTextBox();
            uppass_txt.PasswordChar = '*';
            uppass_txt.Cue = ("Enter new Password");
            uppass_txt.Visible = true;
            uppass_txt.Location = new Point(10, 100);
            pnl.Controls.Add(uppass_txt);
            passConfu_txt = new CueTextBox();
            passConfu_txt.PasswordChar = '*';
            passConfu_txt.Cue = ("Confirm new Password");
            passConfu_txt.Visible = true;
            passConfu_txt.Location = new Point(10, 130);
            pnl.Controls.Add(passConfu_txt);
            btn_upps = new Button();
            btn_upps.Text = "Update";
            btn_upps.Visible = true;
            btn_upps.Location = new Point(120, 128);
            pnl.Controls.Add(btn_upps);
            btn_upps.Click += new EventHandler(btn_upps_Click);
            btn_clos = new Button();
            btn_clos.Text = "Exit";
            btn_clos.Visible = true;
            btn_clos.Location = new Point(70, 330);
            pnl.Controls.Add(btn_clos);
            btn_clos.Click += new EventHandler(btn_clos_Click);
        }
        //exit
        private void btn_clos_Click(object sender, EventArgs e)
        {
            Close();
        }
        //update password
        private void btn_upps_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(uppass_txt.Text) && !string.IsNullOrWhiteSpace(passConfu_txt.Text) && uppass_txt.Text == passConfu_txt.Text)
            {
                string query = "SELECT User_pass FROM Users";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader dbReader = command.ExecuteReader();
                bool pass_isset = false;
                while (dbReader.Read())
                {
                    if (uppass_txt.Text == dbReader["User_pass"].ToString())
                    {
                        pass_isset = true;

                    }
                }

                if (pass_isset)
                {
                    MessageBox.Show($"You have the same password");
                    SuccEvent(null, null);
                }
                else
                {
                    string query_ins = $"UPDATE Users SET User_pass = '{uppass_txt.Text}' WHERE id = {login}";
                    OleDbCommand command_ins = new OleDbCommand(query_ins, myConnection);
                    int result = command_ins.ExecuteNonQuery();
                    if (result != 0)
                    {
                        MessageBox.Show($"Succesfull changed!");
                        SuccEvent(null, null);

                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }
                }


            }
            else
            {
                MessageBox.Show("Fill selected field or check coincidence!");
            }
        }
        //update age
        private void btn_upa_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(ageu_txt.Text))
            {
                string query = "SELECT Age FROM Users";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader dbReader = command.ExecuteReader();
                bool age_isset = false;
                while (dbReader.Read())
                {
                    if (ageu_txt.Text == dbReader["Age"].ToString())
                    {
                        age_isset = true;
                      
                    }
                }

                if (age_isset)
                {
                    MessageBox.Show($"You have the same age in db");
                    SuccEvent(null, null);
                }
                else
                {
                    string query_ins = $"UPDATE Users SET Age = '{ageu_txt.Text}' WHERE id = {login}";
                    OleDbCommand command_ins = new OleDbCommand(query_ins, myConnection);
                    int result = command_ins.ExecuteNonQuery();
                    if (result != 0)
                    {
                        MessageBox.Show($"Succesfull changed!");
                        SuccEvent(null, null);

                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }
                }


            }
            else
            {
                MessageBox.Show("Fill selected field!");
            }
        }
        //update name
        private void btn_upn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(logu_txt.Text))
            {
                string query = "SELECT User_name FROM Users";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader dbReader = command.ExecuteReader();
                bool name_isset = false;
                while (dbReader.Read())
                {
                    if (logu_txt.Text == dbReader["User_name"].ToString())
                    {
                        name_isset = true;
                    }
                }

                if (name_isset)
                {
                    MessageBox.Show("You allready registered with this name!");
                    SuccEvent(null, null);
                }
                else
                {
                    string query_ins = $"UPDATE Users SET User_name = '{logu_txt.Text}' WHERE id = {login}";
                    OleDbCommand command_ins = new OleDbCommand(query_ins, myConnection);
                    int result = command_ins.ExecuteNonQuery();
                    if (result != 0)
                    {
                        MessageBox.Show("Succesfull!");
                        SuccEvent(null, null);

                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }
                }


            }
            else
            {
                MessageBox.Show("Fill selected field!");
            }
        }
        //log out
        private void btn_logout_Click(object sender, EventArgs e)
        {
            LoadEvent(null, null);
        }
        //registration
        private void btn_reg_Click(object sender, EventArgs e)
        {
            pnl.Controls.Clear();
            lbl = new Label();
            lbl.Text = "Create login";
            lbl.Visible = true;
            lbl.Location = new Point(90, 30);
            pnl.Controls.Add(lbl);
            log_txt = new TextBox();
            log_txt.Visible = true;
            log_txt.Location = new Point(60, 60);
            pnl.Controls.Add(log_txt);
            lbl = new Label();
            lbl.Text = "Create password";
            lbl.Visible = true;
            lbl.Location = new Point(80, 90);
            pnl.Controls.Add(lbl);
            pass_txt = new TextBox();
            pass_txt.PasswordChar = '*';
            pass_txt.Visible = true;
            pass_txt.Location = new Point(60, 120);
            pnl.Controls.Add(pass_txt);
            lbl = new Label();
            lbl.Text = "Confirm password";
            lbl.Visible = true;
            lbl.Location = new Point(80, 150);
            pnl.Controls.Add(lbl);
            passConf_txt = new TextBox();
            passConf_txt.PasswordChar = '*';
            passConf_txt.Visible = true;
            passConf_txt.Location = new Point(60, 180);
            pnl.Controls.Add(passConf_txt);
            lbl = new Label();
            lbl.Text = "Enter you`re name";
            lbl.Visible = true;
            lbl.Location = new Point(80, 210);
            pnl.Controls.Add(lbl);
            name_txt = new TextBox();
            name_txt.Visible = true;
            name_txt.Location = new Point(60, 240);
            pnl.Controls.Add(name_txt);
            lbl = new Label();
            lbl.Text = "Enter you`re age";
            lbl.Visible = true;
            lbl.Location = new Point(80, 270);
            pnl.Controls.Add(lbl);
            age_txt = new TextBox();
            age_txt.Visible = true;
            age_txt.Location = new Point(60, 300);
            pnl.Controls.Add(age_txt);
            btn_reg_conf = new Button();
            btn_reg_conf.Text = "Registration";
            btn_reg_conf.Visible = true;
            btn_reg_conf.Location = new Point(70, 330);
            pnl.Controls.Add(btn_reg_conf);
            btn_reg_conf.Click += new EventHandler(btn_reg_conf_Click);


        }
        //registration confirm
        private void btn_reg_conf_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(log_txt.Text) && !string.IsNullOrWhiteSpace(pass_txt.Text) && pass_txt.Text == passConf_txt.Text && !string.IsNullOrWhiteSpace(name_txt.Text) && !string.IsNullOrWhiteSpace(age_txt.Text))
            {
                string query = "SELECT Login FROM Users";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader dbReader = command.ExecuteReader();
                bool login_isset = false;
                while (dbReader.Read())
                {
                    if (log_txt.Text == dbReader["Login"].ToString())
                    {
                        login_isset = true;
                    }
                }

                if (login_isset)
                {
                    MessageBox.Show("We allready get user with same login");
                    LoadEvent(null, null);
                }
                else
                {
                    string query_ins = $"INSERT INTO Users (Login, User_pass, User_name, Age) VALUES ('{log_txt.Text}', '{pass_txt.Text}', '{name_txt.Text}', '{age_txt.Text}')";
                    OleDbCommand command_ins = new OleDbCommand(query_ins, myConnection);
                    int result = command_ins.ExecuteNonQuery();
                    if (result != 0)
                    {
                        MessageBox.Show("Succesfull!");
                        LoadEvent(null, null);

                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }
                }


            }
            else
            {
                MessageBox.Show("Fill all fields!");
            }

        }
        //close db
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }
    }
}
