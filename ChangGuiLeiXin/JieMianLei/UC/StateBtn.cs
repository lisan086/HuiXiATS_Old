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
    public class StateBtn : Button
    {
        public event Action<object, KaiState> DianJiClickEvent;
        /// <summary>
        /// 构造函数
        /// </summary>
        public StateBtn()
            : base()
        {
            this.DoubleBuffered = true;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Click += AnNiuBtn_Click;
            this.Paint += AnNiuBtn_Paint;
        }
        private void AnNiuBtn_Paint(object sender, PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            pevent.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            StringFormat stringAlignment = new StringFormat();
            stringAlignment.Alignment = StringAlignment.Center;
            stringAlignment.LineAlignment = StringAlignment.Center;
            Font ziti = new Font("微软姚黑", _ZiTiDaoXiao);
            SolidBrush solidBrush = new SolidBrush(_ZiTiColor);
            SolidBrush solidBrushkuai = new SolidBrush(_KuanColor);
            if (_IsShangZuo)
            {
                RectangleF1.X = 1;
                RectangleF1.Y = 1;
                RectangleF1.Width = (this.Width / 2 - 2);
                RectangleF1.Height = (this.Height - 2);

                RectangleF2.X = this.Width / 2 + 1;
                RectangleF2.Y = 1;
                RectangleF2.Width = (this.Width / 2 - 2);
                RectangleF2.Height = (this.Height - 2);
            }
            else
            {
                RectangleF1.X = 1;
                RectangleF1.Y = 1;
                RectangleF1.Width = (this.Width - 2);
                RectangleF1.Height = (this.Height / 2 - 2);

                RectangleF2.X = 1;
                RectangleF2.Y = this.Height / 2 + 1;
                RectangleF2.Width = (this.Width - 2);
                RectangleF2.Height = (this.Height / 2 - 2);
            }
            if (_kaiState == KaiState.Open)
            {
                pevent.Graphics.FillRectangle(solidBrushkuai, RectangleF1);
            }
            else if (_kaiState == KaiState.Close)
            {
                pevent.Graphics.FillRectangle(solidBrushkuai, RectangleF2);
            }
            pevent.Graphics.DrawString(_JieShuStr, ziti, solidBrush, RectangleF2, stringAlignment);
            pevent.Graphics.DrawString(_KaiShiStr, ziti, solidBrush, RectangleF1, stringAlignment);
            ziti.Dispose();
            solidBrush.Dispose();
            stringAlignment.Dispose();
        }

        private void AnNiuBtn_Click(object sender, EventArgs e)
        {
        
            bool iszai1 = IsZaiZheLi1();
            bool iszai2 = IsZaiZheLi2();
            if (iszai1)
            {
                if (_kaiState == KaiState.Close)
                {
                    KaiState = KaiState.Open;
                }
                if (DianJiClickEvent != null)
                {
                    DianJiClickEvent(this, KaiState.Open);
                }
                
              
            }
            else
            {
                if (iszai2)
                {
                    if (_kaiState == KaiState.Open)
                    {

                        KaiState = KaiState.Close;
                    }

                    if (DianJiClickEvent != null)
                    {
                        DianJiClickEvent(this, KaiState.Close);
                    }
                   
                }
            }
           
        }
        private bool _IsShangZuo = true;
        private string _KaiShiStr = "打开";
        private string _JieShuStr = "关闭";
        private KaiState _kaiState = KaiState.Close;
        private RectangleF RectangleF1 = new RectangleF();
        private RectangleF RectangleF2 = new RectangleF();
        private float _ZiTiDaoXiao = 9;
        private Color _ZiTiColor = Color.Black;
        private Color _KuanColor = Color.Green;
        public bool IsShangZuo
        {
            get
            {
                return _IsShangZuo;
            }

            set
            {
                _IsShangZuo = value;
                this.Refresh();
            }
        }
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = "";
            }
        }

        public string OpenStr
        {
            get
            {
                return _KaiShiStr;
            }

            set
            {
                _KaiShiStr = value;
                this.Refresh();
            }
        }

        public string CloseStr
        {
            get
            {
                return _JieShuStr;
            }

            set
            {
                _JieShuStr = value;
                this.Refresh();
            }
        }

        public KaiState KaiState
        {
            get
            {
                return _kaiState;
            }

            set
            {
                _kaiState = value;
                this.Refresh();
            }
        }

        public float ZiTiDaoXiao
        {
            get
            {
                return _ZiTiDaoXiao;
            }

            set
            {
                _ZiTiDaoXiao = value;
                this.Refresh();
            }
        }

        public Color ZiTiColor
        {
            get
            {
                return _ZiTiColor;
            }

            set
            {
                _ZiTiColor = value;
                this.Refresh();
            }
        }

        public Color KuanColor
        {
            get
            {
                return _KuanColor;
            }

            set
            {
                _KuanColor = value;
                this.Refresh();
            }
        }

        private bool IsZaiZheLi1()
        {
            Point shuibiao = Control.MousePosition;
            Point zhuanhuan = this.PointToClient(shuibiao);

            if (RectangleF1.Contains(zhuanhuan))
            {
                return true;
            }
            return false;
        }
        private bool IsZaiZheLi2()
        {
            Point shuibiao = Control.MousePosition;
            Point zhuanhuan = this.PointToClient(shuibiao);

            if (RectangleF2.Contains(zhuanhuan))
            {
                return true;
            }
            return false;
        }
    }
    public enum KaiState
    {
        Open = 0,
        Close = 1
    }
}
