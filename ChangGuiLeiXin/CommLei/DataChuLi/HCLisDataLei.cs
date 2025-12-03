using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.DataChuLi
{
    /// <summary>
    /// 缓存的集合单例类
    /// </summary>
    public class HCLisDataLei<T> where T : new()
    {
        private bool IsData = false;
        private JosnOrSModel JosnOrModel;

        /// <summary>
        /// 集合对象
        /// </summary>
        public List<T> LisWuLiao = new List<T>();

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
        private static HCLisDataLei<T> _LogTxt = null;

        private readonly static object _DuiXiang = new object();
        private HCLisDataLei()
        {
            ShuaXinPath();
        }
        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static HCLisDataLei<T> Ceratei()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new HCLisDataLei<T>();
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
                string TexboxPaths = string.Format(@"{0}{1}JiHe{2}.txt", TexboxPath, @"\", WenJian);//项目

                JosnOrModel = new JosnOrSModel(TexboxPaths);

                LisWuLiao = JosnOrModel.GetLisTModel<T>();
                if (LisWuLiao == null)
                {
                    LisWuLiao = new List<T>();
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
                string TexboxPaths = string.Format(@"{0}{1}JiHe{2}.txt", TexboxPath, @"\", WenJian);//项目

                JosnOrModel = new JosnOrSModel(TexboxPaths);

                LisWuLiao = JosnOrModel.GetLisTModel<T>();
                if (LisWuLiao == null)
                {
                    LisWuLiao = new List<T>();
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


        /// <summary>
        /// 修改与新增
        /// </summary>
        /// <param name="wuliao"></param>
        ///  <param name="ChuFaZhen"></param>
        public void XiuGaiOrXinZengBaoCun(T wuliao, Func<T, bool> ChuFaZhen)
        {

            if (wuliao != null)
            {

                bool zhen = false;
                for (int i = 0; i < LisWuLiao.Count; i++)
                {
                    if (ChuFaZhen != null)
                    {
                        if (ChuFaZhen(LisWuLiao[i]))
                        {
                            LisWuLiao[i] = ChangYong.FuZhiShiTi(wuliao);
                            zhen = true;
                            break;
                        }
                    }
                }
                if (zhen == false)
                {
                    LisWuLiao.Add(ChangYong.FuZhiShiTi(wuliao));
                }
                BaoCun();
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="ChuFaZhen"></param>
        /// <returns></returns>
        public List<T> ChaZhao(Func<T, bool> ChuFaZhen)
        {
            if (ChuFaZhen == null)
            {
                return new List<T>();
            }
            List<T> yongzi = new List<T>();
            for (int i = 0; i < LisWuLiao.Count; i++)
            {
                if (ChuFaZhen(LisWuLiao[i]))
                {
                    yongzi.Add(LisWuLiao[i]);
                }
            }
            return yongzi;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ChuFaZhen"></param>
        public void ShanChu(Func<T, bool> ChuFaZhen)
        {
            if (ChuFaZhen == null)
            {
                return;
            }
            for (int i = 0; i < LisWuLiao.Count; i++)
            {
                if (ChuFaZhen(LisWuLiao[i]))
                {
                    LisWuLiao.RemoveAt(i);
                    break;
                }
            }

            BaoCun();
        }
    }
}
