using BaseUI.DaYIngMoBan.Model;
using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace BaseUI.DaYIngMoBan.TuXing
{
    internal class DrawTuPian : FuTuLei
    {
     
        private string TuPianStr = "";
        private Image Image = null;
        private string XianShiTu = "";
      
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
               
                return XianShiTu;
            }
            set
            {            
                XianShiTu = value;
                if (Image!=null)
                {
                    Image.Dispose();
                    Image = null;
                }
                if (string.IsNullOrEmpty(XianShiTu) == false)
                {
                    string LuJing = Application.StartupPath + @"\TuPian";
                    if (Directory.Exists(LuJing) == false)
                    {
                        Directory.CreateDirectory(LuJing);
                    }
                    try
                    {
                        string[] zhaoduixiang = Directory.GetFiles(LuJing);
                        for (int i = 0; i < zhaoduixiang.Length; i++)
                        {
                            try
                            {
                                string changyongs = ChangYong.GetWenJianName(zhaoduixiang[i]);

                                if (changyongs.Equals(XianShiTu))
                                {
                                    Image sok = Image.FromFile(zhaoduixiang[i]);
                                    Image = (Image)sok.Clone();
                                    sok.Dispose();
                                    break;
                                }
                            }
                            catch
                            {


                            }
                        }


                    }
                    catch
                    {


                    }
                }
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
                return TuLeiType.TuPian;
            }
        }

      

        public override void HuaZiJi(Graphics e)
        {

            using (Pen pen = new Pen(Color.Black, this.CiCun))
            {

                Rectangle rectangle = new Rectangle(this.D1point.X, this.D1point.Y, (this.D2point.X - this.D1point.X), (this.D2point.Y - this.D1point.Y));
                e.DrawRectangle(Pens.Black, rectangle);
                if (Image != null)
                {
                    e.DrawImage(Image, rectangle);
                  
                }


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
            if (fu is DrawTuPian)
            {
                return ChangYong.FuZhiShiTi(fu as DrawTuPian);
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
            bianqian.ShiFouShuYuStaicWenBen =1;
            bianqian.Size = CiCun;
            bianqian.TuPianStr = ImageToBase64(Image);
            bianqian.Text = ConntText;
            bianqian.TongShuID = TongShuID;
            bianqian.Type = 5;
            bianqian.Width = (D2point.X - D1point.X);
            bianqian.IsYiTuPian = false;


            return bianqian;
        }

        public override void SetModel(BiaoQian biaoQian)
        {

            DrawTuPian fu = this;
           
            fu.BanDingText = biaoQian.BianLianYangShi;
            fu.CiCun = (int)biaoQian.Size;          
            fu.IsJingTaiBianLiang = true;
            fu.IsXuanZhong = false;
            fu.TongShuID = biaoQian.TongShuID;
            fu.D1point = new Point(biaoQian.Dx, biaoQian.Dy);
            fu.D2point = new Point(biaoQian.Dx + biaoQian.Width, biaoQian.Dy + biaoQian.Gao);
            fu.ConntText = biaoQian.Text;
            TuPianStr = biaoQian.TuPianStr;
            if (Image == null)
            {
                Image ims = Base64ToImage(TuPianStr);
                if (ims != null)
                {
                    Image = (Image)ims.Clone();
                    ims.Dispose();
                }
            }
        }
        /// <summary>
        /// Base64转图片
        /// </summary>
        /// <param name="data"></param>
        private Image Base64ToImage(string data)
        {
            try
            {
                data = data.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");//将base64头部信息替换
                byte[] bytes = Convert.FromBase64String(data);
                MemoryStream memStream = new MemoryStream(bytes);
                Image mImage = Image.FromStream(memStream);
                Bitmap bp = new Bitmap(mImage);
                
                return bp;
            }
            catch
            {

                
            }
            return null;
           
        }
        /// <summary>
        /// 图片转Base64
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private string ImageToBase64(Image img)
        {
            if (img==null)
            {
                return "";
            }
            try
            {
                Bitmap bmp = new Bitmap(img);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch 
            {
                return "";
            }
        }
    }
}
