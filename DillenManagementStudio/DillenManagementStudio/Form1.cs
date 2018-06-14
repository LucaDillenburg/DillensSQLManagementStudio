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

            //execute query or nonQuery depending on the radioButtons
            //if it's a query, show it in the DataGridView 
            //else try to show select of that whole table that was changed

            bool queryExecution;
            if(this.rdAutomatic.Checked)
            {
                int indexSelect = code.IndexOf("select", StringComparison.CurrentCultureIgnoreCase);
                queryExecution = (indexSelect >= 0) && (code.IndexOf("'", 0, indexSelect) < 0);

                if(!queryExecution)
                {
                    indexSelect = code.IndexOf("sp_help", StringComparison.CurrentCultureIgnoreCase);
                    queryExecution = (indexSelect >= 0) && (code.IndexOf("'", 0, indexSelect) < 0);
                }
            }else
            {
                queryExecution = this.rdSelect.Checked;
            }
            

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

            if (!String.IsNullOrWhiteSpace(code))
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmdQ = new SqlCommand(code, this.con);

                    SqlDataAdapter adapt = new SqlDataAdapter(cmdQ);

                    adapt.Fill(ds);

                    if(ds.Tables.Count < 0)
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

                for(int i = 0; i<ds.Tables[0].Columns.Count; i++)
                    table.Columns.Add(ds.Tables[0].Columns[i].ColumnName + " (" + ds.Tables[0].Columns[i].DataType.ToString() + ")", typeof(string));

                for (int ir = 0; ir < ds.Tables[0].Rows.Count; ir++)
                    table.Rows.Add(ds.Tables[0].Rows[ir].ItemArray);

                this.grvSelect.DataSource = table;

                //If the person asked to execute a Query, say if the Query execution was completed
                if (rdSelect.Checked)
                    MessageBox.Show("Selection completed!");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.con.Close();
        }
    }
}
