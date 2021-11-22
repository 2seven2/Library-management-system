using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Camera.Net;

namespace Environment_1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private CameraCapture _Camera = null;
        public CameraCapture Camera
        {
            get
            {
                if (_Camera == null)
                {
                    _Camera = new CameraCapture();
                    _Camera.OnFrameChanged += _Camera_OnFrameChanged;
                }
                return _Camera;
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
           
        }
        private bool openOrCloseFlag = false;//摄像头打开与否标识
        private void button1_Click(object sender, EventArgs e)
        {
            if (!openOrCloseFlag)
            {
                Camera.Open("rtsp://admin:12345678@192.168.0.160/11");
                openOrCloseFlag = true;
            }
            else
            {
                Camera.Close();
                openOrCloseFlag = false;
            }
        }
        private void _Camera_OnFrameChanged(object sender, byte[] buffer)
        {
            if (buffer != null && buffer.Length > 0)
            {
                //页面显示摄像头帧数据
                pictureBox1.Image = BitmapConvert.ToImage(buffer);
            }
        }
        private void SendPTZCommand(string control)
        {
            //step:1 单步，0 非单步；act：转动方向：UP/DOWN/LEFT/RIGHT
            string strUrl = string.Format("http://192.168.0.100:80/web/cgi-bin/hi3510/ptzctrl.cgi?-step=1&-act={0}", control.ToLower());
            try
            {
                HttpWebRequest request = WebRequest.Create(strUrl) as HttpWebRequest;
                //设置请求参数
                request.KeepAlive = false;
                request.Timeout = 5000;
                request.Method = "GET";
                request.Credentials = new NetworkCredential("4", "12345678");

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //获取返回值
                //string resultObj = string.Empty;
                //using (var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                //{
                //    resultObj = sr.ReadToEnd();
                //}
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            SendPTZCommand("UP");
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            SendPTZCommand("DOWN");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            SendPTZCommand("RIGHT");
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            SendPTZCommand("LEFT");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }
    }
 }

