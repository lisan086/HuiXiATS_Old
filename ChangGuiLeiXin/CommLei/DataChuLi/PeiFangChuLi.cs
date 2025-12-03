using CommLei.DataChuLi;
using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Common.DataChuLi
{
    /// <summary>
    /// 用于单个对象处理配方
    /// </summary>
    public  class PeiFangDanChuLi<T> where T:class,new()
    {
        /// <summary>
        /// 需要的model
        /// </summary>
        public T XuYaoModel { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        private string WenJianLuJing = "";

        /// <summary>
        /// 配方路径
        /// </summary>
        private string PeiFangLuJing = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public PeiFangDanChuLi()
        {
            XuYaoModel = new T();
            WenJianLuJing = HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().LisWuLiao.LuJin;
            PeiFangLuJing = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "PeiFang");//项目
            if (!Directory.Exists(PeiFangLuJing))
            {
                Directory.CreateDirectory(PeiFangLuJing);

            }
        }

        /// <summary>
        /// 获取配方名 true获取配方名 false获取配方路径
        /// </summary>
        /// <param name="ispeifangname"></param>
        /// <returns></returns>
        public List<string> GetPeiFang(bool ispeifangname)
        {
            string TexboxPath = PeiFangLuJing;
            
            List<string> list = new List<string>();
            try
            {
                //目录下所有文件路径
                string[] txt = Directory.GetFiles(TexboxPath);
                if (ispeifangname)
                {
                    foreach (string item in txt)
                    {
                        list.Add(ChangYong.GetWenJianName(item));//文件名
                    }
                }
                else
                {
                    foreach (string item in txt)
                    {
                        list.Add(item);//路径
                    }
                }
              

            }
            catch
            {

            }
            return list;
        }

        /// <summary>
        /// 设置当前配方 
        /// </summary>     
        /// <returns></returns>
        public bool SetDanQianBaoPeiFang()
        {
            if (string.IsNullOrEmpty(WenJianLuJing))
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.InitialDirectory= PeiFangLuJing;
                openFile.Filter = "*.txt|*.txt";
              
                if (openFile.ShowDialog()==DialogResult.OK)
                {
                    string name =ChangYong.GetWenJianName(openFile.FileName);                  
                    List<string> lujing = GetPeiFang(true);
                    if (lujing.IndexOf(name)>=0)
                    {
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().LisWuLiao.LuJin = name;
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().BaoCun();
                       
                        return true;
                    }
                  
                }
                return false;
            }
            else
            {
                List<string> lujing = GetPeiFang(true);
                for (int i = 0; i < lujing.Count; i++)
                {
                    if (lujing.Equals(WenJianLuJing))
                    {
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().LisWuLiao.LuJin = lujing[i];
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().BaoCun();
                       
                        return true;
                    }
                }
                return false;
            }
          
        }

        /// <summary>
        /// 加载配方
        /// </summary>     
        public void JiaZaiPeiFang()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = PeiFangLuJing;
            openFile.Filter = "*.txt|*.txt";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string name = openFile.FileName;

                JosnOrModel josnOrModel = new JosnOrModel(name);
                XuYaoModel = josnOrModel.GetTModel<T>();
                if (XuYaoModel == null)
                {
                    XuYaoModel = new T();
                }
                WenJianLuJing = ChangYong.GetWenJianName(name);
            }
           
        }
        /// <summary>
        /// 没有保存路径的文件名
        /// </summary>
        /// <param name="meiyoushiname"></param>
        /// <returns></returns>
        public void BaoCun(string meiyoushiname)
        {
            if (string.IsNullOrEmpty(WenJianLuJing))
            {
                string lujing = string.Format(@"{0}\{1}.txt", PeiFangLuJing, meiyoushiname);
                JosnOrModel josnOrModel = new JosnOrModel(lujing);
                josnOrModel.XieTModel(XuYaoModel);
                WenJianLuJing = meiyoushiname;
            }
            else
            {
                string lujing = string.Format(@"{0}\{1}.txt", PeiFangLuJing, WenJianLuJing);
                JosnOrModel josnOrModel = new JosnOrModel(lujing);
                josnOrModel.XieTModel(XuYaoModel);
              
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Close()
        {
            if (XuYaoModel!=null)
            {
                XuYaoModel = null;
            }
        }
    }

    /// <summary>
    /// 用于多个对象处理配方
    /// </summary>
    public class PeiFangLisChuLi<T> where T : class, new()
    {
        /// <summary>
        /// 需要的model
        /// </summary>
        public List<T> XuYaoModel { get; set; } = new List<T>();

        /// <summary>
        /// 文件名
        /// </summary>
        private string WenJianLuJing = "";

        /// <summary>
        /// 配方路径
        /// </summary>
        private string PeiFangLuJing = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public PeiFangLisChuLi()
        {      
            WenJianLuJing = HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().LisWuLiao.LuJin;
            PeiFangLuJing = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "PeiFang");//项目
            if (!Directory.Exists(PeiFangLuJing))
            {
                Directory.CreateDirectory(PeiFangLuJing);

            }
        }

        /// <summary>
        /// 获取配方名 true获取配方名 false获取配方路径
        /// </summary>
        /// <param name="ispeifangname"></param>
        /// <returns></returns>
        public List<string> GetPeiFang(bool ispeifangname)
        {
            string TexboxPath = PeiFangLuJing;

            List<string> list = new List<string>();
            try
            {
                //目录下所有文件路径
                string[] txt = Directory.GetFiles(TexboxPath);
                if (ispeifangname)
                {
                    foreach (string item in txt)
                    {
                        list.Add(ChangYong.GetWenJianName(item));//文件名
                    }
                }
                else
                {
                    foreach (string item in txt)
                    {
                        list.Add(item);//路径
                    }
                }


            }
            catch
            {

            }
            return list;
        }

        /// <summary>
        /// 设置当前配方 
        /// </summary>     
        /// <returns></returns>
        public bool SetDanQianBaoPeiFang()
        {
            if (string.IsNullOrEmpty(WenJianLuJing))
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.InitialDirectory = PeiFangLuJing;
                openFile.Filter = "*.txt|*.txt";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    string name = ChangYong.GetWenJianName(openFile.FileName);
                    List<string> lujing = GetPeiFang(true);
                    if (lujing.IndexOf(name) >= 0)
                    {
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().LisWuLiao.LuJin = name;
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().BaoCun();

                        return true;
                    }

                }
                return false;
            }
            else
            {
                List<string> lujing = GetPeiFang(true);
                for (int i = 0; i < lujing.Count; i++)
                {
                    if (lujing.Equals(WenJianLuJing))
                    {
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().LisWuLiao.LuJin = lujing[i];
                        HCDanGeDataLei<PeiFangXuanZeModel>.Ceratei().BaoCun();

                        return true;
                    }
                }
                return false;
            }

        }

        /// <summary>
        /// 加载配方
        /// </summary>     
        public void JiaZaiPeiFang()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = PeiFangLuJing;
            openFile.Filter = "*.txt|*.txt";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string name = openFile.FileName;

                JosnOrModel josnOrModel = new JosnOrModel(name);
                XuYaoModel = josnOrModel.GetLisTModel<T>();
                if (XuYaoModel == null)
                {
                    XuYaoModel = new List<T>();
                }
                WenJianLuJing = ChangYong.GetWenJianName(name);
            }

        }
        /// <summary>
        /// 没有保存路径的文件名
        /// </summary>
        /// <param name="meiyoushiname"></param>
        /// <returns></returns>
        public void BaoCun(string meiyoushiname)
        {
            if (string.IsNullOrEmpty(WenJianLuJing))
            {
                string lujing = string.Format(@"{0}\{1}.txt", PeiFangLuJing, meiyoushiname);
                JosnOrModel josnOrModel = new JosnOrModel(lujing);
                josnOrModel.XieTModel(XuYaoModel);
                WenJianLuJing = meiyoushiname;
            }
            else
            {
                string lujing = string.Format(@"{0}\{1}.txt", PeiFangLuJing, WenJianLuJing);
                JosnOrModel josnOrModel = new JosnOrModel(lujing);
                josnOrModel.XieTModel(XuYaoModel);

            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Close()
        {
            if (XuYaoModel != null)
            {
                XuYaoModel.Clear();
                XuYaoModel = null;
            }
        }

    }
    /// <summary>
    /// 选择那个路径的参数
    /// </summary>
    public  class PeiFangXuanZeModel
    {
        /// <summary>
        /// 路径名
        /// </summary>
        public string LuJin { get; set; } = "";
    }
}
