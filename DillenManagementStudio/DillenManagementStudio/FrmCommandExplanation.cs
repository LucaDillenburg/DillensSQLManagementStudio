using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DillenManagementStudio
{
    public partial class FrmCommandExplanation : Form
    {
        //stage
        protected int stage = 0;

        //sql memory
        protected string command;
        protected List<string> titles;
        protected List<string> cmdExplanations;
        protected List<string> textsUserTry;

        //conection
        protected MySqlConnection mySqlConn;
        protected SqlConnection con;
        protected string tableName;

        //auxiliary
        protected const int QTD_MAX_CHARS_PER_LINE = 65;

        //rich text box
        protected SqlRichTextBox sqlRchtxtbx;


        //INICIALIZE
        public FrmCommandExplanation(int codCmd, User user, MySqlConnection mySqlCon)
        {
            InitializeComponent();

            this.mySqlConn = mySqlCon;

            //name
            this.command = user.CommandFromCod(codCmd);
            this.Text = new CultureInfo("en-US", false).TextInfo.ToTitleCase(this.command);
            //fisrt letters of all words in upper case

            //explanation
            user.GetExplanationSqlCommand(codCmd, ref this.titles, ref this.cmdExplanations,
                ref this.textsUserTry);

            //INICIALIZE RICHTEXT BOX
            this.sqlRchtxtbx = new SqlRichTextBox(ref this.rchtxtTryCode, this, mySqlCon, true);
        }

        private void FrmCommandExplanation_Shown(object sender, EventArgs e)
        {
            //put strings in the right way
            this.ManageStrings();

            this.ShowCurrExplanationStage();

            this.CreateTableToUserTry();
        }

        protected void ManageStrings()
        {
            //break string into phrases
            for (int i = 0; i < this.cmdExplanations.Count; i++)
                this.cmdExplanations[i] = this.cmdExplanations[i].BreakWords(QTD_MAX_CHARS_PER_LINE);

            //put color the label and erase marks


            //change [tableName] and [valueX] to real values
            for(int ib = 0; ib<this.textsUserTry.Count; ib++)
            {
                //[tableName]
                this.textsUserTry[ib] = this.textsUserTry[ib].Replace("[tableName]", this.tableName);

                //[valueX]
                for(int im = 1; im<4; im++)
                {
                    string value;
                    switch (im)
                    {
                        case 1:
                            value = "20";
                            break;
                        case 2:
                            value = "'Jack'";
                            break;
                        case 3:
                            value = "'Loves to hear violin in the sea.'";
                            break;
                        default:
                            value = "ERROR";
                            break;
                    }

                    this.textsUserTry[ib] = this.textsUserTry[ib].Replace("[value" + im + "]", value);
                }
            }
        }

        protected void CreateTableToUserTry()
        {
            this.con = new SqlConnection();
            this.con.ConnectionString = this.mySqlConn.ConnStr;
            this.con.Open();

            //get available table name
            this.SetNewTableName();

            //create table
            string code = "create table " + this.tableName + "(" +
                "id int primary key, name varchar(30) not null, description text" +
                ")";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.ExecuteNonQuery();

            //insert some values in the table
            code = "insert into " + this.tableName + " values(1, 'James', 'He has a gun and an Aston Martin.') " +
                "insert into " + this.tableName + " values(2, 'James', 'He has a gun and a very nice car.') " +
                "insert into " + this.tableName + " values(3, 'Indiana', 'His best friend is a whip and he loves adventure.') " +
                "insert into " + this.tableName + " values(4, 'Darth', 'His clothes are all black and he uses a mask.') " +
                "insert into " + this.tableName + " values(5, 'Harry', 'Loves magic.') " +
                "insert into " + this.tableName + " values(6, 'Katniss', 'Hunts people using archery.') " +
                "insert into " + this.tableName + "(id, name) values(7, 'Luke') " +
                "insert into " + this.tableName + " values(8, 'Rocky', 'Punches meat.') " +
                "insert into " + this.tableName + " values(9, 'Jules', 'Vegetarian but loves the taste of a real cheeseburger, also likes blood.') " +
                "insert into " + this.tableName + " values(10, 'Forest', 'Runs.') " +
                "insert into " + this.tableName + " values(11, 'Jack', 'Travels in a big boat and he has a stylish hair.') " +
                "insert into " + this.tableName + " values(12, 'John', 'He is usually calm, but when people steal his mustang and kill his dog, he likes to use pistols.') ";
            cmd = new SqlCommand(code, con);
            cmd.ExecuteNonQuery();
            
            this.btnHelp.PerformClick();
        }

        protected void SetNewTableName()
        {
            string fisrtTableName = "test";
            string tableName = fisrtTableName;
            int i = 1;
            while(this.TableExists(tableName, this.con))
            {
                tableName = fisrtTableName + i;
                i++;
            }

            this.tableName = tableName;
        }

        protected bool TableExists(string tableName, SqlConnection con)
        {
            string code = "select * from " + tableName;
            SqlCommand cmd = new SqlCommand(code, con);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                adapt.Fill(ds);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }


        //Show Stage
        protected void ShowCurrExplanationStage()
        {
            //title
            this.lbTitle.Text = this.titles[this.stage];
            this.CentralizeTitle();

            //cmd explanation
            this.lbExplanation.Text = this.cmdExplanations[this.stage];

            //try text
            this.rchtxtTryCode.Text = this.textsUserTry[this.stage];

            //buttons
            if(this.stage == 0)
                this.btnPrevious.Visible = false;
            else
                this.btnPrevious.Visible = true;
            if (this.stage >= this.titles.Count - 1)
                this.btnNext.Visible = false;
            else
                this.btnNext.Visible = true;
        }

        protected void CentralizeTitle()
        {
            int x = (this.Width - this.lbTitle.Width)/2;
            this.lbTitle.Location = new Point(x, this.lbTitle.Location.Y);
        }


        //next/previous buttons
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.stage++;
            this.ShowCurrExplanationStage();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.stage--;
            this.ShowCurrExplanationStage();
        }


        //btnHelp
        private void btnHelp_Click(object sender, EventArgs e)
        {
            string code = "select * from " + this.tableName;
            DataTable table = new DataTable();
            string excep = null;
            this.mySqlConn.ExecuteOneSQLCmd(code, true, ref table, ref excep);

            this.grvSelectTry.DataSource = table;

            MessageBox.Show("Hi! I created a table so you can practice what I will teach you!\n\r\n\r" +
                "The table's name is " + this.tableName + " and you can see its fields in the selection below...");
        }


        //Form Closed
        private void FrmCommandExplanation_FormClosed(object sender, FormClosedEventArgs e)
        {
            //drop table
            string code = "drop table " + this.tableName;
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.ExecuteNonQuery();
        }

    }
}
