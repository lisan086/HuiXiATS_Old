using JieMianLei.FuFrom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UC.ZuHeKJ
{
    public partial class AddFrom : UserControl
    {
        private ZiDianButton ShangCi = null;
        private Dictionary<ZiDianButton, BaseFuFrom> KongZi = new Dictionary<ZiDianButton, BaseFuFrom>();

        private List<ZiDianButton> lisZiDian = new List<ZiDianButton>();
        public AddFrom()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        private bool _IsXiangXia = false;

        public bool IsXiangXia
        {
            get
            {
                return _IsXiangXia;
            }

            set
            {
                _IsXiangXia = value;
                if (_IsXiangXia)
                {
                    this.pans1.Dock = DockStyle.Bottom;
                }
                else
                {
                    this.pans1.Dock = DockStyle.Top;
                }
            }
        }

        public void ZengJiaFrom(string fromtxt, BaseFuFrom form)
        {

            bool iszai = false;
            ZiDianButton ziDianButton1 = null;
            for (int i = 0; i < lisZiDian.Count; i++)
            {
                if (lisZiDian[i].CaiDanName.Equals(fromtxt))
                {
                    ziDianButton1 = lisZiDian[i];
                    iszai = true;
                    break;
                }
            }
            if (iszai == false)
            {
                ZiDianButton ziDianButton = ShengCheng(fromtxt);
                KongZi.Add(ziDianButton, form);
                lisZiDian.Add(ziDianButton);
                ChuXian();
                this.pans1.Controls.Add(ziDianButton);
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.Parent = this.panel2;
                form.Hide();
                ShowFrom(ziDianButton);
            }
            else
            {
                ShowFrom(ziDianButton1);
            }
        }
        private void ChuXian()
        {
            int kuandu = 0;
            int yanchang = 0;

            for (int i = 0; i < lisZiDian.Count; i++)
            {
                kuandu += lisZiDian[i].Width + 8;
                if (kuandu > this.pans1.Width-10)
                {
                    int shengxia = lisZiDian.Count - i;
                    yanchang = shengxia;
                    break;
                }
            }
            for (int i = 0; i < yanchang; i++)
            {
                ZiDianButton_DianJiEvent(lisZiDian[0], 1);
            }
        }
        private ZiDianButton ShengCheng(string fromtxt)
        {
            ZiDianButton ziDianButton = new ZiDianButton();

            ziDianButton.BorderStyle = BorderStyle.FixedSingle;
            ziDianButton.CaiDanName = fromtxt;
            ziDianButton.ZiTiColor = Color.Black;
            ziDianButton.ZiTiDaXiao = 12F;
            ziDianButton.BackColor = Color.White;
            ziDianButton.Height = this.pans1.Height - 6;
            ziDianButton.Width = (int)(fromtxt.Length)*25;
            ziDianButton.DianJiEvent += ZiDianButton_DianJiEvent;

            return ziDianButton;
        }

        private void ZiDianButton_DianJiEvent(ZiDianButton control, int leixing)
        {
            if (leixing == 1)
            {
                bool keyishanchu = true;
               
                if (keyishanchu)
                {
                    lisZiDian.Remove(control);
                    for (int i = 0; i < this.pans1.Controls.Count; i++)
                    {
                        if (this.pans1.Controls[i].Equals(control))
                        {
                            this.pans1.Controls.RemoveAt(i);
                            break;
                        }
                    }

                    if (KongZi.ContainsKey(control))
                    {
                        BaseFuFrom form = KongZi[control];
                        form.GuanDiao();
                        KongZi.Remove(control);
                    }
                    if (lisZiDian.Count > 0)
                    {
                        ShowFrom(lisZiDian[0]);
                    }
                }
            }
            else
            {
                ShowFrom(control);
            }
        }


        private void ShowFrom(ZiDianButton control)
        {
            if (control.Equals(ShangCi))
            {
                ShangCi = control;
                return;
            }
            ShangCi = control;
            foreach (var item in KongZi.Keys)
            {
                if (item.Equals(control))
                {
                    item.BorderStyle = BorderStyle.Fixed3D;
                    KongZi[item].BringToFront();                  
                    KongZi[item].Show();
                    KongZi[item].ChuShiJiaZai();
                    item.BackColor = Color.Yellow;
                }
                else
                {
                    item.BackColor = Color.White;
                    item.BorderStyle = BorderStyle.FixedSingle;
                    KongZi[item].Hide();
                }

            }


        }


        public void CloseAll()
        {
            var items = KongZi.Keys.ToList();
            if (items.Count>0)
            {
                foreach (var item in items)
                {
                    KongZi[item].GuanDiao();
                }
            }
        }
    }
}
