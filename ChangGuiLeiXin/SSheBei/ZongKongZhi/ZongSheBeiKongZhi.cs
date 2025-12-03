using SSheBei.ABSSheBei;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SSheBei.Model;
using CommLei.DataChuLi;
using Common.DataChuLi;
using CommLei.JiChuLei;
using System.Windows.Forms;
using SSheBei.PeiZhi;

namespace SSheBei.ZongKongZhi
{
    /// <summary>
    /// 总设备控制类
    /// </summary>
    public class ZongSheBeiKongZhi
    {
        /// <summary>
        /// true  表示开启
        /// </summary>
        private bool KaiGuan = true;
        /// <summary>
        /// 总线集合
        /// </summary>
        private Dictionary<int, ABSNSheBei> ZongSheBeis = new Dictionary<int, ABSNSheBei>();
       
        
        /// <summary>
        /// 总线里的消息
        /// </summary>
        public event ZongXianMsg ZongXianMsgEvnt;

        #region 单例
        private static ZongSheBeiKongZhi _LogTxt = null;

        private readonly static object _DuiXiang = new object();
        private ZongSheBeiKongZhi()
        {

        }

     

        /// <summary>
        /// 单例类，
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static ZongSheBeiKongZhi Cerate()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new ZongSheBeiKongZhi();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion

        #region 公开的
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void IniChuShiHua()
        {
            string wenjian = new PathModel().LuJin;
            Debug.WriteLine("加载设备文件夹:"+ wenjian);
            List<JiaZaiSheBeiModel> xuyaowenjian = HCLisDataLei<JiaZaiSheBeiModel>.Ceratei().LisWuLiao;
            List<string> wenjians = new List<string>();
            for (int i = 0; i < xuyaowenjian.Count; i++)
            {
                if (xuyaowenjian[i].IsShiYong)
                {
                    wenjians.Add(xuyaowenjian[i].JiaZaiWanJianName);
                }
            }
            JieKouJiaZaiLei<ABSNSheBei> wenss = new JieKouJiaZaiLei<ABSNSheBei>();
            //Dictionary<string,List<Type>> shebeis = wenss.JiaZaiXuYaoType(wenjian, wenjians);
            Dictionary<string, List<Type>> shebeis = wenss.LoadNeededTypes<ABSNSheBei>(wenjian, wenjians);

            if (shebeis.Count == 0)
            {
                ChuFaMsg(MsgDengJi.SheBeiTangChuang, new MsgModel() { Msg = "没有实现的设备或者是没有配置设备" });
            }

            for (int i = 0; i < xuyaowenjian.Count; i++)
            {
                if (xuyaowenjian[i].IsShiYong == false)
                {
                    ChuFaMsg(MsgDengJi.SheBeiZhengChang, new MsgModel() { Msg = "设备已经禁用" });
                    continue;
                }
                if (shebeis.ContainsKey(xuyaowenjian[i].JiaZaiWanJianName))
                {
                    List<Type> types = shebeis[xuyaowenjian[i].JiaZaiWanJianName];
                    int  zhens = 1;
                    foreach (var item in types)
                    {
                        ABSNSheBei tongjijiekou = (ABSNSheBei)Activator.CreateInstance(item);
                        if (xuyaowenjian[i].SheBeiType == tongjijiekou.SheBeiType)
                        {
                            if (ZongSheBeis.ContainsKey(xuyaowenjian[i].SheBeiID) == false)
                            {
                                try
                                {
                                    ABSNSheBei sshebei = tongjijiekou;
                                    sshebei.SheBeiID = xuyaowenjian[i].SheBeiID;
                                    sshebei.SheBeiName = xuyaowenjian[i].SheBeiName;
                                    sshebei.FenZu = xuyaowenjian[i].SheBeiZu;
                                    sshebei.PeiZhiObjName = xuyaowenjian[i].SheBeiPeiZhi;
                                    sshebei.IniData(false);
                                    ZongSheBeis.Add(sshebei.SheBeiID, sshebei);
                                    ChuFaMsg(MsgDengJi.SheBeiZhengChang, new MsgModel() { Msg = string.Format("{0}:实例化设备", sshebei.SheBeiName), SheBeiID = sshebei.SheBeiID, SheBeiName = sshebei.SheBeiName });
                                    sshebei.SheBeiMsgEvnt += ChuFaMsg;
                                    zhens = 2;
                                }
                                catch (Exception ex)
                                {

                                    ChuFaMsg(MsgDengJi.SheBeiCuoWu, new MsgModel() { Msg = string.Format("{0}:实例化设备出现问题:{1}", xuyaowenjian[i].JiaZaiWanJianName,ex), SheBeiID =-1, SheBeiName = "" });
                                }
                               
                            }
                            else
                            {
                                zhens = 3;
                            }
                            break;
                        }
                    }
                    if (zhens!=2)
                    {
                        if (zhens == 1)
                        {
                            ChuFaMsg(MsgDengJi.SheBeiCuoWu, new MsgModel() { Msg = $"没有找到该文件对应的设备:{xuyaowenjian[i].JiaZaiWanJianName}" });
                        }
                        else if (zhens==3)
                        {
                            ChuFaMsg(MsgDengJi.SheBeiCuoWu, new MsgModel() { Msg = $"该文件对应的ID有重复:{xuyaowenjian[i].JiaZaiWanJianName}" });
                        }
                    }
                }
                else
                {
                    ChuFaMsg(MsgDengJi.SheBeiCuoWu, new MsgModel() { Msg = $"没有找到该文件:{xuyaowenjian[i].JiaZaiWanJianName}" });
                }
            }


     
        }

        /// <summary>
        /// 打开数据
        /// </summary>
        public void Open()
        {
            List<int> keys = ZongSheBeis.Keys.ToList();
            ParallelLoopResult jieguo = Parallel.ForEach(keys, (x) => {
                try
                {
                    ZongSheBeis[x].Open();
                }
                catch
                {

                   
                }

            });

        
            //for (int i = 0; i < keys.Count; i++)
            //{
            //    ZongSheBeis[keys[i]].Open();
            //}

         
        }

        /// <summary>
        /// 关闭数据
        /// </summary>
        public void Close()
        {
            KaiGuan = false;
            Thread.Sleep(100);
            List<int> keys = ZongSheBeis.Keys.ToList();

            ParallelLoopResult jieguo = Parallel.ForEach(keys, (x) => {
                try
                {
                    ZongSheBeis[x].Close();
                }
                catch
                {


                }

            });
           
          
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<DataModel> GetShuJu()
        {
            List<DataModel> shujuss = new List<DataModel>();
            foreach (var item in ZongSheBeis.Keys)
            {
                bool tx = ZongSheBeis[item].TongXin;
                List<JiCunQiModel> jicunqis = ZongSheBeis[item].GetShuJu();
                DataModel model = new DataModel();
                model.SheBeiID = item;
                model.IsKeKao = tx;
                model.JiCunQiModels = jicunqis;
                shujuss.Add(model);
            }
            return shujuss;
        }

        /// <summary>
        /// 获取每个通信状态 key表示设备组
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<TxModel>> GetSheBeiTxState()
        {
            Dictionary<string, List<TxModel>> lis = new Dictionary<string, List<TxModel>>();
            foreach (var item in ZongSheBeis.Keys)
            {
                TxModel tx = ZongSheBeis[item].GetMeiGeTx();
                if (lis.ContainsKey(ZongSheBeis[item].FenZu)==false)
                {
                    lis.Add(ZongSheBeis[item].FenZu,new List<TxModel>());
                }
                lis[ZongSheBeis[item].FenZu].Add(tx);
            }
            return lis;
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="canshus"></param>
        public void XieCanShu(List<XieRuMolde> canshus)
        {
            if (KaiGuan==false)
            {
                return;
            }
            Dictionary<int, List<JiCunQiModel>> fenkaijicunqi = new Dictionary<int, List<JiCunQiModel>>();
            for (int i = 0; i < canshus.Count; i++)
            {
                JiCunQiModel zongxianid = GetJiCunQi(canshus[i]);
                if (zongxianid != null)
                {
                    if (ZongSheBeis.ContainsKey(zongxianid.SheBeiID))
                    {
                        ZongSheBeis[zongxianid.SheBeiID].Clear(false, zongxianid);
                    }
                    zongxianid.Value = canshus[i].Zhi;
                    if (fenkaijicunqi.ContainsKey(zongxianid.SheBeiID) == false)
                    {
                        fenkaijicunqi.Add(zongxianid.SheBeiID, new List<JiCunQiModel>());
                    }
                    fenkaijicunqi[zongxianid.SheBeiID].Add(zongxianid);
                   
                }
            }
            foreach (var item in fenkaijicunqi.Keys)
            {
                if (ZongSheBeis.ContainsKey(item))
                {
                    ZongSheBeis[item].XieShuJu(fenkaijicunqi[item]);
                }
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="canshus"></param>
        public void XieCanShu(XieRuMolde canshus)
        {
            if (KaiGuan == false)
            {
                return;
            }
            JiCunQiModel zongxianid = GetJiCunQi(canshus);
            if (zongxianid != null)
            {
                zongxianid.Value = canshus.Zhi;
                if (ZongSheBeis.ContainsKey(zongxianid.SheBeiID))
                {
                    ZongSheBeis[zongxianid.SheBeiID].XieShuJu(new List<JiCunQiModel>() { zongxianid });
                }
                else
                {
                    MsgModel modes = new MsgModel();
                    modes.Msg = string.Format("没有找到对应的寄存器:{0}", canshus.JiCunQiWeiYiBiaoShi);               
                    ChuFaMsg(MsgDengJi.SheBeiCuoWu, modes);
                }
            }

        }

        /// <summary>
        /// 返回校验结果
        /// </summary>
        /// <param name="canshus"></param>
        /// <returns></returns>
        public JiaoYanJieGuoModel GetIsChengGong(XieRuMolde canshus)
        {
            JiCunQiModel zongxianid = GetJiCunQi(canshus);
            if (zongxianid != null)
            {
                zongxianid.Value = canshus.Zhi;
                if (ZongSheBeis.ContainsKey(zongxianid.SheBeiID))
                {
                    return ZongSheBeis[zongxianid.SheBeiID].JiaoYanChengGong(zongxianid);
                }
            }
            return new JiaoYanJieGuoModel() {WeiYiBiaoShi= canshus.JiCunQiWeiYiBiaoShi ,SheBeiID=canshus.SheBeiID, IsZuiZhongJieGuo = JieGuoType .MeiZhaoDaoJiGuo,Value="没有找到"};
        }
        /// <summary>
        /// 获取配置调试窗体
        /// </summary>
        /// <returns></returns>
        public JieMianFrmModel GetTiaoShiFrm(int shebeid)
        {
         
            foreach (var item in ZongSheBeis.Keys)
            {
                if (item == shebeid)
                {
                    return ZongSheBeis[item].GetFrm(true);
                   
                }
            }
            return null;
        }

        /// <summary>
        /// 获取设备参数的 有设备id，与名称
        /// </summary>
        /// <returns></returns>
        public List<JieMianFrmModel> GetSheBeiCanShu()
        {
            List<JieMianFrmModel> frms = new List<JieMianFrmModel>();
            foreach (var item in ZongSheBeis.Keys)
            {
                JieMianFrmModel model = new JieMianFrmModel();
                model.SheBeiID = item;
                model.FenZu = ZongSheBeis[item].FenZu;
                model.SheBeiName = ZongSheBeis[item].SheBeiName;
                frms.Add(model);
            }
            return frms;
        }
        /// <summary>
        /// 获取配置的读写 1是读 2是写 3是全部
        /// </summary>
        /// <returns></returns>
        public List<JiCunQiModel> GetPeiZhiLisJiCunQi(int type)
        {
            List<JiCunQiModel> lise = new List<JiCunQiModel>();
            foreach (var item in ZongSheBeis.Keys)
            {
                lise.AddRange(ZongSheBeis[item].PeiZhiDuXie(type));
                
            }
           
            return lise;
        }

        /// <summary>
        /// 获取配置的读写 1是读 2是写 3是全部
        /// </summary>
        /// <param name="shebid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<JiCunQiModel> GetPeiZhiJiCunQi(int shebid,int type)
        {
            List<JiCunQiModel> lise = new List<JiCunQiModel>();
            if (ZongSheBeis.ContainsKey(shebid))
            {
                lise.AddRange(ZongSheBeis[shebid].PeiZhiDuXie(type));
            }
           
            return lise;
        }

        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <param name="shebeiid"></param>
        /// <returns></returns>
        public string GetSheBeiName(int shebeiid)
        {
            if (ZongSheBeis.ContainsKey(shebeiid))
            {
                return ZongSheBeis[shebeiid].SheBeiName;
            }
            return "";
        }

        /// <summary>
        /// 获取设备
        /// </summary>
        /// <returns></returns>
        public ABSNSheBei GetSheBei(int shebeiid)
        {

            if (ZongSheBeis.ContainsKey(shebeiid))
            {
                return ZongSheBeis[shebeiid];
            }
            return null;
        }

        /// <summary>
        /// 获取配置控件
        /// </summary>
        /// <param name="shebeiid"></param>
        /// <param name="weiyibiaoshi"></param>
        /// <returns></returns>
        public KJPeiZhiJK GetPeiZhiJieKou(int shebeiid,string weiyibiaoshi)
        {

            if (ZongSheBeis.ContainsKey(shebeiid))
            {
                return ZongSheBeis[shebeiid].GetCanShuKJ(weiyibiaoshi);
            }
            return new MoRenKJ();
        }
        #endregion
        #region 私有的方法
        private void ChuFaMsg(MsgDengJi msgDengJi, MsgModel e)
        {
            if (ZongXianMsgEvnt != null)
            {
                ZongXianMsgEvnt(msgDengJi, e);
            }
        }
        private JiCunQiModel GetJiCunQi(XieRuMolde weiyibiaoshi)
        {
            foreach (var item in ZongSheBeis.Keys)
            {
                List<JiCunQiModel> jiCunQiModels = ZongSheBeis[item].PeiZhiDuXie(3);
                for (int i = 0; i < jiCunQiModels.Count; i++)
                {
                    if (jiCunQiModels[i].WeiYiBiaoShi.Equals(weiyibiaoshi.JiCunQiWeiYiBiaoShi)&& jiCunQiModels[i].SheBeiID==weiyibiaoshi.SheBeiID)
                    {
                        jiCunQiModels[i].SheBeiID = item;
                        return jiCunQiModels[i].FuZhi();
                    }
                }
             
            }
            return null;

        }
        #endregion
      
    }
}
