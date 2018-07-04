using System;
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

        //canClose
        protected bool required;

        //update
        protected bool updating = false;


        //inicialize
        public FrmChangeDatabase(FrmDillenSQLManagementStudio mainForm, bool databaseRequired)
        {
            this.required = databaseRequired;

            InitializeComponent();

            if(this.required)
                this.btnAddNewDatabase.Text = "Add and use new database";

            //variables to send the conection to the main form
            this.frmMain = mainForm;
            this.mySqlCon = mainForm.MySqlConnection;
            this.user = mainForm.User;

            //adding databases to the combobox
            this.conStrs = this.user.ConectionsString;
            for(int i = 0; i<this.conStrs.Count; i++)
                this.cbxChsDtBs.Items.Add(DatabaseName(this.conStrs[i]));

            if(this.cbxChsDtBs.Items.Count > 0)
                this.cbxChsDtBs.SelectedIndex = 0;
        }

        //FORM CLOSING
        protected void FrmChangeDatabase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.frmMain.MySqlConnection = this.mySqlCon;
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
                this.mySqlCon.ConnStr = conStr;

                //set last
                this.user.SetLastDatabase(this.conStrs[this.cbxChsDtBs.SelectedIndex]);
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occurred when trying to connect to the chosen database!");
                return;
            }

            //change database name in lbDatabase in the Main Form
            this.frmMain.ChangeDatabaseName(FrmChangeDatabase.DatabaseName(this.mySqlCon.ConnStr));

            MessageBox.Show("Database connected!");
            
            if (this.required)
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
                //bd
                this.user.DeleteDatabase(this.conStrs[this.cbxChsDtBs.SelectedIndex]);

                //variables
                this.conStrs.RemoveAt(this.cbxChsDtBs.SelectedIndex);
                this.cbxChsDtBs.Items.RemoveAt(this.cbxChsDtBs.SelectedIndex);
                this.cbxChsDtBs.SelectedIndex = -1;
                this.cbxChsDtBs.Text = "";

                MessageBox.Show("Database deleted!\r\nOBS: the database connect hasn't changed...");
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
            if(!this.required)
                this.btnAddNewDatabase.Text = "Update database";
            else
                this.btnAddNewDatabase.Text = "Update and use database";

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
            this.btnAddNewDatabase.Text = "Add new database";
            
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

            string conStr = "Data Source=" + this.txtDataSource.Text +
                ";Initial Catalog=" + this.txtInicialCatalog.Text +
                ";Persist Security Info=True;User ID=" + this.txtUserID.Text +
                ";Password=";

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

            string lastConStr = this.mySqlCon.ConnStr;
            string conStrNoEncryp = conStr + this.txtPassword.Text;
            try
            {
                SqlConnection auxCon = new SqlConnection();
                auxCon.ConnectionString = conStrNoEncryp;
                auxCon.Open();
            }catch(Exception err)
            {
                DialogResult result = MessageBox.Show("Can not connect to this database!\r\nDo you want to add it in your databases even so?",
                    "Attention!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if(result == DialogResult.No)
                    return;
            }

            if (this.updating)
            //delete old database
                this.user.DeleteDatabase(this.conStrs[this.cbxChsDtBs.SelectedIndex]);

            try
            {
                //add new string connection
                this.user.AddDatabase(conStrComplete);
            }
            catch(Exception err) //Exception: 2 register with the same userID and strConn (primary keys)
            {
                this.mySqlCon.ConnStr = lastConStr;
                MessageBox.Show("This database already exists!");
                return;
            }
            
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
                
                MessageBox.Show("New database added!");

                //reset textBoxes
                this.txtDataSource.Text = "";
                this.txtInicialCatalog.Text = "";
                this.txtPassword.Text = "";
                this.txtUserID.Text = "";
            }
            
            if (this.required)
                this.Close();
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

        

        //tests
        private void FrmChangeDatabase_Click(object sender, EventArgs e)
        {
            string password1 = this.txtPassword.Text;
            string encryptedPassword = Encryption.Encrypt(password1);
            string password2 = Encryption.Decrypt(encryptedPassword);

            MessageBox.Show("Password: " + password1 +
                "\n\rEncrypted: " + encryptedPassword +
                "\n\rPassword After: " + password2);

            //this.mySqlCon.ConnStr = Properties.Settings.Default.BD17188ConnectionString;
            //this.Close();
        }

    }
}
