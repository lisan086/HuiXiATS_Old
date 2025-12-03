using BaseUI.UC;
using BaseUI.UI;
using CommLei.JieMianLei;
using JieMianLei.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.UI
{
    public partial class BaseFrom : Form
    {
      
        protected Controlinkove Controlinkove;
        public BaseFrom()
        {
            InitializeComponent();
            Controlinkove = new Controlinkove(this);
        }

        /// <summary>
        /// 启动默认提示框，默认是5秒
        /// </summary>
        /// <param name="msg"></param>    
        /// <param name="shijian"></param>
        /// <returns></returns>
        protected void QiDongTiShiKuang(string msg,int shijian=5)
        {
            MsgBoxFrom chuanti = new MsgBoxFrom();
            chuanti.AddMsg(msg);
            chuanti.SetCanShu(true, "确定", "", shijian);
            chuanti.TopMost = true;
            chuanti.BringToFront();
            chuanti.ShowDialog();
        }

        /// <summary>
        /// 启动默认提示框确定与取消
        /// </summary>
        /// <param name="msg"></param>  
        /// <returns></returns>
        protected bool QueDingOrQuXiao(string msg)
        {
            MsgBoxFrom chuanti = new MsgBoxFrom();
            chuanti.AddMsg(msg);
            chuanti.SetCanShu(false, "确定", "取消", 2);
            chuanti.TopMost = true;
            chuanti.BringToFront();
            bool jieguo = false;
            if (chuanti.ShowDialog() == DialogResult.OK)
            {
                jieguo = chuanti.JieGuo ? false : true;
            }
            return jieguo;
        }
        /// <summary>
        /// 启动默认提示框是与否
        /// </summary>
        /// <param name="msg"></param>    
        /// <returns></returns>
        protected bool ShiOrFou(string msg)
        {
            MsgBoxFrom chuanti = new MsgBoxFrom();
            chuanti.AddMsg(msg);
            chuanti.SetCanShu(false, "是", "否", 2);
            chuanti.TopMost = true;
            chuanti.BringToFront();
            bool jieguo = false;
            if (chuanti.ShowDialog() == DialogResult.OK)
            {
                jieguo = chuanti.JieGuo?false:true;
            }
            return jieguo;
        }

        protected string JianPan(JianPanType jianpantype,string jianpanzhi,bool isjiama)
        {
            string fanhui="";
            switch (jianpantype)
            {
                case JianPanType.ShuZhi:
                    {
                        ShuZiJianPanFrom frm = new ShuZiJianPanFrom();
                        frm.SetCanShu(jianpanzhi);
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            fanhui = frm.ShuZhi;
                        }
                    }
                    break;
                case JianPanType.PingYing:
                    {
                        QuanBuJianPanFrom frm = new QuanBuJianPanFrom();
                        frm.SetCanShu(jianpanzhi, isjiama);
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            fanhui = frm.GetstrNeiRong;
                        }
                    }
                    break;
                case JianPanType.ShouXie:
                    {
                        ShouXieJianPanFrom frm = new ShouXieJianPanFrom();
                        frm.SetCanShu(jianpanzhi, isjiama);
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            fanhui = frm.FullCACText;
                        }
                    }
                    break;
                default:
                    break;
            }

            return fanhui;
        }

        /// <summary>
        /// 权限有关的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LisTQuanXian"></param>
        /// <param name="quanxiannane"></param>
        /// <param name="guolv"></param>
        /// <returns></returns>
        protected bool IsYouQuanXian<T>(List<T> LisTQuanXian, string quanxiannane, Func<List<T>, string, bool> guolv) where T : class
        {
            if (LisTQuanXian.Count == 0)
            {
                QiDongTiShiKuang(string.Format("您没有{0}权限", quanxiannane));
                return false;
            }
            if (guolv == null)
            {
                QiDongTiShiKuang(string.Format("您没有{0}权限", quanxiannane));
                return false;
            }
            bool you = false;
            if (guolv(LisTQuanXian, quanxiannane))
            {
                you = true;
            }
            if (you == false)
            {
                QiDongTiShiKuang(string.Format("您没有{0}权限", quanxiannane));
            }
            return you;
        }

        /// <summary>
        /// 延时加载方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="shijian"></param>
        protected void YanShiJiaZai(Action action, int shijian = 100)
        {
            Task task = Task.Factory.StartNew(() => {
                Thread.Sleep(shijian);
                if (action != null)
                {
                    action();
                }
            });
        }


        /// <summary>
        /// 显示非模式的等待窗体界面（对窗体数据的更新等操作可在等待委托内处理）停留时间是以毫秒计算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="canshu"></param>
        /// <param name="xiaoxi"></param>
        /// <param name="chuangti"></param>
        /// <param name="tingliutime"></param>
        public void Waiting(Action method, string xiaoxi, Control chuangti, int tingliutime = 1000 * 60*5)
        {
            MaXingTiaoXinFrom jindutiaofrom = new MaXingTiaoXinFrom();
            try
            {
                        
                jindutiaofrom.ShowWaitForm(chuangti, xiaoxi, false, method, tingliutime);
            }
            catch
            {

                jindutiaofrom.Close();
            }

        }

        public void BaoJingYouXiaJiao(ITiShiKJ kJ, Func<object> canshu, Form chuantu)
        {

            YouXiaJiaoKJ kj = new YouXiaJiaoKJ();
            kj.JiaZaiKongJian(kJ, canshu, chuantu);

        }
        public void DianJiBaoTiShi(Form chuantu)
        {
            for (int i = 0; i < chuantu.Controls.Count; i++)
            {
                if (chuantu.Controls[i] is YouXiaJiaoKJ)
                {
                    YouXiaJiaoKJ kj = chuantu.Controls[i] as YouXiaJiaoKJ;
                    kj.DianJiQiDong();
                   
                }
            }
        }
    }
}
