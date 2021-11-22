using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLEDU.Core;
using System.Data.SqlClient;

namespace Environment_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        ICCardManager card = new ICCardManager();
        private void button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;
            //this.BackgroundImage = Image.FromFile(@"D:\cccc\Environment_2\Resources\1510643317606947.png");

            // 加载字体



            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Hide();
            form5.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();
        }
        private void showPanl(string panlName)
        {
            foreach (Control x in flowLayoutPanel1.Controls)
            {
                if (x.Name.StartsWith("panel"))
                {
                    //MessageBox.Show(x.GetType().ToString());
                    if (x.Name == panlName)
                    {
                        x.Visible = true;
                    }
                    else
                    {
                        x.Visible = false;
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            showPanl("panel2");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            showPanl("panel3");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            //this.Hide();  //调用Form1的Hide()方法隐藏Form1窗口
            //Form1 form1 = new Form1(); //生成一个Form2对象
            //form1.ShowDialog();  //将Form2窗体显示为模式对话框。

            Form1 form1 = new Form1();
            //this.Hide();
            form1.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            //this.Hide();
            form5.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
           // this.Hide();
            form6.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            this.Hide();
            form4.ShowDialog();
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Form3 form3 = new Form3();
            //this.Hide();
            //form3.ShowDialog();
            panel4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showPanl("panel1");
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
                        MessageBox.Show("注册成功");


                    }
                    else
                    {
                        MessageBox.Show("注册失败");
                    }
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {



        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            //this.Hide();
            form7.ShowDialog();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            //this.Hide();
            form8.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            //this.Hide();
            form9.ShowDialog();
        }
    }
}
