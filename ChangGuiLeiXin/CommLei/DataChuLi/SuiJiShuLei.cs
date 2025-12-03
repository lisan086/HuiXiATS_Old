using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataChuLi
{
    /// <summary>
    /// 随机数产生
    /// </summary>
    public class SuiJiShuLei
    {
        private Random _ShuJiShu;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SuiJiShuLei()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();//生成字节数组
            int iRoot = BitConverter.ToInt32(buffer, 0);//利用BitConvert方法把字节数组转换为整数              
            _ShuJiShu = new Random(iRoot);
            _ShuJiShu.Next(0, 20);
        }
        /// <summary>
        /// 获取随机数整型
        /// </summary>
        /// <param name="DataSuoce">数据源</param>
        /// <returns>返回数据源中一个</returns>
        public int GetShuJuInt(int[] DataSuoce)
        {
            if (DataSuoce.Length > 0)
            {
                try
                {
                    int Tou = 0;
                    int Wei = DataSuoce.Length;
                    int suoyin = _ShuJiShu.Next(Tou, Wei);
                    return DataSuoce[suoyin];

                }
                catch
                {
                    return -80;

                }


            }
            else
            {
                return -90;
            }

        }

        /// <summary>
        ///  获取随机数单精度
        /// </summary>
        /// <param name="DataSuoce">数据源</param>
        /// <returns>返回数据源中一个</returns>
        public float GetShuJuFloat(float[] DataSuoce)
        {
            if (DataSuoce.Length > 0)
            {
                try
                {
                    int Tou = 0;
                    int Wei = DataSuoce.Length;

                    int suoyin = _ShuJiShu.Next(Tou, Wei);
                    return DataSuoce[suoyin];

                }
                catch
                {
                    return -80;

                }


            }
            else
            {
                return -90;
            }

        }

        /// <summary>
        /// 泛型随机数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LisT"></param>
        /// <returns></returns>
        public T GetShuJuT<T>(List<T> LisT)
        {
            if (LisT.Count > 0)
            {
                try
                {
                    int Tou = 0;
                    int Wei = LisT.Count;

                    int suoyin = _ShuJiShu.Next(Tou, Wei);
                    return LisT[suoyin];

                }
                catch
                {
                    return default(T);

                }


            }
            else
            {
                return default(T);
            }

        }



        /// <summary>
        ///概率数据精确到0到100,精确度有0.001,出现时为true，没出现时为false
        /// </summary>
        /// <returns></returns>
        public bool ChuXian1GaiLv(double BaiFenBi)
        {
            if (BaiFenBi <= 0)
            {
                return false;
            }
            if (BaiFenBi >= 100)
            {
                return true;
            }
            double shuju = BaiFenBi * 1000;
            _ShuJiShu.Next(0, 20);
            int shuju1 = _ShuJiShu.Next(0, 100001);
            if (shuju1 <= shuju)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 随机值，不包含最大数
        /// </summary>
        /// <param name="zuixiaoshu"></param>
        /// <param name="zuidashju"></param>
        /// <returns></returns>
        public int SuiJiData(int zuixiaoshu, int zuidashju)
        {
            if (zuidashju < zuixiaoshu)
            {
                return -1;
            }
            else
            {
                _ShuJiShu.Next(1, 20);
                return _ShuJiShu.Next(zuixiaoshu, zuidashju);
            }
        }

        /// <summary>
        /// 随机生成的小数 Len 表示保留多少小数
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="Len"></param>
        /// <returns></returns>
        public double GetRandomNumber(double minimum, double maximum, int Len)   //Len小数点保留位数
        {
            return Math.Round(_ShuJiShu.NextDouble() * (maximum - minimum) + minimum, Len);
        }
    }
}
