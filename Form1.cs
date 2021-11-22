using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace Environment_1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;    //允许跨线程调用
            btn_Close.Enabled = false;      //关闭串口按钮不可用
            
        }
        //串口接收数据后处理函数
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            float add;
            //判断数据头是否正确（0xfd开头），正确就进入循环
            while (serialPort3.ReadByte() == 0xff && serialPort3.ReadByte() == 0xfd)
            {
                add = float.Parse(textBox1.Text);
                byte[] A = new byte[1024 * 1024];
                int a = serialPort3.ReadByte();                   //读取数据
                for (int i = 0; i < 8; i++)
                {
                    A[i] = (byte)serialPort3.ReadByte();          //循环读取数据
                }
                switch (a)
                {
                    case 0x00://光照
                        {
                            label3.Text = A[1] + "." + A[2];

                            if (checkBox2.Checked && float.Parse(label3.Text) < add)
                            {
                                byte[] bts = new byte[] { 0xFA, 0xFB, 0x06, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                serialPort3.Write(bts, 0, bts.Length);
                            }
                            if (checkBox2.Checked && float.Parse(label3.Text) > add)
                            {
                                byte[] bts = new byte[] { 0xFA, 0xFB, 0x06, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                serialPort3.Write(bts, 0, bts.Length);
                            }
                        }
                        break;
                    case 0x01://温湿度
                        {
                            //温度

                        }
                        break;
                    default:
                        break;
                }

            }




        }

        //打开串口
        private void btn_Open_Click(object sender, EventArgs e)
        {

            serialPort3.Open();           //打开串口默认为COM1
            MessageBox.Show("打开串口成功");
            btn_Open.Enabled = false;        //将打开串口按钮设置为不可用
            btn_Close.Enabled = true;        //关闭串口按钮可用

        }
        //关闭串口
        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (serialPort3.IsOpen)
            {
                serialPort3.Close();           //关闭串口默认为COM1
            }

            btn_Open.Enabled = true;        //将打开串口按钮设置为可用
            btn_Close.Enabled = false;      //关闭串口按钮不可用
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lab_temp_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            try
            {
                string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
                conn = new SqlConnection(connStr);
                //3、打开连接
                conn.Open();
                string insertStr = "insert into env(time,guangzhao) values(getdate(),@t1)";
                SqlCommand cmd = new SqlCommand(insertStr, conn);
                cmd.Parameters.Add(new SqlParameter("@t1", SqlDbType.VarChar));
                cmd.Parameters["@t1"].Value = label3.Text; //gd即光照值label.Text
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                // MessageBox.Show("Query Class error:" + ex.ToString());
            }
            conn.Close();
        }
        private const string V = "time";

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
            time.Tick += Time_Tick;
            time.Interval = 2000; // 间隔1s 执行一次Time_Tick方法

            time.Start();
            DataTable dt = CreateDataTable();

            //设置图表的数据源
            chart1.DataSource = dt;
            //设置图表Y轴对应项

            chart1.Series[0].YValueMembers = "guangzhao";
            //设置图表X轴对应项
            //chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].XValueMember = V;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            //绑定数据
            chart1.DataBind();


        }
        private void Time_Tick(object sender, EventArgs e)
        {


            string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
            SqlConnection conn = new SqlConnection(connStr);
            //3、打开连接
            conn.Open();
            Random ra = new Random();
            Series series = chart1.Series[0];
            //series.Points.AddXY(DateTime.Now, ra.Next(48, 52));
            //chart1.ChartAreas[0].AxisX.ScaleView.Position = series.Points.Count - 5;
            string selStr = "select * from (select top(5) time,guangzhao from env order by time desc) as data order by time";
            SqlCommand cmd = new SqlCommand(selStr, conn);
           

            conn.Close();
        }


       

            private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            try
            {
                //2、运用Connection对象建立与数据库的连接
                string connStr = "server=DESKTOP-M52VF5V;database=env;uid=hlc;pwd=110";
                conn = new SqlConnection(connStr);
                //3、打开连接
                conn.Open();


                // 添加实时光照数据到数据库


                string selStr = "select * from (select top(5) time,guangzhao from env order by time desc) as data order by time";

                SqlDataAdapter myAdapter = new SqlDataAdapter(selStr, conn);
                DataSet myDataSet = new DataSet();
                myAdapter.Fill(myDataSet, "env");
                dt = myDataSet.Tables["env"];


            }
            catch (Exception ex)
            {
                MessageBox.Show("Query Class error:" + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return dt;
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bts = new byte[] { 0xFA, 0xFB, 0x06, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };
            serialPort3.Write(bts, 0, bts.Length);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bts = new byte[] { 0xFA, 0xFB, 0x06, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 };
            serialPort3.Write(bts, 0, bts.Length);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }




        private void button4_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {


        }
        private void ShowChart1()
        {

        }
        System.Windows.Forms.Timer chartTimer = new System.Windows.Forms.Timer();



       
    private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
