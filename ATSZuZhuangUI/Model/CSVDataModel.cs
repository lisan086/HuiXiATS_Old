using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSFoZhaoZuZhuangUI.Model
{
    public class CSVDataModel
    {
        [Description("名称")]
        public string ItemName { get; set; } = "";
        [Description("最小值")]
        public string Min { get; set; } = "";
        [Description("最大值")]
        public string Max { get; set; } = "";
        [Description("测试值")]
        public string Zhi { get; set; } = "";
        [Description("结果")]
        public string JieGuo { get; set; } = "";
        [Description("时间")]
        public string ShiJian { get; set; } = "";
        [Description("设备名称")]
        public string SheBeiName { get; set; } = "";
        [Description("设备编码")]
        public string SheBeiBianHao { get; set; } = "";

        [Description("二维码")]
        public string ErWeiMa { get; set; } = "";
    }


    public class BangSuoYouShuJuModel
    {
        public List<CSVDataModel> Datas { get; set; } = new List<CSVDataModel>();

        public string Ma { get; set; } = "";

        public bool IsHeGe { get; set; } = false;
    }
}
