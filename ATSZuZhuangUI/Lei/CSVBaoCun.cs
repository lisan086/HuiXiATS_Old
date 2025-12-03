using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATSFoZhaoZuZhuangUI.Model;
using CommLei.GongYeJieHe;

namespace ATSFoZhaoZuZhuangUI.Lei
{
    public class CSVBaoCun
    {
        private FanXingJiHeLei<BangSuoYouShuJuModel> LisData = new FanXingJiHeLei<BangSuoYouShuJuModel>();
        /// <summary>
        /// 1表示单一上传 2表示所有数据在一起
        /// </summary>
        public int IsDanYiShangChuan { get; set; } = 2;
        private bool KaiGun = true;
        private Dictionary<string, string> BangDingZiDuan = new Dictionary<string, string>();
        #region 单利
        private readonly static object _DuiXiang = new object();
        private static CSVBaoCun _LogTxt = null;
        private CSVBaoCun()
        {
            IniData();
            Thread thread = new Thread(Work);
            thread.IsBackground = true;
            thread.DisableComObjectEagerCleanup();
            thread.Start();
        }



        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static CSVBaoCun Cerate()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new CSVBaoCun();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion

        private void IniData()
        {
            Type t = typeof(CSVDataModel);
            PropertyInfo[] shuxin = t.GetProperties();

            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(DescriptionAttribute), true))
                {
                    object[] objects = item.GetCustomAttributes(true);
                    for (int i = 0; i < objects.Length; i++)
                    {
                        if (objects[i] is DescriptionAttribute)
                        {
                            DescriptionAttribute xid = objects[i] as DescriptionAttribute;
                            string lieming = xid.Description;
                            if (BangDingZiDuan.ContainsKey(lieming)==false)
                            {
                                BangDingZiDuan.Add(lieming, item.Name);
                            }

                        }
                    }


                }
            }

        }

        public void SetCanShu(List<CSVDataModel> shuju,string ma,bool ishege)
        {
            BangSuoYouShuJuModel xinshuju = new BangSuoYouShuJuModel();
            xinshuju.Ma = ma;
            xinshuju.Datas = shuju;
            xinshuju.IsHeGe = ishege;
            LisData.Add(xinshuju);
        }

        public void Close()
        {
            KaiGun = false;
        }

        private string BiJiaoShuJu(bool ishege=false, string ma = "")
        {
            if (string.IsNullOrEmpty(ma))
            {
                string TexboxPath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "DataCSV");//项目
                if (!Directory.Exists(TexboxPath))
                {
                    Directory.CreateDirectory(TexboxPath);

                }
                string BaoPath1 = string.Format(@"{0}\{1}.csv", TexboxPath, DateTime.Now.ToString("yyyy-MM-dd"));
                return BaoPath1;
            }
            else
            {
                string TexboxPath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "DataCSV");//项目
                if (!Directory.Exists(TexboxPath))
                {
                    Directory.CreateDirectory(TexboxPath);

                }
                string BaoPath1 = string.Format(@"{0}\{1}_{2}_{3}.csv", TexboxPath,ma, ishege?"PASS":"NG" ,DateTime.Now.ToString("yyyy-MM-dd"));
                return BaoPath1;
            }
        }

        private void Work()
        {
            List<string> lienames = BangDingZiDuan.Keys.ToList();
            while (KaiGun)
            {
                try
                {
                    int count = LisData.GetCount();
                    if (count > 0)
                    {
                        BangSuoYouShuJuModel shuju = LisData.GetModel_Head_RomeHead();
                        if (IsDanYiShangChuan == 2)
                        {
                            Write(shuju.Datas, lienames);
                        }
                        else if (IsDanYiShangChuan == 1)
                        {
                            Write(shuju.Datas, lienames, shuju.Ma,shuju.IsHeGe);
                        }
                    }
                }
                catch
                {


                }

                Thread.Sleep(100);
            }
        }

        private void Write(List<CSVDataModel> model, List<string> lienames)
        {
            if (model == null || model.Count == 0)
            {
                return;
            }
            try
            {
                bool iscunzai = false;
                string wenjianfile = BiJiaoShuJu();
                if (!File.Exists(wenjianfile))
                {
                    File.Create(wenjianfile).Close();
                    iscunzai = true;
                }
                StringBuilder sbtou = new StringBuilder();
                if (iscunzai)
                {


                    for (int i = 0; i < lienames.Count; i++)
                    {
                        if (i == lienames.Count - 1)
                        {
                            sbtou.Append(string.Format("{0}{1}", lienames[i], "\r\n"));
                        }
                        else
                        {
                            sbtou.Append(string.Format("{0},", lienames[i]));
                        }
                    }


                }
                for (int c = 0; c < model.Count; c++)
                {
                    for (int i = 0; i < lienames.Count; i++)
                    {
                        if (i == lienames.Count - 1)
                        {
                            sbtou.Append(string.Format("{0}{1}", GetZiDuan(model[c], BangDingZiDuan[lienames[i]]), "\r\n"));
                        }
                        else
                        {
                            sbtou.Append(string.Format("{0},", GetZiDuan(model[c], BangDingZiDuan[lienames[i]])));
                        }
                    }

                }


                #region 写入内容             
                using (StreamWriter wirte = new StreamWriter(wenjianfile, true, Encoding.UTF8))
                {
                    try
                    {
                        wirte.Write(sbtou.ToString());
                    }
                    catch
                    {

                    }
                }
                #endregion
            }
            catch
            {
            }
        }

        private void Write(List<CSVDataModel> model, List<string> lienames,string ma,bool ishege)
        {
            if (model == null || model.Count == 0)
            {
                return;
            }
            try
            {
                bool iscunzai = false;
                string wenjianfile = BiJiaoShuJu(ishege,ma);
                if (!File.Exists(wenjianfile))
                {
                    File.Create(wenjianfile).Close();
                    iscunzai = true;
                }
                StringBuilder sbtou = new StringBuilder();
                if (iscunzai)
                {


                    for (int i = 0; i < lienames.Count; i++)
                    {
                        if (i == lienames.Count - 1)
                        {
                            sbtou.Append(string.Format("{0}{1}", lienames[i], "\r\n"));
                        }
                        else
                        {
                            sbtou.Append(string.Format("{0},", lienames[i]));
                        }
                    }


                }
                for (int c = 0; c < model.Count; c++)
                {
                    for (int i = 0; i < lienames.Count; i++)
                    {
                        if (i == lienames.Count - 1)
                        {
                            sbtou.Append(string.Format("{0}{1}", GetZiDuan(model[c], BangDingZiDuan[lienames[i]]), "\r\n"));
                        }
                        else
                        {
                            sbtou.Append(string.Format("{0},", GetZiDuan(model[c], BangDingZiDuan[lienames[i]])));
                        }
                    }

                }


                #region 写入内容             
                using (StreamWriter wirte = new StreamWriter(wenjianfile, true, Encoding.UTF8))
                {
                    try
                    {
                        wirte.Write(sbtou.ToString());
                    }
                    catch
                    {

                    }
                }
                #endregion
            }
            catch
            {
            }
        }
        private string GetZiDuan<T>(T model, string ziduan)
        {
            List<string> yuju = new List<string>();
            Type t = model.GetType();
            PropertyInfo[] shuxin = t.GetProperties();
            int lenth = shuxin.Length;
            for (int i = 0; i < lenth; i++)
            {
                if (shuxin[i].Name.Equals(ziduan))
                {
                    object shuu = shuxin[i].GetValue(model, null);
                    if (shuu != null)
                    {
                        return shuu.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }

            }

            return "";
        }

    }
}
