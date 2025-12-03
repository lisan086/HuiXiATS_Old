using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuZhuangUI.Model
{
    public class YeWuDataModel
    {
        /// <summary>
        /// 检测项名称
        /// </summary>
        public string ItemName { get; set; } = "";
        /// <summary>
        /// 检测码与编号
        /// </summary>
        public string CodeOrNo { get; set; } = "";

        /// <summary>
        /// 下限值
        /// </summary>
        public ShuJuLisModel Low { get; set; } = new ShuJuLisModel();
        /// <summary>
        /// 上限
        /// </summary>
        public ShuJuLisModel Up { get; set; } = new ShuJuLisModel();

        /// <summary>
        /// 状态值
        /// </summary>
        public ShuJuLisModel State { get; set; } = new ShuJuLisModel();

        /// <summary>
        /// 值
        /// </summary>
        public ShuJuLisModel Value { get; set; } = new ShuJuLisModel();

        /// <summary>
        /// 工位ID
        /// </summary>
        public int GWID { get; set; } = 0;

        /// <summary>
        /// 1-PLC状态为准 2-plc和上位机判断为准 3是以上位机判断为准 4是相机对比
        /// </summary>
        public int IsYiZhuangTaiWeiZhun { get; set; } = 1;

        /// <summary>
        /// 1表示要上传
        /// </summary>
        public int IsShangChuan { get; set; } = 1;

        /// <summary>
        /// 请求匹配参数
        /// </summary>
        public string QingQiuPiPei { get; set; } = "";

        /// <summary>
        /// 请求清零值
        /// </summary>
        public string QingLingZhi { get; set; } = "";

        /// <summary>
        /// 合格值
        /// </summary>
        public string PassZhi { get; set; } = "";
        /// <summary>
        /// NG值
        /// </summary>
        public string  NGZhi{ get; set; } = "";

        /// <summary>
        /// 状态匹配值
        /// </summary>
        public string ZhuanTaiPiPeiZhi { get; set; } = "";
        /// <summary>
        /// 单位
        /// </summary>
        public string DanWei { get; set; } = "";

        /// <summary>
        /// 是否保存CSV
        /// </summary>
        public int CSVBaoCun { get; set; } = 1;

        /// <summary>
        /// 排序
        /// </summary>
        public int PaiXu { get; set; } = 0;

        /// <summary>
        /// true  表示本地数据合格
        /// </summary>
        public bool IsShuJuHeGe { get; set; } = true;

        /// <summary>
        /// true  表示上传合格
        /// </summary>
        public bool IsShangChuanHeGe { get; set; } = true;

      
        /// <summary>
        /// 操作数据
        /// </summary>
        public CaoZuoType CaoZuoType { get; set; } = CaoZuoType.Zhan写型号_单;

        /// <summary>
        /// 相机判断码的
        /// </summary>
        public string YongDeMaMingCheng { get; set; } = "";
        public override string ToString()
        {
            return $"[UpLimt:{Up.JiCunValue} LowLimt:{Low.JiCunValue} Value:{Value.JiCunValue} State:{IsShuJuHeGe} CaoZuoType:{CaoZuoType} ItemName:{ItemName} CodeOrNo:{CodeOrNo}]";
        }
    }

    public enum CaoZuoType
    {
        DataShangChuan=0,
        Zhan进站请求_单 =1,
        Zhan进站过程码_单=2,
        Zhan进站写结果_多=3,
        Zhan出站请求_单=4,
        Zhan出站过程码_单=5,
        Zhan出站写结果_多=6,
        Zhan写型号_单=7, 
        Zhan绑码_多 =8,
    }

    public class ShuJuLisModel
    {
        /// <summary>
        ///对应的下限寄存器
        /// </summary>
        public string JCQStr { get; set; } = "";

        /// <summary>
        /// 寄存器的设备ID
        /// </summary>
        public int SheBeiID { get; set; } = -1;

        /// <summary>
        /// 测试参数
        /// </summary>
        public object JiCunValue { get; set; } = "";
   

        /// <summary>
        /// true 是可靠
        /// </summary>
        public bool IsKeKao { get; set; } = false;
    }
}
