using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 总线消息数据
    /// </summary>
    public class MsgModel : EventArgs
    {
        private string _Msg = "";
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get { return _Msg; } set { _Msg = string.Format("{0}:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), value); } }


        /// <summary>
        /// 设备的名称
        /// </summary>
        public string SheBeiName { get; set; } ="";

        /// <summary>
        /// 那些设备总有的消息
        /// </summary>
        public int SheBeiID{ get; set; }=-1;
    }
}
