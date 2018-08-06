using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//to work with the database: MySqlCommand

namespace DillenManagementStudio
{
    public partial class FrmDillenSQLManagementStudio : Form
    {
        //conection
        protected string connStr;
        protected MySqlConnection mySqlCon;
        protected bool connected = false;

        //execution
        protected int lastExecution = 0;
        
        //RICH TEXT BOX
        protected SqlRichTextBox sqlRchtxtbx;
        protected bool showedDialog = false;

        //rest of form darken (except menu)
        protected const int REST_OF_FORM_DARKEN = 25;

        //general
        protected const string TITLE = "Dillen's SQL Management Studio (Dillenburg's Product)";

        //file
        protected string fileName;
        protected bool isSaved = false;

        //notification
        protected bool allowNotification = true;

        //user
        protected User user;
        protected const string MSG_UNICAMP_VPN_DISCONNECTED = "You were disconnected to the Unicamp's VPN! " +
            "If you want to learn more about commands or have your databases saved, connect it again!";

        //FrmChangeDatabase (using VPN Connection)
        protected FrmChangeDatabase frmChangeDatabase;


        //form iniciate and finish
        public FrmDillenSQLManagementStudio()
        {
            InitializeComponent();

            //ajust form
            this.AjustForm();

            //VPN Configuration Text
            this.PutVPNConfigurationTexts();

            //set font to TableName's Cell in grvSelect
            this.grvSelect.TopLeftHeaderCell.Style.Font = new Font(new FontFamily("Modern No. 20"), 10.0F, FontStyle.Bold);

            //set SQL buttons
            this.EnableWichDependsCon(false);
        }

        protected void AjustForm()
        {
            ///Size
            //Form
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //menu
            this.pnlMenu.Width = this.Width;
            this.picMenuMainSeparator.Width = this.Width;
            //RichTextBox Code
            this.rchtxtCode.Width = (int)Math.Round(0.548 * this.Width); //54.8%
            this.rchtxtCode.Height = (int)Math.Round(0.915 * (this.Height - this.pnlMenu.Height)); //91.5%
            this.rchtxtAux.Width = this.rchtxtCode.Width;
            this.rchtxtAux.Height = this.rchtxtCode.Height;
            this.pnlLoading.Width = (int)Math.Round(0.24 * this.Width); //24%
            this.pnlLoading.Height = (int)Math.Round(0.183 * (this.Height - this.pnlMenu.Height)); //16.3%
            //DataGridView Select
            this.grvSelect.Width = (int)Math.Round(0.423 * this.Width); //42.3%
            this.grvSelect.Height = (int)Math.Round(0.92 * (this.Height - this.pnlMenu.Height)); //92%


            ///LOCATION
            /////last button from menu
            this.vpnConfigurationToolStripMenuItem.Location = new Point(this.Width - this.vpnConfigurationToolStripMenuItem.Width - 4,
                this.vpnConfigurationToolStripMenuItem.Location.Y);
            this.pnlVPNConfiguration.Location = new Point(this.Width - this.pnlVPNConfiguration.Width - 4,
                this.pnlVPNConfiguration.Location.Y);
            //RichTextBox Code
            int x = (int)Math.Round(0.0088 * (this.Width)); //0.88%
            int y = (int)Math.Round(0.122 * (this.Height - this.pnlMenu.Height)); //12.8%
            this.rchtxtCode.Location = new Point(x, y);
            this.rchtxtAux.Location = this.rchtxtCode.Location;
            //DataGridView Select
            x = (int)Math.Round(0.571 * (this.Width)); //57.1%
            y = (int)Math.Round(0.158 * (this.Height - this.pnlMenu.Height)); //15.8%
            this.grvSelect.Location = new Point(x, y);
            this.lbExecutionResult.Location = new Point(this.grvSelect.Location.X + 2,
                this.grvSelect.Location.Y - this.lbExecutionResult.Height - 4);
            this.btnAllTables.Location = new Point(this.grvSelect.Location.X + this.grvSelect.Width - this.btnAllTables.Width - 2,
                this.lbExecutionResult.Location.Y - 4);
            this.btnAllProcFunc.Location = new Point(this.btnAllTables.Location.X - this.btnAllProcFunc.Width - 10,
                this.btnAllTables.Location.Y);
            // Close and Minimize
            this.btnClose.Location = new Point(this.Width - this.btnClose.Width, this.btnClose.Location.Y);
            this.btnMinimize.Location = new Point(this.btnClose.Location.X - this.btnMinimize.Width, this.btnClose.Location.Y);
            // Label Database
            x = (int)Math.Round(0.019 * (this.Width)); //1.9%
            y = (int)Math.Round(1.054 * (this.Height - this.pnlMenu.Height)); //105.4%
            this.lbDatabase.Location = new Point(x, y);
            this.pnlSearch.Location = new Point(this.rchtxtCode.Location.X + this.rchtxtCode.Width + 2,
                this.pnlSearch.Location.Y);

            //darken rest of the form
            this.opaquePanel = new OpaquePanel(REST_OF_FORM_DARKEN);
            this.opaquePanel.Visible = false;
            this.opaquePanel.BackColor = Color.Black;
            this.opaquePanel.Location = new Point(0, this.pnlMenu.Height + this.pnlMenu.Location.Y);
            this.opaquePanel.Size = new Size(this.pnlMenu.Width, this.Height - this.opaquePanel.Location.Y);
            this.opaquePanel.Click += new System.EventHandler(this.opaquePanel_Click);
            this.Controls.Add(this.opaquePanel);

            //MessageBox.Show("Width: " + (((float)this.lbDatabase.Location.X) / ((float)this.Width))*100 + "%" ); //1.9%
            //MessageBox.Show("Height: " + (((float)this.lbDatabase.Location.Y) / ((float)this.Height - this.menuStrip.Height))*100 + "%"); //105.4%
        }

        protected void FrmDillenSQLManagementStudio_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;

            //Show Splash
            FrmSplash frmSplash = new FrmSplash();
            frmSplash.Show();
            frmSplash.FormClosed += (s, args) => this.AuxSplashClosed(frmSplash);

            this.TryConnWithMyDtbs(true);

            //mySQLConnection
            this.mySqlCon = new MySqlConnection(this.user);

            //INICIALIZE RICHTEXT BOX
            this.sqlRchtxtbx = new SqlRichTextBox(ref this.rchtxtCode, this, this.mySqlCon, false);
            this.sqlRchtxtbx.SetNewEvents(new System.EventHandler(this.newRchtxtCode_TextChanged),
                new System.Windows.Forms.PreviewKeyDownEventHandler(this.newRchtxtCode_PreviewKeyDown));
            this.sqlRchtxtbx.SQLRichTextBox.KeyPress += new KeyPressEventHandler(this.newRchtxtCode_KeyPress);

            //opacity to darken the rest of the Form except the Menu
            this.opaquePanel.BringToFront();

            //panel in first place
            this.pnlFile.BringToFront();
            this.pnlEdit.BringToFront();
            this.pnlExecuteAs.BringToFront();
            this.pnlVPNConfiguration.BringToFront();

            //splash can close
            frmSplash.CanClose(this.user != null);
        }

        protected void AuxSplashClosed(FrmSplash frmSplash)
        {
            frmSplash.Dispose();
            this.ShowInTaskbar = true;
            this.Opacity = 1;
            this.ShowChangeDatabaseForm(true);
        }

        protected void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.mySqlCon.CloseConnection();
            }
            catch (Exception err)
            { }
        }


        //other form's methods
        protected void FrmDillenSQLManagementStudio_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.showedDialog = false;

            if (e.KeyCode == Keys.F5) //F5
                this.executeToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.S && e.Control) //ctrl+S
            {
                if (e.Shift)
                    this.saveAsToolStripMenuItem_Click(null, null);
                else
                    this.saveToolStripMenuItem_Click(null, null);
            }
            else
            if (e.KeyCode == Keys.F && e.Control) //ctrl+F
                this.findToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.H && e.Control) //ctrl+H
                this.replaceToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.N && e.Control) //ctrl+N
                this.newToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.O && e.Control) //ctrl+O
                this.openToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.F4 && e.Alt) //alt+F4
                this.closeToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.Oemplus && e.Control) //ctrl+[+]
                this.largerRchtxtFontToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.OemMinus && e.Control) //ctrl+[-]
                this.smallerRchtxtFontToolStripMenuItem.PerformClick();
        }

        protected void FrmDillenSQLManagementStudio_KeyDown(object sender, ref KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.F5) //F5
                this.executeToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.S && e.Control) //ctrl+S
            {
                if (e.Shift)
                    this.saveAsToolStripMenuItem_Click(null, null);
                else
                    this.saveToolStripMenuItem_Click(null, null);
            }
            else
            if (e.KeyCode == Keys.F && e.Control) //ctrl+F
                this.findToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.H && e.Control) //ctrl+H
                this.replaceToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.N && e.Control) //ctrl+N
                this.newToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.O && e.Control) //ctrl+O
                this.openToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.F4 && e.Alt) //alt+F4
                this.closeToolStripMenuItem_Click(null, null);
            else
            if (e.KeyCode == Keys.Oemplus && e.Control) //ctrl+[+]
                this.largerRchtxtFontToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.OemMinus && e.Control) //ctrl+[-]
                this.smallerRchtxtFontToolStripMenuItem.PerformClick();
            else
                e.Handled = false;
        }

        protected void EnableWichDependsCon(bool enable)
        {
            this.btnAllTables.Enabled = enable;
            this.executeAsToolStripMenuItem.Enabled = enable;
            this.btnAllProcFunc.Enabled = enable;
            this.executeToolStripMenuItem.Enabled = enable;
            this.connected = enable;
        }

        protected void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
            this.opaquePanel.Visible = false;

            if (this.AskUserWantsToSaveIfNeeded())
                this.Close();
        }

        protected void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //VPN Connection
        protected void tmrCheckVPNConn_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(this.CheckVPNConn)).Start();
        }

        public void CheckVPNConnOutOfForm()
        {
            this.Invoke((MethodInvoker)delegate
            {
                object lastUser = this.user;
                bool lastEnabled = this.tmrCheckVPNConn.Enabled;
                this.tmrCheckVPNConn.Enabled = false;
                this.CheckVPNConn();

                //if user wasn't told if could or couldn't connect
                if (lastUser != null && this.user != null)
                    this.ShowMessageRightPlace("You are still connected with Unicamp's VPN!");
                else
                if (lastUser == null && this.user == null)
                    this.ShowMessageRightPlace("Couldn't connect with Unicamp's VPN!");

                this.tmrCheckVPNConn.Enabled = lastEnabled;
            });
        }

        protected void CheckVPNConn()
        {
            this.Invoke((MethodInvoker)delegate
            {
                //if was connected
                if (this.user != null)
                {
                    //if user was disconnected
                    if (!this.user.IsConnected())
                    {
                        this.User = null;

                        if (this.frmChangeDatabase != null)
                            this.frmChangeDatabase.User = null;

                        this.ShowMessageRightPlace(MSG_UNICAMP_VPN_DISCONNECTED);
                    }
                }
                else
                    this.TryConnWithMyDtbs(false);
            });
        }

        protected void TryConnWithMyDtbs(bool firstTime)
        {
            //Forward, user is null...

            try
            {
                this.User = new User();
            }
            catch (Exception err)
            {
                /*if (firstTime)
                {
                    //MessageBox.Show("Be sure you are connected with Unicamp's VPN! Connect and restart the program!");
                    //this.Close();
                    //return;
                    MessageBox.Show("You are not connected with Unicamp's VPN... If you want to learn more about commands or have your databases saved, connect it!");
                }*/
            }

            this.Invoke((MethodInvoker)delegate
            {
                this.allCommandsSyntaxToolStripMenuItem.Enabled = this.user != null;
                if (this.user != null)
                {
                    this.user.InicializeUser();

                    if (!firstTime)
                    {
                        if (this.frmChangeDatabase != null)
                            this.frmChangeDatabase.User = this.user;

                        this.ShowMessageRightPlace("Now you are connected with Unicamp's VPN! You can learn more about commands or have your databases saved!");
                    }
                }
            });
        }

        protected void ShowMessageRightPlace(string msg)
        {
            if (this.frmChangeDatabase != null)
                this.frmChangeDatabase.ShowMessage(msg);
            else
                MessageBox.Show(msg);
        }


        //FILE MENU
        protected void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showedDialog = true;

            this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
            this.opaquePanel.Visible = false;

            //choose file
            this.saveFileDialog.Title = "New file";
            DialogResult result = this.saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (this.AskUserWantsToSaveIfNeeded())
                {
                    this.fileName = this.saveFileDialog.FileName;

                    this.sqlRchtxtbx.Clear();

                    this.isSaved = true;
                    this.ChangeTitle();
                    //creates and writes file
                    this.SaveFile();
                }
            }
        }

        protected void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showedDialog = true;

            this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
            this.opaquePanel.Visible = false;
            
            //show dialog
            DialogResult result = this.openFileDialog.ShowDialog();

            //if user selected a file
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //sees if wants to save if it's not saved yet
                if (this.AskUserWantsToSaveIfNeeded())
                {
                    this.fileName = this.openFileDialog.FileName;

                    //changes title
                    this.isSaved = true;
                    this.ChangeTitle();

                    this.sqlRchtxtbx.CopyTextFromFile(this.fileName);
                }
            }
        }

        protected void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showedDialog = true;

            this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
            this.opaquePanel.Visible = false;
            
            if (!this.isSaved)
            {
                if (String.IsNullOrEmpty(this.fileName))
                    this.saveAsToolStripMenuItem_Click(this.saveAsToolStripMenuItem, new EventArgs());
                else
                {
                    this.SaveFile();
                    this.isSaved = true;
                    this.ChangeTitle();
                }
            }
        }

        protected void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showedDialog = true;

            this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
            this.opaquePanel.Visible = false;

            this.saveFileDialog.Title = "Save as";
            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.fileName = this.saveFileDialog.FileName;

                this.isSaved = true;
                this.ChangeTitle();
                //creates and writes file
                this.SaveFile();
            }
        }

        //auxiliary file menu
        protected void SaveFile()
        {
            this.sqlRchtxtbx.SaveFile(this.fileName);
        }

        protected bool AskUserWantsToSaveIfNeeded()
        {
            if (this.isSaved || !this.sqlRchtxtbx.HasTyped)
                return true;

            DialogResult result = MessageBox.Show("Do you want to save your file?", "Save file?",
              MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                this.saveToolStripMenuItem_Click(null, null);
            else
            if (result == DialogResult.Cancel)
                return false;

            return true;
        }

        protected void ChangeTitle()
        {
            if (String.IsNullOrEmpty(this.fileName))
                this.lbTitle.Text = TITLE;
            else
                this.lbTitle.Text = this.fileName + (this.isSaved ? "" : "*") + " - " + TITLE;

            this.saveToolStripMenuItem2.Enabled = !this.isSaved;
        }


        //EXECUTE
        protected void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.automaticToolStripMenuItem_Click(null, null);
        }

        protected void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pnlExecuteAs.Visible)
            {
                this.CancelShowMore(this.editToolStripMenuItem, this.pnlExecuteAs);
                this.opaquePanel.Visible = false;
            }

            if (this.connected && this.lastExecution != 0)
                this.mySqlCon.RestartCommands();

            this.Execute(0);

            this.lastExecution = 0;
        }

        protected void executeNonQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pnlExecuteAs.Visible)
            {
                this.CancelShowMore(this.editToolStripMenuItem, this.pnlExecuteAs);
                this.opaquePanel.Visible = false;
            }

            this.Execute(1);
            this.lastExecution = 1;
        }

        protected void queryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pnlExecuteAs.Visible)
            {
                this.CancelShowMore(this.editToolStripMenuItem, this.pnlExecuteAs);
                this.opaquePanel.Visible = false;
            }

            this.Execute(2);
            this.lastExecution = 2;
        }

        protected void Execute(int executeType) //ExecuteType: 0=Automatic, 1=Non-Query, 2=Query
        {
            //get allCodes or lines without even quotation marks
            Queue<int> linesNoEvenQuotMarks = new Queue<int>();
            string allCodes = SqlExecuteProcedures.AllCodes(this.sqlRchtxtbx, ref linesNoEvenQuotMarks);
            int qtdLinesChanged = 0;

            if (linesNoEvenQuotMarks.Count > 0)
            {
                string msg = SqlExecuteProcedures.MessageFromNoEvenQuotMarks(linesNoEvenQuotMarks);

                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            if (executeType == 0)
            {
                Queue<Error> errors = new Queue<Error>();
                string tableName = "";

                //DataGridView
                DataTable dataTable = this.mySqlCon.ExecuteAutomaticSqlCommands(allCodes, ref errors,
                    ref qtdLinesChanged, ref tableName);
                this.grvSelect.TopLeftHeaderCell.Value = tableName;
                //this.lbTableName.Text = tableName;
                //this.lbTableName.Visible = true;
                this.grvSelect.DataSource = dataTable;

                bool worked = (errors == null || errors.Count == 0);

                //change label
                SqlExecuteProcedures.ChangeExecuteResultLabel(ref this.lbExecutionResult, worked, qtdLinesChanged);

                //notification
                if (this.allowNotification)
                {
                    if (worked)
                        MessageBox.Show("Succesfully executed!");
                    else
                        this.ShowErrors(errors);
                }
            }
            else
            {
                //execute one Query or Non-Query (based on the radiobutton checked)
                DataTable dataTable = null;
                bool worked = true;
                string excep = null;
                worked = this.mySqlCon.ExecuteOneSQLCmd(allCodes, executeType == 2, ref dataTable, ref excep, ref qtdLinesChanged);

                this.grvSelect.TopLeftHeaderCell.Value = "";
                //this.lbTableName.Visible = false;
                this.grvSelect.DataSource = dataTable;

                //change label
                SqlExecuteProcedures.ChangeExecuteResultLabel(ref this.lbExecutionResult, worked, qtdLinesChanged);

                //notification
                if (this.allowNotification)
                {
                    if (!worked)
                    {
                        string message = "Error in code: \n\r" +
                                              allCodes +
                                              "\n\rError:" +
                                              excep;

                        MessageBox.Show(message, "SQL Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Succesfull execution!");
                }
            }
        }

        protected void ShowErrors(Queue<Error> errors)
        {
            while (errors.Count > 0)
            {
                //ask if the user wants to know how's the syntax of the wrongs' commands
                Error currErr = errors.Dequeue();

                if (currErr.IsConnectionException)
                {
                    this.EnableWichDependsCon(false);
                    this.lbDatabase.Text = "Not connected";
                    MessageBox.Show(currErr.Exception, "SQL Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string message = "Error in code: \n\r" +
                                    currErr.Code +
                                    "\n\rError:" +
                                    currErr.Exception;

                    if (currErr.CodCommand < 0)
                        MessageBox.Show(message, "SQL Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        message += "\n\r\n\rDo you wanna know more about '" + currErr.StrCommand
                            + "' sintax?";
                        DialogResult result = MessageBox.Show(message, "SQL Error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (result == DialogResult.Yes)
                            try
                            {
                                new FrmCommandExplanation(currErr.CodCommand, this.user, this.mySqlCon).Show();
                            }
                            catch (Exception e)
                            {
                                this.User = null;
                                MessageBox.Show(e.Message);
                            }
                    }
                }
            }
        }


        //sql execute procedures
        protected void btnAllTables_Click(object sender, EventArgs e)
        {
            SqlExecuteProcedures.ChangeExecuteResultLabel(ref this.lbExecutionResult, true, 0);
            this.grvSelect.TopLeftHeaderCell.Value = "";
            //this.lbTableName.Visible = false;
            this.grvSelect.DataSource = this.mySqlCon.AllTables();
        }

        protected void btnAllProcFunc_Click(object sender, EventArgs e)
        {
            SqlExecuteProcedures.ChangeExecuteResultLabel(ref this.lbExecutionResult, true, 0);
            this.grvSelect.TopLeftHeaderCell.Value = "";
            //this.lbTableName.Visible = false;
            this.grvSelect.DataSource = this.mySqlCon.AllProcFunc();
        }


        //CHANGE DATA BASE BUTTON
        //getters and setters
        public User User
        {
            get
            {
                //can be null
                return this.user;
            }

            set
            {
                this.user = value;

                this.allCommandsSyntaxToolStripMenuItem.Enabled = (value != null);
            }
        }

        public MySqlConnection MySqlConnection
        {
            get
            {
                return this.mySqlCon;
            }

            set
            {
                this.mySqlCon = value;
            }
        }

        //show FrmChangeDatabase
        protected List<string> conStrs;
        protected void changeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowChangeDatabaseForm(false);
        }

        protected void ShowChangeDatabaseForm(bool firstTime)
        {
            string oldDatabase = this.mySqlCon.ConnStr;
            bool lastTmrEnabled = this.tmrCheckVPNConn.Enabled;
            this.frmChangeDatabase = new FrmChangeDatabase(this, firstTime, this.conStrs);
            if (!this.tmrCheckVPNConn.Enabled)
                this.tmrCheckVPNConn.Enabled = true;
            this.frmChangeDatabase.FormClosed += (s, arg) => this.ProceduresAfterNewDatabase(oldDatabase, lastTmrEnabled);
            this.frmChangeDatabase.ShowDialog();
        }

        public void ChangeDatabaseName(string databaseName)
        {
            if (databaseName == null)
            {
                this.lbDatabase.Text = "Not connected";
                this.EnableWichDependsCon(false);
            }
            else
            {
                this.lbDatabase.Text = databaseName;
                this.EnableWichDependsCon(true);
            }
        }

        protected void ProceduresAfterNewDatabase(string oldDatabase, bool lastTmrEnabled)
        {
            if (!lastTmrEnabled)
                this.tmrCheckVPNConn.Enabled = lastTmrEnabled;

            this.conStrs = this.frmChangeDatabase.ConnectionStrings;

            string newDatabase = this.mySqlCon.ConnStr;
            //if connected with a database different from the old one
            if (!String.IsNullOrEmpty(newDatabase) && newDatabase != oldDatabase)
                new Thread(() => this.AuxBtnAllTablesClick()).Start();

            //set Focus in RichTextBox
            Force.Focus(this.sqlRchtxtbx.SQLRichTextBox);

            //only lets the user execute if he has connected to a database
            //this.EnableWichDependsCon(!String.IsNullOrEmpty(this.mySqlCon.ConnStr));
            //(ITS ON CHANGEDATABASE NOW)
        }

        protected void AuxBtnAllTablesClick()
        {
            try
            {
                //while(!this.btnAllTables.CanFocus)
                //{}
                while (true)
                {
                    try
                    {
                        bool canFocus = false;
                        this.btnAllTables.Invoke(new Action(() =>
                        {
                            canFocus = this.btnAllTables.CanFocus;
                        }));

                        if (canFocus)
                            break;
                    }
                    catch (Exception e)
                    { }
                }
                //OR
                //for (; ; )
                //    if (this.btnAllTables.CanFocus)
                //        break;

                this.btnAllTables.Invoke(new Action(() => this.btnAllTables.PerformClick()));
            }
            catch (Exception e)
            { }
        }


        //all command sintax
        protected void allCommandsSintaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new FrmAllCommands(this.user, this.mySqlCon, this).Show();
            }
            catch (Exception err)
            {
                this.User = null;
                MessageBox.Show(MSG_UNICAMP_VPN_DISCONNECTED);
            }
        }


        //allow notification
        protected void allowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.allowNotification = !this.allowNotification;

            if (this.allowNotification)
                this.allowOrNotNotificationsToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.switch_on4;
            else
                this.allowOrNotNotificationsToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.switch_off2;
        }


        //SEARCH: FIND and REPLACE
        protected bool isFind = true;
        protected void btnNext_Click(object sender, EventArgs e)
        {
            StringComparison stringComparison = (this.chxIgnoreCase.Checked ? StringComparison.InvariantCultureIgnoreCase : StringComparison.CurrentCulture);

            try
            {
                this.sqlRchtxtbx.Find(this.txtFind.Text, stringComparison);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        protected void btnReplaceCurr_Click(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.Replace(this.txtReplace.Text);
        }

        protected void btnReplaceAll_Click(object sender, EventArgs e)
        {
            StringComparison stringComparison = (this.chxIgnoreCase.Checked ? StringComparison.InvariantCultureIgnoreCase : StringComparison.CurrentCulture);

            try
            {
                int qtdReplaced = this.sqlRchtxtbx.ReplaceAll(this.txtFind.Text, this.txtReplace.Text, stringComparison);
                this.pnlSearch.Visible = qtdReplaced < 0;
                if (qtdReplaced >= 0)
                    MessageBox.Show(qtdReplaced + " occurrence replaced.");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        //visual or resource
        protected void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pnlEdit.Visible)
            {
                this.CancelShowMore(this.editToolStripMenuItem, this.pnlEdit);
                this.opaquePanel.Visible = false;
            }

            //if it's selected something different from txtFind
            bool selectedSomethingDifferent = this.rchtxtCode.SelectionLength > 0 &&
                this.rchtxtCode.SelectedText != this.txtFind.Text;
            if (this.pnlSearch.Visible && this.isFind && !selectedSomethingDifferent)
                this.btnCloseFindReplace.PerformClick();
            else
            {
                this.isFind = true;

                this.pnlSearch.Height = this.btnSeeReplace.Location.Y + this.btnSeeReplace.Height;
                this.btnSeeReplace.Visible = true;

                this.pnlSearch.BringToFront();
                this.pnlSearch.Visible = true;

                if (selectedSomethingDifferent)
                    this.txtFind.Text = this.rchtxtCode.SelectedText;

                this.txtFind.Focus();
            }
        }

        protected void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pnlEdit.Visible)
            {
                this.CancelShowMore(this.editToolStripMenuItem, this.pnlEdit);
                this.opaquePanel.Visible = false;
            }

            bool selectedSomethingDifferent = this.rchtxtCode.SelectionLength > 0 &&
                this.rchtxtCode.SelectedText != this.txtFind.Text;
            if (this.pnlSearch.Visible && !this.isFind && !selectedSomethingDifferent)
                this.btnCloseFindReplace.PerformClick();
            else
            {
                this.isFind = false;

                this.pnlSearch.Height = this.btnNotSeeReplace.Location.Y + this.btnNotSeeReplace.Height;
                this.btnSeeReplace.Visible = false;

                this.pnlSearch.BringToFront();
                this.pnlSearch.Visible = true;

                if (selectedSomethingDifferent)
                    this.txtFind.Text = this.rchtxtCode.SelectedText;

                if (String.IsNullOrEmpty(this.txtFind.Text))
                    this.txtFind.Focus();
                else
                    this.txtReplace.Focus();
            }
        }

        protected void btnCloseFindReplace_Click(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.ConsiderNoSelectionBeforeWithSelection();
            this.sqlRchtxtbx.CancelSearch();
            this.pnlSearch.Visible = false;
        }

        protected void btnSeeReplace_Click(object sender, EventArgs e)
        {
            this.replaceToolStripMenuItem_Click(null, null);
        }

        protected void btnNotSeeReplace_Click(object sender, EventArgs e)
        {
            this.findToolStripMenuItem_Click(null, null);
        }

        protected void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnNext.PerformClick();
                e.Handled = true;
            }
            else
                this.FrmDillenSQLManagementStudio_KeyDown(sender, ref e);

            this.preventCtrlH = e.Control && e.KeyCode == Keys.H;
        }

        protected bool preventCtrlH = false;
        protected void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.preventCtrlH)
                e.Handled = true;
        }

        protected void txtReplace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnReplaceCurr.PerformClick();
                e.Handled = true;
            }
            else
                this.FrmDillenSQLManagementStudio_KeyDown(sender, ref e);
        }

        protected void txtFind_TextChanged(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.ConsiderNoSelectionBeforeWithSelection();
        }


        //Unicamp VPN configuration
        protected void tryToConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pnlVPNConfiguration.Visible)
            {
                this.CancelShowMore(this.vpnConfigurationToolStripMenuItem, this.pnlVPNConfiguration);
                this.opaquePanel.Visible = false;
            }

            if (this.user != null)
                MessageBox.Show("You are already connected wih Unicamp's VPN...");
            else
            {
                this.TryConnWithMyDtbs(false);
                if (this.user == null)
                    MessageBox.Show("Couldn't connected with Unicamp's VPN!");
            }
        }

        protected void stopOrBeginTryingToConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pnlVPNConfiguration.Visible)
            {
                this.CancelShowMore(this.vpnConfigurationToolStripMenuItem, this.pnlVPNConfiguration);
                this.opaquePanel.Visible = false;
            }

            this.tmrCheckVPNConn.Enabled = !this.tmrCheckVPNConn.Enabled;

            this.PutVPNConfigurationTexts();
        }

        protected void PutVPNConfigurationTexts()
        {
            if (this.tmrCheckVPNConn.Enabled)
                this.stopOrBeginTryingToConnectToolStripMenuItem.Text = "Stop trying to connect";
            else
                this.stopOrBeginTryingToConnectToolStripMenuItem.Text = "Start trying to connect";
        }


        //new Rich Text Box procedures
        protected void newRchtxtCode_TextChanged(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.rchtxtCode_TextChanged(sender, e);
            if (this.isSaved && !this.rchtxtCode.ReadOnly)
            {
                this.isSaved = false;
                this.ChangeTitle();
            }
        }

        protected void newRchtxtCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //see if user wants to save/saveAs/open file or execute
            this.FrmDillenSQLManagementStudio_PreviewKeyDown(null, e);
            this.sqlRchtxtbx.rchtxtCode_PreviewKeyDown(sender, e);
        }

        protected void newRchtxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.showedDialog)
                e.Handled = true;
        }


        //richtextbox Size
        protected void smallerRchtxtFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.smallerRchtxtFontToolStripMenuItem.Enabled = this.sqlRchtxtbx.SetRchtxtCodeSmallerFont();
            this.largerRchtxtFontToolStripMenuItem.Enabled = true;
        }

        protected void largerRchtxtFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.largerRchtxtFontToolStripMenuItem.Enabled = this.sqlRchtxtbx.SetRchtxtCodeLargerFont();
            this.smallerRchtxtFontToolStripMenuItem.Enabled = true;
        }


        //undo and redo
        protected void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.Undo();
        }

        protected void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.Redo();
        }


        //DataGridView resources
        protected void grvSelect_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if user doubleClicked on a table name from a sp_help TABLE (including btnAllTables)
            if (e.ColumnIndex == 2 && e.RowIndex >= 0 && String.IsNullOrEmpty((string)this.grvSelect.TopLeftHeaderCell.Value) && this.grvSelect.Columns[e.ColumnIndex].HeaderText == "TABLE_NAME")
            {
                try
                {
                    //get table name
                    string tableName = (string)this.grvSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string code = "select * from " + tableName;

                    //execute
                    DataTable dataTable = null;
                    string exception = null;
                    int qtdLinesChanged = 0;
                    this.mySqlCon.ExecuteOneSQLCmd(code, true, ref dataTable, ref exception, ref qtdLinesChanged);

                    //put in DataGridView
                    SqlExecuteProcedures.ChangeExecuteResultLabel(ref this.lbExecutionResult, true, 0);
                    this.grvSelect.TopLeftHeaderCell.Value = tableName;
                    //this.lbTableName.Text = tableName;
                    //this.lbTableName.Visible = true;
                    this.grvSelect.DataSource = dataTable;
                }
                catch (Exception err)
                { }
            }
        }


        //btnClose and btnMinimize mouse events
        protected void btnClose_MouseEnter(object sender, EventArgs e)
        {
            this.btnClose.Image = global::DillenManagementStudio.Properties.Resources.btnClose_Hover1;
        }

        protected void btnClose_MouseLeave(object sender, EventArgs e)
        {
            this.btnClose.Image = global::DillenManagementStudio.Properties.Resources.btnClose__1_;
        }

        protected void btnMinimize_MouseEnter(object sender, EventArgs e)
        {
            this.btnMinimize.Image = global::DillenManagementStudio.Properties.Resources.btnMinimize_Hover;
        }

        protected void btnMinimize_MouseLeave(object sender, EventArgs e)
        {
            this.btnMinimize.Image = global::DillenManagementStudio.Properties.Resources.btnMinimize;
        }


        //menu
        protected void showMoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int tag = Convert.ToInt32((btn).Tag);

            switch (tag)
            {
                case 1:
                    this.CancelShowMore(this.editToolStripMenuItem, this.pnlEdit);
                    this.CancelShowMore(this.executeAsToolStripMenuItem, this.pnlExecuteAs);
                    this.CancelShowMore(this.vpnConfigurationToolStripMenuItem, this.pnlVPNConfiguration);
                    this.AuxShowMoreToolStrip(btn, this.pnlFile);
                    break;
                case 2:
                    this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
                    this.CancelShowMore(this.executeAsToolStripMenuItem, this.pnlExecuteAs);
                    this.CancelShowMore(this.vpnConfigurationToolStripMenuItem, this.pnlVPNConfiguration);
                    this.AuxShowMoreToolStrip(btn, this.pnlEdit);
                    break;
                case 3:
                    this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
                    this.CancelShowMore(this.editToolStripMenuItem, this.pnlEdit);
                    this.CancelShowMore(this.vpnConfigurationToolStripMenuItem, this.pnlVPNConfiguration);
                    this.AuxShowMoreToolStrip(btn, this.pnlExecuteAs);
                    break;
                case 4:
                    this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
                    this.CancelShowMore(this.editToolStripMenuItem, this.pnlEdit);
                    this.CancelShowMore(this.executeAsToolStripMenuItem, this.pnlExecuteAs);
                    this.AuxShowMoreToolStrip(btn, this.pnlVPNConfiguration);
                    break;
            }
        }

        protected void AuxShowMoreToolStrip(Button btn, Panel pnl)
        {
            pnl.Visible = !pnl.Visible;
            if (pnl.Visible)
            {
                pnl.BringToFront();
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
                this.opaquePanel.Visible = true;
            }
            else
            {
                btn.BackColor = this.pnlMenu.BackColor;
                btn.ForeColor = this.pnlMenu.ForeColor;
                this.opaquePanel.Visible = false;
            }
        }

        protected void CancelShowMore(Button btn, Panel pnl)
        {
            pnl.Visible = false;
            btn.BackColor = this.pnlMenu.BackColor;
            btn.ForeColor = this.pnlMenu.ForeColor;
        }

        protected void moreToShowToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int tag = Convert.ToInt32((btn).Tag);

            Panel pnl = null;

            switch (tag)
            {
                case 1:
                    pnl = this.pnlFile;
                    break;
                case 2:
                    pnl = this.pnlEdit;
                    break;
                case 3:
                    pnl = this.pnlExecuteAs;
                    break;
                case 4:
                    pnl = this.pnlVPNConfiguration;
                    break;
            }

            if (!pnl.Visible)
            {
                btn.BackColor = Color.FromArgb(55, 64, 70);
                this.notShowMoreToolStripMenuItem_MouseEnter(sender, e);
            }
        }

        protected void moreToShowToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int tag = Convert.ToInt32((btn).Tag);

            Panel pnl = null;

            switch (tag)
            {
                case 1:
                    pnl = this.pnlFile;
                    break;
                case 2:
                    pnl = this.pnlEdit;
                    break;
                case 3:
                    pnl = this.pnlExecuteAs;
                    break;
                case 4:
                    pnl = this.pnlVPNConfiguration;
                    break;
            }

            if (!pnl.Visible)
                btn.BackColor = this.pnlMenu.BackColor;
        }

        protected void notShowMoreToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.NotShowMorePanelsMenu();
        }

        protected void opaquePanel_Click(object sender, EventArgs e)
        {
            this.NotShowMorePanelsMenu();
        }

        protected void NotShowMorePanelsMenu()
        {
            this.CancelShowMore(this.fileToolStripMenuItem, this.pnlFile);
            this.CancelShowMore(this.editToolStripMenuItem, this.pnlEdit);
            this.CancelShowMore(this.executeAsToolStripMenuItem, this.pnlExecuteAs);
            this.CancelShowMore(this.vpnConfigurationToolStripMenuItem, this.pnlVPNConfiguration);
            this.opaquePanel.Visible = false;
        }
        
    }
}