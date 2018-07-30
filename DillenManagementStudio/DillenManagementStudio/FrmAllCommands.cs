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
    public partial class FrmAllCommands : Form
    {
        protected FrmDillenSQLManagementStudio frmMain;
        protected User user;
        protected MySqlConnection mySqlConn;

        protected const int FIRST_LABEL = 125;

        public FrmAllCommands(User user, MySqlConnection mySqlConnection, FrmDillenSQLManagementStudio mainForm)
        {
            InitializeComponent();

            this.frmMain = mainForm;
            this.user = user;
            this.mySqlConn = mySqlConnection;

            this.PutCommandsLabel();
        }

        protected void PutCommandsLabel()
        {
            List<string> auxCommandList = this.user.AllSqlCommands;
            List<string> commandList = new List<string>();

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            for (int i = 0; i < auxCommandList.Count; i++)
                commandList[i] = textInfo.ToTitleCase(auxCommandList[i]);
            
            int height = FIRST_LABEL;
            int y = FIRST_LABEL;
            int qtdLabelEachColumn = (int)Math.Floor((decimal)commandList.Count/3) + (commandList.Count%3==0?0:1);
            int lastQuoc = 0;
            for(int i = 0; i<commandList.Count; i++)
            {
                int x;
                int currQuoc = (int)Math.Floor((decimal)i/ qtdLabelEachColumn);
                switch (currQuoc)
                {
                    case 0:
                        x = 49;
                        break;
                    case 1:
                        x = 310;
                        break;
                    default:
                        x = 572;
                        break;
                }

                if(currQuoc != lastQuoc)
                {
                    y = FIRST_LABEL;
                    lastQuoc = currQuoc;
                }

                y += this.PutNewLabel(commandList[i], x, y, i).Height + 5;

                if (y > height)
                    height = y;
            }

            this.Height = height + 80;
        }

        protected Label PutNewLabel(string command, int x, int y, int index)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.Location = new System.Drawing.Point(x, y);
            label.Size = new System.Drawing.Size(127, 31);
            label.TabIndex = 1;
            label.Tag = index;
            label.Text = (index + 1) + ".  " + command;
            label.Click += new System.EventHandler(this.labels_Click);
            this.Controls.Add(label);

            return label;
        }

        protected void labels_Click(object sender, EventArgs e)
        {
            int codCmd = (int)((Label)sender).Tag;

            try
            {
                new FrmCommandExplanation(codCmd, this.user, this.mySqlConn).Show();
            }
            catch(Exception err)
            {
                this.frmMain.User = null;
                MessageBox.Show("Unicamp VPN was disconnected! This resource is not available anymore!");
                this.Close();
            }
        }

    }
}
