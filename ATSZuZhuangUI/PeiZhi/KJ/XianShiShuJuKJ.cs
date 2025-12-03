using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZuZhuangUI.Model;

namespace ZuZhuangUI.PeiZhi.KJ
{
    public partial class XianShiShuJuKJ : UserControl
    {
    
        private YeWuDataModel YeWuDataModel = new YeWuDataModel();
        public XianShiShuJuKJ()
        {
            InitializeComponent();
        }
        public void SetCanShu(YeWuDataModel yeWuData,bool iszhi)
        {
            YeWuDataModel = yeWuData;
            this.label9.Text = YeWuDataModel.ItemName;
          
            if (iszhi == false)
            {
                {
                    int index= this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = "上限";
                    this.dataGridView1.Rows[index].Cells[1].Value = "";
                    this.dataGridView1.Rows[index].Cells[2].Value = yeWuData.QingQiuPiPei;
                    this.dataGridView1.Rows[index].Tag = yeWuData.Up;
                    this.dataGridView1.Rows[index].Height = 32;
                }
                {

                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = "下限";
                    this.dataGridView1.Rows[index].Cells[1].Value = "";
                    this.dataGridView1.Rows[index].Cells[2].Value = yeWuData.QingQiuPiPei;
                    this.dataGridView1.Rows[index].Tag = yeWuData.Low;
                    this.dataGridView1.Rows[index].Height = 32;
                }
                {

                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = "状态";
                    this.dataGridView1.Rows[index].Cells[1].Value = "";
                    this.dataGridView1.Rows[index].Cells[2].Value = yeWuData.QingQiuPiPei;
                    this.dataGridView1.Rows[index].Tag = yeWuData.State;
                    this.dataGridView1.Rows[index].Height = 32;
                }
                {

                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = "值数据";
                    this.dataGridView1.Rows[index].Cells[1].Value = "";
                    this.dataGridView1.Rows[index].Cells[2].Value = yeWuData.QingQiuPiPei;
                    this.dataGridView1.Rows[index].Tag = yeWuData.Value;
                    this.dataGridView1.Rows[index].Height = 32;
                }
            }
            else
            {
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = "值数据";
                    this.dataGridView1.Rows[index].Cells[1].Value = "";
                    this.dataGridView1.Rows[index].Cells[2].Value = yeWuData.QingQiuPiPei;
                    this.dataGridView1.Rows[index].Tag = yeWuData.Value;
                    this.dataGridView1.Rows[index].Height = 32;
                }
            }
           
        }

        public void ShuaXin()
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Tag is ShuJuLisModel)
                {
                    ShuJuLisModel shuju = this.dataGridView1.Rows[i].Tag as ShuJuLisModel;
                    this.dataGridView1.Rows[i].Cells[1].Value = shuju.JiCunValue;
                }
            }
            
        }
    }
}
