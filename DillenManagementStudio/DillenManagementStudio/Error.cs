using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DillenManagementStudio
{
    public class Error
    {
        protected int codCmd = -1;
        protected string code = null;
        protected string exception = null;
        protected bool isConnectionExcp = false;

        public Error(int codCommand, string c, string excep, bool connectionExcp)
        {
            this.codCmd = codCommand;
            this.exception = excep;
            this.code = c;
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
}
