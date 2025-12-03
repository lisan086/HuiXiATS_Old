using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseUI.FuZhuLei;
using JieMianLei.FuZhuLei;
using JieMianLei.UC;
using JieMianLei.UI;

namespace JieMianLei.FuFrom
{
    public partial class BaseFuFrom : BaseFrom
    {

   

        private FromSuoFang FromSuoFang;
        public BaseFuFrom()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            MoveShiJianEvent moveShiJianEvent = new MoveShiJianEvent();
            moveShiJianEvent.BangDingMove(this.picFico, this);
            moveShiJianEvent.BangDingMove(this.labFbiaoTi, this);
            moveShiJianEvent.BangDingMove(this.ucFpanl1, this);//Fpancaozuo
            moveShiJianEvent.BangDingMove(this.Fpancaozuo, this);//Fpancaozuo
            FromSuoFang = new FromSuoFang();
        }

      
        #region 属性
        /// <summary>
        /// 禁用最大化，最大化还在
        /// </summary>
        public bool IsJingYongZuiDa
        {
            get { return this.ucFzuida.Enabled; }
            set
            {
                this.ucFzuida.Enabled = value;
            }
        }


        /// <summary>
        /// 禁用最小化，最小化还在
        /// </summary>
        public bool IsJingYongZuiXiao
        {
            get { return this.ucFzuixiao.Enabled; }
            set
            {
                this.ucFzuixiao.Enabled = value;
            }
        }
        private bool isZhiXianShiX = false;
        /// <summary>
        /// 只显示X按钮
        /// </summary>
        public bool IsZhiXianShiX
        {
            get
            {
                return isZhiXianShiX;
            }
            set
            {
                isZhiXianShiX = value;
                if (isZhiXianShiX)
                {
                    this.ucFzuida.Visible = false;
                    this.ucFzuixiao.Visible = false;
                }
                else
                {
                    this.ucFzuida.Visible = true;
                    this.ucFzuixiao.Visible = true;
                }
            }
        }

        #endregion

        #region 重写的方法
        /// <summary>
        /// 关闭的方法
        /// </summary>
        protected virtual void GuanBi()
        {
            
            this.Close();
        }
     
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void  ChuShiHua()
        {

        }
        #endregion
        #region 共有方法
        public void QuXiaoBiaoTi()
        {
            this.ucFpanl1.Visible = false;
        }
       
        public void GaiBiaSanGeYanSe(Color ZiTiSe)
        {
            this.ucFguanbi.ZColor = ZiTiSe;
            this.ucFzuida.ZColor = ZiTiSe;
            this.ucFzuixiao.ZColor = ZiTiSe;
        }
        public bool ShuRuMiMa(string mima, JianPanType jianPan)
        {
            MiMaFrom from = new MiMaFrom();
            from.SetCanShu(mima, jianPan);
            if (from.ShowDialog(this) == DialogResult.OK)
            {
                return true;
            }
            return false;
        }
        public int ShuRuMiMa(Dictionary<int,string> mima, JianPanType jianPan)
        {
            int shuleix = -1;
            MiMaFrom from = new MiMaFrom();
            from.SetCanShu(mima, jianPan);
            if (from.ShowDialog(this) == DialogResult.OK)
            {
                shuleix = from.FanHuiIndex;
            }
            return shuleix;
        }
        public void GuanDiao()
        {
            GuanBi();
        }
        public void ChuShiJiaZai()
        {
            ChuShiHua();
        }
        #endregion
        #region 继承的方法
        protected void SendJingZhiCH(bool IsJingZhiCH, IntPtr hwnd)
        {
          
        }
        protected void ZengJiaSuoFang(Control kj,bool issuofangwenzi)
        {
            FromSuoFang.Add(kj, issuofangwenzi);
        }
        protected void YiChuSuoFang(Control kj)
        {
            FromSuoFang.YiChu(kj);
        }
        protected void SuoFangShouDongShuaXin()
        {
            FromSuoFang.GuaQi();         
            FromSuoFang.GengGaiGao();
            FromSuoFang.ShuiXing();
        }
        /// <summary>
        /// 最小化
        /// </summary>
        public void ZuiXiaoHuan()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// 正常模式
        /// </summary>
        protected void ZhengChangHua()
        {
         
         
            FromSuoFang.GuaQi();
        
            this.WindowState = FormWindowState.Normal;
            FromSuoFang.GengGaiGao();
           
       
         
            FromSuoFang.ShuiXing();
         
         
        }
        /// <summary>
        /// 最大化
        /// </summary>
        protected void ZuiDaHua()
        {

        
        
            FromSuoFang.GuaQi();
        

            this.WindowState = FormWindowState.Maximized;
            FromSuoFang.GengGaiGao();
          
       
            FromSuoFang.ShuiXing();
        
       
        }
        protected void QuXiaoX()
        {
            this.ucFguanbi.Visible = false;
        }
        #endregion

        private void ucFzuixiao_Click(object sender, EventArgs e)
        {
            ZuiXiaoHuan();
        }

        private void ucFzuida_Click(object sender, EventArgs e)
        {
           
            if (this.WindowState == FormWindowState.Maximized)
            {
                ZhengChangHua();
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                ZuiDaHua();
            }
       
        }

        private void ucFguanbi_Click(object sender, EventArgs e)
        {
            GuanBi();
        }
      
    }
}
