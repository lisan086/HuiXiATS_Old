using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.InModel
{
    public  class InBangMaModel: InFoShanBaseModel
    {
        public List<ChuZhanDataModel> serialList { get; set; } = new List<ChuZhanDataModel>();
    }
    public class BangMaDataModel
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string serialNumber { get; set; } = "";
        /// <summary>
        /// 条码数量
        /// </summary>
        public int qty { get; set; } = 1;

        /// <summary>
        /// 绑定子码
        /// </summary>
        public List<ZiBangMaModel> subReplaceList { get; set; } = new List<ZiBangMaModel>();
    }

    public class ZiBangMaModel
    {
        /// <summary>
        /// 子绑码
        /// </summary>
        public string serialNumber { get; set; } = "";
        /// <summary>
        /// 从1开始递增
        /// </summary>
        public int panelIndex { get; set; } = 1;
        /// <summary>
        /// 非标识 false，标识品 true
        /// </summary>
        public bool isX { get; set; } = true;
    }
}
