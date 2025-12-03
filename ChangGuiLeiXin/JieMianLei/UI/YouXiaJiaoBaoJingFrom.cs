using JieMianLei.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UI
{
    public partial class YouXiaJiaoBaoJingFrom : BaseFrom
    {
        private int BiaoZhi = 0;
        private DateTime DateTime = DateTime.Now;
        private ITiShiKJ KJ;    
        private Func< object> GetCanShu;
        private Form From1 = null;
        private int JuLuY = 0;
        private int YiJingZaiZuiShangMian = 3;
        private int JiLuYuanSiGao = 0;
        private int SuDu = 80;
        private int BuXuYaoTiXian = 1;
        public YouXiaJiaoBaoJingFrom()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
       
        public void JiaZaiKongJian(ITiShiKJ kJ, Func<object> canshu,Form chuantu)
        {
            KJ = kJ;
            Control ks = kJ.GetKJ();
            ks.Dock = DockStyle.Fill;
            this.buKaPanl1.Controls.Add(ks);
            GetCanShu = canshu;
            From1 = chuantu;
            JiLuYuanSiGao =486;
            this.Height = 0;
            DateTime = DateTime.Now;
            this.timer1.Enabled = true;
            this.Hide();
        }

        public void DianJiQiDong()
        {
            BiaoZhi = 3;
        }
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (BiaoZhi == 0)
            {
                if ((DateTime.Now - DateTime).TotalMilliseconds >= 1100)
                {
                    if (KJ != null && GetCanShu != null && From1 != null)
                    {
                        object zhen = GetCanShu();
                        int zhesn = KJ.SetCanShu(zhen);
                        if (BuXuYaoTiXian == 1)
                        {
                            if (zhesn == 1)
                            {
                                if (YiJingZaiZuiShangMian == 3)
                                {
                                    YiJingZaiZuiShangMian = 1;
                                    JuLuY = 0;
                                    Point p = From1.PointToScreen(new Point(0, 0));
                                    this.Location = new Point(p.X + From1.Width - this.Width, p.Y + From1.Height - 25);
                                    this.TopMost = true;
                                    this.BringToFront();
                                    this.Visible = true;

                                    BiaoZhi = 1;
                                }
                            }
                            else if (zhesn == 2)
                            {
                                if (YiJingZaiZuiShangMian == 2)
                                {
                                    YiJingZaiZuiShangMian = 1;
                                    BiaoZhi = 2;
                                }
                            }
                        }
                    }
                    DateTime = DateTime.Now;
                }
            }
            else if (BiaoZhi == 1)
            {
                JuLuY += SuDu;
                this.Location = new Point(this.Location.X, this.Location.Y - SuDu);
                this.Height += SuDu;
                if (JuLuY >= JiLuYuanSiGao)
                {
                    YiJingZaiZuiShangMian = 2;
                    BiaoZhi = 0;
                    JuLuY = JiLuYuanSiGao;
                }
            }
            else if (BiaoZhi == 2)
            {
                JuLuY -= SuDu;
                this.Location = new Point(this.Location.X, this.Location.Y + SuDu);
                this.Height -= SuDu;
                if (JuLuY <= 0)
                {
                    BiaoZhi = 0;
                    YiJingZaiZuiShangMian = 3;
                    this.Visible = false;
                    JuLuY = 0;
                }
            }
            else if (BiaoZhi==3)
            {
                if (YiJingZaiZuiShangMian == 3)
                {
                    YiJingZaiZuiShangMian = 1;
                    JuLuY = 0;
                    Point p = From1.PointToScreen(new Point(0, 0));
                    this.Location = new Point(p.X + From1.Width - this.Width, p.Y + From1.Height - 25);
                    this.TopMost = true;
                    this.BringToFront();
                    this.Visible = true;

                    BiaoZhi = 1;
                }
            }
        }

        private void ucCaoZuoBtn1_Click(object sender, EventArgs e)
        {
            BiaoZhi = 2;
            YiJingZaiZuiShangMian = 1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (BuXuYaoTiXian == 1)
            {
                BuXuYaoTiXian = 0;
            }
            else
            {
                BuXuYaoTiXian = 1;
            }
        }
    }


    public interface ITiShiKJ
    {
        /// <summary>
        /// 1表示上 2表示下 0表示不动
        /// </summary>
        /// <param name="canshu"></param>
        /// <returns></returns>
        int SetCanShu(object canshu);
        Control GetKJ();
    }
}
