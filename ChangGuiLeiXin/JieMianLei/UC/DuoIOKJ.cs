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
    public partial class DuoIOKJ : UserControl
    {
        public event DianJi DianJiOKOrCloseEvent;
        public DuoIOKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        private bool JiRuKJ = false;
        private bool IsManGe = false;
        private List<JiLuModel> _DengStr = new List<JiLuModel>();
        private int _GaoDu = 40;
        private bool _IsDu = false;
        public int GaoDu
        {
            get
            {
                return _GaoDu;
            }

            set
            {
                _GaoDu = value;
            }
        }

        public bool IsDu
        {
            get
            {
                return _IsDu;
            }

            set
            {
                _IsDu = value;
            }
        }
        public void SetXieIO(List<JiLuModel> lismodel)
        {
            _DengStr.Clear();
            foreach (var item in lismodel)
            {
                _DengStr.Add(item);
            }
            this.Refresh();
        }

        public void SetYanSe(string jicunqiid, bool ison)
        {
            foreach (var item in _DengStr)
            {
                if (item.JiCunQiID == jicunqiid)
                {
                    item.IsChengGong = ison;
                    break;
                }
            }
        }

        public void ShuaXin()
        {
            this.Refresh();
        }
        private void GaiBian()
        {
            if (_DengStr != null && _DengStr.Count >= 0)
            {
                int gaodu = _GaoDu * _DengStr.Count + 2 * _DengStr.Count;
                if (gaodu >= this.Height)
                {

                    this.AutoScroll = true;
                    this.AutoScrollMinSize = new Size(0, gaodu + 5);//解决问题需添加
                    IsManGe = true;
                }
                else
                {
                    this.AutoScroll = false;
                    this.AutoScrollMinSize = new Size(0, 0);//解决问题需添加
                    IsManGe = false;
                }


            }
        }
        private void HuaQi(Graphics dc, SolidBrush huashua, Font ziti, Font ziti1, StringFormat frm, JiLuModel model)
        {
            Rectangle diyi = new Rectangle();
            diyi.X = model.Rectangle.X;
            diyi.Y = model.Rectangle.Y;
            int kuandu = model.Rectangle.Width / 4;
            int gaodu = model.Rectangle.Height;
            if (kuandu > gaodu)
            {
                diyi.Width = gaodu;
                diyi.Height = gaodu;
            }
            else
            {
                diyi.Width = kuandu;
                diyi.Height = kuandu;
            }
            if (model.IsChengGong)
            {
                huashua.Color = model.DaKaiColor;
            }
            else
            {
                huashua.Color = model.CloseColor;
            }
            dc.FillEllipse(huashua, diyi);
            int kuang = model.Rectangle.Width - diyi.Width - 30;
            Rectangle dier = new Rectangle();
            dier.X = model.Rectangle.X + diyi.Width;
            dier.Y = model.Rectangle.Y;
            dier.Width = kuang * 2 / 3;
            dier.Height = model.Rectangle.Height;
            dc.DrawString(model.MingCheng, ziti, Brushes.Black, dier, frm);
            if (_IsDu == false)
            {
                Rectangle disan = new Rectangle();
                disan.X = model.Rectangle.X + diyi.Width + dier.Width;
                disan.Y = model.Rectangle.Y;
                disan.Width = kuang / 6;
                disan.Height = model.Rectangle.Height;
                model.SetDaKaiJuXing(disan);
                if (model.IsJinRu == false)
                {
                    dc.DrawRectangle(Pens.Black, disan);
                }
                else
                {
                    dc.DrawRectangle(Pens.Green, disan);
                }
                dc.DrawString("打开", ziti1, Brushes.Black, disan, frm);

                Rectangle disi = new Rectangle();
                disi.X = model.Rectangle.X + diyi.Width + dier.Width + disan.Width;
                disi.Y = model.Rectangle.Y;
                disi.Width = kuang / 6;
                disi.Height = model.Rectangle.Height;
                model.SetColseJuXing(disi);
                if (model.IsClose == false)
                {
                    dc.DrawRectangle(Pens.Black, disi);
                }
                else
                {
                    dc.DrawRectangle(Pens.Green, disi);
                }

                dc.DrawString("关闭", ziti1, Brushes.Black, disi, frm);
            }
        }

        private void ChaoChuLieIOKJ_Load(object sender, EventArgs e)
        {
            this.Paint += ChaoChuLieKJ_Paint;
            this.MouseEnter += ChaoChuLieKJ_MouseEnter;
            this.MouseMove += ChaoChuLieKJ_MouseMove;
            this.MouseLeave += ChaoChuLieKJ_MouseLeave;
            this.MouseClick += ChaoChuLieKJ_MouseClick;
        }
        private void ChaoChuLieKJ_MouseClick(object sender, MouseEventArgs e)
        {
            if (_IsDu == false)
            {
                if (JiRuKJ)
                {
                    Point dian = new Point(e.X, e.Y);
                    for (int i = 0; i < _DengStr.Count; i++)
                    {
                        if (_DengStr[i].DaKaiRec.Contains(dian) && _DengStr[i].Rectangle.Contains(dian))
                        {

                            if (DianJiOKOrCloseEvent != null)
                            {
                                DianJiOKOrCloseEvent(this, true, _DengStr[i]);
                            }
                        }

                        if (_DengStr[i].GuanBiRec.Contains(dian) && _DengStr[i].Rectangle.Contains(dian))
                        {

                            if (DianJiOKOrCloseEvent != null)
                            {
                                DianJiOKOrCloseEvent(this, false, _DengStr[i]);
                            }
                        }

                    }
                }
            }
        }

        private void ChaoChuLieKJ_MouseLeave(object sender, EventArgs e)
        {
            if (_IsDu == false)
            {
                JiRuKJ = false;
                for (int i = 0; i < _DengStr.Count; i++)
                {
                    _DengStr[i].SetJinRu(false);
                    _DengStr[i].SetCloseJinRu(false);
                }
            }
        }


        private void ChaoChuLieKJ_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsDu == false)
            {
                if (JiRuKJ)
                {
                    Point dian = new Point(e.X, e.Y);
                    for (int i = 0; i < _DengStr.Count; i++)
                    {
                        if (_DengStr[i].DaKaiRec.Contains(dian))
                        {
                            _DengStr[i].SetJinRu(true);
                        }
                        else
                        {
                            _DengStr[i].SetJinRu(false);
                        }
                        if (_DengStr[i].GuanBiRec.Contains(dian))
                        {
                            _DengStr[i].SetCloseJinRu(true);
                        }
                        else
                        {
                            _DengStr[i].SetCloseJinRu(false);
                        }
                    }
                    this.Refresh();
                }
            }
        }

        private void ChaoChuLieKJ_MouseEnter(object sender, EventArgs e)
        {
            JiRuKJ = true;
        }

        private void ChaoChuLieKJ_Paint(object sender, PaintEventArgs e)
        {
            GaiBian();
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Graphics dc = e.Graphics;
            Font ziti = new Font("微软姚黑", 12);
            Font ziti1 = new Font("微软姚黑", 8);
            SolidBrush huashua = new SolidBrush(Color.Green);
            StringFormat frm = new StringFormat();
            frm.LineAlignment = StringAlignment.Center;
            frm.Alignment = StringAlignment.Center;
            if (IsManGe == false)
            {
                for (int i = 0; i < _DengStr.Count; i++)
                {
                    Rectangle rec = new Rectangle();
                    rec.X = 0;
                    rec.Y = 0 + _GaoDu * i + i * 2;
                    rec.Width = this.Width - 2;
                    rec.Height = _GaoDu;
                    _DengStr[i].SetJuXing(rec);
                    HuaQi(dc, huashua, ziti, ziti1, frm, _DengStr[i]);
                }
            }
            else
            {
                Size scrollOffset = new Size(this.AutoScrollPosition);//解决问题需添加
                for (int i = 0; i < _DengStr.Count; i++)
                {
                    Rectangle rec = new Rectangle();
                    rec.X = 0 + scrollOffset.Width;
                    rec.Y = 0 + _GaoDu * i + i * 2 + scrollOffset.Height;
                    rec.Width = this.Width - 20;
                    rec.Height = _GaoDu;
                    _DengStr[i].SetJuXing(rec);

                    if ((rec.Y + rec.Height) > 0 && rec.Y < this.Height)
                    {
                        HuaQi(dc, huashua, ziti, ziti1, frm, _DengStr[i]);
                    }
                    else
                    {
                        _DengStr[i].SetJinRu(false);
                        _DengStr[i].SetCloseJinRu(false);
                    }

                }
            }
            ziti.Dispose();
            ziti1.Dispose();
            huashua.Dispose();
            frm.Dispose();
        }
    }

    public delegate void DianJi(object kj, bool isdakai, JiLuModel e);
    public class JiLuModel
    {

        private string _JiCunQiBiaoShi = "";

        private bool _JinRu1 = false;
        private bool _JinRu2 = false;
        private Rectangle _Rectangle = new Rectangle();
        private Rectangle _Rectangle1 = new Rectangle();
        private Rectangle _Rectangle2 = new Rectangle();




        public string MingCheng { get; set; } = "";

        public bool IsJinRu { get { return _JinRu1; } }

        public bool IsClose { get { return _JinRu2; } }

        public bool IsChengGong { get; set; } = true;

        public Color DaKaiColor { get; set; } = Color.Green;

        public Color CloseColor { get; set; } = Color.Red;


        public string JiCunQiID { get { return _JiCunQiBiaoShi; } }

        public Rectangle Rectangle { get { return _Rectangle; } }

        public Rectangle DaKaiRec { get { return _Rectangle1; } }

        public Rectangle GuanBiRec { get { return _Rectangle2; } }

        public void SetJinRu(bool isjinru)
        {
            _JinRu1 = isjinru;
        }
        public void SetCloseJinRu(bool isjinru)
        {
            _JinRu2 = isjinru;
        }

        public void SetJuXing(Rectangle rec)
        {
            _Rectangle.X = rec.X;
            _Rectangle.Y = rec.Y;
            _Rectangle.Width = rec.Width;
            _Rectangle.Height = rec.Height;
        }
        public void SetDaKaiJuXing(Rectangle rec)
        {
            _Rectangle1.X = rec.X;
            _Rectangle1.Y = rec.Y;
            _Rectangle1.Width = rec.Width;
            _Rectangle1.Height = rec.Height;
        }
        public void SetColseJuXing(Rectangle rec)
        {
            _Rectangle2.X = rec.X;
            _Rectangle2.Y = rec.Y;
            _Rectangle2.Width = rec.Width;
            _Rectangle2.Height = rec.Height;
        }

        public void SetJiCunIDAndSheBeiID(string jicunqiid)
        {
            _JiCunQiBiaoShi = jicunqiid;

        }
    }
}
