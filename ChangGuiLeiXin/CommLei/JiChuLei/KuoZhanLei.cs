using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.JiChuLei
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static  class KuoZhanLei
    {
        /// <summary>
        /// 字节相加
        /// </summary>
        /// <param name="data"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte[] ByteAdd(this byte[] data, byte[] val)
        {
            byte[] newdata = new byte[data.Length + val.Length];
            Buffer.BlockCopy(data, 0, newdata, 0, data.Length);
            Buffer.BlockCopy(val, 0, newdata, data.Length, val.Length);
            return newdata;
        }
        /// <summary>
        /// 取某段字节
        /// </summary>
        /// <param name="mydata"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static byte[] SubArray(this byte[] mydata, int index, int count)
        {
            if (count >= mydata.Length) return mydata;
            byte[] message = new byte[count];
            Buffer.BlockCopy(mydata, index, message, 0, count);
            return message;
        }

        /// <summary>
        /// 字符相加原理
        /// </summary>
        /// <param name="yuanshi"></param>
        /// <param name="xiangjia"></param>
        /// <returns></returns>
        public static string AddStr(this string yuanshi,string xiangjia)
        {
            return string.Format("{0}{1}", yuanshi, xiangjia);
        }

   
        /// <summary>
        /// 查找数据是否包含该项
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="socbyte1">泛型数组</param>
        /// <param name="mubiao1">包含的数组</param>
        /// <returns></returns>
        public static int HanYouJiHe<T>(this T[] socbyte1, T[] mubiao1)
        {
            if (socbyte1 == null || mubiao1 == null)
            {
                return -1;
            }
            if (socbyte1.Length == 0)
            {
                return -1;
            }
            if (socbyte1.Length < mubiao1.Length)
            {
                return -1;
            }
            if (mubiao1.Length == 0)
            {
                return -1;
            }
            List<T> socbyte = socbyte1.ToList();
            List<T> mubiao = mubiao1.ToList();
            if (mubiao.Count == 1)
            {
                int indexof = socbyte.IndexOf(mubiao[0], 0);
                return indexof;
            }
            else
            {
                int mubiaoshu = mubiao.Count;
                int zongshu = socbyte.Count;
                int indexof = socbyte.IndexOf(mubiao[0]);
                if (indexof >= 0)
                {
                    if (indexof + 1 > zongshu)
                    {
                        return -1;
                    }
                    for (; ; )
                    {

                        int ssd = indexof;
                        bool zhen = false;
                        if (mubiaoshu < 300)
                        {
                            List<int> jiluyucha = new List<int>();
                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {
                                    jiluyucha.Add(zhaos);
                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                if (mubiaoshu >= 3)
                                {
                                    int yucha = jiluyucha[mubiaoshu - 2] - jiluyucha[mubiaoshu - 3];
                                    if (yucha == 1)
                                    {
                                        indexof = ssd - mubiaoshu;
                                        if (indexof < 0)
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                    else
                                    {
                                        if (yucha >= mubiaoshu)
                                        {
                                            indexof = ssd - mubiaoshu;
                                            if (indexof < 0)
                                            {
                                                indexof = ssd;
                                            }
                                        }
                                        else
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                }
                                else
                                {
                                    indexof = ssd - mubiaoshu;
                                    if (indexof < 0)
                                    {
                                        indexof = ssd;
                                    }
                                }

                            }
                            else
                            {
                                return indexof;
                            }
                        }
                        else
                        {

                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {

                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                        break;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                indexof = ssd - mubiaoshu;
                                if (indexof < 0)
                                {
                                    indexof = ssd;
                                }


                            }
                            else
                            {
                                return indexof;
                            }
                        }

                        indexof = socbyte.IndexOf(mubiao[0], indexof + 1);
                        if (indexof < 0)
                        {
                            return -1;
                        }
                        if (indexof + 1 > zongshu)
                        {
                            return -1;
                        }
                    }
                }
                return -1;


            }
        }
      

        /// <summary>
        ///找是否含有
        /// </summary>
        /// <param name="socbyte"></param>
        /// <param name="mubiao"></param>
        /// <returns></returns>
        public static int HanYouJiHe<T>(this List<T> socbyte, List<T> mubiao)
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
                int mubiaoshu = mubiao.Count;
                int zongshu = socbyte.Count;
                int indexof = socbyte.IndexOf(mubiao[0]);
                if (indexof >= 0)
                {
                    if (indexof+1>zongshu)
                    {
                        return -1;
                    }
                    for (; ; )
                    {
                       
                        int ssd = indexof;
                        bool zhen = false;
                        if (mubiaoshu < 300)
                        {
                            List<int> jiluyucha = new List<int>();
                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {
                                    jiluyucha.Add(zhaos);
                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                if (mubiaoshu >= 3)
                                {
                                    int yucha = jiluyucha[mubiaoshu - 2] - jiluyucha[mubiaoshu - 3];
                                    if (yucha == 1)
                                    {
                                        indexof = ssd - mubiaoshu ;
                                        if (indexof < 0)
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                    else
                                    {
                                        if (yucha >= mubiaoshu)
                                        {
                                            indexof = ssd - mubiaoshu ;
                                            if (indexof < 0)
                                            {
                                                indexof = ssd;
                                            }
                                        }
                                        else
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                }
                                else
                                {
                                    indexof = ssd - mubiaoshu ;
                                    if (indexof < 0)
                                    {
                                        indexof = ssd;
                                    }
                                }

                            }
                            else
                            {
                                return indexof;
                            }
                        }
                        else
                        {

                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {

                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                        break;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                indexof = ssd - mubiaoshu ;
                                if (indexof < 0)
                                {
                                    indexof = ssd;
                                }


                            }
                            else
                            {
                                return indexof;
                            }
                        }

                        indexof = socbyte.IndexOf(mubiao[0], indexof+1);
                        if (indexof < 0)
                        {
                            return -1;
                        }
                        if (indexof + 1 > zongshu)
                        {
                            return -1;
                        }
                    }
                }
                return -1;
            }
        }


        /// <summary>
        /// 查找数据是否包含该项
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="socbyte1">泛型数组</param>
        /// <param name="mubiao1">包含的数组</param>
        /// <param name="xinindex">包含的数组</param>
        /// <returns></returns>
        public static int HanYouJiHe<T>(this T[] socbyte1, T[] mubiao1,int xinindex)
        {
            if (socbyte1 == null || mubiao1 == null)
            {
                return -1;
            }
            if (socbyte1.Length == 0)
            {
                return -1;
            }
            if (socbyte1.Length < mubiao1.Length)
            {
                return -1;
            }
            if (mubiao1.Length == 0)
            {
                return -1;
            }
            List<T> socbyte = socbyte1.ToList();
            List<T> mubiao = mubiao1.ToList();
            if (mubiao.Count == 1)
            {
                int indexof = socbyte.IndexOf(mubiao[0], xinindex);
                return indexof;
            }
            else
            {
                int mubiaoshu = mubiao.Count;
                int zongshu = socbyte.Count;
                int indexof = socbyte.IndexOf(mubiao[0], xinindex);
                if (indexof >= 0)
                {
                    if (indexof + 1 > zongshu)
                    {
                        return -1;
                    }
                    for (; ; )
                    {

                        int ssd = indexof;
                        bool zhen = false;
                        if (mubiaoshu < 300)
                        {
                            List<int> jiluyucha = new List<int>();
                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {
                                    jiluyucha.Add(zhaos);
                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                if (mubiaoshu >= 3)
                                {
                                    int yucha = jiluyucha[mubiaoshu - 2] - jiluyucha[mubiaoshu - 3];
                                    if (yucha == 1)
                                    {
                                        indexof = ssd - mubiaoshu;
                                        if (indexof < 0)
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                    else
                                    {
                                        if (yucha >= mubiaoshu)
                                        {
                                            indexof = ssd - mubiaoshu;
                                            if (indexof < 0)
                                            {
                                                indexof = ssd;
                                            }
                                        }
                                        else
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                }
                                else
                                {
                                    indexof = ssd - mubiaoshu;
                                    if (indexof < 0)
                                    {
                                        indexof = ssd;
                                    }
                                }

                            }
                            else
                            {
                                return indexof;
                            }
                        }
                        else
                        {

                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {

                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                        break;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                indexof = ssd - mubiaoshu;
                                if (indexof < 0)
                                {
                                    indexof = ssd;
                                }


                            }
                            else
                            {
                                return indexof;
                            }
                        }

                        indexof = socbyte.IndexOf(mubiao[0], indexof + 1);
                        if (indexof < 0)
                        {
                            return -1;
                        }
                        if (indexof + 1 > zongshu)
                        {
                            return -1;
                        }
                    }
                }
                return -1;


            }
        }


        /// <summary>
        ///找是否含有
        /// </summary>
        /// <param name="socbyte"></param>
        /// <param name="mubiao"></param>
        /// <param name="xinindex"></param>
        /// <returns></returns>
        public static int HanYouJiHe<T>(this List<T> socbyte, List<T> mubiao,int xinindex)
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
                int indexof = socbyte.IndexOf(mubiao[0], xinindex);
                return indexof;
            }
            else
            {
                int mubiaoshu = mubiao.Count;
                int zongshu = socbyte.Count;
                int indexof = socbyte.IndexOf(mubiao[0], xinindex);
                if (indexof >= 0)
                {
                    if (indexof + 1 > zongshu)
                    {
                        return -1;
                    }
                    for (; ; )
                    {

                        int ssd = indexof;
                        bool zhen = false;
                        if (mubiaoshu < 300)
                        {
                            List<int> jiluyucha = new List<int>();
                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {
                                    jiluyucha.Add(zhaos);
                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                if (mubiaoshu >= 3)
                                {
                                    int yucha = jiluyucha[mubiaoshu - 2] - jiluyucha[mubiaoshu - 3];
                                    if (yucha == 1)
                                    {
                                        indexof = ssd - mubiaoshu;
                                        if (indexof < 0)
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                    else
                                    {
                                        if (yucha >= mubiaoshu)
                                        {
                                            indexof = ssd - mubiaoshu;
                                            if (indexof < 0)
                                            {
                                                indexof = ssd;
                                            }
                                        }
                                        else
                                        {
                                            indexof = ssd;
                                        }
                                    }
                                }
                                else
                                {
                                    indexof = ssd - mubiaoshu;
                                    if (indexof < 0)
                                    {
                                        indexof = ssd;
                                    }
                                }

                            }
                            else
                            {
                                return indexof;
                            }
                        }
                        else
                        {

                            for (int i = 1; i < mubiaoshu; i++)
                            {
                                int zhaos = socbyte.IndexOf(mubiao[i], ssd + 1);

                                if (zhaos < 0)
                                {
                                    return -1;
                                }
                                else
                                {

                                    if (zhaos == ssd + 1)
                                    {
                                        ssd = zhaos;
                                    }
                                    else
                                    {
                                        ssd = zhaos;
                                        zhen = true;
                                        break;
                                    }
                                }
                            }
                            if (zhen)
                            {
                                indexof = ssd - mubiaoshu;
                                if (indexof < 0)
                                {
                                    indexof = ssd;
                                }


                            }
                            else
                            {
                                return indexof;
                            }
                        }

                        indexof = socbyte.IndexOf(mubiao[0], indexof + 1);
                        if (indexof < 0)
                        {
                            return -1;
                        }
                        if (indexof + 1 > zongshu)
                        {
                            return -1;
                        }
                    }
                }
                return -1;
            }
        }
    }
}
