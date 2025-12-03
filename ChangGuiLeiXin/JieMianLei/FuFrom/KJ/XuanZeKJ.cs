using CommLei.JiChuLei;
using JieMianLei.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.FuFrom.KJ
{
    public delegate void DianJiKJ(int biaoshi, string neirong);
    public partial class XuanZeKJ : UserControl
    {
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;
        private List<Control> KJS = new List<Control>();
     
        private bool _IsZiDongTiaoJie = true;
        private bool KongZhiChuFa = false;
        public bool IsZiDongTiaoJie
        {
            get
            {
                return _IsZiDongTiaoJie;
            }

            set
            {
                _IsZiDongTiaoJie = value;
            }
        }

        /// <summary>
        /// 1是查找 2是新增 3为保存
        /// </summary>
        public event DianJiKJ DianJiKJEvent;
        public XuanZeKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ucDaoHangKongJian1.Rows = 80;
            this.weiKuKJ1.DianJiEvent += WeiKuKJ1_DianJiEvent;
            this.ucDaoHangKongJian1.UCClilk += UcDaoHangKongJian1_UCClilk;
        }
        private void UcDaoHangKongJian1_UCClilk(object sender, DaoHangModel e)
        {
            #region 显示
            int index = e.Index;
            int hangshu = e.Row;
            int count = KJS.Count;
            List<Control> shuju = new List<Control>();
            for (int i = hangshu * (index); i < (index + 1) * hangshu; i++)
            {
                if (i >= count)
                {
                    break;
                }
                shuju.Add(KJS[i]);
            }
            KongZhiChuFa = true;
            SendMessage(flowLayoutPanel1.Handle, WM_SETREDRAW, 0, IntPtr.Zero);//禁止重绘
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                this.flowLayoutPanel1.Controls.Clear();
            }
            this.flowLayoutPanel1.Controls.AddRange(shuju.ToArray());
            SendMessage(flowLayoutPanel1.Handle, WM_SETREDRAW, 1, IntPtr.Zero);//取消禁止
            this.flowLayoutPanel1.Refresh();
            KongZhiChuFa = false;
            #endregion
        }
        private void WeiKuKJ1_DianJiEvent(int biaozhi, string neirong)
        {
            if (DianJiKJEvent != null)
            {
                DianJiKJEvent(biaozhi, neirong);
            }
        }
        public void SetCanShu(List<Control> Kjs)
        {
            KongZhiChuFa = true;
            SendMessage(flowLayoutPanel1.Handle, WM_SETREDRAW, 0, IntPtr.Zero);//禁止重绘
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                this.flowLayoutPanel1.Controls.Clear();
            }

            int allpage = Kjs.Count;
            this.ucDaoHangKongJian1.AllPage = ChangYong.PageYeShu(this.ucDaoHangKongJian1.Rows, allpage);
            int row = this.ucDaoHangKongJian1.Rows;
            List<Control> shuju = new List<Control>();
            for (int i = 0; i < row; i++)
            {
                if (i >= allpage)
                {
                    break;
                }
                shuju.Add(Kjs[i]);
            }
            this.flowLayoutPanel1.Controls.AddRange(shuju.ToArray());

            KJS = Kjs;
            Dictionary<string, string> zidian = new Dictionary<string, string>();
            zidian.Add("总数量", allpage.ToString());
            this.heJiKJ1.SetKongJian(zidian);
            SendMessage(flowLayoutPanel1.Handle, WM_SETREDRAW, 1, IntPtr.Zero);//取消禁止
            this.flowLayoutPanel1.Refresh();


            KongZhiChuFa = false;
        }

        public List<Control> GetCanShu()
        {
            List<Control> kjs = new List<Control>();

            for (int i = 0; i < KJS.Count; i++)
            {
                kjs.Add(KJS[i]);
            }
            return kjs;
        }



        public void AddKJ(Control KJ)
        {
            KJS.Insert(0, KJ);
           
            SetCanShu(KJS);
        }
        public void BuXianShiKJ(List<string> kj)
        {
            this.weiKuKJ1.SetXianShi(kj);
        }

        private void flowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (KongZhiChuFa)
            {
                return;
            }
            for (int i = 0; i < KJS.Count; i++)
            {
                if (KJS[i].Equals(e.Control))
                {
                    KJS.RemoveAt(i);
                    Dictionary<string, string> zidian = new Dictionary<string, string>();
                    zidian.Add("总数量", KJS.Count.ToString());
                    this.heJiKJ1.SetKongJian(zidian);
                    break;
                }
            }
        }
    }
}
