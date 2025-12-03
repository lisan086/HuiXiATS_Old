using CommLei.JiChuLei;
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

namespace JieMianLei.UI
{
    public partial class ShuZiJianPanFrom :BaseFuFrom
    {
        private List<string> CanShu = new List<string>();
        private float ZuiXiaoZhi = float.MinValue;
        private float ZuiDaZhi = float.MaxValue;
        public string ShuZhi = "";
        public ShuZiJianPanFrom()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.IsZhiXianShiX = true;
        }
        public void SetCanShu(string tsex)
        {
            this.label1.Text = tsex;
           
            this.label5.Text ="不限制";
            this.label4.Text = "不限制";
        }

        public void SetCanShu(string shuju, string zuidazhi, string zuixiaozhi)
        {
           
            this.labFbiaoTi.Text = shuju;

            ZuiXiaoZhi = ChangYong.TryFloat(zuixiaozhi, float.MinValue);
            ZuiDaZhi = ChangYong.TryFloat(zuidazhi,float.MaxValue);
            if (ZuiXiaoZhi == float.MinValue)
            {
                this.label5.Text = "不限制";
            }
            else
            {
                this.label5.Text = ZuiXiaoZhi.ToString();
                
            }
            if (ZuiDaZhi == float.MaxValue)
            {
                this.label4.Text = "不限制";
            }
            else
            {
                this.label4.Text = ZuiDaZhi.ToString();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.label1.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button bt = sender as Button;
                string shuju = this.label1.Text;
                if (shuju.Length == 1 && shuju == "0")
                {
                    this.label1.Text = string.Format("{0}", bt.Text.Trim());
                }
                else
                {
                    this.label1.Text = string.Format("{0}{1}", this.label1.Text, bt.Text.Trim());
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string canshu = this.label1.Text;
            float zhi = ChangYong.TryFloat(canshu, -1);         
            if (ZuiDaZhi > ZuiXiaoZhi)
            {
                if ((zhi >= ZuiXiaoZhi && zhi <= ZuiDaZhi)==false)
                {
                    this.QiDongTiShiKuang("输入的值，不在范围内啊");
                    return;
                }
              
            }
            ShuZhi = this.label1.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            string shuju = this.label1.Text;
            if ((shuju.Length == 1 && shuju == "0"))
            {
                this.label1.Text = string.Format("-");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string shuju = this.label1.Text;
            if (!(shuju.Length == 1 && shuju == "-"))
            {
                this.label1.Text = string.Format("{0}{1}", this.label1.Text, button10.Text.Trim());
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string shuju = this.label1.Text;
            if (!(shuju.Length == 1 && shuju == "0"))
            {
                this.label1.Text = string.Format("{0}{1}", this.label1.Text, button10.Text.Trim());
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string shuju = this.label1.Text;
            if (shuju.Length <= 1)
            {
                this.label1.Text = string.Format("0");
            }
            else
            {
                int count = shuju.Length - 1;
                shuju = shuju.Substring(0, count);
                this.label1.Text = shuju;
            }

        }
    }
}
