using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.Model
{
    /// <summary>
    /// 路径model
    /// </summary>
    internal class PathModel
    {
        private string _LuJing = "";
        /// <summary>
        /// 路径
        /// </summary>
        public string LuJin { get { return _LuJing; } }

        public PathModel()
        {
             _LuJing = string.Format("{0}{1}", Directory.GetCurrentDirectory(), @"\SheBeiJK");
            if (Directory.Exists(_LuJing) == false)
            {
                Directory.CreateDirectory(_LuJing);
            }
        }
    }
}
