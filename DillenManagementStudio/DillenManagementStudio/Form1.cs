using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
//to work with the database: MySqlCommand

namespace DillenManagementStudio
{
    public partial class FrmDillenSQLManagementStudio : Form
    {
        //conection
        protected string connStr;
        protected MySqlConnection mySqlCon = new MySqlConnection();
        protected bool connected = false;

        //commands
        protected List<string> commands;
        protected List<string> reservedWords;

        //richTextBox color
        protected List<char> specialChars = new List<char>();
        protected int iSingQuot;

        //richTextBox change
        protected bool ctrlZorY = false;
        protected int lastQtdSingQuot = 0;
        protected bool erased = false;

        //richTextBox color
        protected string lastText = "";
        protected string[] lastLines = new string[0];
        //protected List<int> 
        
        //form methods
        public FrmDillenSQLManagementStudio()
        {
            InitializeComponent();

            //reserved words
            reservedWords = mySqlCon.ReservedWords;

            //special chars
            specialChars.Add(' ');
            iSingQuot = specialChars.Count;
            specialChars.Add('\'');
            specialChars.Add('.');
            specialChars.Add(';');
            specialChars.Add(',');
            specialChars.Add('*');
        }

        protected void Form1_Load(object sender, EventArgs e)
        {
            this.cbxChsDtBs.SelectedIndex = 0;

            //set SQL buttons
            this.EnableWichDependsCon(false);

            //set RichTextBox
            this.rchtxtCode.Focus();
            this.rchtxtCode.SelectionStart = 0;
            this.rchtxtCode.SelectionLength = this.rchtxtCode.Text.Length;
        }

        protected void EnableWichDependsCon(bool enable)
        {
            this.btnExecute.Enabled = enable;
            this.btnAllTables.Enabled = enable;
            this.rdAutomatic.Enabled = enable;
            this.rdNonQuery.Enabled = enable;
            this.rdSelect.Enabled = enable;
            this.btnAllProcFunc.Enabled = enable;
            this.connected = enable;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
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


        //change data base
        protected void btnChangeDtBs_Click(object sender, EventArgs e)
        {
            bool error = false;
            try
            {
                //stablish conection with the datebase
                switch (cbxChsDtBs.SelectedIndex)
                {
                    case 0:
                        mySqlCon.ConnStr = Properties.Settings.Default.BD17188ConnectionString;
                        break;
                    case 1:
                        mySqlCon.ConnStr = Properties.Settings.Default.BDPRII17188ConnectionString;
                        break;
                    default:
                        MessageBox.Show("btnEscBanco_Click error due to new datebase!");
                        break;
                }
            }
            catch (Exception err)
            {
                error = true;
                MessageBox.Show("An error occurred when trying to connect to the database chosen!");
                this.EnableWichDependsCon(false);
            }

            if (!error)
            {
                this.commands = this.mySqlCon.Commands;
                this.EnableWichDependsCon(true);

                MessageBox.Show("Database connected!");
            }
        }


        //text procedures
        protected void rchtxtCode_TextChanged(object sender, EventArgs e)
        {
            //if there's nothing written in the richTextBox, there's nothing to do
            if (this.rchtxtCode.Lines.Length == 0 || this.ctrlZorY)
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
                    
                    this.putWordRealColorAlsoString(currLine, indexBegin, currCoutApp, qtdCharsOtherLines);
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
                    int countL2 = this.lastLines[lineFinishDeleting].CountAppearances('\'', 0, indexStartDeleting - qtdNewChars - qtdCharsOtherEndL);
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

                this.putWordRealColorAlsoString(line, indexSingQuot, countApp, qtdCharsOtherLines);

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
                        this.putWordRealColor(line, qtdCharsOtherLines);
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
                                            this.putAllWordsRealColorFromIndex(line, indexSingQuot + 1, endString, qtdCharsOtherLines);

                                        even = !even;
                                        indexSingQuot = endString;
                                    }
                                } else //any other special char
                                {
                                    firstWordLetter = this.rchtxtCode.SelectionStart - qtdCharsOtherLines;
                                    bool isString = false;
                                    this.putWordRealColor(line, firstWordLetter, qtdCharsOtherLines, ref isString);

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
            this.ctrlZorY = false;

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
                    this.ctrlZorY = true;
            }
            else
            if (e.KeyCode == Keys.Y)
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                    this.ctrlZorY = true;
            }
        }

        
        //put words real color 
        protected void putWordRealColorAlsoString(string line, int indexSingQuot, int countApp, int qtdCharsOtherLines)
        {
            bool even = countApp % 2 == 0;

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
                    this.putAllWordsRealColorFromIndex(line, startIndex, endString, qtdCharsOtherLines);
                else
                    this.rchtxtCode.ChangeTextColor(Color.Red, startIndex + qtdCharsOtherLines, endString + qtdCharsOtherLines);

                even = !even;
                startIndex = endString;
            }
        }
        
        protected void putAllWordsRealColorFromIndex(string line, int startIndex, int endString, int qtdCharsOtherLines)
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

                this.putWordRealColor(line, startIndex, lastChar, qtdCharsOtherLines);

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

        protected void putWordRealColor(string line, int qtdCharsOtherLines)
        {
            bool isString = false;
            this.putWordRealColor(line, qtdCharsOtherLines, ref isString);
        }

        protected void putWordRealColor(string line, int qtdCharsOtherLines, ref bool isString)
        {
            int end = this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 2;
            int firstLetter;
            if (end >= 0)
                firstLetter = line.LastIndexOf(specialChars, end) + 1;
            else
                firstLetter = 0;
            this.putWordRealColor(line, firstLetter, qtdCharsOtherLines, ref isString);
        }

        protected void putWordRealColor(string line, int firstWordLetter, int qtdCharsOtherLines)
        {
            bool isString = false;
            this.putWordRealColor(line, firstWordLetter, qtdCharsOtherLines, ref isString);
        }
        
        protected void putWordRealColor(string line, int firstWordLetter, int qtdCharsOtherLines, ref bool isString)
        {
            int lastChar = line.IndexOf(specialChars, firstWordLetter);
            if (lastChar < 0)
                lastChar = line.Length;
            this.putWordRealColor(line, firstWordLetter, lastChar, qtdCharsOtherLines, ref isString);
        }

        protected void putWordRealColor(string line, int firstWordLetter, int lastChar, int qtdCharsOtherLines)
        {
            bool isString = false;
            this.putWordRealColor(line, firstWordLetter, lastChar, qtdCharsOtherLines, ref isString);
        }
        
        protected void putWordRealColor(string line, int firstWordLetter, int lastChar, int qtdCharsOtherLines, ref bool isString)
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


        //sql execute procedures
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            bool allowNotification = true;
            //fazer

            Queue<int> linesNoEvenQuotMarks = new Queue<int>();
            //put txtCode.Items in a String (with spaces between each line)
            string allCodes = "";
            for (int i = 0; i < this.rchtxtCode.Lines.Length; i++)
            {
                //if there're even
                if (this.rchtxtCode.Lines[i].CountAppearances('\'') % 2 != 0)
                    linesNoEvenQuotMarks.Enqueue(i);
                else
                   allCodes += " " + this.rchtxtCode.Lines[i];
            }

            if (linesNoEvenQuotMarks.Count > 0)
            {
                string msg = "Error! Add closing single quotation marks in lines: ";

                while(linesNoEvenQuotMarks.Count > 0)
                    msg += linesNoEvenQuotMarks.Dequeue() + (linesNoEvenQuotMarks.Count==0?", ":"!");

                MessageBox.Show(msg);
            } else
            if (this.rdAutomatic.Checked)
            {
                Queue<Error> errors = new Queue<Error>();
                this.grvSelect.DataSource = this.mySqlCon.ExecuteAutomaticSqlCommands(allCodes, ref errors);

                if (errors == null || errors.Count == 0)
                {
                    this.lbExecutionResult.Text = "Succesfully executed!";
                    this.lbExecutionResult.ForeColor = Color.Green;

                    if (!this.lbExecutionResult.Visible)
                        this.lbExecutionResult.Visible = true;

                    if (allowNotification)
                        MessageBox.Show("Succesfully executed!");
                }
                else
                {
                    this.lbExecutionResult.Text = "Unsuccesfully executed!";
                    this.lbExecutionResult.ForeColor = Color.Red;

                    if (!this.lbExecutionResult.Visible)
                        this.lbExecutionResult.Visible = true;

                    if (allowNotification)
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
                worked = this.mySqlCon.ExecuteOneSQLCmd(allCodes, this.rdSelect.Checked, ref dataTable, ref excep);

                this.grvSelect.DataSource = dataTable;

                if(allowNotification)
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
        
        protected void btnAllTables_Click(object sender, EventArgs e)
        {
            this.grvSelect.DataSource = this.mySqlCon.AllTables();
        }

        private void btnAllProcFunc_Click(object sender, EventArgs e)
        {
            this.grvSelect.DataSource = this.mySqlCon.AllProcFunc();
        }


        //others
        private void rdSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdAutomatic.Checked && this.connected)
                this.mySqlCon.restartCommands();
        }


        //testss
        private void FrmDillenSQLManagementStudio_Click(object sender, EventArgs e)
        {
            //SELECTION START EH SEMPRE O QUE ESTA MAIS PERTO DO COMECO
            MessageBox.Show("SelecioctionStart: " + this.rchtxtCode.SelectionStart + "\n\rSelectionLength: " + this.rchtxtCode.SelectionLength);
        }
                
    }
}

//classes:
// 1. MyString (not static, extends String + metodos)
// 2. SqlCommands (not static, extends SqlCommands)
    //todos os metodos com inicial minuscula