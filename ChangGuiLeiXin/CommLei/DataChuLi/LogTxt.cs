using CommLei.GongYeJieHe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommLei.DataChuLi
{
    /// <summary>
    /// 日记记录
    /// </summary>
    public class LogTxt
    {
        /// <summary>
        /// 开启日志，默认为不开启
        /// </summary>
        public bool KaiqiRiZhi { get; set; }
        /// <summary>
        /// 文件大于5M时，迁移路径
        /// </summary>
        private string _MovePath = "";
        /// <summary>
        /// 日志文件放在哪个路径
        /// </summary>
        private string _WenJianCunFangPath = "";    
        /// <summary>
        /// 文件大小
        /// </summary>
        public int WenJianDaiXiao { get; set; } = 1024 * 1024 * 15;

        private int JinTianHaoMa = 0;

        private Dictionary<string, string> AddNeiRong = new Dictionary<string, string>();

        private Dictionary<string, string> FZiDian = new Dictionary<string, string>();
        private Dictionary<string, string> JianCeDuiPath = new Dictionary<string, string>();

        private Dictionary<string, FanXingJiHeLei<string>> _CunShuJu = new Dictionary<string, FanXingJiHeLei<string>>();

        private Dictionary<string, string> ZiDianDuoGe = new Dictionary<string, string>();

        /// <summary>
        /// 日志是否一个路径,true为一个单个文件，通过规则来区分,true 为单路径，要通过文件名
        /// </summary>
        public bool IsDanDuCunFang { get; set; }
        private bool _bRuning = false;

        private string FileName = "";

        private Thread _ThWorking = null;

        #region 单例
        private static LogTxt _LogTxt = null;

        private readonly static object _DuiXiang = new object();




        private LogTxt()
        {
            string path = string.Format(@"{0}\{1}\{2}", Path.GetFullPath("."), "日志", DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            _WenJianCunFangPath = path;
            _MovePath= path;
            KaiqiRiZhi = false;
            _bRuning = true;
            IsDanDuCunFang = false;
            FileName = "log";
            _ThWorking = new Thread(Working);
            _ThWorking.IsBackground = true;
            _ThWorking.DisableComObjectEagerCleanup();
            _ThWorking.Start();
        }

        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static LogTxt Cerate()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new LogTxt();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion

        /// <summary>
        /// 关闭日志，只有在程序关闭时调用
        /// </summary>
        public void Close()
        {
            _bRuning = false;
            Thread.Sleep(10);
            ClearZiDian();
        }
        /// <summary>
        /// 增加日志
        /// </summary>
        /// <param name="strMsg">日记记录</param>
        /// <param name="FileNameOrGuiZe">文件名或者规则</param>
        public void AddMsg(string strMsg, string FileNameOrGuiZe)
        {
            if (KaiqiRiZhi)
            {
               
                if (IsDanDuCunFang)
                {
                    if (FZiDian.Keys.Contains(FileNameOrGuiZe))
                    {

                        string filename = FZiDian[FileNameOrGuiZe];
                     

                        _CunShuJu[filename].Add(string.Format("{0}┉→{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), strMsg));


                    }

                }
                else
                {
                    this.ZiDianDuoGe.Add(string.Format("{0}┉→{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), strMsg), FileName);
                }
            }
        }


        /// <summary>
        /// 当存放在一起的时候才可以用
        /// </summary>
        public void AddGuiZe(string FileName, string GuiZe)
        {
            if (!KaiqiRiZhi)
            {
                return;
            }
            if (IsDanDuCunFang)
            {
                try
                {
                    JinTianHaoMa = DateTime.Now.Day;
                    if (Directory.Exists(_WenJianCunFangPath)==false)
                    {
                        Directory.CreateDirectory(_WenJianCunFangPath);
                    }
                    if (Directory.Exists(_MovePath) == false)
                    {
                        Directory.CreateDirectory(_MovePath);
                    }
                    string wenjianpath = string.Format(@"{0}\{1}.{2}", _WenJianCunFangPath, FileName, "txt");
                    if (!File.Exists(wenjianpath))
                    {
                        File.Create(wenjianpath).Close();
                    }
                    if (FZiDian.ContainsKey(GuiZe))
                    {
                        FZiDian[GuiZe] = wenjianpath;
                    }
                    else
                    {
                        FZiDian.Add(GuiZe, wenjianpath);
                    }               
                    _CunShuJu.Add(wenjianpath, new FanXingJiHeLei<string>());
                    JianCeDuiPath.Add(wenjianpath, FileName);
                    if (AddNeiRong.ContainsKey(FileName) == false)
                    {
                        AddNeiRong.Add(FileName, GuiZe);
                    }
                }
                catch
                {


                }
            }
        }


        /// <summary>
        /// 删除字典
        /// </summary>
        public void ClearZiDian()
        {
           
            _CunShuJu.Clear();
            FZiDian.Clear();
            ZiDianDuoGe.Clear();
            JianCeDuiPath.Clear();
        }


        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="filename"></param>
        private void XieRiZhiXin(string Msg, string filename)
        {
            if (KaiqiRiZhi)
            {
                if (string.IsNullOrEmpty(filename))
                {
                    return;
                }
                if (!Directory.Exists(_WenJianCunFangPath))
                {
                    Directory.CreateDirectory(_WenJianCunFangPath);
                }
                if (!Directory.Exists(_MovePath))
                {
                    Directory.CreateDirectory(_MovePath);
                }
                if (!File.Exists(filename))
                {
                    File.Create(filename).Close();
                }
                #region 写入内容             
                FileInfo wenjianliu = new FileInfo(filename);
                if (wenjianliu.Length > WenJianDaiXiao)
                {
                    try
                    {
                        string pathtxt = string.Format(@"{0}\{1}{2}.{3}", _MovePath, JianCeDuiPath[filename], DateTime.Now.ToString("yyyyMMddHHmmss"), "txt");
                        File.Move(filename, pathtxt);

                    }
                    catch
                    {


                    }

                }

                using (StreamWriter wirte = wenjianliu.AppendText())
                {
                    try
                    {
                        wirte.Write(Msg);
                    }
                    catch (Exception ex)
                    {
                        wirte.Write(string.Format("{0}:{1},{2}", DateTime.Now.ToString(), "写入日志报错", ex.Message));
                    }

                }


                #endregion
            }
        }

        private void XieRiZhiMuLu(string Msg, string filename, string mulu)
        {
            if (KaiqiRiZhi)
            {

                if (mulu == "")
                {


                }
                else
                {
                    if (_MovePath == "")
                    {

                    }
                    else
                    {
                        string WenJianName = filename;
                        #region 写入内容
                        if (!Directory.Exists(mulu))
                        {
                            Directory.CreateDirectory(mulu);
                        }
                        if (!Directory.Exists(_MovePath))
                        {
                            Directory.CreateDirectory(_MovePath);
                        }
                        string wenjianpath = string.Format(@"{0}\{1}.{2}", mulu, "Log", "txt");
                        if (!string.IsNullOrEmpty(WenJianName))
                        {
                            wenjianpath = string.Format(@"{0}\{1}.{2}", mulu, WenJianName, "txt");
                        }

                        using (FileStream xieruliu = new FileStream(wenjianpath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            try
                            {
                                byte[] shuju = Encoding.Default.GetBytes(Msg);
                                xieruliu.Write(shuju, 0, shuju.Length);
                            }
                            catch
                            {



                            }


                        }

                        #endregion


                    }
                }
            }
        }



        private void Working()
        {
            
            while (_bRuning)
            {
                if (KaiqiRiZhi == false)
                {
                    Thread.Sleep(100);
                    continue;
                }
                try
                {
                    if (JinTianHaoMa != DateTime.Now.Day)
                    {
                       
                        JinTianHaoMa = DateTime.Now.Day;
                        string path = string.Format(@"{0}\{1}\{2}", Path.GetFullPath("."), "日志", DateTime.Now.ToString("yyyy-MM-dd"));
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                            _WenJianCunFangPath = path;
                            _MovePath = path;
                            foreach (var item in AddNeiRong.Keys)
                            {
                                AddGuiZe(item, AddNeiRong[item]);
                            }

                        }
                        else
                        {                       
                            _WenJianCunFangPath = path;
                            _MovePath = path;
                            foreach (var item in AddNeiRong.Keys)
                            {
                                AddGuiZe(item, AddNeiRong[item]);
                            }
                        }
                    }
                    if (IsDanDuCunFang)
                    {
                        if (_CunShuJu.Count > 0)
                        {
                            foreach (var item in _CunShuJu.Keys)
                            {
                                int count = _CunShuJu[item].GetCount();
                                if (count > 0)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    for (int i = 0; i < count; i++)
                                    {
                                        string shuju = _CunShuJu[item].GetModel_Head_RomeHead();
                                        sb.Append(shuju);
                                        if (KaiqiRiZhi == false || _bRuning == false)
                                        {
                                            break;
                                        }
                                    }
                                    XieRiZhiXin(sb.ToString(), item);
                                }
                                if (KaiqiRiZhi == false || _bRuning == false)
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        if (ZiDianDuoGe.Count > 0)
                        {
                            string mulu = string.Format(@"{0}\{1}", _WenJianCunFangPath, DateTime.Now.ToString("yyyy-MM-dd"));
                            string key = ZiDianDuoGe.Keys.First();
                            XieRiZhiMuLu(key, ZiDianDuoGe[key], mulu);

                            ZiDianDuoGe.Remove(key);
                        }
                    }

                }
                catch
                {


                }

                Thread.Sleep(200);
            }
        }


     

        /// <summary>
        /// 删除文件中文件，当文件数量大于多少进行删除 ShanChuCount删除数量
        /// </summary>
        /// <param name="Path">文件夹</param>
        /// <param name="Count">大于多少数量</param>
        /// <param name="ShanChuCount">大于多少数量</param>
        public void ShanChuWenJianCount(string Path, int Count, int ShanChuCount)
        {
            string path = "";
            if (Directory.Exists(Path))
            {
                path = Path;
            }
            else
            {
                path = _MovePath;
            }
            try
            {
                string[] wenjian = Directory.GetFileSystemEntries(Path);
                int counts = 0;
                if (wenjian.Length >= Count)
                {
                    counts = ShanChuCount;
                    if (counts> Count)
                    {
                        counts = Count;
                    }
                }
              
                for (int i = 0; i < counts; i++)
                {
                    int biaoshiss = IsWenJian(wenjian[i]);
                    if (biaoshiss == 1)
                    {
                        FileInfo fi = new FileInfo(wenjian[i]);
                        try
                        {
                            fi.Delete();
                        }
                        catch
                        {


                        }
                    }
                    else if (biaoshiss == 2)
                    {
                        DirectoryInfo info = new DirectoryInfo(wenjian[i]);
                        try
                        {
                            info.Delete(true);
                        }
                        catch
                        {


                        }
                    }
                }
            }
            catch 
            {

              
            }
         
        }

        /// <summary>
        /// 删除文件中文件，多少天为准
        /// </summary>
        /// <param name="Path">文件夹</param>
        /// <param name="TianShu">天数</param>
        public void ShanChuWenJian(string Path, int TianShu)
        {
            string path = "";
            if (Directory.Exists(Path))
            {
                path = Path;
            }
            else
            {
                path = _MovePath;
            }
            try
            {
                string[] wenjian = Directory.GetFileSystemEntries(Path);
                int counts = wenjian.Length;

                for (int i = 0; i < counts; i++)
                {
                    int biaoshiss = IsWenJian(wenjian[i]);
                    if (biaoshiss == 1)
                    {
                        FileInfo fi = new FileInfo(wenjian[i]);
                        if (fi.LastWriteTime.AddDays(TianShu) <= DateTime.Now)
                        {
                            try
                            {
                                fi.Delete();
                            }
                            catch
                            {


                            }

                        }
                    }
                    else if (biaoshiss == 2)
                    {
                        DirectoryInfo info = new DirectoryInfo(wenjian[i]);
                        if (info.LastWriteTime.AddDays(TianShu) <= DateTime.Now)
                        {
                            try
                            {
                                info.Delete(true);
                            }
                            catch
                            {


                            }
                        }
                    }
                }
            }
            catch
            {

            }

        }
        private int IsWenJian(string lumu)
        {
            if (File.Exists(lumu))
            {
                return 1;
            }
            else if (Directory.Exists(lumu))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
