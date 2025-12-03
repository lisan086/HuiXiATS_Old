using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpFanWen
{
    /// <summary>
    /// 更新器的参数
    /// </summary>
    public class GengXinQi
    {
        /// <summary>
        /// 访问网址
        /// </summary>
        public string WanZhi { get; set; } = "";

        /// <summary>
        /// 1为中文
        /// </summary>
        public int YuYanQieHuan { get; set; } = 1;

        /// <summary>
        /// 1表示禁用更新
        /// </summary>
        public int JingYong { get; set; } = 1;
        /// <summary>
        /// 更新标志  由软件控制
        /// </summary>
        public int GengXinBiaoZhi { get; set; } = 1;

        /// <summary>
        /// 1表示需要登录
        /// </summary>
        public int IsXuYaoDengLu { get; set; } = 1;
    }
}
