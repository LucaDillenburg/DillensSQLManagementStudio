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
    public partial class Form1 : Form
    {
        protected string connStr;
        protected SqlConnection con;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cbxChsDtBs.SelectedIndex = 0;

            btnChangeDtBs.PerformClick();
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

        private void btnExecute_Click(object sender, EventArgs e)
        {
            //put txtCode.Items in a String (with spaces between each line)
            String code = "";
            for (int i = 0; i < this.txtCode.Lines.Length; i++)
                code += " " + this.txtCode.Lines[i];

            if (rdNonQuery.Checked)
                this.executeSQL(code, false, true);
            else
            if (rdSelect.Checked)
                this.executeSQL(code, true, false);
            else
            {
                //separate the whole code in commands in a QUEUE
                Queue<int> codeIsQuery = new Queue<int>();
                Queue<string> codes = this.transformCodeInCommands(code, ref codeIsQuery);
                //Dequeue()	Remove and retorns the first object of the Queue

                MessageBox.Show("depois");

                bool queryExists = false;
                Queue<int> clIsQuery = new Queue<int>(codeIsQuery);
                while (clIsQuery.Count > 0)
                {
                    if(codeIsQuery.Dequeue() >= 4)
                    {
                        queryExists = true;
                        break;
                    }
                }

                while (codes.Count > 0)
                    //execute SQL commands
                    this.executeSQL(codes.Dequeue(), codeIsQuery.Dequeue() >= 4, !queryExists && codes.Count==0);
            }
        }

        private void executeSQL(string code, bool queryExecution, bool executeQueryFromNonQuery)
        {
            //execute query or nonQuery depending on the radioButtons
            //if it's a query, show it in the DataGridView 
            //else try to show select of that whole table that was changed

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
                        return;
                    }
                    else
                        MessageBox.Show("Non Query completed!");
                }
                catch (Exception err)
                {
                    MessageBox.Show("Non Query error: " + err);
                    return;
                }

                //transform code in the code to see all the top 50 of a table
                int lastIndexOfInsert = code.LastIndexOf("Insert", StringComparison.CurrentCultureIgnoreCase); //not case sensitive
                int lastIndexOfUpdate = code.LastIndexOf("Update", StringComparison.CurrentCultureIgnoreCase); //not case sensitive

                //Select from UPDATED table or INSERTED table
                int lastIndexOfAll;

                if (lastIndexOfInsert > lastIndexOfUpdate)
                    lastIndexOfAll = code.IndexOf("into", lastIndexOfInsert, StringComparison.CurrentCultureIgnoreCase) + 4;
                else
                    lastIndexOfAll = lastIndexOfUpdate + 6;

                if (lastIndexOfAll < 0)
                    return;

                int whereFirstLetter = -1;
                for (int i = lastIndexOfAll; ; i++)
                    if (code[i] != ' ')
                    {
                        whereFirstLetter = i;
                        break;
                    }

                int length = code.IndexOf(" ", whereFirstLetter) - whereFirstLetter;
                String tableName = code.Substring(whereFirstLetter, length);
                code = "Select top(100)* from " + tableName;
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
                        MessageBox.Show("Non Query error! No exception raised...");
                        return;
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Non Query error: " + err);
                    return;
                }

                //change number of rows and columns of the DataGridView
                //put the names of the columns
                DataTable table = new DataTable();

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    table.Columns.Add(ds.Tables[0].Columns[i].ColumnName + " (" + ds.Tables[0].Columns[i].DataType.ToString() + ")", typeof(string));

                for (int ir = 0; ir < ds.Tables[0].Rows.Count; ir++)
                    table.Rows.Add(ds.Tables[0].Rows[ir].ItemArray);

                this.grvSelect.DataSource = table;

                //If the person asked to execute a Query, say if the Query execution was completed
                if (rdSelect.Checked)
                    MessageBox.Show("Selection completed!");
            }
        }

        private Queue<string> transformCodeInCommands(string code, ref Queue<int> codeIsQuery)
        {
            //separate the whole code in commands in a QUEUE
            Queue<string> codes = new Queue<string>();
            //Enqueue(Object) Add an object in the Queue

            string[] commands = new string[6];
            commands[0] = "insert%into"; //0
            commands[1] = "drop%table";  //1
            commands[2] = "alter%table"; //2
            commands[3] = "update";      //3
            commands[4] = "select";      //4
            commands[5] = "sp_help";     //5
            
            int startIndex = 0;
            int prevCmdNumber = -1; //any number
            for(int i = 0; ; i++)
            {
                int cmdNumber = -1; //any number
                
                int index = IndexOfSupreme(code, commands, startIndex, ref cmdNumber);
                MessageBox.Show(index+"");

                if (index < 0)
                {
                    codes.Enqueue(code.Substring(startIndex));
                    codeIsQuery.Enqueue(prevCmdNumber);
                    break;
                }

                if(i > 0)
                {
                    codes.Enqueue(code.Substring(startIndex, index));
                    codeIsQuery.Enqueue(prevCmdNumber);
                    startIndex = index;
                }

                prevCmdNumber = cmdNumber;
            }
            
            return codes;
        }

        private int IndexOfSupreme(string code, string[] commands, int startIndex, ref int cmdNumber)
        {
            int ret = code.Length;
            cmdNumber = -1;

            for (int i = 0; i<commands.Length; i++)
            {
                int currentIndex;

                int indexPerc = commands[i].IndexOf("%");
                if (indexPerc < 0)
                    currentIndex = code.IndexOf(commands[i], startIndex);
                else
                    currentIndex = indexOfWithNWhiteSpaces(code, commands[i].Substring(0, indexPerc), commands[i].Substring(indexPerc+1), startIndex);
                
                if (currentIndex >= 0 && currentIndex < ret)
                {
                    ret = currentIndex;
                    cmdNumber = i;
                }
            }

            if (ret == code.Length)
                return -1;
            return ret;
        }

        //str.indexOf(ministr1%ministr2);
        private int indexOfWithNWhiteSpaces(string str, string ministr1, string ministr2, int startIndex)
        {
            int indexOf1 = str.IndexOf(ministr1, startIndex, StringComparison.CurrentCultureIgnoreCase);
            
            if (indexOf1 < 0)
                return indexOf1;

            int indexOf2 = str.IndexOf(ministr2, indexOf1+ministr1.Length+2, StringComparison.CurrentCultureIgnoreCase);

            if(indexOf2 < 0)
                return indexOf2;

            for (int i = indexOf1; i < indexOf2; i++)
                if (str[i] != ' ')
                    return -1;

            return indexOf1;
        }

        private int indexOfStringArrayWithNWhiteSpaces(string str, string[] ministr1, string[] ministr2, int startIndex)
        {
            int ret = -1;

            for(int i = 0; i<ministr1.Length; i++)
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

        private int indexOfStringArray(string str, string[] strArray, int startIndex)
        {
            int ret = -1;

            foreach(string searchStr in strArray)
            {
                int indexStr = str.IndexOf(searchStr, startIndex);
                if (indexStr < ret)
                    ret = indexStr;
            }

            return ret;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.con.Close();
        }
    }
}
