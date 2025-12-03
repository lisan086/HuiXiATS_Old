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
    public partial class SheBeiDengKJ : UserControl
    {
        public SheBeiDengKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

     

        private Color _ColorYanSe;
        public Color ColorYanSe
        {
            get
            {
                return _ColorYanSe;
            }
            set
            {
                Color yanse = value;
                if (yanse != _ColorYanSe)
                {
                    _ColorYanSe = value;
                    this.Invalidate();
                   
                }

            }
        }

        private string _StrName = "";

        public string StrName
        {
            get { return _StrName; }
            set
            {
                if (_StrName.Equals(value) == false)
                {
                    _StrName = value;
                    this.Invalidate();
                }
            }
        }

        private Color _ZiTiYanSe = Color.Black;

        public Color ZiTiYanSe
        {
            get { return _ZiTiYanSe; }
            set { _ZiTiYanSe = value; this.Invalidate(); }
        }

        private int _ZiTiDaXiao = 12;

        public int ZiTiDaXiao
        {
            get { return _ZiTiDaXiao; }
            set { _ZiTiDaXiao = value; this.Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int kuang = this.Width;
            int Gao = this.Height - 10;
            if (Gao > 0)
            {
                Rectangle rc1 = new Rectangle(5, 5, Gao, Gao);
                SolidBrush huashua = new SolidBrush(_ColorYanSe);
                e.Graphics.FillEllipse(huashua, rc1);
                e.Graphics.DrawEllipse(Pens.Black, rc1);
                huashua.Dispose();
                StringFormat stringAlignment = new StringFormat();
                stringAlignment.Alignment = StringAlignment.Near;
                stringAlignment.LineAlignment = StringAlignment.Center;
                Font ziti = new Font("微软雅黑", _ZiTiDaXiao);
                SolidBrush huashua1 = new SolidBrush(_ZiTiYanSe);
                Rectangle rectangle = new Rectangle();
                rectangle.X = Gao + 6;
                rectangle.Y = 5;
                rectangle.Width = this.Width - Gao - 6;
                rectangle.Height = Gao;
                e.Graphics.DrawString(_StrName, ziti, huashua1, rectangle, stringAlignment);
                stringAlignment.Dispose();
                huashua1.Dispose();
                ziti.Dispose();
            }

        }
    }
}
