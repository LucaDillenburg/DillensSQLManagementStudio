using System.Collections.Generic;
using System;
//to work with the database
using System.Data.SqlClient;
//to use DataTable
using System.Data;
//to use regex
using System.Text.RegularExpressions;

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
        protected int iFirstRealFunc;
        //to do a select based on a non-query
        protected int iDropTable;
        protected int iCreateTrigger;
        protected int iAlterTrigger;
        //to add or remove procedures and functions from this.commands
        protected int iCreateProc;
        protected int iCreateProcedure;
        protected int iCreateFunc;
        protected int iDropProc;
        protected int iDropProcedure;
        protected int iDropFunc;
        //other
        protected int iSpHelp;
        //nonQuery to select
        protected List<string> nonQueryToSelect = new List<string>();

        //user: to get all commands
        protected User user;

        //special chars (to separe commands)
        protected List<char> specialChars = new List<char>();
        protected int iSingQuot;

        //reserved words
        protected List<string> reservedWords = new List<string>();
        
        public MySqlConnection(User us)
        {
            //user: to get all commands
            this.user = us;

            //Non Query To Select
            nonQueryToSelect.Add("insert into ");
            nonQueryToSelect.Add("update ");
            nonQueryToSelect.Add("alter table ");
            nonQueryToSelect.Add("create table ");
            nonQueryToSelect.Add("delete from ");


            //special chars
            specialChars.Add(' ');
            iSingQuot = specialChars.Count;
            specialChars.Add('\'');
            specialChars.Add('(');
            specialChars.Add(')');
            specialChars.Add('.');
            specialChars.Add(';');
            specialChars.Add(',');
            specialChars.Add('*');


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
                string strConn = value;
                if(!String.IsNullOrEmpty(value))
                    this.NewConnection(strConn);
            }
        }
        
        public List<string> ReservedWords
        {
            get
            {
                List<string> ret = new List<string>(this.reservedWords);
                //for (int i = this.iFirstProcFuncTrigger; i < this.commands.Count; i++)
                //    ret.Add(this.commands[i]);

                return ret;
            }
        }

        public List<char> SpecialChars
        {
            get
            {
                return new List<char>(specialChars);
            }
        }

        public int IndexSingQuot
        {
            get
            {
                return iSingQuot;
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
                //fazer?
                return new List<string>();
            }
        }


        //conection on/off
        public void CloseConnection()
        {
            this.con.Close();
        }

        protected void NewConnection(string strConn)
        {
            this.con = new SqlConnection();
            strConn = strConn.Substring(strConn.IndexOf("Data Source"));
            this.con.ConnectionString = strConn;
            this.con.Open();

            this.connStr = strConn;

            this.PutAllCommands();
        }

        protected void PutAllCommands()
        {
            //INICIALIZAR COMMANDS
            this.GetSqlCommandsFromDb();
            
            //REAL PROCEDURES AND FUNCTIONS
            //get all procedures and functions
            this.iFirstRealProc = this.commands.Count; //i BASIC

            if(this.con != null)
            {
                for(int ipf = 0; ipf < 2; ipf++)
                {
                    string code;
                    if (ipf == 0)
                        code = "SELECT NAME from SYS.PROCEDURES";
                    else
                    {
                        code = "SELECT name FROM sys.sql_modules m " +
                            "INNER JOIN sys.objects o ON m.object_id = o.object_id " +
                            "WHERE type_desc like '%function%'";

                        this.iFirstRealFunc = this.commands.Count; //i BASIC
                    }
                    
                    SqlCommand cmdQ = new SqlCommand(code, this.con);

                    SqlDataAdapter adapt = new SqlDataAdapter(cmdQ);

                    DataSet ds = new DataSet();
                    adapt.Fill(ds);

                    try
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string nameProc = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                            this.commands.Add(nameProc + " ");
                        }
                    }
                    catch(Exception e) { }
                }
            }
        }

        protected void GetSqlCommandsFromDb()
        {
            this.commands = this.user.SqlCommands;

            //NON-QUERIES
            this.iDropTable = this.commands.IndexOf("drop table ");
            this.iDropProc = this.commands.IndexOf("drop proc ");
            this.iDropProcedure = this.commands.IndexOf("drop procedure ");
            this.iDropFunc = this.commands.IndexOf("drop function ");

            //PROCEDURES, FUNCTIONS, TRIGGER (and view)
            this.iCreateProc = this.commands.IndexOf("create proc ");
            this.iFirstProcFuncTrigger = this.iCreateProc; //BASIC
            this.iCreateProcedure = this.commands.IndexOf("create procedure ");
            this.iCreateFunc = this.commands.IndexOf("create function ");
            this.iCreateTrigger = this.commands.IndexOf("create trigger ");
            this.iAlterTrigger = this.commands.IndexOf("alter trigger ");

            //DON'T PUT IT IN THE COMMANDS' QUEUE, BECAUSE WE DONT'T NEED IT
            this.iCommandExec = this.commands.IndexOf("exec "); //BASIC

            //QUERIES
            this.iFirstQuery = this.commands.IndexOf("sp_bindrule "); //BASIC
            this.iSpHelp = this.commands.IndexOf("sp_help ");
        }

        public void RestartCommands()
        {
            this.PutAllCommands();
        }
        

        //sql execute user commands
        public DataTable ExecuteAutomaticSqlCommands(string allCodes, ref Queue<Error> cmdErrors)
        {
            //replace multiple spaces with a single space where it's not an string
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);

            string auxAllCodes = "";
            bool isString = false;
            int startIndex = 0;
            
            while (startIndex < allCodes.Length)
            {
                int endIndex = allCodes.IndexOf('\'', startIndex + 1);
                if (endIndex < 0)
                    endIndex = allCodes.Length;

                string aux = allCodes.Substring(startIndex, endIndex - startIndex);
                if (isString)
                    auxAllCodes += aux;
                else
                    auxAllCodes += regex.Replace(aux, " ").ToLower();

                startIndex = endIndex;
                isString = !isString;
            }

            allCodes = auxAllCodes.Trim();

            //separate the whole code in a commands QUEUE
            Queue<int> nSQLCommands = new Queue<int>();
            int nQuery = -1;
            int nNonQuerySelect = -1;
            Queue<string> codes = this.TransformCodeInCommands(allCodes, ref nSQLCommands, ref nQuery, ref nNonQuerySelect);
            
            cmdErrors = new Queue<Error>();

            if (!this.IsConected())
            {
                //Say that the conection is over
                cmdErrors.Enqueue(new Error(-1, "", "", "Connection with database is down!", true));
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

                bool executeQuery = (nQuery == iCode //if it's the last select
                      || (nQuery < 0 && nNonQuerySelect == iCode)); //if it's the last non-query select and there's no queries

                bool worked = this.ExecuteOneSQLCmd(currCode, 
                    (cmdN >= this.iFirstQuery && cmdN < this.iFirstRealProc),
                    executeQuery,
                    cmdN, ref auxDtTable, ref excep);
                //returns true if it worked and false if it didn't work

                if (!worked)
                {
                    int cmdNError;
                    if (cmdN >= this.iFirstRealProc)
                    {
                        if (cmdN < this.iFirstRealFunc) //if it's a procedure
                            cmdNError = this.iFirstRealProc;
                        else //if it's a function
                            cmdNError = this.iFirstRealProc + 1;
                    }
                    else
                        cmdNError = cmdN;

                    Error curErr = new Error(cmdNError, cmdN>=0?this.commands[cmdN].Trim().ToUpper():"Not existent command", currCode, excep, false);
                    cmdErrors.Enqueue(curErr);
                }
                else
                {
                    //if procedure or function was dropped or created, respectively add and remove from this.commands (change iFirstRealFunc if necessary)
                    if (cmdN == iCreateProc || cmdN == iCreateProcedure || cmdN == iCreateFunc || cmdN == iDropProc || cmdN == iDropProcedure || cmdN == iDropFunc)
                        this.AddOrRemoveProfFunc(currCode, cmdN);

                    if (executeQuery)
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
                    int indAfterWord = code.IndexOf(specialChars, firstLetter + 1);
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
                    MySqlConnection.ConstructTable(ds, ref table);
            }

            return true;
        }

        public static DataTable DataTableFromDs(DataSet ds)
        {
            DataTable table = new DataTable();
            MySqlConnection.ConstructTable(ds, ref table);
            return table;
        }

        protected static void ConstructTable(DataSet ds, ref DataTable table)
        {
            //change number of rows and columns of the DataGridView
            //put the names of the columns                 
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                table.Columns.Add(ds.Tables[0].Columns[i].ColumnName + " (" + ds.Tables[0].Columns[i].DataType.ToString() + ")", ds.Tables[0].Columns[i].DataType == typeof(bool) ? typeof(string) : ds.Tables[0].Columns[i].DataType);

            for (int ir = 0; ir < ds.Tables[0].Rows.Count; ir++)
                table.Rows.Add(ds.Tables[0].Rows[ir].ItemArray);
        }

        protected bool CmdIsNonQueryToSelect(int iCommand)
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

        
        //transform allCodes in a commands Queue
        protected Queue<string> TransformCodeInCommands(string code, ref Queue<int> nSQLCommands, ref int nQuery, ref int nNonQuerySelect)
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

                //verificar
                if(codes.Count > 0 && cmdNumber >= this.iFirstRealFunc && prevCmdNumber == this.iSpHelp)
                    index = code.IndexOfFirstMinistrListCI(commands, index + 1, ref cmdNumber, ref lastIndexOfWords);

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
                    else
                    //if there ins't any query yet and it's a command that can be selected
                    if (nQuery < 0 && this.CmdIsNonQueryToSelect(prevCmdNumber))
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
                    if (nQuery < 0 && this.CmdIsNonQueryToSelect(prevCmdNumber))
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
                        startIndex = this.LastIndexProc(code, indexBegin) + 1;
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

        protected int LastIndexProc(string code, int indexBegin)
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


        //add or remove procedure or function from this.commands (change iFirstRealFunc if necessary)
        protected void AddOrRemoveProfFunc(string currCode, int cmdN)
        {
            bool create;
            bool isProc;
            string cmd;
            if (cmdN == iCreateProc)
            {
                cmd = "create proc";
                isProc = true;
                create = true;
            }
            else
            if (cmdN == iCreateProcedure)
            {
                cmd = "create procedure";
                isProc = true;
                create = true;
            }
            else
            if (cmdN == iCreateFunc)
            {
                cmd = "create function";
                isProc = false;
                create = true;
            }
            else
            if (cmdN == iDropProc)
            {
                cmd = "drop proc";
                isProc = true;
                create = false;
            }
            else
            if (cmdN == iDropProcedure)
            {
                cmd = "drop procedure";
                isProc = true;
                create = false;
            }
            else
            if (cmdN == iDropFunc)
            {
                cmd = "drop function";
                isProc = false;
                create = false;
            }
            else
                throw new Exception("Invalid command!");

            string procOrFuncName = currCode.FirstWord(cmd);

            //if user created a proc or function
            if(create)
            {
                if (isProc)
                {
                    this.commands.Insert(this.iFirstRealProc, procOrFuncName + " ");
                    this.iFirstRealFunc++;
                }
                else
                    this.commands.Add(procOrFuncName + " ");
            }else
            //if user droped a proc or function
            {
                this.commands.Remove(procOrFuncName + " ");

                if (isProc)
                    this.iFirstRealFunc--;
            }
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


        //some sql commands disponbles
        public DataTable AllTables()
        {
            if (this.con == null)
                throw new Exception("No conection!");

            return this.con.GetSchema("Tables");
        }

        public DataTable AllProcFunc()
        {
            DataTable ret = new DataTable();

            for (int i = this.iFirstRealProc; i < this.commands.Count; i++)
            {
                SqlCommand cmdQ = new SqlCommand("sp_help " + this.commands[i], this.con);

                SqlDataAdapter adapt = new SqlDataAdapter(cmdQ);

                DataSet ds = new DataSet();
                adapt.Fill(ds);
                
                if(i == this.iFirstRealProc)
                {
                    //change number of rows and columns of the DataGridView
                    //put the names of the columns                 
                    for (int ic = 0; ic < ds.Tables[0].Columns.Count; ic++)
                        ret.Columns.Add(ds.Tables[0].Columns[ic].ColumnName + " (" + ds.Tables[0].Columns[ic].DataType.ToString() + ")", 
                            ds.Tables[0].Columns[ic].DataType == typeof(bool) ? typeof(string) : ds.Tables[0].Columns[ic].DataType);
                }

                for (int ir = 0; ir < ds.Tables[0].Rows.Count; ir++)
                    ret.Rows.Add(ds.Tables[0].Rows[ir].ItemArray);
            }

            return ret;
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
                && this.commands.Equals(mySqlCon.commands));
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
