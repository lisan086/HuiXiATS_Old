using CommLei.GongYeJieHe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.DataChuLi
{
    /// <summary>
    /// 分析串口固定数据的类(主要是收集)
    /// </summary>
    public class SerialDataGuDingJieXi
    {
        /// <summary>
        /// 接收数据的存储
        /// </summary>
        private List<byte> _ShuJu = null;

        /// <summary>
        /// 获取的解析出来的数据集合
        /// </summary>
        private FanXingJiHeLei<byte[]> _LisDataByte;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SerialDataGuDingJieXi()
        {
            _ShuJu = new List<byte>();
            _LisDataByte = new FanXingJiHeLei<byte[]>();

        }

        /// <summary>
        /// 数据多少
        /// </summary>
        public int DataCount
        {
            get
            {
                int count = _LisDataByte.GetCount();
                return count;
            }
        }
        #region 获取解析数据的集合
        /// <summary>
        /// 获取当前数据，就是实时数据,移除以前的
        /// </summary>
        /// <returns></returns>
        public byte[] GetShiShiData()
        {
            byte[] data = _LisDataByte.GetModel_End_RomeALL();
            return data;
        }
        /// <summary>
        /// 从开头获取数据，移除首次
        /// </summary>
        /// <returns></returns>
        public byte[] GetHeadAndRomveData()
        {
            byte[] data = _LisDataByte.GetModel_Head_RomeHead();
            return data;
        }
        #endregion

        #region 增加数据
        /// <summary>
        /// 增加串口返回的数据
        /// </summary>
        /// <param name="bytearr">数据源</param>
        public void AddByteList(byte[] bytearr)
        {
            _ShuJu.AddRange(bytearr);
        }

        /// <summary>
        /// 增加单个数据
        /// </summary>
        /// <param name="byt">单个数据</param>
        public void AddByte(byte byt)
        {
            _ShuJu.Add(byt);
        }

        /// <summary>
        ///清理数据
        /// </summary>
        public void Clear()
        {
            _LisDataByte.Romve_All();
            _ShuJu.Clear();

        }

        #endregion

        #region 解析大数据
     
        /// <summary>
        /// 完美解析数据
        /// </summary>
        /// <param name="shijichangdu">数据的实际长度，就是报文的长度</param>
        /// <param name="qianduanjuku">开头几位数据</param>
        /// <param name="shifoujiaoyan">是否需要校验，如果不校验，委托可以穿null</param>
        /// <param name="jiaoyanhanshu">校验函数</param>
        /// <param name="tiaochu">是否在查第一次就跳出</param>
        public void JieXiWanMeiData(int shijichangdu, byte[] qianduanjuku, bool shifoujiaoyan, Func<List<byte>, bool> jiaoyanhanshu, bool tiaochu)
        {

            if (shifoujiaoyan)
            {
                #region 校验部分
                if (_ShuJu.Count >= shijichangdu)
                {
                    DiGuiADDJiaoYanNew(qianduanjuku, shijichangdu, jiaoyanhanshu, tiaochu);
                }
                #endregion

            }
            else
            {
                #region 非校验部分
                if (_ShuJu.Count >= shijichangdu)
                {
                    DiGuiADDNew(qianduanjuku, shijichangdu, tiaochu);

                }
                #endregion
            }
        }
        #endregion

        #region 私有递归思想
        private void DiGuiNeww(List<byte> lianshijihe1, byte[] xinjiehe, int shijichangdu)
        {
            int fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], 0);
            int chushiliang = xinjiehe.Length;
            while (fanhuisuoyin >= 0)
            {
                try
                {
                    #region MyRegion
                    bool zhen = true;
                    if (lianshijihe1.Count - fanhuisuoyin >= shijichangdu)
                    {
                        for (int i = 0; i < chushiliang - 1; i++)
                        {
                            if (lianshijihe1[fanhuisuoyin + i + 1] == xinjiehe[i + 1])
                            {

                            }
                            else
                            {
                                zhen = false;
                                break;
                            }
                        }
                        if (zhen)
                        {
                            #region MyRegion
                            List<byte> xinshuju = new List<byte>();
                            xinshuju.Clear();
                            for (int i = fanhuisuoyin; i < fanhuisuoyin + shijichangdu; i++)
                            {
                                xinshuju.Add(lianshijihe1[i]);
                            }

                            fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], fanhuisuoyin + 1);

                            _LisDataByte.Add(xinshuju.ToArray());
                            if (fanhuisuoyin < 0)
                            {
                                break;
                            }
                            #endregion

                        }
                        else
                        {
                            fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], fanhuisuoyin + 1);
                        }
                    }
                    else
                    {
                        break;
                    }
                    #endregion
                }
                catch
                {


                }


            }
            return;

        }


        private List<byte> DiGui1New(List<byte> lianshijihe1, byte[] xinjiehe, int shijichangdu, Func<List<byte>, bool> jiaoyanhanshu)
        {

            List<byte> xinshuju = new List<byte>();
            xinshuju.Clear();
            int fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], 0);
            int chushiliang = xinjiehe.Length;
            while (fanhuisuoyin >= 0)
            {
                try
                {
                    #region MyRegion
                    List<byte> xinshuju1 = new List<byte>();
                    bool zhen = true;
                    if (lianshijihe1.Count - fanhuisuoyin >= shijichangdu)
                    {
                        for (int i = 0; i < chushiliang - 1; i++)
                        {
                            if (lianshijihe1[fanhuisuoyin + i + 1] == xinjiehe[i + 1])
                            {

                            }
                            else
                            {
                                zhen = false;
                                break;
                            }
                        }
                        if (zhen)
                        {

                            for (int i = fanhuisuoyin; i < fanhuisuoyin + shijichangdu; i++)
                            {
                                xinshuju1.Add(lianshijihe1[i]);
                            }
                            if (jiaoyanhanshu(xinshuju1))
                            {
                                xinshuju = xinshuju1;

                                fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], fanhuisuoyin + 1);
                                if (fanhuisuoyin < 0)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                xinshuju.Clear();
                                fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], fanhuisuoyin + 1);
                            }
                        }
                        else
                        {
                            fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], fanhuisuoyin + 1);
                        }
                    }
                    else
                    {
                        break;
                    }
                    #endregion
                }
                catch
                {

                }


            }
            return xinshuju;

        }

        private List<byte> DiGui2New(List<byte> lianshijihe1, byte[] xinjiehe, int shijichangdu, Func<List<byte>, bool> jiaoyanhanshu)
        {

            List<byte> xinshuju = new List<byte>();
            xinshuju.Clear();
            int fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], 0);
            int chushiliang = xinjiehe.Length;
            while (fanhuisuoyin >= 0)
            {
                try
                {
                    #region MyRegion
                    List<byte> xinshuju1 = new List<byte>();
                    if (lianshijihe1.Count - fanhuisuoyin >= shijichangdu)
                    {
                        for (int i = fanhuisuoyin; i < fanhuisuoyin + shijichangdu; i++)
                        {
                            xinshuju1.Add(lianshijihe1[i]);
                        }
                        if (jiaoyanhanshu(xinshuju1))
                        {
                            xinshuju = xinshuju1;
                            xinshuju.ForEach((x) => lianshijihe1.Remove(x));
                            fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], fanhuisuoyin + 1);
                            if (fanhuisuoyin < 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            xinshuju.Clear();
                            fanhuisuoyin = lianshijihe1.IndexOf(xinjiehe[0], fanhuisuoyin + 1);
                        }

                    }
                    else
                    {
                        break;
                    }
                    #endregion
                }
                catch
                {


                }


            }
            return xinshuju;

        }

        private void DiGuiADDJiaoYanNew(byte[] xinjiehe, int shijichangdu, Func<List<byte>, bool> jiaoyanhanshu, bool tiaochu)
        {

            while (_ShuJu.Count >= shijichangdu)
            {
                try
                {
                    #region 函数部分
                    int jiluchangsu = _ShuJu.Count - shijichangdu;
                    List<byte> lianshishuju = new List<byte>();
                    for (int i = 0; i < shijichangdu; i++)
                    {
                        lianshishuju.Add(_ShuJu[i]);
                    }
                    if (xinjiehe == null || xinjiehe.Length == 0)
                    {
                        #region 没有传进来

                        if (jiaoyanhanshu(lianshishuju))
                        {
                            _LisDataByte.Add(lianshishuju.ToArray());
                            lianshishuju.ForEach((x) => _ShuJu.Remove(x));
                            if (_ShuJu.Count < shijichangdu)
                                break;
                            if (tiaochu)
                            {
                                break;
                            }
                        }
                        else
                        {
                            // _LisDataByte.Add(lianshishuju.ToArray());
                            lianshishuju.ForEach((x) => _ShuJu.Remove(x));
                            if (_ShuJu.Count < shijichangdu)
                                break;
                            if (tiaochu)
                            {
                                break;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        int fanhuisuoyin = lianshishuju.IndexOf(xinjiehe[0], 0);
                        if (fanhuisuoyin < 0)
                        {
                            move(shijichangdu);
                            if (tiaochu)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (fanhuisuoyin > 0)
                            {
                                int moveshuliang = (fanhuisuoyin) - 0;
                                move(moveshuliang);
                                if (tiaochu)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                int suoying = 0;
                                #region 在零位置起步
                                if (DiGuiXinJiaoYan(lianshishuju, xinjiehe, jiaoyanhanshu, out suoying))
                                {

                                    _LisDataByte.Add(lianshishuju.ToArray());
                                    lianshishuju.ForEach((x) => _ShuJu.Remove(x));
                                    if (_ShuJu.Count < shijichangdu)
                                        break;
                                }
                                else
                                {
                                    if (suoying < 0)
                                    {
                                        lianshishuju.ForEach((x) => _ShuJu.Remove(x));
                                        if (tiaochu)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        int moveshuliang = (suoying) - 0;
                                        move(moveshuliang);
                                        if (tiaochu)
                                        {
                                            break;
                                        }

                                    }
                                }
                                #endregion

                            }
                        }
                    }
                    #endregion
                }
                catch
                {


                }

            }
        }

        private bool DiGuiXinJiaoYan(List<byte> lianshijihe1, byte[] xinjiehe, Func<List<byte>, bool> jiaoyanhanshu, out int xinshuoying)
        {

            bool zhenshi = true;
            bool zhen = true;
            int chushiliang = xinjiehe.Length;
            for (int i = 0; i < chushiliang - 1; i++)
            {
                if (lianshijihe1[i + 1] == xinjiehe[i + 1])
                {

                }
                else
                {
                    zhen = false;
                    break;
                }
            }
            if (zhen)
            {
                xinshuoying = -5;

                if (jiaoyanhanshu(lianshijihe1))
                {
                    zhenshi = true;
                }
                else
                {
                    zhenshi = false;
                    xinshuoying = lianshijihe1.IndexOf(xinjiehe[0], 1);
                }
            }
            else
            {
                zhenshi = false;
                xinshuoying = lianshijihe1.IndexOf(xinjiehe[0], 1);
            }

            return zhenshi;

        }

        private void move(int shuliang)
        {
            for (int i = 0; i < shuliang; i++)
            {
                if (_ShuJu.Count > 0)
                {
                    _ShuJu.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }

        private void DiGuiADDNew(byte[] xinjiehe, int shijichangdu, bool tiaochu)
        {

            while (_ShuJu.Count >= shijichangdu)
            {
                try
                {
                    #region 函数部分
                    int jiluchangsu = _ShuJu.Count - shijichangdu;
                    List<byte> lianshishuju = new List<byte>();
                    for (int i = 0; i < shijichangdu; i++)
                    {
                        lianshishuju.Add(_ShuJu[i]);
                    }
                    if (xinjiehe == null || xinjiehe.Length == 0)
                    {
                        #region 没有传进来

                        _LisDataByte.Add(lianshishuju.ToArray());
                        lianshishuju.ForEach((x) => _ShuJu.Remove(x));
                        if (_ShuJu.Count < shijichangdu)
                            break;
                        if (tiaochu)
                        {
                            break;
                        }

                        #endregion
                    }
                    else
                    {
                        int fanhuisuoyin = lianshishuju.IndexOf(xinjiehe[0], 0);
                        if (fanhuisuoyin < 0)
                        {

                            move(shijichangdu);
                            if (tiaochu)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (fanhuisuoyin > 0)
                            {
                                int moveshuliang = (fanhuisuoyin) - 0;
                                move(moveshuliang);
                                if (tiaochu)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                int suoying = 0;
                                #region 在零位置起步
                                if (DiGuiXin(lianshishuju, xinjiehe, out suoying))
                                {

                                    _LisDataByte.Add(lianshishuju.ToArray());
                                    lianshishuju.ForEach((x) => _ShuJu.Remove(x));
                                    if (_ShuJu.Count < shijichangdu)
                                        break;
                                }
                                else
                                {
                                    if (suoying < 0)
                                    {
                                        lianshishuju.ForEach((x) => _ShuJu.Remove(x));
                                        if (tiaochu)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        int moveshuliang = (suoying) - 0;
                                        move(moveshuliang);
                                        if (tiaochu)
                                        {
                                            break;
                                        }
                                    }
                                }
                                #endregion

                            }
                        }
                    }


                    #endregion
                }
                catch
                {


                }

            }
        }

        private bool DiGuiXin(List<byte> lianshijihe1, byte[] xinjiehe, out int xinshuoying)
        {


            bool zhen = true;
            int chushiliang = xinjiehe.Length;
            for (int i = 0; i < chushiliang - 1; i++)
            {
                if (lianshijihe1[i + 1] == xinjiehe[i + 1])
                {

                }
                else
                {
                    zhen = false;
                    break;
                }
            }
            if (zhen)
            {
                xinshuoying = -5;
            }
            else
            {
                xinshuoying = lianshijihe1.IndexOf(xinjiehe[0], 1);
            }

            return zhen;

        }
        #endregion
    }
}
