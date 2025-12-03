using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpFanWen
{
   /// <summary>
   /// 返回的父类
   /// </summary>
   /// <typeparam name="T"></typeparam>
    public class OutBaseModel<T>
    {
        /// <summary>
        ///  接口返回是否成功,成功为true,失败为false
        /// </summary>
        public bool ChengGong { get; set; }

        /// <summary>
        ///  接口返回的josn数据，如果不是josn数据，就用空model
        /// </summary>
        public T ShuJu { get; set; }

         /// <summary>
         /// 消息
         /// </summary>
        public string Msg { get; set; } = "";


        /// <summary>
        ///  请求返回的内容
        /// </summary>
        public OutBaseModel()
        {
            ChengGong = false;
            ShuJu = default(T);
        }
    }

    /// <summary>
    /// 返回的父类
    /// </summary> 
    internal class OutBaseModel
    {
        /// <summary>
        ///  接口返回是否成功,成功为true,失败为false
        /// </summary>
        public bool ChengGong { get; set; }

        /// <summary>
        ///  接口返回的josn数据，如果不是josn数据，就用空model
        /// </summary>
        public object ShuJu { get; set; } = "";

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";


        /// <summary>
        ///  请求返回的内容
        /// </summary>
        public OutBaseModel()
        {
            ChengGong = false;
           
        }
    }
}
