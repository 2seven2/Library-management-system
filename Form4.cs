using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Environment_1
{
    public partial class Form4 : Form
    {
       
        public Form4()
        {
            InitializeComponent();
            
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            //获取界面上用户输入信息
            string userName = textBox1.Text;
            string userPwd = textBox2.Text;
          
            //2.运用Connection对象建立与数据库的连接   
            //(1)定义连接数据库的字符串             
            string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
            //(2)创建Connection对象
            SqlConnection conn = new SqlConnection(connStr);
            //(3)打开连接
            conn.Open();
            //3.利用Command对象执行sql语句
            //(1)定义要执行的sql语句
            string sql = "select count(1) from denglu where userName=@t1 and userPwd=@t2 ";
            //(2)通过Connection对象和sql语句，创建Command对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            //(3)处理Command对象的参数
            cmd.Parameters.Add("@t1", SqlDbType.VarChar).Value = userName;
            cmd.Parameters.Add("@t2", SqlDbType.VarChar).Value = userPwd;
           
            //(4)执行SQL语句
            if ((int)cmd.ExecuteScalar() > 0)
            {
                Form2 Form2 = new Form2();
                userName = textBox1.Text;
                userPwd = textBox2.Text;
                Form2.Show();
                MessageBox.Show("欢迎进入图书馆管理系统！", "登陆成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // this.Hide(); //查到数据的处理逻辑代码
            }
            else
            {
                MessageBox.Show("用户名或者密码错误", "!提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //没查到数据的处理逻辑代码           
            }

            //4.关闭数据库连接
            conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
        
        private void Form4_Load(object sender, EventArgs e)
        {
           
         



            label5.BackColor = Color.FromArgb(70, Color.Black);
            label2.BackColor = Color.FromArgb(70, Color.Black);
            label1.BackColor = Color.FromArgb(70, Color.Black);
            panel1.BackColor = Color.FromArgb(70, Color.Black);
            this.BackgroundImage = Image.FromFile(@"D:\cccc\Environment_2\Resources\35a96f63880911ebb6edd017c2d2eca2.jpg");

            label3.BackColor = Color.Transparent;
           // label3.Parent = pictureBox2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox1.Focus();

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox2.Focus();

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form7 form7 = new Form7();
            this.Hide();
            form7.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Location = new Point(900, 200);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }
        private void Draw(Rectangle rectangle, Graphics g, int _radius, bool cusp, Color begin_color, Color end_color)
        {
            int span = 2;
            //抗锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //渐变填充
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangle, begin_color, end_color, LinearGradientMode.Vertical);
            //画尖角
            if (cusp)
            {
                span = 10;
                PointF p1 = new PointF(rectangle.Width - 12, rectangle.Y + 10);
                PointF p2 = new PointF(rectangle.Width - 12, rectangle.Y + 30);
                PointF p3 = new PointF(rectangle.Width, rectangle.Y + 20);
                PointF[] ptsArray = { p1, p2, p3 };
                g.FillPolygon(myLinearGradientBrush, ptsArray);
            }
            //填充
            g.FillPath(myLinearGradientBrush, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - span, rectangle.Height - 1, _radius));
        }

        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {
            Draw(e.ClipRectangle, e.Graphics, 18, false, Color.FromArgb(70, Color.Black), Color.FromArgb(70, Color.Black));
            base.OnPaint(e);
            Graphics g = e.Graphics;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
