using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ATSFoZhaoZuZhuangUI.Lei;
using ATSFoZhaoZuZhuangUI.Model;
using ATSJianMianJK.Log;
using ATSJianMianJK.Mes;
using ATSZuZhuangUI.Lei.GongNengLei;
using CommLei.GongYeJieHe;
using CommLei.JiChuLei;
using ZuZhuangUI.Model;

namespace ZuZhuangUI.Lei.GongNengLei
{
    public abstract  class ABSGongNengLei
    {
        #region 属性
        /// <summary>
        /// 设备的id 唯一
        /// </summary>
        public int SheBeiID { get; set; } = -1;

        /// <summary>
        /// 设备的名称
        /// </summary>
        public string SheBeiName { get; set; } = "";

        /// <summary>
        /// 设备的编号
        /// </summary>
        public string BianHao { get; set; } = "";

        /// <summary>
        /// 设备类型
        /// </summary>
        public SheBeiType SheBeiType { get; set; } = SheBeiType.GongWeiZhan;
        #endregion
        /// <summary>
        /// 保存CSV文件用的
        /// </summary>
        protected List<CSVDataModel> LisData = new List<CSVDataModel>();

        #region 重写
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="model"></param>
        public abstract void IniData(SheBeiZhanModel model);

        /// <summary>
        /// 打开
        /// </summary>
        public abstract void Open();
        /// <summary>
        /// 关闭软件
        /// </summary>
        public abstract void Close();


        public abstract IFKJ GetFKJ();

        #endregion

        #region 半保护方法用于实现类的
        /// <summary>
        /// 写日记与界面记录
        /// </summary>
        /// <param name="riji"></param>
        /// <param name="msg"></param>
        protected void WriteLog(RiJiEnum riji, string msg)
        {
            RiJiLog.Cerate().Add(riji, $"{SheBeiName}:{msg}", SheBeiID);
        }

        /// <summary>
        /// 进站mes
        /// </summary>
        /// <returns></returns>
        protected LianWanModel JinZhanMes(string ketima, string qitama, bool isshouzan)
        {

            ShangChuanDataModel mesmodel = new ShangChuanDataModel();
            mesmodel.GuoChengMa = ketima;
            mesmodel.KaiShiModel.IsShouZhan = isshouzan;
            mesmodel.KaiShiModel.QiTaZhi = qitama;
            mesmodel.ShangChuanType = ShangChuanType.KaiShi;
            mesmodel.TDID = SheBeiID;
            mesmodel.TDName = SheBeiName;
            LianWanModel jieguo = ShangChuanMesLei.Cerate().ShangMes(mesmodel);
            return jieguo;
        }

        /// <summary>
        /// 用于绑码
        /// </summary>
        /// <param name="ketima"></param>
        /// <param name="mas"></param>
        /// <param name="isdanyi"></param>
        /// <returns></returns>
        protected LianWanModel BangMaJiaoYanMes(string ketima, List<string> mas, bool isdanyi)
        {
            ShangChuanDataModel mesmodel = new ShangChuanDataModel();
            mesmodel.GuoChengMa = ketima;
            mesmodel.TDID = SheBeiID;
            mesmodel.TDName = SheBeiName;
            mesmodel.BangMaModel.MaS.AddRange(mas);
            mesmodel.BangMaModel.IsDanMa = isdanyi;
            mesmodel.ShangChuanType = ShangChuanType.BangMa;
            LianWanModel jieguo = ShangChuanMesLei.Cerate().ShangMes(mesmodel);
            return jieguo;
        }

        /// <summary>
        /// 获取mes信息
        /// </summary>
        /// <param name="ketima"></param>
        /// <param name="canshu"></param>
        /// <returns></returns>
        protected LianWanModel GetMesXinXi(string ketima, string canshu)
        {
            ShangChuanDataModel mesmodel = new ShangChuanDataModel();
            mesmodel.GuoChengMa = ketima;
            mesmodel.TDID = SheBeiID;
            mesmodel.TDName = SheBeiName;
            mesmodel.HuoQuXinXiModel.CanShu = canshu;
            mesmodel.ShangChuanType = ShangChuanType.HuoQuXinXi;
            LianWanModel jieguo = ShangChuanMesLei.Cerate().ShangMes(mesmodel);
           
            return jieguo;
        }

        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="yeWuData"></param>
        /// <param name="ketima"></param>
        /// <returns></returns>
        protected LianWanModel BuZhouShuJuMes(YeWuDataModel yeWuData, string ketima)
        {

            ShangChuanDataModel mesmodel = new ShangChuanDataModel();
            mesmodel.GuoChengMa = ketima;
            mesmodel.TDID = SheBeiID;
            mesmodel.TDName = SheBeiName;
            mesmodel.BuZhouModel.BuZhouShuJu = yeWuData;
            mesmodel.BuZhouModel.JieGuo = yeWuData.IsShuJuHeGe;
            mesmodel.ShangChuanType = ShangChuanType.BuZhouShangChuan;
            if (yeWuData.CSVBaoCun == 1)
            {
                CSVDataModel scmodel = new CSVDataModel();
                scmodel.ItemName = yeWuData.ItemName;
                scmodel.JieGuo = yeWuData.IsShuJuHeGe ? "PASS" : "NG";
                scmodel.Max = ChangYong.TryStr(yeWuData.Up.JiCunValue, "");
                scmodel.Min = ChangYong.TryStr(yeWuData.Low.JiCunValue, "");
                scmodel.Zhi = ChangYong.TryStr(yeWuData.Value.JiCunValue, "");
                scmodel.ShiJian = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                scmodel.SheBeiBianHao = BianHao;
                scmodel.SheBeiName = SheBeiName;
                scmodel.ErWeiMa = ketima;
                LisData.Add(scmodel);
            }
            LianWanModel jieguo = ShangChuanMesLei.Cerate().ShangMes(mesmodel);
            return jieguo;
        }

        /// <summary>
        /// 清理数据的
        /// </summary>
        /// <param name="ketima"></param>
        /// <returns></returns>
        protected bool ClearData(string ketima)
        {
            {
                ChuFaJieMianEvent(EventType.ClearData,new JieMianShiJianModel());
            }
            ShangChuanDataModel mesmodel = new ShangChuanDataModel();
            mesmodel.GuoChengMa = ketima;
            mesmodel.TDID = SheBeiID;
            mesmodel.TDName = SheBeiName;
            mesmodel.ShangChuanType = ShangChuanType.Clear;
            LianWanModel jieguo = ShangChuanMesLei.Cerate().ShangMes(mesmodel);
            if (jieguo.FanHuiJieGuo == JinZhanJieGuoType.Pass)
            {
                //WriteLog(RiJiEnum.MesData, $"{ketima}删除数据:{jieguo.NeiRong}");
                return true;
            }
            else
            {
                //WriteLog(RiJiEnum.MesData, $"{ketima}删除数据:{jieguo.NeiRong}");
                return false;
            }
        }

        /// <summary>
        /// 出站用的
        /// </summary>
        /// <param name="ketima"></param>
        /// <param name="zongjieguo"></param>
        /// <param name="canshu"></param>
        /// <returns></returns>
        protected LianWanModel ChuZhanMes(string ketima, bool zongjieguo, string canshu)
        {
            if (LisData.Count > 0)
            {
                List<CSVDataModel> shuju = new List<CSVDataModel>();
                shuju.AddRange(LisData.ToArray());
                CSVBaoCun.Cerate().SetCanShu(shuju, ketima, zongjieguo);
                LisData.Clear();
            }
            ShangChuanDataModel mesmodel = new ShangChuanDataModel();
            mesmodel.GuoChengMa = ketima;
            mesmodel.TDID = SheBeiID;
            mesmodel.TDName = SheBeiName;
            mesmodel.JieSuModel.IsHeGe = zongjieguo;
            mesmodel.JieSuModel.KaiShiTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mesmodel.JieSuModel.JieSuTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mesmodel.ShangChuanType = ShangChuanType.JieSu;
            mesmodel.JieSuModel.CanShu = canshu;
            LianWanModel jieguo = ShangChuanMesLei.Cerate().ShangMes(mesmodel);
            return jieguo;
        }

        /// <summary>
        /// 校验本地数据
        /// </summary>
        /// <param name="testmodel"></param>
        /// <returns></returns>
        protected virtual bool JiaoYanHeGe(YeWuDataModel testmodel)
        {
            if (testmodel.IsYiZhuangTaiWeiZhun == 1)
            {
                string peizhizhi = testmodel.ZhuanTaiPiPeiZhi;
                string xianzhizhi = ChangYong.TryStr(testmodel.State.JiCunValue, "");
                if (peizhizhi.Equals(xianzhizhi))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (testmodel.IsYiZhuangTaiWeiZhun == 2)
            {
                bool zhen1 = true;
                bool zhen2 = true;
                {
                    double shangxian = ChangYong.TryDouble(testmodel.Up.JiCunValue, 0);
                    double xiaxian = ChangYong.TryDouble(testmodel.Low.JiCunValue, 0);
                    double zhi = ChangYong.TryDouble(testmodel.Value.JiCunValue, -1);
                    if (zhi >= xiaxian && zhi <= shangxian)
                    {
                        zhen1 = true;
                    }
                    else
                    {
                        zhen1 = false;
                    }
                }
                {
                    if (ChangYong.TryStr(testmodel.State.JiCunValue, "") == "1")
                    {
                        zhen2 = true;
                    }
                    else
                    {
                        zhen2 = false;
                    }
                }
                if (zhen1 && zhen2)
                {
                    return true;
                }
                return false;
            }
            else if (testmodel.IsYiZhuangTaiWeiZhun == 3)
            {
                double shangxian = ChangYong.TryDouble(testmodel.Up.JiCunValue, 0);
                double xiaxian = ChangYong.TryDouble(testmodel.Low.JiCunValue, 0);
                double zhi = ChangYong.TryDouble(testmodel.Value.JiCunValue, -1);
                if (zhi >= xiaxian && zhi <= shangxian)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 写入数据   1-清零值 2-是合格值 3-不合格值  0是需要参数
        /// </summary>
        /// <param name="caoZuoType"></param>
        /// <param name="zhi"></param>
        /// <param name="leixing"></param>
        protected void XieShuJu(CaoZuoType caoZuoType, object zhi, int leixing, bool isjiaoyan = true)
        {
            SheBeiJiHe.Cerate().XieYiBanZhi(caoZuoType, zhi, leixing, SheBeiID, isjiaoyan);
        }
        #endregion

        #region MyRegion
        protected void ChuFaJieMianEvent(EventType eventType, JieMianShiJianModel model)
        {
            model.GWID = SheBeiID;
            JieMianCaoZuoLei.CerateDanLi().ChuFaJieMianEvent(eventType, model);
        }


        #endregion

    }
}
