using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 连接器的设置Model
    /// </summary>
    public class SetLianJieQiModel
    {
        /// <summary>
        /// ip与串口
        /// </summary>
        public string IPOrCOMStr { get; set; }

        /// <summary>
        /// 波特率与端口号
        /// </summary>
        public int SpeedOrPort { get; set; }
    }
}
