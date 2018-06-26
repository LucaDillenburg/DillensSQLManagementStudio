using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DillenManagementStudio
{
    public static class RichTextBoxExtensions
    {
        //https://stackoverflow.com/questions/1926264/color-different-parts-of-a-richtextbox-string
        public static void ChangeTextColor(this RichTextBox rchtxt, Color color, int startIndex, int endIndex)
        {
            int currSelIndex = rchtxt.SelectionStart;
            int currSelLength = rchtxt.SelectionLength;

            rchtxt.SelectionStart = startIndex;
            rchtxt.SelectionLength = endIndex - startIndex;
            rchtxt.SelectionColor = color;

            rchtxt.SelectionStart = currSelIndex;
            rchtxt.SelectionLength = currSelLength;
        }
    }
}
