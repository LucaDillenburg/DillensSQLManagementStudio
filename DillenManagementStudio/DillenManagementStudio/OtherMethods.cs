using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
//new
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Data.SqlClient;
using System.ComponentModel;

namespace DillenManagementStudio
{
    public class Computer
    {
        public static string MacAdress
        {
            get
            {
                return NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback).Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();
            }
        }
    }

    public class Force
    {
        public static void Focus(Control ctrl)
        {
            new Thread(() => Force.AuxForceFocus(ctrl)).Start();
        }

        protected static void AuxForceFocus(Control ctrl)
        {
            try
            {
                while (!(bool)ctrl.Invoke(new Action(() => ctrl.Focus())))
                { }
            }
            catch (Exception e)
            { }
        }

    }

    public static class RichTextBoxExtensions
    {
        //general multicolor richtextbox
        //https://stackoverflow.com/questions/1926264/color-different-parts-of-a-richtextbox-string
        public static void ChangeTextColor(this RichTextBox rchtxt, Color color, int startIndex, int endIndex)
        {
            int currSelIndex = rchtxt.SelectionStart;
            int currSelLength = rchtxt.SelectionLength;

            rchtxt.Select(startIndex, endIndex - startIndex);
            rchtxt.SelectionColor = color;

            rchtxt.Select(currSelIndex, currSelLength);
        }
    }

    public static class SqlCommandExtension
    {
        public static bool IsConnected(this SqlConnection con)
        {
            try
            {
                //the best way of seen if the conection is on, is to make a simple select
                SqlCommand cmd = new SqlCommand("Select 1", con);
                int returnedValue = (Int32)cmd.ExecuteScalar();
                return returnedValue == 1;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }

    public static class PreviewKeyDownEventArgsExtensions
    {
        public static bool IsKeyChangeText(this PreviewKeyDownEventArgs e)
        {
            return !e.Control && !e.Alt && (e.KeyCode == Keys.D1 || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D3 || e.KeyCode == Keys.D4
                || e.KeyCode == Keys.D5 || e.KeyCode == Keys.D6 || e.KeyCode == Keys.D7 || e.KeyCode == Keys.D8
                || e.KeyCode == Keys.D9 || e.KeyCode == Keys.D0 || e.KeyCode == Keys.Q || e.KeyCode == Keys.W
                || e.KeyCode == Keys.E || e.KeyCode == Keys.R || e.KeyCode == Keys.T || e.KeyCode == Keys.Y
                || e.KeyCode == Keys.U || e.KeyCode == Keys.I || e.KeyCode == Keys.O || e.KeyCode == Keys.P
                || e.KeyCode == Keys.A || e.KeyCode == Keys.S || e.KeyCode == Keys.D || e.KeyCode == Keys.F
                || e.KeyCode == Keys.G || e.KeyCode == Keys.H || e.KeyCode == Keys.J || e.KeyCode == Keys.K
                || e.KeyCode == Keys.L || e.KeyCode == Keys.Z || e.KeyCode == Keys.X || e.KeyCode == Keys.C
                || e.KeyCode == Keys.V || e.KeyCode == Keys.B || e.KeyCode == Keys.N || e.KeyCode == Keys.M
                || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space
                || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.OemSemicolon || e.KeyCode == Keys.OemQuotes
                || e.KeyCode == Keys.OemQuestion || e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.OemPipe
                || e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.OemOpenBrackets || e.KeyCode == Keys.OemMinus
                || e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.OemCloseBrackets || e.KeyCode == Keys.OemBackslash
                || e.KeyCode == Keys.Oem1 || e.KeyCode == Keys.Oem2 || e.KeyCode == Keys.Oem3 || e.KeyCode == Keys.Oem4
                || e.KeyCode == Keys.Oem5 || e.KeyCode == Keys.Oem6 || e.KeyCode == Keys.Oem7 || e.KeyCode == Keys.Oem8);
        }
    }

    public class OpaquePanel : Panel
    {
        protected const int WS_EX_TRANSPARENT = 0x20;
        public OpaquePanel(int opacity)
        {
            this.opacity = opacity;
            SetStyle(ControlStyles.Opaque, true);
        }

        protected int opacity;
        [DefaultValue(50)]
        public int Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("value must be between 0 and 100");
                this.opacity = value;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb(this.opacity * 255 / 100, this.BackColor)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            base.OnPaint(e);
        }

    }

}
