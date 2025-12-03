using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseUI.UI;

namespace BaseUI.UC
{
    public partial class YouXiaJiaoKJ : UserControl
    {
        private int BiaoZhi = 0;
        private DateTime DateTime = DateTime.Now;
        private ITiShiKJ KJ;
        private Func<object> GetCanShu;
        private Form From1 = null;
    
        private int YiJingZaiZuiShangMian = 3;
        private int ShengCount = 1;
        private int JiangCount = 1;
        private int SuDu = 80;
        private int GaoDu = 0;
        private int BuXuYaoTiXian = 1;
        public YouXiaJiaoKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        public void JiaZaiKongJian(ITiShiKJ kJ, Func<object> canshu, Form chuantu)
        {
            KJ = kJ;
            Control ks = kJ.GetKJ();
            ks.Dock = DockStyle.Fill;
            this.buKaPanl1.Controls.Add(ks);
            GetCanShu = canshu;
            From1 = chuantu;
            GaoDu = this.Height;
            DateTime = DateTime.Now;
            this.timer1.Enabled = true;
            this.Hide();
            chuantu.Controls.Add(this);
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
                                    this.Location = new Point(From1.Width - this.Width, From1.Height + this.Height);
                                    this.BringToFront();
                                    this.Visible = true;

                                    BiaoZhi = 1;
                                }
                                else if (YiJingZaiZuiShangMian==2)
                                {
                                    this.Location = new Point(From1.Width - this.Width, From1.Height -this.Height);
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
                this.Location = new Point(From1.Width - this.Width, From1.Height - SuDu*ShengCount);
                ShengCount++;
                if (this.Location.Y+this.Height <= From1.Height)
                {
                    ShengCount = 1;
                    YiJingZaiZuiShangMian = 2;
                    BiaoZhi = 0;
                    
                }
            }
            else if (BiaoZhi == 2)
            {
               
                this.Location = new Point(From1.Width - this.Width, From1.Height- GaoDu + SuDu * JiangCount);
                JiangCount++;
                if (this.Location.Y >= From1.Height)
                {
                    BiaoZhi = 0;
                    YiJingZaiZuiShangMian = 3;
                    this.Visible = false;
                  
                }
            }
            else if (BiaoZhi == 3)
            {
                if (YiJingZaiZuiShangMian == 3)
                {
                    YiJingZaiZuiShangMian = 1;


                    this.Location = new Point(From1.Width - this.Width, From1.Height - this.Height);

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
}
