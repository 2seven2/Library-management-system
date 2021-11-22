using SmartCardTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using NLEDU.Core;
using System;
using System.Timers;

namespace Environment_1
{
    public partial class Form6 : Form
    {
       
        public Form6()
        {
            InitializeComponent();
        }
        ICCardManager card = new ICCardManager();

      
        private static void test(object source, ElapsedEventArgs e)
        {

           

        }
        private void Form6_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
            time.Tick += Time_Tick;
            time.Interval = 2000; // 间隔1s 执行一次Time_Tick方法
            
            time.Start();
            


        }
        private void Time_Tick(object sender, EventArgs e)
        {
            string a = "";
            try
            {

                if (card.TryRead(out a, 1, 1))              //读卡
                {
                    textBox1.Text = a;
                    //MessageBox.Show("读取成功", "Card");

                }

            }

            catch (Exception ex)
            {
                //MessageBox.Show("异常" + ex.Message + "\r\n");
            }
            if (textBox1.Text == a) {
                string xuehao = textBox1.Text;

                //2.运用Connection对象建立与数据库的连接   
                //(1)定义连接数据库的字符串             
                string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
                //(2)创建Connection对象
                SqlConnection conn = new SqlConnection(connStr);
                //(3)打开连接
                conn.Open();
                //3.利用Command对象执行sql语句
                string sql = "select count(1) from xinxi where xuehao=@t1  ";
                //(2)通过Connection对象和sql语句，创建Command对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //(3)处理Command对象的参数
                cmd.Parameters.Add("@t1", SqlDbType.VarChar).Value = xuehao;
                if (textBox1.Text == "")
                {
                    pictureBox1.Image = Image.FromFile(@"D:\cccc\Environment_2\Resources\16pic_2513002_s (1).png");
                    label4.Text = "请刷卡";
                }
                else
                {
                   
                        //(4)执行SQL语句
                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                           // string sqlStr = "select time from jilu";
                            pictureBox1.Image = Image.FromFile(@"D:\cccc\Environment_2\Resources\QQ1.png");
                           
                            label4.Text = "欢迎进入";
                        return;

                    }
                        else
                        {
                            pictureBox1.Image = Image.FromFile(@"D:\cccc\Environment_2\Resources\qwe.png");
                            label4.Text = "权限不足，请联系管理员！";

                        }
                    conn.Close();
                    if (label4.Text == "欢迎进入")
                    {

                        string card = textBox1.Text;
                        string time = label2.Text;
                        string connStr1 = "server=DESKTOP-M52VF5V;database=jilu;uid=hlc;pwd=110";
                        //(2)创建Connection对象
                        SqlConnection conn1 = new SqlConnection(connStr1);
                        conn.Open();
                        string selectStr = "insert into jilu(xuehao,time)values(@t1,@t2)";
                        SqlCommand cmd1 = new SqlCommand(selectStr, conn1);
                        cmd1.Parameters.Add("@t1", SqlDbType.VarChar).Value = card;
                        cmd1.Parameters.Add("@t2", SqlDbType.VarChar).Value = time;
                        int res = cmd1.ExecuteNonQuery();
                        //4.根据返回值判断是否添加成功
                        if (res == 1)
                        {

                        }
                        else
                        {

                        }
                        conn.Close();

                    }
                   


                }
                conn.Close();
            }

            else
            {
                label4.Text = "请刷卡";
                textBox1.Text = "";


            }
            
        }
       

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();//关闭串口
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//时间显示格式  （yyyy-MM-dd hh:mm:ss  12小时制）

              
            }
            catch { 
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

   
        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        }

       

     
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
   