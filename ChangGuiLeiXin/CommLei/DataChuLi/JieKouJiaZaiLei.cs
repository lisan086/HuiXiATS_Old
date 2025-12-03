using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataChuLi
{
    /// <summary>
    /// 加载接口用的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JieKouJiaZaiLei<T> where T : class
    {
        /// <summary>
        /// 加载接口，需要目录
        /// </summary>
        /// <param name="MuLu"></param>
        /// <returns></returns>
        public List<T> JiaZaiLisT(string MuLu)
        {
            List<T> ShuJuT = new List<T>();
            if (string.IsNullOrEmpty(MuLu))
            {
                return ShuJuT;
            }
            if (Directory.Exists(MuLu))
            {
                string[] filename = Directory.GetFiles(MuLu, "*.dll");
                if (filename.Length > 0)
                {
                    foreach (string item in filename)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }
                        Assembly xianshidllwenjian = Assembly.LoadFrom(item);
                        Type[] leixing = xianshidllwenjian.GetExportedTypes();
                        Type TJjk = typeof(T);
                        foreach (Type item1 in leixing)
                        {
                            if (TJjk.IsAssignableFrom(item1) && !item1.IsAbstract)
                            {
                                T tongjijiekou = (T)Activator.CreateInstance(item1);
                                ShuJuT.Add(tongjijiekou);
                            }
                        }
                    }
                    return ShuJuT;
                }
                else
                {
                    return ShuJuT;
                }

            }
            else
            {
                Directory.CreateDirectory(MuLu);
                return ShuJuT;
            }


        }

        /// <summary>
        /// 加载接口type类型，需要目录
        /// </summary>
        /// <param name="MuLu"></param>
        /// <returns></returns>
        public List<Type> JiaZaiLisType(string MuLu)
        {
            List<Type> ShuJuT = new List<Type>();
            if (string.IsNullOrEmpty(MuLu))
            {
                return ShuJuT;
            }
            if (Directory.Exists(MuLu))
            {
                string[] filename = Directory.GetFiles(MuLu, "*.dll");
                if (filename.Length > 0)
                {
                    foreach (string item in filename)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }
                        Assembly xianshidllwenjian = Assembly.LoadFrom(item);
                        Type[] leixing = xianshidllwenjian.GetExportedTypes();
                        Type TJjk = typeof(T);
                        foreach (Type item1 in leixing)
                        {
                            if (TJjk.IsAssignableFrom(item1) && !item1.IsAbstract)
                            {
                                //T tongjijiekou = (T)Activator.CreateInstance(item1);
                                ShuJuT.Add(item1);
                            }
                        }
                    }
                    return ShuJuT;
                }
                else
                {
                    return ShuJuT;
                }

            }
            else
            {
                Directory.CreateDirectory(MuLu);
                return ShuJuT;
            }


        }

        /// <summary>
        /// 加载接口type类型，需要目录,可以过滤的文件
        /// </summary>
        /// <param name="MuLu"></param>
        /// <param name="xuyaoyongdewenjian"></param>
        /// <returns></returns>
        public List<Type> JiaZaiLisType(string MuLu,List<string> xuyaoyongdewenjian)
        {
            List<Type> ShuJuT = new List<Type>();
            if (string.IsNullOrEmpty(MuLu))
            {
                return ShuJuT;
            }
            if (Directory.Exists(MuLu))
            {
                string[] filename = Directory.GetFiles(MuLu, "*.dll");
                if (filename.Length > 0)
                {
                    foreach (string item in filename)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }
                        string wenjianname = ChangYong.GetWenJianQuanName(item);
                        if (xuyaoyongdewenjian.IndexOf(wenjianname)<0)
                        {
                            continue;
                        }
                        Assembly xianshidllwenjian = Assembly.LoadFrom(item);
                        Type[] leixing = xianshidllwenjian.GetExportedTypes();
                        Type TJjk = typeof(T);
                        foreach (Type item1 in leixing)
                        {
                            if (TJjk.IsAssignableFrom(item1) && !item1.IsAbstract)
                            {
                                //T tongjijiekou = (T)Activator.CreateInstance(item1);
                                ShuJuT.Add(item1);
                            }
                        }
                    }
                    return ShuJuT;
                }
                else
                {
                    return ShuJuT;
                }

            }
            else
            {
                Directory.CreateDirectory(MuLu);
                return ShuJuT;
            }


        }

        /// <summary>
        /// 加载接口type类型，需要目录,可以过滤的文件,返回的是每个文件对应的实现Type
        /// </summary>
        /// <param name="MuLu"></param>
        /// <param name="xuyaoyongdewenjian"></param>
        /// <returns></returns>
        public Dictionary<string,List<Type>> JiaZaiXuYaoType(string MuLu, List<string> xuyaoyongdewenjian)
        {
            Dictionary<string, List<Type>> ShuJuT =new Dictionary<string, List<Type>>();
            if (string.IsNullOrEmpty(MuLu))
            {
                return ShuJuT;
            }
            if (Directory.Exists(MuLu))
            {
                string[] filename = Directory.GetFiles(MuLu, "*.dll");
                if (filename.Length > 0)
                {
                    foreach (string item in filename)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }
                        string wenjianname = ChangYong.GetWenJianQuanName(item);
                        if (xuyaoyongdewenjian.IndexOf(wenjianname) < 0)
                        {
                            continue;
                        }
                        Assembly xianshidllwenjian = Assembly.LoadFrom(item);
                        Type[] leixing = xianshidllwenjian.GetExportedTypes();
                        Type TJjk = typeof(T);
                        foreach (Type item1 in leixing)
                        {
                            if (TJjk.IsAssignableFrom(item1))
                            {
                                if (ShuJuT.ContainsKey(wenjianname)==false)
                                {
                                    ShuJuT.Add(wenjianname,new List<Type>());
                                }
                                //T tongjijiekou = (T)Activator.CreateInstance(item1);
                                ShuJuT[wenjianname].Add(item1);
                            }
                        }
                    }
                    return ShuJuT;
                }
                else
                {
                    return ShuJuT;
                }

            }
            else
            {
                Directory.CreateDirectory(MuLu);
                return ShuJuT;
            }


        }

        /// <summary>
        /// 加载接口type类型，需要目录,可以过滤的文件,返回的是每个文件对应的实现Type
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="directoryPath">目录</param>
        /// <param name="allowedFileNames">设备名称</param>
        /// <returns></returns>
        public Dictionary<string, List<Type>> LoadNeededTypes<T>(string directoryPath, List<string> allowedFileNames)
        {
            var result = new Dictionary<string, List<Type>>();

            if (string.IsNullOrEmpty(directoryPath)) return result;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return result;
            }

            string[] dllFiles = Directory.GetFiles(directoryPath, "*.dll");
            if (dllFiles.Length == 0) return result;

            // 将目标类型提取到循环外
            Type targetType = typeof(T);

            foreach (string filePath in dllFiles)
            {
                if (string.IsNullOrEmpty(filePath)) continue;

                // 使用标准库获取文件名
                string fileName = Path.GetFileName(filePath);

                // 检查白名单
                if (!allowedFileNames.Contains(fileName)) continue;

                try
                {
                    // 加载程序集
                    Assembly assembly = Assembly.LoadFrom(filePath);
                    Type[] exportedTypes = assembly.GetExportedTypes();

                    foreach (Type type in exportedTypes)
                    {
                        // 判断 type 是否继承自 T，且不是 T 本身（防止 T 是接口或抽象类时把自己加进去），且不是抽象类
                        if (targetType.IsAssignableFrom(type) && type != targetType && !type.IsAbstract)
                        {
                            if (!result.ContainsKey(fileName))
                            {
                                result.Add(fileName, new List<Type>());
                            }
                            result[fileName].Add(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 建议记录日志：某个 DLL 加载失败
                    Console.WriteLine($"无法加载 DLL {fileName}: {ex.Message}");
                }
            }

            return result;
        }

        /// <summary>
        /// 通过传来的文件来实现接口
        /// </summary>
        /// <param name="lisfilename"></param>
        /// <returns></returns>
        public List<T> JiaZaiLisT(List<string> lisfilename)
        {
            List<T> ShuJuT = new List<T>();
            if (lisfilename.Count > 0)
            {
                foreach (string item in lisfilename)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    if (!File.Exists(item))
                    {
                        continue;
                    }
                    Assembly xianshidllwenjian = Assembly.LoadFrom(item);
                    Type[] leixing = xianshidllwenjian.GetExportedTypes();
                    Type TJjk = typeof(T);
                    foreach (Type item1 in leixing)
                    {
                        if (TJjk.IsAssignableFrom(item1) && !item1.IsAbstract)
                        {
                            T tongjijiekou = (T)Activator.CreateInstance(item1);
                            ShuJuT.Add(tongjijiekou);
                        }
                    }
                }
                return ShuJuT;
            }
            else
            {
                return ShuJuT;
            }

        }
    }
}
