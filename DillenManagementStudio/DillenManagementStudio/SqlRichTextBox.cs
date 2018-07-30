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
        protected bool erased = false;
        //richTextBox color
        protected string lastText;
        protected string[] lastLines;
        //resource of new words
        protected string lastWord = "";
        protected int lastCursorStart = 0;
        protected bool isSelected = false;
        //larger or smaller font
        protected const int MIN_RCHTXT_ZOOM = 1;
        protected const int MAX_RCHTXT_ZOOM = 5;
        protected float rchtxtZoomFactor; //inicialized in SqlRichTextBoxProc()

        //to TextChanged
        protected Keys keyPressed = new Keys();

        //UNDO and REDO
        protected Stack<UndoOrRedoInfo> ctrlZStack = new Stack<UndoOrRedoInfo>();
        protected Stack<UndoOrRedoInfo> ctrlYStack = new Stack<UndoOrRedoInfo>();
        protected bool undoingOrRedoing = false;
        protected bool notPutInUndoOrRedoStack = false;

        //if has typed anything
        protected bool hasTyped = false;

        //find and replace
        protected bool rchtxtHasChangedSinceLastSearch = true;
        protected Point lastTextSelected = new Point(-1, -1); //x: selectionStart, y: selectionLength
        protected int lastStartSelection = -1;


        //CONSTRUCTOR
        public SqlRichTextBox(ref RichTextBox rchtxtCode, Form frmContainsRchtxtCode,
            MySqlConnection mySqlCon, bool textChangeSimpleProcedures = true, bool findReplaceSimpleProcedures = true)
        {
            this.frmContainsRchtxtCode = frmContainsRchtxtCode;
            this.rchtxtCode = rchtxtCode;
            this.specialChars = mySqlCon.SpecialChars;
            this.reservedWords = mySqlCon.ReservedWords;
            this.iSingQuot = mySqlCon.IndexSingQuot;

            this.rchtxtZoomFactor = this.rchtxtCode.ZoomFactor;
            this.lastText = this.rchtxtCode.Text;
            this.lastLines = this.rchtxtCode.Lines;

            if (textChangeSimpleProcedures)
            {
                this.rchtxtCode.TextChanged += new System.EventHandler(this.rchtxtCode_TextChanged);
                this.rchtxtCode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rchtxtCode_PreviewKeyDown);
            }

            if (findReplaceSimpleProcedures)
            {
                this.rchtxtCode.Click += new System.EventHandler(this.rchtxtCode_Click);
                this.rchtxtCode.Enter += new System.EventHandler(this.rchtxtCode_Enter);
            }

            //more attributes configuration
            this.rchtxtCode.AcceptsTab = false;

            this.PutInCtrlZ();

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
        public void rchtxtCode_TextChanged(object sender, EventArgs e)
        {
            //clear undo
            this.rchtxtCode.ClearUndo();

            //change colors (and begin/end)
            int selectionStart = this.rchtxtCode.SelectionStart;
            int selectionLength = this.rchtxtCode.SelectionLength;
            if (this.rchtxtCode_TextChanged(ref selectionStart, ref selectionLength))
            //same selection as last time
                this.rchtxtCode.Select(selectionStart, selectionLength);

            //other variables
            this.lastLines = this.rchtxtCode.Lines;
            this.lastText = this.rchtxtCode.Text;
            this.keyPressed = new Keys();
        }

        protected bool rchtxtCode_TextChanged(ref int selectionStart, ref int selectionLength)
        {
            if (this.undoingOrRedoing)
                return false;

            this.hasTyped = true;

            if (!this.notPutInUndoOrRedoStack && this.lastText != this.rchtxtCode.Text && this.ctrlYStack.Count > 0)
                //if user typed something Ctrl+Y will be null
                this.ClearCtrlY();

            if (!this.notPutInUndoOrRedoStack)
            {
                int differenceLength = this.rchtxtCode.Text.Length - this.lastText.Length;
                if (differenceLength > 1 || this.isSelected || this.keyPressed == Keys.Space || 
                    this.keyPressed == Keys.Enter)
                    //if something was pasted
                    this.PutInCtrlZ();
                else
                    try
                    {
                        if (this.keyPressed == Keys.Delete && this.lastText[this.rchtxtCode.SelectionStart] == ' ')
                            this.PutInCtrlZ();
                    }
                    catch (Exception err)
                    { }
            }
            else
                this.notPutInUndoOrRedoStack = false;

            //help to put END in BEGIN
            bool cursorPosChanged = this.rchtxtCode.SelectionStart != this.lastCursorStart + 1;
            if (!this.notChangeTxtbxCode && (this.keyPressed != Keys.Enter || this.isSelected || cursorPosChanged))
            {
                if (this.erased)
                {
                    int lengthDeleted = this.lastCursorStart - this.rchtxtCode.SelectionStart;
                    if (this.lastText.Length - this.rchtxtCode.Text.Length == lengthDeleted &&
                        this.lastWord.Length - lengthDeleted > 0 && this.lastWord.Length > 0)
                        this.lastWord = this.lastWord.Substring(0, this.lastWord.Length - lengthDeleted);
                    else
                        this.lastWord = "";
                }
                else
                {
                    //[if changed cursor position (not only because he wrote another letter)]
                    if (cursorPosChanged)
                        this.lastWord = "";

                    //[if something was selected]       //[if something was pasted]
                    bool eraseWord = this.isSelected || this.rchtxtCode.Text.Length - this.lastText.Length > 1;
                    char c = '*';
                    if (!eraseWord)
                    {
                        try
                        {
                            c = this.rchtxtCode.Text[this.rchtxtCode.SelectionStart - 1];

                            //if SPACE, but lastWord.Equals("begin", StringComparison.InvariantCultureIgnoreCase), eraseWord is false
                            eraseWord = c == ' ' && !this.lastWord.TrimEnd().Equals("begin", StringComparison.InvariantCultureIgnoreCase);
                        }
                        catch (Exception e)
                        { }
                    }

                    if (eraseWord)
                        this.lastWord = "";
                    else
                        this.lastWord += c;
                }
            }

            this.lastCursorStart = this.rchtxtCode.SelectionStart;
            //if there's nothing written in the richTextBox, there's nothing to do
            if (this.rchtxtCode.Lines.Length == 0 || this.notChangeTxtbxCode)
                return false;


            //PUT END in begin
            if (this.keyPressed == Keys.Enter)
                if (this.lastWord.TrimEnd().Equals("begin", StringComparison.InvariantCultureIgnoreCase))
                {
                    //if the user wrote "begin" and then just spaces, when he presses [Enter] 
                    //and the cursor is in front of the "begin":
                    //  begin
                    //    [cursor]
                    //  end

                    bool capslock = this.lastWord[0] == 'B';

                    int notUsing = 0;
                    int currBeginLineIndex = this.IndexOfLine(this.rchtxtCode.SelectionStart, ref notUsing) - 1;
                    string beginLine = this.rchtxtCode.Lines[currBeginLineIndex];

                    this.notPutInUndoOrRedoStack = true;

                    int auxIndex = beginLine.Length - this.lastWord.Length - 1;
                    if (auxIndex < 0 || beginLine[auxIndex] == ' ')
                    {
                        bool skipLine = !String.IsNullOrWhiteSpace(this.rchtxtCode.Lines[currBeginLineIndex + 1]);

                        string spacesBeforeBegin = "";
                        while (beginLine[spacesBeforeBegin.Length] == ' ')
                            spacesBeforeBegin += " ";

                        //ajust spaces before between BEGIN/END
                        this.rchtxtCode.SelectedText = spacesBeforeBegin + "   " + Environment.NewLine;
                        selectionStart += spacesBeforeBegin.Length + 3;

                        //write END
                        this.notPutInUndoOrRedoStack = true;
                        this.rchtxtCode.SelectedText = spacesBeforeBegin + (capslock ? "END" : "end") +
                            (skipLine ? "\n" : "");

                        //change cursor position to between BEGIN/END
                        this.rchtxtCode.SelectionStart -= spacesBeforeBegin.Length + 4 + (skipLine ? 1 : 0);
                        this.rchtxtCode.SelectionLength = 0;
                    }

                    this.lastWord = "";

                    this.PutInCtrlZ();
                }


            ///put red in strings, green in numbers, blue in reserved words and black in the rest
            /*
             this.PutAllRchTxtRealColorAlsoString();
             return;
             */

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
                
                return true;
            }

            int indexLine = this.IndexOfLine(this.rchtxtCode.SelectionStart, ref qtdCharsOtherLines);
            string line = this.rchtxtCode.Lines[indexLine];

            int countApp = -1;
            bool erasedSingQuot = false;
            /*if (this.isSelect) //erased more than one char
            {
                ARRUMAR A PALAVRA: JUNCAO DA PALAVRA QUE FICOU DO INICIO DA SELECAO COM O 
                FINAL DA PALAVRA QUE ACABOU A SELECAO????????
            }*/

            if ((qtdNewChars == -1 && this.lastText[this.rchtxtCode.SelectionStart] == '\'') //if just a single quot was erased
                || erasedSingQuot) //if one or more single quot were erased in just one line and need change
            {
                int indexSingQuot = this.rchtxtCode.SelectionStart - qtdCharsOtherLines;

                if (countApp < 0)
                    //count number of ' before the current '
                    countApp = line.CountAppearances('\'', 0, indexSingQuot);

                this.PutWordRealColorAlsoString(line, indexSingQuot, countApp, qtdCharsOtherLines);
            }
            else
            {
                //if the line has just whites spaces, there's nothing to do
                if (String.IsNullOrWhiteSpace(line))
                    return true;

                if (this.keyPressed == Keys.Enter)
                {
                    //Enter line
                    this.PutWordRealColor(line, qtdCharsOtherLines);

                    //Line before enter
                    this.rchtxtCode.SelectionStart--;
                    char c = this.rchtxtCode.Text[this.rchtxtCode.SelectionStart];
                    this.PutWordRealColor(this.rchtxtCode.Lines[indexLine - 1], 
                    qtdCharsOtherLines - this.rchtxtCode.Lines[indexLine - 1].Length - 1);
                    this.rchtxtCode.SelectionStart++;
                }else
                {
                    //word left to the cursor
                    this.PutWordRealColor(line, qtdCharsOtherLines);
                    
                    //word right to the cursor
                    int firstWordLetter;
                    try
                    {
                        char c = this.rchtxtCode.Text[this.rchtxtCode.SelectionStart - 1];
                        int equalsNumber = c.EqualsList(specialChars);

                        if (equalsNumber >= 0) //if user had written any special char
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
                    {}
                }
            }
            
            return true;
        }

        public void rchtxtCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.rchtxtCode.ClearUndo();

            this.erased = false;
            this.notChangeTxtbxCode = false;
            //to TextChanged
            this.keyPressed = e.KeyCode;
            //to BEGIN and END
            this.isSelected = this.rchtxtCode.SelectionLength > 0;

            if (this.isSelected && e.IsKeyChangeText() && e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete)
            {
                //Explanation:
                //if something is selected:
                //Stage 1. Erase selection
                //Stage 2. color the rest of the text

                //Realization
                //Stage 1: delete selected text
                this.notPutInUndoOrRedoStack = true;
                this.rchtxtCode.SelectedText = "";
                this.notPutInUndoOrRedoStack = false;
                //Stage 2: following
            }

            //put spaces when the user presses tab
            if (e.KeyCode == Keys.Tab)
            {
                int cursorStart = this.rchtxtCode.SelectionStart;
                int cursorLength = this.rchtxtCode.SelectionLength;
                bool shift = e.Shift;

                if (this.rchtxtCode.SelectionLength > 0)
                {
                    int qtdCharsOtherLines = 0;
                    int firstLine = this.IndexOfLine(cursorStart, ref qtdCharsOtherLines);
                    int notUsing = 0;
                    int lastLine = this.IndexOfLine(cursorStart + cursorLength,
                        ref notUsing);

                    //if something is selected
                    //if shift isn't pressed
                    //put 3 spaces in the beginning of each selected lines
                    //else
                    //remove maximum 3 of possible spaces in from of the selected lines
                    int allLength = 0;
                    for (int i = firstLine; i <= lastLine; i++)
                    {
                        if (!String.IsNullOrEmpty(this.rchtxtCode.Lines[i]))
                        {
                            if (shift)
                            {
                                this.rchtxtCode.SelectionStart = qtdCharsOtherLines;
                                int length = 0;
                                while (length < 3)
                                {
                                    if (this.rchtxtCode.Text[qtdCharsOtherLines + length] == ' ')
                                        length++;
                                    else
                                        break;
                                }

                                if (length > 0)
                                    this.notPutInUndoOrRedoStack = true;
                                this.rchtxtCode.SelectionLength = length;
                                this.rchtxtCode.SelectedText = "";
                                allLength -= length;
                            }
                            else
                            {
                                this.notPutInUndoOrRedoStack = true;
                                this.rchtxtCode.SelectionStart = qtdCharsOtherLines;
                                this.rchtxtCode.SelectionLength = 0;
                                this.rchtxtCode.SelectedText = "   ";
                                allLength += 3;
                            }
                        }

                        qtdCharsOtherLines += this.rchtxtCode.Lines[i].Length + 1;
                    }

                    cursorLength += allLength;

                    if (allLength != 0)
                        this.PutInCtrlZ();
                }
                else
                {
                    if (shift)
                    {
                        //if shift is pressed, erase maximum 3 spaces before cursor start
                        int length = 0;
                        while (length < 3)
                        {
                            if (this.rchtxtCode.Text[this.rchtxtCode.SelectionStart - length - 1] == ' ')
                                length++;
                            else
                                break;
                        }

                        cursorStart -= length;
                        this.rchtxtCode.SelectionStart = cursorStart;
                        this.rchtxtCode.SelectionLength = length;
                        this.rchtxtCode.SelectedText = "";
                    }
                    else
                    {
                        //if shift is not pressed
                        //put 3 spaces in from of each line
                        this.notPutInUndoOrRedoStack = true;
                        this.rchtxtCode.SelectionLength = 0;
                        this.rchtxtCode.SelectedText = "   ";

                        cursorStart += 3;

                        this.PutInCtrlZ();
                    }
                }

                this.rchtxtCode.SelectionStart = cursorStart;
                this.rchtxtCode.SelectionLength = cursorLength;

                //Focus comes back to RichTextBox
                Force.Focus(this.rchtxtCode);
            }
            else
            if (e.KeyCode == Keys.Z && e.Control)
                //undo
                this.Undo();
            else
            if (e.KeyCode == Keys.Y && e.Control)
                //redo
                this.Redo();
            else
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                this.erased = true;
            else
            if (e.KeyCode == Keys.Z && e.Control)
                this.notChangeTxtbxCode = true;
            else
            if (e.KeyCode == Keys.Y && e.Control)
                this.notChangeTxtbxCode = true;
        }


        //FIND AND REPLACE
        public void Find(string searchedText, StringComparison stringComparison)
        {
            if (String.IsNullOrEmpty(searchedText))
                return;

            int cursorStart = this.rchtxtCode.SelectionStart;
            int cursorLength = this.rchtxtCode.SelectionLength;

            this.ChangeBackColorFromLastSearch();

            //SEARCHES IN MAXIMIUM THE WHOLE TEXT ONCE
            int oldLength = searchedText.Length;
            //real important variables
            int nextIndex = -1;
            int startIndex;
            int cursor;
            int finalIndex;
            if (cursorLength <= 0)
            {
                startIndex = 0;
                cursor = this.rchtxtCode.SelectionStart + (this.rchtxtHasChangedSinceLastSearch ? 0 : 1);
                finalIndex = this.rchtxtCode.Text.Length;
            }
            else
            {
                startIndex = this.rchtxtCode.SelectionStart;
                //if there was nothing selected
                if (this.lastStartSelection < 0)
                    cursor = this.rchtxtCode.SelectionStart;
                else
                    cursor = this.lastStartSelection + 1;
                finalIndex = this.rchtxtCode.SelectionStart + this.rchtxtCode.SelectionLength;
            }

            while (true)
            {
                int currIndex = this.rchtxtCode.Text.IndexOf(searchedText, startIndex, finalIndex - startIndex, stringComparison);

                if (currIndex == -1)
                    break;

                if (currIndex >= cursor)
                {
                    nextIndex = currIndex;
                    break;
                }
                else
                if (nextIndex == -1)
                    nextIndex = currIndex;

                startIndex = currIndex + oldLength;
            }

            /*
            //SEARCHES IN MAXIMIUM THE WHOLE TEXT ONCE
            int start = this.rchtxtCode.SelectionStart + (this.rchtxtHasChangedSinceLastSearch ? 0 : 1);
            int nextIndex = this.rchtxtCode.Text.IndexOf(this.txtFind.Text, start, stringComparison);
            
            if(nextIndex < 0 && this.rchtxtCode.SelectionStart > 0)
            //app has to search all text again (because he can search for a phrase and be with the cursor in the middle of it)
                nextIndex = this.rchtxtCode.Text.IndexOf(this.txtFind.Text, stringComparison); */

            if (nextIndex < 0)
            {
                this.lastTextSelected = new Point(-1, -1);
                throw new Exception("The following specified text was not found:\n\r" + searchedText);
            }
            else
            {
                this.rchtxtCode.SelectionStart = nextIndex;
                this.rchtxtCode.SelectionLength = searchedText.Length;
                this.rchtxtCode.SelectionBackColor = Color.Orange;

                this.lastTextSelected = new Point(this.rchtxtCode.SelectionStart, this.rchtxtCode.SelectionLength);

                if (cursorLength > 0)
                {
                    this.rchtxtCode.SelectionStart = cursorStart;
                    this.rchtxtCode.SelectionLength = cursorLength;
                    this.lastStartSelection = nextIndex;
                }
                else
                    this.rchtxtCode.SelectionLength = 0;

                this.rchtxtHasChangedSinceLastSearch = false;
            }
        }

        public void Replace(string textThatWillReplace)
        {
            if (this.lastTextSelected.Y >= 0)
            {
                //because searched word doesn't stays selected
                this.rchtxtCode.SelectionStart = this.lastTextSelected.X;
                this.rchtxtCode.SelectionLength = this.lastTextSelected.Y;

                this.rchtxtCode.SelectedText = textThatWillReplace;
                this.lastTextSelected.Y = textThatWillReplace.Length;
                this.ChangeBackColorFromLastSearch();
            }
        }

        public bool ReplaceAll(string oldText, string newText, StringComparison stringComparison)
        {
            if (!String.IsNullOrEmpty(oldText))
            {
                int cursorPos = this.rchtxtCode.SelectionStart;
                int cursorLength = this.rchtxtCode.SelectionLength;

                //replace all apperances of txtFind.Text with txtReplace.Text
                int lengthNew = newText.Length;
                int lengthOld = oldText.Length;
                int startIndex = (cursorLength > 0 ? cursorPos : 0);
                int lastIndex = (cursorLength > 0 ? cursorLength + cursorPos : this.rchtxtCode.Text.Length);
                int qtdReplaced = 0;
                while (true)
                {
                    int currIndex = this.rchtxtCode.Text.IndexOf(oldText, startIndex, lastIndex - startIndex, stringComparison);

                    if (currIndex == -1)
                        break;

                    this.rchtxtCode.SelectionStart = currIndex;
                    this.rchtxtCode.SelectionLength = lengthOld;
                    this.rchtxtCode.SelectedText = newText;
                    lastIndex += lengthNew - lengthOld;
                    qtdReplaced++;

                    startIndex = currIndex + lengthNew;
                }

                if (qtdReplaced <= 0)
                    throw new Exception("The following specified text was not found:\n\r" + oldText);

                this.rchtxtCode.Focus();
                this.rchtxtCode.SelectionStart = cursorPos;
                this.rchtxtCode.SelectionLength = (cursorLength == 0 ? 0 :
                    cursorLength + qtdReplaced * (lengthNew - lengthOld));
                return true;
            }
            else
                return false;
        }
        //auxiliary
        public void ChangeBackColorFromLastSearch()
        {
            //if there was a selection before
            if (this.lastTextSelected.Y >= 0)
            {
                int selStart = this.rchtxtCode.SelectionStart;
                int selLength = this.rchtxtCode.SelectionLength;

                //put background color back to normal
                this.rchtxtCode.SelectionStart = this.lastTextSelected.X;
                this.rchtxtCode.SelectionLength = this.lastTextSelected.Y;
                this.rchtxtCode.SelectionBackColor = this.rchtxtCode.BackColor;

                this.rchtxtCode.SelectionStart = selStart;
                this.rchtxtCode.SelectionLength = selLength;
            }
        }
        //richtextbox events to do Find and Replace
        protected void rchtxtCode_Click(object sender, EventArgs e)
        {
            this.rchtxtHasChangedSinceLastSearch = true;

            if (this.lastTextSelected.Y >= 0)
            {
                this.ChangeBackColorFromLastSearch();
                this.rchtxtCode.SelectionStart = this.lastTextSelected.X;
                this.rchtxtCode.SelectionLength = this.lastTextSelected.Y;
                this.lastTextSelected = new Point(-1, -1);
            }
        }

        protected void rchtxtCode_Enter(object sender, EventArgs e)
        {
            this.rchtxtHasChangedSinceLastSearch = true;
            this.lastStartSelection = -1;
            this.ChangeBackColorFromLastSearch();
        }
        //other procedures that will be called from main form
        public void ConsiderNoSelectionBeforeWithSelection()
        {
            this.lastStartSelection = -1;
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
            if (this.ctrlZStack.Count > 0)
            {
                //insert last text on
                this.ctrlYStack.Push(new UndoOrRedoInfo(this.rchtxtCode.Text, this.rchtxtCode.SelectionStart,
                    this.rchtxtCode.SelectionLength));

                this.notPutInUndoOrRedoStack = true;
                UndoOrRedoInfo info = this.ctrlZStack.Pop();
                while(info.Text == this.rchtxtCode.Text)
                    info = this.ctrlZStack.Pop();

                this.PutUndoOrRedoInfoOnRchtxtCode(info);

                //this.rchtxtCode.Undo();
            }
        }

        public void Redo()
        {
            if (this.ctrlYStack.Count > 0)
            {
                //insert last text on
                this.PutInCtrlZ();

                this.notPutInUndoOrRedoStack = true;
                UndoOrRedoInfo info = this.ctrlYStack.Pop();
                this.PutUndoOrRedoInfoOnRchtxtCode(info);

                //this.rchtxtCode.Redo();
            }
        }

        protected void PutUndoOrRedoInfoOnRchtxtCode(UndoOrRedoInfo info)
        {
            this.rchtxtCode.Visible = false;

            this.undoingOrRedoing = true;

            float lastZoom = this.rchtxtCode.ZoomFactor;
            this.rchtxtCode.ZoomFactor = 1;
            this.rchtxtCode.Text = "";
            this.undoingOrRedoing = false;
            this.rchtxtCode.Text = info.Text;
            this.rchtxtCode.ZoomFactor = lastZoom;

            this.rchtxtCode.SelectionStart = info.SelectionStart;
            this.rchtxtCode.SelectionLength = info.SelectionLength;

            this.rchtxtCode.Visible = true;
            this.rchtxtCode.Focus();
        }

        protected void PutInCtrlZ()
        {
            //insert last text on
            this.ctrlZStack.Push(new UndoOrRedoInfo(this.rchtxtCode.Text, this.rchtxtCode.SelectionStart,
                this.rchtxtCode.SelectionLength));
        }

        protected void ClearCtrlY()
        {
            //clear ctrl+Y
            this.ctrlYStack = new Stack<UndoOrRedoInfo>();
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
                    this.PutAllWordsRealColorFromIndex(line, startIndex, endString /*+ (endString==line.Length?0:1)*/, qtdCharsOtherLines);
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