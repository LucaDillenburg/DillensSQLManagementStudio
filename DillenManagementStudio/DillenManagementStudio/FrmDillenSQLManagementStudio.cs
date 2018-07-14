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
        
        //form methods
        public FrmDillenSQLManagementStudio()
        {
            InitializeComponent();

            //set SQL buttons
            this.EnableWichDependsCon(false);
        }

        protected void FrmDillenSQLManagementStudio_Load(object sender, EventArgs e)
        {
            //user
            try
            {
                this.user = new User();
            }
            catch (Exception err)
            {
                MessageBox.Show("Be sure you are connected with the Unicamp VPN! Connect and restart the program!");
                this.Close();
                return;
            }
            
            this.user.InicializeUser();

            //mySQLConnection
            this.mySqlCon = new MySqlConnection(this.user);

            //INICIALIZE RICHTEXT BOX
            this.sqlRchtxtbx = new SqlRichTextBox(ref this.rchtxtCode, this, this.mySqlCon, false);
            this.sqlRchtxtbx.SetNewEvents(new System.EventHandler(this.newRchtxtCode_TextChanged),
                new System.Windows.Forms.PreviewKeyDownEventHandler(this.newRchtxtCode_PreviewKeyDown));

            this.ShowChangeDatabaseForm(true);
        }

        protected void FrmDillenSQLManagementStudio_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                this.executeToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.S)
            {
                if (e.Control)
                {
                    if (e.Shift)
                        this.saveAsToolStripMenuItem.PerformClick();
                    else
                        this.saveToolStripMenuItem.PerformClick();
                }
            }
            else
            if (e.KeyCode == Keys.N && e.Control)
                this.newToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.O && e.Control)
                this.openToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.F4 && e.Alt)
                this.closeToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.Oemplus && e.Control && this.largerRchtxtFontToolStripMenuItem.Enabled)
                this.largerRchtxtFontToolStripMenuItem.PerformClick();
            else
            if (e.KeyCode == Keys.OemMinus && e.Control && this.smallerRchtxtFontToolStripMenuItem.Enabled)
                this.smallerRchtxtFontToolStripMenuItem.PerformClick();
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

        protected void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.mySqlCon.CloseConnection();
            }
            catch (Exception err)
            { }
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

        public void ChangeTitle()
        {
            if (String.IsNullOrEmpty(this.fileName))
                this.lbTitle.Text = TITLE;
            else
                this.lbTitle.Text = this.fileName + (this.isSaved?"":"*") + " - " + TITLE;
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
            Queue<int> linesNoEvenQuotMarks = new Queue<int>();
            //put txtCode.Items in a String (with spaces between each line)
            string allCodes = "";
            
            RichTextBox rchtxtCode = this.sqlRchtxtbx.SQLRichTextBox;
            //or this.rchtxtCode

            //if there's nothing selected
            if (rchtxtCode.SelectionLength <= 0)
                for (int i = 0; i < rchtxtCode.Lines.Length; i++)
                {
                    //if there're even
                    if (rchtxtCode.Lines[i].CountAppearances('\'') % 2 != 0)
                        linesNoEvenQuotMarks.Enqueue(i);
                    else
                        allCodes += " " + rchtxtCode.Lines[i];
                }
            else
            //there's something selected
            {
                int qtdOtherChars1 = 0;
                int indexFirstLine = this.sqlRchtxtbx.IndexOfLine(rchtxtCode.SelectionStart, ref qtdOtherChars1);
                int qtdOtherChars2 = 0;
                int indexLastLine = this.sqlRchtxtbx.IndexOfLine(rchtxtCode.SelectionStart + rchtxtCode.SelectionLength, 
                    ref qtdOtherChars2);

                for(int i = indexFirstLine; i <= indexLastLine; i++)
                {
                    string line;
                    if (i == indexFirstLine)
                    {
                        int final;
                        if (indexFirstLine == indexLastLine)
                            final = rchtxtCode.SelectionStart + rchtxtCode.SelectionLength - qtdOtherChars1;
                        else
                            final = rchtxtCode.Lines[i].Length;

                        int start = rchtxtCode.SelectionStart - qtdOtherChars1;

                        line = rchtxtCode.Lines[i].Substring(start, final - start);
                    }
                    else
                    if (i == indexLastLine)
                        line = rchtxtCode.Lines[i].Substring(0, rchtxtCode.SelectionStart + rchtxtCode.SelectionLength - qtdOtherChars2);
                    else
                    if (i < indexLastLine)
                        line = rchtxtCode.Lines[i];
                    else
                        break;

                    //if there're even
                    if (line.CountAppearances('\'') % 2 != 0)
                        linesNoEvenQuotMarks.Enqueue(i);
                    else
                        allCodes += " " + line;
                }
            }

            if (linesNoEvenQuotMarks.Count > 0)
            {
                string msg = "Error! Add closing single quotation marks in lines: ";

                while (linesNoEvenQuotMarks.Count > 0)
                    msg += linesNoEvenQuotMarks.Dequeue() + (linesNoEvenQuotMarks.Count == 0 ? "!" : ", ");

                MessageBox.Show(msg);
            }
            else
            if (executeType == 0)
            {
                Queue<Error> errors = new Queue<Error>();
                this.grvSelect.DataSource = this.mySqlCon.ExecuteAutomaticSqlCommands(allCodes, ref errors);

                if (errors == null || errors.Count == 0)
                {
                    this.lbExecutionResult.Text = "Succesfully executed!";
                    this.lbExecutionResult.ForeColor = Color.Green;

                    if (!this.lbExecutionResult.Visible)
                        this.lbExecutionResult.Visible = true;

                    if (this.allowNotification)
                        MessageBox.Show("Succesfully executed!");
                }
                else
                {
                    this.lbExecutionResult.Text = "Unsuccesfully executed!";
                    this.lbExecutionResult.ForeColor = Color.Red;

                    if (!this.lbExecutionResult.Visible)
                        this.lbExecutionResult.Visible = true;

                    if (this.allowNotification)
                        this.ShowErrors(errors);
                }
            }
            else
            {
                //execute one Query or Non-Query (based on the radiobutton checked)
                DataTable dataTable = null;
                bool worked = true;
                string excep = null;
                worked = this.mySqlCon.ExecuteOneSQLCmd(allCodes, executeType == 2, ref dataTable, ref excep);

                this.grvSelect.DataSource = dataTable;

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
                    MessageBox.Show(currErr.Exception, "SQL Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.EnableWichDependsCon(false);
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
                            new FrmCommandExplanation(currErr.CodCommand, this.user, this.mySqlCon).Show();
                    }
                }
            }
        }


        //sql execute procedures
        protected void btnAllTables_Click(object sender, EventArgs e)
        {
            this.grvSelect.DataSource = this.mySqlCon.AllTables();
        }

        protected void btnAllProcFunc_Click(object sender, EventArgs e)
        {
            this.grvSelect.DataSource = this.mySqlCon.AllProcFunc();
        }
        

        //CHANGE DATA BASE BUTTON
        //GETTERS AND SETTERS
        public User User
        {
            get
            {
                return this.user;
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
        protected void changeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowChangeDatabaseForm(false);
        }

        protected void ShowChangeDatabaseForm(bool fisrtTime)
        {
            FrmChangeDatabase frmChangeDatabase = new FrmChangeDatabase(this);
            frmChangeDatabase.FormClosed += (s, arg) => this.ProceduresAfterNewDatabase(fisrtTime);
            frmChangeDatabase.ShowDialog();
            
            //set Focus in RichTextBox
            this.sqlRchtxtbx.ForceFocus();
        }

        public void ChangeDatabaseName(string databaseName)
        {
            this.lbDatabase.Text = databaseName;
        }

        protected void ProceduresAfterNewDatabase(bool firstTime)
        {
            //if user didn't connected to any database and closed the form
            if(firstTime && String.IsNullOrEmpty(this.mySqlCon.ConnStr))
            {
                this.Close();
                return;
            }
            
            this.EnableWichDependsCon(!String.IsNullOrEmpty(this.mySqlCon.ConnStr));
        }


        //others
        protected void allowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.allowNotification)
                this.allowToolStripMenuItem.Text = "Allow";
            else
                this.allowToolStripMenuItem.Text = "Not Allow";

            this.allowNotification = !this.allowNotification;
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

    }
}

//classes:
// 1. MyString (not static, extends String + metodos)
// 2. SqlCommands (not static, extends SqlCommands)
    //todos os metodos com inicial minuscula