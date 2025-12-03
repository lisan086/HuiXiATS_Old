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

namespace BaseUI.FuFrom.KJ
{
    public partial class XuanZeQiTaFrom : BaseFuFrom
    {
        private List<GongNengModel> ShanYiCi = new List<GongNengModel>();
        private List<GongNengModel> julu = new List<GongNengModel>();

     
        public XuanZeQiTaFrom()
        {
            InitializeComponent();
            this.IsZhiXianShiX = true;
        }
        public void SetCanShu(List<GongNengModel> all, List<GongNengModel> bufen)
        {
            if (all == null || all.Count == 0)
            {
                return;
            }

            for (int i = 0; i < all.Count; i++)
            {
                int index = this.listBox1.Items.Add(all[i].Name);
               
            }
            julu.AddRange(all);
            if (bufen == null || bufen.Count == 0)
            {
                return;
            }
            Dictionary<int, TreeNode> bufend = new Dictionary<int, TreeNode>();
            for (int i = 0; i < bufen.Count; i++)
            {
                int index = this.listBox2.Items.Add(bufen[i].Name);             
            }
          
            ShanYiCi.AddRange(bufen);
        }
        protected override void GuanBi()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public List<GongNengModel> GetCanShu(out bool isheshangciyiyang)
        {
            isheshangciyiyang = false;
            List<GongNengModel> suan = new List<GongNengModel>();
            for (int i = 0; i < this.listBox2.Items.Count; i++)
            {
                string name = this.listBox2.Items[i].ToString();
                GongNengModel gong = julu.Find((x) => { return  x.Name == name; });
                if (gong!=null)
                {
                    suan.Add(gong);
                }
            }

            if (suan.Count == 0 && ShanYiCi.Count == 0)
            {
                isheshangciyiyang = true;
            }
            if (suan.Count == ShanYiCi.Count)
            {
                bool xiangdeng = false;
                for (int i = 0; i < suan.Count; i++)
                {
                    GongNengModel gong = ShanYiCi.Find((x) => { return  x.Name == suan[i].Name; });
                    if (gong == null)
                    {
                        xiangdeng = true;
                        break;
                    }
                }
                if (xiangdeng == false)
                {
                    isheshangciyiyang = true;
                }
            }

            return suan;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                object shuju = listBox1.SelectedItem;
                if (shuju!=null)
                {
                    bool iscumzai = false;
                    for (int i = 0; i < listBox2.Items.Count; i++)
                    {
                        if (listBox2.Items[i].ToString().Equals(shuju.ToString()))
                        {
                            iscumzai = true;
                            break;
                        }
                    }
                    if (iscumzai==false)
                    {
                        listBox2.Items.Add(shuju.ToString());
                    }
                }
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                object shuju = listBox1.SelectedItem;
                if (shuju != null)
                {
                    listBox2.Items.Remove(shuju);
                   
                }
            }
        }
    }
}
