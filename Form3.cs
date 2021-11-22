using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLEDU.Core;
using System.Data.SqlClient;

namespace Environment_1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        ICCardManager card = new ICCardManager();
        private void Form3_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(@"D:\cccc\Environment_2\Resources\1510643317606947.png");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (card.TryInit())    //寻卡，连接设备打开卡（这个地方有个报错，可以忽略不管，按F5继续运行）
                {
                    MessageBox.Show("连接成功", "Card");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex.Message + "\r\n");
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                if (card.TryWrite(this.txtWriteStr.Text, 1, 1))   //写卡
                {
                    //  MessageBox.Show("写入成功", "Card");
                    //获取界面上用户输入信息
                    string xuehao = txtWriteStr.Text.Trim();
                    string name = textBox1.Text.Trim();
                    //连接数据库
                    string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
                    SqlConnection conn = new SqlConnection(connStr);
                    //打开数据库
                    conn.Open();
                    //3.利用Command对象执行sql语句
                    string selectStr = "insert into xinxi(xuehao,name)values(@t1,@t2)";
                    SqlCommand cmd = new SqlCommand(selectStr, conn);
                    cmd.Parameters.Add("@t1", SqlDbType.VarChar).Value = xuehao;
                    cmd.Parameters.Add("@t2", SqlDbType.VarChar).Value = name;
                    int res = cmd.ExecuteNonQuery();
                    //4.根据返回值判断是否添加成功
                    if (res == 1)
                    {
                        MessageBox.Show("发卡成功");


                    }
                    else
                    {
                        MessageBox.Show("发卡失败");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex.Message + "\r\n");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string a = "";
            try
            {
                if (card.TryRead(out a, 1, 1))              //读卡
                {
                    txtReadStr.Text = a;
                    MessageBox.Show("读取成功", "Card");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex.Message + "\r\n");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                card.Close();
                MessageBox.Show("读卡器连接已断开");
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex.Message + "\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
