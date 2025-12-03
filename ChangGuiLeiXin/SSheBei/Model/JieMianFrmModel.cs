using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSheBei.Model
{
    /// <summary>
    /// 显示调试界面的参数
    /// </summary>
    public class JieMianFrmModel
    {
        /// <summary>
        /// 调试界面
        /// </summary>
        public Form Form { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string SheBeiName { get; set; }

        /// <summary>
        /// 设备分组
        /// </summary>
        public string FenZu { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public int SheBeiID { get; set; }
     
    }
}
