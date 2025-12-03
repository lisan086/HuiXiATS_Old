using BaseUI.DaYIngMoBan.Model;
using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUI.DaYIngMoBan.TuXing
{
    internal class DrawRect : FuTuLei
    {
        private int JuXingZhiJing = 3;
        /// <summary>
        /// 尺寸大小
        /// </summary>
        [Browsable(true)]
        public int CiCun { get; set; } = 1;
        public override string BanDingText
        {
            get
            {
                return "";
            }

            set
            {

            }
        }

        public override string ConntText
        {
            get
            {
                return "";
            }

            set
            {

            }
        }

        public override bool IsJingTaiBianLiang
        {
            get
            {
                return true;
            }

            set
            {

            }
        }

        public override TuLeiType Type
        {
            get
            {
                return TuLeiType.JuXing;
            }
        }

        public override void HuaZiJi(Graphics e)
        {
            if (IsXuanZhong)
            {
                for (int i = 1; i <= 8; i++)
                {
                    Rectangle rectangle = GetRectangle(i);
                    e.FillRectangle(Brushes.Black, rectangle);
                }
            }
            using (Pen pen = new Pen(Color.Black, this.CiCun))
            {
                e.DrawRectangle(pen, this.D1point.X, this.D1point.Y, (this.D2point.X - this.D1point.X), (this.D2point.Y - this.D1point.Y));
            }

        }
        public override bool IsShuBiaoShiFouZai(Point point)
        {
            try
            {
                Rectangle rct = new Rectangle(this.D1point.X, this.D1point.Y, 1, 1);
                rct.Width = this.D2point.X - this.D1point.X;
                rct.Height = this.D2point.Y - this.D1point.Y;
                bool zhen = rct.Contains(point);
                return zhen;
            }
            catch
            {

                return false;
            }
        }

        protected override Rectangle GetRectangle(int fangxiang)
        {
            Rectangle rectangle = new Rectangle(0, 0, 0, 0);
            switch (fangxiang)
            {
                case 1:
                    {
                        rectangle.X = D1point.X - JuXingZhiJing;
                        rectangle.Y = D1point.Y - JuXingZhiJing;
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                case 2:
                    {
                        rectangle.X = D1point.X + ((D2point.X - D1point.X) / 2 - JuXingZhiJing);
                        rectangle.Y = D1point.Y - JuXingZhiJing;
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                case 3:
                    {
                        rectangle.X = D2point.X - JuXingZhiJing;
                        rectangle.Y = D1point.Y - JuXingZhiJing;
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                case 4:
                    {
                        rectangle.X = D2point.X - JuXingZhiJing;
                        rectangle.Y = D1point.Y + ((D2point.Y - D1point.Y) / 2 - JuXingZhiJing);
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                case 5:
                    {
                        rectangle.X = D2point.X - JuXingZhiJing;
                        rectangle.Y = D2point.Y - JuXingZhiJing;
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                case 6:
                    {
                        rectangle.X = D1point.X + ((D2point.X - D1point.X) / 2 - JuXingZhiJing);
                        rectangle.Y = D2point.Y - JuXingZhiJing;
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                case 7:
                    {
                        rectangle.X = D1point.X - JuXingZhiJing;
                        rectangle.Y = D2point.Y - JuXingZhiJing;
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                case 8:
                    {
                        rectangle.X = D1point.X - JuXingZhiJing;
                        rectangle.Y = D1point.Y + ((D2point.Y - D1point.Y) / 2 - JuXingZhiJing);
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                default:
                    break;
            }
            return rectangle;
        }

        public override BiaoQian GetModel(out bool ismingxi)
        {
            ismingxi = false;
          
            BiaoQian bianqian = new BiaoQian();
            bianqian.BianLianYangShi = BanDingText;
            bianqian.Dx = D1point.X;
            bianqian.Dy = D1point.Y;
            bianqian.Gao = (D2point.Y - D1point.Y);


            bianqian.ShiFouShuYuStaicWenBen = 1;
            bianqian.Size = CiCun;
            bianqian.Text = ConntText;
            bianqian.TongShuID = TongShuID;
            bianqian.Type = 3;
            bianqian.Width = (D2point.X - D1point.X);

         

            return bianqian;
        }

        public override void SetModel(BiaoQian biaoQian)
        {

            DrawRect fu =this;
            fu.BanDingText = biaoQian.BianLianYangShi;
            fu.CiCun = (int)biaoQian.Size;
            fu.ConntText = biaoQian.Text;
            fu.IsJingTaiBianLiang = true;

            fu.IsXuanZhong = false;
            fu.TongShuID = biaoQian.TongShuID;
            fu.D1point = new Point(biaoQian.Dx, biaoQian.Dy);
            fu.D2point = new Point(biaoQian.Dx + biaoQian.Width, biaoQian.Dy + biaoQian.Gao);
            

        }

        public override FuTuLei FuZhi(FuTuLei fu)
        {
            if (fu is DrawRect)
            {
                return ChangYong.FuZhiShiTi(fu as DrawRect);
            }
            return null;
        }
    }
}
