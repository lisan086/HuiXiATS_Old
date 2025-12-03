using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.InModel
{
    public  class InZhuangXiangModel: InFoShanBaseModel
    {

    }

    public class InManXiangModel : InFoShanBaseModel
    {
        public string boxNumber { get; set; } = "";
        public float weight { get; set; } = 0;
    }

    public class InXiangChuZhanModel : InFoShanBaseModel
    {
        public string boxNumber { get; set; } = "";
        public List<string> serialNumberList { get; set; } = new List<string>();
      
        public string remark { get; set; } = "";
    }
}
