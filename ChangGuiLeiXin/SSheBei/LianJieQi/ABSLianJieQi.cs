using SSheBei.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.LianJieQi
{
    /// <summary>
    /// 连接器的总称
    /// </summary>
    public abstract class ABSLianJieQi
    {

        /// <summary>
        /// 用于描述内容
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 通信状态 bool表示通信成功
        /// </summary>
        public abstract bool TongXinState { get; }

        /// <summary>
        /// 发送数据的方法,如果ResultModel.IsChengGong==true,表示发送数据成功
        /// </summary>
        /// <param name="data">发送数据</param>
        /// <returns></returns>
        public abstract ResultModel Send(byte[] data);
        /// <summary>
        /// 读取数据方法,如果ResultModel.IsChengGong==true,表示读取数据成功
        /// </summary>
        /// <returns></returns>
        public abstract ResultModel Read();

        /// <summary>
        ///打开通信 如果ResultModel.IsChengGong==true,表示打开成功
        /// </summary>
        /// <returns></returns>
        public abstract ResultModel Open();

        /// <summary>
        ///关闭通信 如果ResultModel.IsChengGong==true,表示关闭成功
        /// </summary>
        /// <returns></returns>
        public abstract ResultModel Close();

        /// <summary>
        /// 外部实现接口 设置参数
        /// </summary>
        /// <param name="model"></param>
        public abstract void SetCanShu(SetLianJieQiModel model);


        /// <summary>
        /// 重新连接 true表示重新连接成功
        /// </summary>
        /// <returns></returns>
        public abstract bool ChongLian();


    }
}
