using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuZhuangUI.Model
{
    public class JieMianShiJianModel
    {
        public int GWID { get; set; } = -1;
        public string TangChuangMsg{ get; set; } = "";
        public string PeiFangName { get; set; } = "";
    
        public YeWuDataModel Data { get; set; } = null;
    }

    /// <summary>
    /// 事件的类型
    /// </summary>
    public enum EventType
    {     
        QieHuanPeiFang,
        ClearData,
        JiaZaiData,
        TangChuang,    
    }

  

 
}
