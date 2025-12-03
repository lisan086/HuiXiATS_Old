using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATSJianMianJK;
using ATSJianMianJK.Log;
using CommLei.JiChuLei;
using ZuZhuangUI.Model;

namespace ATSZuZhuangUI.Lei.GongNengLei.ShiXian.KJ
{
    public partial class ZhanKJ : UserControl, IFKJ
    {
        Image OK = null;
        Image NG = null;
        private int TDID = -1;
        private ZiYuanModel ZiYuan;
        private SheBeiZhanModel SheBeiZhanModel;
        private GongZhanModel GongZhanModel=null;
        public ZhanKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.pictureBox1.Image = null;
            this.pictureBox1.Visible = false;
            this.label1.Text = "";
            this.label2.Text = "";
        }
        public void SetCanShu(ZiYuanModel ziYuanModel)
        {
            ZiYuan = ziYuanModel;
        }
        public void SetLog(int tdtd, RiJiModel riji)
        {
            if (TDID == tdtd)
            {
                this.ucJiLvContor1.LogAppend(riji.IsRed ? Color.Red : Color.Black, riji.Msg);
            }
        }
        public void SetModel(SheBeiZhanModel zhanModel)
        {
            SheBeiZhanModel = zhanModel;
            TDID = SheBeiZhanModel.GWID;
            this.label2.Visible = true;
            this.label2.Text = zhanModel.LineCode;
            if (SheBeiZhanModel.KJCanShuFuModel is GongZhanModel)
            {
                GongZhanModel = SheBeiZhanModel.KJCanShuFuModel as GongZhanModel;
            }
            try
            {
                string wenjian = Application.StartupPath + @"\TuPian\OK.jpg";
                if (File.Exists(wenjian))
                {
                    Image sok = Image.FromFile(wenjian);
                    OK = (Image)sok.Clone();
                    sok.Dispose();
                }
            }
            catch
            {


            }
            try
            {
                string wenjian = Application.StartupPath + @"\TuPian\NG.jpg";
                if (File.Exists(wenjian))
                {
                    Image sok = Image.FromFile(wenjian);
                    NG = (Image)sok.Clone();
                    sok.Dispose();
                }
            }
            catch
            {
            }
            SheBeiZhanModel.KJCanShuFuModel.ShuXinEvent += KJCanShuFuModel_ShuXinEvent;
        }

        private void KJCanShuFuModel_ShuXinEvent()
        {
            if (ZiYuan != null)
            {
                ZiYuan.It.FanXingGaiBing(() => {
                    ShuXin();
                });
            }
        }

        public void SetCanShu(EventType arg1, JieMianShiJianModel model)
        {
            if (TDID != model.GWID)
            {
                return;
            }
            switch (arg1)
            {            
                case EventType.ClearData:
                    {
                        this.pictureBox1.Visible = false;
                        this.dataGridView1.Rows.Clear();
                    }
                    break;
                case EventType.JiaZaiData:
                    {
                        YeWuDataModel modes = model.Data;
                        if (modes != null)
                        {
                            int index = this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[index].Cells[0].Value = modes.ItemName;
                            this.dataGridView1.Rows[index].Cells[1].Value = modes.Low.JiCunValue;
                            this.dataGridView1.Rows[index].Cells[2].Value = modes.Up.JiCunValue;
                            this.dataGridView1.Rows[index].Cells[3].Value = modes.Value.JiCunValue;
                            this.dataGridView1.Rows[index].Cells[4].Value = modes.IsShuJuHeGe ? "√" : "×";
                            this.dataGridView1.Rows[index].Cells[5].Value = modes.IsShangChuanHeGe ? "√" : "×";
                            this.dataGridView1.Rows[index].Height = 32;
                            if (modes.IsShuJuHeGe == false || modes.IsShangChuanHeGe == false)
                            {
                                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                            }
                            else
                            {
                                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Green;
                            }
                        }
                    }
                    break;
                case EventType.TangChuang:
                    {
                        SetTanChuan(model.GWID, model.TangChuangMsg);
                    }
                    break;
                default:
                    break;
            }
         
        }

        public void SetTanChuan(int tdid, string msg)
        {
            if (tdid == TDID)
            {
                ZiYuan.TiShiKuang(msg);
            }
        }

        private void FuZhiTuPian(bool jinzhanstate)
        {
            this.pictureBox1.BringToFront();
            if (jinzhanstate)
            {

                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                pictureBox1.Image = OK == null ? null : (Image)OK.Clone();
            }
            else
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                pictureBox1.Image = NG == null ? null : (Image)NG.Clone();
            }

            this.pictureBox1.Visible = true;
        }

     

        public void ShuXin()
        {
            if (GongZhanModel!=null)
            {
                if (GongZhanModel.KaiQiTest!=0)
                {
                    if (GongZhanModel.KaiQiTest==1)
                    {
                        this.label1.Text = "";
                        this.pictureBox1.Visible = false;
                        this.dataGridView1.Rows.Clear();
                    }
                    else  if (GongZhanModel.KaiQiTest == 2)
                    {
                        this.label1.Text = GongZhanModel.ErWeiMa;
                    }
                    else if (GongZhanModel.KaiQiTest == 3)
                    {
                        FuZhiTuPian(GongZhanModel.TestJieGuo);
                    }
                    GongZhanModel.KaiQiTest = 0;

                }
                if (this.label4.Text.Equals(GongZhanModel.MiaoSu)==false)
                {
                    this.label4.Text = GongZhanModel.MiaoSu;
                }
            }
        }

     

        public void Close()
        {

        }

        public Control GetKJ()
        {
            return this;
        }
    }
}
