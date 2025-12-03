using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SheBeiTeXing
{
    /// <summary>
    /// 表格对于集合的显示特性
    /// </summary>
    public class BiaoGeXianShiTeXing : Attribute
    {
       

        private string Value = "";

        /// <summary>
        /// 构造函数用于表格的集合
        /// </summary>
        /// <param name="value"></param> 
        public BiaoGeXianShiTeXing(string value)
        {
            Value = value;
          
          
        }
       
        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetValue()
        {
            Dictionary<string, string> fenkai = new Dictionary<string, string>();
            string[] fenge = Value.Split(',');
            for (int i = 0; i < fenge.Length; i++)
            {
                string[] zaifen = fenge[i].Split(':');
                if (zaifen.Length>=2)
                {
                    if (fenkai.ContainsKey(zaifen[0])==false)
                    {
                        fenkai.Add(zaifen[0], zaifen[1]);
                    }
                }
            }
            return fenkai;
        }
       

    }
}
