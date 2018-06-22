using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//para banco
using System.Data.SqlClient;

namespace DillenManagementStudio
{
    public partial class FrmDillenSQLManagementStudio : Form
    {
        protected const bool testandoSemInternet = true;

        //to separe commands
        protected List<string> commands = new List<string>();
        
        protected int iCommandExec;
        protected int iFirstProc;
        protected int firstQuery;

        //conection
        protected string connStr;
        protected SqlConnection con;
                

        //form methods
        public FrmDillenSQLManagementStudio()
=======
        public Form1()
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
        {
            InitializeComponent();

            //GET ALL PROCEDURES AND FUNCIONS
            int nProcFunc = 0;
//fazer
        
            //INICIALIZAR COMMANDS
            //NON-QUERIES
            this.commands.Add("insert%into ");
            this.commands.Add("drop%table ");
            this.commands.Add("alter%table ");
            this.commands.Add("delete%from ");
            this.commands.Add("update ");
            this.commands.Add("create%table ");
            this.commands.Add("create%rule ");
            this.commands.Add("drop%proc ");
            this.commands.Add("drop%procedure ");
            this.commands.Add("drop%function ");
            this.commands.Add("drop%trigger ");
            //PROCEDURES, FUNCTIONS, TRIGGER
            this.iFirstProc = this.commands.Count;
            this.commands.Add("create%proc ");
            this.commands.Add("create%procedure ");
            this.commands.Add("alter%proc ");
            this.commands.Add("alter%procedure ");
            this.commands.Add("create%function ");
            this.commands.Add("alter%function ");
            this.commands.Add("create%trigger ");
            //DON'T PUT IT IN THE COMMANDS' QUEUE, BECAUSE WE DONT'T NEED IT
            this.commands.Add("exec ");
            iCommandExec = this.commands.Count - 1;
            //QUERIES
            this.firstQuery = this.commands.Count;
            this.commands.Add("sp_bindrule ");
            this.commands.Add("sp_unbindrule ");
            this.commands.Add("select ");
            this.commands.Add("sp_help ");
            //for (int i = 0; i < nProcFunc; i++)
            //    this.commands.Add(procedures[i]);
//fazer
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cbxChsDtBs.SelectedIndex = 0;

            if(!testandoSemInternet)
                btnChangeDtBs.PerformClick();

            this.rchtxtCode.Focus();
        }

        private void btnChangeDtBs_Click(object sender, EventArgs e)
        {
            //stablish conection with the datebase
            switch (cbxChsDtBs.SelectedIndex)
            {
                case 0:
                    this.connStr = Properties.Settings.Default.BD17188ConnectionString;
                    break;
                case 1:
                    this.connStr = Properties.Settings.Default.BDPRII17188ConnectionString;
                    break;
                default:
                    throw new Exception("btnEscBanco_Click error due to new datebase!");
            }

            try
            {
                this.con = new SqlConnection();
                this.connStr = this.connStr.Substring(this.connStr.IndexOf("Data Source"));
                this.con.ConnectionString = this.connStr;
                this.con.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occurred when trying to connect to the database chosen! \n\rSee if you are with the Unicamp VPN and connected to the internet! \n\rRESTART THE PROGRAM!");
                this.Close();
            }

            MessageBox.Show("Database connected!");
        }

        private void rchtxtCode_TextChanged(object sender, EventArgs e)
        {
            //put different colors in the numbers and commands
        }

        private void rchtxtCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //put spaces when the user presses tab
            if (e.KeyCode == Keys.Tab)
            {
                MessageBox.Show("Working on it!");
                //if nothing is selected and shift is not pressed
                //put 3 spaces in from of the 

                //if something is selected
                //if shift isn't pressed
                //put 3 spaces in the beginning of each selected lines
                //else
                //remove maximum 3 of possible spaces in from of the selected lines

                //show the key was already managed
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            //put txtCode.Items in a String (with spaces between each line)
            String allCodes = "";
            for (int i = 0; i < this.rchtxtCode.Lines.Length; i++)
                allCodes += " " + this.rchtxtCode.Lines[i];
=======
            for (int i = 0; i < this.txtCode.Lines.Length; i++)
                allCodes += " " + this.txtCode.Lines[i];
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f

            if (rdNonQuery.Checked)
                this.executeEachSQLCmd(allCodes, false, true);
            else
            if (rdSelect.Checked)
                this.executeEachSQLCmd(allCodes, true, false);
            else
            {
                this.executeAutomaticSqlCommands(allCodes);

                //dequeue every repeated number

                //ask if the user wants to know how's the syntax of the command
            }
        }
        
        private void btnAllTables_Click(object sender, EventArgs e)
        {
            this.grvSelect.DataSource = this.allTablesFromDtBs();
        }
=======
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.con.Close();
            }
            catch (Exception err)
            { }
        }
        
=======

        private void btnAllTables_Click(object sender, EventArgs e)
        {
            this.grvSelect.DataSource = this.allTablesFromDtBs();
        }

>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f

        //class MySqlCommand extends SqlCommand
        private void executeAutomaticSqlCommands(String allCodes)
        {
            //separate the whole code in commands in a QUEUE
            Queue<int> nSQLCommands = new Queue<int>();
            Queue<string> codes = this.transformCodeInCommands(allCodes, ref nSQLCommands);
            //Dequeue()	Remove and retorns the first object of the Queue

            bool queryExists = false;
            Queue<int> clIsQuery = new Queue<int>(nSQLCommands);
            while (clIsQuery.Count > 0)
            {
                if(clIsQuery.Dequeue() >= 4)
                {
                    queryExists = true;
                    break;
                }
            }

            if(!testandoSemInternet)
            {
                Queue<int> cmdNumbersNotWorked = new Queue<int>();
                while (codes.Count > 0)
                {
                    int cmdN = nSQLCommands.Dequeue();
                    //execute SQL commands
                    bool worked = this.executeEachSQLCmd(codes.Dequeue(), cmdN >= 4, !queryExists && codes.Count == 0);
                    //returns true if it worked and false if it didn't work

                    if (!worked)
                        cmdNumbersNotWorked.Enqueue(cmdN);
                }
=======

            Queue<int> cmdNumbersNotWorked = new Queue<int>();
            while (codes.Count > 0)
            {
                int cmdN = nSQLCommands.Dequeue();
                //execute SQL commands
                bool worked = this.executeEachSQLCmd(codes.Dequeue(), cmdN >= 4, !queryExists && codes.Count == 0);
                //returns true if it worked and false if it didn't work

                if (!worked)
                    cmdNumbersNotWorked.Enqueue(cmdN);
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
            }
        }

        private bool executeEachSQLCmd(string code, bool queryExecution, bool executeQueryFromNonQuery)
        {
            //execute query or nonQuery depending on the radioButtons
            //if it's a query, show it in the DataGridView 
            //else try to show select of that whole table that was changed

            //returns true if it worked and false if it didn't work

            if (!queryExecution && !String.IsNullOrWhiteSpace(code))
            {
                try
                {
                    SqlCommand cmdNQ = new SqlCommand(code, this.con);

                    int iResult = cmdNQ.ExecuteNonQuery();

                    //Say if the non Query execution was completed
                    if (iResult <= 0)
                    {
                        MessageBox.Show("Non Query error! No exception raised...");
                        return false;
                    }
                    else
                    if(this.rdNonQuery.Checked)
                        MessageBox.Show("Non Query completed!");
                }
                catch (Exception err)
                {
                    MessageBox.Show("Non Query error: " + err);
                    return false;
                }

                if (executeQueryFromNonQuery)
                {
                    //do a select based on 
                    List<string> nonQueryCommands = new List<string>();
                    nonQueryCommands[0] = "insert%into"; //0
                    nonQueryCommands[1] = "alter%table"; //2
                    nonQueryCommands[2] = "create%table";        //3
                    nonQueryCommands[3] = "update";      //3

                    int cmdNumeber = -1;
                    int lastIndexOfAll = -1;
                    int notUsing = IndexOfSupreme(code, nonQueryCommands, 0, ref cmdNumeber, ref lastIndexOfAll);
=======
                    string[] nonQueryCommands = new string[4];
                    nonQueryCommands[0] = "insert%into"; //0
                    nonQueryCommands[1] = "drop%table";  //1
                    nonQueryCommands[2] = "alter%table"; //2
                    nonQueryCommands[3] = "update";      //3

                    int cmdNumeber = -1;
                    int lastIndexOfAll = IndexOfSupreme(code, nonQueryCommands, 0, ref cmdNumeber, false);
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f

                    int whereFirstLetter = -1;
                                //lastIndexOfAll is the first space
                    for (int i = lastIndexOfAll+1; i<code.Length; i++)
                        if (code[i] != ' ')
                        {
                            whereFirstLetter = i;
                            break;
                        }

                    int length = code.IndexOf(" ", whereFirstLetter) - whereFirstLetter;
                    String tableName = code.Substring(whereFirstLetter, length);
                    code = "Select top(100)* from " + tableName;
                }
            }

            //just execute it if it has something in the code and if it is a query execution or if it doesn't have any query in the whole code
            if (!String.IsNullOrWhiteSpace(code) &&
                queryExecution || executeQueryFromNonQuery)
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmdQ = new SqlCommand(code, this.con);

                    SqlDataAdapter adapt = new SqlDataAdapter(cmdQ);

                    adapt.Fill(ds);

                    if (ds.Tables.Count < 0)
                    {
                        DataTable tab = new DataTable();
                        this.grvSelect.DataSource = tab;
                        MessageBox.Show("Query error! No exception raised...");
                        return false;
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Query error: " + err);
                    return false;
                }

                //change number of rows and columns of the DataGridView
                //put the names of the columns
                DataTable table = new DataTable();

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    table.Columns.Add(ds.Tables[0].Columns[i].ColumnName + " (" + ds.Tables[0].Columns[i].DataType.ToString() + ")", ds.Tables[0].Columns[i].DataType==typeof(bool)?typeof(string): ds.Tables[0].Columns[i].DataType);

                for (int ir = 0; ir < ds.Tables[0].Rows.Count; ir++)
                    table.Rows.Add(ds.Tables[0].Rows[ir].ItemArray);

                this.grvSelect.DataSource = table;

                //If the person asked to execute a Query, say if the Query execution was completed
                if (rdSelect.Checked)
                    MessageBox.Show("Selection completed!");
            }

            return true;
        }

        private Queue<string> transformCodeInCommands(string code, ref Queue<int> nSQLCommands)
        {
            //separate the whole code in commands in a QUEUE
            Queue<string> codes = new Queue<string>();
            //Enqueue(Object) Add an object in the Queue       
            
=======
            //Enqueue(Object) Add an object in the Queue

            string[] commands = new string[6];
            commands[0] = "insert%into"; //0
            commands[1] = "drop%table";  //1
            commands[2] = "alter%table"; //2
            commands[3] = "update";      //3
            commands[4] = "select";      //4
            commands[5] = "sp_help";     //5

>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
            int startIndex = 0;
            int iCommandBegins = 0;
            int prevCmdNumber = -1; //not any number
            for(int i = 0; ; i++)
            {
                int cmdNumber = -1; //not any number

                int lastIndexOfWords = -1; //not any number
                int index = IndexOfSupreme(code, commands, startIndex, ref cmdNumber, ref lastIndexOfWords);
=======
                int cmdNumber = -1; //any number
                
                int index = IndexOfSupreme(code, commands, startIndex, ref cmdNumber, true);
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f

                if (index < 0)
                {
                    if (i == 0)
                        codes.Enqueue(code);
                    else
                        codes.Enqueue(code.Substring(iCommandBegins));

                    //if i==0, prevCmdNumber is already -1
                    nSQLCommands.Enqueue(prevCmdNumber);
=======
                    {
                        codes.Enqueue(code);
                        nSQLCommands.Enqueue(-1);
                    }else
                    {
                        if (i == 1)
                            codes.Enqueue(code);
                        else
                            codes.Enqueue(code.Substring(startIndex - 1));

                        nSQLCommands.Enqueue(prevCmdNumber);
                    }
                    
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
                    break;
                }
                
                string oneCommand = code.Substring(iCommandBegins, index - iCommandBegins);
                if(!String.IsNullOrWhiteSpace(oneCommand))
                {
                    codes.Enqueue(oneCommand);
                    nSQLCommands.Enqueue(prevCmdNumber);
                }

                if (cmdNumber >= iFirstProc && cmdNumber < iCommandExec)
                {
                    //colocar startIndex apos ultimo end
                    // ou depois do primeiro command
                    int lastIndexOfCmd = -1; //not any number
                    int indexAnyCommand = IndexOfSupreme(code, commands, lastIndexOfWords + 1, ref cmdNumber, ref lastIndexOfCmd);
                    int indexBegin = code.IndexOf("begin", lastIndexOfWords, StringComparison.CurrentCultureIgnoreCase);

                    if (indexBegin < 0 || indexAnyCommand < indexBegin)
                    {
                        //the procedure, function or trigger has just one command
                        startIndex = lastIndexOfCmd + 1;
                    } else
                        startIndex = this.lastIndexProc(code, indexBegin) + 1;
                } else
                    startIndex = lastIndexOfWords + 1;

                //if it's the first command, pick since the beginning of the string
                if (i == 0)
                {
                    iCommandBegins = 0;
                    if (cmdNumber == iCommandExec)
                        code = code.Substring(0, index) + code.Substring(index + this.commands[iCommandExec].Length);
                }
                else
                if (cmdNumber == iCommandExec)
                {
                    iCommandBegins = startIndex;
                    cmdNumber = -1;
=======
                    if(i == 1)
                        codes.Enqueue(code.Substring(0, index));
                    else
                           // -1 because it's adding 1 some lines below
                        codes.Enqueue(code.Substring(startIndex-1, index - startIndex));

                    nSQLCommands.Enqueue(prevCmdNumber);
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
                }
                else
                    iCommandBegins = index;

                startIndex = index+1;

                prevCmdNumber = cmdNumber;
            }
            
            return codes;
        }

        private int lastIndexProc(string code, int indexBegin)
        {
            //the procedure, function or trigger has more than one command
            int lastIndexBeginEnd = indexBegin+5; //index after first begin
            int qtdBegin = 1; //the first begin was already counted

            while (true)
            {
                int indexEnd = code.IndexOf("end", lastIndexBeginEnd, StringComparison.CurrentCultureIgnoreCase);
                indexBegin = code.IndexOf("begin", lastIndexBeginEnd, StringComparison.CurrentCultureIgnoreCase);

                //error: the user forgot some "end"s
                if (indexEnd < 0 && indexBegin < 0)
                    break;

                //indexBegin cannot be -1 (inexistent)
                if (indexBegin >= 0 && indexBegin < indexEnd)
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

        private DataTable allTablesFromDtBs()
        {
            return this.con.GetSchema("Tables");
        }


        //class MyString extends String
        private int IndexOfSupreme(string str, List<String> ministrs, int startIndex, ref int iArrayNumber, ref int lastLetter)
        {
            //function will return the index of the first letter of the string that was been search
                //example: " hi, how are you? ".IndexOfSupreme("how%are") => 5
                //          012345678901234567
            //lastLetter: it will return the index of the last letter of the string
                //example: " hi, how are you? ".IndexOfSupreme("how%are", lastLetter) => lastLetter = 12
                //          012345678901234567
            
            int ret = str.Length;
            iArrayNumber = -1;

            for (int i = 0; i< ministrs.Count; i++)
=======
        private DataTable allTablesFromDtBs()
        {
            return this.con.GetSchema("Tables");
        }

        //class MyString extends String
        private int IndexOfSupreme(string str, string[] ministrs, int startIndex, ref int iArrayNumber, bool getFirstLetter)
        {
            //bool getFirstLetter: 
            //if it's true, it will return the index of the first letter of the string that was been search
                //example: " hi, how are you? ".IndexOfSupreme("how%are", true) => 5
                //          012345678901234567
            //if it's false, it will return the index of the last letter of the string
                //example: " hi, how are you? ".IndexOfSupreme("how%are", true) => 12
                //          012345678901234567

            int ret = str.Length;
            iArrayNumber = -1;

            for (int i = 0; i< ministrs.Length; i++)
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
            {
                int currentIndex;
                int currentLastIndex = -1;

                int indexPerc = ministrs[i].IndexOf("%");
                if (indexPerc < 0)
                {
                    currentIndex = indexOfEvenSingQuotMarks(str, ministrs[i], startIndex);
                    if (currentIndex >= 0)
                        currentLastIndex = currentIndex + ministrs[i].Length;
                }
                else
                    currentIndex = indexOfWithNWhiteSpaces(str, ministrs[i].Substring(0, indexPerc), ministrs[i].Substring(indexPerc+1), startIndex, ref currentLastIndex);
=======
                    if (currentIndex >= 0 && !getFirstLetter)
                        currentIndex += ministrs[i].Length;
                }
                else
                    currentIndex = indexOfWithNWhiteSpaces(str, ministrs[i].Substring(0, indexPerc), ministrs[i].Substring(indexPerc+1), startIndex, getFirstLetter);
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
                
                if (currentIndex >= 0 && currentIndex < ret)
                {
                    ret = currentIndex;
                    lastLetter = currentLastIndex;
=======
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
                    iArrayNumber = i;
                }
            }

            if (ret == str.Length)
                return -1;
            return ret;
        }

        private int indexOfEvenSingQuotMarks(string str, string ministr, int startIndex)
        {
            int ret = str.IndexOf(ministr, startIndex, StringComparison.CurrentCultureIgnoreCase);
            
            int currentIndex = startIndex-1;
            int qtdSingQuotMarks = 0;
            while(true)
            {
                currentIndex = str.IndexOf("'", currentIndex+1);
                if (currentIndex >= 0 && currentIndex < ret)
                    qtdSingQuotMarks++;
                else
                    break;
            }

            if (qtdSingQuotMarks%2 != 0)
                return -1;
            return ret;
        }
        
        private int indexOfWithNWhiteSpaces(string str, string ministr1, string ministr2, int startIndex, ref int lastLetter)
        {
=======
        {
            int ret = str.IndexOf(ministr, startIndex, StringComparison.CurrentCultureIgnoreCase);
            
            int currentIndex = startIndex-1;
            int qtdSingQuotMarks = 0;
            while(true)
            {
                currentIndex = str.IndexOf("'", currentIndex+1);
                if (currentIndex >= 0 && currentIndex < ret)
                    qtdSingQuotMarks++;
                else
                    break;
            }

            if (qtdSingQuotMarks%2 != 0)
                return -1;
            return ret;
        }
        
        private int indexOfWithNWhiteSpaces(string str, string ministr1, string ministr2, int startIndex, bool getFirstLetter)
        {
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
            //str.indexOf(ministr1%ministr2);

            int indexOf1 = indexOfEvenSingQuotMarks(str, ministr1, startIndex);
            
            if (indexOf1 < 0)
                return indexOf1;

            int indexOf2 = str.IndexOf(ministr2, indexOf1 + ministr1.Length + 1, StringComparison.CurrentCultureIgnoreCase);

            if(indexOf2 < 0)
                return indexOf2;

            for (int i = indexOf1 + ministr1.Length; i < indexOf2; i++)
                if (str[i] != ' ')
                    return -1;

            lastLetter = indexOf2 + ministr2.Length;
            return indexOf1;
            
=======
            if(getFirstLetter)
                return indexOf1;
            return indexOf2 + ministr2.Length;
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
        }
        
        //not using
        private int indexOfStringArray(string str, string[] strArray, int startIndex)
        {
            int ret = -1;

            foreach(string searchStr in strArray)
            {
                int indexStr = str.IndexOf(searchStr, startIndex, StringComparison.CurrentCultureIgnoreCase);
                if (indexStr < ret)
                    ret = indexStr;
            }

            return ret;
        }

        private int indexOfStringArrayWithNWhiteSpaces(string str, string[] ministr1, string[] ministr2, int startIndex)
        {
            int ret = -1;

            for (int i = 0; i < ministr1.Length; i++)
            {
                int indexOf1 = str.IndexOf(ministr1[i], startIndex, StringComparison.CurrentCultureIgnoreCase);

                if (indexOf1 >= 0)
                {
                    int indexOf2 = str.IndexOf(ministr2[i], indexOf1 + ministr1.Length + 2, StringComparison.CurrentCultureIgnoreCase);

                    for (int iAux = indexOf1; iAux < indexOf2; iAux++)
                        if (str[iAux] != ' ')
                        {
                            indexOf2 = -1;
                            break;
                        }

                    if (indexOf2 >= 0 && indexOf1 < ret)
                        ret = indexOf1;
                }
            }

            return ret;
        }

        
=======
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
    }
}

//classes:
// 1. MyString (not static, extends String + metodos)
// 2. SqlCommands (not static, extends SqlCommands)
>>>>>>> 4708feee2444a4c8704395b6251ee5d0ef7aa52f
