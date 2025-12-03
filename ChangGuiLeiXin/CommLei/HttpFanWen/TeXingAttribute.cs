using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpFanWen
{
    /// <summary>
    /// 标记字段是否再用特性
    /// </summary>
    public class TeXingAttribute : Attribute
    {
        private bool IsShiYong = false;

        private string Name = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isshiyong"></param>
        public TeXingAttribute(bool isshiyong)
        {
            IsShiYong = isshiyong;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isshiyong"></param>
        /// <param name="name"></param>
        public TeXingAttribute(bool isshiyong, string name)
        {
            IsShiYong = isshiyong;
            Name = name;
        }
        /// <summary>
        /// 获取是否使用
        /// </summary>
        /// <returns></returns>
        public bool GetShiYong()
        {
            return IsShiYong;
        }
        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return Name;
        }
    }
}
