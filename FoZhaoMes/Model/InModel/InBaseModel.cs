using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.InModel
{
    /// <summary>
    /// 输入参数
    /// </summary>
    public  class InFoShanBaseModel
    {
        /// <summary>
        /// 工单编码 1、条码未初始化必传 2、条码已初始化取条码工单
        /// </summary>
        public string lotCode { get; set; } = "";
        /// <summary>
        /// 工位名称
        /// </summary>
        public string stationName { get; set; } = "";
        /// <summary>
        ///工序名称
        /// </summary>
        public string stepName { get; set; } = "";
        /// <summary>
        ///用户编码
        /// </summary>
        public string userCode { get; set; } = "";
    }
}
