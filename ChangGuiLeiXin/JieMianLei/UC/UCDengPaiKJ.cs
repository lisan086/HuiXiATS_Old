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
    public partial class UCDengPaiKJ : UserControl
    {
        public UCDengPaiKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private string ShangBiaoTi = "";
        private Color ShangColor = Color.Black;
        private string XiaBiaoTi = "";
        private Color XiaoColor = Color.Black;
        private float ShangDaXiao = 12f;
        private float XiaDengDaXiao = 12f;

        private void UCDengPaiKJ_Load(object sender, EventArgs e)
        {
            this.Paint += UCDengPaiKJ_Paint;
        }

        private void UCDengPaiKJ_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            float gaodu = ShangDaXiao * 2 + 5f;
            RectangleF shangkuang = new RectangleF();
            shangkuang.X = 2;
            shangkuang.Y = 2;
            shangkuang.Width = this.Width - 4;
            shangkuang.Height = gaodu;
            SolidBrush huashua = new SolidBrush(ShangColor);
            StringFormat stringAlignment = new StringFormat();
            stringAlignment.Alignment = StringAlignment.Center;
            stringAlignment.LineAlignment = StringAlignment.Center;
            using (Font ziiti = new Font("微软姚黑", ShangDaXiao))
            {
                e.Graphics.DrawString(ShangBiaoTi, ziiti, huashua, shangkuang, stringAlignment);
            }
            float duichu = 3;
            using (Pen huabi=new Pen(ShangColor,3))
            {
                e.Graphics.DrawLine(huabi,0, shangkuang.Y+ shangkuang.Height+ duichu, shangkuang.X+shangkuang.Width, shangkuang.Y + shangkuang.Height + duichu);
            }
            RectangleF xiakuang = new RectangleF();
            xiakuang.X = 2;
            xiakuang.Y = shangkuang.Y + shangkuang.Height + duichu;
            xiakuang.Width = this.Width - 4;
            xiakuang.Height = this.Height-(shangkuang.Y + shangkuang.Height + duichu);
            huashua.Color = XiaoColor;
            using (Font ziiti = new Font("微软姚黑", XiaDengDaXiao))
            {
                e.Graphics.DrawString(XiaBiaoTi, ziiti, huashua, xiakuang, stringAlignment);
            }
            huashua.Dispose();
            stringAlignment.Dispose();
        }

        public void SetCanShu(string shangbiaoti,string xiabiaoti,Color shangyanse,Color xiacolor,float shangdaxiao=12,float xiadaxiao=12)
        {
            ShangBiaoTi = shangbiaoti;
            XiaBiaoTi = xiabiaoti;
            ShangColor = shangyanse;
            XiaoColor = xiacolor;
            ShangDaXiao = shangdaxiao;
            XiaDengDaXiao = xiadaxiao;
            this.Invalidate();
        }
    }
}
