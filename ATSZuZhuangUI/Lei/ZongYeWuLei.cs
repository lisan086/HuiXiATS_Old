using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATSJianMianJK.Log;
using ATSJianMianJK.Mes;
using ATSZuZhuangUI.Lei.GongNengLei;
using CommLei.JiChuLei;
using ZuZhuangUI.Lei.GongNengLei;
using ZuZhuangUI.Lei.GongNengLei.ShiXian;
using ZuZhuangUI.Model;

namespace ZuZhuangUI.Lei
{
    public  class ZongYeWuLei
    {
        private List<int> KeyInt = new List<int>();
        private  Dictionary<int, ABSGongNengLei> GongZhanLeilist = new Dictionary<int, ABSGongNengLei>();     
        #region 单例
        private static ZongYeWuLei _LogTxt = null;
        private readonly static object _DuiXiang = new object();

    

        private ZongYeWuLei()
        {
           
        }
        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static ZongYeWuLei Ceratei()
        {
            if (_LogTxt == null)
            {
                lock (_DuiXiang)//线程锁
                {
                    if (_LogTxt == null)
                    {
                        _LogTxt = new ZongYeWuLei();
                    }
                }
            }
            return _LogTxt;
        }
        #endregion

        //初始化
        public  void IniData()
        {

            //站点
            GongZhanLeilist.Clear();
            
            List<SheBeiZhanModel> zhan = DataJiHe.Cerate().LisSheBeiBianHao;//获取所有站点编号列表         
            for (int i = 0; i < zhan.Count; i++)
            {
                if (zhan[i].IsZhengZhanDian == SheBeiType.GongWeiZhan)
                {
                    ABSGongNengLei aBSGongNengLei = new GongWeiLei();
                    aBSGongNengLei.SheBeiID = zhan[i].GWID;
                    aBSGongNengLei.SheBeiName = zhan[i].LineCode;
                    aBSGongNengLei.BianHao = zhan[i].XingHao;
                    aBSGongNengLei.SheBeiType = zhan[i].IsZhengZhanDian;
                    if (GongZhanLeilist.ContainsKey(zhan[i].GWID) == false)
                    {
                        aBSGongNengLei.IniData(zhan[i]);
                        GongZhanLeilist.Add(zhan[i].GWID, aBSGongNengLei);
                        ShangChuanMesLei.Cerate().ShuXinTdState(zhan[i].GWID, zhan[i].IsMes);
                        if (string.IsNullOrEmpty(zhan[i].MaName) == false)
                        {
                            MaGuanLi.CerateDanLi().SetDanQian(zhan[i].GWID, zhan[i].MaName);
                        }
                    }
                }
            
            }

            KeyInt = GongZhanLeilist.Keys.ToList();
        }


        public void Open()
        {
            foreach (var item in KeyInt)
            {
                GongZhanLeilist[item].Open();
            }
        }
        /// <summary>
        /// 关闭，清理内存
        /// </summary>
        public void Close()
        {
            foreach (var item in KeyInt)
            {
                GongZhanLeilist[item].Close();
            }        
            Thread.Sleep(150);        
        }


        public List<IFKJ> GetLisKJ()
        {
            List<IFKJ> KJS = new List<IFKJ>();
            for (int i = 0; i < KeyInt.Count; i++)
            {
                IFKJ fKJ = GongZhanLeilist[KeyInt[i]].GetFKJ();
                if (fKJ != null)
                {
                    KJS.Add(fKJ);
                }
            }
            return KJS;
        }
     
    }
}
