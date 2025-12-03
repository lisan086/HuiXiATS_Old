using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.ShuJuModel
{
    public  class ZongShuJuModel
    {
        /// <summary>
        /// 3表示已经完成
        /// </summary>
        public int BiaoZhi { get; set; } = 0;

        /// <summary>
        /// 发送的数据
        /// </summary>
        public List<YeWuDataModel> LisDatas { get; set; } = new List<YeWuDataModel>();
    }
}
