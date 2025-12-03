using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.YuYin
{
    /// <summary>
    /// 语音类
    /// </summary>
    public abstract class ABSYuYin
    {
        /// <summary>
        /// 增加语音
        /// </summary>
        /// <param name="neirong"></param>
        public abstract void AddHua(string neirong);
        /// <summary>
        /// 设置音量
        /// </summary>
        public abstract int SetYingLiang { get; set; }

        /// <summary>
        /// 设置语速
        /// </summary>
        public abstract int SetYuShu { get; set; }

        /// <summary>
        /// KaiQi true表示开启语音 false 停止语音
        /// </summary>
        /// <param name="KaiQi"></param>
        public abstract void YuYinCaoZuo(bool KaiQi);

       

        /// <summary>
        /// 关闭的方法
        /// </summary>
        public abstract void Close();
    }
}
