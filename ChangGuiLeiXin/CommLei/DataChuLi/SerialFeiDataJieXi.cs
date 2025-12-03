using CommLei.GongYeJieHe;
using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.DataChuLi
{
    /// <summary>
    /// 分析串口非固定数据的类(主要是收集)
    /// </summary>
    public class SerialFeiDataJieXi
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
        public SerialFeiDataJieXi()
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
        /// 完美解析数据,true 为跳出
        /// </summary>      
        /// <param name="qianduanjuku">开头几位数据</param>
        /// <param name="jiesufu">结束数据</param>
        /// <param name="shifoujiaoyan">是否需要校验，如果不校验，委托可以穿null</param>
        /// <param name="jiaoyanhanshu">校验函数</param>
        /// <param name="tiaochu">是否在查第一次就跳出</param>
        public void JieXiWanMeiData(List<byte> qianduanjuku, List<byte> jiesufu, bool shifoujiaoyan, Func<List<byte>, bool> jiaoyanhanshu, bool tiaochu)
        {
            if (qianduanjuku == null || jiesufu == null)
            {
                return;
            }
            if (qianduanjuku.Count == 0 || jiesufu.Count == 0)
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
            int jieweishuliang = jiesufu.Count;
            int count = _ShuJu.Count;
            if (shifoujiaoyan)
            {
                if (tiaochu)
                {
                    #region 校验部分

                    int jiesuindex = _ShuJu.HanYouJiHe<byte>(jiesufu);
                    int kaishiindex = _ShuJu.HanYouJiHe<byte>(qianduanjuku);
                    if (kaishiindex >= 0)
                    {
                        if (kaishiindex <= jiesuindex && jiesuindex >= 0)
                        {
                            List<byte> Get = _ShuJu.GetRange(kaishiindex, (jiesuindex - kaishiindex) + jiesufu.Count);
                          
                            if (jiaoyanhanshu != null)
                            {
                                bool zhen = jiaoyanhanshu(Get);
                                if (zhen)
                                {
                                    _LisDataByte.Add(Get.ToArray());
                                    if (kaishiindex > 0)
                                    {
                                        MoveData(kaishiindex);
                                    }
                                    MoveData(Get.Count);
                                }
                                else
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        jiesuindex = _ShuJu.HanYouJiHe<byte>(jiesufu, jiesuindex + 1);
                                        if (jiesuindex >= 0)
                                        {
                                            List<byte> Get1 = _ShuJu.GetRange(kaishiindex, (jiesuindex - kaishiindex) + jiesufu.Count);
                                            bool hege = jiaoyanhanshu(Get1);
                                            if (hege)
                                            {
                                                _LisDataByte.Add(Get.ToArray());
                                                if (kaishiindex > 0)
                                                {
                                                    MoveData(kaishiindex);
                                                }
                                                MoveData(Get1.Count);
                                                break;
                                            }
                                            else
                                            {
                                                if (i == 2)
                                                {
                                                    if (kaishiindex > 0)
                                                    {
                                                        MoveData(kaishiindex);
                                                    }
                                                    MoveData(qianduanjuku.Count);
                                                }
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
                                if (kaishiindex > 0)
                                {
                                    MoveData(kaishiindex);
                                }
                                MoveData(qianduanjuku.Count);
                            }
                           
                        }
                        else
                        {
                            if (kaishiindex > 0)
                            {
                                MoveData(kaishiindex);
                            }
                        }
                    }
                    else
                    {
                        if (qianduanjuku.Count >= 2)
                        {
                            int index = _ShuJu.IndexOf(qianduanjuku[0]);
                            if (index >= 0)
                            {
                                MoveData(index);
                            }
                            else
                            {
                                MoveData(count);
                            }
                        }
                        else
                        {
                            MoveData(count);
                        }

                    }


                    #endregion
                }
                else
                {
                    int jiesuindex = 0;
                    int kaishiindex = 0;
                    for (; ; )
                    {
                        jiesuindex = _ShuJu.HanYouJiHe<byte>(jiesufu);
                        kaishiindex = _ShuJu.HanYouJiHe<byte>(qianduanjuku);
                        if (kaishiindex <= jiesuindex && kaishiindex >= 0)
                        {
                            int shuliang = (jiesuindex - kaishiindex) + jieweishuliang;
                            List<byte> Get = _ShuJu.GetRange(kaishiindex, shuliang);
                          
                            if (jiaoyanhanshu != null)
                            {
                                bool zhen = jiaoyanhanshu(Get);
                                if (zhen)
                                {
                                    _LisDataByte.Add(Get.ToArray());
                                    if (kaishiindex > 0)
                                    {
                                        MoveData(kaishiindex);
                                    }
                                    MoveData(Get.Count);
                                }
                                else
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        jiesuindex = _ShuJu.HanYouJiHe<byte>(jiesufu, jiesuindex + 1);
                                        if (jiesuindex >= 0)
                                        {
                                            List<byte> Get1 = _ShuJu.GetRange(kaishiindex, (jiesuindex - kaishiindex) + jiesufu.Count);
                                            bool hege = jiaoyanhanshu(Get1);
                                            if (hege)
                                            {
                                                _LisDataByte.Add(Get1.ToArray());
                                                if (kaishiindex > 0)
                                                {
                                                    MoveData(kaishiindex);
                                                }
                                                MoveData(Get1.Count);
                                                break;
                                            }
                                            else
                                            {
                                                if (i==2)
                                                {
                                                    if (kaishiindex > 0)
                                                    {
                                                        MoveData(kaishiindex);
                                                    }
                                                    MoveData(qianduanjuku.Count);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (kaishiindex > 0)
                                {
                                    MoveData(kaishiindex);
                                }
                                MoveData(qianduanjuku.Count);
                            }
                          
                        }
                        else
                        {
                            if (kaishiindex >= 0)
                            {
                                MoveData(kaishiindex);
                            }
                            else
                            {
                                if (qianduanjuku.Count >= 2)
                                {
                                    int index = _ShuJu.IndexOf(qianduanjuku[0]);
                                    if (index >= 0)
                                    {
                                        MoveData(index);
                                    }
                                    else
                                    {
                                        MoveData(count);
                                    }
                                }
                                else
                                {
                                    MoveData(count);
                                }

                            }
                        }
                        #region 判断跳出
                        if (_ShuJu.Count < qianduanjuku.Count || _ShuJu.Count < jiesufu.Count)
                        {
                            break;
                        }
                        if (jiesuindex < 0 || kaishiindex < 0)
                        {
                            break;
                        }
                        #endregion

                    }
                }


            }
            else
            {
                #region 非校验部分
                if (tiaochu)
                {
                    int jiesuindex = _ShuJu.HanYouJiHe<byte>(jiesufu);
                    int kaishiindex = _ShuJu.HanYouJiHe<byte>(qianduanjuku);
                    if (kaishiindex <= jiesuindex && kaishiindex >= 0)
                    {
                        List<byte> Get = _ShuJu.GetRange(kaishiindex, (jiesuindex - kaishiindex) + jiesufu.Count);
                        _LisDataByte.Add(Get.ToArray());

                        if (kaishiindex > 0)
                        {
                            MoveData(kaishiindex);
                        }
                        MoveData(Get.Count);
                    }
                    else
                    {
                        if (kaishiindex >= 0)
                        {
                            MoveData(kaishiindex);
                        }
                        else
                        {
                            if (qianduanjuku.Count >= 2)
                            {
                                int index = _ShuJu.IndexOf(qianduanjuku[0]);
                                if (index >= 0)
                                {
                                    MoveData(index);
                                }
                                else
                                {
                                    MoveData(count);
                                }
                            }
                            else
                            {
                                MoveData(count);
                            }
                        }
                    }

                }
                else
                {
                    int jiesuindex = 0;
                    int kaishiindex = 0;
                    for (; ; )
                    {
                        jiesuindex = _ShuJu.HanYouJiHe<byte>(jiesufu);
                        kaishiindex = _ShuJu.HanYouJiHe<byte>(qianduanjuku);
                        if (kaishiindex <= jiesuindex && kaishiindex >= 0)
                        {
                            int zhengshishuliang = (jiesuindex - kaishiindex) + jiesufu.Count;

                            List<byte> Get = _ShuJu.GetRange(kaishiindex, zhengshishuliang);
                            _LisDataByte.Add(Get.ToArray());
                            if (kaishiindex > 0)
                            {
                                MoveData(kaishiindex);
                            }
                            MoveData(Get.Count);
                        }
                        else
                        {
                            if (kaishiindex >= 0)
                            {
                                MoveData(kaishiindex);
                            }
                            else
                            {
                                if (qianduanjuku.Count >= 2)
                                {
                                    int index = _ShuJu.IndexOf(qianduanjuku[0]);
                                    if (index >= 0)
                                    {
                                        MoveData(index);
                                    }
                                    else
                                    {
                                        MoveData(count);
                                    }
                                }
                                else
                                {
                                    MoveData(count);
                                }
                            }
                        }
                        if (_ShuJu.Count < qianduanjuku.Count || _ShuJu.Count < jiesufu.Count)
                        {

                            break;
                        }
                        if (jiesuindex < 0 || kaishiindex < 0)
                        {
                            break;
                        }
                    }
                }

                #endregion
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
