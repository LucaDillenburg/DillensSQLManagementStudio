using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DillenManagementStudio
{
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
}
