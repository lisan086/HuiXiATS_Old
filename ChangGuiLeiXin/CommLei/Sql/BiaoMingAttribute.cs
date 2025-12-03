using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Sql
{
    /// <summary>
    /// 表名特性
    /// </summary>
    public class BiaoMingAttribute : Attribute
    {
        /// <summary>
        /// true表示使用
        /// </summary>
        public bool IsShiYong { get; set; } = false;
        /// <summary>
        /// 表名
        /// </summary>
        public string BiaoName { get; set; } = "";

        /// <summary>
        /// 表名
        /// </summary>
        /// <param name="biaoming"></param>
        /// <param name="isshiyong"></param>
        public BiaoMingAttribute(string biaoming,bool isshiyong)
        {
            BiaoName = biaoming;
            IsShiYong = isshiyong;
        }
    }
}
