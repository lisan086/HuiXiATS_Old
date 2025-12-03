using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATSFoZhaoZuZhuangUI.PeiZhi.Frm;
using ATSFoZhaoZuZhuangUI.UI;
using ATSJianMianJK;
using ATSJianMianJK.Log;
using ATSJuanChengZuZhuangUI.PeiZhi.Frm;
using ATSZuZhuangUI.Lei.GongNengLei;
using BaseUI.DaYIngMoBan.Frm;
using CommLei.JiChuLei;
using SSheBei.PeiZhi;
using ZuZhuangUI.Lei;
using ZuZhuangUI.Model;
using ZuZhuangUI.PeiZhi.Frm;

namespace ZuZhuangUI.UI
{
    public partial class ZhuJieMian : UserControl
    {
        List<IFKJ> liskj = new List<IFKJ>();
      
        private ZiYuanModel ZiYuanModel;
        public ZhuJieMian(ZiYuanModel ziYuanModel)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            ZiYuanModel = ziYuanModel;
        }

        public void Close()
        {
            JieMianCaoZuoLei.CerateDanLi().JieMianCuoZuo(DoType.Close, new  JieMianCaoZuoModel());

            JieMianCaoZuoLei.CerateDanLi().JieMianEvent -= ZhuKJ_JieMianEvent;
        }

        public void SetLog(List<RiJiModel> lismodel)
        {
            for (int i = 0; i < lismodel.Count; i++)
            {
                if (lismodel[i].IsTanChuang)
                {
                    Task.Factory.StartNew((x) => {
                        ZiYuanModel.TiShiKuang(x.ToString());
                    }, lismodel[i].Msg);
                  
                }
                foreach (var item in liskj)
                {
                    if (lismodel[i].IsTongDao)
                    {
                        item.SetLog(lismodel[i].TDID, lismodel[i]);
                    }
                }

            }
        }
        public void QieHuanYongHu()
        {
            foreach (var item in liskj)
            {
                item.SetCanShu(ZiYuanModel);
            }
            QuanXian();
        }

        private void QuanXian()
        {
          
           
        }

        private void ZhuJieMian_Load(object sender, EventArgs e)
        {
            JieMianCaoZuoLei.CerateDanLi().JieMianCuoZuo(DoType.IniData, new JieMianCaoZuoModel());
            QuanXian();
            IniJieMian();
            JieMianCaoZuoLei.CerateDanLi().JieMianEvent += ZhuKJ_JieMianEvent;       
            JieMianCaoZuoLei.CerateDanLi().JieMianCuoZuo(DoType.Open, new JieMianCaoZuoModel());           
        }

        private void IniJieMian()
        {
            if (liskj.Count>0)
            {
                try
                {
                    for (int i = 0; i < liskj.Count; i++)
                    {
                        liskj[i].Close();
                    }
                }
                catch
                {

                  
                }
              
            }
            List<IFKJ> lis = JieMianCaoZuoLei.CerateDanLi().ZongYeWuLei.GetLisKJ();
            int count = lis.Count;
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
          
            if (count > 0)
            {

                List<Control> kjs = new List<Control>();
                foreach (var item in lis)
                {
                    Control kj = item.GetKJ();
                    kj.Dock = DockStyle.Fill;
                    item.SetCanShu(ZiYuanModel);
                    liskj.Add(item);
                    kjs.Add(kj);
                }
                count = kjs.Count;
                float bili = (1f / count) * 100f;
                for (int i = 0; i < count; i++)
                {
                    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, bili));

                }
                if (count > 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    }
                }
                else
                {
                    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                }

                for (int i = 0; i < kjs.Count; i++)
                {
                    this.tableLayoutPanel1.Controls.Add(kjs[i]);
                }
            }
        }

        private void ZhuKJ_JieMianEvent(EventType arg1, JieMianShiJianModel arg2)
        {

            ZiYuanModel.It.FanXingGaiBing(() => {
                if (arg1 == EventType.QieHuanPeiFang)
                {
                    IniJieMian();
                }
                else
                {
                    foreach (var item in liskj)
                    {
                        item.SetCanShu(arg1, arg2);
                    }
                }
               
            });
          
        }

     

   
        public void ShuaXin()
        {

        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            TDShuJuFrm tDShuJuFrm = new TDShuJuFrm();
            tDShuJuFrm.Show(this);
        }
    }
}
