using CommLei.JiChuLei;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
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
    /// 读取model的ini文件,model是单一的没有复合model 也是安全model类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IniGetModelLei<T> where T : new()
    {
        private string TexboxPath = "";
        /// <summary>
        /// 保存的路径
        /// </summary>
        public string TexboxPath1
        {
            get { return TexboxPath; }
        }
        private ReadOrWriteIni _Ini;

        /// <summary>
        /// 需要传来biaoshi,自动创建路径
        /// </summary>
        /// <param name="biaoshi"></param>
        public IniGetModelLei(string biaoshi)
        {
            TexboxPath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "PeiZhi");//项目
            if (!Directory.Exists(TexboxPath))
            {
                Directory.CreateDirectory(TexboxPath);

            }
           
            string TexboxPaths = string.Format(@"{0}{1}M{2}.ini", TexboxPath, @"\", biaoshi);//项目
            _Ini = new ReadOrWriteIni(TexboxPaths, true);
           
        }
       

        /// <summary>
        /// 写进ini文件
        /// </summary>     
        /// <param name="model"></param>
        public void XieTModel(T model) 
        {
            Type t = model.GetType();
            string secion = t.Name;
            PropertyInfo[] shuxin = t.GetProperties();
            List<string> ziduans = new List<string>();
            for (int i = 0; i < shuxin.Length; i++)
            {
                ziduans.Add(shuxin[i].Name);
            }
            QingLiDuoYuKey(ziduans, secion);
            for (int i = 0; i < shuxin.Length; i++)
            {
                string key = shuxin[i].Name;
                try
                {
                    _Ini.WriteIni(secion, key, shuxin[i].GetValue(model, null).ToString());
                }
                catch
                {
                    _Ini.WriteIni(secion, key, "");
                }

            }
        }

        private void QingLiDuoYuKey(List<string> ziduan,string secion)
        {
            Dictionary<string, string> kesy = _Ini.GetSectionSuoYouKeyZhi(secion);
            List<string> keys = kesy.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (ziduan.IndexOf(keys[i])<0)
                {
                    _Ini.RomveMouGeKey(secion, keys[i]);
                }
            }
        }

        /// <summary>
        /// 写进ini文件
        /// </summary>
        /// <param name="model"></param>
        public void XieTModel(List<T> model)
        {
            Type t = typeof(T);
            string secion = t.Name;
            List<string> secions = _Ini.GetSuoYouSection();
            List<string> slis = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
                string s = string.Format("{0}{1}", secion, i);
                slis.Add(s);
            }
            for (int i = 0; i < secions.Count; i++)
            {
                if (slis.IndexOf(secions[i])<0)
                {
                    _Ini.RomveSection(secions[i]);
                }
            }
            for (int j = 0; j < model.Count; j++)
            {
                string s = string.Format("{0}{1}", secion, j);
                PropertyInfo[] shuxin = model[j].GetType().GetProperties();
                {
                    List<string> ziduans = new List<string>();
                    for (int i = 0; i < shuxin.Length; i++)
                    {
                        ziduans.Add(shuxin[i].Name);
                    }
                    QingLiDuoYuKey(ziduans, s);
                }
                {
                    for (int i = 0; i < shuxin.Length; i++)
                    {
                        string key = shuxin[i].Name;
                        try
                        {

                            _Ini.WriteIni(s, key, shuxin[i].GetValue(model[j], null).ToString());
                        }
                        catch
                        {
                            _Ini.WriteIni(s, key, "");
                        }

                    }
                }
            }

        }

        /// <summary>
        /// 获取某个key对应的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTModel(string key) 
        {
            Type t = typeof(T);
            return _Ini.ReadIni(t.Name, key);
        }

        /// <summary>
        /// 能获取的对象只能是int flaot string datetime类型的属性对象
        /// </summary>        
        /// <returns></returns>
        public T GetTModel() 
        {
            T duixiang = Activator.CreateInstance<T>();
            Type t = duixiang.GetType();

            string seceion = t.Name;
            Dictionary<string, string> zidian = _Ini.GetSectionSuoYouKeyZhi(seceion);
            if (zidian.Count > 0)
            {
                PropertyInfo[] shuxin = t.GetProperties();
                for (int i = 0; i < shuxin.Length; i++)
                {
                    string key = shuxin[i].Name;
                    FuZhu(duixiang, shuxin[i],zidian.ContainsKey(key)? zidian[key]:"");
                }
                return duixiang;
            }


            return default(T);
        }

        private T GetTmodel(Dictionary<string, string> shujuzidian) 
        {
            T duixiang = Activator.CreateInstance<T>();
            Type t = duixiang.GetType();

            string seceion = t.Name;
            Dictionary<string, string> zidian = shujuzidian;
            if (zidian.Count > 0)
            {
                PropertyInfo[] shuxin = t.GetProperties();
                for (int i = 0; i < shuxin.Length; i++)
                {
                    string key = shuxin[i].Name;
                    FuZhu(duixiang, shuxin[i], zidian.ContainsKey(key) ? zidian[key] : "");
                }
                return duixiang;
            }
            return default(T);
        }
        /// <summary>
        /// 获取lis的值（inilis的值）
        /// </summary>
        /// <returns></returns>
        public List<T> GetLisTModel() 
        {
            List<T> lisduixiang = new List<T>();

            Type t = typeof(T);
            string seceion = t.Name;
            Dictionary<string, Dictionary<string, string>> zidian = _Ini.GetFilteSectionKeyValue(seceion);
            if (zidian.Count > 0)
            {
                foreach (var item in zidian.Keys)
                {
                    T duixiang = GetTmodel(zidian[item]);
                    if (duixiang != null)
                    {
                        lisduixiang.Add(duixiang);
                    }
                }

            }


            return lisduixiang;
        }

        private void FuZhu(object duixiang, PropertyInfo shhuxing,string zhi)
        {
            try
            {
                if (shhuxing.PropertyType == typeof(int))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryInt(zhi, 0));
                }
                else if (shhuxing.PropertyType == typeof(float))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryFloat(zhi, 0f));
                }
                else if (shhuxing.PropertyType == typeof(string))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryStr( zhi,""));
                }
                else if (shhuxing.PropertyType == typeof(double))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryDouble(zhi, 0d));
                }
                else if (shhuxing.PropertyType == typeof(DateTime))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryDate(zhi, DateTime.Now));
                }
                else if (shhuxing.PropertyType == typeof(bool))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryStr(zhi, "false").ToLower().Contains("true"));
                }
                else if (shhuxing.PropertyType == typeof(byte))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryByte(zhi, (byte)0));
                }
                else if (shhuxing.PropertyType == typeof(short))
                {                  
                    shhuxing.SetValue(duixiang, ChangYong.TryShort(zhi,0));
                }
                else if (shhuxing.PropertyType == typeof(object))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryStr(zhi, ""));
                }            
                else if (shhuxing.PropertyType.BaseType.Name.ToLower().Contains("enum"))
                {
                   
                    shhuxing.SetValue(duixiang, ChangYong.GetMeiJuZhi(shhuxing.PropertyType, zhi));
                }
            }
            catch
            {

            }
        }
      
    }

    /// <summary>
    /// 读取model的ini文件,非泛型的
    /// </summary>
    public class IniGetModelLei
    {
        private string TexboxPath = "";
        /// <summary>
        /// 保存的路径
        /// </summary>
        public string TexboxPath1
        {
            get { return TexboxPath; }
        }
        private ReadOrWriteIni _Ini;

        /// <summary>
        /// 需要传来biaoshi,自动创建路径
        /// </summary>
        /// <param name="biaoshi"></param>
        public IniGetModelLei(string biaoshi)
        {
            TexboxPath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "PeiZhi");//项目
            if (!Directory.Exists(TexboxPath))
            {
                Directory.CreateDirectory(TexboxPath);

            }

            string TexboxPaths = string.Format(@"{0}{1}M{2}.ini", TexboxPath, @"\", biaoshi);//项目
            _Ini = new ReadOrWriteIni(TexboxPaths, true);

        }


        /// <summary>
        /// 写进ini文件
        /// </summary>     
        /// <param name="model"></param>
        public void XieTModel<T>(T model)
        {
            Type t = model.GetType();
            string secion = t.Name;
            PropertyInfo[] shuxin = t.GetProperties();
            List<string> ziduans = new List<string>();
            for (int i = 0; i < shuxin.Length; i++)
            {
                ziduans.Add(shuxin[i].Name);
            }
            QingLiDuoYuKey(ziduans, secion);
            for (int i = 0; i < shuxin.Length; i++)
            {
                string key = shuxin[i].Name;
                try
                {
                    _Ini.WriteIni(secion, key, shuxin[i].GetValue(model, null).ToString());
                }
                catch
                {
                    _Ini.WriteIni(secion, key, "");
                }

            }
        }

        private void QingLiDuoYuKey(List<string> ziduan, string secion)
        {
            Dictionary<string, string> kesy = _Ini.GetSectionSuoYouKeyZhi(secion);
            List<string> keys = kesy.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (ziduan.IndexOf(keys[i]) < 0)
                {
                    _Ini.RomveMouGeKey(secion, keys[i]);
                }
            }
        }

        /// <summary>
        /// 写进ini文件
        /// </summary>
        /// <param name="model"></param>
        public void XieTModel<T>(List<T> model)
        {
            Type t = typeof(T);
            string secion = t.Name;
            List<string> secions = _Ini.GetSuoYouSection();
            List<string> slis = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
                string s = string.Format("{0}{1}", secion, i);
                slis.Add(s);
            }
            for (int i = 0; i < secions.Count; i++)
            {
                if (slis.IndexOf(secions[i]) < 0)
                {
                    _Ini.RomveSection(secions[i]);
                }
            }
            for (int j = 0; j < model.Count; j++)
            {
                string s = string.Format("{0}{1}", secion, j);
                PropertyInfo[] shuxin = model[j].GetType().GetProperties();
                {
                    List<string> ziduans = new List<string>();
                    for (int i = 0; i < shuxin.Length; i++)
                    {
                        ziduans.Add(shuxin[i].Name);
                    }
                    QingLiDuoYuKey(ziduans, s);
                }
                {
                    for (int i = 0; i < shuxin.Length; i++)
                    {
                        string key = shuxin[i].Name;
                        try
                        {

                            _Ini.WriteIni(s, key, shuxin[i].GetValue(model[j], null).ToString());
                        }
                        catch
                        {
                            _Ini.WriteIni(s, key, "");
                        }

                    }
                }
            }

        }

        /// <summary>
        /// 获取某个key对应的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTModel(string key)
        {
            Type t = typeof(T);
            return _Ini.ReadIni(t.Name, key);
        }

        /// <summary>
        /// 能获取的对象只能是int flaot string datetime类型的属性对象
        /// </summary>        
        /// <returns></returns>
        public T GetTModel<T>()
        {
            T duixiang = Activator.CreateInstance<T>();
            Type t = duixiang.GetType();

            string seceion = t.Name;
            Dictionary<string, string> zidian = _Ini.GetSectionSuoYouKeyZhi(seceion);
            if (zidian.Count > 0)
            {
                PropertyInfo[] shuxin = t.GetProperties();
                for (int i = 0; i < shuxin.Length; i++)
                {
                    string key = shuxin[i].Name;
                    FuZhu(duixiang, shuxin[i], zidian.ContainsKey(key) ? zidian[key] : "");
                }
                return duixiang;
            }


            return default(T);
        }

        private T GetTmodel<T>(Dictionary<string, string> shujuzidian)
        {
            T duixiang = Activator.CreateInstance<T>();
            Type t = duixiang.GetType();

            string seceion = t.Name;
            Dictionary<string, string> zidian = shujuzidian;
            if (zidian.Count > 0)
            {
                PropertyInfo[] shuxin = t.GetProperties();
                for (int i = 0; i < shuxin.Length; i++)
                {
                    string key = shuxin[i].Name;
                    FuZhu(duixiang, shuxin[i], zidian.ContainsKey(key) ? zidian[key] : "");
                }
                return duixiang;
            }
            return default(T);
        }
        /// <summary>
        /// 获取lis的值（inilis的值）
        /// </summary>
        /// <returns></returns>
        public List<T> GetLisTModel<T>()
        {
            List<T> lisduixiang = new List<T>();

            Type t = typeof(T);
            string seceion = t.Name;
            Dictionary<string, Dictionary<string, string>> zidian = _Ini.GetFilteSectionKeyValue(seceion);
            if (zidian.Count > 0)
            {
                foreach (var item in zidian.Keys)
                {
                    T duixiang = GetTmodel<T>(zidian[item]);
                    if (duixiang != null)
                    {
                        lisduixiang.Add(duixiang);
                    }
                }

            }


            return lisduixiang;
        }

        private void FuZhu(object duixiang, PropertyInfo shhuxing, string zhi)
        {
            try
            {
                if (shhuxing.PropertyType == typeof(int))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryInt(zhi, 0));
                }
                else if (shhuxing.PropertyType == typeof(float))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryFloat(zhi, 0f));
                }
                else if (shhuxing.PropertyType == typeof(string))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryStr(zhi, ""));
                }
                else if (shhuxing.PropertyType == typeof(double))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryDouble(zhi, 0d));
                }
                else if (shhuxing.PropertyType == typeof(DateTime))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryDate(zhi, DateTime.Now));
                }
                else if (shhuxing.PropertyType == typeof(bool))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryStr(zhi, "false").ToLower().Contains("true"));
                }
                else if (shhuxing.PropertyType == typeof(byte))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryByte(zhi, (byte)0));
                }
                else if (shhuxing.PropertyType == typeof(short))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryShort(zhi, 0));
                }
                else if (shhuxing.PropertyType == typeof(object))
                {
                    shhuxing.SetValue(duixiang, ChangYong.TryStr(zhi, ""));
                }
                else if (shhuxing.PropertyType.BaseType.Name.ToLower().Contains("enum"))
                {

                    shhuxing.SetValue(duixiang, ChangYong.GetMeiJuZhi(shhuxing.PropertyType, zhi));
                }
            }
            catch
            {

            }
        }

       
    }
}
