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

namespace JieMianLei.UC
{
    public partial class HeJiKJ : UserControl
    {
        private Dictionary<string, string> ShuJu = new Dictionary<string, string>();
        public HeJiKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Load += UCXinHeJiKongJian_Load;
        }
        private void UCXinHeJiKongJian_Load(object sender, EventArgs e)
        {
            this.Paint += UCXinHeJiKongJian_Paint;
        }

        private void UCXinHeJiKongJian_Paint(object sender, PaintEventArgs e)
        {
            Graphics huabu = e.Graphics;
            huabu.CompositingQuality = CompositingQuality.HighQuality;
            huabu.SmoothingMode = SmoothingMode.HighQuality;
            List<string> keys = ShuJu.Keys.ToList();
            if (keys.Count > 0)
            {
                StringFormat stringAlignment = new StringFormat();
                stringAlignment.Alignment = StringAlignment.Center;
                stringAlignment.LineAlignment = StringAlignment.Center;
                Font ziti = this.Font;
                SolidBrush huashua = new SolidBrush(this.ForeColor);
                int zkuang = 0;
                int yigezidaxiao = (int)(ziti.Size + 3);
                for (int i = 0; i < keys.Count; i++)
                {
                    string luss = string.Format("{0}:{1}", keys[i], ShuJu[keys[i]]);
                    int kuang = yigezidaxiao * luss.Length;
                    RectangleF rectangle = new RectangleF();
                    rectangle.X = zkuang;
                    rectangle.Y = 1;
                    rectangle.Width = kuang;
                    rectangle.Height = this.Height - 2;
                    huabu.DrawString(luss, ziti, huashua, rectangle, stringAlignment);
                    //  huabu.DrawRectangle(Pens.Black,rectangle.X, rectangle.Y,rectangle.Width,rectangle.Height);
                    zkuang += kuang + 2;
                }
                stringAlignment.Dispose();

                huashua.Dispose();
            }
        }





        public void SetKongJian(Dictionary<string, string> ZiDianShuJu)
        {
            if (ZiDianShuJu == null)
            {
                Clear();
                return;
            }
            ShuJu.Clear();
            foreach (var item in ZiDianShuJu.Keys)
            {
                ShuJu.Add(item, ZiDianShuJu[item]);
            }
            this.Invalidate();

        }

        public void Clear()
        {
            ShuJu.Clear();
            this.Invalidate();
        }
    }
}
