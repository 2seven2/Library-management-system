using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace 通讯录连接数据库
{
    class MyMeans
    {
        //连接字符串，后面字符串SQL SERVER中的连接属性中可以找到。
       public static string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
      // public static string connStr = @"Data Source=DESKTOP-LK27AO7;Initial Catalog = TXL; Integrated Security = True";
        public static SqlConnection conn = new SqlConnection(connStr);


    }
}