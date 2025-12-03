using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UC
{
    public class UCBtn : Button
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCBtn()
            : base()
        {
            this.DoubleBuffered = true;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Paint += UCBtn_Paint;
        
        }

       

        private void UCBtn_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                CreateRoundedRectanglePath(_BanJin, e.Graphics);
            }
            catch
            {


            }
        }
        private void CreateRoundedRectanglePath(int cornerRadius, Graphics e)
        {

            #region 获取裁剪的矩形     

            #endregion
            #region 初始化画笔和画布
            Graphics g = e;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            Pen huanbiXian = new Pen(_WaiBianKuangSe, _XianKuang);
            huanbiXian.StartCap = LineCap.RoundAnchor;
            huanbiXian.EndCap = LineCap.RoundAnchor;
            #endregion
            // 圆角半径
            int cRadius = cornerRadius;

            #region 初始倒角
            Rectangle ric = this.ClientRectangle;
            RectangleF rect1 = new RectangleF();
            rect1.X = ric.X;
            rect1.Y = ric.Y;
            rect1.Width = ric.Width;
            rect1.Height = ric.Height;

            #region 走线
            GraphicsPath myPath = new GraphicsPath();
            myPath.StartFigure();
            myPath.AddArc(new RectangleF(new PointF(rect1.X, rect1.Y), new Size(2 * cRadius, 2 * cRadius)), 180, 90);
            myPath.AddLine(new PointF(rect1.X + cRadius, rect1.Y), new PointF(rect1.Right - cRadius, rect1.Y));
            myPath.AddArc(new RectangleF(new PointF(rect1.Right - 2 * cRadius, rect1.Y), new SizeF(2 * cRadius, 2 * cRadius)), 270, 90);
            myPath.AddLine(new PointF(rect1.Right, rect1.Y + cRadius), new PointF(rect1.Right, rect1.Bottom - cRadius));
            myPath.AddArc(new RectangleF(new PointF(rect1.Right - 2 * cRadius, rect1.Bottom - 2 * cRadius), new Size(2 * cRadius, 2 * cRadius)), 0, 90);
            myPath.AddLine(new PointF(rect1.Right - cRadius, rect1.Bottom), new PointF(rect1.X + cRadius, rect1.Bottom));
            myPath.AddArc(new RectangleF(new PointF(rect1.X, rect1.Bottom - 2 * cRadius), new SizeF(2 * cRadius, 2 * cRadius)), 90, 90);
            myPath.AddLine(new PointF(rect1.X, rect1.Bottom - cRadius), new PointF(rect1.X, rect1.Y + cRadius));
            myPath.CloseFigure();
            this.Region = new Region(myPath);
            #endregion

            #endregion
            float chaju = 0f;
            #region 画边框         
            rect1.X = rect1.X + _XianKuang + chaju;
            rect1.Y = ric.Y + _XianKuang + chaju;
            rect1.Width = rect1.Width - 2 * (_XianKuang + chaju);
            rect1.Height = rect1.Height - 2 * (_XianKuang + chaju);
            GraphicsPath zouxianPath = new GraphicsPath();
            zouxianPath.StartFigure();
            zouxianPath.AddArc(new RectangleF(new PointF(rect1.X, rect1.Y), new Size(2 * cRadius, 2 * cRadius)), 180, 90);
            zouxianPath.AddLine(new PointF(rect1.X + cRadius, rect1.Y), new PointF(rect1.Right - cRadius, rect1.Y));
            zouxianPath.AddArc(new RectangleF(new PointF(rect1.Right - 2 * cRadius, rect1.Y), new SizeF(2 * cRadius, 2 * cRadius)), 270, 90);
            zouxianPath.AddLine(new PointF(rect1.Right, rect1.Y + cRadius), new PointF(rect1.Right, rect1.Bottom - cRadius));
            zouxianPath.AddArc(new RectangleF(new PointF(rect1.Right - 2 * cRadius, rect1.Bottom - 2 * cRadius), new Size(2 * cRadius, 2 * cRadius)), 0, 90);
            zouxianPath.AddLine(new PointF(rect1.Right - cRadius, rect1.Bottom), new PointF(rect1.X + cRadius, rect1.Bottom));
            zouxianPath.AddArc(new RectangleF(new PointF(rect1.X, rect1.Bottom - 2 * cRadius), new SizeF(2 * cRadius, 2 * cRadius)), 90, 90);
            zouxianPath.AddLine(new PointF(rect1.X, rect1.Bottom - cRadius), new PointF(rect1.X, rect1.Y + cRadius));
            zouxianPath.CloseFigure();
            g.DrawPath(huanbiXian, zouxianPath);
            #endregion 
            huanbiXian.Dispose();


        }
        /// <summary>
        /// 倒角半径
        /// </summary>
        private int _BanJin = 1;
        /// <summary>
        /// 画边线的宽度
        /// </summary>
        private float _XianKuang = 1.5f;
        /// <summary>
        /// 画边线的宽度
        /// </summary>
        public float XianKuang
        {
            get { return _XianKuang; }
            set { _XianKuang = value; this.Refresh(); }
        }
        /// <summary>
        /// 外边框色调
        /// </summary>
        private Color _WaiBianKuangSe = Color.Green;
        /// <summary>
        /// 外边框色调
        /// </summary>
        public Color WaiBianKuangSe
        {
            get { return _WaiBianKuangSe; }
            set
            {
                _WaiBianKuangSe = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 倒角半径
        /// </summary>
        public int BanJin
        {
            get { return _BanJin; }
            set
            {
                _BanJin = value;
                this.Refresh();
            }
        }

    }
}
