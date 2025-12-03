using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataChuLi
{

    /// <summary>
    /// 提供josn文件转对象,单线程改的,不能用于实时刷新,高速下有问题
    /// </summary>
    public class JosnOrModel
    {

        /// <summary>
        /// 文件路径
        /// </summary>
        private string FilePath = "";
        /// <summary>
        /// 需要传来路径
        /// </summary>
        /// <param name="path"></param>
        public JosnOrModel(string path)
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
            if (string.IsNullOrEmpty(FilePath))
            {
                return default(T);
            }
            try
            {
                List<T> DuiXiang = new List<T>();
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    long intdata = fs.Length;
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
                if (DuiXiang.Count > 0)
                {
                    return DuiXiang[0];
                }
            }
            catch
            {


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
            if (string.IsNullOrEmpty(FilePath))
            {
                return new List<T>();
            }
            try
            {
                List<T> DuiXiang = new List<T>();
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    long intdata = fs.Length;
                    byte[] shuju = new byte[intdata];
                    fs.Read(shuju, 0, shuju.Length);
                    string Josnshuju = Encoding.UTF8.GetString(shuju);
                    DuiXiang = ChangYong.HuoQuJsonToShiTi<List<T>>(Josnshuju);
                    if (DuiXiang == null)
                    {
                        DuiXiang = new List<T>();
                        T canshu = ChangYong.HuoQuJsonToShiTi<T>(Josnshuju);
                        if (canshu != null)
                        {
                            DuiXiang.Add(canshu);
                        }
                    }

                }
                if (DuiXiang == null)
                {
                    DuiXiang = new List<T>();
                }
                return DuiXiang;

            }
            catch
            {


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
}
