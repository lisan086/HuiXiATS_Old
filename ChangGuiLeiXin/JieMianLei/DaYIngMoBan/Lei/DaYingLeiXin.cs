using BaseUI.DaYIngMoBan.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommLei.JiChuLei;
using System.IO;
using System.Drawing.Drawing2D;
using ThoughtWorks.QRCode.Codec;


namespace BaseUI.DaYIngMoBan.Lei
{
    public class DaYingLeiXin<T,V> where T:class where V:class
    {
        /// <summary>
        /// 参数
        /// </summary>
        private JiaZaiMoXing _XMLHelper = JiaZaiMoXing.Ceratei();

        private PrintDocument _PrintDocument;

        private T Tmodel;

        private List<V> _lisVmianXianModel;
       
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="XmlPath"></param>     
        public DaYingLeiXin(string FilePath):this()
        {
            _XMLHelper.LoadMoXing(FilePath);
        

        }
        public DaYingLeiXin()
        {
            _PrintDocument = new PrintDocument();
            _lisVmianXianModel = new List<V>();
            _PrintDocument.PrintPage += _PrintDocument_PrintPage;
        }

        /// <summary>
        /// 刷新模板打印
        /// </summary>
        public void ShuXin(string FilePath)
        {
            _XMLHelper.LoadMoXing(FilePath);
        }

        private void _PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            DaYingShuChu( e.Graphics, _lisVmianXianModel, Tmodel);
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
            //    Margin = 0
            //};
            //BarcodeWriter writer = new BarcodeWriter();
            //writer.Format = BarcodeFormat.QR_CODE;
            //writer.Options = options;
            //return writer.Write(wenben);
            System.Drawing.Bitmap bt;
            if (string.IsNullOrEmpty(wenben))
            {
                wenben = "未知";
            }

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
        private Image ShengChengTuPian(string wenben)
        {         
            if (string.IsNullOrEmpty(wenben) == false)
            {
                Image sok = Base64ToImage(wenben);
                if (sok != null)
                {
                    //Image xin = (Image)sok.Clone();
                   // sok.Dispose();
                    return sok;
                }
            }
            return null;
        }
        /// <summary>
        /// 打印输出
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="g"></param>
        /// <param name="LisMingXi"></param>
        /// <param name="feimingxi"></param>
        protected  void DaYingShuChu(Graphics g, List<V> LisMingXi, T feimingxi)
        {
          
            ZBiaoQianModel pt = _XMLHelper.LisWuLiao;
            if (pt != null)
            {
               
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                ShuChu(pt, g);
                //if (pt.IsCaiYongTuPianShuChu)
                //{
                //   // TuPianShuChu(pt,g);
                   

                  
                //}
                //else
                //{
                   
                //}
              
            }

        }
        private void TuPianShuChu(ZBiaoQianModel pt, Graphics g)
        {
            int widthDPI = (int)g.DpiX;
            int heightDPI = (int)g.DpiY;
            int Height = pt.GaoDu * heightDPI / 100;
            int Width = pt.KuanDu * widthDPI / 100;
            Bitmap bitmap = new Bitmap(Width, Height);
            Graphics grapPic = Graphics.FromImage(bitmap);

            grapPic.CompositingQuality = CompositingQuality.HighQuality;
            grapPic.SmoothingMode =SmoothingMode.HighQuality;
            grapPic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grapPic.PixelOffsetMode =PixelOffsetMode.HighQuality;


            ShuChu(pt, grapPic);
            Bitmap xin =bitmap;
            grapPic.Dispose();
          

            Rectangle sec = new Rectangle();
            sec.X = 0;
            sec.Y = 0;
            sec.Width = pt.KuanDu;
            sec.Height = pt.GaoDu;
            Rectangle secd = new Rectangle();
            secd.X = 0;
            secd.Y = 0;
            secd.Width = Width;
            secd.Height = Height;
            g.DrawImage(xin, sec, secd,GraphicsUnit.Pixel);

            xin.Dispose();
           
        }

        private void ShuChu(ZBiaoQianModel pt, Graphics g)
        {
            int mixiangaodu = 0;
            int weizhi = 0;
            int c = 0;
            int gaodu = 0;
            if (pt.MingXian.Count > 0)
            {
                if (_lisVmianXianModel.Count > 0)
                {
                    for (int i = 0; i < _lisVmianXianModel.Count; i++)
                    {
                        #region MyRegion
                        V mianxi = _lisVmianXianModel[i];
                        Type t = mianxi.GetType();
                        PropertyInfo[] shuxin = t.GetProperties();
                        foreach (BiaoQian item in pt.MingXian)
                        {
                            string value = GetMingXiValue(shuxin, item.BianLianYangShi, mianxi);
                            HuaText(g, item, value, mixiangaodu);

                            if (c == 0)
                            {
                                c++;
                                weizhi = item.Dy;
                                gaodu = item.Gao;
                            }
                        }

                        #endregion
                        mixiangaodu += gaodu;

                    }
                }

            }
            foreach (BiaoQian item in pt.lc)
            {
                if (item.ShiFouShuYuStaicWenBen == 1)
                {

                    #region 打印
                    if (item.Type == 1)
                    {
                        #region dabao
                        if (mixiangaodu == 0)
                        {
                            HuaText(g, item, item.Text, mixiangaodu);

                        }
                        else
                        {
                            if (item.Dy > weizhi)
                            {
                                #region MyRegion

                                HuaText(g, item, item.Text, mixiangaodu + 1);

                                #endregion

                            }
                            else
                            {
                                HuaText(g, item, item.Text, 0);
                            }
                        }
                        #endregion
                    }
                    else if (item.Type == 2)
                    {

                        #region dabao
                        if (mixiangaodu == 0)
                        {
                            using (Pen pen = new Pen(Color.Black, item.Size))
                            {
                                g.DrawLine(pen, item.Dx, item.Dy, item.Dx + item.Width, item.Dy + item.Gao);
                            }

                        }
                        else
                        {
                            if (item.Dy > weizhi)
                            {

                                using (Pen pen = new Pen(Color.Black, item.Size))
                                {
                                    g.DrawLine(pen, item.Dx, item.Dy, item.Dx + item.Width, item.Dy + item.Gao + mixiangaodu + 1);
                                }
                            }
                            else
                            {
                                using (Pen pen = new Pen(Color.Black, item.Size))
                                {
                                    g.DrawLine(pen, item.Dx, item.Dy, item.Dx + item.Width, item.Dy + item.Gao);
                                }
                            }
                        }
                        #endregion

                    }
                    else if (item.Type == 3)
                    {
                        Rectangle rectangle = new Rectangle(item.Dx, item.Dy, item.Width, item.Gao);
                        #region dabao
                        if (mixiangaodu == 0)
                        {
                            using (Pen pen = new Pen(Color.Black, item.Size))
                            {
                                g.DrawRectangle(pen, rectangle);
                            }

                        }
                        else
                        {
                            if (item.Dy > weizhi)
                            {
                                rectangle.Y += mixiangaodu + 1;
                                using (Pen pen = new Pen(Color.Black, item.Size))
                                {
                                    g.DrawRectangle(pen, rectangle);
                                }
                            }
                            else
                            {
                                using (Pen pen = new Pen(Color.Black, item.Size))
                                {
                                    g.DrawRectangle(pen, rectangle);
                                }
                            }
                        }
                        #endregion
                    }
                    else if (item.Type == 5)
                    {
                        Rectangle rectangle = new Rectangle(item.Dx, item.Dy, item.Width, item.Gao);
                        Image shijian = ShengChengTuPian(item.TuPianStr);
                        #region dabao
                        if (mixiangaodu == 0)
                        {

                        }
                        else
                        {
                            if (item.Dy > weizhi)
                            {
                                #region MyRegion
                                rectangle.Y += mixiangaodu + 1;

                                #endregion

                            }

                        }
                        if (shijian != null)
                        {
                            g.DrawImage(shijian, rectangle);
                            // g.DrawImage(shijian, rectangle,new RectangleF(0,0, shijian.Width, shijian.Height),GraphicsUnit.Pixel);
                            shijian.Dispose();
                        }
                        #endregion
                    }
                    #endregion

                }
                else
                {
                    string value = GetZongValue(item);
                    if (item.Type == 1)
                    {
                        if (mixiangaodu == 0)
                        {
                            HuaText(g, item, value, 0);
                        }
                        else
                        {
                            if (item.Dy > weizhi)
                            {
                                HuaText(g, item, value, mixiangaodu + 1);
                            }
                            else
                            {
                                HuaText(g, item, value, 0);
                            }
                        }
                    }
                    else if (item.Type == 4)
                    {
                        if (string.IsNullOrEmpty(value) == false)
                        {
                            Rectangle rectangle = new Rectangle(item.Dx, item.Dy, item.Width, item.Gao);
                            Bitmap shijian = ShengChengDuPian(value, rectangle.Width, rectangle.Height);
                            #region dabao
                            if (mixiangaodu == 0)
                            {

                            }
                            else
                            {
                                if (item.Dy > weizhi)
                                {
                                    #region MyRegion
                                    rectangle.Y += mixiangaodu + 1;

                                    #endregion

                                }

                            }
                            g.DrawImage(shijian, rectangle);
                            shijian.Dispose();
                        }
                        #endregion
                    }

                }
            }
        }
        private string GetMingXiValue(PropertyInfo[] shuxin,string ziduan,V mianxi)
        {
            string value = "明细";
            for (int j = 0; j < shuxin.Length; j++)
            {
                #region 赋值
                string shu = shuxin[j].Name;
                if (ziduan.EndsWith(shu))
                {
                    try
                    {

                        value = shuxin[j].GetValue(mianxi, null).ToString();
                    }
                    catch
                    {


                    }


                    break;
                }
                #endregion
            }
            return value;
        }

        private string GetZongValue(BiaoQian item)
        {
            Type t;
            if (Tmodel == null)
            {
                t = typeof(object);

            }
            else
            {
                t = Tmodel.GetType();
            }
            string txt = item.BianLianYangShi;
            txt = txt.Replace("?", "");
            bool zhen = true;
            PropertyInfo[] shuxin = t.GetProperties();
            string value = "";
            for (int i = 0; i < shuxin.Length; i++)
            {
                string shu = shuxin[i].Name;
                if (txt.Equals(shu))
                {
                    try
                    {
                        zhen = false;
                        value = shuxin[i].GetValue(Tmodel, null).ToString();
                    }
                    catch
                    {


                    }


                    break;
                }

            }
            if (zhen)
            {
                value = item.Text;
            }
            if (item.BianLianYangShi == "?DanQianShi")
            {
                value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return value;
        }

        private void HuaText(Graphics g, BiaoQian item,string value,int mixiangaodu)
        {
            Rectangle rectangle = new Rectangle(item.Dx, item.Dy + mixiangaodu, item.Width, item.Gao);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            if (item.JuZhong == 2)
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            else if (item.JuZhong == 3)
            {
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            if (item.IsYiTuPian == false)
            {
                using (Font ziti = new Font(item.ZiTiYangShi, item.Size, item.IsJiaCu ? FontStyle.Bold : FontStyle.Regular, item.GraphicsUnit))
                {
                   
                   
                    if (item.IsXianShiKuan)
                    {
                        g.DrawRectangle(Pens.Black, rectangle);
                    }
                    g.DrawString(value, ziti, solidBrush, rectangle, stringFormat);
                  
                    stringFormat.Dispose();
                    solidBrush.Dispose();

                }
            }
            else
            {
                try
                {
                    int kaudu = item.Width;
                    int gaodu = item.Gao + 20;
                    Bitmap bitmap = new Bitmap(kaudu, gaodu);                                 
                    Graphics grapPic = Graphics.FromImage(bitmap);
                    grapPic.CompositingQuality = CompositingQuality.HighQuality;
                    grapPic.SmoothingMode = SmoothingMode.HighQuality;
                    grapPic.InterpolationMode = InterpolationMode.NearestNeighbor;
                    grapPic.PixelOffsetMode = PixelOffsetMode.Half;
                    Font ziti = new Font(item.ZiTiYangShi, item.Size, item.IsJiaCu ? FontStyle.Bold : FontStyle.Regular, item.GraphicsUnit);
                    string text = value;                
                    grapPic.DrawString(text, ziti, solidBrush, new Rectangle(0,0, kaudu, gaodu), stringFormat);
                    g.DrawImage(bitmap, rectangle, new Rectangle(0, 0, kaudu, gaodu),GraphicsUnit.Pixel);
                    grapPic.Dispose();
                    ziti.Dispose();
                    bitmap.Dispose();
                  
                    stringFormat.Dispose();
                    solidBrush.Dispose();
                }
                catch 
                {

                    
                }
              

            }
           
        }

        /// <summary>
        /// 提供打印预览
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lisv"></param>
        public virtual void DaYingYuLan(T model, List<V> lisv, int fenshu, string dayingjiming,bool iszhijiedaying)
        {
            Tmodel = model;
            _lisVmianXianModel = lisv;
            ZBiaoQianModel pt = _XMLHelper.LisWuLiao;
            if (pt != null)
            {
               
                int yeshu = fenshu <= 0 ? 1 : fenshu;
                this._PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", pt.KuanDu, pt.GaoDu);
                this._PrintDocument.DefaultPageSettings.Landscape = pt.DaYingMoShi == 2;
                this._PrintDocument.PrinterSettings.Copies = (short)yeshu;

                if (string.IsNullOrEmpty(dayingjiming) == false)
                {
                    this._PrintDocument.PrinterSettings.PrinterName = dayingjiming;


                }
                if (iszhijiedaying==false)
                {
                    PrintPreviewDialog pd = new PrintPreviewDialog();
                    pd.Document = this._PrintDocument;
                    pd.PrintPreviewControl.Zoom = 1.0;
                    pd.WindowState = FormWindowState.Maximized;
                    pd.ShowInTaskbar = true;
                    pd.ShowDialog();
                  
                }
                else
                {
                    this._PrintDocument.Print();
                }
            }

      

        }


    }
}
