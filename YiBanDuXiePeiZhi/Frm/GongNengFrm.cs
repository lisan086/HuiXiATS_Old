using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JieMianLei.FuFrom;
using YiBanSaoMaQi.Model;

namespace YiBanDuXiePeiZhi.Frm
{
    public partial class GongNengFrm : BaseFuFrom
    {
        public GongNengFrm()
        {
            InitializeComponent();
        }
        public void SetCanShu(List<CunModel> lis)
        {
            List<JiCunQiKJ> liskj = new List<JiCunQiKJ>();
            for (int i = 0; i < lis.Count; i++)
            {
                JiCunQiKJ kj = new JiCunQiKJ();
                kj.SetCanShu(lis[i]);
                liskj.Add(kj);
            }
            this.flowLayoutPanel1.Controls.AddRange(liskj.ToArray());
        }
        protected override void GuanBi()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public List<CunModel> GetShuJu()
        {
            List<CunModel> LisShuJu = new List<CunModel>();
            for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
            {
                if (this.flowLayoutPanel1.Controls[i] is JiCunQiKJ)
                {
                    JiCunQiKJ jiCunQiKJ = this.flowLayoutPanel1.Controls[i] as JiCunQiKJ;
                    LisShuJu.Add(jiCunQiKJ.GetSaoMaModel());
                }
            }
            return LisShuJu;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            JiCunQiKJ kj = new JiCunQiKJ();
            kj.SetCanShu(new CunModel());
            this.flowLayoutPanel1.Controls.Add(kj);
        }
    }
}
