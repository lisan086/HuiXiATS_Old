using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.OutMdeol
{
    public class OutYiBanModel
    {
        /// <summary>
        /// 0是表示合格
        /// </summary>
        public int errorcode { get; set; } = 1;

        public string content { get; set; } = "";

        /// <summary>
        /// 0是正常做 2是跳过 1是不合格
        /// </summary>
        public int serialcheckCode { get; set; } = 1;
    }
}
