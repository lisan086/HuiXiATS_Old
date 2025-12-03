using CommLei.DataChuLi;
using CommLei.JiChuLei;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DataChuLi
{
    /// <summary>
    /// 用于小型数据库
    /// </summary>
    public class XiaoXinSQLDB<T> where T : new()
    {
        private bool GuanBi = false;
  
        private ConcurrentQueue<T> DuiLieJiHe = new ConcurrentQueue<T>();

        JosnOrXSModel JosnOrXSModel;
        #region 单例
        private static XiaoXinSQLDB<T> _LogTxt = null;

        private readonly static object _DuiXiang = new object();
        private XiaoXinSQLDB()
        {
            string LuJing = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Data");//项目
            if (!Directory.Exists(LuJing))
            {
                Directory.CreateDirectory(LuJing);

            }
            Type S = typeof(T);
            string WenJian = S.Name;
            string TexboxPaths = string.Format(@"{0}{1}Data{2}.txt", LuJing, @"\", WenJian);//项目
            JosnOrXSModel = new JosnOrXSModel(TexboxPaths);
            GuanBi = true;
            Thread th = new Thread(Work);
            th.IsBackground = true;
            th.DisableComObjectEagerCleanup();
            th.Start();
        }
    
        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static XiaoXinSQLDB<T> Ceratei()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new XiaoXinSQLDB<T>();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion

    
        /// <summary>
        /// 增加数据
        /// </summary>       
        public void Add(T model)
        {
            DuiLieJiHe.Enqueue(model);
        }

        /// <summary>
        /// 关闭的方法
        /// </summary>
        public void Close()
        {
            GuanBi = false;
        }

        /// <summary>
        ///获取数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetShuJu()
        {
            return JosnOrXSModel.GetLisTModel<T>();
        }
      
        private void Work()
        {
            int shouci = 0;
            while (GuanBi)
            {
                try
                {
                    bool count = DuiLieJiHe.IsEmpty;
                    if (count == false)
                    {
                        int xincount = DuiLieJiHe.Count;
                        if (xincount >= 10 && shouci > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < 10; i++)
                            {
                                T model = default(T);
                                bool quchu = DuiLieJiHe.TryDequeue(out model);
                                if (quchu)
                                {
                                    if (model != null)
                                    {
                                        sb.Append(string.Format(",{0}", ChangYong.HuoQuJsonStr(model)));
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(sb.ToString()) == false)
                            {
                                JosnOrXSModel.XieTModel(sb.ToString());
                            }
                        }
                       else   if (xincount >= 5&&shouci>0)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < 5; i++)
                            {
                                T model = default(T);
                                bool quchu = DuiLieJiHe.TryDequeue(out model);
                                if (quchu)
                                {
                                    if (model != null)
                                    {
                                        sb.Append(string.Format(",{0}",ChangYong.HuoQuJsonStr(model)));
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(sb.ToString())==false)
                            {
                                JosnOrXSModel.XieTModel(sb.ToString());
                            }
                        }
                        else
                        {
                            shouci++;
                            T model = default(T);
                            bool quchu = DuiLieJiHe.TryDequeue(out model);
                            if (quchu)
                            {
                                if (model != null)
                                {
                                    JosnOrXSModel.XieTModel(model);
                                }
                            }
                        }
                    }
                }
                catch 
                {

                  
                }
                Thread.Sleep(20);
            }
        }
    }
}
