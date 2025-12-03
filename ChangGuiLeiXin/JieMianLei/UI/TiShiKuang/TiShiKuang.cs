using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.UI.TiShiKuang
{
    internal partial class TiShiKuang : Form
    {
        private int leixing = 1;
        public TiShiKuang()
        {
            InitializeComponent();
        }
        public void ShowTiShiKuang(List<string> neirong, Control fuform, Point bangdingkongjian, int kuang, int KuangTiGao, Point bangdingkongjian1)
        {
            leixing = 1;
            this.BringToFront();//放在前端显示     
            this.Height = KuangTiGao;
            this.Location = new Point(bangdingkongjian.X + kuang, bangdingkongjian.Y);
            Point p = this.Location;
            if (p.X + this.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                this.Location = new Point(bangdingkongjian.X - this.Width, bangdingkongjian.Y);
                if (p.Y + this.Height > Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Location = new Point(bangdingkongjian.X - this.Width, bangdingkongjian.Y - this.Height);
                }
            }
            else
            {
                if (p.Y + this.Height > Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Location = new Point(bangdingkongjian.X + kuang, bangdingkongjian.Y - this.Height);
                }
            }
            SetCanShu(neirong);
            this.Show(fuform);
        }

        public void ShowTiShiKuang( Control fuform, Point bangdingkongjian, int kuang, int KuangTiGao, Point bangdingkongjian1,Control xianshikj,int kutishikuan)
        {
           
            leixing = 2;
            this.BringToFront();//放在前端显示     
            this.Height = KuangTiGao;
            this.Width = kutishikuan;
            this.Location = new Point(bangdingkongjian.X + kuang, bangdingkongjian.Y);
            Point p = this.Location;
            if (p.X + this.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                this.Location = new Point(bangdingkongjian.X - this.Width, bangdingkongjian.Y);
                if (p.Y + this.Height > Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Location = new Point(bangdingkongjian.X - this.Width, bangdingkongjian.Y - this.Height);
                }
            }
            else
            {
                if (p.Y + this.Height > Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Location = new Point(bangdingkongjian.X + kuang, bangdingkongjian.Y - this.Height);
                }
            }
            this.Controls.Clear();
            xianshikj.Dock = DockStyle.Fill;
            this.Controls.Add(xianshikj);
            this.Show(fuform);
        }
        private void SetCanShu(List<string> neirong)
        {
            StringBuilder sb = new StringBuilder();
            if (neirong.Count > 0)
            {
                for (int i = 0; i < neirong.Count; i++)
                {
                    sb.AppendLine(neirong[i]);
                }
                this.label1.Text = sb.ToString();
            }
        }
        public void CloseGuanBi()
        {
            if (leixing==2)
            {
                this.Controls.Clear();
            }
            this.Close();
        }
    }
}
