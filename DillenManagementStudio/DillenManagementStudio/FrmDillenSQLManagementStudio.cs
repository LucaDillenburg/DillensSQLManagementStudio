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

        //general
        protected const string TITLE = "Dillen's SQL Management Studio (Dillenburg's Product)";

        //file
        protected string fileName;
        protected bool isSaved = false;

        //notification
        protected bool allowNotification = true;

        //user
        protected User user;

        //FrmChangeDatabase (using VPN Connection)
        protected FrmChangeDatabase frmChangeDatabase;


        //form iniciate and finish
        public FrmDillenSQLManagementStudio()
        {
            InitializeComponent();

            //ajust form
            this.AjustForm();

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
            //RichTextBox Code
            this.rchtxtCode.Width = (int)Math.Round(0.548 * this.Width); //54.8%
            this.rchtxtCode.Height = (int)Math.Round(0.915 * (this.Height - this.menuStrip.Height)); //91.5%
            this.rchtxtAux.Width = this.rchtxtCode.Width;
            this.rchtxtAux.Height = this.rchtxtCode.Height;
            this.pnlLoading.Width = (int)Math.Round(0.24 * this.Width); //24%
            this.pnlLoading.Height = (int)Math.Round(0.183 * (this.Height - this.menuStrip.Height)); //16.3%
            //DataGridView Select
            this.grvSelect.Width = (int)Math.Round(0.423 * this.Width); //42.3%
            this.grvSelect.Height = (int)Math.Round(0.92 * (this.Height - this.menuStrip.Height)); //92%


            ///LOCATION
            //RichTextBox Code
            int x = (int)Math.Round(0.0088 * (this.Width)); //0.88%
            int y = (int)Math.Round(0.122 * (this.Height - this.menuStrip.Height)); //12.8%
            this.rchtxtCode.Location = new Point(x, y);
            this.rchtxtAux.Location = this.rchtxtCode.Location;
            //DataGridView Select
            x = (int)Math.Round(0.571 * (this.Width)); //57.1%
            y = (int)Math.Round(0.158 * (this.Height - this.menuStrip.Height)); //15.8%
            this.grvSelect.Location = new Point(x, y);
            this.lbExecutionResult.Location = new Point(this.grvSelect.Location.X + 2, 
                this.grvSelect.Location.Y - this.lbExecutionResult.Height - 4);
            this.btnAllTables.Location = new Point(this.grvSelect.Location.X + this.grvSelect.Width - this.btnAllTables.Width - 2,
                this.lbExecutionResult.Location.Y - 4);
            this.btnAllProcFunc.Location = new Point(this.btnAllTables.Location.X - this.btnAllProcFunc.Width - 10,
                this.btnAllTables.Location.Y);
            // Close and Minimize
            int distanceBetweenBtns = 12;
            this.btnClose.Location = new Point(this.Width - this.btnClose.Width - distanceBetweenBtns, this.btnClose.Location.Y);
            this.btnMinimize.Location = new Point(this.btnClose.Location.X - this.btnMinimize.Width - distanceBetweenBtns, this.btnClose.Location.Y);
            // Label Database
            x = (int)Math.Round(0.019 * (this.Width)); //1.9%
            y = (int)Math.Round(1.054 * (this.Height - this.menuStrip.Height)); //105.4%
            this.lbDatabase.Location = new Point(x, y);
            this.pnlSearch.Location = new Point(this.rchtxtCode.Location.X + this.rchtxtCode.Width + 2,
                this.pnlSearch.Location.Y);


            //MessageBox.Show("Width: " + (((float)this.lbDatabase.Location.X) / ((float)this.Width))*100 + "%" ); //1.9%
            //MessageBox.Show("Height: " + (((float)this.lbDatabase.Location.Y) / ((float)this.Height - this.menuStrip.Height))*100 + "%"); //105.4%
        }

        protected void FrmDillenSQLManagementStudio_Load(object sender, EventArgs e)
        {
            this.TryConnWithMyDtbs(true);

            //mySQLConnection
            this.mySqlCon = new MySqlConnection(this.user);

            //INICIALIZE RICHTEXT BOX
            this.sqlRchtxtbx = new SqlRichTextBox(ref this.rchtxtCode, this, this.mySqlCon, false);
            this.sqlRchtxtbx.SetNewEvents(new System.EventHandler(this.newRchtxtCode_TextChanged),
                new System.Windows.Forms.PreviewKeyDownEventHandler(this.newRchtxtCode_PreviewKeyDown));

            this.tmrCheckVPNConn.Enabled = true;
            this.ShowChangeDatabaseForm();
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
            if (e.KeyCode == Keys.F5) //F5
                this.executeToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.S && e.Control) //ctrl+S
            {
                if (e.Shift)
                    this.saveAsToolStripMenuItem.PerformClick();
                else
                    this.saveToolStripMenuItem.PerformClick();
            }
            else
            if (e.KeyCode == Keys.F && e.Control) //ctrl+F
                this.findToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.H && e.Control) //ctrl+H
                this.replaceToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.N && e.Control) //ctrl+N
                this.newToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.O && e.Control) //ctrl+O
                this.openToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.F4 && e.Alt) //alt+F4
                this.closeToolStripMenuItem.PerformClick();
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
                    this.saveAsToolStripMenuItem.PerformClick();
                else
                    this.saveToolStripMenuItem.PerformClick();
            }
            else
            if (e.KeyCode == Keys.F && e.Control) //ctrl+F
                this.findToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.H && e.Control) //ctrl+H
                this.replaceToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.N && e.Control) //ctrl+N
                this.newToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.O && e.Control) //ctrl+O
                this.openToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.F4 && e.Alt) //alt+F4
                this.closeToolStripMenuItem.PerformClick();
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
            if (this.AskUserWantsToSaveIfNeeded())
                this.Close();
        }
        

        //VPN Connection
        protected void tmrCheckVPNConn_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(this.CheckVPNConn)).Start();
        }

        protected void CheckVPNConn()
        {
            this.Invoke((MethodInvoker)delegate {
                //if was connected
                if (this.user != null)
                {
                    //if user was disconnected
                    if (!this.user.IsConnected())
                    {
                        this.user = null;
                        this.allCommandsSintaxToolStripMenuItem.Enabled = false;

                        if (this.frmChangeDatabase != null)
                            this.frmChangeDatabase.User = null;

                        this.ShowMessageRightPlace("You were disconnected to the Unicamp VPN! If you want to learn more about commands or have your databases saved, connect it again!");
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
                this.user = new User();
            }
            catch (Exception err)
            {
                if (firstTime)
                {
                    //MessageBox.Show("Be sure you are connected with Unicamp VPN! Connect and restart the program!");
                    //this.Close();
                    //return;
                    MessageBox.Show("You are not connected with Unicamp VPN... If you want to learn more about commands or have your databases saved, connect it!");
                }
            }

            this.Invoke((MethodInvoker)delegate {
                this.allCommandsSintaxToolStripMenuItem.Enabled = this.user != null;
                if (this.user != null)
                {
                    this.user.InicializeUser();

                    if (!firstTime)
                    {
                        if (this.frmChangeDatabase != null)
                            this.frmChangeDatabase.User = this.user;

                        this.ShowMessageRightPlace("Now you are connected with Unicamp VPN! You can learn more about commands or have your databases saved!");
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
            //choose file
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
            if (!this.isSaved)
            {
                if (String.IsNullOrEmpty(this.fileName))
                    this.saveAsToolStripMenuItem.PerformClick();
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
                this.saveToolStripMenuItem.PerformClick();
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
        }


        //EXECUTE
        protected void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.automaticToolStripMenuItem.PerformClick();
        }

        protected void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.connected && this.lastExecution != 0)
                this.mySqlCon.RestartCommands();

            this.Execute(0);

            this.lastExecution = 0;
        }

        protected void executeNonQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Execute(1);
            this.lastExecution = 1;
        }

        protected void queryToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                            }catch(Exception e)
                            {
                                this.User = null;
                                this.allCommandsSintaxToolStripMenuItem.Enabled = false;
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
            this.ShowChangeDatabaseForm();
        }

        protected void ShowChangeDatabaseForm()
        {
            this.frmChangeDatabase = new FrmChangeDatabase(this, this.conStrs);
            string oldDatabase = this.mySqlCon.ConnStr;
            this.frmChangeDatabase.FormClosed += (s, arg) => this.ProceduresAfterNewDatabase(oldDatabase);
            this.frmChangeDatabase.ShowDialog();
        }

        public void ChangeDatabaseName(string databaseName)
        {
            if(databaseName == null)
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

        protected void ProceduresAfterNewDatabase(string oldDatabase)
        {
            this.conStrs = this.frmChangeDatabase.ConnectionStrings;

            string newDatabase = this.mySqlCon.ConnStr;
            //if connected with a database different from the old one
            if(!String.IsNullOrEmpty(newDatabase) && newDatabase != oldDatabase)
                new Thread(() => this.AuxBtnAllTablesClick()).Start();

            //set Focus in RichTextBox
            Force.Focus(this.sqlRchtxtbx.SQLRichTextBox);

            //only lets the user execute if he has connected to a database
            //this.EnableWichDependsCon(!String.IsNullOrEmpty(this.mySqlCon.ConnStr));
            //ITS ON CHANGEDATABASE NOW
        }

        protected void AuxBtnAllTablesClick()
        {
            try
            {
                //while(!this.btnAllTables.CanFocus)
                //{}
                while(true)
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
                    catch(Exception e)
                    {}
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


        //allow notification
        protected void allowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.allowNotification)
                this.allowToolStripMenuItem.Text = "Allow";
            else
                this.allowToolStripMenuItem.Text = "Not Allow";

            this.allowNotification = !this.allowNotification;
        }


        //SEARCH: FIND and REPLACE
        protected bool isFind = true;
        protected void btnNext_Click(object sender, EventArgs e)
        {
            StringComparison stringComparison = (this.chxIgnoreCase.Checked ? StringComparison.InvariantCultureIgnoreCase : StringComparison.CurrentCulture);

            try
            {
                this.sqlRchtxtbx.Find(this.txtFind.Text, stringComparison);
            }catch(Exception err)
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
            this.pnlSearch.Visible = !this.sqlRchtxtbx.ReplaceAll(this.txtFind.Text, this.txtReplace.Text, stringComparison);
        }
        

        //visual or resource
        protected void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

                this.pnlSearch.Visible = true;

                if (selectedSomethingDifferent)
                    this.txtFind.Text = this.rchtxtCode.SelectedText;

                Force.Focus(this.txtFind);
            }
        }

        protected void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool selectedSomethingDifferent = this.rchtxtCode.SelectionLength > 0 &&
                this.rchtxtCode.SelectedText != this.txtFind.Text;
            if (this.pnlSearch.Visible && !this.isFind && !selectedSomethingDifferent)
                this.btnCloseFindReplace.PerformClick();
            else
            {
                this.isFind = false;

                this.pnlSearch.Height = this.btnNotSeeReplace.Location.Y + this.btnNotSeeReplace.Height;
                this.btnSeeReplace.Visible = false;
                
                this.pnlSearch.Visible = true;

                if (selectedSomethingDifferent)
                    this.txtFind.Text = this.rchtxtCode.SelectedText;

                if (String.IsNullOrEmpty(this.txtFind.Text))
                    Force.Focus(this.txtFind);
                else
                    Force.Focus(this.txtReplace);
            }
        }

        private void btnCloseFindReplace_Click(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.ConsiderNoSelectionBeforeWithSelection();
            this.sqlRchtxtbx.ChangeBackColorFromLastSearch();
            this.pnlSearch.Visible = false;
        }
        
        protected void btnSeeReplace_Click(object sender, EventArgs e)
        {
            this.replaceToolStripMenuItem.PerformClick();
        }

        protected void btnNotSeeReplace_Click(object sender, EventArgs e)
        {
            this.findToolStripMenuItem.PerformClick();
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


        //new Rich Text Box procedures
        protected void newRchtxtCode_TextChanged(object sender, EventArgs e)
        {
            this.sqlRchtxtbx.rchtxtCode_TextChanged();
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
                }catch(Exception err)
                { }
            }
        }

    }
}