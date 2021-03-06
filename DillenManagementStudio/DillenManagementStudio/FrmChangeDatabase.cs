﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//to work with the database
using System.Data.SqlClient;
using System.Threading;

namespace DillenManagementStudio
{
    public partial class FrmChangeDatabase : Form
    {
        //variables to send the conection to the main form
        protected FrmDillenSQLManagementStudio frmMain;
        protected MySqlConnection mySqlCon;
        protected User user;

        //conection strings
        protected List<string> conStrs;

        //update
        protected bool updating = false;

        //to inicialize
        protected bool firstTime;


        //inicialize
        public FrmChangeDatabase(FrmDillenSQLManagementStudio mainForm, bool firstTime, List<string> conStrs = null)
        {
            InitializeComponent();

            //variables to send the conection to the main form
            this.frmMain = mainForm;
            this.mySqlCon = mainForm.MySqlConnection;

            if (String.IsNullOrEmpty(this.mySqlCon.ConnStr))
                this.btnDisconnect.Enabled = false;

            //user
            if(conStrs != null)
            {
                this.conStrs = conStrs;
                this.PutConStrInCbx();
            }
            this.User = mainForm.User;

            this.firstTime = firstTime;
        }

        protected void FrmChangeDatabase_Shown(object sender, EventArgs e)
        {
            //if VPN Unicamp is not connected and cbx is empty
            if (this.firstTime && this.user == null && this.cbxChsDtBs.Items.Count <= 0)
                MessageBox.Show("You are not connected with Unicamp VPN... If you want to learn more about commands or have your databases saved, connect it!");
        }

        public User User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.user = value;

                //adding databases to the combobox
                if (this.user != null)
                {
                    if (this.conStrs == null)
                        this.conStrs = this.user.ConectionsString;

                    this.PutConStrInCbx();

                    //visual
                    this.lbTitle1.Enabled = true;
                    this.cbxChsDtBs.Enabled = true;
                    this.btnSelectDatabase.Enabled = true;
                    this.btnUpdateDatabase.Enabled = true;
                    this.btnDeleteDatabase.Enabled = true;
                    this.btnAddNewDatabase.Text = "Connect with new database";
                }
                else
                {
                    //visual

                    if (this.cbxChsDtBs.Items.Count <= 0)
                    {
                        this.lbTitle1.Enabled = false;
                        this.cbxChsDtBs.Enabled = false;
                        this.btnSelectDatabase.Enabled = false;
                    }

                    this.btnUpdateDatabase.Enabled = false;
                    this.btnDeleteDatabase.Enabled = false;

                    this.btnAddNewDatabase.Text = "Connect";
                }
            }
        }
        
        protected void PutConStrInCbx()
        {
            //clear
            while (this.cbxChsDtBs.Items.Count > 0)
                this.cbxChsDtBs.Items.RemoveAt(0);
            for (int i = 0; i < this.conStrs.Count; i++)
                this.cbxChsDtBs.Items.Add(DatabaseName(this.conStrs[i]));

            if (this.cbxChsDtBs.Items.Count > 0)
                this.cbxChsDtBs.SelectedIndex = 0;
        }
        
        protected void FrmChangeDatabase_Load(object sender, EventArgs e)
        {
            Force.Focus(this.btnSelectDatabase);
        }


        //FORM CLOSING
        protected void FrmChangeDatabase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.frmMain.MySqlConnection = this.mySqlCon;
        }

        public List<string> ConnectionStrings
        {
            get
            {
                return this.conStrs;
            }
        }


        //SELECT
        protected void btnSelectDatabase_Click(object sender, EventArgs e)
        {
            if (this.cbxChsDtBs.SelectedIndex < 0)
            {
                MessageBox.Show("Choose a valid database!");
                return;
            }

            try
            {
                //change database
                string choosenConStr = this.conStrs[this.cbxChsDtBs.SelectedIndex];
                string password = Encryption.Decrypt(
                    choosenConStr.Substring(choosenConStr.LastIndexOf("Password=") + 9));

                string conStr = choosenConStr.Substring(0, choosenConStr.LastIndexOf("Password=") + 9) + password;

                //SPENDS JUST 5 SECONDS TO KNOW IF DATABASE IS CONNECTED OR NOT
                string dataSource = conStr.Substring(12, conStr.IndexOf("Initial Catalog=") - 13);
                if (new System.Net.NetworkInformation.Ping().Send(dataSource).Status ==
                System.Net.NetworkInformation.IPStatus.TimedOut)
                    throw new Exception("XXXX");

                //SEE IF OTHER FIELDS ARE RIGHT
                this.mySqlCon.ConnStr = conStr;
                
                try
                {
                    //set last
                    this.user.SetLastDatabase(this.conStrs[this.cbxChsDtBs.SelectedIndex]);
                }
                catch(Exception err2)
                {}
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occurred when trying to connect to the chosen database!");
                return;
            }

            //change database name in lbDatabase in the Main Form
            this.frmMain.ChangeDatabaseName(FrmChangeDatabase.DatabaseName(this.mySqlCon.ConnStr));

            this.Close();
        }


        //DELETE
        protected void btnDeleteDatabase_Click(object sender, EventArgs e)
        {
            if (this.cbxChsDtBs.SelectedIndex < 0)
            {
                MessageBox.Show("Choose a valid database!");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this database?", "Attention!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    //bd
                    this.user.DeleteDatabase(this.conStrs[this.cbxChsDtBs.SelectedIndex]);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Unicamp VPN was disconnected! This resource is not available...");
                    this.User = null;
                    this.frmMain.User = null;
                    return;
                }

                string currConStr = this.mySqlCon.ConnStr;
                if(!String.IsNullOrEmpty(currConStr))
                {
                    int indexPass = currConStr.LastIndexOf(";Password=") + 10;
                    currConStr = currConStr.Substring(0, indexPass) + Encryption.Encrypt(currConStr.Substring(indexPass));
                    if (this.conStrs[this.cbxChsDtBs.SelectedIndex] == currConStr)
                    {
                        //disconnects if user deleted the database that he is in
                        this.mySqlCon.ConnStr = null;
                        this.frmMain.ChangeDatabaseName(null);
                        this.btnDisconnect.Enabled = false;
                    }
                }

                //variables
                this.conStrs.RemoveAt(this.cbxChsDtBs.SelectedIndex);
                this.cbxChsDtBs.Items.RemoveAt(this.cbxChsDtBs.SelectedIndex);
                if (this.cbxChsDtBs.Items.Count > 0)
                    this.cbxChsDtBs.SelectedIndex = 0;
                else
                {
                    this.cbxChsDtBs.SelectedIndex = -1;
                    this.cbxChsDtBs.Text = "";
                }
            }
        }


        //UPDATE
        protected void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            if (this.cbxChsDtBs.SelectedIndex < 0)
            {
                MessageBox.Show("Choose a valid database!");
                return;
            }

            this.updating = true;

            //disable all buttons except update buttons
            this.cbxChsDtBs.Enabled = false;
            this.btnSelectDatabase.Enabled = false;
            this.btnUpdateDatabase.Enabled = false;
            this.btnDeleteDatabase.Enabled = false;

            //enable and change names of the update buttons
            this.btnCancel.Visible = true;
            this.btnAddNewDatabase.Text = "Update database";

            string strCon = this.conStrs[this.cbxChsDtBs.SelectedIndex];

            //datasource
            string dataSource = strCon.Substring(12, strCon.IndexOf("Initial Catalog=") - 13);
            this.txtDataSource.Text = dataSource;

            //initialCatalog
            int start = strCon.IndexOf("Initial Catalog=") + 16;
            string initialCatalog = strCon.Substring(start,
                strCon.IndexOf("Persist Security Info=") - 1 - start);
            this.txtInicialCatalog.Text = initialCatalog;

            //userID
            start = strCon.IndexOf("User ID=") + 8;
            string userID = strCon.Substring(start,
                strCon.IndexOf("Password=") - 1 - start);
            this.txtUserID.Text = userID;

            //password
            string password = strCon.Substring(strCon.LastIndexOf("Password=") + 9);
            this.txtPassword.Text = Encryption.Decrypt(password);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.updating = false;

            //enable all buttons except update buttons
            this.cbxChsDtBs.Enabled = true;
            this.btnSelectDatabase.Enabled = true;
            this.btnUpdateDatabase.Enabled = true;
            this.btnDeleteDatabase.Enabled = true;

            //enable and change names of the update buttons
            this.btnCancel.Visible = false;
            this.btnAddNewDatabase.Text = "Connect with new database";
            
            //cancel all texts
            this.txtDataSource.Text = "";
            this.txtInicialCatalog.Text = "";
            this.txtUserID.Text = "";
            this.txtPassword.Text = "";
        }


        // NEW DATABASE
        protected void btnAddNewDatabase_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(this.txtDataSource.Text) ||
                String.IsNullOrEmpty(this.txtInicialCatalog.Text) ||
                String.IsNullOrEmpty(this.txtPassword.Text) ||
                String.IsNullOrEmpty(this.txtUserID.Text))
            {
                MessageBox.Show("Complete all fields!");
                return;
            }

            string conStr = this.GetConnStrWithoutPasswFromFields();
            
            string conStrComplete = conStr + Encryption.Encrypt(this.txtPassword.Text);
            if (this.updating && this.conStrs[this.cbxChsDtBs.SelectedIndex] == conStrComplete)
            {
                MessageBox.Show("Update to a new database!");
                return;
            }

            if (this.updating)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to update this database?", "Attention!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;
            }
            
            string conStrNoEncryp = conStr + this.txtPassword.Text;
            bool canConnect = true;
            SqlConnection auxCon = null;
            try
            {
                //SPENDS JUST 5 SECONDS TO KNOW IF DATABASE IS CONNECTED OR NOT
                if (new System.Net.NetworkInformation.Ping().Send(this.txtDataSource.Text).Status ==
                System.Net.NetworkInformation.IPStatus.TimedOut)
                    throw new Exception("XXXX");

                //SEE IF OTHER FIELDS ARE RIGHT
                if (this.user == null)
                    //connect to new database
                    this.mySqlCon.ConnStr = conStrNoEncryp;
                else
                {
                    auxCon = new SqlConnection();
                    auxCon.ConnectionString = conStrNoEncryp;
                    auxCon.Open();
                }
            }catch(Exception err)
            {
                DialogResult result = DialogResult.No;
                if (this.user == null)
                    MessageBox.Show("Cannot connect to this database!",
                        "Connection Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    result = MessageBox.Show("Cannot connect to this database!\r\nDo you want to add it in your databases even so?",
                        "Attention!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if(result == DialogResult.No)
                    return;

                canConnect = false;
            }

            if (this.user == null)
            {
                //change database name in lbDatabase in the Main Form
                this.frmMain.ChangeDatabaseName(FrmChangeDatabase.DatabaseName(this.mySqlCon.ConnStr));
                this.Close();
                return;
            }

            //checks if this database already exists
            if(this.DatabaseExists(conStrComplete))
            {
                MessageBox.Show("This database already exists!");
                return;
            }

            if (this.updating)
            //delete old database
                this.user.DeleteDatabase(this.conStrs[this.cbxChsDtBs.SelectedIndex]);
            
            //add new string connection
            this.user.AddDatabase(conStrComplete);
            
            if(this.updating)
            {
                //variables
                this.conStrs[this.cbxChsDtBs.SelectedIndex] = conStrComplete;
                this.cbxChsDtBs.Items[this.cbxChsDtBs.SelectedIndex] = FrmChangeDatabase.DatabaseName(conStrComplete);
                                
                MessageBox.Show("Database updated!");
                this.btnCancel.PerformClick();
            }
            else
            {
                //variables
                this.conStrs.Add(conStrComplete);
                this.cbxChsDtBs.Items.Add(DatabaseName(conStrComplete));
                this.cbxChsDtBs.SelectedIndex = this.cbxChsDtBs.Items.Count - 1;
                
                if(canConnect)
                {
                    this.mySqlCon.Conn = auxCon;
                    this.frmMain.ChangeDatabaseName(FrmChangeDatabase.DatabaseName(this.mySqlCon.ConnStr));
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("New database added!");
                    //reset textBoxes
                    this.txtDataSource.Text = "";
                    this.txtInicialCatalog.Text = "";
                    this.txtPassword.Text = "";
                    this.txtUserID.Text = "";
                }
            }
        }

        protected bool DatabaseExists(string conStrComplete)
        {
            for (int i = 0; i < this.conStrs.Count; i++)
                if (conStrComplete == this.conStrs[i])
                    return true;

            return false;
        }

        protected string GetConnStrWithoutPasswFromFields()
        {
            return "Data Source=" + this.txtDataSource.Text +
                ";Initial Catalog=" + this.txtInicialCatalog.Text +
                ";Persist Security Info=True;User ID=" + this.txtUserID.Text +
                ";Password=";
        }


        // Try Connection
        protected void btnTryConn_Click(object sender, EventArgs e)
        {
            string conStr;
            bool fromCbx = String.IsNullOrEmpty(this.txtDataSource.Text) ||
                String.IsNullOrEmpty(this.txtInicialCatalog.Text) ||
                String.IsNullOrEmpty(this.txtPassword.Text) ||
                String.IsNullOrEmpty(this.txtUserID.Text);
            if (fromCbx)
            {
                //selected item in combobox

                if (this.cbxChsDtBs.SelectedIndex < 0)
                {
                    MessageBox.Show((this.cbxChsDtBs.Items.Count > 0 ? "Select a databse in combobox or c" : "C") + "omplete all fields!");
                    return;
                }

                //Try connection with database
                string choosenConStr = this.conStrs[this.cbxChsDtBs.SelectedIndex];
                string password = Encryption.Decrypt(
                    choosenConStr.Substring(choosenConStr.LastIndexOf("Password=") + 9));
                conStr = choosenConStr.Substring(0, choosenConStr.LastIndexOf("Password=") + 9) + password;
            }
            else
            {
                //every field is completed: Connection based on fields
                conStr = this.GetConnStrWithoutPasswFromFields() + this.txtPassword.Text;
            }

            bool worked = true;
            string dataSource;
            if (fromCbx)
                dataSource = conStr.Substring(12, conStr.IndexOf("Initial Catalog=") - 13);
            else
                dataSource = this.txtDataSource.Text;
            try
            {
                //SPENDS JUST 5 SECONDS TO KNOW IF DATABASE IS CONNECTED OR NOT
                if (new System.Net.NetworkInformation.Ping().Send(dataSource).Status ==
                System.Net.NetworkInformation.IPStatus.TimedOut)
                    throw new Exception("XXXX");

                //SEE IF OTHER FIELDS ARE RIGHT
                SqlConnection con = new SqlConnection(conStr);
                if (con != null)
                {
                    con.Open();
                    worked = con.IsConnected();
                }
            }
            catch (Exception err)
            {
                worked = false;
            }

            string databaseName = FrmChangeDatabase.DatabaseName(conStr);
            if (worked)
                MessageBox.Show("Successfully connection with " + databaseName + "!", "Succefully connection",
               MessageBoxButtons.OK);
            else
                MessageBox.Show("Unsuccessfully connection with " + databaseName + "!", "Unsuccefully connection",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        // Disconnect
        protected void btnDisconnect_Click(object sender, EventArgs e)
        {
            string lastDatabase = this.mySqlCon.ConnStr;
            this.mySqlCon.ConnStr = null;
            this.frmMain.ChangeDatabaseName(null);
            this.btnDisconnect.Enabled = false;
            MessageBox.Show(FrmChangeDatabase.DatabaseName(lastDatabase) + " was disconnected!");
        }


        //view password
        protected void checkBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.checkBox1.Checked = true;
            this.txtPassword.PasswordChar = '\0';
        }

        protected void checkBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.checkBox1.Checked = false;
            this.txtPassword.PasswordChar = '*';
        }


        ///auxiliaries methods
        protected static string DatabaseName(string conStr)
        {
            int start = conStr.IndexOf("Initial Catalog=") + 16;
            string currDatabase = conStr.Substring(start,
                conStr.IndexOf("Persist Security Info=") - 1 - start);

            string currDataSource = conStr.Substring(12,
                conStr.IndexOf("Initial Catalog=") - 13);
            return currDatabase + " (" + currDataSource + ")";
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }



        private void btnTryConnUnicamp_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(this.frmMain.CheckVPNConnOutOfForm)).Start();
        }
    }
}
