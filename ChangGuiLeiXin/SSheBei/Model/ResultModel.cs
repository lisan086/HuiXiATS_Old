using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 接收的结果
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 是否成功，成功为true，失败为false
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public byte[] TData { get; set; } = null;

        /// <summary>
        /// 构造函数,默认是失败的,返回消息是空
        /// </summary>
        public ResultModel()
        {
            IsSuccess = false;
            Msg = "";
        }
        /// <summary>
        /// 设置返回的参数
        /// </summary>
        /// <param name="issuccess">是否成功</param>
        /// <param name="msg">消息</param>
        public void SetValue(bool issuccess, string msg)
        {
            IsSuccess = issuccess;
            Msg = msg;
        }

        /// <summary>
        /// 设置返回的参数
        /// </summary>
        /// <param name="issuccess">是否成功</param>
        /// <param name="msg">消息</param>
        /// <param name="tData">数据</param>
        public void SetValue(bool issuccess, string msg, byte[] tData)
        {
            IsSuccess = issuccess;
            Msg = msg;
            TData = tData;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="tData">返回的数据</param>
        public void SetValue(byte[] tData)
        {
            TData = tData;
        }
    }
}
