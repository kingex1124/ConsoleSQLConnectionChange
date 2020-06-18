using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSQLConnectionChange
{
    class Program
    {
        private string connStr = string.Empty;
        private bool changeFlag = true;
        static void Main(string[] args)
        {
            Program pg = new Program();

            pg.connStr = pg.ChangeConnStr();

            for (int i = 0; i < 3; i++)
            {
                if (pg.SQLConnection())
                    break;
                if (i == 2)
                {
                    pg.connStr = pg.ChangeConnStr();
                    i = 0;
                }
            }
            
            Console.ReadKey();
        }

        public bool SQLConnection()
        {
            using (SqlConnection cnn = new SqlConnection(connStr))
            {
                try
                {
                    cnn.Open();
                    Console.WriteLine("{0} 連線成功", connStr);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} 連線失敗", connStr);
                    return false;
                }
            }
        }

        public string ChangeConnStr()
        {
            if (changeFlag)
            {
                changeFlag = !changeFlag;
                return ConfigurationManager.ConnectionStrings["CodeFirstModel"].ConnectionString;
            }
            else
            {
                changeFlag = !changeFlag;
                return ConfigurationManager.ConnectionStrings["FAMC"].ConnectionString;
            }
        }
    }
}
