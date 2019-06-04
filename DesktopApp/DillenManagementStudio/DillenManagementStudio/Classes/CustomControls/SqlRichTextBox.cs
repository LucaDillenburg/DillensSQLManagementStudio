using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EducativeSQLManagementStudio
{
    public class SqlRichTextBox
    {
        //form that constains Sql Rich Text Box
        protected Form frmContainsRchtxtCode;

        //RICH TEXT BOX
        protected RichTextBox rchtxtCode;

        //to TextChanged
        //
        protected List<string> reservedWords;
        protected List<char> specialChars;
        protected int iSingQuot;
        //
        protected int lastCursorStart = 0;
        protected bool isSelected = false;
        protected string lastText;
        protected string[] lastLines;
        //
        protected Keys keyPressed = new Keys();
        protected bool notChangeTxtbxCode = false;
        protected bool erased = false;
        protected bool pasted = false;
        //resource of new words
        protected string lastWord = "";
        //to set rchtxtCode visible false
        protected const int QTD_CHARS_CHANGE_TO_SET_VISIBLE_FALSE = 13;
        protected const bool VISIBLE_TEXT_BOX_FALSE_IF_TOO_MANY_CHANGES = true;

        //larger or smaller font
        protected const int MIN_RCHTXT_ZOOM = 1;
        protected const int MAX_RCHTXT_ZOOM = 5;
        protected float rchtxtZoomFactor; //inicialized in SqlRichTextBoxProc()
        
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

            //clear undo
            this.rchtxtCode.ClearUndo();
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
                //int indexDif = this.rchtxtCode.Text.IndexDiferent(this.lastText); //index added
                int indexDif = this.rchtxtCode.SelectionStart - qtdNewChars;
                if (indexDif < 0)
                    indexDif = 0;
                
                //change pasted Font
                int selStart = this.rchtxtCode.SelectionStart;
                int selLength = this.rchtxtCode.SelectionLength;
                this.rchtxtCode.Select(indexDif, qtdNewChars);
                this.rchtxtCode.SelectionFont = this.rchtxtCode.Font;
                this.rchtxtCode.Select(selStart, selLength);

                int firstLine = this.IndexOfLine(indexDif, ref qtdCharsOtherLines);
                int qtdCharsOtherLines2 = -1;
                int lastLine = this.IndexOfLine(indexDif + qtdNewChars, ref qtdCharsOtherLines2);

                //if there's too many things to color, set visible false
                bool lastRchtxtbxVisible = this.rchtxtCode.Visible;
                if (VISIBLE_TEXT_BOX_FALSE_IF_TOO_MANY_CHANGES && (qtdNewChars >= QTD_CHARS_CHANGE_TO_SET_VISIBLE_FALSE || //if pasted too many thing
                    (this.rchtxtCode.Text.IndexOf('\'', indexDif, qtdNewChars)>=0 && //if pasted a single quotation mark and there's too many things to color
                    this.rchtxtCode.Lines[lastLine].Length - (indexDif - qtdCharsOtherLines2) >= QTD_CHARS_CHANGE_TO_SET_VISIBLE_FALSE)))
                    this.rchtxtCode.Visible = false;

                int indexFirstChar = this.rchtxtCode.Lines[firstLine].LastIndexOf(specialChars, indexDif - qtdCharsOtherLines);
                if (indexFirstChar < 0)
                    indexFirstChar = 0;
                
                for (int i = firstLine; i <= lastLine; i++)
                {
                    string currLine = this.rchtxtCode.Lines[i];

                    int indexBegin;
                    int currCoutApp;
                    if (i == 0)
                    {
                        indexBegin = indexFirstChar;
                        currCoutApp = currLine.CountAppearances('\'', 0, indexDif);
                    }
                    else
                    {
                        indexBegin = 0;
                        currCoutApp = 0;
                    }

                    this.PutWordsRealColorAlsoString(currLine, indexBegin, currLine.Length - 1, currCoutApp, qtdCharsOtherLines);
                    qtdCharsOtherLines += currLine.Length + 1;
                }

                if (!this.rchtxtCode.Visible)
                {
                    //comes back to the original Visibility
                    this.rchtxtCode.Visible = lastRchtxtbxVisible;
                    this.rchtxtCode.Focus();
                }
                return true;
            }else
            //if pasted just one char
            if (this.pasted && qtdNewChars == 1)
            {
                //change pasted Font
                this.rchtxtCode.SelectionStart--;
                this.rchtxtCode.SelectionLength = 1;
                this.rchtxtCode.SelectionFont = this.rchtxtCode.Font;
                this.rchtxtCode.SelectionStart++;
                this.rchtxtCode.SelectionLength = 0;
            }

            int indexLine = this.IndexOfLine(this.rchtxtCode.SelectionStart, ref qtdCharsOtherLines);
            string line = this.rchtxtCode.Lines[indexLine];
            
            bool erasedSingQuot = false;
            if (this.isSelected && qtdNewChars<1) //erased more than one char
            {
                //VER SE APAGOU SIGLE QUOTATION MARK
                erasedSingQuot = this.lastText.IndexOf('\'', this.rchtxtCode.SelectionStart, -qtdNewChars) >= 0;

                //ARRUMAR A PALAVRA: JUNCAO DA PALAVRA QUE FICOU DO INICIO DA SELECAO COM O 
                //FINAL DA PALAVRA QUE ACABOU A SELECAO????????
            }

            if ((qtdNewChars == -1 && this.lastText[this.rchtxtCode.SelectionStart] == '\'') //if just a single quot was erased
                || erasedSingQuot) //if one or more single quot were erased in just one line and need change
            {
                int indexWasSingQuot = this.rchtxtCode.SelectionStart - qtdCharsOtherLines;
                
                //if wasn't the last char
                if(indexWasSingQuot < line.Length)
                {
                    //count number of ' before the current '
                    int countApp = line.CountAppearances('\'', 0, indexWasSingQuot);

                    int startIndex;
                    if (countApp % 2 == 0)
                    {
                        startIndex = line.LastIndexOf(specialChars, indexWasSingQuot);
                        if (startIndex < 0)
                            startIndex = 0;
                    }
                    else
                        startIndex = indexWasSingQuot;

                    //if there's too many things to color, set visible false
                    bool lastRchtxtbxVisible = this.rchtxtCode.Visible;
                                                //number of chars PutRealColorAlsoString will change color
                    if (QTD_CHARS_CHANGE_TO_SET_VISIBLE_FALSE <= line.Length - startIndex)
                        this.rchtxtCode.Visible = false;

                    this.PutWordsRealColorAlsoString(line, startIndex, line.Length - 1, countApp, qtdCharsOtherLines);
                    
                    if (!this.rchtxtCode.Visible)
                    {
                        //comes back to the original Visibility
                        this.rchtxtCode.Visible = lastRchtxtbxVisible;
                        this.rchtxtCode.Focus();
                    }
                }
            }
            else
            {
                //if the line has just whites spaces, there's nothing to do
                if (String.IsNullOrWhiteSpace(line))
                    return true;

                if (this.keyPressed == Keys.Enter)
                {
                    //Line before enter
                    string lineBeforeBegin = this.rchtxtCode.Lines[indexLine - 1];
                    int indexFirstCharLastWord = lineBeforeBegin.LastIndexOf(specialChars, lineBeforeBegin.Length - 1);
                    if (indexFirstCharLastWord < 0)
                        indexFirstCharLastWord = 0;
                    else
                        indexFirstCharLastWord++;

                    int countSingleQuotApp = -1;
                    if (indexFirstCharLastWord < lineBeforeBegin.Length)
                    {
                        countSingleQuotApp = lineBeforeBegin.CountAppearances('\'', 0, indexFirstCharLastWord);
                        this.PutWordRealColorNotString(lineBeforeBegin, indexFirstCharLastWord, qtdCharsOtherLines - lineBeforeBegin.Length - 1);
                    }

                    //Enter line
                    if (countSingleQuotApp < 0)
                        countSingleQuotApp = lineBeforeBegin.CountAppearances('\'');
                    if (countSingleQuotApp % 2 == 0)
                        this.PutWordRealColorNotString(line, 0, qtdCharsOtherLines);
                    else
                        this.PutWordsRealColorAlsoString(line, 0, line.Length-1, countSingleQuotApp, qtdCharsOtherLines);
                }
                else
                {
                    if(!String.IsNullOrWhiteSpace(line))
                    {
                        char c = 'X';
                        bool error = false;
                        try
                        {
                            c = this.rchtxtCode.Text[this.rchtxtCode.SelectionStart - 1];
                        }catch(Exception e)
                        {
                            error = true;

                            //change color of the word in the right
                            this.PutWordRealColorNotString(line, 0, qtdCharsOtherLines);
                        }
                        
                        if(!error)
                        {
                            int equalsNumber = c.EqualsList(specialChars);
                            int countApp = line.CountAppearances('\'', 0, this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 1);

                            //if user has written a single quotation mark
                            if (equalsNumber == iSingQuot && !this.erased)
                            {
                                int indexSingQuot = this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 1;
                                int startIndex;
                                if (countApp % 2 == 0 && indexSingQuot>0)
                                {
                                    int indexList = -1;
                                    startIndex = line.LastIndexOf(specialChars, indexSingQuot - 1, ref indexList);
                                    if (startIndex < 0)
                                        startIndex = 0;

                                    if (indexList == iSingQuot)
                                        countApp--;
                                } else
                                    startIndex = indexSingQuot;

                                //if there's too many things to color, set visible false
                                bool lastRchtxtbxVisible = this.rchtxtCode.Visible;
                                                    //number of chars PutRealColorAlsoString will change color
                                if (QTD_CHARS_CHANGE_TO_SET_VISIBLE_FALSE <= line.Length - startIndex)
                                    this.rchtxtCode.Visible = false;

                                //color single quotation mark
                                this.PutWordsRealColorAlsoString(line, startIndex, line.Length - 1,
                                    countApp, qtdCharsOtherLines);

                                if (!this.rchtxtCode.Visible)
                                {
                                    //comes back to the original Visibility
                                    this.rchtxtCode.Visible = lastRchtxtbxVisible;
                                    this.rchtxtCode.Focus();
                                }
                            }
                            else
                            {
                                if (countApp % 2 != 0)
                                    //put last char written in red
                                    this.rchtxtCode.ChangeTextColor(Color.Red, this.rchtxtCode.SelectionStart - 1, this.rchtxtCode.SelectionStart);
                                else
                                {
                                    int startIndexSearchLeftWord;
                                    if (equalsNumber >= 0)
                                    {
                                        //change color of the word in the left of the cursor (following) and in the right

                                        //specialChar
                                        if(qtdNewChars == 1)
                                            this.rchtxtCode.ChangeTextColor(this.rchtxtCode.ForeColor, this.rchtxtCode.SelectionStart - 1, this.rchtxtCode.SelectionStart);

                                        //right word
                                        if (this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 1 < line.Length - 1)
                                            this.PutWordRealColorNotString(line, this.rchtxtCode.SelectionStart - qtdCharsOtherLines, qtdCharsOtherLines);

                                        //help for left word
                                        startIndexSearchLeftWord = this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 2;
                                    }
                                    else
                                        //change color of the word in the left of the cursor (following)
                                        //help for left word
                                        startIndexSearchLeftWord = this.rchtxtCode.SelectionStart - qtdCharsOtherLines - 1;

                                    if(startIndexSearchLeftWord >= 0)
                                    {
                                        //left word
                                        int indexFirstChar = line.LastIndexOf(specialChars, startIndexSearchLeftWord);
                                        if (indexFirstChar < 0)
                                            indexFirstChar = 0;
                                        else
                                            indexFirstChar++;

                                        this.PutWordRealColorNotString(line, indexFirstChar, qtdCharsOtherLines);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            return true;
        }

        public void rchtxtCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.rchtxtCode.ClearUndo();

            this.erased = false;
            this.pasted = false;
            this.notChangeTxtbxCode = false;
            //to TextChanged
            this.keyPressed = e.KeyCode;
            //to BEGIN and END
            this.isSelected = this.rchtxtCode.SelectionLength > 0;

            //Erases selected text before continuing (just if user pressed a key that would overwrite it)
            //so TextChanged doesn't have to worry about overwrites
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
            if (e.KeyCode == Keys.V && e.Control)
                this.pasted = true;
            else
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                this.erased = true;
            else
            if ((e.KeyCode == Keys.Z && e.Control) || (e.KeyCode == Keys.Y && e.Control))
                this.notChangeTxtbxCode = true;
        }
        

        //AUXILIARY METHODS (put words real color)
        protected void PutAllRchTxtRealColorAlsoString()
        {
            int qtdCharsOtherLines = 0;
            for (int i = 0; i < this.rchtxtCode.Lines.Length; i++)
            {
                this.PutWordsRealColorAlsoString(this.rchtxtCode.Lines[i], 0, this.rchtxtCode.Lines[i].Length - 1,
                    0, qtdCharsOtherLines);
                qtdCharsOtherLines += this.rchtxtCode.Lines[i].Length + 1;
            }
        }
        //until start and not counting it
        protected void PutWordsRealColorAlsoString(string line, int startIndex, int finalIncludedIndex, int countApp, int qtdCharsOtherLines)
        {
            if (startIndex >= line.Length)
                return;

            bool even = countApp % 2 == 0;

            int finalIncludedIndexPart;
            bool isFirstTime = true;
            bool canContinue = false;
            while (startIndex <= finalIncludedIndex || canContinue)
            {
                finalIncludedIndexPart = line.IndexOf('\'', startIndex, finalIncludedIndex - startIndex + 1);

                bool foundSingQuot = finalIncludedIndexPart >= 0;
                if (finalIncludedIndexPart < 0)
                    finalIncludedIndexPart = finalIncludedIndex;

                if (even)
                {
                    if (finalIncludedIndexPart - 1 >= 0)
                        this.PutWordsRealColorNotString(line, startIndex, finalIncludedIndexPart + (foundSingQuot ? -1 : 0), qtdCharsOtherLines);
                }
                else
                    this.rchtxtCode.ChangeTextColor(Color.Red, startIndex + qtdCharsOtherLines - (isFirstTime ? 0 : 1),
                        //because it's endIndex so it isn't included
                        finalIncludedIndex + 1 + qtdCharsOtherLines);

                even = !even;
                startIndex = finalIncludedIndexPart + 1;
                isFirstTime = false;
                canContinue = finalIncludedIndexPart == finalIncludedIndex && foundSingQuot;
            }
        }
        //included
        protected void PutWordsRealColorNotString(string line, int startIndex, int finalIncludedIndex, int qtdCharsOtherLines)
        {
            int proxIndex = line.Length;

            while (true)
            {
                int nextStartIndex = line.IndexFirstWord(specialChars, startIndex, finalIncludedIndex);
                if (nextStartIndex < 0)
                    break;
                this.rchtxtCode.ChangeTextColor(this.rchtxtCode.ForeColor, startIndex, nextStartIndex + 1);
                startIndex = nextStartIndex;

                int lastChar;
                try
                {                                                        //because it's ENDINDEX (not included)
                    lastChar = line.IndexOf(specialChars, startIndex + 1, finalIncludedIndex + 1);
                }
                catch (Exception e)
                { break; }

                if (lastChar < 0)
                    lastChar = finalIncludedIndex;
                else
                    lastChar--;

                this.PutWordRealColorNotString(line, startIndex, lastChar, qtdCharsOtherLines);

                if (lastChar == finalIncludedIndex)
                    break;

                startIndex = lastChar + 1;
            }
        }

        protected void PutWordRealColorNotString(string line, int firstWordLetter, int qtdCharsOtherLines)
        {
            int lastChar = line.IndexOf(specialChars, firstWordLetter);
            if (lastChar < 0)
                lastChar = line.Length;
            this.PutWordRealColorNotString(line, firstWordLetter, lastChar - 1, qtdCharsOtherLines);
        }

        protected void PutWordRealColorNotString(string line, int firstWordLetter, int lastChar, int qtdCharsOtherLines)
        {
            int wordLength = (lastChar - firstWordLetter) + 1;
            string newWord = line.Substring(firstWordLetter, wordLength);

            //if it's numeric put the number in red
            if (int.TryParse(newWord, out int res))
                this.rchtxtCode.ChangeTextColor(Color.Green, qtdCharsOtherLines + firstWordLetter, qtdCharsOtherLines + lastChar + 1);
            else
            if (!String.IsNullOrWhiteSpace(newWord))
            {
                bool isResWord = false;
                foreach (string resWord in this.reservedWords)
                {
                    if (newWord.Equals(resWord, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //put the word in a different color
                        this.rchtxtCode.ChangeTextColor(Color.Blue, qtdCharsOtherLines + firstWordLetter, qtdCharsOtherLines + lastChar + 1);
                        isResWord = true;

                        break;
                    }
                }

                if (!isResWord)
                    this.rchtxtCode.ChangeTextColor(this.rchtxtCode.ForeColor, qtdCharsOtherLines + firstWordLetter, qtdCharsOtherLines + lastChar + 1);
            }
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

        public int ReplaceAll(string oldText, string newText, StringComparison stringComparison)
        {
            if (!String.IsNullOrEmpty(oldText))
            {
                this.ChangeBackColorFromLastSearch();
                this.lastTextSelected = new Point(-1, -1);

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
                return qtdReplaced;
            }
            
            return -1;
        }
        //auxiliary
        public void CancelSearch()
        {
            this.ChangeBackColorFromLastSearch();
            this.lastTextSelected = new Point(-1, -1);
        }

        protected void ChangeBackColorFromLastSearch()
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