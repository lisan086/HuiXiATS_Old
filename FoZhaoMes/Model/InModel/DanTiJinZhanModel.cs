using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.InModel
{
    /// <summary>
    /// 单体追溯model
    /// </summary>
    public class DanTiJinZhanModel: InFoShanBaseModel
    {

        public List<JinZhanDataModel> serialList { get; set; } = new List<JinZhanDataModel>();
    }

    public class JinZhanDataModel
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string serialNumber { get; set; } = "";
        /// <summary>
        /// 条码数量
        /// </summary>
        public int qty { get; set; } =1;
    }

    public class DuoBanJinZhanModel: JinZhanDataModel
    {
        public int panelIndex { get; set; } = 1;
        public bool isX { get; set; } = false;
    }
}
