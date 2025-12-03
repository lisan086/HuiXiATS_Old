using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUI.FuFrom.XinWeiHuFrm
{
    public class JiChuLieModel
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string LieName { get; set; } = "";
        /// <summary>
        /// 绑定名称
        /// </summary>
        public string BangDingName { get; set; } = "";

        /// <summary>
        /// 第几列
        /// </summary>
        public int DiJiLie { get; set; } = 1;
        /// <summary>
        /// 宽度
        /// </summary>
        public float Kuan { get; set; } = 100;

        public Dictionary<string, string> XianShiZhi { get; set; } = new Dictionary<string, string>();
    }
}
