using Common.JieMianLei;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.SheBeiTeXing
{
    /// <summary>
    /// 获取设备特性的类
    /// </summary>
    public static class HuoQuSheBeiTeXing
    {
        /// <summary>
        /// 获取model的特性 第一个参数是列 第二个是变量名 第三个是列,第4个表示满宽
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static List<List<string>> GetSheBeiLie<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            List<List<string>> ziduan = new  List<List<string>>();
           
            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(SheBeiLieTeXing), true))
                {
                    object[] objects = item.GetCustomAttributes(true);
                    for (int i = 0; i < objects.Length; i++)
                    {
                        if (objects[i] is SheBeiLieTeXing)
                        {
                            SheBeiLieTeXing xid = objects[i] as SheBeiLieTeXing;
                            if (xid.GetKeJian()&&string.IsNullOrEmpty(xid.GetLieName())==false)
                            {
                                List<string> lis = new List<string>();
                                lis.Add(xid.GetLieName());
                                lis.Add(item.Name.ToString());
                                lis.Add(xid.GetDiJiLie().ToString());
                                lis.Add(xid.GetManKuan().ToString());
                                ziduan.Add(lis);
                            }
                        }
                    }
                 

                }
            }
            return ziduan;
        }

        /// <summary>
        /// 获取表格显示值的特性 第一个参数是vale，第二个值是 显示值 第三个是变量名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static List<List<string>> GetSheBeiXianShi<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            List<List<string>> ziduan = new List<List<string>>();

            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(BiaoGeXianShiTeXing), true))
                {
                    object[] objects = item.GetCustomAttributes(true);
                    for (int i = 0; i < objects.Length; i++)
                    {
                        if (objects[i] is BiaoGeXianShiTeXing)
                        {
                            BiaoGeXianShiTeXing xid = objects[i] as BiaoGeXianShiTeXing;
                            Dictionary<string, string> zhenshi = xid.GetValue();
                            if (zhenshi.Count>0)
                            {
                               
                                foreach (var itemd in zhenshi.Keys)
                                {
                                    List<string> lis = new List<string>();
                                    lis.Add(itemd);
                                    lis.Add(zhenshi[itemd]);
                                    lis.Add(item.Name);
                                    ziduan.Add(lis);
                                }
                              
                            }
                        }
                    }


                }
            }
            return ziduan;
        }
    }
}
