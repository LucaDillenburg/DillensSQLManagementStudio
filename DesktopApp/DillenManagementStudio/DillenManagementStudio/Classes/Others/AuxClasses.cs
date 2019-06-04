using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducativeSQLManagementStudio
{
    public class Error
    {
        protected int codCmd = -1;
        protected string code = null;
        protected string exception = null;
        protected bool isConnectionExcp = false;
        protected string strCmd = null;

        public Error(int codCommand, string strCommand, string wrongCode, string excep, bool connectionExcp)
        {
            this.codCmd = codCommand;
            this.strCmd = strCommand;
            this.exception = excep;
            this.code = wrongCode;
            this.isConnectionExcp = connectionExcp;
        }

        public int CodCommand
        {
            get
            {
                return this.codCmd;
            }

            set
            {
                this.codCmd = value;
            }
        }

        public string StrCommand
        {
            get
            {
                return this.strCmd;
            }

            set
            {
                this.strCmd = value;
            }
        }

        public string Exception
        {
            get
            {
                return this.exception;
            }

            set
            {
                this.exception = value;
            }
        }

        public string Code
        {
            get
            {
                return this.code;
            }

            set
            {
                this.code = value;
            }
        }

        public bool IsConnectionException
        {
            get
            {
                return this.isConnectionExcp;
            }

            set
            {
                this.isConnectionExcp = value;
            }
        }
    }

    public class UndoOrRedoInfo
    {
        protected string text = "";
        protected int selectionStart = 0;
        protected int selectionLength = 0;

        public UndoOrRedoInfo(string text, int selectionStart, int selectionLength)
        {
            this.text = text;
            this.selectionStart = selectionStart;
            this.selectionLength = selectionLength;
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        public int SelectionStart
        {
            get
            {
                return this.selectionStart;
            }

            set
            {
                this.selectionStart = value;
            }
        }

        public int SelectionLength
        {
            get
            {
                return this.selectionLength;
            }

            set
            {
                this.selectionLength = value;
            }
        }
        
    }
    
}
