using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DillenManagementStudio
{
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
