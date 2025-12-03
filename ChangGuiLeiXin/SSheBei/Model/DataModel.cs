using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 总线返回的数据
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// 设备的ID
        /// </summary>
        public int SheBeiID { get; set; }
        /// <summary>
        /// true表示可靠数据
        /// </summary>
        public bool IsKeKao { get; set; } = false;

        /// <summary>
        /// 返回的读寄存器数据集合
        /// </summary>
        public List<JiCunQiModel> JiCunQiModels { get; set; } = new List<JiCunQiModel>();
    }
}
