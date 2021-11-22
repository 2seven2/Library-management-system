//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardTest
{
    /// <summary>
    /// ISO15693标准卡片类
    /// </summary>
    class ISO15693Card
    {
        /*
         * 定义ISO15693命令
         */
        public static string COMMAND_WRITE_REG = "010C00030410002101000000";//写寄存器
        public static string COMMAND_SET_AGC = "0109000304F0000000";//设置AGC
        public static string COMMAND_SET_RECV_MODE = "0109000304F1FF0000";//设置接收器模式
        public static string COMMAND_INVEN_CARD = "010B000304140401000000";//寻卡
        public static string COMMAND_READ_SINGLE_BLOCK = "0113000304182020{0}{1}0000";//读单块数据
        public static string COMMAND_WRITE_SINGLE_BLOCK = "0117000304182021{0}{1}{2}0000";//写单块数据

        private string _id;

        public string ID
        {
            get
            {
                return this._id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">卡ID</param>
        public ISO15693Card(string id)
        {
            this._id = id;
        }
    }
}
