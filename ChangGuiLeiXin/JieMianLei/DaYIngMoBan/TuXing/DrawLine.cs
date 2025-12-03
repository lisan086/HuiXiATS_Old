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
    internal class DrawLine : FuTuLei
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
                return TuLeiType.ZhiXian;
            }
        }

        public override void HuaZiJi(Graphics e)
        {
            if (IsXuanZhong)
            {
                for (int i = 1; i <= 3; i += 2)
                {
                    Rectangle rectangle = GetRectangle(i);
                    e.FillRectangle(Brushes.Black, rectangle);
                }
            }
            using (Pen pen = new Pen(Color.Black, this.CiCun))
            {
                e.DrawLine(pen, this.D1point, this.D2point);
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
                case 3:
                    {
                        rectangle.X = D2point.X - JuXingZhiJing;
                        rectangle.Y = D2point.Y - JuXingZhiJing;
                        rectangle.Width = JuXingZhiJing * 2;
                        rectangle.Height = JuXingZhiJing * 2;
                    }
                    break;
                default:
                    break;
            }
            return rectangle;
        }

        public override bool IsShuBiaoShiFouZai(Point point)
        {
            try
            {
                int xuanzhong = CiCun < 4 ? 5 : CiCun;
                Pen pen = new Pen(Color.Empty, xuanzhong);
                GraphicsPath path = new GraphicsPath();
                path.AddLine(this.D1point, this.D2point);
                path.Widen(pen);
                Region region = new Region(path);
                bool zhen = region.IsVisible(point);
                pen.Dispose();
                region.Dispose();
                return zhen;
            }
            catch
            {

                return false;
            }
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
            bianqian.Type = 2;
            bianqian.Width = (D2point.X - D1point.X);

            return bianqian;
        }

        public override void SetModel(BiaoQian biaoQian)
        {
          
            this.BanDingText = biaoQian.BianLianYangShi;
            this.CiCun = (int)biaoQian.Size;
            this.ConntText = biaoQian.Text;
            this.IsJingTaiBianLiang = true;
            this.IsXuanZhong = false;
            this.D1point = new Point(biaoQian.Dx, biaoQian.Dy);
            this.D2point = new Point(biaoQian.Dx + biaoQian.Width, biaoQian.Dy + biaoQian.Gao);
            this.TongShuID = biaoQian.TongShuID;
          
        }

        public override FuTuLei FuZhi(FuTuLei fu)
        {
            if (fu is DrawLine)
            {
                return ChangYong.FuZhiShiTi(fu as DrawLine);
            }
            return null;
        }
    }
}
