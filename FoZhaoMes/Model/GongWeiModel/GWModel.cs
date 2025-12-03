using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.GongWeiModel
{
    public class GWModel
    {
        /// <summary>
        /// 通道ID
        /// </summary>
        public int GWID { get; set; } = -1;

        /// <summary>
        /// 工单编码
        /// </summary>
        public string lotCode { get; set; } = "";
        /// <summary>
        /// 工位名称
        /// </summary>
        public string stationName { get; set; } = "";
        /// <summary>
        /// 工序名称
        /// </summary>
        public string stepName { get; set; } = "";

        /// <summary>
        /// 用户编码
        /// </summary>
        public string userCode { get; set; } = "";

        /// <summary>
        /// 进站网址
        /// </summary>
        public string JinZhanWangZhi { get; set; } = "";

        /// <summary>
        /// 出站网址
        /// </summary>
        public string ChuZhanWangZhi { get; set; } = "";


        /// <summary>
        /// 装箱网址
        /// </summary>
        public string ZhuangXiangWangZhi { get; set; } = "";
        /// <summary>
        /// 装箱网址
        /// </summary>
        public string TiJiaoXiangZiWangZhi { get; set; } = "";
    }
}
