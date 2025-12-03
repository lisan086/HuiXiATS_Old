using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System.ComponentModel;

namespace CommLei.JiChuLei
{
    /// <summary>
    /// 常用的静态类  数据转换  日期天数获取 josn 字符分割 枚举转换
    /// </summary>
    public static class ChangYong
    {
      
        /// <summary>
        /// 尝试去转换 Tvalue  转换的失败值
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static int TryInt(object value, int Tvalue)
        {
            if (value == null)
            {
                return Tvalue;
            }
            int zhi = 0;
            bool ischenggong = int.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }

        /// <summary>
        /// 尝试去转换 Tvalue  转换的失败值
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static short TryShort(object value, short Tvalue)
        {
            if (value == null)
            {
                return Tvalue;
            }
            short zhi = 0;
            bool ischenggong = short.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }

        /// <summary>
        /// 尝试去转换 Tvalue  转换的失败值
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static uint TryUInt(object value, uint Tvalue)
        {
            if (value == null)
            {
                return Tvalue;
            }
            uint zhi = 0;
            bool ischenggong = uint.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }

        /// <summary>
        /// 尝试去转换 Tvalue  转换的失败值
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static uint TryUShort(object value, ushort Tvalue)
        {
            if (value == null)
            {
                return Tvalue;
            }
            ushort zhi = 0;
            bool ischenggong = ushort.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }

        /// <summary>
        /// 尝试去转换  Tvalue  转换的失败值
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static long TryLong(object value, long Tvalue)
        {
            if (value==null)
            {
                return Tvalue;
            }
            long zhi = 0;
            bool ischenggong = long.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }
        /// <summary>
        /// 尝试去转换  Tvalue  转换的失败值
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static float TryFloat(object value, float Tvalue)
        {
            if (value==null)
            {
                return Tvalue;
            }
            float zhi = 0;
            bool ischenggong = float.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }

        /// <summary>
        /// 尝试去转换Tvalue  转换的失败值
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static double TryDouble(object value, double Tvalue)
        {
            if (value == null)
            {
                return Tvalue;
            }

            double zhi = 0;
            bool ischenggong = double.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }
        /// <summary>
        /// 尝试去转换
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static byte TryByte(string value, byte Tvalue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Tvalue;
            }
            byte zhi = 0;
            bool ischenggong = byte.TryParse(value.ToString(), out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }

        /// <summary>
        /// 尝试去转换
        /// </summary>
        /// <param name="value">string类型</param>
        /// <param name="Tvalue">T类型的值</param>
        /// <returns></returns>
        public static DateTime TryDate(string value, DateTime Tvalue)
        {
            if (string.IsNullOrEmpty(value))
            {

                return Tvalue;
            }

            DateTime zhi = DateTime.Now;
            bool ischenggong = DateTime.TryParse(value, out zhi);
            if (ischenggong)
            {
                return zhi;
            }
            else
            {
                return Tvalue;
            }
        }

        /// <summary>
        /// 尝试转换字符串，如果字符串是string类型，是空数据的话，就会取ShiBaiZhi
        /// </summary>
        /// <param name="Value">泛型值</param>
        /// <param name="ShiBaiZhi">失败时给出的值</param>
        /// <returns></returns>
        public static string TryStr(object Value, string ShiBaiZhi)
        {
            string zhi = "";
            try
            {
                if (Value != null)
                {
                    zhi = Value.ToString();
                }
                else
                {
                    zhi = ShiBaiZhi;
                }
            }
            catch
            {
                zhi = ShiBaiZhi;

            }
            return zhi;
        }
        /// <summary>
        /// 把对象转换成josn
        /// </summary>       
        /// <param name="model"></param>
        /// <returns></returns>
        public static string HuoQuJsonStr(object model)
        {
            string jieguo = "";
            if (model != null)
            {
                jieguo = JsonConvert.SerializeObject(model);
            }
            return jieguo;
        }
        /// <summary>
        /// 获取匿名动态对象,
        /// </summary>
        /// <param name="josn">josn内容</param>
        /// <returns></returns>
        public static dynamic GetNiMingJosn(string josn)
        {
            if (string.IsNullOrEmpty(josn))
            {
                return null;
            }
            try
            {
                dynamic dyn = JObject.Parse(josn);
                return dyn;
            }
            catch
            {

                return null;
            }
        }

        /// <summary>
        /// 获取匿名动态对象,
        /// </summary>
        /// <param name="josn">josn内容</param>
        /// <returns></returns>
        public static dynamic[] GetNiMingJiHeJosn(string josn)
        {
            if (string.IsNullOrEmpty(josn))
            {
                return null;
            }
            try
            {
                var jsonarr = JsonConvert.DeserializeObject<dynamic[]>(josn);
                return jsonarr;
            }
            catch
            {

                return null;
            }
        }

        /// <summary>
        /// josn实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Josn"></param>
        /// <returns></returns>
        public static T HuoQuJsonToShiTi<T>(string Josn)
        {
            try
            {
                T shiti = JsonConvert.DeserializeObject<T>(Josn);
                return shiti;
            }
            catch
            {


            }
            return default(T);
        }
        /// <summary>
        /// 复制新的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T FuZhiShiTi<T>(T model)
        {
            try
            {
                T shiti = JsonConvert.DeserializeObject<T>(HuoQuJsonStr(model));
                return shiti;
            }
            catch
            {


            }
            return default(T);
        }
        /// <summary>
        /// 如果格式一样可以复制新的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T FuZhiShiTi<T, V>(V model)
        {
            try
            {
                T shiti = JsonConvert.DeserializeObject<T>(HuoQuJsonStr(model));
                return shiti;
            }
            catch
            {


            }
            return default(T);
        }

        /// <summary>
        /// 用于计算页数
        /// </summary>
        /// <param name="Row">单位行</param>
        /// <param name="AllRow">总行</param>
        /// <returns></returns>
        public static int PageYeShu(int Row, int AllRow)
        {
            int count = 1;
            try
            {
                float frow = Row;
                float allrow = AllRow;
                if (AllRow != 0)
                {
                    float fcount = allrow / Row;
                    count = AllRow / Row;
                    if (fcount > count)
                    {
                        count = count + 1;
                    }

                }
            }
            catch
            {
                count = 1;
            }
            return count;
        }

        /// <summary>
        /// 把字节数据转换标准字符串报文格式16进制，以什么进行分割
        /// </summary>
        /// <param name="shuju"></param>
        /// <param name="fengezhiduan"></param>
        /// <returns></returns>
        public static string ByteOrString(List<byte> shuju, string fengezhiduan)
        {
            if (shuju==null)
            {
                return "";
            }
            int count = shuju.Count;
            if (count == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (i == count - 1)
                {
                    sb.Append(shuju[i].ToString("X2"));
                }
                else
                {
                    sb.Append(string.Format("{0}{1}", shuju[i].ToString("X2"), fengezhiduan));
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 把字节数据转换标准字符串报文格式16进制，以什么进行分割
        /// </summary>
        /// <param name="shuju"></param>
        /// <param name="fengezhiduan"></param>
        /// <returns></returns>
        public static string ByteOrString(byte[] shuju, string fengezhiduan)
        {
            if (shuju==null||  shuju.Length== 0)
            {
                return "";
            }
            int count = shuju.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (i == count - 1)
                {
                    sb.Append(shuju[i].ToString("X2"));
                }
                else
                {
                    sb.Append(string.Format("{0}{1}", shuju[i].ToString("X2"), fengezhiduan));
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 把一个对象进行分割起来 例如objeec 对象 转为1,2,1,3,1这样子
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeec">分割对象</param>
        /// <param name="fengefu">分隔符</param>
        /// <returns></returns>
        public static string FenGeDaBao<T>(List<T> objeec, string fengefu)
        {
            StringBuilder sb = new StringBuilder();
            if (objeec != null && objeec.Count > 0)
            {
                int count = objeec.Count;
                for (int i = 0; i < count; i++)
                {
                    if (i == count - 1)
                    {
                        sb.Append(string.Format("{0}", objeec[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0}{1}", objeec[i], fengefu));
                    }
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 把一个对象进行分割起来 例如objeec 对象 转为1,2,1,3,1这样子
        /// </summary>       
        /// <param name="zifuchuan">分割对象</param>
        /// <param name="fengefu">分隔符</param>
        /// <returns></returns>
        public static List<string> JieGeStr(string zifuchuan, char fengefu)
        {
            if (string.IsNullOrEmpty(zifuchuan))
            {
                return new List<string>();
            }
            List<string> jie = new List<string>();
            string[] femge = zifuchuan.Split(fengefu);
            if (femge.Length > 0)
            {
                jie = femge.ToList();
            }
            return jie;

        }

        /// <summary>
        /// 把一个对象进行分割起来 例如objeec 对象 转为1,2,1,3,1这样子
        /// </summary>      
        /// <param name="zifuchuan">分割对象</param>
        /// <param name="fengefu">分隔符</param>
        /// <returns></returns>
        public static List<int> JieGeInt(string zifuchuan, char fengefu)
        {
            List<int> jie = new List<int>();
            string[] femge = zifuchuan.Split(fengefu);
            if (femge.Length > 0)
            {
                int count = femge.Length;
                for (int i = 0; i < count; i++)
                {
                    if (string.IsNullOrEmpty(femge[i]))
                    {
                        continue;
                    }
                    int value = -99;
                    bool zhuanhuan = int.TryParse(femge[i], out value);
                    if (zhuanhuan)
                    {
                        jie.Add(value);
                    }
                }

            }
            return jie;

        }

        /// <summary>
        /// 把一个对象进行分割起来 例如objeec 对象 转为1,2,1,3,1这样子
        /// </summary>
        /// <param name="zifuchuan">分割对象</param>
        /// <param name="fengefu">分隔符</param>
        /// <returns></returns>
        public static List<float> JieGeFlaot(string zifuchuan, char fengefu)
        {
            List<float> jie = new List<float>();
            string[] femge = zifuchuan.Split(fengefu);
            if (femge.Length > 0)
            {
                int count = femge.Length;
                for (int i = 0; i < count; i++)
                {
                    if (string.IsNullOrEmpty(femge[i]))
                    {
                        continue;
                    }
                    float value = -99;
                    bool zhuanhuan = float.TryParse(femge[i], out value);
                    if (zhuanhuan)
                    {
                        jie.Add(value);
                    }
                }

            }
            return jie;

        }

        /// <summary>
        /// 把一个对象进行分割起来 例如objeec 对象 转为1,2,1,3,1这样子
        /// </summary>
        /// <param name="zifuchuan">分割对象</param>
        /// <param name="fengefu">分隔符</param>
        /// <returns></returns>
        public static List<double> JieGeDouble(string zifuchuan, char fengefu)
        {
            List<double> jie = new List<double>();
            string[] femge = zifuchuan.Split(fengefu);
            if (femge.Length > 0)
            {
                int count = femge.Length;
                for (int i = 0; i < count; i++)
                {
                    if (string.IsNullOrEmpty(femge[i]))
                    {
                        continue;
                    }
                    double value = -99;
                    bool zhuanhuan = double.TryParse(femge[i], out value);
                    if (zhuanhuan)
                    {
                        jie.Add(value);
                    }
                }

            }
            return jie;

        }

        /// <summary>
        /// 查找是否含有 含有返回所在index  否则返回-1，数量在1000000 一百以下 都是比较快的,目前只支持int double flaot，byte，string，连续的
        /// </summary>
        /// <param name="socbyte">源数据</param>
        /// <param name="mubiao">包含数据</param>
        /// <returns></returns>
        public static int LisFindHanYou<T>(List<T> socbyte, List<T> mubiao)
        {

            if (socbyte == null || mubiao == null)
            {
                return -1;
            }
            if (socbyte.Count == 0)
            {
                return -1;
            }
            if (socbyte.Count < mubiao.Count)
            {
                return -1;
            }
            if (mubiao.Count == 0)
            {
                return -1;
            }
            if (mubiao.Count == 1)
            {
                int indexof = socbyte.IndexOf(mubiao[0]);
                return indexof;
            }
            else
            {

                int indexof = socbyte.IndexOf(mubiao[0]);
                if (indexof >= 0)
                {
                    if ((indexof + 1) >= socbyte.Count)
                    {
                        return -1;
                    }
                    int indexof1 = socbyte.IndexOf(mubiao[1], indexof + 1);
                    if (indexof1 > indexof)
                    {
                        for (; true;)
                        {
                            if ((indexof + 1) == indexof1)
                            {
                                int chazhi = mubiao.Count - 2;
                                if (chazhi == 0)
                                {
                                    return indexof;
                                }
                                else
                                {
                                    if ((indexof1 + chazhi) >= socbyte.Count)
                                    {
                                        return -1;
                                    }
                                    bool cunzai = socbyte.Skip(indexof).Take(mubiao.Count).SequenceEqual(mubiao);
                                    if (cunzai)
                                    {
                                        return indexof;
                                    }
                                    else
                                    {

                                        if ((indexof + 1) >= socbyte.Count)
                                        {
                                            return -1;
                                        }

                                        indexof = socbyte.IndexOf(mubiao[0], indexof + 1);

                                        if ((indexof + 1) >= socbyte.Count)
                                        {
                                            return -1;
                                        }

                                        indexof1 = socbyte.IndexOf(mubiao[1], indexof + 1);
                                        if (indexof1 < indexof)
                                        {
                                            return -1;
                                        }
                                        if (indexof < 0 || indexof1 < 0)
                                        {
                                            return -1;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if ((indexof + 1) >= socbyte.Count)
                                {
                                    return -1;
                                }

                                indexof = socbyte.IndexOf(mubiao[0], indexof + 1);

                                if ((indexof + 1) >= socbyte.Count)
                                {
                                    return -1;
                                }

                                indexof1 = socbyte.IndexOf(mubiao[1], indexof + 1);
                                if (indexof1 < indexof)
                                {
                                    return -1;
                                }
                                if (indexof < 0 || indexof1 < 0)
                                {
                                    return -1;
                                }
                            }
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                return -1;
            }
        }

        /// <summary>
        /// 把枚举转名称转化为list
        /// </summary>
        /// <param name="meijulei"></param>
        /// <returns></returns>
        public static List<string> MeiJuLisName(Type meijulei)
        {
            List<string> shuju = new List<string>();
            foreach (var suit in Enum.GetValues(meijulei))
            {
                shuju.Add(suit.ToString());
            }
            return shuju;
        }
        /// <summary>
        /// 获取枚举值，枚举的字符串名称
        /// </summary>
        /// <param name="meiju"></param>
        /// <param name="mingcheng"></param>
        /// <returns></returns>
        public static object GetMeiJuZhi(Type meiju, string mingcheng)
        {
            FieldInfo[] shuxin1 = meiju.GetFields();
            for (int j = 0; j < shuxin1.Length; j++)
            {
                if (shuxin1[j].Name.Equals(mingcheng))
                {
                    return shuxin1[j].GetRawConstantValue();

                }
            }
            return null;
        }

        /// <summary>
        /// 获取枚举值，通过传入枚举的名称就可以相对应的枚举
        /// </summary>
        /// <param name="mingcheng"></param>
        /// <returns></returns>
        public static T GetMeiJuZhi<T>(string mingcheng) where  T:Enum
        {
            Type meiju = typeof(T);
            FieldInfo[] shuxin1 = meiju.GetFields();
            for (int j = 0; j < shuxin1.Length; j++)
            {
                if (shuxin1[j].Name.Equals(mingcheng))
                {
                    return (T)shuxin1[j].GetRawConstantValue();

                }
            }
            return default(T);
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBenJiIP()
        {
            string jieqiname = Dns.GetHostName();
            IPAddress[] shuju = Dns.GetHostAddresses(jieqiname);
            List<string> vs = new List<string>();
            if (shuju!=null&&shuju.Length > 0)
            {
                foreach (var item in shuju)
                {
                    vs.Add(item.ToString());
                }
            }
            return vs;
        }

        /// <summary>
        /// 获取本机的com
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBenJiCom()
        {
            string[] liss = SerialPort.GetPortNames();
            List<string> vs = new List<string>();
            if (liss != null && liss.Length > 0)
            {
                foreach (var item in liss)
                {
                    vs.Add(item.ToString());
                }
            }
            return vs;
        }

        /// <summary>
        /// 获取文件的后缀
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string GetWenJianHouZui(string filename)
        {
            try
            {
                return Path.GetExtension(filename);
            }
            catch
            {

                return "";
            }
        }

        /// <summary>
        /// 获取文件名，无后缀的,不包含目录部分
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetWenJianName(string filename)
        {
            try
            {
                return Path.GetFileNameWithoutExtension(filename);
            }
            catch
            {

                return "";
            }
        }
        /// <summary>
        /// 获取文件名，有后缀的 不包含目录部分
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetWenJianQuanName(string filename)
        {
            try
            {
                return Path.GetFileName(filename);
            }
            catch
            {

                return "";
            }
        }

        /// <summary>
        /// 获取文件目录部分
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetWenJianMuLu(string filename)
        {
            try
            {
                return Path.GetDirectoryName(filename);
            }
            catch
            {

                return "";
            }
        }


        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        public static DateTime GetGaiYueDY(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetGaiYueZH(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        ///  取得上个月第一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetSYueDY(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(-1);
        }

        /// <summary>
        /// 取得上个月的最后一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetSYueZH(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddDays(-1);
        }

        /// <summary>
        /// 取得当前日期的23：59：59 到 00:00:00
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static List<string> GetDangQianSM(DateTime datetime)
        {
            if (datetime == null)
            {
                return new List<string>();
            }
            List<string> data = new List<string>();
            string kaishi = string.Format("{0} 23:59:59", datetime.AddDays(-1).ToString("yyyy-MM-dd"));
            string JieSu = string.Format("{0} 00:00:00", datetime.AddDays(+1).ToString("yyyy-MM-dd"));
            data.Add(kaishi);
            data.Add(JieSu);
            return data;
        }

        /// <summary>
        /// 取得当前日期的23：59：59 到 00:00:00 qianhoutianshu表示天数，type 1为表示取该时间  2为前面几天到该时间，3为从该时间到后面几天
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="qianhoutianshu"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetRenYiSM(DateTime datetime, int qianhoutianshu, int type)
        {
            if (datetime == null || qianhoutianshu < 0 || type > 3 || type < 0)
            {
                return new List<string>();
            }
            List<string> data = new List<string>();
            switch (type)
            {
                case 1:
                    {
                        data.Clear();
                        string kaishi = string.Format("{0} 23:59:59", datetime.AddDays(-1).ToString("yyyy-MM-dd"));
                        string JieSu = string.Format("{0} 00:00:00", datetime.AddDays(+1).ToString("yyyy-MM-dd"));
                        data.Add(kaishi);
                        data.Add(JieSu);
                    }
                    break;
                case 2:
                    {
                        data.Clear();
                        string kaishi = string.Format("{0} 23:59:59", datetime.AddDays(-qianhoutianshu - 1).ToString("yyyy-MM-dd"));
                        string JieSu = string.Format("{0} 00:00:00", datetime.AddDays(+1).ToString("yyyy-MM-dd"));
                        data.Add(kaishi);
                        data.Add(JieSu);
                    }
                    break;
                case 3:
                    {
                        data.Clear();
                        string kaishi = string.Format("{0} 23:59:59", datetime.AddDays(-1).ToString("yyyy-MM-dd"));
                        string JieSu = string.Format("{0} 00:00:00", datetime.AddDays(qianhoutianshu + 1).ToString("yyyy-MM-dd"));
                        data.Add(kaishi);
                        data.Add(JieSu);
                    }
                    break;
                default:
                    break;
            }

            return data;
        }
        /// <summary>
        /// 周的星期几
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int ZhouDiJiTian(DateTime dt)
        {
            int DayinWeek = 0;
            string week = dt.DayOfWeek.ToString();
            switch (week)
            {
                case "Monday":
                    DayinWeek = 1;
                    break;
                case "Tuesday":
                    DayinWeek = 2;
                    break;
                case "Wednesday":
                    DayinWeek = 3;
                    break;
                case "Thursday":
                    DayinWeek = 4;
                    break;
                case "Friday":
                    DayinWeek = 5;
                    break;
                case "Saturday":
                    DayinWeek = 6;
                    break;
                case "Sunday":
                    DayinWeek = 7;
                    break;
            }

            return DayinWeek;
        }

        /// <summary>
        /// 月的第几天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int YueDiJiTian(DateTime dt)
        {
            int DayinWeek = 0;
            DayinWeek = dt.Day;
            return DayinWeek;
        }

        /// <summary>
        /// 清除事件所有注册的方法 duixiang 事件的类 eventName事件名称
        /// </summary>
        /// <param name="duixiang"></param>
        /// <param name="eventName"></param>
        public static void ClearEvent(object duixiang, string eventName)
        {
            if (duixiang == null)
            {
                return;
            }
            try
            {
                EventInfo[] events = duixiang.GetType().GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (events == null || events.Length < 1)
                {
                    return;
                }

                for (int i = 0; i < events.Length; i++)
                {
                    EventInfo ei = events[i];
                    if (ei.Name == eventName)
                    {
                        FieldInfo fi = ei.DeclaringType.GetField(eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fi != null)
                        {
                            fi.SetValue(duixiang, null);
                        }
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 把Ansi转换为Uif
        /// </summary>
        /// <param name="AnsiString"></param>
        /// <returns></returns>
        public static string Ansi_To_Uif(string AnsiString)
        {
            // string zhuanhuan = EncodeString(UtfString);
            //用convet直接转换
            //byte[] buffer1 = Encoding.Unicode.GetBytes(zhuanhuan);
            //byte[] buffer2 = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, buffer1, 0, buffer1.Length);
            //string strBuffer = Encoding.UTF8.GetString(buffer2, 0, buffer2.Length);
            try
            {
                char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
                Encoding utf8 = Encoding.UTF8;
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < AnsiString.Length; i++)
                {
                    string sub = AnsiString.Substring(i, 1);
                    byte[] bytes = utf8.GetBytes(sub);
                    if (bytes.Length == 1) //普通英文字母或数字
                    {
                        result.Append(sub);
                    }
                    else  //其它字符，转换成为编码
                    {
                        for (int j = 0; j < bytes.Length; j++)
                        {
                            result.Append("%" + hexDigits[bytes[j] >> 4] + hexDigits[bytes[j] & 0XF]);
                        }
                    }
                }
                return result.ToString();
            }
            catch
            {

                return "";
            }




            //  return UtfString;

        }

        /// <summary>
        /// 10转2进制
        /// </summary>
        /// <param name="zhi"></param>
        /// <param name="shuliang"></param>
        /// <returns></returns>
        public static List<int> Get10Or2(int zhi,int shuliang)
        {
            List<int> shuju = new List<int>();
            while (zhi != 0)
            {
                shuju.Add(zhi % 2);
                zhi = zhi / 2;
            }
            int count = shuju.Count;
            if (count< shuliang)
            {
                int chazhi = shuliang - count;
                for (int i = 0; i < chazhi; i++)
                {
                    shuju.Add(0);
                }
            }
            return shuju;
        }

        /// <summary>
        /// string 类型的数据截取
        /// </summary>
        /// <param name="startValue">开始截取的字符串</param>
        /// <param name="endValue">末尾截取字符串</param>
        ///  <param name="strData">原始值</param>
        ///  <param name="isbaoliu">1保留末尾与原始值</param>
        /// <returns></returns>
        public static string StrDataCut(string strData, string startValue, string endValue,int isbaoliu=1)
        {
            string jiequ = "";
            int index = strData.IndexOf(startValue);
            int index1 = strData.IndexOf(endValue);
            
            try
            {
                if (index<0|| index1<0)
                {
                    return "";
                }
                if (index1< index)
                {
                    return "";
                }
                if (isbaoliu == 1)
                {
                    jiequ = strData.Substring(index, index1 - index + endValue.Length);
                }
                else if (isbaoliu == 2)
                {
                    jiequ = strData.Substring(index, index1 - index + endValue.Length);
                    jiequ = jiequ.Replace(startValue, "").Replace(endValue, "");
                }
            }
            catch
            {

                jiequ = "";
            }


            return jiequ;
        }

        /// <summary>
        /// 把16进制的字符串的报文转换为byte[]数组
        /// </summary>
        /// <param name="hexstring"></param>
        /// <returns></returns>
        public static byte[] HexStringToByte(string hexstring)
        {
            string hexstringxin = hexstring.Trim().Replace(" ","");
            if (string.IsNullOrEmpty(hexstringxin))
            {
                return new byte[0];
            }
            List<string> lisstr = new List<string>();
            int count = hexstringxin.Length;
            int yushu = count % 2;
            if (yushu == 0)
            {              
                for (int i = 0; i < count; i += 2)
                {
                    lisstr.Add(string.Format("{0}{1}", hexstringxin[i], hexstringxin[i + 1]));
                }
            }
            else
            {
                for (int i = 0; i < count-1; i += 2)
                {
                    lisstr.Add(string.Format("{0}{1}", hexstringxin[i], hexstringxin[i + 1]));
                }
                lisstr.Add(string.Format("{0}", hexstringxin[count - 1]).PadLeft(2,'0'));
            }
            StringBuilder sb1 = new StringBuilder();
            if (lisstr.Count > 0)
            {
                for (int i = 0; i < lisstr.Count; i++)
                {
                    if (i == lisstr.Count - 1)
                    {
                        sb1.Append(string.Format("{0}", lisstr[i]));
                    }
                    else
                    {
                        sb1.Append(string.Format("{0} ", lisstr[i]));
                    }
                }
            }

            string[] tmpary = sb1.ToString().Split(' ');
            byte[] buff = new byte[tmpary.Length];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = Convert.ToByte(tmpary[i], 16);
            }
            return buff;
        }

        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return "";
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }
}
