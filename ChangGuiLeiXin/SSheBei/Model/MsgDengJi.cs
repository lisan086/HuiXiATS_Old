using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 消息等级枚举
    /// </summary>
    public enum MsgDengJi
    {
        /// <summary>
        /// 正常显示
        /// </summary>
        SheBeiZhengChang = 0,
        /// <summary>
        /// 错误要显示
        /// </summary>
        SheBeiCuoWu = 1,
        /// <summary>
        /// 弹窗
        /// </summary>
        SheBeiTangChuang = 2,
        /// <summary>
        /// 不显示
        /// </summary>
        SheBeiBaoWen = 3,
        /// <summary>
        /// 设备写参数
        /// </summary>
        SheBeiXie=4,
    }

    /// <summary>
    /// 总线上的信息
    /// </summary>
    /// <param name="dengJi">消息等级</param>
    /// <param name="e">消息内容</param>  
    public delegate void ZongXianMsg(MsgDengJi dengJi, MsgModel e);
}
