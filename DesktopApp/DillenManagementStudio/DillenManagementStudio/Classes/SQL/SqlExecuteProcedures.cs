using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducativeSQLManagementStudio
{
    public class SqlExecuteProcedures
    {
        public static string AllCodes(SqlRichTextBox sqlRchtxtbx, ref Queue<int> linesNoEvenQuotMarks)
        {
            RichTextBox rchtxtCode = sqlRchtxtbx.SQLRichTextBox;
            //or this.rchtxtCode

            //put txtCode.Items in a String (with spaces between each line)
            string allCodes = "";

            //if there's nothing selected
            if (rchtxtCode.SelectionLength <= 0)
                for (int i = 0; i < rchtxtCode.Lines.Length; i++)
                {
                    string currLine = rchtxtCode.Lines[i];

                    //if there're even
                    if (SqlExecuteProcedures.RealCodeLine(ref currLine))
                        allCodes += " " + currLine;
                    else
                        linesNoEvenQuotMarks.Enqueue(i);
                }
            else
            //there's something selected
            {
                int qtdOtherChars1 = 0;
                int indexFirstLine = sqlRchtxtbx.IndexOfLine(rchtxtCode.SelectionStart, ref qtdOtherChars1);
                int qtdOtherChars2 = 0;
                int indexLastLine = sqlRchtxtbx.IndexOfLine(rchtxtCode.SelectionStart + rchtxtCode.SelectionLength,
                    ref qtdOtherChars2);

                for (int i = indexFirstLine; i <= indexLastLine; i++)
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
                    if (SqlExecuteProcedures.RealCodeLine(ref line))
                        allCodes += " " + line;
                    else
                        linesNoEvenQuotMarks.Enqueue(i);
                }
            }

            return allCodes;
        }
        //RealCode: returns line until commented part ("--" not between quotation marks)
        protected static bool RealCodeLine(ref string realCodeLine)
        {
            int startIndex = 0;
            int countQuotMarks = 0;
            int indexCommented = realCodeLine.Length;
            bool putRealCodeLine = false;

            while (true)
            {
                if (countQuotMarks % 2 == 0)
                {
                    indexCommented = realCodeLine.IndexOf("--", startIndex);
                    if (indexCommented < 0)
                        indexCommented = realCodeLine.Length;
                }
                else
                    indexCommented = realCodeLine.Length;

                int currIndex = realCodeLine.IndexOf("'", startIndex);

                if (currIndex < 0)
                    break;
                
                if (indexCommented < currIndex)
                {
                    realCodeLine = realCodeLine.Substring(0, indexCommented);
                    putRealCodeLine = true;
                    break;
                }

                startIndex = currIndex + 1;
                countQuotMarks++;
            }

            bool hasEvenQuotMarks = countQuotMarks%2==0;

            if(hasEvenQuotMarks && !putRealCodeLine)
                realCodeLine = realCodeLine.Substring(0, indexCommented);

            return hasEvenQuotMarks;
        }

        public static string MessageFromNoEvenQuotMarks(Queue<int> linesNoEvenQuotMarks)
        {
            string msg = "Error! Add closing single quotation marks in lines: ";

            while (linesNoEvenQuotMarks.Count > 0)
                msg += linesNoEvenQuotMarks.Dequeue() + (linesNoEvenQuotMarks.Count == 0 ? "!" : ", ");

            return msg;
        }

        public static void ChangeExecuteResultLabel(ref Label lbExecutionResult, bool worked, int qtdLinesChanged)
        {
            if(worked)
            {
                lbExecutionResult.Text = "Succesfully executed! (" + qtdLinesChanged + " lines changed...)";
                lbExecutionResult.ForeColor = Color.Green;
            }else
            {
                lbExecutionResult.Text = "Unsuccesfully executed! (" + qtdLinesChanged + " lines changed...)";
                lbExecutionResult.ForeColor = Color.Red;
            }
            
            if (!lbExecutionResult.Visible)
                lbExecutionResult.Visible = true;
        }

    }
}
