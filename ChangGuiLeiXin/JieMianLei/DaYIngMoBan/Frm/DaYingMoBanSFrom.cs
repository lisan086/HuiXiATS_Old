using BaseUI.DaYIngMoBan.Model;
using CommLei.JiChuLei;
using JieMianLei.UI;
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

namespace BaseUI.DaYIngMoBan.Frm
{
    public partial class DaYingMoBanSFrom : BaseFrom
    {
        public ZBiaoQianModel ZBiaoQianModel;
        private  string MuBanName = "";
        private int GaoDu = 0;
        private int KuanDu = 0;
        private int DaYingFangXiang = 1;
        private List<string> BianLiangMing = new List<string>();
        private List<string> MinXi = new List<string>();
        public DaYingMoBanSFrom()
        {
            InitializeComponent();
        }

        public void SetCanShu(string gaodu, string kuangdu, string fangxiang,List<string> bianliangming, List<string> mx)
        {
            txbGao.Text = gaodu;
            txbKuan.Text = kuangdu;
            txbCount.Text = fangxiang;
            this.listBox1.Items.Clear();
            for (int i = 0; i < bianliangming.Count; i++)
            {
                this.listBox1.Items.Add(bianliangming[i]);
            }
            this.listBox2.Items.Clear();
            for (int i = 0; i < mx.Count; i++)
            {
                this.listBox2.Items.Add(mx[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.txbName.Text == "")
            {
                MessageBox.Show("请填写模板名称");
                return;
            }
            if (this.txbGao.Text == "")
            {
                MessageBox.Show("请填写高度");
                return;
            }
            else
            {
                if (!int.TryParse(this.txbGao.Text, out GaoDu))
                {
                    MessageBox.Show("填写高度有误");
                    return;
                }
            }
            if (this.txbKuan.Text == "")
            {
                MessageBox.Show("请填写宽度");
                return;
            }
            else
            {
                if (!int.TryParse(this.txbKuan.Text, out KuanDu))
                {
                    MessageBox.Show("填写宽度有误");
                    return;
                }
            }
            int.TryParse(txbCount.Text, out DaYingFangXiang);
            this.MuBanName = this.txbName.Text;
            BianLiangMing.Clear();
            for (int i = 0; i < this.listBox1.Items.Count; i++)
            {
                BianLiangMing.Add(this.listBox1.Items[i].ToString());
            }
            MinXi.Clear();
            for (int i = 0; i < this.listBox2.Items.Count; i++)
            {
                MinXi.Add(this.listBox2.Items[i].ToString());
            }
            ZBiaoQianModel = new ZBiaoQianModel();
            ZBiaoQianModel.name = this.MuBanName;
            ZBiaoQianModel.GaoDu = GaoDu;
            ZBiaoQianModel.DaYingMoShi = DaYingFangXiang;
            ZBiaoQianModel.KuanDu = KuanDu;
            ZBiaoQianModel.MingXiZiDuan = ChangYong.FenGeDaBao<string>(MinXi, ",");
            ZBiaoQianModel.ZiDuan = ChangYong.FenGeDaBao<string>(BianLiangMing, ",");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( this.textBox1.Text)==false)
            {
                bool iscunzai = false;
                foreach (var item in this.listBox1.Items)
                {
                    if (item.ToString().Equals(this.textBox1.Text))
                    {
                        iscunzai = true;
                        break;
                    }
                }
                if (iscunzai==false)
                {
                    this.listBox1.Items.Add(this.textBox1.Text);
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count>0)
            {
                if (this.listBox1.SelectedItems.Count>0)
                {
                    this.listBox1.Items.Remove(this.listBox1.SelectedItems[0]);
                }
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBox2.Items.Count > 0)
            {
                if (this.listBox2.SelectedItems.Count > 0)
                {
                    this.listBox2.Items.Remove(this.listBox2.SelectedItems[0]);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text) == false)
            {
                bool iscunzai = false;
                foreach (var item in this.listBox2.Items)
                {
                    if (item.ToString().Equals(this.textBox2.Text))
                    {
                        iscunzai = true;
                        break;
                    }
                }
                if (iscunzai == false)
                {
                    this.listBox2.Items.Add(this.textBox2.Text);
                }
            }
        }
    }
}
