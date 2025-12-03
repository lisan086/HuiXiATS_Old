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

namespace BaseUI.FuFrom.XinWeiHuFrm
{
    public partial class ChaoZhaoFrm : BaseFuFrom
    {
        public Action<string> ChaZhao;
        public ChaoZhaoFrm()
        {
            InitializeComponent();
            this.IsZhiXianShiX = true;
        }
        public void SetCanShu(string mc)
        {
            this.label1.Text = mc;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ChaZhao!=null)
            {
                ChaZhao(this.textBox1.Text);
            }
        }
    }
}
