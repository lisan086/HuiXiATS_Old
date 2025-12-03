using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.GongYeJieHe
{
    /// <summary>
    /// 泛型集合类 工业用的多一点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FanXingJiHeLei<T>
    {
        private readonly object XingNneng = new object();
      
        /// <summary>
        /// 泛型集合
        /// </summary>
        private List<T> FanXinList = new List<T>();
        /// <summary>
        /// 增加对象
        /// </summary>
        /// <param name="Tobject">泛型集合要增加的对象</param>
        public void Add(T Tobject)
        {
            try
            {
                lock (XingNneng)
                {
                    FanXinList.Add(Tobject);
                }
            }
            catch
            {


            }

        }

        /// <summary>
        /// 增加对象
        /// </summary>
        /// <param name="Tobject">泛型集合要增加的对象</param>
        public void Add(T[] Tobject)
        {
            try
            {
                lock (XingNneng)
                {
                    FanXinList.AddRange(Tobject);
                }
            }
            catch
            {


            }

        }
        /// <summary>
        /// 移除泛型集合的首行
        /// </summary>
        public void Romve_Head()
        {
            try
            {
                if (FanXinList.Count > 0)
                {
                    lock (XingNneng)
                    {
                        if (FanXinList.Count > 0)
                        {
                            FanXinList.RemoveAt(0);
                        }
                    }
                }

            }
            catch
            {


            }

        }
        /// <summary>
        /// 获取首个对象
        /// </summary>
        /// <returns></returns>
        public T GetModel_Head()
        {
            try
            {
                if (FanXinList.Count > 0)
                {
                    return FanXinList[0];
                }

            }
            catch
            {


            }

            return default(T);
        }

        /// <summary>
        /// 获指定一个对象
        /// </summary>
        /// <returns></returns>
        public T GetModel_iHead(int i)
        {
            try
            {

                if (FanXinList.Count > i)
                {
                    return FanXinList[i];
                }


            }
            catch
            {


            }

            return default(T);
        }

        /// <summary>
        /// 获取首个对象，且移除首个
        /// </summary>
        /// <returns></returns>
        public T GetModel_Head_RomeHead()
        {
            try
            {
                if (FanXinList.Count > 0)
                {
                    lock (XingNneng)
                    {
                        if (FanXinList.Count > 0)
                        {
                            T Shu = FanXinList[0];
                            FanXinList.RemoveAt(0);
                            return Shu;
                        }
                    }
                }

            }
            catch
            {


            }

            return default(T);
        }

        /// <summary>
        /// 获取最后对象，且移除所有
        /// </summary>
        /// <returns></returns>
        public T GetModel_End_RomeALL()
        {
            try
            {
                
                if (FanXinList.Count > 0)
                {
                    lock (XingNneng)
                    {
                        int count = FanXinList.Count;
                        if (count > 0)
                        {
                            T Shu = FanXinList[count-1];
                            FanXinList.Clear();
                            return Shu;
                        }
                    }
                }

            }
            catch
            {


            }

            return default(T);
        }
        /// <summary>
        /// 提供制定移除
        /// </summary>
        /// <param name="Index">索引</param>
        public void Romve_Zhiding(int Index)
        {
            if (Index < 0)
            {
                return;
            }
            try
            {
                if (FanXinList.Count > Index)
                {
                    lock (XingNneng)
                    {
                        if (FanXinList.Count > Index)
                        {
                            FanXinList.RemoveAt(Index);
                        }
                    }
                }
            }
            catch
            {


            }

        }
        /// <summary>
        /// 提供制定移除
        /// </summary>
        /// <param name="Tobject">T 类型</param>
        public void Romve_Zhiding(T Tobject)
        {
            if (FanXinList.Count > 0 && FanXinList.IndexOf(Tobject) >= 0)
            {
                lock (XingNneng)
                {
                    if (FanXinList.Count > 0 && FanXinList.IndexOf(Tobject) >= 0)
                    {
                        FanXinList.Remove(Tobject);
                    }
                }
            }
        }
        /// <summary>
        /// 移除所有
        /// </summary>
        public void Romve_All()
        {
            if (FanXinList.Count > 0)
            {
                FanXinList.Clear();
            }
        }

        /// <summary>
        /// 一定范围
        /// </summary>
        public void Romve_Range(int kaishi, int count)
        {
            int shuliang = kaishi + count;
            if (FanXinList.Count > 0 && FanXinList.Count >= shuliang)
            {
                lock (XingNneng)
                {
                    if (FanXinList.Count > 0 && FanXinList.Count >= shuliang)
                    {
                        try
                        {
                            FanXinList.RemoveRange(kaishi, count);
                        }
                        catch
                        {


                        }

                    }
                }
            }
        }
        /// <summary>
        /// 获取泛型集合
        /// </summary>
        /// <returns>泛型集合</returns>
        public List<T> GetListT()
        {
            return FanXinList;
        }

        /// <summary>
        /// 获取泛型集合数量
        /// </summary>
        /// <returns>返回集合的数量</returns>
        public int GetCount()
        {
            if (FanXinList.Count > 0)
            {
                return FanXinList.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}
