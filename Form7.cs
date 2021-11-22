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
using 通讯录连接数据库;

namespace Environment_1
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();

        }

        private void Form7_Load(object sender, EventArgs e)
        {
           // Form7 form7 = new Form7();
           //this.Hide();
           // form7.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("用户名不能为空！");
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("密码不能为空!");
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("确认密码不能为空!");
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("密码和确认密码不相符!");
                textBox2.Text = "";
                textBox3.Text = "";
            }
            try
            {
                string sql = string.Format("select count(*) from denglu where userName='{0}'", textBox1.Text);
                SqlCommand cmd = new SqlCommand(sql, MyMeans.conn);

                MyMeans.conn.Open();
                int a = (int)cmd.ExecuteScalar();//返回一个值，看用户是否存在
                StringBuilder strsql = new StringBuilder();
                if (a == 0)
                {
                    strsql.Append("insert into denglu(userName,userPwd)");
                    strsql.Append("values(");
                    strsql.Append("'" + textBox1.Text.Trim().ToString() + "',");
                    strsql.Append("'" + textBox2.Text.Trim().ToString() + "'");
                    strsql.Append(")");
                    using (SqlCommand cmd2 = new SqlCommand(strsql.ToString(), MyMeans.conn))
                    {
                        cmd2.ExecuteNonQuery();

                    }

                    MessageBox.Show("注册成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();

                }



                else
                {
                    MessageBox.Show("用户已存在！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Exit();
            }

            finally
            {
                MyMeans.conn.Close();
                MyMeans.conn.Dispose();
            }


        }
    }
}
