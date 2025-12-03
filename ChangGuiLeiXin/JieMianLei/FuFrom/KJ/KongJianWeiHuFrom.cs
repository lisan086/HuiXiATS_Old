using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.FuFrom.KJ
{
    public partial class KongJianWeiHuFrom : BaseFuFrom
    {
        private int JiShu = 1;
        public KongJianWeiHuFrom()
        {
            InitializeComponent();
            MoveShiJianKJ se = new MoveShiJianKJ();
            se.BangDingMove(this.upanel2);
            this.DoubleBuffered = true;
            this.xuanZeKJ1.DianJiKJEvent += WeiKuKJ1_DianJiEvent;
            this.upanel3.Visible = false;
        }
        private void WeiKuKJ1_DianJiEvent(int biaozhi, string neirong)
        {
            if (biaozhi == 1)
            {
                ChaZhao(neirong);

            }
            else if (biaozhi == 2)
            {
                XinZeng();
            }
            else if (biaozhi == 3)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "*.xlsx|*.xlsx";
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string wenjinnane = openFileDialog.FileName;
                    if (string.IsNullOrEmpty(wenjinnane) == false)
                    {
                        DaoRu(wenjinnane);
                    }//EXCEL模板里的数据添加到界面gridview成功！              
                }
            }
            else if (biaozhi == 4)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "*.xlsx|*.xlsx";
                string wenjinnane = "";
                saveFile.FileName = DateTime.Now.ToString("yyyy-MM-dd");
                if (saveFile.ShowDialog(this) == DialogResult.OK)
                {
                    wenjinnane = saveFile.FileName;


                }
                if (string.IsNullOrEmpty(wenjinnane) == false)
                {
                    DaoChu(wenjinnane);

                }
            }
            else if (biaozhi == 5)
            {
                List<Control> kj = this.xuanZeKJ1.GetCanShu();
                BaoCun(kj);
            }
        }
        #region 需要重写的方法
        protected virtual void Clear()
        {

        }
        protected virtual void ChaZhao(string chazhao)
        {

        }
        protected virtual void DaoRu(string wenjiannale)
        {

        }
        protected virtual void DaoChu(string wenjiannale)
        {

        }
        protected virtual void BaoCun(List<Control> kj)
        {

        }
        protected virtual void QueDing()
        {

        }
        protected virtual void XinZeng()
        {
            Clear();
            if (JiShu == 1)
            {
                JiShu++;
                this.upanel3.Location = new Point(this.Width / 2 - this.upanel3.Width / 2, this.Height / 2 - this.upanel3.Height / 2);

            }
            this.upanel3.Visible = true;
        }
        #endregion
        #region 继承调用的方法
        protected void XianShiPanl(bool xianshi)
        {
            if (JiShu == 1)
            {
                JiShu++;
                this.upanel3.Location = new Point(this.Width / 2 - this.upanel3.Width / 2, this.Height / 2 - this.upanel3.Height / 2);

            }
            this.upanel3.Visible = xianshi;
        }
        protected void JiaRuKJ(Control kj)
        {
            this.upanel4.Controls.Add(kj);
        }
        protected List<Control> GetCanShu()
        {
            return this.xuanZeKJ1.GetCanShu();
        }
        protected void AddKJ(Control kj)
        {
            this.xuanZeKJ1.AddKJ(kj);
        }
        protected void AddKJ(List<Control> kj)
        {
            this.xuanZeKJ1.SetCanShu(kj);
        }
        protected void BuXianShiKJ(List<string> kj)
        {
            this.xuanZeKJ1.BuXianShiKJ(kj);
        }
        #endregion

        private void ubutton2_Click(object sender, EventArgs e)
        {
            this.upanel3.Visible = false;
        }

        private void ubutton4_Click(object sender, EventArgs e)
        {
            QueDing();
        }
       
    }
}
