using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommLei.JiChuLei;
using FoZhaoMes.Model.GongWeiModel;

namespace FoZhaoMes.PeiZhiFrm
{
    public partial class MesKJ : UserControl
    {
        public MesKJ()
        {
            InitializeComponent();
        }

        public void SetCanShu(GWModel model)
        {
            this.txbMoShi.Text = model.GWID.ToString();
            this.textBox1.Text=model.lotCode.ToString();
            this.textBox4.Text=model.stationName.ToString();
            this.textBox5.Text=model.stepName.ToString();
            this.textBox2.Text = model.userCode.ToString();
            this.textBox3.Text = model.JinZhanWangZhi.ToString();
            this.textBox6.Text = model.ChuZhanWangZhi.ToString();
            this.textBox7.Text = model.ZhuangXiangWangZhi.ToString();
            this.textBox8.Text = model.TiJiaoXiangZiWangZhi.ToString();
        }
        public GWModel GetModel()
        {
            GWModel model = new GWModel();
            model.GWID = ChangYong.TryInt(this.txbMoShi.Text,-1);
            model.lotCode = this.textBox1.Text;
            model.stationName=this.textBox4.Text;
            model.stepName=this.textBox5.Text;
            model.userCode=this.textBox2.Text;
            model.JinZhanWangZhi=this.textBox3.Text;
            model.ChuZhanWangZhi= this.textBox6.Text;
            model.ChuZhanWangZhi = this.textBox6.Text;
            model.ZhuangXiangWangZhi= this.textBox7.Text;
            model.TiJiaoXiangZiWangZhi = this.textBox8.Text;
            return model;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(this);
            }
        }
    }
}
