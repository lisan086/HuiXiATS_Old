using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommLei.DataChuLi;
using FoZhaoMes.Model.GongWeiModel;
using JieMianLei.FuFrom;

namespace FoZhaoMes.PeiZhiFrm
{
    public partial class TongDaoPeiZhiFrm : BaseFuFrom
    {
        public TongDaoPeiZhiFrm()
        {
            InitializeComponent();
        }

        private void SetCanShu()
        {
            List<GWModel> lis = HCLisDataLei<GWModel>.Ceratei().LisWuLiao;
            List<MesKJ> liskj = new List<MesKJ>();
            for (int i = 0; i < lis.Count; i++)
            {
                MesKJ kj = new MesKJ();
                kj.SetCanShu(lis[i]);
                liskj.Add(kj);
            }
            this.flowLayoutPanel1.Controls.AddRange(liskj.ToArray());
        }

        private void TongDaoPeiZhiFrm_Load(object sender, EventArgs e)
        {
            SetCanShu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MesKJ kj = new MesKJ();
            kj.SetCanShu(new GWModel());
            this.flowLayoutPanel1.Controls.Add(kj);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<GWModel> shujulis = new List<GWModel>();
            for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
            {
                if (this.flowLayoutPanel1.Controls[i] is MesKJ)
                {
                    MesKJ mesKJ = this.flowLayoutPanel1.Controls[i] as MesKJ;
                    shujulis.Add(mesKJ.GetModel());
                }
            }
            HCLisDataLei<GWModel>.Ceratei().LisWuLiao = shujulis;
            HCLisDataLei<GWModel>.Ceratei().BaoCun();
            this.QiDongTiShiKuang("保存成功");
        }
    }
}
