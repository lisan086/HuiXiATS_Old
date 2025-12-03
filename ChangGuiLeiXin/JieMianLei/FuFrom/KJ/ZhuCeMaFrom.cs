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
using Common.DataChuLi;
using CommLei.DataChuLi;

namespace BaseUI.FuFrom.KJ
{
    public partial class ZhuCeMaFrom : BaseFuFrom
    {
        private SuiJiShuLei SuiJiShuLei;
        public ZhuCeMaFrom()
        {
            InitializeComponent();
            SuiJiShuLei = new SuiJiShuLei();
        }


        private void ShengChengSuiJiMa()
        {
          this.label4.Text=  string.Format("{0}{1}{2}{3}{4}{5}", SuiJiShuLei.SuiJiData(0,10), SuiJiShuLei.SuiJiData(0, 10), SuiJiShuLei.SuiJiData(0, 10), SuiJiShuLei.SuiJiData(0, 10), SuiJiShuLei.SuiJiData(0, 10), SuiJiShuLei.SuiJiData(0, 10));
        }

        private void ZhuCeMaFrom_Load(object sender, EventArgs e)
        {
            ShengChengSuiJiMa();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                this.QiDongTiShiKuang("请填写参数");
                return;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                this.QiDongTiShiKuang("请填写参数");
                return;
            }
            QiangJiaMi qiangJia = new QiangJiaMi();
            string jiechutishima = qiangJia.JieMi(this.textBox1.Text);
            if (jiechutishima.StartsWith(this.label4.Text))
            {
                jiechutishima = jiechutishima.Replace(this.label4.Text, "");
                jiechutishima = qiangJia.JiaMi(jiechutishima);
            }
            else
            {
                this.QiDongTiShiKuang("注册码不对");
                return;
            }
            string jissdd = qiangJia.JieMi(this.textBox2.Text);
            if (jissdd.StartsWith(this.label4.Text))
            {
                jissdd = jissdd.Replace(this.label4.Text, "");
                jissdd = qiangJia.JiaMi(jissdd);
            }
            else
            {
                this.QiDongTiShiKuang("注册码不对");
                return;
            }
            bool zhen = ZhuCheLei.Ceratei().ZhuCeShiJian(jiechutishima, jissdd, "000252");
            if (zhen)
            {
                ShengChengSuiJiMa();
            }
            else
            {
                this.QiDongTiShiKuang("注册失败,请重新注册");
            }
        }
    }
}
