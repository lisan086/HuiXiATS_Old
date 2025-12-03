using BaseUI.DaYIngMoBan.Model;
using JieMianLei.UI.TiShiKuang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataChuLi;

namespace BaseUI.DaYIngMoBan.Lei
{
    /// <summary>
    ///打印模型的加载
    /// </summary>
    internal class JiaZaiMoXing
    {       
        /// <summary>
        /// 对象
        /// </summary>
        public ZBiaoQianModel LisWuLiao = new ZBiaoQianModel();
        #region 单例
        private static JiaZaiMoXing _LogTxt = null;

        private readonly static object _DuiXiang = new object();
        private JiaZaiMoXing()
        {
          

        }
        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static JiaZaiMoXing Ceratei()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new JiaZaiMoXing();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion
        /// <summary>
        /// 保存对象
        /// </summary>
        public void BaoCun(string baocunfilepath)
        {
            JosnOrModel JosnOrModel = new JosnOrModel(baocunfilepath);
            if (LisWuLiao != null)
            {
                JosnOrModel.XieTModel(LisWuLiao);
            }
        }

        /// <summary>
        /// 加载模型
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool LoadMoXing(string filepath)
        {

            JosnOrModel JosnOrModel = new JosnOrModel(filepath);

            LisWuLiao = JosnOrModel.GetTModel<ZBiaoQianModel>();
            if (LisWuLiao == null)
            {
                LisWuLiao = new ZBiaoQianModel();
                return false;
            }
            return true;
        }

    }
}
