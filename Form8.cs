using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environment_1
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            //获取界面用户输入
            string xuehao = textBox1.Text.Trim();
          
            //1.运用Connection对象建立与数据库的连接                
            string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            //2.利用DataAdapter对象，建立与数据库的连接桥
            string selectStr = "select * from jilu where 1=1 ";
            if (!xuehao.Equals(""))
            {
                selectStr += " and xuehao=@t1";
            }
          
            SqlDataAdapter adapter = new SqlDataAdapter(selectStr, conn);
            adapter.SelectCommand.Parameters.Add("@t1", SqlDbType.VarChar).Value = xuehao;
         
            //3.通过DataAdapter桥，将查询结果存储到DataSet对象中
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //4.利用DataGridView控件将DataSet中的查询结果显示出来
            dataGridView1.DataSource = ds.Tables[0];
            
            //5.关闭数据库连接
            conn.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
