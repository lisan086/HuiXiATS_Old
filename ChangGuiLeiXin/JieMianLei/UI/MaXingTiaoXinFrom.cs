using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.UI
{
   
    public partial class MaXingTiaoXinFrom : Form
    {
        /// <summary>
        /// 马水平平移
        /// </summary>
        private int _MaShuiPingYi = 5;

        private int _JiCount = 1;
        private float ShangYiCi = 0;
        private RectangleF shangquyue;
        private string JiaZaiMiaoShu = "描述";
        /// <summary>
        /// 计时器
        /// </summary>
        private DateTime JiTimeQi = DateTime.Now;



        private Action Action;
      

        private bool ZongKaiGuan = true;
        private bool FenKaiGuan = false;

        private int BiaoZhi = 0;

        private Thread Thread1;
        private int TingLiuTime = 60 * 1000;
        public MaXingTiaoXinFrom()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            //  this.timer1.Enabled = false;
            shangquyue = new RectangleF();
           
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
                return cp;
            }
        }
        public int MaShuiPingYi
        {
            get
            {
                return _MaShuiPingYi;
            }

            set
            {
                _MaShuiPingYi = value;

            }
        }

        public int JiCount
        {
            get
            {
                return _JiCount;
            }

            set
            {
                _JiCount = value;
                this.Invalidate(new Rectangle((int)shangquyue.X, (int)shangquyue.Y, (int)shangquyue.Width, (int)shangquyue.Height));
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Plum, 0, 0, this.Width, this.Height / 2f);
            RectangleF shangquyue = new RectangleF();
            shangquyue.X = ShangYiCi + _MaShuiPingYi;
            shangquyue.Y = 1;
            shangquyue.Width = this.Width / 10;
            shangquyue.Height = this.Height / 2 - 1;
            ShangYiCi += _MaShuiPingYi;
            #region 画奔跑的马
            //float huabidaxiao = 2;
            //RectangleF yuan = new RectangleF();
            //float bangjing = (shangquyue.Height / 2f) * 2f / 3f;
            //yuan.X = shangquyue.X + (shangquyue.Width * 3f) / 4f;
            //yuan.Y = shangquyue.Y;
            //yuan.Width = bangjing;
            //yuan.Height = bangjing;
            //e.Graphics.FillEllipse(Brushes.Red, yuan);
            //PointF jingkaishidian = new PointF(yuan.X + yuan.Width / 2f, yuan.Y + yuan.Height);
            //PointF jingweidian = new PointF(yuan.X + yuan.Width / 2f, yuan.Y + yuan.Height + (shangquyue.Height / 2f) * 1f / 3f);
            //using (Pen pen = new Pen(Color.Red, huabidaxiao))
            //{
            //    e.Graphics.DrawLine(pen, jingkaishidian, jingweidian);
            //}

            //PointF shengkaishidian = new PointF(shangquyue.X + shangquyue.Width / 8f, yuan.Y + yuan.Height + (shangquyue.Height / 2f) * 1f / 3f);
            //PointF shengweidian = new PointF(yuan.X + yuan.Width / 2f, yuan.Y + yuan.Height + (shangquyue.Height / 2f) * 1f / 3f);
            //using (Pen pen = new Pen(Color.Red, huabidaxiao))
            //{
            //    e.Graphics.DrawLine(pen, shengkaishidian, shengweidian);
            //}
            //if (_JiCount % 2 == 0)
            //{
            //    PointF qianjiaokaishidian = new PointF(yuan.X, shengkaishidian.Y);
            //    PointF qianjiaoweidian = new PointF(yuan.X, shangquyue.Y + shangquyue.Height);

            //    PointF houjiaokaishidian = new PointF(shangquyue.X + shangquyue.Width / 4f, shengkaishidian.Y);
            //    PointF houjiaoweidian = new PointF(shangquyue.X + shangquyue.Width / 4f, shangquyue.Y + shangquyue.Height);
            //    using (Pen pen = new Pen(Color.Red, huabidaxiao))
            //    {
            //        e.Graphics.DrawLine(pen, qianjiaokaishidian, qianjiaoweidian);
            //        e.Graphics.DrawLine(pen, houjiaokaishidian, houjiaoweidian);
            //    }
            //}
            //else
            //{
            //    PointF qianjiaokaishidian = new PointF(yuan.X, shengkaishidian.Y);
            //    PointF qianjiaoweidian = new PointF(yuan.X + yuan.Width / 2f, shangquyue.Y + shangquyue.Height);

            //    PointF houjiaokaishidian = new PointF(shangquyue.X + shangquyue.Width / 4f, shengkaishidian.Y);
            //    PointF houjiaoweidian = new PointF(shangquyue.X + shangquyue.Width / 2f, shangquyue.Y + shangquyue.Height);
            //    using (Pen pen = new Pen(Color.Red, huabidaxiao))
            //    {
            //        e.Graphics.DrawLine(pen, qianjiaokaishidian, qianjiaoweidian);
            //        e.Graphics.DrawLine(pen, houjiaokaishidian, houjiaoweidian);
            //    }
            //}
            #endregion
            #region 画奔跑的马
          
            RectangleF yuan = new RectangleF();
            float bangjing = (shangquyue.Height / 2f) * 2f / 3f;
            yuan.X = shangquyue.X + (shangquyue.Width * 3f) / 4f;
            yuan.Y = shangquyue.Y;
            yuan.Width = bangjing;
            yuan.Height = bangjing;
            e.Graphics.FillEllipse(Brushes.SkyBlue, yuan);      
           
            if (_JiCount % 2 == 0)
            {
             
            }
            else
            {
              
            }
            #endregion
            #region 加载描述
            Font ZiTi1 = new Font("微软姚黑", 15);
            RectangleF wenzi = new RectangleF();
            wenzi.X = 0;
            wenzi.Y = this.Height / 2f;
            wenzi.Width = this.Width;
            wenzi.Height = this.Height / 2f;
            StringFormat stringAlignment = new StringFormat();
            stringAlignment.Alignment = StringAlignment.Near;
            stringAlignment.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString( string.Format("{0}({1}s)", JiaZaiMiaoShu, ((DateTime.Now - JiTimeQi).TotalMilliseconds/1000d).ToString("0.00")), ZiTi1, Brushes.Black, wenzi, stringAlignment);
            stringAlignment.Dispose();
            ZiTi1.Dispose();
            #endregion
            if (shangquyue.X > this.Width)
            {
                ShangYiCi = -this.Width / 10;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            JiCount++;
            if (BiaoZhi == 1)
            {
                this.timer1.Enabled = false;
                this.Colse1();
            }
            if ((DateTime.Now - JiTimeQi).TotalMilliseconds > TingLiuTime)
            {
                this.timer1.Enabled = false;
                this.Colse1();
            }
        }

        private void MaXingTiaoXinFrom_Load(object sender, EventArgs e)
        {
            this.Paint += Panel1_Paint;
            ZongKaiGuan = true;
            this.timer1.Enabled = true;
            Thread1 =new Thread(Work);
            Thread1.IsBackground = true;
            Thread1.DisableComObjectEagerCleanup();
            Thread1.Start();
            
        }

        private void Work()
        {

            while (ZongKaiGuan)
            {
                if (FenKaiGuan == false)
                {
                    Thread.Sleep(12);
                    continue;
                }
                try
                {
                    if (Action!=null)
                    {
                        Action();
                    }
                    BiaoZhi = 1;
                    ZongKaiGuan = false;
                }
                catch
                {

                    BiaoZhi = 1;
                    ZongKaiGuan = false;

                }
                Thread.Sleep(15);
            }

        }

     
        private  void Colse1()
        {
            BiaoZhi = 1;
            ZongKaiGuan = false;
            try
            {
                if (Thread1!=null)
                {
                    Thread1.Abort();
                 
                }
            }
            catch 
            {

              
            }
            Thread.Sleep(30);
            this.Close();
        }
      
        public void ShowWaitForm(Control fuform, string neirong, bool isxianshiguanbi,Action action, int tingliushijian)
        {

            Application.DoEvents();
            ShangYiCi = 0;
            this.TopMost = true;
            this.BringToFront();//放在前端显示
            this.Activate(); //当前窗体是LoadingForm          
            Control fu = GetFuKongJian(fuform);
            if (fuform is Form)
            {
                Point p = fu.PointToScreen(new Point(0, 0));
                this.Location = new Point(p.X + fuform.Width / 2 - this.Width / 2, p.Y + fuform.Height / 2 - this.Height / 2);
            }
            else
            {
                Point p = fu.PointToScreen(fuform.Location);
                this.Location = new Point(p.X + fuform.Width / 2 - this.Width / 2, p.Y + fuform.Height / 2 - this.Height / 2);
            }
            Action = action;
            JiaZaiMiaoShu = neirong;
            JiTimeQi = DateTime.Now;
            TingLiuTime = tingliushijian;
            FenKaiGuan = true;
            BiaoZhi = 0;
            Application.DoEvents();
            this.ShowDialog(fuform);
        }
    
        private Control GetFuKongJian(Control fuform)
        {
            if (fuform is Form)
            {
                return fuform;
            }
            else
            {
                return fuform.Parent;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.Colse1();
        }
    }
}
