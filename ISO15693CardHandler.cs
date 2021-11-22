using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardTest
{
    
    /// <summary>
    /// 智能卡操作类
    /// </summary>
    class ISO15693CardHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">发送寻卡指令后，由下位机返回的响应</param>
        /// <returns></returns>
        public static List<ISO15693Card> InventoryCard(string response)
        {
            List<ISO15693Card> cards = new List<ISO15693Card>();

            if (string.IsNullOrEmpty(response)) return cards;

            string[] lines = response.Split(new string[] {"\r\n"}, StringSplitOptions.None);//按行分割字符串

            /*
             * 开始解析每一行字符串，提取出寻到的卡ID 
             */

            int count = 0;//寻到的卡数目

            foreach(string line in lines)
            {
                string temp = line;

                //非以“[”开头，跳过本次循环，迭代下一个字符串
                if (!line.StartsWith("[")) continue;

                /**********以“[”开头，则开始提取卡号**********/

                //第一步：去掉首尾的“[]”符号
                temp = temp.TrimStart(new char[1] { '[' });//去掉头部的“[”
                temp = temp.TrimEnd(new char[1] { ']' });//去掉尾部的“]”

                //第二步：按“,”号分割字符串，取第一个元素，则为可能为卡号
                temp = temp.Split(new string[] { "," }, StringSplitOptions.None)[0];

                //第三部：判断长度
                if (temp.Length <= 0) continue;//为空，则不包含卡号，迭代
                else
                {
                    temp = ISO15693CardHandler.CovertEndian(temp);//颠倒卡号字节
                    cards.Add(new ISO15693Card(temp));//以该卡号实例化ISO15693Card类，添加到列表中
                    count++;
                }
            }

            return cards;
        }

        /// <summary>
        /// 由读单块命令返回的字符串解析出单块数据
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GenerateBlockData(string response)
        {
            string data = "";

            if (string.IsNullOrEmpty(response)) return data;

            string[] lines = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);//按行分割字符串

            foreach(string line in lines)
            {
                string temp = line;

                //非以“[”开头，跳过本次循环，迭代下一个字符串
                if (!line.StartsWith("[")) continue;

                /**********以“[”开头，则开始提取数据**********/

                //第一步：去掉首尾的“[]”符号
                temp = temp.TrimStart(new char[1] { '[' });//去掉头部的“[”
                temp = temp.TrimEnd(new char[1] { ']' });//去掉尾部的“]”

                //第二步：根据ISO15693的块存储空间，长度为32位，即4字节，8个16进制字符
                if (temp.Length <= 8) return data;//不满足，则返回

                //第三步：去掉首字节标识
                temp = temp.Substring(2);

                data = temp;

                break;
            }

            return data;
        }

        /// <summary>
        /// 转换大小端字节序
        /// </summary>
        /// <param name="Hex">以字符串表示的16进制字节</param>
        /// <returns></returns>
        public static string CovertEndian(string hexString)
        {
            if (string.IsNullOrEmpty(hexString) || (hexString.Length % 2 != 0))
                throw new Exception("非法的字符串！");

            //计算字节数
            int bytesCount = hexString.Length / 2;
            //创建一个堆栈
            Stack<string> bytes = new Stack<string>(bytesCount);

            for(int i = 0; i < bytesCount; i++)
            {
                string b = hexString.Substring(i * 2, 2);
                if (!CheckValidHexByte(b)) throw new Exception("十六进制格式非法！");
                bytes.Push(b);//入栈
            }

            StringBuilder sb = new StringBuilder();

            while(bytes.Count > 0)
            {
                sb.Append(bytes.Pop());//出栈，添加到尾部
            }

            return sb.ToString();
        }

        /// <summary>
        /// 检查输入的16进制单字节是否合法。前面无需添加0x前缀。
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Boolean CheckValidHexByte(string hex)
        {
            //定义正则表达式
            string PATTERN = @"^([A-Fa-f0-9]){2}$";
            //开始正则匹配
            Boolean isMatch =  System.Text.RegularExpressions.Regex.IsMatch(hex, PATTERN);
            return isMatch;
        }

        /// <summary>
        /// 检查16进制字节集的格式是否合法
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static Boolean CheckValidHexBytes(string hexString)
        {
            if (string.IsNullOrEmpty(hexString) || (hexString.Length % 2 != 0))
                return false;

            //计算字节数
            int bytesCount = hexString.Length / 2;

            //检查格式
            for(int i = 0; i < bytesCount; i++)
            {
                string b = hexString.Substring(i * 2, 2);
                if (!CheckValidHexByte(b)) return false;
            }

            return true;
        }
    }
}
