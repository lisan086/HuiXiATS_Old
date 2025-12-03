using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.OutMdeol
{
    public  class OutZhuangXiangModel
    {
        /// <summary>
        /// 0是表示合格
        /// </summary>
        public int errorcode { get; set; } = 1;

        public string content { get; set; } = "";

        public int serialCheckCode { get; set; } = 0;

        public InfoModel info { get; set; } = new InfoModel();
    }

    public class InfoModel
    {
        public string id { get; set; } = "";
        public string productCode { get; set; } = "";
        public string lotCode { get; set; } = "";
        public string stationId { get; set; } = "";
        public string stepId { get; set; } = "";
        public string boxNumber { get; set; } = "";
      
    }

   
}
