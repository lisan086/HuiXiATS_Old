using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuZhuangUI.Model
{
    public  class SheBeiZhanModel
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string LineCode { get; set; } = "AS1";
        /// <summary>
        ///工位ID
        /// </summary>
        public int GWID { get; set; } = 1;

        /// <summary>
        /// true 表示上传mes
        /// </summary>
        public bool IsMes { get; set; } = true;

        /// <summary>
        /// 其他参数
        /// </summary>
        public string QiTaCanShu { get; set; } = "";


        /// <summary>
        /// 1表示全部上传 其他表示遇到不合格就不上传
        /// </summary>
        public int IsQuanBuShangChuan { get; set; } = 1;

        /// <summary>
        /// 设备类型
        /// </summary>
        public SheBeiType IsZhengZhanDian { get; set; } = SheBeiType.GongWeiZhan;

        /// <summary>
        /// 型号
        /// </summary>
        public string XingHao { get; set; } = "";

        /// <summary>
        /// 带有打码系统的参数
        /// </summary>
        public string MaName { get; set; } = "";

        /// <summary>
        /// 切换型号用的
        /// </summary>
        public string QieXingHaoMa { get; set; } = "";

        /// <summary>
        /// 控件刷新的数据
        /// </summary>
        public KJCanShuFuModel KJCanShuFuModel { get; set; } = new KJCanShuFuModel();

        /// <summary>
        /// 每个站的数据
        /// </summary>
        public List<YeWuDataModel> LisData { get; set; } = new List<YeWuDataModel>();

        /// <summary>
        /// 每个站的请求
        /// </summary>
        public List<YeWuDataModel> LisQingQiu { get; set; } = new List<YeWuDataModel>();
    }

    public enum SheBeiType
    {
        [Description("Zhan")]
        GongWeiZhan,

    }


    public class KJCanShuFuModel
    {    
       
        public event Action ShuXinEvent;

        public void ShuXin(bool istongbu=false)
        {
            if (ShuXinEvent!=null)
            {
                if (istongbu)
                {
                    ShuXinEvent();
                }
                else
                {
                    ShuXinEvent.BeginInvoke(null,null);
                }
            }
        }
    }

    public class GongZhanModel: KJCanShuFuModel
    {
        private string _MiaoSu = "";
        public string MiaoSu 
        {
            get { return _MiaoSu; }
            set 
            {
                _MiaoSu = value;
               
            } 
        } 
        /// <summary>
        /// 1-开始测试 2-显示二维码 3-测试结束
        /// </summary>
        public int KaiQiTest { get; set; } = 0;
        public string ErWeiMa { get; set; } = "";
        public bool TestJieGuo { get; set; } = false;
    }

}
