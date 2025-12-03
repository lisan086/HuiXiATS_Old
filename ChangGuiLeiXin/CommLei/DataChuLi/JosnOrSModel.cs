
using CommLei.JiChuLei;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.DataChuLi
{
    /// <summary>
    /// 新的josn配置，将支持连续作业
    /// </summary>
    public class JosnOrSModel
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        private string FilePath = "";

        /// <summary>
        /// 文件路径
        /// </summary>
        private string LianShiFilePath = "";

        /// <summary>
        /// 需要传来路径
        /// </summary>
        /// <param name="path"></param>
        public JosnOrSModel(string path)
        {
            FilePath = path;

            string huoquomulu = Path.GetDirectoryName(path);
            string wenjinming = Path.GetFileName(path);
            string[] houzui = wenjinming.Split('.');

            if (houzui.Length >= 2)
            {
                string shengcheng = SuiJiShengShuJu(houzui[0]);
                LianShiFilePath = string.Format(@"{0}\{1}.{2}", huoquomulu, shengcheng, houzui[1]);
            }
        }

        private string SuiJiShengShuJu(string wenjinming)
        {

            return string.Format("{0}{1}", wenjinming, "fuben");
        }
        /// <summary>
        /// 写进ini文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void XieTModel<T>(T model) where T : new()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }
            if (string.IsNullOrEmpty(LianShiFilePath))
            {
                return;
            }
            try
            {
                string josn = ChangYong.HuoQuJsonStr(model);
                byte[] shuju = Encoding.UTF8.GetBytes(josn);
                using (FileStream fs = new FileStream(LianShiFilePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(shuju, 0, shuju.Length);
                    fs.Flush(true);
                }

            }
            catch
            {

            }
            try
            {
                string josn = ChangYong.HuoQuJsonStr(model);
                byte[] shuju = Encoding.UTF8.GetBytes(josn);
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(shuju, 0, shuju.Length);
                    fs.Flush(true);
                }
            }
            catch
            {


            }
        }

        /// <summary>
        /// 写进ini文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void XieTModel<T>(List<T> model) where T : new()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }
            if (string.IsNullOrEmpty(LianShiFilePath))
            {
                return;
            }
            try
            {
                string josn = ChangYong.HuoQuJsonStr(model);
                byte[] shuju = Encoding.UTF8.GetBytes(josn);
                using (FileStream fs = new FileStream(LianShiFilePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(shuju, 0, shuju.Length);
                    fs.Flush(true);
                }

            }
            catch
            {

            }
            try
            {
                string josn = ChangYong.HuoQuJsonStr(model);
                byte[] shuju = Encoding.UTF8.GetBytes(josn);
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(shuju, 0, shuju.Length);
                    fs.Flush(true);
                }
            }
            catch
            {


            }
        }


        /// <summary>
        /// 能获取的对象只能是int flaot string datetime类型的属性对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetTModel<T>() where T : new()
        {
            bool zhen = File.Exists(FilePath);
            bool zhen1 = File.Exists(LianShiFilePath);
            if (zhen == false && zhen1 == false)
            {
                return default(T);
            }

            if (zhen)
            {
                List<T> DuiXiang = new List<T>();
                try
                {
                    bool duzhen = false;
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long intdata = fs.Length;
                        if (intdata <= 0)
                        {
                            duzhen = true;
                        }
                        if (duzhen == false)
                        {
                            byte[] shuju = new byte[intdata];
                            fs.Read(shuju, 0, shuju.Length);
                            string Josnshuju = Encoding.UTF8.GetString(shuju);
                            T canshu = ChangYong.HuoQuJsonToShiTi<T>(Josnshuju);
                            if (canshu != null)
                            {
                                DuiXiang.Add(canshu);
                            }
                            else
                            {
                                DuiXiang = ChangYong.HuoQuJsonToShiTi<List<T>>(Josnshuju);
                                if (DuiXiang == null)
                                {
                                    duzhen = true;
                                }
                            }
                        }
                    }
                    if (duzhen)
                    {
                        DuiXiang = new List<T>();
                        duzhen = false;
                        using (FileStream fs = new FileStream(LianShiFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            long intdata = fs.Length;
                            if (intdata <= 0)
                            {
                                duzhen = true;
                            }
                            if (duzhen == false)
                            {
                                byte[] shuju = new byte[intdata];
                                fs.Read(shuju, 0, shuju.Length);
                                string Josnshuju = Encoding.UTF8.GetString(shuju);
                                T canshu = ChangYong.HuoQuJsonToShiTi<T>(Josnshuju);
                                if (canshu != null)
                                {
                                    DuiXiang.Add(canshu);
                                }
                                else
                                {
                                    DuiXiang = ChangYong.HuoQuJsonToShiTi<List<T>>(Josnshuju);
                                }
                            }
                        }
                    }
                    if (DuiXiang.Count > 0)
                    {
                        return DuiXiang[0];
                    }
                }
                catch
                {


                }
            }
            else
            {
                if (zhen1)
                {
                    List<T> DuiXiang = new List<T>();
                    bool duzhen = false;
                    using (FileStream fs = new FileStream(LianShiFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long intdata = fs.Length;
                        if (intdata <= 0)
                        {
                            duzhen = true;
                        }
                        if (duzhen == false)
                        {
                            byte[] shuju = new byte[intdata];
                            fs.Read(shuju, 0, shuju.Length);
                            string Josnshuju = Encoding.UTF8.GetString(shuju);
                            T canshu = ChangYong.HuoQuJsonToShiTi<T>(Josnshuju);
                            if (canshu != null)
                            {
                                DuiXiang.Add(canshu);
                            }
                            else
                            {
                                DuiXiang = ChangYong.HuoQuJsonToShiTi<List<T>>(Josnshuju);
                            }
                        }
                    }
                    if (DuiXiang.Count > 0)
                    {
                        return DuiXiang[0];
                    }
                }
            }
            return default(T);
        }


        /// <summary>
        /// 获取lis的值（inilis的值）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetLisTModel<T>() where T : new()
        {
            bool zhen = File.Exists(FilePath);
            bool zhen1 = File.Exists(LianShiFilePath);
            if (zhen == false && zhen1 == false)
            {
                return new List<T>();
            }

            if (zhen)
            {
                List<T> DuiXiang = new List<T>();
                try
                {
                    bool duzhen = false;
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long intdata = fs.Length;
                        if (intdata <= 0)
                        {
                            duzhen = true;
                        }
                        if (duzhen == false)
                        {
                            byte[] shuju = new byte[intdata];
                            fs.Read(shuju, 0, shuju.Length);
                            string Josnshuju = Encoding.UTF8.GetString(shuju);
                            T canshu = ChangYong.HuoQuJsonToShiTi<T>(Josnshuju);
                            if (canshu != null)
                            {
                                DuiXiang.Add(canshu);
                            }
                            else
                            {
                                DuiXiang = ChangYong.HuoQuJsonToShiTi<List<T>>(Josnshuju);
                                if (DuiXiang == null)
                                {
                                    duzhen = true;
                                }
                            }
                        }
                    }
                    if (duzhen)
                    {
                        DuiXiang = new List<T>();
                        duzhen = false;
                        using (FileStream fs = new FileStream(LianShiFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            long intdata = fs.Length;
                            if (intdata <= 0)
                            {
                                duzhen = true;
                            }
                            if (duzhen == false)
                            {
                                byte[] shuju = new byte[intdata];
                                fs.Read(shuju, 0, shuju.Length);
                                string Josnshuju = Encoding.UTF8.GetString(shuju);
                                T canshu = ChangYong.HuoQuJsonToShiTi<T>(Josnshuju);
                                if (canshu != null)
                                {
                                    DuiXiang.Add(canshu);
                                }
                                else
                                {
                                    DuiXiang = ChangYong.HuoQuJsonToShiTi<List<T>>(Josnshuju);
                                }
                            }
                        }
                    }
                    return DuiXiang;
                }
                catch
                {


                }
            }
            else
            {
                if (zhen1)
                {
                    List<T> DuiXiang = new List<T>();
                    bool duzhen = false;
                    using (FileStream fs = new FileStream(LianShiFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long intdata = fs.Length;
                        if (intdata <= 0)
                        {
                            duzhen = true;
                        }
                        if (duzhen == false)
                        {
                            byte[] shuju = new byte[intdata];
                            fs.Read(shuju, 0, shuju.Length);
                            string Josnshuju = Encoding.UTF8.GetString(shuju);
                            T canshu = ChangYong.HuoQuJsonToShiTi<T>(Josnshuju);
                            if (canshu != null)
                            {
                                DuiXiang.Add(canshu);
                            }
                            else
                            {
                                DuiXiang = ChangYong.HuoQuJsonToShiTi<List<T>>(Josnshuju);
                            }
                        }
                    }
                    return DuiXiang;
                }
            }
            return new List<T>();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        public void ShanChuWenJian()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    File.Delete(FilePath);
                }
                catch
                {


                }

            }
        }
    }


    /// <summary>
    /// 新的josn配置，将支持连续作业
    /// </summary>
    internal class JosnOrXSModel
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        private string FilePath = "";

   

        /// <summary>
        /// 需要传来路径
        /// </summary>
        /// <param name="path"></param>
        public JosnOrXSModel(string path)
        {
            FilePath = path;


           
        }

     
        /// <summary>
        /// 写进ini文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void XieTModel<T>(T model) where T : new()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }
         
         
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (fs.Length == 0)
                    {
                        string xian = string.Format("[{0}]", ChangYong.HuoQuJsonStr(model));
                        byte[] shuju = Encoding.UTF8.GetBytes(xian);
                        fs.Write(shuju, 0, shuju.Length);
                        fs.Flush(true);
                    }
                    else
                    {
                        fs.Position = fs.Length - 1;
                        string xian = string.Format(",{0}]", ChangYong.HuoQuJsonStr(model));
                        byte[] shuju = Encoding.UTF8.GetBytes(xian);
                        fs.Write(shuju,0, shuju.Length);
                        fs.Flush(true);
                    }

                }
            }
            catch
            {


            }
        }

    
        /// <summary>
        /// 写数据的
        /// </summary>
        /// <param name="neirong"></param>
        public void XieTModel(string neirong) 
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }


            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (fs.Length == 0)
                    {
                        string xian = string.Format("[{0}]", neirong);
                        byte[] shuju = Encoding.UTF8.GetBytes(xian);
                        fs.Write(shuju, 0, shuju.Length);
                        fs.Flush(true);
                    }
                    else
                    {
                        fs.Position = fs.Length - 1;
                        string xian = string.Format("{0}]", neirong);
                        byte[] shuju = Encoding.UTF8.GetBytes(xian);
                        fs.Write(shuju, 0, shuju.Length);
                        fs.Flush(true);
                    }

                }
            }
            catch
            {


            }
        }



        /// <summary>
        /// 获取lis的值（inilis的值）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetLisTModel<T>() where T : new()
        {
            bool zhen = File.Exists(FilePath);
           
            if (zhen == false )
            {
                return new List<T>();
            }

            if (zhen)
            {
                List<T> DuiXiang = new List<T>();
                try
                {
                 
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        StreamReader reader = new StreamReader(fs,Encoding.UTF8);
                        var jr = new JsonTextReader(reader); //Newtonsoft JSON读取器，它解决了JSON数组流式返回需要分析json格式的问题。
                        JsonSerializer serializer = new JsonSerializer();
                        try
                        {
                            DuiXiang = serializer.Deserialize<List<T>>(jr);
                        }
                        catch
                        {

                          
                        }

                        jr.Close();
                        reader.Close();
                        reader.Dispose();
                      
                    }
                  
                    return DuiXiang;
                }
                catch
                {


                }
            }
     
            return new List<T>();
        }

    
    }
 
}
