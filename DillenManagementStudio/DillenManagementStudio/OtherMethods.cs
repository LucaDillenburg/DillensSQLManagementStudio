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

namespace DillenManagementStudio
{
    class Computer
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

            rchtxt.SelectionStart = startIndex;
            rchtxt.SelectionLength = endIndex - startIndex;
            rchtxt.SelectionColor = color;

            rchtxt.SelectionStart = currSelIndex;
            rchtxt.SelectionLength = currSelLength;
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

}
