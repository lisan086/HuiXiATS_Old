using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUI.DaYIngMoBan.Model
{
    public class ZBiaoQianModel
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string name { get; set; } = "";

        /// <summary>
        /// 绘制文本的宽度
        /// </summary>
        public int KuanDu { get; set; } = 0;

        /// <summary>
        /// 绘制文本的高度
        /// </summary>
        public int GaoDu { get; set; } = 0;

        /// <summary>
        /// 绑定字段
        /// </summary>
        public string ZiDuan { get; set; } = "";

        /// <summary>
        /// 明细字段
        /// </summary>
        public string MingXiZiDuan { get; set; } = "";

        /// <summary>
        /// 1表示竖着打,  2表示横着打印
        /// </summary>
        public int DaYingMoShi { get; set; } = 1;

        /// <summary>
        /// true表示采用图片输出
        /// </summary>
        public bool IsCaiYongTuPianShuChu { get; set; } = false;

        /// <summary>
        /// 模板标签一般内容
        /// </summary>
        public List<BiaoQian> lc { get; set; }

        /// <summary>
        /// 明细标签
        /// </summary>
        public List<BiaoQian> MingXian { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ZBiaoQianModel()
        {
            lc = new List<BiaoQian>();
            MingXian = new List<BiaoQian>();
        }
    }

    /// <summary>
    /// 标签内容
    /// </summary>
    public class BiaoQian
    {
        /// <summary>
        /// 左上角x
        /// </summary>
        public int Dx { get; set; } = 0;
        /// <summary>
        /// 左上角y
        /// </summary>
        public int Dy { get; set; } = 0;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; } = 0;
        /// <summary>
        /// 高度
        /// </summary>
        public int Gao { get; set; } = 0;

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// 文本大小
        /// </summary>
        public float Size { get; set; } = 12;

        /// <summary>
        /// 字体编码，比如微软雅黑
        /// </summary>
        public string ZiTiYangShi { get; set; } = "微软姚黑";

        /// <summary>
        /// true表示加粗
        /// </summary>
        public bool IsJiaCu { get; set; } = false;

        /// <summary>
        /// true斜体
        /// </summary>
        public bool IsXieTi { get; set; } = false;

        /// <summary>
        /// 1是居左 2是居中 3是居右
        /// </summary>
        public int JuZhong { get; set; } = 1;

        /// <summary>
        /// 是否显示框，true表示显示
        /// </summary>
        public bool IsXianShiKuan { get; set; } = false;

        /// <summary>
        /// 属于什么控件1为文本控件，2线段 ,3表示矩形 4属于二维码 5属于图形
        /// </summary>
        public int Type { get; set; } = 1;

        /// <summary>
        /// 是否属于静态文本,1表示属于，2表示不属于
        /// </summary>
        public int ShiFouShuYuStaicWenBen { get; set; } = 1;

        /// <summary>
        /// 变量的样式，比如时间变量，等
        /// </summary>
        public string BianLianYangShi { get; set; } = "";

        /// <summary>
        /// 大于0表示同属
        /// </summary>
        public int TongShuID { get; set; } = -1;

        public GraphicsUnit GraphicsUnit { get; set; } = GraphicsUnit.Point;

        /// <summary>
        /// 存图片
        /// </summary>
        public string TuPianStr { get; set; } = "";
        /// <summary>
        /// 如果是图片类型  文件变成图片
        /// </summary>
        public bool IsYiTuPian { get; set; } = false;
    }
}
