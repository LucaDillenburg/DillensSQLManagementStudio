using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//to work with the database
using System.Data.SqlClient;
using System.Data;

namespace DillenManagementStudio
{
    //to manage MY database: BD17188 (collect data and facilitate the usage of the app: collecting databases and teaching users)
    public class User
    {
        protected SqlConnection con;
        protected string MAC_ADRESS;

        public User()
        {
            string connStr = Properties.Settings.Default.BD17188ConnectionString;
            connStr = connStr.Substring(connStr.IndexOf("Data Source"));

            this.con = new SqlConnection();
            this.con.ConnectionString = connStr;
            this.con.Open();

            this.MAC_ADRESS = Computer.MacAdress;
        }

        ///Command Explanation
        public string CommandFromCod(int codCmd)
        {
            string code = "select name from sqlCommand " +
                "where codCmd = @codCmd";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@codCmd", codCmd);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);

            return ds.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        public void GetExplanationSqlCommand(int codCmd, ref List<string> titles, 
            ref List<string> cmdExplanations, ref List<string> textsUserTry)
        {
            string code = "select title, explanation, textUserTry " +
                "from explanationSqlCommand where codCmd = @codCmd order by stage";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@codCmd", codCmd);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);

            titles = new List<string>();
            cmdExplanations = new List<string>();
            textsUserTry = new List<string>();
            for(int i = 0; i< ds.Tables[0].Rows.Count; i++)
            {
                titles.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                cmdExplanations.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString());
                textsUserTry.Add(ds.Tables[0].Rows[i].ItemArray[2].ToString());
            }
        }
        

        ///getters
        //first of the list is the last one user used
        //returns with the password encrypted
        public List<string> ConectionsString
        {
            get
            {
                string code = "select conStr, encryptedpassword, wasLast from userdatabase " +
                "where macAdressPC = @macAdressPC";
                SqlCommand cmd = new SqlCommand(code, this.con);
                cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);

                List<string> databases = new List<string>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string encryptedPassword = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    bool wasLast = Convert.ToBoolean(ds.Tables[0].Rows[i].ItemArray[2]);

                    string currConStr = ds.Tables[0].Rows[i].ItemArray[0].ToString() //databaseWithoutPass
                        + "Password=" + encryptedPassword; //password
                    if (wasLast)
                        databases.Insert(0, currConStr);
                    else
                        databases.Add(currConStr);
                }

                return databases;
            }
        }

        public List<string> SqlCommands
        {
            get
            {
                string code = "select codCmd, name from SqlCommand where justUsedOnHelp = 0 order by codCmd";
                SqlCommand cmd = new SqlCommand(code, this.con);

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);

                List<string> sqlCommands = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    sqlCommands.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString());
                return sqlCommands;
            }
        }


        ///inicialize methods
        public void InicializeUser()
        {
            if (this.UserExists())
                this.UpdateQtdUsed();
            else
                this.InsertNewUser();
        }

        protected bool UserExists()
        {
            string code = "select * from dillensqlmanagementstudiouser where macAdressPC = @macAdressPC ";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);

            return ds.Tables[0].Rows.Count >= 1;
        }

        protected void UpdateQtdUsed()
        {
            string code = "update dillensqlmanagementstudiouser set qtdTimesUsedApp = " +
                "(select qtdTimesUsedApp from dillensqlmanagementstudiouser where macAdressPC = @macAdressPC) + 1 " +
                "where macAdressPC = @macAdressPC";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);

            int iResult = cmd.ExecuteNonQuery();
            if (iResult <= 0)
                throw new Exception("MyDatabaseOperations.InsertNewUserValues() couldn't update!");
        }

        protected void InsertNewUser()
        {
            string code = "insert into dillensqlmanagementstudiouser values(@macAdressPC, @qtdUsed)";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);
            cmd.Parameters.AddWithValue("@qtdUsed", 1);

            int iResult = cmd.ExecuteNonQuery();
            if (iResult <= 0)
                throw new Exception("MyDatabaseOperations.InsertNewUser() couldn't insert!");
        }


        ///when user makes an execution
        public void OneMoreExecution(string conString)
        {
            string code = "update userdatabase set qtdExecutions = " +
                "(select qtdExecutions from userdatabase where macAdressPC = @macAdressPC and conStr = @conStr) + 1 " +
                "where macAdressPC = @macAdressPC and constr = @strconn";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);
            cmd.Parameters.AddWithValue("@conStr", conString.Substring(0, conString.LastIndexOf("Password=")));

            int iResult = cmd.ExecuteNonQuery();
            if (iResult <= 0)
                throw new Exception("MyDatabaseOperations.OneMoreExecution(conString) couldn't update!");
        }


        ///add and delete databases
        //strConn must have the password encrypted already
        public void AddDatabase(string strConn)
        {
            //1. put every database with last = 0
            string code = "update userdatabase set wasLast = 0 " +
                "where macAdressPC = @macAdressPC";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);

            cmd.ExecuteNonQuery();

            //2. insert database with wasLast = 1
            code = "insert into userdatabase values(@macAdressPC, @strconn, @password, @qtdExecutions, @qtdSintaxHelp, @wasLast)";
            cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);
            cmd.Parameters.AddWithValue("@strconn", strConn.Substring(0, strConn.LastIndexOf("Password=")));
            cmd.Parameters.AddWithValue("@password", strConn.Substring(strConn.LastIndexOf("Password=") + 9));
            cmd.Parameters.AddWithValue("@qtdExecutions", 0);
            cmd.Parameters.AddWithValue("@qtdSintaxHelp", 0);
            cmd.Parameters.AddWithValue("@wasLast", 1);

            int iResult = cmd.ExecuteNonQuery();
            if (iResult <= 0)
                throw new Exception("MyDatabaseOperations.AddDatabase(conString) couldn't insert!");
        }
        
        public void DeleteDatabase(string strConn)
        {
            string code = "delete from userdatabase where macAdressPC = @macAdressPC and conStr = @conStr";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);
            cmd.Parameters.AddWithValue("@conStr",
                strConn.Substring(0, strConn.LastIndexOf("Password=")));

            int iResult = cmd.ExecuteNonQuery();
            if (iResult <= 0)
                throw new Exception("MyDatabaseOperations.DeleteDatabase(conString) couldn't delete!");
        }
        

        ///set last
        public void SetLastDatabase(string strConn)
        {
            //1. put every database with last = 0
            string code = "update userdatabase set wasLast = 0 " +
                "where macAdressPC = @macAdressPC";
            SqlCommand cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);

            cmd.ExecuteNonQuery();
            
            //2. set last = 1 in this database
            code = "update userdatabase set wasLast = 1 " +
                "where macAdressPC = @macAdressPC and constr = @strconn";
            cmd = new SqlCommand(code, this.con);
            cmd.Parameters.AddWithValue("@macAdressPC", this.MAC_ADRESS);
            cmd.Parameters.AddWithValue("@strconn", strConn.Substring(0, strConn.LastIndexOf("Password=")));

            cmd.ExecuteNonQuery();
        }

    }
}
