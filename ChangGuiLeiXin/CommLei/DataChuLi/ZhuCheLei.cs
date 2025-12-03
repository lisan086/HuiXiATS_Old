using CommLei.DataChuLi;
using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DataChuLi
{
    /// <summary>
    /// 校验注册
    /// </summary>
    public class ZhuCheLei
    {
        private double ZongTime = 0;
        private double YongTime = 0;
        private DateTime ZongShiJian = DateTime.Now;
        private DateTime dt = DateTime.Now;
     
        private int IsZongZhuCe =0;
        private QiangJiaMi QiangJiaMi;
   
        private bool KaiGuan = false;
        private string BiaoShi = "";
     
        private int _GuoQi = 1;

        /// <summary>
        /// 0表示不过 1表示过
        /// </summary>
        public int GuoQi
        {
            get
            {
                return _GuoQi;
            }
        }



        #region 单例
        private static ZhuCheLei _LogTxt = null;

        private readonly static object _DuiXiang = new object();
        private ZhuCheLei()
        {
           
            QiangJiaMi = new QiangJiaMi();
      
            DuQu();
        }
        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static ZhuCheLei Ceratei()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new ZhuCheLei();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion

        private void DuQu()
        {
            ZhuCeModel model = HCDanGeDataLei<ZhuCeModel>.Ceratei().LisWuLiao;          
            BiaoShi = model.DianNaoMAC;
            string shouci = model.SCBiaoZhi;

            bool jianyan = GetMACID(BiaoShi);
            if (shouci == "95648")
            {
                jianyan = true;
                KaiShiZhuCe(30,false);
            }
            if (jianyan)
            {
                string iszhuce = QiangJiaMi.JieMi(model.IsZongZhuCe);
                string sss = ChangYong.TryStr(iszhuce, "000");
                if (sss.Equals("003"))
                {
                    _GuoQi = 0;
                    IsZongZhuCe = 1;
                }
                else
                {
                    string ztime = QiangJiaMi.JieMi(model.ZonTime);
                    ZongTime = ChangYong.TryDouble(ztime, 0d);
                    string yongtime = QiangJiaMi.JieMi(model.YongTime);
                    YongTime = ChangYong.TryDouble(yongtime, 0d);
                    string zongshijian = QiangJiaMi.JieMi(model.XianZhiRiQi);
                    ZongShiJian = DateTime.Now;
                    if (DateTime.TryParse(zongshijian, out ZongShiJian) == false)
                    {
                        ZongShiJian = DateTime.Now;
                    }
                    if (ZongTime >= YongTime)
                    {
                        if (ZongShiJian >= DateTime.Now)
                        {
                            _GuoQi = 0;
                        }
                        else
                        {
                            _GuoQi = 1;
                        }
                    }
                    else
                    {                       
                        _GuoQi = 1;
                    }
                }
            }
            else
            {
                KaiShiZhuCe(0,false);
                 _GuoQi = 1;
                IsZongZhuCe =0;
            }
            KaiGuan = true;
            Thread thread = new Thread(Work);
            thread.IsBackground = true;
            thread.DisableComObjectEagerCleanup();
            thread.Start();
        }
             
        private void Work()
        {
            dt = DateTime.Now;
            while (KaiGuan)
            {
                if (IsZongZhuCe==1)
                {

                    Thread.Sleep(1000);
                }
                else
                {
                    if (GuoQi == 1)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    try
                    {
                        double guoleduosshijian = (DateTime.Now - dt).TotalMilliseconds;
                        guoleduosshijian = guoleduosshijian / ((double)(60 * 60 * 1000));
                        double zongmiaoshu = YongTime + guoleduosshijian;

                        string qinfli = QiangJiaMi.JiaMi(zongmiaoshu.ToString("0.00"));
                        BaoCun(qinfli);

                        if (zongmiaoshu > ZongTime)
                        {
                            if (_GuoQi == 0)
                            {
                                _GuoQi = 1;

                            }
                        }
                        if (ZongShiJian < DateTime.Now)
                        {
                            if (_GuoQi == 0)
                            {
                                double zongstime = ZongTime;
                                string qinfli2 = QiangJiaMi.JiaMi(zongstime.ToString("0.00"));
                                BaoCun(qinfli2);
                                _GuoQi = 1;
                            }
                        }

                    }
                    catch
                    {


                    }
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// 保存用时
        /// </summary>
        /// <param name="yongshi"></param>
        private void BaoCun(string yongshi)
        {
            ZhuCeModel model = HCDanGeDataLei<ZhuCeModel>.Ceratei().LisWuLiao;
           
            model.YongTime = yongshi;
         
            HCDanGeDataLei<ZhuCeModel>.Ceratei().BaoCun();
        }

        /// <summary>
        /// 关闭的方法
        /// </summary>
        public void  Close()
        {
            KaiGuan = false;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="xiaoshi"></param>
        /// <param name="shijian"></param>
        ///  <param name="iszhengshi"></param>
        public bool ZhuCeShiJian(string xiaoshi, string shijian, string iszhengshi)
        {
            string zoongdd = QiangJiaMi.JieMi(xiaoshi);
            double xiaoshi2 = ChangYong.TryDouble(zoongdd,0d);
            if (xiaoshi2<=0)
            {
                return false;
            }
            double zongshijian = ZongTime + xiaoshi2;
            string zoong = QiangJiaMi.JiaMi(zongshijian.ToString());

            string sdaji = QiangJiaMi.JieMi(shijian);
            int tianshu = ChangYong.TryInt(sdaji, 0);
            if (tianshu<0)
            {
                return false;
            }

            DateTime time = DateTime.Now.AddDays(tianshu);
          
            string qingjiamia = QiangJiaMi.JiaMi(time.ToString("yyyy-MM-dd hh:mm:ss"));

            string zhusdds = QiangJiaMi.JieMi(iszhengshi);

            string iszhuce = zhusdds=="003" ? "003" : "0022";
            string iszhucue = QiangJiaMi.JiaMi(iszhuce.ToString());
          
            string shebei = string.Format("{0}", GetQiZhongMACID());
            BiaoShi = shebei;
            ZhuCeModel model = HCDanGeDataLei<ZhuCeModel>.Ceratei().LisWuLiao;
            string yongshi = QiangJiaMi.JieMi(model.YongTime);
            YongTime = ChangYong.TryDouble(yongshi, ZongTime);
            model.ZonTime = zoong;
            model.IsZongZhuCe = iszhucue;
            model.XianZhiRiQi = qingjiamia;           
            model.DianNaoMAC = shebei;
            HCDanGeDataLei<ZhuCeModel>.Ceratei().BaoCun();
            IsZongZhuCe = zhusdds == "003"?1:0;
            ZongShiJian = time;
            ZongTime = zongshijian;
            dt = DateTime.Now;
            _GuoQi = 0;
            return true;
        }

        private void KaiShiZhuCe(int tianshu,bool iskais)
        {
            
           
            string zoong = QiangJiaMi.JiaMi((tianshu*8).ToString());
            string qingjiamia = QiangJiaMi.JiaMi(DateTime.Now.AddDays(tianshu).ToString("yyyy-MM-dd hh:mm:ss"));
            string iszhucue = QiangJiaMi.JiaMi("0");
            string yongshi = QiangJiaMi.JiaMi("0");
            string shebei = string.Format("{0}", GetQiZhongMACID());
            ZhuCeModel model = HCDanGeDataLei<ZhuCeModel>.Ceratei().LisWuLiao;
            model.ZonTime = zoong;
            model.IsZongZhuCe = iszhucue;
            model.XianZhiRiQi = qingjiamia;
            model.YongTime = yongshi;
            model.DianNaoMAC = shebei;
            model.SCBiaoZhi = "5858";
            HCDanGeDataLei<ZhuCeModel>.Ceratei().BaoCun();
        }

     
        private string GetQiZhongMACID()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    string qutou = adapter.Id.Replace("{", "").Replace("}", "");
                    return qutou;
                }
            }
            return "";
        }
        private bool GetMACID(string jiaoyan)
        {
           
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    string shuju = string.Format("{0}", adapter.Id.Replace("{", "").Replace("}", ""));
                    if (shuju.Equals(jiaoyan))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }

    /// <summary>
    /// 检验注册的参数
    /// </summary>
    public class ZhuCeModel
    {
        /// <summary>
        /// 总时间(以小时为单位)
        /// </summary>
        public string ZonTime { get; set; } = "";

        /// <summary>
        /// 限制日期
        /// </summary>
        public string XianZhiRiQi { get; set; } = "";

        /// <summary>
        /// 总时间(以小时为单位)
        /// </summary>
        public string YongTime { get; set; } = "";

        /// <summary>
        /// 电脑的mac吗
        /// </summary>
        public string DianNaoMAC { get; set; } = "";

        /// <summary>
        /// 是否总注册 
        /// </summary>
        public string IsZongZhuCe { get; set; } = "";

        /// <summary>
        /// 首次标志
        /// </summary>
        public string SCBiaoZhi { get; set; } = "";
    }
}
