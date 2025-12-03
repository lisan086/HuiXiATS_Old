using BaseUI.DaYIngMoBan.Model;
using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUI.DaYIngMoBan.TuXing
{
    internal class DrawText : FuTuLei
    {
        private int JuXingZhiJing = 3;
        private string BanDing = "";
        private string XianShi = "";
        private bool IsJingTaiWenBen = false;
     
        public override string BanDingText
        {
            get
            {

                return BanDing;
            }

            set
            {
                BanDing = value;

            }
        }

        public override string ConntText
        {
            get
            {
                return XianShi;
            }

            set
            {
                XianShi = value;
            }
        }

        public override bool IsJingTaiBianLiang
        {
            get
            {
                if (BanDing != "")
                {
                    IsJingTaiWenBen = false;
                }
                else
                {
                    IsJingTaiWenBen = true;
                }
                return IsJingTaiWenBen;
            }

            set
            {

            }
        }

        public override TuLeiType Type
        {
            get
            {
                return TuLeiType.WenZi;
            }
        }

        /// <summary>
        /// 1是居左 2是居中 3是居右
        /// </summary>
        public int IsJuZhong { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public bool IsWenZiTuPian { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public bool XianShiKuan { get; set; } = false;

        public Font Font { get; set; } = new Font("微软姚黑",8);

        public DrawText()
        {
          
            XianShi = "加入";
        }

        public override void HuaZiJi(Graphics e)
        {
            if (IsXuanZhong)
            {
                for (int i = 1; i <= 8; i++)
                {
                    Rectangle rectangles = GetRectangle(i);
                    e.FillRectangle(Brushes.Black, rectangles);
                }
            }
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            if (this.IsJuZhong == 2)
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            else if (this.IsJuZhong == 3)
            {
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            Rectangle rectangle = new Rectangle(this.D1point.X, this.D1point.Y, (this.D2point.X - this.D1point.X), (this.D2point.Y - this.D1point.Y));
            e.DrawRectangle(Pens.Black, rectangle);

            e.DrawString(XianShi, Font, Brushes.Black, rectangle, stringFormat);
            stringFormat.Dispose();

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
            bianqian.IsJiaCu =Font.Bold;
            bianqian.GraphicsUnit = Font.Unit;
            bianqian.IsXieTi = Font.Italic;
            bianqian.IsXianShiKuan = XianShiKuan;
            bianqian.JuZhong = IsJuZhong;
            bianqian.ShiFouShuYuStaicWenBen =IsJingTaiBianLiang ? 1 : 2;
            bianqian.Size = Font.Size;
            bianqian.Text = ConntText;
            bianqian.TongShuID = TongShuID;
            bianqian.Type = 1;
            bianqian.Width = (D2point.X - D1point.X);
            bianqian.ZiTiYangShi = Font.Name;
            bianqian.IsYiTuPian = IsWenZiTuPian;
            if (IsJingTaiBianLiang)
            {
              
            }
            else
            {
                if (bianqian.BianLianYangShi.StartsWith("*&"))
                {
                    ismingxi = true;
                }
                else
                {
                   
                }
            }



            return bianqian;
        }

        public override void SetModel(BiaoQian biaoQian)
        {

            DrawText fu = this;
          
            fu.BanDingText = biaoQian.BianLianYangShi;
            fu.Font = new Font(biaoQian.ZiTiYangShi, biaoQian.Size, biaoQian.IsJiaCu ? FontStyle.Bold : FontStyle.Regular,biaoQian.GraphicsUnit);
            fu.ConntText = biaoQian.Text;
            fu.IsJingTaiBianLiang = biaoQian.ShiFouShuYuStaicWenBen == 1 ? true : false;
            fu.IsJuZhong = biaoQian.JuZhong;
            fu.IsXuanZhong = false;
            fu.D1point = new Point(biaoQian.Dx, biaoQian.Dy);
            fu.D2point = new Point(biaoQian.Dx + biaoQian.Width, biaoQian.Dy + biaoQian.Gao);
            fu.TongShuID = biaoQian.TongShuID;
            fu.IsWenZiTuPian = biaoQian.IsYiTuPian;
        }

        public override FuTuLei FuZhi(FuTuLei fu)
        {
            if (fu is DrawText)
            {
                return ChangYong.FuZhiShiTi(fu as DrawText);
            }
            return null;
        }
    }
}
