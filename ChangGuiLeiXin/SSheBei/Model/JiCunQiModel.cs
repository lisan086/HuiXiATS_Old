using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 对外输出寄存器model
    /// </summary>
    public class JiCunQiModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string WeiYiBiaoShi { get; set; } = "";
      
        /// <summary>
        /// 寄存器写入读出参数
        /// </summary>
        public object Value { get; set; } ="";


        /// <summary>
        /// 设备的ID
        /// </summary>
        public int SheBeiID { get; set; }

        /// <summary>
        /// true  表示可靠
        /// </summary>
        public bool IsKeKao { get; set; } = false;

        /// <summary>
        /// 描述:赋值描述:最小值:最大值:单位:采用比较关系
        /// </summary>
        public string MiaoSu { get; set; } = "";

        /// <summary>
        ///1是读 2是写 3是读写一起
        /// </summary>
        public int DuXie { get; set; } = 1;

        /// <summary>
        /// 复制一个副本
        /// </summary>
        /// <returns></returns>
        public JiCunQiModel FuZhi()
        {
            JiCunQiModel model = new JiCunQiModel();
            model.SheBeiID = SheBeiID;
            model.WeiYiBiaoShi = WeiYiBiaoShi;
            model.Value = Value;
            model.DuXie = DuXie;
            model.IsKeKao = IsKeKao;
            model.MiaoSu = MiaoSu;
          
            return model;
        }
    }

    /// <summary>
    /// 校验结果的model
    /// </summary>
    public class JiaoYanJieGuoModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string WeiYiBiaoShi { get; set; } = "";

        /// <summary>
        /// 寄存器写入读出参数
        /// </summary>
        public object Value { get; set; } = new object();


        /// <summary>
        /// 设备的ID
        /// </summary>
        public int SheBeiID { get; set; }
    
        /// <summary>
        /// true 表示最终结果
        /// </summary>
        public JieGuoType IsZuiZhongJieGuo { get; set; } = JieGuoType.JingXingZhong;

    }

    /// <summary>
    /// 结果的类型
    /// </summary>
    public enum JieGuoType
    {
        /// <summary>
        /// 数据正在计算中，还没有结果
        /// </summary>
        JingXingZhong,
        /// <summary>
        /// 数据是失败结果
        /// </summary>
        ShiBaiJiGuo,
        /// <summary>
        /// 数据是成功的
        /// </summary>
        ChengGongJiGuo,
        /// <summary>
        /// 通信断开，导致不可靠结果
        /// </summary>
        BuKeKaoJieGuo,
        /// <summary>
        /// 没有找到该寄存器
        /// </summary>
        MeiZhaoDaoJiGuo,
    }
}
