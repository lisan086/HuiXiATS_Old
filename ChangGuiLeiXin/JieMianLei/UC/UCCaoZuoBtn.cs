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
    public class UCCaoZuoBtn : Button
    {
        private GouKouCha _GouKouCha = GouKouCha.ZuiXiao;
        public UCCaoZuoBtn()
            : base()
        {

            this.DoubleBuffered = true;
            this.Text = "";
        }

        private Color _ZColor = Color.Black;
        public GouKouCha GouKouCha
        {
            get { return _GouKouCha; }
            set { _GouKouCha = value; this.Refresh(); }
        }

        public Color ZColor
        {
            get
            {
                return _ZColor;
            }

            set
            {
                _ZColor = value;
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            try
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                int julix = 10;
                int jiliy = 10;
                switch (_GouKouCha)
                {
                    case GouKouCha.ZuiXiao:
                        {
                            float zhongxiny = (float)(this.Height) / 2f;
                            Pen huabi = new Pen(_ZColor, 1.5f);
                            PointF pointF1 = new PointF(julix, zhongxiny - huabi.Width / 2);
                            PointF pointF2 = new PointF(this.Width - julix, zhongxiny - huabi.Width / 2);
                            pevent.Graphics.DrawLine(huabi, pointF1, pointF2);
                            huabi.Dispose();
                        }
                        break;
                    case GouKouCha.ZuiDa:
                        {
                            float zhongbux = (float)(this.Width) / 2f;
                            float zhongxiny = (float)(this.Height) / 2f;
                            Pen huabi = new Pen(_ZColor, 1.5f);
                            pevent.Graphics.DrawRectangle(huabi, julix, jiliy, this.Width - 2 * julix, this.Height - 2 * jiliy);
                            huabi.Dispose();
                        }
                        break;
                    case GouKouCha.GuanBi:
                        {
                            float zhongbux = (float)(this.Width) / 2f;
                            float zhongxiny = (float)(this.Height) / 2f;
                            Pen huabi = new Pen(_ZColor, 1.5f);
                            pevent.Graphics.DrawLine(huabi, julix, jiliy, this.Width - julix, this.Height - jiliy);
                            pevent.Graphics.DrawLine(huabi, this.Width - julix, jiliy, julix, this.Height - jiliy);
                            huabi.Dispose();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }

        }
    }
    public enum GouKouCha
    {
        ZuiXiao = 0,
        ZuiDa = 1,
        GuanBi = 2
    }
}
