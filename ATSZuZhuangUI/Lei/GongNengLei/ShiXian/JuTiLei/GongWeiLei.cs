using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATSJianMianJK.GongNengLei;
using ATSJianMianJK.Log;
using ATSJianMianJK.Mes;
using ATSJianMianJK.XiTong.Model;
using ATSZuZhuangUI.Lei.GongNengLei;
using ATSZuZhuangUI.Lei.GongNengLei.ShiXian.KJ;
using BaseUI.DaYIngMoBan.Lei;
using CommLei.JiChuLei;
using ZuZhuangUI.Model;
using ZuZhuangUI.UI;

namespace ZuZhuangUI.Lei.GongNengLei.ShiXian
{
    public class GongWeiLei : ABSGongNengLei
    {
      
        private bool ZongKaiGuan = true;
        private SheBeiZhanModel SheBeiZhanModel;
        private GongZhanModel GongZhanModel;
        public override void IniData(SheBeiZhanModel model)
        {
          
            SheBeiZhanModel = model;
            GongZhanModel = new GongZhanModel();
            SheBeiZhanModel.KJCanShuFuModel = GongZhanModel;          
          
           
        }
        public override void Open()
        {
            ZongKaiGuan = true;
            Thread thread = new Thread(Work);
            thread.IsBackground = true;
            thread.DisableComObjectEagerCleanup();
            thread.Start();
        }
        private void Work()
        {
            if (SheBeiID<0)
            {
                return;
            }         
            DateTime jinzhantime = DateTime.Now;
            DateTime chuzhantime = DateTime.Now;
            bool jinzhan = false;
            bool chuzhan = false;
            while (ZongKaiGuan)
            {              
                //处理进站
                {
                    try
                    {
                        bool jinzhanzhi = DataJiHe.Cerate().GetGWPiPei(SheBeiID, CaoZuoType.Zhan进站请求_单);
                        if (jinzhanzhi)
                        {
                            if ((DateTime.Now - jinzhantime).TotalMilliseconds >= 500)
                            {
                                if (jinzhan == false)
                                {

                                    jinzhan = true;
                                    WriteLog(RiJiEnum.TDLiuCheng, $"收到PLC进站请求,准备校验");
                                    XieShuJu(CaoZuoType.Zhan进站请求_单, "", 1);
                                    JinZhan();
                                    WriteLog(RiJiEnum.TDLiuCheng, $"进站结束");
                                }
                                else
                                {
                                    WriteLog(RiJiEnum.TDLiuCheng, $"进站状态没有变化");
                                }
                                jinzhantime = DateTime.Now;
                            }

                        }
                        else
                        {
                            jinzhan = false;
                            jinzhantime = DateTime.Now;
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLog(RiJiEnum.TDLiuChengRed, $"进站{ex}");
                    }
                }
                //处理出站
                {
                    try
                    {

                        bool jinzhanzhi = DataJiHe.Cerate().GetGWPiPei(SheBeiID, CaoZuoType.Zhan出站请求_单);
                        if (jinzhanzhi)
                        {
                            if ((DateTime.Now - chuzhantime).TotalMilliseconds >= 500)
                            {
                                if (chuzhan == false)
                                {
                                    chuzhan = true;
                                    WriteLog(RiJiEnum.TDLiuCheng, $"收到PLC出站请求,准备上传数据");
                                  
                                    XieShuJu(CaoZuoType.Zhan出站请求_单, "", 1);                                  
                                    ChuZhanJiaoYan();                               
                                    WriteLog(RiJiEnum.TDLiuCheng, $"出站结束");
                                }
                                else
                                {
                                    WriteLog(RiJiEnum.TDLiuCheng, $"出站状态没有变化");
                                }
                                chuzhantime = DateTime.Now;
                            }

                        }
                        else
                        {
                            chuzhan = false;
                            chuzhantime = DateTime.Now;
                        }
                    }
                    catch (Exception ex)
                    {

                        WriteLog(RiJiEnum.TDLiuChengRed, $"出站{ex}");
                    }
                }
                Thread.Sleep(10);
            }
        }

      
        public override void Close()
        {
            ZongKaiGuan = false;
        }
        public override IFKJ GetFKJ()
        {
            IFKJ fKJ = new ZhanKJ();
            fKJ.SetModel(SheBeiZhanModel);
            return fKJ;
        }
        private void JinZhan()
        {
            {
                GongZhanModel.KaiQiTest = 1;
                GongZhanModel.MiaoSu = $"开始进站读取二维码";
                GongZhanModel.ShuXin(true);
            }
            string mesma = DataJiHe.Cerate().GetGWZhiStr(SheBeiID, CaoZuoType.Zhan进站过程码_单, "");
            bool ishege = true;        
            if (string.IsNullOrEmpty(mesma))
            {                                  
                WriteLog(RiJiEnum.TDLiuChengRed,$"读到进站二维码为空，进站失败:{mesma}");
                ishege = false;
            }
            else
            {            
                WriteLog(RiJiEnum.TDLiuCheng, $"读到出站二维码,开始进站:{mesma}");
                ishege = true;
            }
            {
                GongZhanModel.KaiQiTest = 2;
                GongZhanModel.ErWeiMa = mesma;
                GongZhanModel.MiaoSu = ishege? "开始上传二维码" : $"进站二维码为空";
                GongZhanModel.ShuXin(true);
            }
            if (ishege)
            {
                LianWanModel jieguo = JinZhanMes(mesma, "", false);
                if (jieguo.FanHuiJieGuo==JinZhanJieGuoType.NG)
                {
                    ishege = false;
                }
                WriteLog(RiJiEnum.TDLiuCheng, jieguo.NeiRong);
            }
            if (ishege)
            {
                XieShuJu(CaoZuoType.Zhan进站写结果_多,"",2);                    
                WriteLog(RiJiEnum.TDLiuCheng, $"mes校验合格，写合格结果，成功入站:{mesma}");
                {                  
                    GongZhanModel.MiaoSu = $"进站成功";
                    GongZhanModel.ShuXin(true);
                }
            }
            else
            {
                XieShuJu(CaoZuoType.Zhan进站写结果_多, "", 3);
                WriteLog(RiJiEnum.TDLiuCheng, $"mes校验不合格，写NG结果，NG入站:{mesma}");
                {
                    GongZhanModel.TestJieGuo = false;
                    GongZhanModel.KaiQiTest = 3;               
                    GongZhanModel.MiaoSu = $"进站失败";
                    GongZhanModel.ShuXin(true);
                }
            }          
           
        }

        private void ChuZhanJiaoYan()
        {

            {
                GongZhanModel.MiaoSu = $"准备出站读取二维码";
                GongZhanModel.ShuXin(false);
            }
            string mesma = DataJiHe.Cerate().GetGWZhiStr(SheBeiID, CaoZuoType.Zhan出站过程码_单, "");
            bool ishege = true;
            if (string.IsNullOrEmpty(mesma))
            {                  
                WriteLog(RiJiEnum.TDLiuChengRed,$"读到出站二维码为空，出站失败:{mesma}");              
                ishege = false;
            }
            else
            {           
                WriteLog(RiJiEnum.TDLiuCheng, $"读到出站二维码:{mesma}");
                ishege = true;
            }
            {
                GongZhanModel.ErWeiMa = mesma;
                GongZhanModel.MiaoSu = ishege ? "开始上传出站二维码数据" : $"出站二维码为空";
                GongZhanModel.KaiQiTest = 2;
                GongZhanModel.ShuXin(false);
            }
            if (ishege)
            {
                WriteLog(RiJiEnum.TDLiuCheng, $"开始获取出站数据");
                List<YeWuDataModel> lise = DataJiHe.Cerate().GetDataModel(SheBeiID,CaoZuoType.DataShangChuan,true);          
                if (lise.Count > 0)
                {
                    lise.Sort((x, y) =>
                    {
                        if (x.PaiXu > y.PaiXu)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    });
                }
                ClearData(mesma);
                WriteLog(RiJiEnum.TDLiuCheng, $"获取到的出站数据的数量:{lise.Count}");
                for (int i = 0; i < lise.Count; i++)
                {                   
                    if (lise[i].IsShangChuan==1)
                    {                      
                        lise[i].IsShuJuHeGe = JiaoYanHeGe(lise[i]);
                        lise[i].IsShangChuanHeGe = BuZhouShuJuMes(lise[i], mesma).FanHuiJieGuo==JinZhanJieGuoType.Pass;
                        if (lise[i].IsShuJuHeGe == false || lise[i].IsShangChuanHeGe == false)
                        {
                            ishege = false;

                        }
                        {
                            JieMianShiJianModel datamodel = new JieMianShiJianModel();
                            datamodel.GWID = SheBeiID;                        
                            datamodel.Data = lise[i];
                            ChuFaJieMianEvent(EventType.JiaZaiData, datamodel);
                        }
                        if (SheBeiZhanModel.IsQuanBuShangChuan != 1)
                        {
                            if (ishege==false)
                            {                               
                                break;
                            }
                        }                    
                    }
                }
               
                List<YeWuDataModel> bangmas = DataJiHe.Cerate().GetGWDataModel(SheBeiID, CaoZuoType.Zhan绑码_多, true);
                if (bangmas.Count > 0)
                {
                    List<string> bangms = new List<string>();
                    for (int i = 0; i < bangmas.Count; i++)
                    {
                        bangms.Add(bangmas[i].Value.JiCunValue.ToString());
                    }
                    LianWanModel bangmahege = BangMaJiaoYanMes(mesma, bangms,true);
                    if (bangmahege.FanHuiJieGuo == JinZhanJieGuoType.NG)
                    {
                        ishege = false;
                    }
                    WriteLog(RiJiEnum.TDLiuCheng, $"绑码结果:{bangmahege.NeiRong}");
                }
                WriteLog(RiJiEnum.TDLiuCheng, $"开始出站上传");
                LianWanModel shangchuan =ChuZhanMes(mesma, ishege,"");
                if (shangchuan.FanHuiJieGuo == JinZhanJieGuoType.NG)
                {
                    ishege = false;
                }
                WriteLog(RiJiEnum.TDLiuCheng, $"{shangchuan.NeiRong}");
             
            }
            if (ishege)
            {
                XieShuJu(CaoZuoType.Zhan出站写结果_多,"",2);            
                WriteLog(RiJiEnum.TDLiuCheng, $"{SheBeiName}，写合格结果，成功出站:{mesma}");
            }
            else
            {
                XieShuJu(CaoZuoType.Zhan出站写结果_多, "", 3);
                WriteLog(RiJiEnum.TDLiuCheng, $"{SheBeiName}:，写不合格结果，失败出站:{mesma}");
            }
            {
                GongZhanModel.TestJieGuo = ishege;
                GongZhanModel.KaiQiTest = 3;
                GongZhanModel.MiaoSu = ishege?"出站成功": $"出站失败";
                GongZhanModel.ShuXin(true);
            }
        }

      
    }
}
