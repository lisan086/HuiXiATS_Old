using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.CRCJiaoYan
{
    /// <summary>
    /// CRC校验
    /// </summary>
    public static class CRC
    {
        /// <summary>
        /// CRC16_Modbus效验 true 表示高位在前，false表示高位在后
        /// </summary>
        /// <param name="byteData">要进行计算的字节数组</param>
        /// <param name="gaiweizaiqian">true 表示高位在前，false表示高位在后</param>
        /// <returns>计算后的数组</returns>
        public static byte[] ToModbus(List<byte> byteData, bool gaiweizaiqian)
        {
            byte[] CRC = new byte[2];

            UInt16 wCrc = 0xFFFF;
            for (int i = 0; i < byteData.Count; i++)
            {
                wCrc ^= Convert.ToUInt16(byteData[i]);
                for (int j = 0; j < 8; j++)
                {
                    if ((wCrc & 0x0001) == 1)
                    {
                        wCrc >>= 1;
                        wCrc ^= 0xA001;//异或多项式
                    }
                    else
                    {
                        wCrc >>= 1;
                    }
                }
            }
            if (gaiweizaiqian)
            {
                CRC[0] = (byte)((wCrc & 0xFF00) >> 8);//高位在前
                CRC[1] = (byte)(wCrc & 0x00FF);       //低位在后
            }
            else
            {
                CRC[1] = (byte)((wCrc & 0xFF00) >> 8);//高位在后
                CRC[0] = (byte)(wCrc & 0x00FF);       //低位在前
            }
            return CRC;

        }

        /// <summary>
        /// 获取该数据段对于的int32值
        /// </summary>
        /// <param name="shuju"></param>
        /// <param name="failvalue">转换不成功的值</param>
        /// <returns></returns>
        public static int GetInt(List<byte> shuju, int failvalue)
        {
            if (shuju == null || shuju.Count > 4)
            {
                return failvalue;
            }
            int count = shuju.Count;
            if (count == 4)
            {
                Int32 value = BitConverter.ToInt32(shuju.ToArray(), 0);
                return value;
            }
            else if (count == 2)
            {
                Int16 value = BitConverter.ToInt16(shuju.ToArray(), 0);
                return value;
            }
            else if (count == 1)
            {
                Int32 value = shuju[0];
                return value;
            }
            else
            {
                if (shuju.Count < 4)
                {
                    int shuliang = 4 - shuju.Count;
                    for (int i = 0; i < shuliang; i++)
                    {
                        shuju.Insert(0, 0x00);
                    }
                    Int32 value = BitConverter.ToInt32(shuju.ToArray(), 0);
                    return value;
                }
            }

            return failvalue;

        }

        /// <summary>
        /// 获取该数据段对于的int32值
        /// </summary>
        /// <param name="shuju"></param>
        /// <param name="isxunxu"></param>
        /// <param name="failvalue">转换不成功的值</param>
        /// <returns></returns>
        public static int GetInt(List<byte> shuju,bool isxunxu, int failvalue)
        {
            if (shuju == null || shuju.Count > 4)
            {
                return failvalue;
            }
            if (isxunxu)
            {
                int count = shuju.Count;
                if (count == 4)
                {
                    Int32 value = BitConverter.ToInt32(shuju.ToArray(), 0);
                    return value;
                }
                else if (count == 2)
                {
                    Int16 value = BitConverter.ToInt16(shuju.ToArray(), 0);
                    return value;
                }
                else if (count == 1)
                {
                    Int32 value = shuju[0];
                    return value;
                }
                else
                {
                    if (shuju.Count < 4)
                    {
                        int shuliang = 4 - shuju.Count;
                        for (int i = 0; i < shuliang; i++)
                        {
                            shuju.Insert(0, 0x00);
                        }
                        Int32 value = BitConverter.ToInt32(shuju.ToArray(), 0);
                        return value;
                    }
                }

                return failvalue;
            }
            else
            {
                int count = shuju.Count;
                shuju.Reverse();
                if (count == 4)
                {
                    Int32 value = BitConverter.ToInt32(shuju.ToArray(), 0);
                    return value;
                }
                else if (count == 2)
                {
                    Int16 value = BitConverter.ToInt16(shuju.ToArray(), 0);
                    return value;
                }
                else if (count == 1)
                {
                    Int32 value = shuju[0];
                    return value;
                }
                else
                {
                    if (shuju.Count < 4)
                    {
                        int shuliang = 4 - shuju.Count;
                        for (int i = 0; i < shuliang; i++)
                        {
                            shuju.Insert(0, 0x00);
                        }
                        Int32 value = BitConverter.ToInt32(shuju.ToArray(), 0);
                        return value;
                    }
                }

                return failvalue;
            }

        }

        /// <summary>
        /// 获取标准的浮点数
        /// </summary>
        /// <param name="shuju"></param>
        /// <returns></returns>
        public static float GetFloat(byte[] shuju)
        {
            float zhi = 0;
            if (BitConverter.IsLittleEndian)
            {
                byte[] bytes = new byte[]
                {
                        shuju[3],
                        shuju[2],
                        shuju[1],
                        shuju[0]
                };
                zhi = BitConverter.ToSingle(bytes, 0);
            }

            return zhi;
        }


        /// <summary>
        /// 把十进制转成两个二进制 true高位在前，低位在后
        /// </summary>
        /// <param name="len"></param>
        /// <param name="isgaoweizaiqian">true高位在前，低位在后</param>
        /// <returns></returns>
        public static byte[] ShiOrByte2(int len, bool isgaoweizaiqian)
        {
            List<byte> shuju = new List<byte>();
            byte[] intBuff = BitConverter.GetBytes(len);
            if (isgaoweizaiqian)
            {
                if (intBuff.Length >= 2)
                {
                    shuju.Add(intBuff[1]);
                    shuju.Add(intBuff[0]);
                }
            }
            else
            {
                if (intBuff.Length >= 2)
                {
                    shuju.Add(intBuff[0]);
                    shuju.Add(intBuff[1]);
                }
            }
            return shuju.ToArray();
        }

        /// <summary>
        /// 高低位交换用于modbutcp
        /// </summary>
        /// <param name="geshu"></param>
        /// <returns></returns>
        public static List<byte> JiaoHuanByte(List<byte> geshu)
        {
            List<byte> shuju = new List<byte>();

            for (int i = 0; i < geshu.Count; i += 2)
            {
                if (geshu.Count > i + 1)
                {
                    shuju.Add(geshu[i + 1]);
                    shuju.Add(geshu[i]);
                }
                else
                {
                    if (geshu.Count > i)
                    {
                        shuju.Add(geshu[i]);
                    }
                }
            }
            return shuju;
        }

        /// <summary>
        /// 把一个10进制转成4个16进制的byte数据
        /// </summary>
        /// <param name="len"></param>
        /// <param name="isgaoweizaiqian">true高位在前，低位在后</param>
        /// <returns></returns>
        public static List<byte> Get10OrB4(int len, bool isgaoweizaiqian)
        {
            List<byte> shuju = new List<byte>();
            byte[] intBuff = BitConverter.GetBytes(len);
            if (isgaoweizaiqian)
            {
                if (intBuff.Length >= 4)
                {
                    shuju.Add(intBuff[3]);
                    shuju.Add(intBuff[2]);
                    shuju.Add(intBuff[1]);
                    shuju.Add(intBuff[0]);
                }
            }
            else
            {
                if (intBuff.Length >= 4)
                {
                    shuju.Add(intBuff[0]);
                    shuju.Add(intBuff[1]);
                    shuju.Add(intBuff[2]);
                    shuju.Add(intBuff[3]);
                }
            }
            return shuju;
        }

        /// <summary>
        /// 把一个10进制转成两个16进制的byte数据
        /// </summary>
        /// <param name="len"></param>
        /// <param name="isgaoweizaiqian">true高位在前，低位在后</param>
        /// <returns></returns>
        public static List<byte> Get10OrB(int len, bool isgaoweizaiqian)
        {
            List<byte> shuju = new List<byte>();
            byte[] intBuff = BitConverter.GetBytes(len);
            if (isgaoweizaiqian)
            {
                if (intBuff.Length >= 2)
                {
                    shuju.Add(intBuff[1]);
                    shuju.Add(intBuff[0]);
                }
            }
            else
            {
                if (intBuff.Length >= 2)
                {
                    shuju.Add(intBuff[0]);
                    shuju.Add(intBuff[1]);
                }
            }
            return shuju;
        }

        /// <summary>
        /// 用于描述数据
        /// </summary>
        /// <param name="canshu"></param>
        /// <param name="zuixiaozhi"></param>
        /// <param name="zuidazhi"></param>
        /// <param name="danwei"></param>
        /// <param name="biaojiao"></param>
        /// <param name="miaosu"></param>
        /// <returns></returns>
        public static string GetMiaoSu(string canshu, string zuixiaozhi, string zuidazhi, string danwei, string biaojiao, string miaosu)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"参数类型:{canshu}");
            sb.AppendLine($"最小值:{zuixiaozhi}");
            sb.AppendLine($"最大值:{zuidazhi}");
            sb.AppendLine($"单位:{danwei}");
            sb.AppendLine($"比较:{biaojiao}");
            sb.AppendLine($"描述:{miaosu}");
            return sb.ToString();
        }
    }
}
