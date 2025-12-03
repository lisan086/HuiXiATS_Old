using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.UC
{
    public class UCPanL : Panel
    {

        public UCPanL()
           : base()
        {
            this.DoubleBuffered = true;
          
        }

        private bool _IsKongZhi = true;
        private Color _TColor = Color.FromArgb(14, 78, 175);

        public Color TColor
        {
            get { return _TColor; }
            set
            {
                _TColor = value;
                this.Refresh();
            }
        }

        private Color _FColor = Color.Transparent;
        public Color FColor
        {
            get { return _FColor; }
            set
            {
                _FColor = value;
                this.Refresh();
            }
        }

        public bool IsKongZhi
        {
            get
            {
                return _IsKongZhi;
            }

            set
            {
                _IsKongZhi = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_IsKongZhi)
            {
                Graphics g = e.Graphics;
                try
                {
                    Brush b = new LinearGradientBrush(this.ClientRectangle, _FColor, _TColor, LinearGradientMode.Vertical);
                    g.FillRectangle(b, this.ClientRectangle);
                    b.Dispose();
                }
                catch
                {


                }
            }

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (_IsKongZhi)
            {
             
                this.Invalidate();
            }
        }
    }
}
