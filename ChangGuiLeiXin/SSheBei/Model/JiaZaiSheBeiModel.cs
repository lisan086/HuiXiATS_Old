using Common.SheBeiTeXing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 用于加载设备
    /// </summary>
    public class JiaZaiSheBeiModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SheBeiLieTeXing(true,"设备ID")]
        public int SheBeiID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [SheBeiLieTeXing(true, "设备名称")]
        public string SheBeiName { get; set; } = "";


        /// <summary>
        /// true表示使用
        /// </summary>
        [SheBeiLieTeXing(true, "是否使用")]
        public bool IsShiYong { get; set; } = true;
        /// <summary>
        /// 设备类型
        /// </summary>
        [SheBeiLieTeXing(true, "设备类型")]
        public string SheBeiType { get; set; } = "";

        /// <summary>
        /// 加载的文件名称
        /// </summary>
        [SheBeiLieTeXing(true, "加载文件")]
        public string JiaZaiWanJianName { get; set; } = "";

        /// <summary>
        /// 设备配置
        /// </summary>
        [SheBeiLieTeXing(true, "设备配置")]
        public string SheBeiPeiZhi { get; set; } = "";

        /// <summary>
        /// 设备配置
        /// </summary>
        [SheBeiLieTeXing(true, "设备组")]
        public string SheBeiZu { get; set; } = "";
    }
}
