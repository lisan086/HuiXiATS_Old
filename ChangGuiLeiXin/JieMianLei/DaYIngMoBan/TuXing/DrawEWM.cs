using BaseUI.DaYIngMoBan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommLei.JiChuLei;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;


namespace BaseUI.DaYIngMoBan.TuXing
{
    internal class DrawEWM : FuTuLei
    {
        private string BanDing = "";
        private int JuXingZhiJing = 3;

     

        private string NeiRong = "";
        /// <summary>
        /// 尺寸大小
        /// </summary>
        [Browsable(true)]
        public int CiCun { get; set; } = 1;
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
                return NeiRong;
            }

            set
            {
                NeiRong = value;
            }
        }

        public override bool IsJingTaiBianLiang
        {
            get
            {
                return false;
            }

            set
            {
                
            }
        }

        public override TuLeiType Type
        {
            get
            {
                return TuLeiType.ErWeiMa;
            }
        }

        public override void HuaZiJi(Graphics e)
        {
            
            using (Pen pen = new Pen(Color.Black, this.CiCun))
            {
              
                Rectangle rectangle = new Rectangle(this.D1point.X, this.D1point.Y, (this.D2point.X - this.D1point.X), (this.D2point.Y - this.D1point.Y));
                Bitmap shijian = ShengChengDuPian(BanDing, rectangle.Width, rectangle.Height);           
         
                e.DrawImage(shijian, rectangle);
                shijian.Dispose();
             
              
            }
            if (IsXuanZhong)
            {
                for (int i = 1; i <= 8; i++)
                {
                    Rectangle rectangle = GetRectangle(i);
                    e.FillRectangle(Brushes.Black, rectangle);
                }
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
        public override FuTuLei FuZhi(FuTuLei fu)
        {
            if (fu is DrawEWM)
            {
                return ChangYong.FuZhiShiTi(fu as DrawEWM);
            }
            return null;
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


            bianqian.ShiFouShuYuStaicWenBen =2;
            bianqian.Size = CiCun;
            bianqian.Text = ConntText;
            bianqian.TongShuID = TongShuID;
            bianqian.Type = 4;
            bianqian.Width = (D2point.X - D1point.X);



            return bianqian;
        }

        public override void SetModel(BiaoQian biaoQian)
        {

            DrawEWM fu = this;
            fu.BanDingText = biaoQian.BianLianYangShi;
            fu.CiCun = (int)biaoQian.Size;
            fu.ConntText = biaoQian.Text;
            fu.IsJingTaiBianLiang =false;

            fu.IsXuanZhong = false;
            fu.TongShuID = biaoQian.TongShuID;
            fu.D1point = new Point(biaoQian.Dx, biaoQian.Dy);
            fu.D2point = new Point(biaoQian.Dx + biaoQian.Width, biaoQian.Dy + biaoQian.Gao);


        }

        private Bitmap ShengChengDuPian(string wenben, int kuan, int gao)
        {
            //if (string.IsNullOrEmpty(wenben))
            //{
            //    wenben = "未知";
            //}
            //EncodingOptions options = new QrCodeEncodingOptions
            //{
            //    DisableECI = true,
            //    CharacterSet = "UTF-8",
            //    Width = kuan,
            //    Height = gao,
            //    NoPadding = false,
            //    Margin = 1
            //};
            //BarcodeWriter writer = new BarcodeWriter();
            //writer.Format = BarcodeFormat.QR_CODE;
            //writer.Options = options;
            //return writer.Write(wenben);

            System.Drawing.Bitmap bt;
          
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
            qrCodeEncoder.QRCodeScale = 12;//大小(值越大生成的二维码图片像素越高)
            qrCodeEncoder.QRCodeVersion = 2;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;//错误效验、错误更正(有4个等级)
            qrCodeEncoder.QRCodeBackgroundColor = Color.White;//背景色
            qrCodeEncoder.QRCodeForegroundColor = Color.Black;//前景色

            bt = qrCodeEncoder.Encode(wenben,Encoding.UTF8);


            return bt;
        }
    }
}
