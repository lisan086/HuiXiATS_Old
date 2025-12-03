using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATSJianMianJK.Mes;
using ATSJianMianJK.QuanXian;
using CommLei.DataChuLi;
using CommLei.JiChuLei;
using Common.DataChuLi;
using FoZhaoMes.Model.GongWeiModel;
using FoZhaoMes.Model.InModel;
using FoZhaoMes.Model.OutMdeol;
using FoZhaoMes.Model.ShuJuModel;
using FoZhaoMes.PeiZhiFrm;

namespace FoZhaoMes.Lei
{
    public class FoShanShangMes : XuShangChuanLei
    {
        private readonly object Suo1 = new object();
        private readonly object Suo2 = new object();
        private Dictionary<string, List<YeWuDataModel>> CeShiData = new Dictionary<string, List<YeWuDataModel>>();
     
        private List<GWModel> CanShuModel = new List<GWModel>();
        public FoShanShangMes()
        {

          
        }

    
        public override void SetCanShu(int TDID)
        {
            List<GWModel> lis = HCLisDataLei<GWModel>.Ceratei().LisWuLiao;
            CanShuModel = lis;
            List<int> tdids = new List<int>();
            for (int i = 0; i < lis.Count; i++)
            {
                tdids.Add(lis[i].GWID);
            }
            XingDeMesQingQiuLei.Cerate().SetShuJu(tdids);
          
        }
        public override LianWanModel KaiShiTiJiaoMa(ShangChuanDataModel models)
        {
            LianWanModel jieguomodel = new LianWanModel();
            GWModel gWModel = GetTDModel(models.TDID);
            if (gWModel != null)
            {
                DanTiJinZhanModel qianmodel = new DanTiJinZhanModel();
                if (models.KaiShiModel.QiTaZhi.Contains("镭雕"))
                {
                    qianmodel.lotCode = "leidiaochecklot";
                }
                else
                {
                    qianmodel.lotCode = gWModel.lotCode;
                }
              
                qianmodel.stationName = gWModel.stationName;
                qianmodel.stepName = gWModel.stepName;
                qianmodel.userCode = gWModel.userCode;// QuanXianLei.CerateDanLi().GetDengLuMing();
                if (string.IsNullOrEmpty(models.KaiShiModel.QiTaZhi) == false && models.KaiShiModel.QiTaZhi.Contains(":"))//多板子进站
                {
                    string[] shujussma = models.KaiShiModel.QiTaZhi.Split(':');
                    for (int i = 0; i < shujussma.Length; i++)
                    {
                        DuoBanJinZhanModel jinzhanmodel = new DuoBanJinZhanModel();
                        jinzhanmodel.serialNumber = shujussma[i];
                        jinzhanmodel.panelIndex = i + 1;
                        jinzhanmodel.qty = 1;
                        qianmodel.serialList.Add(jinzhanmodel);
                    }
                }
                else
                {
                    JinZhanDataModel jinzhanmodel = new JinZhanDataModel();
                    jinzhanmodel.serialNumber = models.GuoChengMa;
                    qianmodel.serialList.Add(jinzhanmodel);
                }

                OutFoShanBaseModel<OutYiBanModel> jieguo = XingDeMesQingQiuLei.Cerate().QiuQiuHttp<DanTiJinZhanModel, OutYiBanModel>(qianmodel, gWModel.GWID, gWModel.JinZhanWangZhi);

                if (jieguo != null)
                {
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】{jieguo.Msg}";
                    if (jieguo.ChengGong)
                    {
                        jieguomodel.FanHuiCanShu = jieguo.ShuJu.serialcheckCode.ToString();
                        if (jieguo.ShuJu.errorcode == 0)
                        {
                            jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                        }

                    }

                }
                else
                {
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】通讯不良，导致没有反馈";
                }
            }
            else
            {
                jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                jieguomodel.NeiRong = $"【{models.TDName}】没有找到相应的配置";
            }

            return jieguomodel;
        }

        public override LianWanModel BuZhouShangChuan(ShangChuanDataModel models)
        {
            GWModel gWModel = GetTDModel(models.TDID);
            if (gWModel != null)
            {
                string key = $"{models.TDID}:{models.GuoChengMa}";
                YeWuDataModel shuju = ChangYong.HuoQuJsonToShiTi<YeWuDataModel>(ChangYong.HuoQuJsonStr(models.BuZhouModel.BuZhouShuJu));
                LianWanModel recmodels = new LianWanModel();
                if (shuju == null)
                {
                    recmodels.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    recmodels.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】没有实例化结果";
                }
                else
                {
                    recmodels.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                    recmodels.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】已经打包";
                    if (CeShiData.ContainsKey(key))
                    {
                        CeShiData[key].Add(shuju);
                    }
                    else
                    {
                        lock (Suo1)
                        {
                            if (CeShiData.ContainsKey(key) == false)
                            {
                                CeShiData.Add(key, new List<YeWuDataModel>());

                            }
                            CeShiData[key].Add(shuju);
                        }
                    }                 
                }
                return recmodels;
            }
            else
            {
                LianWanModel jieguomodel = new LianWanModel();
                jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                jieguomodel.NeiRong = $"【{models.TDName}】 没有找到相应的配置";
                return jieguomodel;
            }
        }

        public override void Close()
        {
        
            XingDeMesQingQiuLei.Cerate().Close();
        }

        /// <summary>
        /// 要重写
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override LianWanModel GetQiTaXinXi(ShangChuanDataModel models)
        {
            if (models.HuoQuXinXiModel.CanShu == "ZXK")
            {
                LianWanModel jieguomodel = new LianWanModel();
                GWModel gWModel = GetTDModel(models.TDID);
                if (gWModel != null)
                {
                    InZhuangXiangModel qianmodel = new InZhuangXiangModel();
                    qianmodel.lotCode = gWModel.lotCode;
                    qianmodel.stationName = gWModel.stationName;
                    qianmodel.stepName = gWModel.stepName;
                    qianmodel.userCode = gWModel.userCode;// QuanXianLei.CerateDanLi().GetDengLuMing();
                 
                    OutFoShanBaseModel<OutZhuangXiangModel> jieguo = XingDeMesQingQiuLei.Cerate().QiuQiuHttp<InZhuangXiangModel, OutZhuangXiangModel>(qianmodel, gWModel.GWID, gWModel.ZhuangXiangWangZhi);
                    if (jieguo != null)
                    {
                        jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                        jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】{jieguo.Msg}";
                        if (jieguo.ChengGong)
                        {                          
                            if (jieguo.ShuJu.errorcode == 0)
                            {
                                jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                                jieguomodel.FanHuiCanShu = jieguo.ShuJu.info.boxNumber.ToString();
                            }

                        }

                    }
                    else
                    {
                        jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                        jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】通讯不良，导致没有反馈";
                    }
                }
                else
                {
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    jieguomodel.NeiRong = $"【{models.TDName}】没有找到相应的配置";
                }

                return jieguomodel;

            }
            else if (models.HuoQuXinXiModel.CanShu.StartsWith("zxman,"))
            {
                string[] fenge= models.HuoQuXinXiModel.CanShu.Split(',');
                float zhongliang = ChangYong.TryFloat(fenge[1],0);
                LianWanModel jieguomodel = new LianWanModel();
                GWModel gWModel = GetTDModel(models.TDID);
                if (gWModel != null)
                {
                    InManXiangModel qianmodel = new InManXiangModel();
                    qianmodel.lotCode = gWModel.lotCode;
                    qianmodel.stationName = gWModel.stationName;
                    qianmodel.stepName = gWModel.stepName;
                    qianmodel.userCode = gWModel.userCode;// QuanXianLei.CerateDanLi().GetDengLuMing();
                    qianmodel.boxNumber = models.GuoChengMa;
                    qianmodel.weight = zhongliang;
                    OutFoShanBaseModel<OutYiBanModel> jieguo = XingDeMesQingQiuLei.Cerate().QiuQiuHttp<InManXiangModel, OutYiBanModel>(qianmodel, gWModel.GWID, gWModel.TiJiaoXiangZiWangZhi);
                    if (jieguo != null)
                    {
                        jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                        jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】{jieguo.Msg}";
                        if (jieguo.ChengGong)
                        {
                            if (jieguo.ShuJu.errorcode == 0)
                            {
                                jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                             
                            }

                        }

                    }
                    else
                    {
                        jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                        jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】通讯不良，导致没有反馈";
                    }
                }
                else
                {
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    jieguomodel.NeiRong = $"【{models.TDName}】没有找到相应的配置";
                }

                return jieguomodel;

            }
            else if (models.HuoQuXinXiModel.CanShu.StartsWith("ZX,"))
            {
                string[] fenge = models.HuoQuXinXiModel.CanShu.Split(',');
                if (fenge.Length >= 5)
                {
                    float zhongliang = ChangYong.TryFloat(fenge[1], 0);
                    LianWanModel jieguomodel = new LianWanModel();
                    GWModel gWModel = GetTDModel(models.TDID);
                    if (gWModel != null)
                    {
                        InXiangChuZhanModel qianmodel = new InXiangChuZhanModel();
                        qianmodel.lotCode = gWModel.lotCode;
                        qianmodel.stationName = gWModel.stationName;
                        qianmodel.stepName = gWModel.stepName;
                        qianmodel.userCode = gWModel.userCode;// QuanXianLei.CerateDanLi().GetDengLuMing();
                        qianmodel.boxNumber = fenge[4];
                        qianmodel.serialNumberList.Add( models.GuoChengMa);
                        qianmodel.remark = $"第{ChangYong.TryInt(fenge[3],0)}排 {ChangYong.TryInt(fenge[2], 0)}列 第{ChangYong.TryInt(fenge[1], 0)}层";
                        OutFoShanBaseModel<OutYiBanModel> jieguo = XingDeMesQingQiuLei.Cerate().QiuQiuHttp<InXiangChuZhanModel, OutYiBanModel>(qianmodel, gWModel.GWID, gWModel.ChuZhanWangZhi);
                        if (jieguo != null)
                        {
                            jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                            jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】{jieguo.Msg}";
                            if (jieguo.ChengGong)
                            {
                                if (jieguo.ShuJu.errorcode == 0)
                                {
                                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.Pass;

                                }

                            }

                        }
                        else
                        {
                            jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                            jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】通讯不良，导致没有反馈";
                        }
                    }
                    else
                    {
                        jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                        jieguomodel.NeiRong = $"【{models.TDName}】没有找到相应的配置";
                    }
                    return jieguomodel;
                }
                else
                {
                    LianWanModel jieguomodel = new LianWanModel();
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    jieguomodel.NeiRong = $"【{models.TDName}】数据格式有问题:{models.HuoQuXinXiModel.CanShu}";
                    return jieguomodel;
                }
              

            }
            else if (models.HuoQuXinXiModel.CanShu == "agvweizhi")
            {
                LianWanModel lianWanModel = new LianWanModel();
                lianWanModel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                lianWanModel.FanHuiCanShu = "2";
                lianWanModel.NeiRong = $"【{models.TDName}】发送装箱启动";
                return lianWanModel;
            }
            else
            {
                LianWanModel lianWanModel = new LianWanModel();
                lianWanModel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                lianWanModel.FanHuiCanShu = "2";
                lianWanModel.NeiRong = $"【{models.TDName}】PCB校验成功";
                return lianWanModel;
            }
        }

        /// <summary>
        /// 要写
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override LianWanModel JieSu(ShangChuanDataModel models)
        {
            GWModel gWModel = GetTDModel(models.TDID);
            if (gWModel != null)
            {
                bool shifouzai = false;
                InChuZhanModel qianmodel = new InChuZhanModel();
                qianmodel.lotCode = gWModel.lotCode;
                qianmodel.stationName = gWModel.stationName;
                qianmodel.stepName = gWModel.stepName;
                qianmodel.userCode = gWModel.userCode;
                if (models.GuoChengMa.Contains(":"))//多板码
                {
                    string[] fenge = models.GuoChengMa.Split(':');
                    for (int i = 0; i < fenge.Length; i++)
                    {
                        string key = $"{models.TDID}:{fenge[i]}";
                        qianmodel.serialList.Add(new ChuZhanDataModel());
                        qianmodel.serialList[i].serialNumber = fenge[i];
                        qianmodel.serialList[i].panelIndex = i+1;
                        if (CeShiData.ContainsKey(key))
                        {
                            shifouzai = true;
                            List<BuLiangModel> buliang = new List<BuLiangModel>();
                            List<TestDataModel> testmodels = GetChuZhanData(CeShiData[key], gWModel.stationName, gWModel.stepName, out buliang);
                            qianmodel.serialList[i].testDataList.AddRange(testmodels);
                            if (buliang.Count > 0)
                            {
                                qianmodel.serialList[i].defectList.AddRange(buliang);
                            }
                          
                        }
                        if (shifouzai)
                        {
                            lock (Suo2)
                            {
                                if (CeShiData.ContainsKey(key))
                                {
                                    CeShiData[key].Clear();
                                    CeShiData.Remove(key);
                                }
                            }
                        }

                    }
                }
                else
                {
                    string key = $"{models.TDID}:{models.GuoChengMa}";
                    qianmodel.serialList.Add(new ChuZhanDataModel());
                    qianmodel.serialList[0].serialNumber = models.GuoChengMa;
                    if (CeShiData.ContainsKey(key))
                    {
                        shifouzai = true;
                        List<BuLiangModel> buliang = new List<BuLiangModel>();
                        List<TestDataModel> testmodels = GetChuZhanData(CeShiData[key], gWModel.stationName, gWModel.stepName, out buliang);
                        qianmodel.serialList[0].testDataList.AddRange(testmodels);
                        if (buliang.Count > 0)
                        {
                            qianmodel.serialList[0].defectList.AddRange(buliang);
                        }
                        if (string.IsNullOrEmpty(models.JieSuModel.CanShu)==false)
                        {
                            string[] fengw = models.JieSuModel.CanShu.Split(':');
                            if (fengw.Length >= 2)
                            {
                                Model.InModel.BangMaModel bangMaModel = new Model.InModel.BangMaModel();
                                bangMaModel.assemblyName = fengw[0];
                                bangMaModel.assemblySn = fengw[1];
                                qianmodel.serialList[0].assemblyList.Add(bangMaModel);
                            }
                        }
                    }
                    if (shifouzai)
                    {
                        lock (Suo2)
                        {
                            if (CeShiData.ContainsKey(key))
                            {
                                CeShiData[key].Clear();
                                CeShiData.Remove(key);
                            }
                        }
                    }
                }                                  
                OutFoShanBaseModel<OutYiBanModel> jieguo = XingDeMesQingQiuLei.Cerate().QiuQiuHttp<InChuZhanModel, OutYiBanModel>(qianmodel, gWModel.GWID, gWModel.ChuZhanWangZhi);
                LianWanModel jieguomodel = new LianWanModel();
                if (jieguo != null)
                {
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    jieguomodel.NeiRong =$"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】{jieguo.Msg}" ;
                    if (jieguo.ChengGong)
                    {
                        if (jieguo.ShuJu.errorcode == 0)
                        {
                            jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                        }
                    }

                }
                else
                {
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                    jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】通讯不良，导致没有反馈";
                }
            
                return jieguomodel;
            }
            else
            {
                LianWanModel jieguomodel = new LianWanModel();
                jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                jieguomodel.NeiRong = $"【{models.TDName}】没有找到相应的配置";
                return jieguomodel;
            }
        }

      


        public override LianWanModel ZhongJianBangMa(ShangChuanDataModel models)
        {
            LianWanModel jieguomodel = new LianWanModel();
            GWModel gWModel = GetTDModel(models.TDID);
            if (gWModel != null)
            {
               
                if (models.BangMaModel.IsDanMa)
                {
                    jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                    jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】单一码不用这个接口";
                    return jieguomodel;
                }
                else
                {
                    InBangMaModel qianmodel = new InBangMaModel();
                    qianmodel.lotCode = gWModel.lotCode;
                    qianmodel.stationName = gWModel.stationName;
                    qianmodel.stepName = gWModel.stepName;
                    qianmodel.userCode = gWModel.userCode;
                    for (int i = 0; i < models.BangMaModel.MaS.Count; i++)
                    {
                        string[] fengw = models.BangMaModel.MaS[i].Split(':');
                        if (fengw.Length > 1)
                        {
                            ChuZhanDataModel jinzhanmodel = new ChuZhanDataModel();
                            jinzhanmodel.serialNumber = fengw[0];
                            jinzhanmodel.qty = 1;
                            jinzhanmodel.panelIndex = i + 1;

                            TestDataModel testmodel = new TestDataModel();
                            testmodel.attrType = " String";
                            testmodel.attrUnit = "NA";
                            testmodel.key = "扫码";
                            testmodel.max = "/";
                            testmodel.min = "/";
                            testmodel.modelvalue = "/";
                            testmodel.value = fengw[1];
                            jinzhanmodel.testDataList.Add(testmodel);
                            qianmodel.serialList.Add(jinzhanmodel);
                        }
                    }

                    OutFoShanBaseModel<OutYiBanModel> jieguo = XingDeMesQingQiuLei.Cerate().QiuQiuHttp<InBangMaModel, OutYiBanModel>(qianmodel, gWModel.GWID, gWModel.ChuZhanWangZhi);
                    if (jieguo != null)
                    {
                        jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                        jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】{jieguo.Msg}";
                        if (jieguo.ChengGong)
                        {
                            if (jieguo.ShuJu.errorcode == 0)
                            {
                                jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.Pass;
                            }
                        }

                    }
                    else
                    {
                        jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                        jieguomodel.NeiRong = $"【{gWModel.stationName},{gWModel.stepName} {models.TDName}】通讯不良，导致没有反馈";
                    }
                }
              
            }
            else
            {
                jieguomodel.FanHuiJieGuo = JinZhanJieGuoType.NG;
                jieguomodel.NeiRong = $"【{models.TDName}】没有找到相应的配置";
            }

            return jieguomodel;

        }

        public override void GetPeiZhiForm()
        {
            TongDaoPeiZhiFrm peiZhiFrm = new TongDaoPeiZhiFrm();
          
            peiZhiFrm.TopMost = true;
            peiZhiFrm.BringToFront();
            peiZhiFrm.Show();

        }

        private GWModel GetTDModel(int gwid)
        {
            for (int i = 0; i < CanShuModel.Count; i++)
            {
                if (CanShuModel[i].GWID== gwid)
                { 
                    return CanShuModel[i];
                }
            }
            return null;
        }

        public override void Clear(ShangChuanDataModel models)
        {
          
            lock (Suo1)
            {
                string key = $"{models.TDID}:{models.GuoChengMa}";
                if (CeShiData.ContainsKey(key))
                {
                    CeShiData[key].Clear();
                    CeShiData.Remove(key);
                }
            }
        }

        private List<TestDataModel> GetChuZhanData(List<YeWuDataModel> shujuyuan,string bianhao,string shebeiname,out List<BuLiangModel> buliangmodels)
        {
            buliangmodels = new List<BuLiangModel>();
            List<TestDataModel> lisjieguo = new List<TestDataModel>();
           
            for (int i = 0; i < shujuyuan.Count; i++)
            {
                if (shujuyuan[i].IsShuJuHeGe==false)
                {
                    BuLiangModel buliangmodel = new BuLiangModel();
                    buliangmodel.defectType = "自动化测试";
                    buliangmodel.defectLocation = shebeiname;
                    buliangmodel.defectLabel = shebeiname;
                    buliangmodel.attrType = shujuyuan[i].CodeOrNo;
                    buliangmodel.attrUnit = shujuyuan[i].DanWei;
                    buliangmodel.key = shujuyuan[i].ItemName;
                    buliangmodel.max = ChangYong.TryStr(shujuyuan[i].Up.JiCunValue, "");
                    buliangmodel.min = ChangYong.TryStr(shujuyuan[i].Low.JiCunValue, "");
                    buliangmodel.modelvalue = "/";
                    buliangmodel.value = ChangYong.TryStr(shujuyuan[i].Value.JiCunValue, "");
                    buliangmodel.Result = shujuyuan[i].IsShuJuHeGe ? "PASS" : "NG";
                    buliangmodels.Add(buliangmodel);
                }
              
                TestDataModel testmodel = new TestDataModel();
                testmodel.attrType = shujuyuan[i].CodeOrNo;
                testmodel.attrUnit = shujuyuan[i].DanWei;
                testmodel.key = shujuyuan[i].ItemName;
                testmodel.max =ChangYong.TryStr( shujuyuan[i].Up.JiCunValue,"");
                testmodel.min = ChangYong.TryStr(shujuyuan[i].Low.JiCunValue, "");
                testmodel.modelvalue = "/";
                testmodel.value= ChangYong.TryStr(shujuyuan[i].Value.JiCunValue, "");
                testmodel.Result = shujuyuan[i].IsShuJuHeGe ? "PASS" : "NG";
                lisjieguo.Add(testmodel);
            }
           
            return lisjieguo;
        }
    }
}
