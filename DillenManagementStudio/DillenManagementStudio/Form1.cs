using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
//to work with the database: MySqlCommand

namespace DillenManagementStudio
{
    public partial class FrmDillenSQLManagementStudio : Form
    {
        //teste
        protected const bool teste = true;


        //conection
        protected string connStr;
        protected MySqlConnection mySqlCon = new MySqlConnection();
        protected bool connected = false;

        //commands
        protected List<string> commands;
        protected List<string> reservedWords;

        //execution
        protected int lastExecution = 0;

        //richTextBox color
        protected List<char> specialChars;
        protected int iSingQuot;
        //richTextBox change
        protected bool notChangeTxtbxCode = false;
        protected int lastQtdSingQuot = 0;
        protected bool erased = false;
        //richTextBox color
        protected string lastText = "";
        protected string[] lastLines = new string[0];
        //larger or smaller font
        protected const int MIN_RCHTXT_ZOOM = 1;
        protected const int MAX_RCHTXT_ZOOM = 5;
        protected float rchtxtZoomFactor; //inicialized in FrmDillenSQLManagementStudio()

        //general
        protected const string TITLE = "Dillen's SQL Management Studio (Dillenburg's Product)";

        //file
        protected string fileName;
        protected bool isSaved = false;

        //notification
        protected bool allowNotification = true;

        //has't typed anything
        protected bool hasTyped = false;
        
        //user
        protected string MAC_ADRESS = Computer.MacAdress;
        protected User user;
        
        //form methods
        public FrmDillenSQLManagementStudio()
        {
            InitializeComponent();

            MessageBox.Show(this.MAC_ADRESS);

            //set SQL buttons
            this.EnableWichDependsCon(false);

            //zoom
            this.rchtxtZoomFactor = this.rchtxtCode.ZoomFactor;

            //reserved words
            reservedWords = mySqlCon.ReservedWords;

            //special chars
            specialChars = mySqlCon.SpecialChars;
            iSingQuot = mySqlCon.IndexSingQuot;
        }

        protected void Form1_Load(object sender, EventArgs e)
        {
            //set RichTextBox
            this.rchtxtCode.Focus();
            this.rchtxtCode.SelectionStart = 0;
            this.rchtxtCode.SelectionLength = this.rchtxtCode.Text.Length;

            bool closed = false;
            //user
            try
            {
                this.user = new User(this.MAC_ADRESS);
            }
            catch (Exception err)
            {
                MessageBox.Show("Be sure you are connected with the Unicamp VPN! Connect and restart the program!");
                closed = true;
                this.Close();
            }

            if(!closed)
            {
                this.user.InicializeUser();

                this.Show();
                this.ShowChangeDatabaseForm(true);
            }
        }

        protected void EnableWichDependsCon(bool enable)
        {
            this.btnAllTables.Enabled = enable;
            this.executeAsToolStripMenuItem.Enabled = enable;
            this.btnAllProcFunc.Enabled = enable;
            this.executeToolStripMenuItem.Enabled = enable;
            this.connected = enable;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.AskUserWantsToSaveIfNeeded())
            {
                //choose file
                DialogResult result = this.saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.fileName = this.saveFileDialog.FileName;

                    this.rchtxtCode.Text = "";
                    this.isSaved = true;
                    this.ChangeTitle();
                    //creates and writes file
                    this.SaveFile();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sees if wants to save if it's not saved yet
            if (!this.AskUserWantsToSaveIfNeeded())
                return;

            //show dialog
            DialogResult result = this.openFileDialog.ShowDialog();

            //if user selected a file
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //visual
                this.rchtxtCode.Visible = false;
                this.picLoading.Visible = true;
                this.lbLoading.Visible = true;
                this.rchtxtCode.ReadOnly = true;

                this.fileName = this.openFileDialog.FileName;

                StreamReader reader = new StreamReader(this.fileName);

                //Clear
                this.rchtxtCode.Lines = new string[0];

                //changes title
                this.isSaved = true;
                this.ChangeTitle();

                //write all text on RichTextBox and Colors it
                this.notChangeTxtbxCode = true;
                this.rchtxtCode.Text = File.ReadAllText(this.fileName, Encoding.UTF8);
                this.PutAllRchTxtRealColorAlsoString();
                this.notChangeTxtbxCode = false;

                /* ARRUMAR
                int qtdCharsOtherLines = 0;
                for (; ;)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;

                    this.rchtxtCode.Text += System.Environment.NewLine + line;
                    this.PutWordRealColorAlsoString (line, 0, 0, qtdCharsOtherLines);
                    qtdCharsOtherLines += line.Length;
                }*/

                //close the file
                reader.Close();

                //visual 
                this.picLoading.Visible = false;
                this.lbLoading.Visible = false;
                this.rchtxtCode.Visible = true;
                this.rchtxtCode.ReadOnly = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
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
            this.rchtxtCode.SaveFile(this.fileName, RichTextBoxStreamType.UnicodePlainText);

            /*
            //clear and write fisrt line
            StreamWriter writer = new StreamWriter(this.fileName);
            if(this.rchtxtCode.Lines.Length > 0)
                writer.WriteLine(this.rchtxtCode.Lines[0]);
            else
                writer.WriteLine("");
            writer.Close();

            //write other lines
            writer = new StreamWriter(this.fileName, true);
            for(int i = 1; i< this.rchtxtCode.Lines.Length; i++)
                writer.WriteLine(this.rchtxtCode.Lines[i]); //use \n\r if necessary
            writer.Close();*/
        }

        protected bool AskUserWantsToSaveIfNeeded()
        {
            if (this.isSaved || !this.hasTyped)
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
                this.lbTitle.Text = this.fileName + (this.isSaved?"":"*") + " - " + TITLE;
        }


        //EXECUTE
        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.automaticToolStripMenuItem.PerformClick();
        }

        private void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.connected && this.lastExecution != 0)
                this.mySqlCon.RestartCommands();

            this.Execute(0);

            this.lastExecution = 0;
        }

        private void executeNonQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Execute(1);
            this.lastExecution = 1;
        }

        private void queryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Execute(2);
            this.lastExecution = 2;
        }
        
        protected void Execute(int executeType) //ExecuteType: 0=Automatic, 1=Non-Query, 2=Query
        {
            Queue<int> linesNoEvenQuotMarks = new Queue<int>();
            //put txtCode.Items in a String (with spaces between each line)
            string allCodes = "";

            //if there's nothing selected
            if (this.rchtxtCode.SelectionLength <= 0)
                for (int i = 0; i < this.rchtxtCode.Lines.Length; i++)
                {
                    //if there're even
                    if (this.rchtxtCode.Lines[i].CountAppearances('\'') % 2 != 0)
                        linesNoEvenQuotMarks.Enqueue(i);
                    else
                        allCodes += " " + this.rchtxtCode.Lines[i];
                }
            else
            //there's something selected
            {
                int qtdOtherChars1 = 0;
                int indexFirstLine = this.IndexOfLine(this.rchtxtCode.SelectionStart, ref qtdOtherChars1);
                int qtdOtherChars2 = 0;
                int indexLastLine = this.IndexOfLine(this.rchtxtCode.SelectionStart + this.rchtxtCode.SelectionLength, 
                    ref qtdOtherChars2);

                for(int i = indexFirstLine; i <= indexLastLine; i++)
                {
                    string line;
                    if (i == indexFirstLine)
                    {
                        int final;
                        if (indexFirstLine == indexLastLine)
                            final = this.rchtxtCode.SelectionStart + this.rchtxtCode.SelectionLength - qtdOtherChars1;
                        else
                            final = this.rchtxtCode.Lines[i].Length;

                        int start = this.rchtxtCode.SelectionStart - qtdOtherChars1;

                        line = this.rchtxtCode.Lines[i].Substring(start, final - start);
                    }
                    else
                    if (i == indexLastLine)
                        line = this.rchtxtCode.Lines[i].Substring(0, this.rchtxtCode.SelectionStart + this.rchtxtCode.SelectionLength - qtdOtherChars2);
                    else
                    if (i < indexLastLine)
                        line = this.rchtxtCode.Lines[i];
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
                    {
                        while (errors.Count > 0)
                        {
                            //ask if the user wants to know how's the syntax of the command
                            Error currErr = errors.Dequeue();

                            if (currErr.IsConnectionException)
                            {
                                MessageBox.Show(currErr.Exception, "SQL Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.EnableWichDependsCon(false);
                                break;
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
                                    message += "\n\r\n\rDo you wanna know more about '" + this.commands[currErr.CodCommand].Trim().ToUpper()
                                        + "' sintax?";
                                    DialogResult result = MessageBox.Show(message, "SQL Error",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                                    if (result == DialogResult.Yes)
                                    {
                                        //fazer
                                        break;
                                    }
                                }
                            }
                        }
                    }
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


        //sql execute procedures
        protected void btnAllTables_Click(object sender, EventArgs e)
        {
            this.grvSelect.DataSource = this.mySqlCon.AllTables();
        }

        private void btnAllProcFunc_Click(object sender, EventArgs e)
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
        private void changeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowChangeDatabaseForm(false);
        }

        protected void ShowChangeDatabaseForm(bool fisrtTime)
        {
            FrmChangeDatabase frmChangeDatabase = new FrmChangeDatabase(this);
            frmChangeDatabase.FormClosed += (s, arg) => this.ProceduresAfterNewDatabase(fisrtTime);
            frmChangeDatabase.ShowDialog();
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
            
            this.commands = this.mySqlCon.Commands;
            this.EnableWichDependsCon(!String.IsNullOrEmpty(this.mySqlCon.ConnStr));
        }


        //others
        private void allowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.allowNotification)
                this.allowToolStripMenuItem.Text = "Allow";
            else
                this.allowToolStripMenuItem.Text = "Not Allow";

            this.allowNotification = !this.allowNotification;
        }


        //richtextbox Size
        private void smallerRchtxtFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rchtxtZoomFactor == 2 || this.rchtxtZoomFactor == 1.5)
                this.rchtxtZoomFactor = this.rchtxtZoomFactor - (float)0.5;
            else
                this.rchtxtZoomFactor--;

            this.rchtxtCode.ZoomFactor = this.rchtxtZoomFactor;

            if (this.rchtxtZoomFactor <= MIN_RCHTXT_ZOOM)
                this.smallerRchtxtFontToolStripMenuItem.Enabled = false;
            this.largerRchtxtFontToolStripMenuItem.Enabled = true;
        }

        private void largerRchtxtFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rchtxtZoomFactor == 1 || this.rchtxtZoomFactor == 1.5)
                this.rchtxtZoomFactor = this.rchtxtZoomFactor + (float)0.5;
            else
                this.rchtxtZoomFactor++;

            this.rchtxtCode.ZoomFactor = this.rchtxtZoomFactor;

            if (this.rchtxtZoomFactor >= MAX_RCHTXT_ZOOM)
                this.largerRchtxtFontToolStripMenuItem.Enabled = false;
            this.smallerRchtxtFontToolStripMenuItem.Enabled = true;
        }


        //richtext procedures
        protected void rchtxtCode_TextChanged(object sender, EventArgs e)
        {
            this.hasTyped = true;

            if (this.isSaved && !this.rchtxtCode.ReadOnly)
            {
                this.isSaved = false;
                this.ChangeTitle();
            }

            //if there's nothing written in the richTextBox, there's nothing to do
            if (this.rchtxtCode.Lines.Length == 0 || this.notChangeTxtbxCode)
            {
                //to control the addition of text
                this.lastLines = this.rchtxtCode.Lines;
                this.lastText = this.rchtxtCode.Text;
                this.lastQtdSingQuot = 0;
                return;
            }

            ///put red in strings, green in numbers, blue in reserved words and almost black in the rest

            int qtdCharsOtherLines = 0;
            int qtdNewChars = this.rchtxtCode.Text.Length - this.lastText.Length;
            //if something was pasted (more than one char)
            if(qtdNewChars > 1)
            {
                int indexDif = this.rchtxtCode.Text.IndexDiferent(this.lastText); //index added
                int firstLine = this.IndexOfLine(indexDif, ref qtdCharsOtherLines);
                int notUsing = -1;
                int lastLine = this.IndexOfLine(indexDif + qtdNewChars, ref notUsing);

                for(int i = firstLine; i <= lastLine; i++)
                {
                    string currLine = this.rchtxtCode.Lines[i];

                    int indexBegin;
                    int currCoutApp;
                    if(i==0)
                    {
                        indexBegin = indexDif;
                        currCoutApp = currLine.CountAppearances('\'', 0, indexDif);
                    }else
                    {
                        indexBegin = 0;
                        currCoutApp = 0;
                    }
                    
                    this.PutWordRealColorAlsoString(currLine, indexBegin, currCoutApp, qtdCharsOtherLines);
                    qtdCharsOtherLines += currLine.Length + 1;
                }

                this.lastLines = this.rchtxtCode.Lines;
                this.lastText = this.rchtxtCode.Text;

                return;
            }
            
            int indexLine = this.IndexOfLine(this.rchtxtCode.SelectionStart, ref qtdCharsOtherLines);
            string line = this.rchtxtCode.Lines[indexLine];

            int countApp = -1;
            bool erasedSingQuot = false;
            if (qtdNewChars < -1) //erased more than one char
            {
                //see if user deleted one or more single quotation marks
                /*int indexStartDeleting = this.rchtxtCode.Text.IndexDiferent(this.lastText); //index deleted

                int qtdCharsOtherStartL = -1;
                int lineStartedDeleting = this.IndexOfLine(this.lastLines, indexStartDeleting, ref qtdCharsOtherStartL);
                int qtdCharsOtherEndhL = -1;
                int lineFinishDeleting = this.IndexOfLine(this.lastLines, indexStartDeleting - qtdNewChars, ref qtdCharsOtherEndhL);*/

                int indexStartDeleting = this.rchtxtCode.SelectionStart;
                int qtdCharsOtherStartL = qtdCharsOtherLines;
                int lineStartedDeleting = indexLine;

                int qtdCharsOtherEndL = -1;
                int lineFinishDeleting = this.IndexOfLine(this.lastLines, indexStartDeleting - qtdNewChars, ref qtdCharsOtherEndL);

                if(lineStartedDeleting == lineFinishDeleting)
                {
                    int qtdSingQuotInDeleted = this.lastText.CountAppearances('\'', indexStartDeleting, indexStartDeleting - qtdNewChars);
                    erasedSingQuot = (qtdSingQuotInDeleted % 2 != 0) && (indexStartDeleting - qtdNewChars < this.lastLines[lineStartedDeleting].Length);
                    //if the deleted chars had even number of single quotation marks, there's nothing to change
                }else
                {
                    int countL1 = this.lastLines[lineStartedDeleting].CountAppearances('\'', 0, indexStartDeleting - qtdCharsOtherStartL);
                    bool evenL1 = countL1%2==0;
                    int countL2 = this.lastLines[lineFinishDeleting].CountAppearances('\'', 0, indexStartDeleting - qtdNewChars - qtdCharsOtherEndL - 1);
                    bool evenL2 = countL2 % 2 == 0;

                    if (evenL1 != evenL2)
                    {
                        erasedSingQuot = true;
                        countApp = countL1;
                    }
                }

                if(!erasedSingQuot)
                {
                    this.lastLines = this.rchtxtCode.Lines;
                    this.lastText = this.rchtxtCode.Text;
                    return;
                }
            }
            
            if ((qtdNewChars == -1 && this.lastText[this.rchtxtCode.SelectionStart] == '\'') //if just a single quot was erased
                || erasedSingQuot) //if one or more single quot were erased in just one line and need change
            {
                int indexSingQuot = this.rchtxtCode.SelectionStart - qtdCharsOtherLines;

                if (countApp < 0)
                    //count number of ' before the current '
                    countApp = line.CountAppearances('\'', 0, indexSingQuot);

                this.PutWordRealColorAlsoString(line, indexSingQuot, countApp, qtdCharsOtherLines);

                this.lastLines = this.rchtxtCode.Lines;
                this.lastText = this.rchtxtCode.Text;
            }
            else
            {
                //if the line has just whites spaces, there's nothing to do
                if (String.IsNullOrWhiteSpace(line))
                {
                    this.lastText = this.rchtxtCode.Text;
                    this.lastLines = this.rchtxtCode.Lines;
                    return;
                }

                for (int i = 0; i < 2; i++)
                {
                    if (i == 0) //word to the left
                        this.PutWordRealColor(line, qtdCharsOtherLines);
                    else //word to the right
                    {
                        int firstWordLetter;
                        try
                        {
                            char c = this.rchtxtCode.Text[this.rchtxtCode.SelectionStart - 1];
                            int equalsNumber = c.EqualsList(specialChars);

                            if (equalsNumber >= 0) //if user had writter any special char
                            {
                                if (equalsNumber == iSingQuot && !this.erased) //single quotation mark
                                {
                                    int indexSingQuot = this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 1;

                                    //count number of ' before the current '
                                    bool even = line.CountAppearances('\'', 0, indexSingQuot) % 2 == 0;
                                    //even: put everything in red
                                    //odd: put real word color

                                    int endString = 0;
                                    while (endString < line.Length)
                                    {
                                        endString = line.IndexOf('\'', indexSingQuot + 1);
                                        if (endString < 0)
                                            endString = line.Length;

                                        if (even)
                                            this.rchtxtCode.ChangeTextColor(Color.Red, indexSingQuot + qtdCharsOtherLines, endString + qtdCharsOtherLines);
                                        else
                                            this.PutAllWordsRealColorFromIndex(line, indexSingQuot + 1, endString, qtdCharsOtherLines);

                                        even = !even;
                                        indexSingQuot = endString;
                                    }
                                } else //any other special char
                                {
                                    firstWordLetter = this.rchtxtCode.SelectionStart - qtdCharsOtherLines;
                                    bool isString = false;
                                    this.PutWordRealColor(line, firstWordLetter, qtdCharsOtherLines, ref isString);

                                    if (!isString)
                                        this.rchtxtCode.ChangeTextColor(this.rchtxtCode.ForeColor, this.rchtxtCode.SelectionStart - 1, this.rchtxtCode.SelectionStart);
                                    else
                                        this.rchtxtCode.ChangeTextColor(Color.Red, this.rchtxtCode.SelectionStart - 1, this.rchtxtCode.SelectionStart);
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            break;
                        }
                    }
                }

                this.lastLines = this.rchtxtCode.Lines;
                this.lastText = this.rchtxtCode.Text;
            }
        }

        protected void rchtxtCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.erased = false;
            this.notChangeTxtbxCode = false;

            if (e.KeyCode == Keys.F5)
            {
                this.executeToolStripMenuItem.PerformClick();
            }else
            //put spaces when the user presses tab
            if (e.KeyCode == Keys.Tab)
            {

                //AcceptsTab

                //MessageBox.Show("Working on it!");
                //if nothing is selected and shift is not pressed
                //put 3 spaces in from of the 

                //if something is selected
                //if shift isn't pressed
                //put 3 spaces in the beginning of each selected lines
                //else
                //remove maximum 3 of possible spaces in from of the selected lines

                //show the key was already managed
            }
            else
            if (e.KeyCode == Keys.Enter)
            {
                //if the user wrote "begin" and then just spaces, when he presses [Enter] 
                //and the cursor is in front of the "begin":
                //  begin
                //    [cursor]
                //  end
            }
            else
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                this.erased = true;
            else
            if (e.KeyCode == Keys.Z)
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                    this.notChangeTxtbxCode = true;
            }
            else
            if (e.KeyCode == Keys.Y)
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                    this.notChangeTxtbxCode = true;
            }
        }

        
        //put words real color 
        protected void PutAllRchTxtRealColorAlsoString()
        {
            int qtdCharsOtherLines = 0;
            for(int i = 0; i<this.rchtxtCode.Lines.Length; i++)
            {
                this.PutWordRealColorAlsoString(this.rchtxtCode.Lines[i], 0, 0, qtdCharsOtherLines);
                qtdCharsOtherLines += this.rchtxtCode.Lines[i].Length + 1;
            }
        }

        protected void PutWordRealColorAlsoString(string line, int indexSingQuot, int countApp, int qtdCharsOtherLines)
        {
            bool even = countApp % 2 == 0;

            if (indexSingQuot == line.Length)
                return;

            int startIndex;
            if (even)
            {
                startIndex = line.LastIndexOf(specialChars, indexSingQuot);
                if (startIndex < 0)
                    startIndex = 0;
            }
            else
                startIndex = indexSingQuot;

            int endString = 0;
            while (endString < line.Length)
            {
                endString = line.IndexOf('\'', startIndex + 1);
                
                if (endString < 0)
                    endString = line.Length;
                else
                    endString += (even ? 0 : 1);

                if (even)
                    this.PutAllWordsRealColorFromIndex(line, startIndex, endString, qtdCharsOtherLines);
                else
                    this.rchtxtCode.ChangeTextColor(Color.Red, startIndex + qtdCharsOtherLines, endString + qtdCharsOtherLines);

                even = !even;
                startIndex = endString;
            }
        }
        
        protected void PutAllWordsRealColorFromIndex(string line, int startIndex, int endString, int qtdCharsOtherLines)
        {
            int proxIndex = line.Length;
            while (true)
            {
                int lastChar = line.IndexOf(specialChars, startIndex, endString);
                if (lastChar < 0)
                {
                    if (endString >= line.Length)
                        lastChar = line.Length;
                    else
                        break;
                }else
                    this.rchtxtCode.ChangeTextColor(this.rchtxtCode.ForeColor, lastChar + qtdCharsOtherLines, lastChar + 1 + qtdCharsOtherLines);

                this.PutWordRealColor(line, startIndex, lastChar, qtdCharsOtherLines);

                if (proxIndex == line.Length)
                    startIndex = line.IndexOf(specialChars, startIndex, endString);
                else
                    startIndex = proxIndex;

                if (startIndex < 0)
                    break;
                startIndex++;

                proxIndex = line.IndexOf(specialChars, startIndex, endString);
                if (proxIndex > endString)
                    break;
            }
        }

        protected void PutWordRealColor(string line, int qtdCharsOtherLines)
        {
            bool isString = false;
            this.PutWordRealColor(line, qtdCharsOtherLines, ref isString);
        }

        protected void PutWordRealColor(string line, int qtdCharsOtherLines, ref bool isString)
        {
            int end = this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 2;
            int firstLetter;
            if (end >= 0)
                firstLetter = line.LastIndexOf(specialChars, end) + 1;
            else
                firstLetter = 0;
            this.PutWordRealColor(line, firstLetter, qtdCharsOtherLines, ref isString);
        }

        protected void PutWordRealColor(string line, int firstWordLetter, int qtdCharsOtherLines)
        {
            bool isString = false;
            this.PutWordRealColor(line, firstWordLetter, qtdCharsOtherLines, ref isString);
        }
        
        protected void PutWordRealColor(string line, int firstWordLetter, int qtdCharsOtherLines, ref bool isString)
        {
            int lastChar = line.IndexOf(specialChars, firstWordLetter);
            if (lastChar < 0)
                lastChar = line.Length;
            this.PutWordRealColor(line, firstWordLetter, lastChar, qtdCharsOtherLines, ref isString);
        }

        protected void PutWordRealColor(string line, int firstWordLetter, int lastChar, int qtdCharsOtherLines)
        {
            bool isString = false;
            this.PutWordRealColor(line, firstWordLetter, lastChar, qtdCharsOtherLines, ref isString);
        }
        
        protected void PutWordRealColor(string line, int firstWordLetter, int lastChar, int qtdCharsOtherLines, ref bool isString)
        {
            int lengthFromSpace = (lastChar - firstWordLetter) + 1;
            string newWord = line.Substring(firstWordLetter, lengthFromSpace - 1);

            //the new word won't change anything if it's between quotations marks
            int count = line.CountAppearances('\'', 0, lastChar);
            isString = count % 2 != 0;

            if (!isString)
            {
                //if it's numeric put the number in red
                if (int.TryParse(newWord, out int res))
                    this.rchtxtCode.ChangeTextColor(Color.Green, qtdCharsOtherLines + firstWordLetter, qtdCharsOtherLines + lastChar);
                else
                if (!String.IsNullOrWhiteSpace(newWord))
                {
                    bool isResWord = false;
                    foreach (string resWord in this.reservedWords)
                    {
                        if (newWord.Equals(resWord, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //put the word in a different color
                            this.rchtxtCode.ChangeTextColor(Color.Blue, qtdCharsOtherLines + firstWordLetter, qtdCharsOtherLines + lastChar);
                            isResWord = true;

                            break;
                        }
                    }

                    if (!isResWord)
                        this.rchtxtCode.ChangeTextColor(this.rchtxtCode.ForeColor, qtdCharsOtherLines + firstWordLetter, qtdCharsOtherLines + lastChar);
                }
            }else
            //put red color on the char of the string
                this.rchtxtCode.ChangeTextColor(Color.Red, this.rchtxtCode.SelectionStart - 1, this.rchtxtCode.SelectionStart);
        }


        //auxiliary
        protected int IndexOfLine(int index, ref int qtdCharsOtherLines)
        {
            return this.IndexOfLine(this.rchtxtCode.Lines, index, ref qtdCharsOtherLines);
        }

        protected int IndexOfLine(string[] lis, int index, ref int qtdCharsOtherLines)
        {
            int indexOfLine = 0;
            int lengthIndex = index + 1;

            while (true)
            {
                int lengthLine = lis[indexOfLine].Length;
                if (lengthIndex - lengthLine <= 1)
                    //there're one more position to the cursor to stay (where there's no character in ints front)
                    return indexOfLine;
                else
                {
                    lengthIndex -= lengthLine + 1;
                    qtdCharsOtherLines += lengthLine + 1;
                    indexOfLine++;
                }
            }
        }


        //tests
        private void FrmDillenSQLManagementStudio_Click(object sender, EventArgs e)
        {
            if(teste)
            //SELECTION START EH SEMPRE O QUE ESTA MAIS PERTO DO COMECO
                MessageBox.Show("SelecioctionStart: " + this.rchtxtCode.SelectionStart + "\n\rSelectionLength: " + this.rchtxtCode.SelectionLength);
        }


    }
}

//classes:
// 1. MyString (not static, extends String + metodos)
// 2. SqlCommands (not static, extends SqlCommands)
    //todos os metodos com inicial minuscula