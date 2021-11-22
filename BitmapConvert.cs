using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Interop;
//using System.Windows.Media.Imaging;


namespace Environment_1
{
    public static class BitmapConvert
    {
        /// <summary>
        /// byte[]转为Bitmap
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(byte[] byteArray)
        {
            try
            {
                using (MemoryStream outStream = new MemoryStream(byteArray))
                {
                    return new System.Drawing.Bitmap(outStream);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// byte[]转Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image ToImage(byte[] buffer)
        {
            return Image.FromStream(new MemoryStream(buffer));
        }

        /// <summary>
        /// Bitmap转byte[]
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] ToBuffer(Bitmap bmp)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                bmp.Save(mem, ImageFormat.Bmp);
                return mem.ToArray();
            }
        }
    }
}
