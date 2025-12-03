using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.DataChuLi
{
    /// <summary>
    /// 缓存单个数据的类
    /// </summary>
    public class HCDanGeDataLei<T> where T : new()
    {
        private bool IsData = false;
        private JosnOrSModel JosnOrModel;

        /// <summary>
        /// 对象
        /// </summary>
        public T LisWuLiao = default(T);
        /// <summary>
        /// true表示数据会存在数据块里,false 表示存在配置块
        /// </summary>
        public bool IsShuJuKuai
        {
            get { return IsData; }
            set
            {
                IsData = value;
                ShuaXinPath();
            }
        }

        #region 单例
        private static HCDanGeDataLei<T> _LogTxt = null;

        private readonly static object _DuiXiang = new object();
        private HCDanGeDataLei()
        {
            string TexboxPath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "PeiZhi");//项目
            if (!Directory.Exists(TexboxPath))
            {
                Directory.CreateDirectory(TexboxPath);

            }
            Type S = typeof(T);
            string WenJian = S.Name;

            string TexboxPaths = string.Format(@"{0}{1}DanGe{2}.txt", TexboxPath, @"\", WenJian);//项目

            JosnOrModel = new JosnOrSModel(TexboxPaths);

            LisWuLiao = JosnOrModel.GetTModel<T>();
            if (LisWuLiao == null)
            {
                LisWuLiao = new T();
            }

        }
        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static HCDanGeDataLei<T> Ceratei()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new HCDanGeDataLei<T>();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion

        private void ShuaXinPath()
        {
            if (IsData == false)
            {
                string TexboxPath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "PeiZhi");//项目
                if (!Directory.Exists(TexboxPath))
                {
                    Directory.CreateDirectory(TexboxPath);

                }
                Type S = typeof(T);
                string WenJian = S.Name;

                string TexboxPaths = string.Format(@"{0}{1}DanGe{2}.txt", TexboxPath, @"\", WenJian);//项目

                JosnOrModel = new JosnOrSModel(TexboxPaths);

                LisWuLiao = JosnOrModel.GetTModel<T>();
                if (LisWuLiao == null)
                {
                    LisWuLiao = new T();
                }
            }
            else
            {
                string TexboxPath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "DataShuJu");//项目
                if (!Directory.Exists(TexboxPath))
                {
                    Directory.CreateDirectory(TexboxPath);

                }
                Type S = typeof(T);
                string WenJian = S.Name;

                string TexboxPaths = string.Format(@"{0}{1}DanGe{2}.txt", TexboxPath, @"\", WenJian);//项目

                JosnOrModel = new JosnOrSModel(TexboxPaths);

                LisWuLiao = JosnOrModel.GetTModel<T>();
                if (LisWuLiao == null)
                {
                    LisWuLiao = new T();
                }
            }
        }
        /// <summary>
        /// 保存对象
        /// </summary>
        public void BaoCun()
        {
            if (LisWuLiao != null)
            {
                JosnOrModel.XieTModel(LisWuLiao);
            }
        }
       

    }
}
