using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DillenManagementStudio
{
    public partial class FrmSplash : Form
    {
        //time
        protected int miliseconds = 0;

        //conection
        protected byte connection = 0;
        protected const byte CONNECTED = 1;
        protected const byte NOT_CONNECTED = 2;
        protected const byte NONE = 0;

        //last time
        protected bool lastTime = false;


        public FrmSplash()
        {
            InitializeComponent();

            //Application.DoEvents();
        }

        protected void tmr_Tick(object sender, EventArgs e)
        {
            this.miliseconds += this.tmr.Interval;

            if (this.lastTime)
            {
                this.tmr.Enabled = false;
                this.Close();
                return;
            }

            if (this.connection != NONE && this.miliseconds>=1750)
            {
                //visible
                this.picLoading.Visible = false;
                if (this.connection == CONNECTED)
                    this.lbStage.Text = "Connected!";
                else
                    this.lbStage.Text = "Couldn't connect";
                
                this.tmr.Interval = 750;

                this.lastTime = true;
            }
        }

        public void CanClose(bool connected)
        {
            this.connection = (connected?CONNECTED:NOT_CONNECTED);
        }

    }
}
