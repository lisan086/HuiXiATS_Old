using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSheBei.ABSSheBei
{
    /// <summary>
    /// 用于配置加载控件
    /// </summary>
    public interface KJPeiZhiJK
    {
        /// <summary>
        /// 加载配置控件
        /// </summary>
        /// <returns></returns>
        /// <param name="jicunqibiaoshi"></param>
        Control GetPeiZhiKJ(string jicunqibiaoshi);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="canshu"></param>
        void SetCanShu(string canshu);

        /// <summary>
        /// 获取配置参数
        /// </summary>
        /// <returns></returns>
        string GetCanShu();
    }
}
