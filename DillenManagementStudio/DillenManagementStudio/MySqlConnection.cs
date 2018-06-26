using System.Collections.Generic;
//to use my strings methods: static MyStringMethods
//to work with the database
using System.Data.SqlClient;
//to use DataTable
using System.Data;
using System;
using System.Text.RegularExpressions;
using System.IO;

namespace DillenManagementStudio
{
    public class MySqlConnection : ICloneable
    {
        //conection
        protected string connStr;
        protected SqlConnection con;

        //commands
        protected List<string> commands = new List<string>(); //nonqueries, procs, exec, queries, sps
        //most important indexes (basic ones)
        protected int iFirstProcFuncTrigger;
        protected int iCommandExec;
        protected int iFirstQuery;
        protected int iFirstRealProc;
        //others indexes
        protected int iDropTable;
        protected int iCreateTrigger;
        protected int iAlterTrigger;
        //nonQuery to select
        protected List<string> nonQueryToSelect = new List<string>();

        //reserved words
        protected List<string> reservedWords = new List<string>();

        //tables
        protected List<string> tables = new List<string>();

        public MySqlConnection()
        {
            //Non Query To Select
            nonQueryToSelect.Add("insert into ");
            nonQueryToSelect.Add("update ");
            nonQueryToSelect.Add("alter table ");
            nonQueryToSelect.Add("create table ");
            nonQueryToSelect.Add("delete from ");

            //Reserved Words
            reservedWords.Add("select");
            reservedWords.Add("insert");
            reservedWords.Add("into");
            reservedWords.Add("update");
            reservedWords.Add("delete");
            reservedWords.Add("from");

            reservedWords.Add("table");
            reservedWords.Add("create");
            reservedWords.Add("drop");
            reservedWords.Add("alter");

            reservedWords.Add("begin");
            reservedWords.Add("end");

            reservedWords.Add("rule");
            reservedWords.Add("proc");
            reservedWords.Add("procedure");
            reservedWords.Add("function");
            reservedWords.Add("trigger");
            reservedWords.Add("exec");

            reservedWords.Add("where");
            reservedWords.Add("from");
            reservedWords.Add("values");
            reservedWords.Add("in");
            reservedWords.Add("between");
            reservedWords.Add("as");
            reservedWords.Add("on");
            reservedWords.Add("set");
            reservedWords.Add("if");
            reservedWords.Add("else");
            reservedWords.Add("while");
            reservedWords.Add("for");
            reservedWords.Add("instead");
            reservedWords.Add("of");
            //reservedWords.Add("insert");
            //reservedWords.Add("delete");
            reservedWords.Add("after");
            reservedWords.Add("with");
        }
        

        //getters and setters
        public string ConnStr
        {
            get
            {
                return this.connStr;
            }

            set
            {
                this.connStr = value;
                this.NewConnection();
            }
        }


        public List<string> ReservedWords
        {
            get
            {
                List<string> ret = new List<string>(this.reservedWords);
                for (int i = this.iFirstProcFuncTrigger; i < this.commands.Count; i++)
                    ret.Add(this.commands[i]);

                return ret;
            }
        }

        public List<string> ProcFuncTriggers
        {
            get
            {
                List<string> ret = new List<string>();
                for (int i = this.iFirstProcFuncTrigger; i < this.commands.Count; i++)
                    ret.Add(this.commands[i]);

                return ret;
            }
        }

        public List<string> Commands
        {
            get
            {
                return new List<string>(this.commands);
            }
        }

        public List<string> Tables
        {
            get
            {
                return new List<string>(this.tables);
            }
        }


        //conection on/off
        public void CloseConnection()
        {
            this.con.Close();
        }

        protected void NewConnection()
        {
            this.con = new SqlConnection();
            this.connStr = this.connStr.Substring(this.connStr.IndexOf("Data Source"));
            this.con.ConnectionString = this.connStr;
            this.con.Open();

            this.PutAllCommands();
            this.PutAllTables();
        }

        protected void PutAllCommands()
        {
            this.commands = new List<string>();

            //INICIALIZAR COMMANDS
            //NON-QUERIES
            this.commands.Add("insert into ");
            this.iDropTable = this.commands.Count; //i
            this.commands.Add("drop table ");
            this.commands.Add("alter table ");
            this.commands.Add("delete from ");
            this.commands.Add("update ");
            this.commands.Add("create table ");
            this.commands.Add("create rule ");
            this.commands.Add("drop proc ");
            this.commands.Add("drop procedure ");
            this.commands.Add("drop function ");
            this.commands.Add("drop trigger ");
            
            //PROCEDURES, FUNCTIONS, TRIGGER
            this.iFirstProcFuncTrigger = this.commands.Count; //i BASIC
            this.commands.Add("create proc ");
            this.commands.Add("create procedure ");
            this.commands.Add("alter proc ");
            this.commands.Add("alter procedure ");
            this.commands.Add("create function ");
            this.commands.Add("alter function ");
            this.iCreateTrigger = this.commands.Count; //i
            this.commands.Add("create trigger ");
            this.iAlterTrigger = this.commands.Count; //i
            this.commands.Add("alter trigger ");

            //DON'T PUT IT IN THE COMMANDS' QUEUE, BECAUSE WE DONT'T NEED IT
            this.iCommandExec = this.commands.Count; //i BASIC
            this.commands.Add("exec ");
            
            //QUERIES
            this.iFirstQuery = this.commands.Count; //i BASIC
            this.commands.Add("sp_bindrule ");
            this.commands.Add("sp_unbindrule ");
            this.commands.Add("select ");
            this.commands.Add("sp_help ");

            //REAL PROCEDURES AND FUNCTIONS
            //get all procedures and functions
            int nProcFunc = 0;
//fazer
            this.iFirstRealProc = this.commands.Count; //i BASIC
            //for (int i = 0; i < nProcFunc; i++)
            //    this.commands.Add(procedures[i]);
//fazer
        }

        protected void PutAllTables()
        {
            //fazer
            //this.tables
        }


        //sql execute
        public DataTable ExecuteAutomaticSqlCommands(string allCodes, ref Queue<Error> cmdErrors)
        {
            //replace multiple spaces with a single space
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            allCodes = regex.Replace(allCodes, " ").Trim();

            //separate the whole code in a commands QUEUE
            Queue<int> nSQLCommands = new Queue<int>();
            int nQuery = -1;
            int nNonQuerySelect = -1;
            Queue<string> codes = this.transformCodeInCommands(allCodes, ref nSQLCommands, ref nQuery, ref nNonQuerySelect);
            
            cmdErrors = new Queue<Error>();

            if (!this.IsConected())
            {
                //Say that the conection is over
                cmdErrors.Enqueue(new Error(-1, "", "Connection with database is down!", true));
                return new DataTable();
            }

            //execute each
            DataTable ultimateDataTable = null;
            for (int iCode = 0; codes.Count > 0; iCode++)
            {
                int cmdN = nSQLCommands.Dequeue();

                //execute SQL commands
                DataTable auxDtTable = null; //not any value
                string excep = null;
                string currCode = codes.Dequeue();
                bool worked = this.ExecuteOneSQLCmd(currCode, 
                    (cmdN >= this.iFirstQuery && cmdN < this.iFirstRealProc), //isQuery
                    //executeQuery 
                    (nQuery == iCode //if it's the last select
                      || (nQuery < 0 && nNonQuerySelect == iCode)), 
                    //if it's the last non-query select and there's no queries
                    cmdN, ref auxDtTable, ref excep);
                //returns true if it worked and false if it didn't work

                if (!worked)
                {
                    Error curErr = new Error(cmdN, currCode, excep, false);
                    cmdErrors.Enqueue(curErr);
                }
                else
                {
                    if(auxDtTable != null)
                        ultimateDataTable = auxDtTable;
                }
            }

            if (ultimateDataTable == null)
                ultimateDataTable = new DataTable();
            return ultimateDataTable;
        }

        public bool ExecuteOneSQLCmd(string code, bool queryExecution, ref DataTable table, ref string exception)
        {
            return this.ExecuteOneSQLCmd(code, queryExecution, queryExecution, -1, ref table, ref exception);
        }

        protected bool ExecuteOneSQLCmd(string code, bool queryExecution, bool executeQuery, int iCommand, ref DataTable table, ref string exception)
        {
            //execute query or nonQuery depending of the parameters
            //if it's a query, show it in the DataGridView 
            //else try to show select of that whole table that was changed

            //returns true if it worked and false if it didn't work

            if (!queryExecution && !String.IsNullOrWhiteSpace(code))
            {
                try
                {
                    SqlCommand cmdNQ = new SqlCommand(code, this.con);

                    int iResult = cmdNQ.ExecuteNonQuery();
                    //iResult value is the number of rows affected by the command.
                }
                catch (Exception err)
                {
                    exception = "Non Query error: " + err.Message;
                    return false;
                }

                if (executeQuery)
                {
                    int firstLetter;
                    
                    if (iCommand == this.iDropTable)
                    {
                        table = this.AllTables();
                        return true;
                    }else
                    if(iCommand == this.iAlterTrigger || iCommand == this.iCreateTrigger)
                        firstLetter = code.IndexOf(" on ", StringComparison.CurrentCultureIgnoreCase) + 4;
                    else
                        firstLetter = code.IndexOfEvenSingQuotMarksAndNothingBefore(this.commands[iCommand], 0) + this.commands[iCommand].Length;

                    //create select based on the table that was modified
                    int indAfterWord = code.IndexOf(" ", firstLetter + 1);
                    if (indAfterWord < 0) //if the table name is the last thing in the sentence
                        indAfterWord = code.Length;

                    int length = indAfterWord - firstLetter;
                    string tableName = code.Substring(firstLetter, length);
                    code = "Select top(100)* from " + tableName;
                }
            }

            //just execute it if it has something in the code and if it is a query execution or if it doesn't have any query in the whole code
            if (!String.IsNullOrWhiteSpace(code))
            {
                DataSet ds = new DataSet();

                table = new DataTable();
                try
                {
                    SqlCommand cmdQ = new SqlCommand(code, this.con);

                    SqlDataAdapter adapt = new SqlDataAdapter(cmdQ);

                    adapt.Fill(ds);

                    if (ds.Tables.Count <= 0)
                    {
                        if (!queryExecution)
                            return true;
                        //if an error was made in trying to do a select based on the nonQuery execution, I did it wrong

                        exception = "Query error! No exception raised...";
                        return false;
                    }
                }
                catch (Exception err)
                {
                    if (!queryExecution)
                        return true;
                    //if an error was made in trying to do a select based on the nonQuery execution, I did it wrong

                    exception = "Query error: " + err.Message;
                    return false;
                }
                

                //I have to do what's in above independing if it was a select or not 
                //because if it was wrong I'd have to tell the user
                if (executeQuery)
                {
                    //change number of rows and columns of the DataGridView
                    //put the names of the columns                 
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        table.Columns.Add(ds.Tables[0].Columns[i].ColumnName + " (" + ds.Tables[0].Columns[i].DataType.ToString() + ")", ds.Tables[0].Columns[i].DataType == typeof(bool) ? typeof(string) : ds.Tables[0].Columns[i].DataType);

                    for (int ir = 0; ir < ds.Tables[0].Rows.Count; ir++)
                        table.Rows.Add(ds.Tables[0].Rows[ir].ItemArray);
                }
            }

            return true;
        }

        protected bool cmdIsNonQueryToSelect(int iCommand)
        {
            if (iCommand < 0 || iCommand >= this.commands.Count)
                return false;

            //special 
            if (iCommand == this.iDropTable || iCommand == this.iCreateTrigger || iCommand == this.iAlterTrigger)
                return true;

            for (int i = 0; i < nonQueryToSelect.Count; i++)
                if (this.commands[iCommand].Equals(nonQueryToSelect[i]))
                    return true;
            return false;
        }

        protected Queue<string> transformCodeInCommands(string code, ref Queue<int> nSQLCommands, ref int nQuery, ref int nNonQuerySelect)
        {
            //separate the whole code in commands in a QUEUE
            Queue<string> codes = new Queue<string>();
            //Enqueue(Object) Add an object in the Queue       
            
            int startIndex = 0;
            int iCommandBegins = 0;
            int prevCmdNumber = -1; //not any number
            for (int i = 0; ; i++)
            {
                int cmdNumber = -1; //not any number

                int lastIndexOfWords = -1; //not any number
                int index;
                if (startIndex >= code.Length)
                    index = -1;
                else
                    index = code.IndexOfFirstMinistrListCI(commands, startIndex, ref cmdNumber, ref lastIndexOfWords);

                if (index < 0)
                {
                    if (i == 0)
                        codes.Enqueue(code);
                    else
                        codes.Enqueue(code.Substring(iCommandBegins).Trim());
                    
                    //if i==0, prevCmdNumber is already -1
                    nSQLCommands.Enqueue(prevCmdNumber);

                    //if it's a query command
                    if(prevCmdNumber >= this.iFirstQuery && prevCmdNumber < this.iFirstRealProc)
                        nQuery = codes.Count - 1;
                    //if there ins't any query yet and it's a command that can be selected
                    if (nQuery < 0 && this.cmdIsNonQueryToSelect(prevCmdNumber))
                        nNonQuerySelect = codes.Count - 1;
                    break;
                }

                string oneCommand = code.Substring(iCommandBegins, index - iCommandBegins).Trim();
                if (!System.String.IsNullOrWhiteSpace(oneCommand))
                {
                    codes.Enqueue(oneCommand);
                    nSQLCommands.Enqueue(prevCmdNumber);

                    //if it's a query command
                    if (prevCmdNumber >= this.iFirstQuery && prevCmdNumber < this.iFirstRealProc)
                        nQuery = codes.Count - 1;
                    //if there ins't any query yet and it's a command that can be selected
                    if (nQuery < 0 && this.cmdIsNonQueryToSelect(prevCmdNumber))
                        nNonQuerySelect = codes.Count - 1;
                }

                if (cmdNumber >= this.iFirstProcFuncTrigger && cmdNumber < this.iCommandExec)
                {
                    //colocar startIndex apos ultimo end
                    // ou depois do primeiro command
                    int curCmdNumber = -1;
                    int lastIndexOfCmd = -1; //not any number
                    int indexAnyCommand = code.IndexOfFirstMinistrListCI(commands, lastIndexOfWords + 1, ref curCmdNumber, ref lastIndexOfCmd);
                    int indexBegin = code.IndexOfEvenSingQuotMarksAndNothingBefore("begin ", lastIndexOfWords);

                    if (indexBegin < 0 || indexAnyCommand < indexBegin)
                    {
                        //the procedure, function or trigger has just one command
                        startIndex = lastIndexOfCmd + 1;
                    }
                    else
                        startIndex = this.lastIndexProc(code, indexBegin) + 1;
                }
                else
                    startIndex = lastIndexOfWords + 1;

                //if it's the first command, pick since the beginning of the string
                if (i == 0)
                {
                    iCommandBegins = index;
                    if (cmdNumber == iCommandExec)
                        code = code.Substring(0, index) + code.Substring(index + this.commands[iCommandExec].Length);
                }
                else
                if (cmdNumber == iCommandExec)
                {
                    iCommandBegins = startIndex;
                    cmdNumber = -1;
                }
                else
                    iCommandBegins = index;

                prevCmdNumber = cmdNumber;
            }

            return codes;
        }

        protected int lastIndexProc(string code, int indexBegin)
        {
            //the procedure, function or trigger has more than one command
            int lastIndexBeginEnd = indexBegin + 5; //index after first begin
            int qtdBegin = 1; //the first begin was already counted

            while (true)
            {
                int indexEnd = code.IndexOfEvenSingQuotMarksAndNothingBefore("end ", lastIndexBeginEnd);
                indexBegin = code.IndexOfEvenSingQuotMarksAndNothingBefore("begin ", lastIndexBeginEnd);

                //error: the user forgot some "end"s
                if (indexEnd < 0 && indexBegin < 0)
                    break;

                //indexBegin cannot be -1 (inexistent)
                if (indexBegin >= 0 && (indexBegin < indexEnd || indexEnd < 0))
                {
                    qtdBegin++;
                    lastIndexBeginEnd = indexBegin + 4; //after the "begin"
                }
                else
                {
                    qtdBegin--;
                    lastIndexBeginEnd = indexEnd + 3; //after the "end"

                    if (qtdBegin == 0)
                        break;
                }
            }

            return lastIndexBeginEnd;
        }


        //test conection
        protected bool IsConected()
        {
            try
            {
                //the best way of seen if the conection is on, is to make a simple select
                SqlCommand cmd = new SqlCommand("Select 1", this.con);
                int returnedValue = (Int32)cmd.ExecuteScalar();
                return returnedValue == 1;
            }catch(Exception e)
            {
                return false;
            }
        }


        //others
        public DataTable AllTables()
        {
            if (this.con == null)
                throw new Exception("No conection!");

            return this.con.GetSchema("Tables");
        }


        //required methods
        public override string ToString()
        {
            string ret = "{";
            ret += "Conection string: " + this.connStr;
            ret += "\n\rCommands: ";

            ret += "\n\rNon-Query: ";
            for (int iz = 0; iz <= this.iCommandExec; iz++)
                ret += this.commands[iz] + ", ";

            ret += "\n\rQuery: ";
            int i = this.iFirstQuery;
            for (; i < this.iFirstRealProc; i++)
                ret += this.commands[i] + (i==this.commands.Count?"":", ");

            if(i < this.commands.Count)
            {
                ret += "\n\rProcedures and Functions: ";
                for (int iz = i; iz < this.commands.Count; iz++)
                    ret += this.commands[iz] + (i==this.commands.Count?"":", ");
            }

            ret += "}";

            return ret;
        }

        public override int GetHashCode()
        {
            int ret = 1;
            if(this.connStr != null)
                ret = ret * 7 + this.connStr.GetHashCode();

            ret = ret * 7 + this.commands.GetHashCode();
            ret = ret * 7 + this.tables.GetHashCode();
            return ret;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;

            if (!(obj is MySqlConnection))
                return false;

            MySqlConnection mySqlCon = (MySqlConnection)obj;
            return (((this.connStr == null && mySqlCon.connStr == null) || this.connStr.Equals(mySqlCon.connStr))
                && this.commands.Equals(mySqlCon.commands) && this.tables.Equals(mySqlCon.tables));
            //every variable is condense in here because the "iCommands" are based on the List<string> commands
            //and "con" is based on "connStr"
        }

        public MySqlConnection(MySqlConnection sample)
        {
            if (sample == null)
                throw new Exception("Null sample!");

            this.ConnStr = sample.connStr;
        }

        public Object Clone()
        {
            return new MySqlConnection(this);
        }
    }
}
