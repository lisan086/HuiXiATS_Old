using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BaseUI.UC
{
    [Designer(typeof(LayoutPanelDesigner))]
    [ToolboxBitmap(typeof(ShenSuoKJ))]
    public class ShenSuoKJ : Panel
    {
        private System.ComponentModel.Container components = null;
        public ShenSuoKJ()
        {
            components = new System.ComponentModel.Container();
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Dock = DockStyle.Right;
            GaiBianBtnRect();
            GaiBianPadding();
        }
        #region Properties
        private Color _arrowColor = Color.White;
        private int _controlSize = 20;
        private Color _buttonColor = Color.FromArgb(55, 55, 55);
        private Color _hoverColor = Color.Orange;
        private States _state = States.Open;
        private int _memorySize = 50;
        private bool _hover = false;
        private bool _down = false;

        public override DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                base.Dock = value == DockStyle.Left || value == DockStyle.Right || value == DockStyle.Bottom || value == DockStyle.Top ? value : Dock;
                GaiBianPadding();
                GaiBianBtnRect();
                Invalidate();
            }
        }

        public int ButtonSize
        {
            get
            {
                return _controlSize;
            }
            set
            {
                _controlSize = value >= 20 ? value : 20;
                changeSize();
                Invalidate();
            }
        }
        public Color ArrowColor
        {
            get { return _arrowColor; }
            set { _arrowColor = value; Invalidate(); }
        }
        public Color ButtonColor
        {
            get { return _buttonColor; }
            set { _buttonColor = value; Invalidate(); }
        }
        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; Invalidate(buttonRect); }
        }
        private bool Hover
        {
            get { return _hover; }
            set
            {
                _hover = value;
                if (_hover)
                    Cursor = Cursors.Hand;
                else
                    Cursor = Cursors.Default;
                Invalidate(buttonRect);
            }
        }
        private bool Down
        {
            get { return _down; }
            set { _down = value; }
        }
        [Browsable(false)]
        public int MemorySize
        {
            get { return _memorySize; }
            set { _memorySize = value; }
        }
        public States State
        {
            get { return _state; }
            set
            {
                _state = value;
                changeSize();
                Invalidate(buttonRect);
            }
        }
        public enum States
        { Open, Close };
        #endregion


        Rectangle buttonRect;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            if (Dock == DockStyle.Left || Dock == DockStyle.Right)
            {
                HuiZhiZuoYou(G);
            }
            else
            {
                HuiZhiShangXia(G);
            }

        }
        private void HuiZhiZuoYou(Graphics G)
        {
            //绘制虚线框
            if (Site != null)
            {
                using (Pen pen = new Pen(Color.Blue))
                {
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 5, 5 };
                    if (Dock == DockStyle.Left)
                    {
                        G.DrawRectangle(pen, new Rectangle(Padding.Left, Top, Width - Padding.Right - 1, Height - Padding.Bottom - 1));
                    }
                    else
                    {
                        G.DrawRectangle(pen, new Rectangle(Padding.Left, Top, Width - Padding.Left - 1, Height - Padding.Bottom - 1));
                    }
                }
            }

            Rectangle circleRect = new Rectangle(buttonRect.X + 1, buttonRect.Y + 1, buttonRect.Width - 2, buttonRect.Height - 2);
            using (LinearGradientBrush lb = new LinearGradientBrush(Dock == DockStyle.Left ? buttonRect :
                new Rectangle(buttonRect.X, buttonRect.Y, buttonRect.Width + 1, buttonRect.Height), BackColor, Color.FromArgb(180, _buttonColor), 0F))
            {
                lb.SetBlendTriangularShape(0.5F);
                using (GraphicsPath GP = CreatePath())
                    G.FillPath(lb, GP);
            }
            using (SolidBrush sb = new SolidBrush(ControlPaint.Dark(_buttonColor, 0.2F)))
                G.FillEllipse(sb, buttonRect);
            using (SolidBrush sb = new SolidBrush(_buttonColor))
            {
                G.FillEllipse(sb, circleRect);
            }
            if (Dock == DockStyle.Left || Dock == DockStyle.Right)
            {
                DrawLeftRight(G, circleRect);
            }
        }

        private void HuiZhiShangXia(Graphics G)
        {
            //绘制虚线框
            if (Site != null)
            {
                using (Pen pen = new Pen(Color.Blue))
                {
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 5, 5 };
                    if (Dock == DockStyle.Bottom)
                    {
                        G.DrawRectangle(pen, new Rectangle(Padding.Left, Padding.Top, Width - Padding.Right - 1, Height - Padding.Top - 1));
                    }
                    else
                    {
                        G.DrawRectangle(pen, new Rectangle(Padding.Left, Padding.Top, Width - Padding.Right - 1, Height - Padding.Bottom - 1));
                    }
                }
            }

            Rectangle circleRect = new Rectangle(buttonRect.X + 1, buttonRect.Y + 1, buttonRect.Width - 2, buttonRect.Height - 2);
            using (LinearGradientBrush lb = new LinearGradientBrush(Dock == DockStyle.Bottom ? buttonRect :
                new Rectangle(buttonRect.X, buttonRect.Y, buttonRect.Width + 1, buttonRect.Height), BackColor, Color.FromArgb(180, _buttonColor), 0F))
            {
                lb.SetBlendTriangularShape(0.5F);
                using (GraphicsPath GP = CreatePathSX())
                {
                    G.FillPath(lb, GP);
                }
            }
            using (SolidBrush sb = new SolidBrush(ControlPaint.Dark(_buttonColor, 0.2F)))
            {
                G.FillEllipse(sb, buttonRect);
            }
            using (SolidBrush sb = new SolidBrush(_buttonColor))
            {
                G.FillEllipse(sb, circleRect);
            }
            if (Dock == DockStyle.Bottom || Dock == DockStyle.Top)
            {
                DrawLeftRightSX(G, circleRect);
            }
        }
        private void DrawLeftRight(Graphics G, Rectangle circleRect)
        {
            using (Pen pen = new Pen(_hover ? _hoverColor : _arrowColor, 2))
            {
                if (Dock == DockStyle.Left)
                    if (State == States.Open)
                    {
                        G.DrawLine(pen, circleRect.X + circleRect.Width / 8 + circleRect.Width / 4, circleRect.Y + 1 + circleRect.Height / 2,
                               circleRect.X + circleRect.Width / 8 + circleRect.Width / 2, circleRect.Y + 1 + circleRect.Height / 4);
                        G.DrawLine(pen, circleRect.X + circleRect.Width / 8 + circleRect.Width / 4, circleRect.Y + circleRect.Height / 2,
                            circleRect.X + circleRect.Width / 8 + circleRect.Width / 2, circleRect.Y + circleRect.Height - circleRect.Height / 4);
                    }
                    else
                    {
                        G.DrawLine(pen, circleRect.X + circleRect.Width / 2 - circleRect.Width / 8, circleRect.Y + 1 + circleRect.Height / 4,
                              circleRect.X - circleRect.Width / 8 + circleRect.Width - circleRect.Width / 4, circleRect.Y + 1 + circleRect.Height / 2);
                        G.DrawLine(pen, circleRect.X + circleRect.Width / 2 - circleRect.Width / 8, circleRect.Y + circleRect.Height - circleRect.Height / 4,
                               circleRect.X - circleRect.Width / 8 + circleRect.Width - circleRect.Width / 4, circleRect.Y + circleRect.Height / 2);
                    }
                else
                if (State == States.Close)
                {
                    G.DrawLine(pen, circleRect.X + circleRect.Width / 8 + circleRect.Width / 4, circleRect.Y + 1 + circleRect.Height / 2,
                           circleRect.X + circleRect.Width / 8 + circleRect.Width / 2, circleRect.Y + 1 + circleRect.Height / 4);
                    G.DrawLine(pen, circleRect.X + circleRect.Width / 8 + circleRect.Width / 4, circleRect.Y + circleRect.Height / 2,
                        circleRect.X + circleRect.Width / 8 + circleRect.Width / 2, circleRect.Y + circleRect.Height - circleRect.Height / 4);
                }
                else
                {
                    G.DrawLine(pen, circleRect.X + circleRect.Width / 2 - circleRect.Width / 8, circleRect.Y + 1 + circleRect.Height / 4,
                          circleRect.X - circleRect.Width / 8 + circleRect.Width - circleRect.Width / 4, circleRect.Y + 1 + circleRect.Height / 2);
                    G.DrawLine(pen, circleRect.X + circleRect.Width / 2 - circleRect.Width / 8, circleRect.Y + circleRect.Height - circleRect.Height / 4,
                           circleRect.X - circleRect.Width / 8 + circleRect.Width - circleRect.Width / 4, circleRect.Y + circleRect.Height / 2);
                }
            }
        }

        private void DrawLeftRightSX(Graphics G, Rectangle circleRect)
        {
            using (Pen pen = new Pen(_hover ? _hoverColor : _arrowColor, 2))
            {
                if (Dock == DockStyle.Top)
                {
                    if (State == States.Open)
                    {
                        G.DrawLine(pen, circleRect.X + 1 + circleRect.Width / 2, circleRect.Y + circleRect.Height / 8 + circleRect.Height / 4,
                           circleRect.X + 1 + circleRect.Width / 4, circleRect.Y + circleRect.Height / 8 + circleRect.Height / 2);

                        G.DrawLine(pen, circleRect.X + 1 + circleRect.Width / 2, circleRect.Y + circleRect.Height / 8 + circleRect.Height / 4,
                          circleRect.X + circleRect.Width - circleRect.Width / 4, circleRect.Y + circleRect.Height / 8 + circleRect.Height / 2);
                    }
                    else
                    {


                        G.DrawLine(pen, circleRect.X + 1 + circleRect.Width / 4, circleRect.Y + circleRect.Height / 2 - circleRect.Height / 8,
                        circleRect.X + 1 + circleRect.Width / 2, circleRect.Y - circleRect.Height / 8 + circleRect.Height - circleRect.Width / 4);

                        G.DrawLine(pen, circleRect.X + 1 + circleRect.Width - circleRect.Width / 4, circleRect.Y + circleRect.Height / 2 - circleRect.Height / 8,
                          circleRect.X + circleRect.Width / 2, circleRect.Y - circleRect.Height / 8 + circleRect.Height - circleRect.Width / 4);
                    }
                }
                else
                {
                    if (State == States.Close)
                    {
                        G.DrawLine(pen, circleRect.X + circleRect.Width / 2, circleRect.Y + circleRect.Height / 8 + circleRect.Height / 4,
                   circleRect.X + circleRect.Width / 4, circleRect.Y + circleRect.Height / 8 + circleRect.Width / 2);

                        G.DrawLine(pen, circleRect.X + circleRect.Width / 2, circleRect.Y + circleRect.Height / 8 + circleRect.Height / 4,
                          circleRect.X + circleRect.Width - circleRect.Width / 4, circleRect.Y + circleRect.Height / 8 + circleRect.Height / 2);
                    }
                    else
                    {


                        G.DrawLine(pen, circleRect.X + 1 + circleRect.Width / 4, circleRect.Y + circleRect.Height / 2 - circleRect.Height / 8,
               circleRect.X + 1 + circleRect.Width / 2, circleRect.Y - circleRect.Height / 8 + circleRect.Width - circleRect.Width / 4);

                        G.DrawLine(pen, circleRect.X + circleRect.Width - circleRect.Width / 4, circleRect.Y - circleRect.Height / 8 + circleRect.Height / 2,
                          circleRect.X + circleRect.Width / 2, circleRect.Y - circleRect.Height / 8 + circleRect.Height - circleRect.Height / 4);
                    }
                }
            }
        }
        private GraphicsPath CreatePath()
        {
            GraphicsPath GP = new GraphicsPath();
            if (Dock == DockStyle.Left)
                GP.AddLines(new PointF[] {new PointF(Width-_controlSize,Height/2-2*_controlSize),
            new Point(Width-_controlSize/2,Height/2-_controlSize),new PointF(Width-_controlSize/2,Height/2+_controlSize)
            ,new PointF(Width-_controlSize,Height/2+2*_controlSize),new PointF(Width-_controlSize,Height/2-2*_controlSize)});
            else if (Dock == DockStyle.Right)
                GP.AddLines(new PointF[] {new PointF(_controlSize,Height/2-2*_controlSize),
            new Point(_controlSize/2,Height/2-_controlSize),new PointF(_controlSize/2,Height/2+_controlSize)
            ,new PointF(_controlSize,Height/2+2*_controlSize),new PointF(_controlSize,Height/2-2*_controlSize)});
            return GP;
        }

        private GraphicsPath CreatePathSX()
        {
            GraphicsPath GP = new GraphicsPath();
            if (Dock == DockStyle.Bottom)
            {
                GP.AddLines(new PointF[] {new PointF(Width/2-2*  _controlSize,_controlSize),
            new Point(Width/2-  _controlSize,_controlSize/2),new PointF(Width/2+_controlSize,_controlSize/2)
            ,new PointF(Width/2+2*  _controlSize,_controlSize),new PointF(Width/2-2*  _controlSize,_controlSize)});
            }
            else
            {
                GP.AddLines(new PointF[] {new PointF(Width/2-2*  _controlSize,Height-_controlSize),
            new Point(Width/2-  _controlSize,Height-_controlSize/2),new PointF(Width/2+_controlSize,Height-_controlSize/2)
            ,new PointF(Width/2+2*  _controlSize,Height-_controlSize),new PointF(Width/2-2*  _controlSize,Height-_controlSize)});
            }
            return GP;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.X > buttonRect.X && e.X < buttonRect.X + buttonRect.Width && e.Y > buttonRect.Y && e.Y < buttonRect.Y + buttonRect.Height)
            {
                if (!Down)
                    Down = true;
            }
            else { if (Down) Down = false; }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Hover = false;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Down = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.X > buttonRect.X && e.X < buttonRect.X + buttonRect.Width && e.Y > buttonRect.Y && e.Y < buttonRect.Y + buttonRect.Height)
            {
                if (!Hover)
                    Hover = true;
            }
            else { if (Hover) Hover = false; }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Hover && Down)
                State = _state == States.Open ? States.Close : States.Open;
        }
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            JiaoYanChiCun();
            GaiBianBtnRect();
            Invalidate();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 
        private void changeSize()
        {
            GaiBianPadding();
            if (State == States.Close)
            {
                if (Dock == DockStyle.Left)
                {
                    Size = new Size(_controlSize, Height);

                }
                else if (Dock == DockStyle.Right)
                {
                    Size = new Size(_controlSize, Height);

                }
                else if (Dock == DockStyle.Bottom)
                {
                    Size = new Size(Width, _controlSize);
                }
                else if (Dock == DockStyle.Top)
                {
                    Size = new Size(Width, _controlSize);
                }
            }
            else
            {
                if (Dock == DockStyle.Left)
                {

                    Size = new Size(_memorySize, Height);
                }
                else if (Dock == DockStyle.Right)
                {
                    Size = new Size(MemorySize, Height);

                }
                else if (Dock == DockStyle.Bottom)
                {
                    Size = new Size(Width, MemorySize);
                }
                else if (Dock == DockStyle.Top)
                {
                    Size = new Size(Width, MemorySize);
                }
            }
        }
        private void JiaoYanChiCun()
        {
            if (Dock == DockStyle.Left || Dock == DockStyle.Right)
            {
                if (State != States.Close && this.Width > _controlSize)
                {
                    MemorySize = Width;
                }
            }
            else
            {
                if (State != States.Close && this.Height > _controlSize)
                {
                    MemorySize = Height;
                }
            }
        }
        private void GaiBianBtnRect()
        {
            if (Dock == DockStyle.Left)
            {
                if (_state == States.Open)
                {

                    buttonRect = new Rectangle(Width - _controlSize, Height / 2 - _controlSize / 2, _controlSize - 1, _controlSize - 1);
                }
                else
                {

                    buttonRect = new Rectangle(0, Height / 2 - _controlSize / 2, _controlSize - 1, _controlSize - 1);
                }
            }
            else if (Dock == DockStyle.Right)
            {
                if (_state == States.Open)
                {

                    buttonRect = new Rectangle(0, Height / 2 - _controlSize / 2, _controlSize - 1, _controlSize - 1);
                }
                else
                {

                    buttonRect = new Rectangle(0, Height / 2 - _controlSize / 2, _controlSize - 1, _controlSize - 1);
                }
            }
            else if (Dock == DockStyle.Bottom)
            {
                if (_state == States.Open)
                {

                    buttonRect = new Rectangle(Width / 2 - _controlSize / 2, 0, _controlSize - 1, _controlSize - 1);
                }
                else
                {

                    buttonRect = new Rectangle(Width / 2 - _controlSize / 2, 0, _controlSize - 1, _controlSize - 1);
                }
            }
            else if (Dock == DockStyle.Top)
            {
                if (_state == States.Open)
                {

                    buttonRect = new Rectangle(Width / 2 - _controlSize / 2, Height - _controlSize, _controlSize - 1, _controlSize - 1);
                }
                else
                {

                    buttonRect = new Rectangle(Width / 2 - _controlSize / 2, 0, _controlSize - 1, _controlSize - 1);
                }
            }
        }

        private void GaiBianPadding()
        {
            if (Dock == DockStyle.Left)
            {
                if (_state == States.Open)
                {
                    Padding = new Padding(0, 0, _controlSize, 0);

                }
                else
                {
                    Padding = new Padding(_controlSize, 0, 0, 0);

                }
            }
            else if (Dock == DockStyle.Right)
            {
                if (_state == States.Open)
                {
                    Padding = new Padding(_controlSize, 0, 0, 0);

                }
                else
                {
                    Padding = new Padding(_controlSize, 0, 0, 0);

                }
            }
            else if (Dock == DockStyle.Bottom)
            {
                if (_state == States.Open)
                {
                    Padding = new Padding(0, _controlSize, 0, 0);

                }
                else
                {
                    Padding = new Padding(0, _controlSize, 0, 0);

                }
            }
            else if (Dock == DockStyle.Top)
            {
                if (_state == States.Open)
                {
                    Padding = new Padding(0, 0, 0, _controlSize);

                }
                else
                {
                    Padding = new Padding(0, _controlSize, 0, 0);

                }
            }
        }
        #endregion
    }

    public class LayoutPanelDesigner : ParentControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                if ((Control as ShenSuoKJ).State == ShenSuoKJ.States.Close)
                    return System.Windows.Forms.Design.SelectionRules.None;//让容器在关闭的状态下不可以被调整
                else
                    return SelectionRules.AllSizeable;
            }
        }
        protected override bool GetHitTest(Point point)
        {
            ShenSuoKJ lay = base.Control as ShenSuoKJ;//得到宿主控件
            Point e = lay.PointToClient(point);//得到鼠标的坐标
            Rectangle buttonRect = new Rectangle(lay.Width - lay.ButtonSize, lay.Height / 2 - lay.ButtonSize / 2, lay.ButtonSize - 1, lay.ButtonSize - 1);//定义可以点击的按钮区域
            if (lay.Dock == DockStyle.Left)
            {
                if (lay.State == ShenSuoKJ.States.Open)
                    buttonRect = new Rectangle(lay.Width - lay.ButtonSize, lay.Height / 2 - lay.ButtonSize / 2, lay.ButtonSize - 1, lay.ButtonSize - 1);
                else
                    buttonRect = new Rectangle(0, lay.Height / 2 - lay.ButtonSize / 2, lay.ButtonSize - 1, lay.ButtonSize - 1);
            }
            else if (lay.Dock == DockStyle.Right)
            {
                if (lay.State == ShenSuoKJ.States.Open)
                    buttonRect = new Rectangle(0, lay.Height / 2 - lay.ButtonSize / 2, lay.ButtonSize - 1, lay.ButtonSize - 1);
                else
                    buttonRect = new Rectangle(0, lay.Height / 2 - lay.ButtonSize / 2, lay.ButtonSize - 1, lay.ButtonSize - 1);
            }
            else if (lay.Dock == DockStyle.Bottom)
            {
                if (lay.State == ShenSuoKJ.States.Open)
                    buttonRect = new Rectangle(lay.Width / 2 - lay.ButtonSize / 2, 0, lay.ButtonSize - 1, lay.ButtonSize - 1);
                else
                    buttonRect = new Rectangle(lay.Width / 2 - lay.ButtonSize / 2, 0, lay.ButtonSize - 1, lay.ButtonSize - 1);
            }
            else if (lay.Dock == DockStyle.Top)
            {
                if (lay.State == ShenSuoKJ.States.Open)
                    buttonRect = new Rectangle(lay.Width / 2 - lay.ButtonSize / 2, lay.Height - lay.ButtonSize, lay.ButtonSize - 1, lay.ButtonSize - 1);
                else
                    buttonRect = new Rectangle(lay.Width / 2 - lay.ButtonSize / 2, 0, lay.ButtonSize - 1, lay.ButtonSize - 1);
            }
            if (e.X > buttonRect.X && e.X < buttonRect.X + buttonRect.Width && e.Y > buttonRect.Y && e.Y < buttonRect.Y + buttonRect.Height)
            {
                if (isMouseDown)
                {
                    lay.State = lay.State == ShenSuoKJ.States.Close ? ShenSuoKJ.States.Open : ShenSuoKJ.States.Close;
                    isMouseDown = false;
                }
            }
            else
            { isMouseDown = false; }
            return false;
        }
        bool isMouseDown = false;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0202)
            { isMouseDown = true; }
            base.WndProc(ref m);
        }
    }
}
