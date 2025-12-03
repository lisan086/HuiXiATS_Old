using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 用于写入的参数
    /// </summary>
    public class XieRuMolde
    {

        /// <summary>
        /// 寄存器的唯一标识
        /// </summary>
        public string JiCunQiWeiYiBiaoShi { get; set; } = "";

        /// <summary>
        /// 设备的ID
        /// </summary>
        public int SheBeiID { get; set; }

        /// <summary>
        /// 写入的值
        /// </summary>
        public object Zhi { get; set; } = new object();

        /// <summary>
        /// 复制一个副本
        /// </summary>
        /// <returns></returns>
        public XieRuMolde FuZhi()
        {
            XieRuMolde fuzhimodel = new XieRuMolde();

            fuzhimodel.JiCunQiWeiYiBiaoShi = JiCunQiWeiYiBiaoShi;
            fuzhimodel.Zhi = Zhi;
            fuzhimodel.SheBeiID = SheBeiID;
            return fuzhimodel;
        }
    }


    /// <summary>
    /// 通信状态数据
    /// </summary>
    public class TxModel
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int SheBeiTD { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string SheBeiName { get; set; } = "";

        /// <summary>
        /// 设备组
        /// </summary>
        public string SheBeuZu { get; set; } = "";

        /// <summary>
        /// true需要判断
        /// </summary>
        public bool IsXuYaoPanDuan { get; set; } = true;

        /// <summary>
        /// true 表示所有通讯成功
        /// </summary>
        public bool ZongTX { get; set; } = false;
        /// <summary>
        /// 子设备通信状态
        /// </summary>
        public List<ZiTxModel> LisTx { get; set; } = new List<ZiTxModel>();
    }
    /// <summary>
    /// 子设备通信名称
    /// </summary>
    public class ZiTxModel
    {
        /// <summary>
        /// 子设备ID
        /// </summary>
        public int ZiSheBeiID { get; set; }

        /// <summary>
        /// 子设备名称
        /// </summary>
        public string ZiSheBeiName { get; set; } = "";
        /// <summary>
        /// true 通信成功
        /// </summary>
        public bool Tx { get; set; } = false;
    }
}
