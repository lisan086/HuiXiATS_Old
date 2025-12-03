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
    public partial class IOYuan : UserControl
    {
        public IOYuan()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            iniData();
        }

        #region 对外公开属性
        /// <summary>
        /// 设置颜色
        /// </summary>
        private Color _ColorYanSe;
        /// <summary>
        /// 设置颜色
        /// </summary>
        public Color ColorYanSe
        {
            get
            {
                return _ColorYanSe;
            }
            set
            {
                if (_ColorYanSe.Equals(value) == false)
                {
                    _ColorYanSe = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// 设置是否填充
        /// </summary>
        private FullOrKong _FullOrKong;
        /// <summary>
        /// 设置是否填充
        /// </summary>
        public FullOrKong fullOrKong
        {
            get
            {
                return _FullOrKong;
            }
            set
            {
                if (_FullOrKong.Equals(value))
                {
                    _FullOrKong = value;
                    this.Invalidate();
                }
            }
        }

        #endregion
        private void iniData()
        {
            this._ColorYanSe = Color.Blue;
            this._FullOrKong = FullOrKong.Full;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                SolidBrush huashua = new SolidBrush(_ColorYanSe);
                Pen huabi = new Pen(_ColorYanSe, 1.5f);
                int kuan = this.Width;
                if (kuan > this.Height)
                {
                    kuan = this.Height;
                }
                kuan = kuan - 4;
                Rectangle rect1 = this.ClientRectangle;
                rect1.Y = this.Height / 2 - kuan / 2;
                rect1.X = this.Width / 2 - kuan / 2;
                rect1.Width = kuan;
                rect1.Height = kuan;
                switch (_FullOrKong)
                {
                    case FullOrKong.Full:
                        {
                            e.Graphics.FillEllipse(huashua, rect1);
                        }
                        break;
                    case FullOrKong.Kong:
                        {

                            e.Graphics.DrawEllipse(huabi, rect1);
                        }
                        break;
                    default:
                        {

                            e.Graphics.FillEllipse(huashua, rect1);
                        }
                        break;
                }
                huabi.Dispose();
                huashua.Dispose();
            }
            catch
            {


            }


            base.OnPaint(e);
        }
    }

    /// <summary>
    /// 表示是否填充
    /// </summary>
    public enum FullOrKong
    {
        /// <summary>
        /// 填充
        /// </summary>
        Full = 0,
        /// <summary>
        /// 不填充
        /// </summary>
        Kong = 1
    }
}
