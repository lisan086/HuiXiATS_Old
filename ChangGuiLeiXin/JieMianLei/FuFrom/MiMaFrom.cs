using JieMianLei.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.FuFrom
{
    public partial class MiMaFrom : BaseFuFrom
    {
        public int FanHuiIndex = -99;
        private Dictionary<int, string> DuiXiangMiMa = new Dictionary<int, string>();
        private string MiMa = "";
        private int BiaoZhi = 1;
        public MiMaFrom()
        {
            InitializeComponent();
            this.IsZhiXianShiX = true;
        }
        public void SetWenBen(string biaoti, string mima)
        {
            this.labFbiaoTi.Text = biaoti;
            this.label1.Text = mima;
        }
        public void SetCanShu(string miama, JianPanType jianPan)
        {
          
            this.textBoxE1.JianPan = jianPan;
            MiMa = miama;
        }
        public void SetCanShu(Dictionary<int, string> miama, JianPanType jianPan)
        {

            this.textBoxE1.JianPan = jianPan;
            foreach (var item in miama.Keys)
            {
                DuiXiangMiMa.Add(item, miama[item]);
            }
            BiaoZhi = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BiaoZhi == 1)
            {
                if (MiMa == this.textBoxE1.Text)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.QiDongTiShiKuang("输入错误");
                }
            }
            else
            {
                bool iszai = false;
                FanHuiIndex = -100;
                string mima = this.textBoxE1.Text;
                foreach (var item in DuiXiangMiMa.Keys)
                {
                    if (DuiXiangMiMa[item].Equals(mima))
                    {
                        iszai = true;
                        FanHuiIndex = item;
                        break;
                    }
                }
                if (iszai)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.QiDongTiShiKuang("输入错误");
                }
            }
        }
    }
}
