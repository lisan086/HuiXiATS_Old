using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoZhaoMes.Model.InModel
{
    /// <summary>
    /// 出站数据
    /// </summary>
    public class InChuZhanModel : InFoShanBaseModel
    {
        public List<ChuZhanDataModel> serialList { get; set; } = new List<ChuZhanDataModel>();
     
    }
  
    public class ChuZhanDataModel
    {
        public string serialNumber { get; set; } = "";
        /// <summary>
        /// 条码数量
        /// </summary>
        public int qty { get; set; } = 1;
        /// <summary>
        /// 
        /// </summary>
        public int panelIndex { get; set; } = 1;

        public bool isX { get; set; } = false;

        public List<TestDataModel> testDataList { get; set; } = new List<TestDataModel>();
        public List<BuLiangModel> defectList { get; set; } = new List<BuLiangModel>();

        public List<BangMaModel> assemblyList { get; set; } = new List<BangMaModel>();
    }

    public class TestDataModel
    {
        public string key { get; set; } = "";
        public string value { get; set; } = "";
        public string max { get; set; } = "";
        public string min { get; set; } = "";
        public string modelvalue { get; set; } = "";
        public string attrType { get; set; } = "";
        public string attrUnit { get; set; } = "";

        public string Result { get; set; } = "";
      
    }

    public class BuLiangModel
    {
        public string defectType { get; set; } = "";
        public string defectLabel { get; set; } = "";
        public string defectLocation { get; set; } = "";
        public string key { get; set; } = "";
        public string value { get; set; } = "";
        public string max { get; set; } = "";
        public string min { get; set; } = "";
        public string modelvalue { get; set; } = "";
        public string attrType { get; set; } = "";
        public string attrUnit { get; set; } = "";

        public string Result { get; set; } = "";
    }

    public class BangMaModel
    {
        public string assemblyName { get; set; } = "";
        public string assemblySn { get; set; } = "";
    }
}
