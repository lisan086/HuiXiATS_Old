using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpFanWen
{
    /// <summary>
    /// http请求的父类参数
    /// </summary>
    public class InBaseModel
    {
        ///<summary>
        ///接口名称,每个都有接口名称了
        /// </summary>
        [TeXingAttribute(true)]
        public string JKName { get; set; }
        ///<summary>
        ///接口是否使用
        /// </summary>
        [TeXingAttribute(false)]
        public bool IsShiYong { get; set; }    
        /// <summary>
        /// 实例化
        /// </summary>
        public InBaseModel()
        {
            JKName = "";
            IsShiYong = true;
           
        }
    }
}
