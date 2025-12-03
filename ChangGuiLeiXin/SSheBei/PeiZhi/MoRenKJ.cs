using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SSheBei.ABSSheBei;

namespace SSheBei.PeiZhi
{
    /// <summary>
    /// 默认KJ
    /// </summary>
    public partial class MoRenKJ : UserControl,KJPeiZhiJK
    {
        /// <summary>
        /// 默认参数KJ
        /// </summary>
        public MoRenKJ()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetCanShu()
        {
            return this.textBox8.Text;
        }
        /// <summary>
        /// 加载控件
        /// </summary>
        /// <param name="jicunqibiaoshi"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Control GetPeiZhiKJ(string jicunqibiaoshi)
        {
            return this;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="canshu"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetCanShu(string canshu)
        {
            this.textBox8.Text = canshu;
        }
    }
}
