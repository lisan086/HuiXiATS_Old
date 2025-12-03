using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataChuLi
{
    /// <summary>
    /// 读取ini文件
    /// </summary>
    public class ReadOrWriteIni
    {
        /// <summary>
        /// 路径
        /// </summary>
        private string path;
        private int DaXiao = 1024 * 40;

        private bool IsKaiQiZhiNeng = false;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }



        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //方便遍历
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);

        /// <summary>
        /// 空的配置文档，需要定义属性Path
        /// </summary>
        public ReadOrWriteIni()
        {

        }
        /// <summary>
        /// 需要传入路径
        /// </summary>
        /// <param name="path1">文件存放路径</param>
        public ReadOrWriteIni(string path1)
        {
            this.path = path1;
        }
        /// <summary>
        /// 需要传入路径
        /// </summary>
        /// <param name="path1">文件存放路径</param>
        /// <param name="daxiao">文件存放大小</param> 
        public ReadOrWriteIni(string path1, int daxiao)
        {
            this.path = path1;
            DaXiao = daxiao;
            if (DaXiao <= 0)
            {
                DaXiao = 1024 * 10;
            }
        }

        /// <summary>
        /// 需要传入路径
        /// </summary>
        /// <param name="path1">文件存放路径</param>
        /// <param name="iszhineng">智能算法</param> 
        public ReadOrWriteIni(string path1, bool iszhineng)
        {
            this.path = path1;
            IsKaiQiZhiNeng = iszhineng;

        }
        /// <summary>
        /// 写入的参数
        /// </summary>
        /// <param name="section">大标题</param>
        /// <param name="key">键值索引</param>
        /// <param name="val">键值</param>
        /// <returns></returns>
        public bool WriteIni(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, path);
            if (duqu1(section, key) == val)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写入的参数
        /// </summary>
        /// <param name="section">大标题</param>
        /// <param name="key">键值索引</param>
        /// <param name="val">键值</param>
        /// <returns></returns>
        public bool WriteIni(string section, string key, bool val)
        {
            WritePrivateProfileString(section, key, val.ToString(), path);
            if (duqu1(section, key) == val.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写入的参数
        /// </summary>
        /// <param name="section">大标题</param>
        /// <param name="key">键值索引</param>
        /// <param name="val">键值</param>
        /// <returns></returns>
        public bool WriteIni(string section, string key, int val)
        {
            WritePrivateProfileString(section, key, val.ToString(), path);
            if (duqu1(section, key) == val.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写入的参数
        /// </summary>
        /// <param name="section">大标题</param>
        /// <param name="key">键值索引</param>
        /// <param name="val">键值</param>
        /// <returns></returns>
        public bool WriteIni(string section, string key, float val)
        {
            WritePrivateProfileString(section, key, val.ToString(), path);
            if (duqu1(section, key) == val.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 读取配置文件，返回一个string类型的键值
        /// </summary>
        /// <param name="section">大标题</param>
        /// <param name="key">键值索引</param>
        /// <returns>返回一个string类型的键值</returns>
        public string ReadIni(string section, string key)
        {
            int daxiao = DaXiao;
            if (IsKaiQiZhiNeng)
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    daxiao = (int)fileInfo.Length;
                }
                else
                {
                    return "";
                }

            }

            StringBuilder neirong = new StringBuilder(daxiao);
            GetPrivateProfileString(section, key, "", neirong, daxiao, path);
            return neirong.ToString();
        }

        /// <summary>
        /// 读取ini文件变成byte数据
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] ReadByte(string section, string key)
        {
            int daxiao = DaXiao;
            if (IsKaiQiZhiNeng)
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    daxiao = (int)fileInfo.Length;
                }
                else
                {
                    return new byte[] { 1 };
                }

            }

            byte[] shuju = new byte[daxiao];
            if (key == "")
            {
                key = null;
            }
            if (section == "")
            {
                section = null;
            }
            int i = GetPrivateProfileString(section, key, "", shuju, shuju.Length, this.path);
            return shuju;

        }
        private string duqu1(string section, string key)
        {
            int daxiao = DaXiao;
            if (IsKaiQiZhiNeng)
            {

                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    daxiao = (int)fileInfo.Length;
                }
                else
                {
                    return "";
                }

            }
            StringBuilder neirong = new StringBuilder(daxiao);
            string leirong = "";
            GetPrivateProfileString(section, key, leirong, neirong, daxiao, path);
            return neirong.ToString();
        }

        /// <summary>
        /// 删除某段落的某节点，
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">某个节点</param>
        public void RomveMouGeKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, path);
        }

        /// <summary>
        /// 删除父节点，不保留section
        /// </summary>
        /// <param name="section"></param>
        public void RomveSection(string section)
        {
            RomveMouGeKey(section, null);
        }

        /// <summary>
        /// 移除所有还有section
        /// </summary>
        /// <param name="filtersection"></param>
        public void RomveFilterSection(string filtersection)
        {
            List<string> section = GetSuoYouSection();
            foreach (var item in section)
            {
                if (item.Contains(filtersection))
                {
                    RomveSection(item);
                }
            }
        }

        /// <summary>
        /// 删除某段落的某节点，保留section
        /// </summary>
        /// <param name="section">段落</param>       
        public void RomveQuanBuKey(string section)
        {
            byte[] shuju = ReadByte(section, "");
            string jiexie = Encoding.Default.GetString(shuju);
            string[] huoqushuju = jiexie.Trim().Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in huoqushuju)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    RomveMouGeKey(section, item);
                }
            }
        }


        /// <summary>
        /// 获取section下的所有key值
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetSectionSuoYouKeyZhi(string section)
        {
            Dictionary<string, string> zidian = new Dictionary<string, string>();
            byte[] shuju = ReadByte(section, "");
            string jiexie = Encoding.Default.GetString(shuju);
            string[] huoqushuju = jiexie.Trim().Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in huoqushuju)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (zidian.ContainsKey(item) == false)
                    {
                        zidian.Add(item, duqu1(section, item));
                    }
                }
            }
            return zidian;
        }
        /// <summary>
        /// 获取所有section值
        /// </summary>
        /// <returns></returns>
        public List<string> GetSuoYouSection()
        {
            List<string> lis = new List<string>();
            byte[] shuju = ReadByte("", "");
            string jiexie = Encoding.Default.GetString(shuju);
            string[] huoqushuju = jiexie.Trim().Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in huoqushuju)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    lis.Add(item);
                }
            }
            return lis;
        }
        /// <summary>
        /// 找到与Filtesection一样的所有KeyValue值
        /// </summary>
        /// <param name="Filtesection"></param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, string>> GetFilteSectionKeyValue(string Filtesection)
        {
            Dictionary<string, Dictionary<string, string>> zidian = new Dictionary<string, Dictionary<string, string>>();

            List<string> lisstr = GetSuoYouSection();
            foreach (string item in lisstr)
            {
                if (item.Contains(Filtesection))
                {
                    if (zidian.ContainsKey(item) == false)
                    {
                        zidian.Add(item, GetSectionSuoYouKeyZhi(item));
                    }
                }
            }
            return zidian;
        }

    }
}
