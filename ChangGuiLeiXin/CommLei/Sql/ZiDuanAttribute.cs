using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Sql
{
    /// <summary>
    /// 字段特性
    /// </summary>
    public class ZiDuanAttribute : Attribute
    {
        /// <summary>
        /// true表示使用
        /// </summary>
        public bool IsShiYong { get; set; } = false;
        /// <summary>
        /// 字段名称
        /// </summary>
        public  string ZiDuanName { get; set; } = "";
        /// <summary>
        /// true 表示更新键
        /// </summary>
        public  bool IsWhereGengXinJian { get; set; } = false;
        /// <summary>
        /// true表示自增键
        /// </summary>
        public  bool IsZiZeng { get; set; } = false;

        /// <summary>
        /// true表示删除键
        /// </summary>
        public bool IsShanChu{ get; set; } = false;

        /// <summary>
        /// /实例化
        /// </summary>
        /// <param name="ziduanname"></param>
        /// <param name="isshiyong"></param>
        /// <param name="isgengxinjian"></param>
        /// <param name="iszizeng"></param>
        /// <param name="isshanchu"></param>
        public ZiDuanAttribute(string ziduanname,bool isshiyong,bool isgengxinjian,bool iszizeng,bool isshanchu)
        {
            IsShiYong = isshiyong;
            ZiDuanName = ziduanname;
            IsWhereGengXinJian = isgengxinjian;
            IsZiZeng = iszizeng;
            IsShanChu = isshanchu;
        }
      
    }
}
