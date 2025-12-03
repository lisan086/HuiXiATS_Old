using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UC
{
    public partial class ZhuXingKJ : UserControl
    {
        private Pen HuaBi;
        private SolidBrush HuaShua;
        private Font ZiTi;
        public ZhuXingKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            HuaBi = new Pen(Color.Black, 3);
            HuaBi.StartCap = LineCap.Round;
            HuaBi.EndCap = LineCap.Triangle;
            HuaShua = new SolidBrush(Color.Black);
            ZiTi = new Font("微软姚黑", 10);
        }
        private string _XStr = "X轴";
        private string _YStr = "Y轴";
        private float _YJu = 2000f;
    
        private Dictionary<string, List<ZhuXingModel>> ShuJu = new Dictionary<string, List<ZhuXingModel>>();
        public float YJu
        {
            get
            {
                return _YJu;
            }

            set
            {
                _YJu = value;
                this.Refresh();
            }
        }

        public string XStr
        {
            get
            {
                return _XStr;
            }

            set
            {
                _XStr = value;
                this.Refresh();
            }
        }

        public string YStr
        {
            get
            {
                return _YStr;
            }

            set
            {
                _YStr = value;
                this.Refresh();
            }
        }

      

        public void AddData(string x, ZhuXingModel Zhi)
        {
            if (ShuJu.Keys.Contains(x))
            {
                ShuJu[x].Add(Zhi);
            }
            else
            {
                ShuJu.Add(x, new List<ZhuXingModel>());
                ShuJu[x].Add(Zhi);
            }

        }
        public void ShuaXin()
        {
            this.Invalidate();
        }

        public void Clear()
        {
            ShuJu.Clear();
           
        }

        private void ZhuXingKJ_Load(object sender, EventArgs e)
        {
            this.Paint += ZhuXingKJ_Paint;
            this.SizeChanged += ZhuXingKJ_SizeChanged;
        }

        private void ZhuXingKJ_SizeChanged(object sender, EventArgs e)
        {
          
            this.Invalidate();
        }

        private void ZhuXingKJ_Paint(object sender, PaintEventArgs e)
        {
            Graphics huabu = e.Graphics;
            huabu.CompositingQuality = CompositingQuality.HighQuality;
            huabu.SmoothingMode = SmoothingMode.HighQuality;
            #region 定义高度与宽度
            float kaunjux = 20;
            float kaunjuy = 20;
            PointF YuanDain = new PointF(kaunjux, kaunjuy);
            PointF XuanDain = new PointF(this.Width - kaunjux, this.Height - kaunjuy);
            PointF YS = new PointF(kaunjux, this.Height - kaunjuy);
            #endregion
            #region 画X,Y轴
            HuaShua.Color = Color.Black;
            huabu.DrawLine(HuaBi, YS, XuanDain);
            huabu.DrawLine(HuaBi, YS, YuanDain);
            huabu.DrawString(_XStr, ZiTi, HuaShua, XuanDain.X - 20, XuanDain.Y + 5);

            Rectangle rc = new Rectangle();
            rc.X = (int)(YuanDain.X - 20);
            rc.Y = (int)(YuanDain.Y + 10);
            rc.Width = 20;
            rc.Height = 60;
            huabu.DrawString(_YStr, ZiTi, HuaShua, rc);

            #endregion
            #region 画数据
            Font ZiTi1 = new Font("微软姚黑", 8);
            float ybili = QiuBiLi(YuanDain.Y - YS.Y, _YJu);
            float kuangx = 10;
            float kuandu = 30;
            StringFormat stringAlignment = new StringFormat();
            stringAlignment.Alignment = StringAlignment.Center;
            stringAlignment.LineAlignment = StringAlignment.Center;
            foreach (var item in ShuJu.Keys)
            {
                if (ShuJu[item].Count > 0)
                {
                    float x = 0;
                    float y = YS.Y + 2;
                    float Ax = 0;
                    float AY = 20;
                    for (int i = 0; i < ShuJu[item].Count; i++)
                    {
                        HuaShua.Color = ShuJu[item][i].YanSe;
                        PointF dian = new PointF(kuangx, ShuJu[item][i].Value * ybili);
                        PointF zhuanhuan = ZuoBiaoHuanSuan(dian, YS);
                        RectangleF recs = new RectangleF();
                        recs.X = zhuanhuan.X;
                        recs.Y = zhuanhuan.Y;
                        recs.Width = kuandu;
                        recs.Height = XuanDain.Y - zhuanhuan.Y;
                        huabu.FillRectangle(HuaShua, recs);

                        RectangleF recs2 = new RectangleF();
                        recs2.X = zhuanhuan.X;
                        recs2.Y = zhuanhuan.Y-30;
                        recs2.Width = kuandu;
                        recs2.Height = 30;
                        huabu.DrawString(ShuJu[item][i].Value.ToString(), ZiTi1, HuaShua, recs2, stringAlignment);
                        if (i == 0)
                        {
                            x = zhuanhuan.X;
                        }
                        Ax += kuandu;

                        kuangx += kuandu;
                    }
                    huabu.DrawString(item.ToString(), ZiTi, Brushes.Black, new RectangleF(x, y, Ax, AY), stringAlignment);
                    kuangx += 5;
                }

            }
            stringAlignment.Dispose();
            ZiTi1.Dispose();
            #endregion
           
        }

        private float QiuBiLi(float shijigaodu, float xuliegao)
        {
            if (shijigaodu < 0)
            {
                shijigaodu = -shijigaodu;
            }
            float biali = shijigaodu / xuliegao;
            return biali;
        }
        private PointF ZuoBiaoHuanSuan(PointF pointF, PointF Yuandian)
        {
            PointF huansuanpointF = new PointF(0, 0);
            huansuanpointF.X = pointF.X + Yuandian.X;
            huansuanpointF.Y = Yuandian.Y - pointF.Y;
            return huansuanpointF;
        }
    }
    public class ZhuXingModel
    {
        public float Value { get; set; }

        public Color YanSe { get; set; }

        public ZhuXingModel()
        {
            YanSe = Color.Black;
        }
    }
}
