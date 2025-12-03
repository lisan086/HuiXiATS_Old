using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.JieMianLei
{
    /// <summary>
    /// 列的特性
    /// </summary>
    public class LieTeXing : Attribute
    {
        private bool IsKeJian = false;

        private string LieName = "";

        private int DiJiLie = 0;

        private int ManKuan = 50;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iskejian"></param>
        /// <param name="liename"></param>
        /// <param name="dijilie"></param>
        /// <param name="mankuan"></param>
        public LieTeXing(bool iskejian, string liename,int dijilie,int mankuan)
        {
            IsKeJian = iskejian;
            LieName = liename;
            DiJiLie = dijilie;
            ManKuan = mankuan;
        }
        /// <summary>
        /// true表示可见
        /// </summary>
        /// <returns></returns>
        public bool GetKeJian()
        {
            return IsKeJian;
        }
        /// <summary>
        /// 获取显示列名
        /// </summary>
        /// <returns></returns>
        public string GetLieName()
        {
            return LieName;
        }

        /// <summary>
        /// 获取第几列
        /// </summary>
        /// <returns></returns>
        public int GetDiJiLie()
        {
            return DiJiLie;
        }

        /// <summary>
        /// 获取第几列
        /// </summary>
        /// <returns></returns>
        public int GetManKuan()
        {
            return ManKuan;
        }
    }
}
