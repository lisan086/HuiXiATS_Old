using CommLei.GongYeJieHe;
using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataChuLi
{
    /// <summary>
    /// 解析带长度数据
    /// </summary>
    public class SerialDaiChangDuDataJieXi
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
        public SerialDaiChangDuDataJieXi()
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
        /// 完美解析
        /// </summary>
        /// <param name="qianduanjuku"></param>
        /// <param name="jingeshu"></param>
        /// <param name="shujuchangdu"></param>
        /// <param name="tezhengfu"></param>
        /// <param name="shifoujiaoyan"></param>
        /// <param name="jiaoyanhanshu"></param>
        public void JieXiWanMeiData(List<byte> qianduanjuku, Dictionary<int, byte> tezhengfu, int jingeshu, int shujuchangdu, bool shifoujiaoyan, Func<List<byte>, bool> jiaoyanhanshu)
        {
            if (qianduanjuku == null || tezhengfu == null)
            {
                return;
            }
            if (qianduanjuku.Count == 0 || shujuchangdu == 0 || tezhengfu.Count == 0)
            {
                return;
            }
            if (shifoujiaoyan)
            {
                if (jiaoyanhanshu == null)
                {
                    return;
                }
            }
            if (_ShuJu.Count == 0)
            {
                return;
            }
            int count = _ShuJu.Count;
            while (_ShuJu.Count > qianduanjuku.Count + jingeshu + shujuchangdu)
            {
                int zhaochu = _ShuJu.HanYouJiHe(qianduanjuku);

                if (zhaochu >= 0)
                {
                    if (zhaochu > 0)
                    {
                        MoveData(zhaochu);
                    }
                    if (_ShuJu.Count >= jingeshu + shujuchangdu + qianduanjuku.Count)
                    {
                        List<byte> huoqu = _ShuJu.GetRange(qianduanjuku.Count + jingeshu, shujuchangdu);

                        int changdu = GetChangDu(huoqu);

                        if (changdu <= 0)
                        {
                            _ShuJu.RemoveAt(0);
                        }
                        else
                        {
                            bool zhen = true;
                            foreach (var item in tezhengfu.Keys)
                            {
                                if (_ShuJu.Count > item)
                                {
                                    if (_ShuJu[item] != tezhengfu[item])
                                    {
                                        zhen = false;
                                        break;
                                    }
                                }
                            }
                            if (zhen == false)
                            {
                                _ShuJu.RemoveAt(0);
                            }
                            else
                            {
                                if (_ShuJu.Count >= jingeshu + shujuchangdu + qianduanjuku.Count + changdu)
                                {
                                    List<byte> xinjiequ = _ShuJu.GetRange(0, jingeshu + shujuchangdu + qianduanjuku.Count + changdu);
                                    if (shifoujiaoyan)
                                    {
                                        if (jiaoyanhanshu(xinjiequ))
                                        {
                                            _LisDataByte.Add(xinjiequ.ToArray());
                                            _ShuJu.RemoveRange(0, jingeshu + shujuchangdu + qianduanjuku.Count + changdu);
                                        }
                                        else
                                        {
                                            int zhaochus = _ShuJu.IndexOf(qianduanjuku[0], 1);
                                            if (zhaochus < 0)
                                            {
                                                _ShuJu.RemoveRange(0, count);
                                            }
                                            else
                                            {
                                                MoveData(zhaochus);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _LisDataByte.Add(xinjiequ.ToArray());
                                        _ShuJu.RemoveRange(0, jingeshu + shujuchangdu + qianduanjuku.Count + changdu);
                                    }


                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    int zhaochus = _ShuJu.IndexOf(qianduanjuku[0], 1);
                    if (zhaochus < 0)
                    {
                        _ShuJu.RemoveRange(0, count);
                    }
                    else
                    {
                        MoveData(zhaochus);
                    }
                }

            }
        }



        private int GetChangDu(List<byte> changdu)
        {
            int chazhi = 4 - changdu.Count;
            if (chazhi > 0)
            {
                for (int i = 0; i < chazhi; i++)
                {
                    changdu.Insert(0, 0x00);
                }
            }
            if (BitConverter.IsLittleEndian)
            {
                List<byte> ssd = new List<byte>()
                {
                    changdu[3],
                    changdu[2],
                    changdu[1],
                    changdu[0],
                };
                return BitConverter.ToInt32(ssd.ToArray(), 0);
            }
            else
            {
                return BitConverter.ToInt32(changdu.ToArray(), 0);
            }
        }
        private void MoveData(int shuliang)
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
        #endregion

    }
}
