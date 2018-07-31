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

        //main atributes
        protected string command;
        protected int codCmd;

        //sql memory
        protected List<string> titles;
        protected List<string> cmdExplanations;
        protected List<string> textsUserTry;

        //conection
        protected MySqlConnection mySqlConn;
        protected SqlConnection con;
        protected string tableName;

        //rich text box
        protected SqlRichTextBox sqlRchtxtbx;

        //multicolor label
        protected MulticolorLabel multicolorLabel;

        protected const int FORM_MORE_WIDTH_PIXELS = 17;
        protected const int FORM_MORE_HEIGHT_PIXELS = 38;


        //INICIALIZE
        public FrmCommandExplanation(int codCmd, User user, MySqlConnection mySqlCon)
        {
            InitializeComponent();
            //set font to TableName's Cell in grvSelect
            this.grvSelectTry.TopLeftHeaderCell.Style.Font = new Font(new FontFamily("Modern No. 20"), 10.0F, FontStyle.Bold);

            if (!user.IsConnected())
                throw new Exception("Unicamp VPN was disconnected! This resource is not available anymore!");

            this.mySqlConn = mySqlCon;
            this.codCmd = codCmd;

            //name
            this.command = new CultureInfo("en-US", false).TextInfo.ToTitleCase(user.CommandFromCod(codCmd));
            //fisrt letters of all words in upper case
            this.Text = this.command;

            //explanation
            user.GetExplanationSqlCommand(codCmd, ref this.titles, ref this.cmdExplanations,
                ref this.textsUserTry);

            //INICIALIZE RICHTEXT BOX
            this.sqlRchtxtbx = new SqlRichTextBox(ref this.rchtxtTryCode, this, mySqlCon);

            //Inicialize MulticolorLabel
            this.multicolorLabel = new MulticolorLabel(new Point(0, 0), 0, 0, this, new Font(new FontFamily("Courier New"), 12.0F),
                Color.Black, SystemColors.Control);
        }

        protected void FrmCommandExplanation_Shown(object sender, EventArgs e)
        {
            this.CreateTableToUserTry();

            //put strings in the right way
            this.ManageStrings();

            this.ShowCurrExplanationStage(false);
            FrmCommandExplanation_Resize(null, null);

            this.btnHelp.PerformClick();
        }

        protected void ManageStrings()
        {
            /*
            //not needed because MulticolorLabel does that automatically
            break string into phrases
            for (int i = 0; i < this.cmdExplanations.Count; i++)
                this.cmdExplanations[i] = this.cmdExplanations[i].BreakWords(QTD_MAX_CHARS_PER_LINE);
            */

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

        
        //AJUST
        protected void FrmCommandExplanation_Resize(object sender, EventArgs e)
        {
            //title
            this.CentralizeTitle();
            this.pnlTitle.Width = this.Width;

            //explanation
            this.AjustMulticolorLabel();
            this.PutCmdExplanationInLabel();

            //bottom buttons
            this.AjustNextPrevButtons();

            //try area
            this.AjustRichTxtBxArea();
            this.AjustSelectionArea();
            this.AjustExecute();
        }

        protected void AjustMulticolorLabel()
        {
            int x = (int)Math.Round(0.012*(this.Width-FORM_MORE_WIDTH_PIXELS)); //1.2%
            int y = 59;
            this.multicolorLabel.X = x;
            this.multicolorLabel.Y = y;
            this.multicolorLabel.Width = this.Width - 2 * x - FORM_MORE_WIDTH_PIXELS;
            this.multicolorLabel.Height = this.rchtxtAux.Location.Y - y -
                (int)Math.Round(0.012 * (this.Width - FORM_MORE_HEIGHT_PIXELS));
        }

        protected void AjustNextPrevButtons()
        {
            this.btnPrevious.Location = new Point(12, this.Height - 74);
            this.btnNext.Location = new Point(this.Width - 91, this.Height - 74);
        }

        protected void AjustRichTxtBxArea()
        {
            //rchtxtTryCode
            this.rchtxtTryCode.Width = (int)Math.Round(0.38 * (this.Width - FORM_MORE_WIDTH_PIXELS)); //38%
            this.rchtxtTryCode.Height = (int)Math.Round(0.41 * (this.Height - FORM_MORE_HEIGHT_PIXELS)); //41%
            int x = (int)Math.Round(0.03 * (this.Width - FORM_MORE_WIDTH_PIXELS)); //3%
            int y = (int)Math.Round(0.50 * (this.Height - FORM_MORE_HEIGHT_PIXELS)); //50%
            this.rchtxtTryCode.Location = new Point(x, y);

            //rchtxtAux
            this.rchtxtAux.Width = this.rchtxtTryCode.Width;
            this.rchtxtAux.Height = this.rchtxtTryCode.Height;
            this.rchtxtAux.Location = this.rchtxtTryCode.Location;

            //picLoading
            this.picLoading.Width = (int)Math.Round(0.08 * (this.Width - FORM_MORE_WIDTH_PIXELS)); //8%
            this.picLoading.Height = this.picLoading.Width; //
            x = (this.rchtxtTryCode.Width - this.picLoading.Width) / 2 + this.rchtxtTryCode.Location.X;
            y = (this.rchtxtTryCode.Height - this.picLoading.Height) / 2 + this.rchtxtTryCode.Location.Y;
            this.picLoading.Location = new Point(x, y);

            //btnHelp
            this.btnHelp.Location = new Point(this.rchtxtTryCode.Location.X + this.rchtxtTryCode.Width + 5,
                this.rchtxtTryCode.Location.Y);
        }

        protected void AjustSelectionArea()
        {
            this.grvSelectTry.Width = (int)Math.Round(0.44 * (this.Width - FORM_MORE_WIDTH_PIXELS)); //44%
            this.grvSelectTry.Height = (int)Math.Round(0.41 * (this.Height - FORM_MORE_HEIGHT_PIXELS))
                - this.lbExecutionResult.Height - 3; //42%

            int x = (int)Math.Round(0.96 * (this.Width - FORM_MORE_WIDTH_PIXELS))/*96%*/
                - this.grvSelectTry.Width;
            this.lbExecutionResult.Location = new Point(x, this.rchtxtTryCode.Location.Y);
            this.grvSelectTry.Location = new Point(this.lbExecutionResult.Location.X,
                this.lbExecutionResult.Location.Y + this.lbExecutionResult.Height + 3);
        }

        protected void AjustExecute()
        {
            this.btnExecute.Width = (int)Math.Round(0.094 * (this.Width - FORM_MORE_WIDTH_PIXELS)); //9.4
            this.btnExecute.Height = (int)Math.Round(0.088 * (this.Height - FORM_MORE_HEIGHT_PIXELS)); //8.8

            int y = (this.rchtxtTryCode.Height - this.btnExecute.Height) / 2 + this.rchtxtTryCode.Location.Y;
            int x = (this.grvSelectTry.Location.X - this.rchtxtTryCode.Location.X - this.rchtxtTryCode.Width - this.btnExecute.Width) / 2 
                + this.rchtxtTryCode.Location.X + this.rchtxtTryCode.Width;
            this.btnExecute.Location = new Point(x, y);
        }


        //Show Stage
        protected void ShowCurrExplanationStage(bool putCmdExplanationInLabel)
        {
            //title
            this.lbTitle.Text = this.titles[this.stage];
            this.CentralizeTitle();

            //cmd explanation
            //put color in the label and erase marks
            if(putCmdExplanationInLabel)
                this.PutCmdExplanationInLabel();
            //this.lbExplanation.Text = this.cmdExplanations[this.stage];

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

        protected void PutCmdExplanationInLabel()
        {
            this.multicolorLabel.Clear();

            string text = this.cmdExplanations[this.stage];

            //put Colorful, Bold, Italic and normal text in Label
            int startIndex = 0;
            while(startIndex < text.Length)
            {
                int beginSpecialText = text.IndexOf("<<", startIndex);

                //put text until "<<"
                this.multicolorLabel.Append(text.Substring(startIndex, 
                    (beginSpecialText<0?text.Length:beginSpecialText) - startIndex));

                if (beginSpecialText < 0)
                    break;

                //put text after "<<" until ">>"
                int endSpecialText = text.IndexOf(">>", beginSpecialText);
                if(endSpecialText < 0)
                {
                    this.multicolorLabel.Append(text.Substring(beginSpecialText));
                    break;
                }
                else
                {
                    Color color = this.multicolorLabel.DefaultTextColor;
                    Font font = this.multicolorLabel.DefaultFont;
                    this.ColorOrFontFromLetter(text[beginSpecialText+2], ref color, ref font);

                    int start = beginSpecialText + 3;
                    string curText = text.Substring(start, endSpecialText - start);
                    this.multicolorLabel.Append(curText, color, font);
                }

                startIndex = endSpecialText + 2;
            }
        }

        protected void ColorOrFontFromLetter(char c, ref Color color, ref Font font)
        {
            /*
            __ Explanation __
              - <<*bold>>
              - <<_underlined>>
              - <<bBLUE TEXT>>
              - <<rRED TEXT>>
            */

            switch (c)
            {
                case '*':
                    font = new Font(font, FontStyle.Bold);
                    break;
                case '_':
                    font = new Font(font, FontStyle.Italic);
                    break;
                case 'b':
                    color = Color.Blue;
                    break;
                case 'r':
                    color = Color.Red;
                    break;
            }
        }

        protected void CentralizeTitle()
        {
            int x = (this.Width - this.lbTitle.Width)/2;
            this.lbTitle.Location = new Point(x, this.lbTitle.Location.Y);
        }


        //next/previous buttons
        protected void btnNext_Click(object sender, EventArgs e)
        {
            this.stage++;
            this.ShowCurrExplanationStage(true);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            this.stage--;
            this.ShowCurrExplanationStage(true);
        }


        // EXECUTE
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            //get allCodes or lines without even quotation marks
            Queue<int> linesNoEvenQuotMarks = new Queue<int>();
            string allCodes = SqlExecuteProcedures.AllCodes(this.sqlRchtxtbx, ref linesNoEvenQuotMarks);

            if (linesNoEvenQuotMarks.Count > 0)
            {
                string msg = SqlExecuteProcedures.MessageFromNoEvenQuotMarks(linesNoEvenQuotMarks);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //1. See if user is practicing the right command
                Queue<int> commandsCod = this.mySqlConn.GetCodCommandsFromCode(allCodes);
                bool onlyTryingCmd = true;
                while (commandsCod.Count > 0)
                    if (commandsCod.Dequeue() != this.codCmd)
                    {
                        onlyTryingCmd = false;
                        break;
                    }

                if (!onlyTryingCmd)
                {
                    this.grvSelectTry.DataSource = new DataTable();
                    MessageBox.Show("This area is for you to practice '" + this.command.Trim().ToUpper() + "'command ! Use the main form to make other commands!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //2. Execute the command
                    DataTable dataTable = new DataTable();
                    string excep = null;
                    int qtdLinesChanged = 0;
                    bool worked = this.mySqlConn.ExecuteOneSQLCmd(allCodes, this.mySqlConn.CommandIsQuery(this.codCmd), ref dataTable, ref excep, ref qtdLinesChanged);
                    
                    //change label
                    SqlExecuteProcedures.ChangeExecuteResultLabel(ref this.lbExecutionResult, worked, qtdLinesChanged);

                    string tableName;
                    if (worked)
                        tableName = this.mySqlConn.TableName(allCodes, this.codCmd);
                    else
                        tableName = "";
                    this.grvSelectTry.TopLeftHeaderCell.Value = tableName;

                    this.grvSelectTry.DataSource = dataTable;

                    if (!worked)
                        MessageBox.Show("SQL Exception:\r\n" + excep,
                            "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

        //btnHelp
        protected void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string code = "select * from " + this.tableName;
                DataTable table = new DataTable();
                string excep = null;
                int qtdLinesChanged = 0;
                this.mySqlConn.ExecuteOneSQLCmd(code, true, ref table, ref excep, ref qtdLinesChanged);

                SqlExecuteProcedures.ChangeExecuteResultLabel(ref this.lbExecutionResult, true, 0);
                this.grvSelectTry.TopLeftHeaderCell.Value = this.tableName;
                this.grvSelectTry.DataSource = table;

                MessageBox.Show("Hi! I created a table so you can practice what I will teach you!\n\r\n\r" +
                    "The table's name is '" + this.tableName + "' and you can see its fields in the selection...");
            }
            catch(Exception err)
            {
                MessageBox.Show("Someone has dropped the table I created('" + this.tableName + "')!");
            }
        }


        //Form Closed
        protected void FrmCommandExplanation_FormClosed(object sender, FormClosedEventArgs e)
        {
            //drop table
            string code = "drop table " + this.tableName;
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.ExecuteNonQuery();
        }


        //more resources (things to facilitate)
        protected void FrmCommandExplanation_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                this.btnExecute.PerformClick();
                e.Handled = true;
            }
        }
        
    }
}
