using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DillenManagementStudio
{
    public class SqlRichTextBox
    {
        //form that constains Sql Rich Text Box
        protected Form frmContainsRchtxtCode;

        //RICH TEXT BOX
        protected RichTextBox rchtxtCode;

        //commands
        protected List<string> reservedWords;
        //richTextBox color
        protected List<char> specialChars;
        protected int iSingQuot;
        //richTextBox change
        protected bool notChangeTxtbxCode = false;
        protected int lastQtdSingQuot = 0;
        protected bool erased = false;
        //richTextBox color
        protected string lastText;
        protected string[] lastLines;
        //larger or smaller font
        protected const int MIN_RCHTXT_ZOOM = 1;
        protected const int MAX_RCHTXT_ZOOM = 5;
        protected float rchtxtZoomFactor; //inicialized in SqlRichTextBoxProc()

        //if has typed anything
        protected bool hasTyped = false;

        //CONSTRUCTOR
        public SqlRichTextBox(ref RichTextBox rchtxtCode, Form frmContainsRchtxtCode,
            MySqlConnection mySqlCon, bool simpleProcedures)
        {
            this.frmContainsRchtxtCode = frmContainsRchtxtCode;
            this.rchtxtCode = rchtxtCode;
            this.specialChars = mySqlCon.SpecialChars;
            this.reservedWords = mySqlCon.ReservedWords;
            this.iSingQuot = mySqlCon.IndexSingQuot;
    
            this.rchtxtZoomFactor = this.rchtxtCode.ZoomFactor;
            this.lastText = this.rchtxtCode.Text;
            this.lastLines = this.rchtxtCode.Lines;

            if (simpleProcedures)
            {
                this.rchtxtCode.TextChanged += new System.EventHandler(this.rchtxtCode_TextChanged);
                this.rchtxtCode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rchtxtCode_PreviewKeyDown);
            }

            //ADD THIS.SQLRICHTEXTBOX.RICHTEXTBOX NO FORM
            this.frmContainsRchtxtCode.Controls.Add(this.rchtxtCode);
            this.rchtxtCode.BringToFront();
        }

        //param1: new System.EventHandler(this.newRchtxtCode_TextChanged)
        //param2: new System.Windows.Forms.PreviewKeyDownEventHandler(newRchtxtCode_PreviewKeyDown)
        public void SetNewEvents(EventHandler textChanged, PreviewKeyDownEventHandler previewKeyDown)
        {
            this.rchtxtCode.TextChanged += textChanged;
            this.rchtxtCode.PreviewKeyDown += previewKeyDown;
        }


        //GETTERS AND SETTERS
        public ref RichTextBox SQLRichTextBox
        {
            get
            {
                return ref this.rchtxtCode;
            }
        }

        public bool HasTyped
        {
            get
            {
                return this.hasTyped;
            }
        }


        //RCHTXTCODE PROCEDURES
        protected void rchtxtCode_TextChanged(object sender, EventArgs e)
        {
            this.rchtxtCode_TextChanged();
        }

        public void rchtxtCode_TextChanged()
        {
            this.hasTyped = true;

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
            if (qtdNewChars > 1)
            {
                int indexDif = this.rchtxtCode.Text.IndexDiferent(this.lastText); //index added
                int firstLine = this.IndexOfLine(indexDif, ref qtdCharsOtherLines);
                int notUsing = -1;
                int lastLine = this.IndexOfLine(indexDif + qtdNewChars, ref notUsing);

                for (int i = firstLine; i <= lastLine; i++)
                {
                    string currLine = this.rchtxtCode.Lines[i];

                    int indexBegin;
                    int currCoutApp;
                    if (i == 0)
                    {
                        indexBegin = indexDif;
                        currCoutApp = currLine.CountAppearances('\'', 0, indexDif);
                    }
                    else
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

                if (lineStartedDeleting == lineFinishDeleting)
                {
                    int qtdSingQuotInDeleted = this.lastText.CountAppearances('\'', indexStartDeleting, indexStartDeleting - qtdNewChars);
                    erasedSingQuot = (qtdSingQuotInDeleted % 2 != 0) && (indexStartDeleting - qtdNewChars < this.lastLines[lineStartedDeleting].Length);
                    //if the deleted chars had even number of single quotation marks, there's nothing to change
                }
                else
                {
                    int countL1 = this.lastLines[lineStartedDeleting].CountAppearances('\'', 0, indexStartDeleting - qtdCharsOtherStartL);
                    bool evenL1 = countL1 % 2 == 0;
                    int countL2 = this.lastLines[lineFinishDeleting].CountAppearances('\'', 0, indexStartDeleting - qtdNewChars - qtdCharsOtherEndL - 1);
                    bool evenL2 = countL2 % 2 == 0;

                    if (evenL1 != evenL2)
                    {
                        erasedSingQuot = true;
                        countApp = countL1;
                    }
                }

                if (!erasedSingQuot)
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
                                }
                                else //any other special char
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
        
        public void rchtxtCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.erased = false;
            this.notChangeTxtbxCode = false;
            
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
                if (e.Control)
                    this.notChangeTxtbxCode = true;
            }
            else
            if (e.KeyCode == Keys.Y)
            {
                if (e.Control)
                    this.notChangeTxtbxCode = true;
            }
        }


        //RCHTXTCODE SIZE (zoom)
        public bool SetRchtxtCodeSmallerFont()
        {
            //returns true if font can be larger and false if it can't

            if (this.rchtxtZoomFactor == 2 || this.rchtxtZoomFactor == 1.5)
                this.rchtxtZoomFactor = this.rchtxtZoomFactor - (float)0.5;
            else
                this.rchtxtZoomFactor--;

            this.rchtxtCode.ZoomFactor = this.rchtxtZoomFactor;
            
            if (this.rchtxtZoomFactor <= MIN_RCHTXT_ZOOM)
                return false;
            return true;
        }

        public bool SetRchtxtCodeLargerFont()
        {
            //returns true if font can be larger and false if it can't

            if (this.rchtxtZoomFactor == 1 || this.rchtxtZoomFactor == 1.5)
                this.rchtxtZoomFactor = this.rchtxtZoomFactor + (float)0.5;
            else
                this.rchtxtZoomFactor++;

            this.rchtxtCode.ZoomFactor = this.rchtxtZoomFactor;
            
            if (this.rchtxtZoomFactor >= MAX_RCHTXT_ZOOM)
                return false;
            return true;
        }


        //FILE
        public void CopyTextFromFile(string fileName)
        {
            //visual
            this.rchtxtCode.Visible = false;
            this.rchtxtCode.ReadOnly = true;
            
            //Clear
            this.rchtxtCode.Text = "";

            //write all text on RichTextBox and Colors it
            this.notChangeTxtbxCode = true;
            //text
            float lastZoom = this.rchtxtCode.ZoomFactor;
            this.rchtxtCode.ZoomFactor = 1;
            this.rchtxtCode.Text = File.ReadAllText(fileName);
            this.rchtxtCode.ZoomFactor = lastZoom;
            //color
            this.PutAllRchTxtRealColorAlsoString();
            this.notChangeTxtbxCode = false;

            //visual
            this.rchtxtCode.Visible = true;
            this.rchtxtCode.ReadOnly = false;
        }

        public void SaveFile(string fileName)
        {
            this.rchtxtCode.SaveFile(fileName, RichTextBoxStreamType.PlainText);

            /*
             * IN ANOTHER WAY:
            
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

        
        //UNDO and REDO
        public void Undo()
        {
            this.rchtxtCode.Undo();
        }

        public void Redo()
        {
            this.rchtxtCode.Redo();
        }


        //others
        public void Clear()
        {
            //Clear RichTextBox and let the same ZoomFactor 
            //(because when a RichTextBox's Text receives "", its ZoomFactor becames 1)
            float lastZoom = this.rchtxtCode.ZoomFactor;
            this.rchtxtCode.Text = "";
            this.rchtxtCode.ZoomFactor = lastZoom;
        }

        public void ForceFocus()
        {
            new Thread(() => this.AuxForceFocus()).Start();
        }

        protected void AuxForceFocus()
        {
            try
            {
                while (! (bool)this.rchtxtCode.Invoke(new Action(() => this.rchtxtCode.Focus())) )
                { }
            }catch(Exception e)
            { }
        }


        //AUXILIARY METHODS (put words real color)
        protected void PutAllRchTxtRealColorAlsoString()
        {
            int qtdCharsOtherLines = 0;
            for (int i = 0; i < this.rchtxtCode.Lines.Length; i++)
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
                }
                else
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
            }
            else
                //put red color on the char of the string
                this.rchtxtCode.ChangeTextColor(Color.Red, this.rchtxtCode.SelectionStart - 1, this.rchtxtCode.SelectionStart);
        }


        //AUXILIARY (index of line)
        public int IndexOfLine(int index, ref int qtdCharsOtherLines)
        {
            return this.IndexOfLine(this.rchtxtCode.Lines, index, ref qtdCharsOtherLines);
        }

        public int IndexOfLine(string[] lis, int index, ref int qtdCharsOtherLines)
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
        
    }
}
