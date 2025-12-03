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

namespace BaseUI.UC.ZuHeKJ
{
    public delegate void DianJi1(ZiDianButton ziDianButton, int leixing);
    [DesignTimeVisible(false)]
    public  partial class ZiDianButton : UserControl
    {
        public event DianJi1 DianJiEvent;
        private bool JinRu = false;
        private Color Color = Color.White;
        private bool JinRukj = false;
        public ZiDianButton()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.DoubleBuffered = true;
            Color = this.BackColor;
            this.Paint += ZiDianButton_Paint;
            this.MouseEnter += ZiDianButton_MouseEnter;
            this.MouseLeave += ZiDianButton_MouseLeave;
            this.MouseMove += ZiDianButton_MouseMove;
            this.Click += ZiDianButton_Click;
        }
        private int JiLuKuan = 20;
        private Color _ZiTiColor = Color.Black;

        private float _ZiTiDaXiao = 12f;

        private string _CaiDanName = "菜单";
        private Rectangle youbian = new Rectangle();
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

        public float ZiTiDaXiao
        {
            get
            {
                return _ZiTiDaXiao;
            }

            set
            {
                _ZiTiDaXiao = value;
                this.Refresh();
            }
        }

        public string CaiDanName
        {
            get
            {
                return _CaiDanName;
            }

            set
            {
                _CaiDanName = value;
                this.Refresh();
            }
        }

        private void ZiDianButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Font ziti = new Font("微软雅黑", _ZiTiDaXiao);

            Rectangle zuobian = new Rectangle();
            zuobian.X = 0;
            zuobian.Y = 0;
            zuobian.Width = (int)(this.Width - JiLuKuan);
            zuobian.Height = this.Height;
            #region 下区域
            SolidBrush solidBrush = new SolidBrush(_ZiTiColor);
            StringFormat stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(_CaiDanName, ziti, solidBrush, zuobian, stringFormat);

            #endregion
            #region 上区域

            youbian.X = this.Width - JiLuKuan;
            youbian.Y = (this.Height) / 2 - JiLuKuan / 2;
            youbian.Width = JiLuKuan - 5;
            youbian.Height = JiLuKuan - 5;

            Pen huabi = new Pen(Color.Red, 1.2f);
            if (JinRukj)
            {
                e.Graphics.DrawLine(huabi, youbian.X, youbian.Y, youbian.X + youbian.Width, youbian.Y + youbian.Height);
                e.Graphics.DrawLine(huabi, youbian.X + youbian.Width, youbian.Y, youbian.X, youbian.Y + youbian.Height);
            }
            if (JinRu)
            {
                e.Graphics.DrawRectangle(huabi, youbian);


            }
            huabi.Dispose();
            ziti.Dispose();
            solidBrush.Dispose();
            stringFormat.Dispose();
            #endregion

        }


        private void ZiDianButton_Click(object sender, EventArgs e)
        {
            bool iszai = IsZaiZheLi1();

            if (DianJiEvent != null)
            {
                int leixing = iszai ? 1 : 2;
                DianJiEvent(this, leixing);
            }

        }

        private void ZiDianButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (JinRu == false)
            {
                bool iszai = IsZaiZheLi1();
                if (iszai)
                {
                    JinRu = true;
                    this.Refresh();
                }


            }
            else
            {
                bool iszai = IsZaiZheLi1();
                if (iszai == false)
                {
                    JinRu = false;
                    this.Refresh();
                }
            }


        }

        private void ZiDianButton_MouseLeave(object sender, EventArgs e)
        {
            // this.BackColor = Color;
            if (JinRu)
            {
                JinRu = false;

            }
            if (JinRukj)
            {
                JinRukj = false;

            }
            if (JinRu == false || JinRukj == false)
            {
                this.Refresh();
            }
        }

        private void ZiDianButton_MouseEnter(object sender, EventArgs e)
        {
            // this.BackColor = Color.LightBlue;
            if (JinRu == false)
            {
                bool iszai = IsZaiZheLi1();
                if (iszai)
                {
                    JinRu = true;

                }
            }
            if (JinRukj == false)
            {
                JinRukj = true;
            }
            if (JinRu || JinRukj)
            {
                this.Refresh();
            }
        }
        private bool IsZaiZheLi1()
        {
            Point shuibiao = Control.MousePosition;
            Point zhuanhuan = this.PointToClient(shuibiao);

            if (youbian.Contains(zhuanhuan))
            {
                return true;
            }
            return false;
        }
    }
}
