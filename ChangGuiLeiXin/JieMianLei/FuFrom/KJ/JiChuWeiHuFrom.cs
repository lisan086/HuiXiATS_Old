using CommLei.DataChuLi;
using Common.JieMianLei;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.FuFrom.KJ
{
    public partial class JiChuWeiHuFrom : BaseFuFrom
    {
        private int JiShu = 1;
        public JiChuWeiHuFrom()
        {
            InitializeComponent();
            MoveShiJianKJ se = new MoveShiJianKJ();
            se.BangDingMove(this.upanel2);
            this.DoubleBuffered = true;
            this.uweiKuKJ1.DianJiEvent += WeiKuKJ1_DianJiEvent;
            this.upanel3.Visible = false;
            this.ubiaoGeFenYeKJ1.ChuaJieGuoEvent += BiaoGeKJ1_ChuaJieGuoEvent;
        }
        private void BiaoGeKJ1_ChuaJieGuoEvent(object sender, DataGridViewRow row, int lie, bool isshanchu)
        {
            GengGai(sender, row, lie, isshanchu);
        }
        private void WeiKuKJ1_DianJiEvent(int biaozhi, string neirong)
        {
            if (biaozhi == 1)
            {
                ChaZhao(neirong);

            }
            else if (biaozhi == 2)
            {
                XingZeng();
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
                DaoChu(wenjinnane);
                
            }
            else if (biaozhi == 5)
            {
                BaoCun();
            }
        }

        #region 需要重写
        protected virtual void GengGai(object sender, DataGridViewRow row, int lie, bool isshanchu)
        {

        }
        protected virtual void Clear()
        {

        }

        protected virtual void ChaZhao(string chazhao)
        {

        }
        protected virtual void QueDing()
        {

        }

        protected virtual void BaoCun()
        {

        }
        protected virtual void DaoRu(string wenjiannale)
        {

        }
        protected virtual void DaoChu(string wenjinnane)
        {
            if (string.IsNullOrEmpty(wenjinnane) == false)
            {
                DuRuExclWenDan daoChuXlxsLei = new DuRuExclWenDan();
                this.Waiting(() => {
                    Dictionary<string, List<object>> BiaoGe = this.ubiaoGeFenYeKJ1.GetDaoChuNeiRong();
                    daoChuXlxsLei.DaoChuExc(wenjinnane, BiaoGe);
                }, "正在导出...", this.ubiaoGeFenYeKJ1);

            }
        }
        protected virtual void XingZeng()
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
        #region 继承调用
        protected void HeJi(Dictionary<string, string> jihelie)
        {
            this.ubiaoGeFenYeKJ1.HeJiNeiRong(jihelie);
        }
        protected void SetQueDingWenBen(string wenben)
        {
            this.ubutton4.Text = wenben;
        }
        protected void FuZhi<T>(List<T> jihelie)
        {
            this.ubiaoGeFenYeKJ1.FuZhi(jihelie);
        }
        protected void AddLie(List<LieModel> jihelie)
        {
            this.ubiaoGeFenYeKJ1.AddLie(jihelie);
        }
        protected void XianShiWenBen(List<string> xianshiwenben)
        {
            this.uweiKuKJ1.XianShiWenBen(xianshiwenben);
        }
        protected void XianShiPanl(bool xianshi)
        {
            if (JiShu == 1)
            {
                JiShu++;
                this.upanel3.Location = new Point(this.Width / 2 - this.upanel3.Width / 2, this.Height / 2 - this.upanel3.Height / 2);

            }
            this.upanel3.Visible = xianshi;
        }
        protected void AddKJ(Control kj)
        {
            this.upanel4.Controls.Add(kj);
        }
        protected void BuXianShiKJ(List<string> kj)
        {
            this.uweiKuKJ1.SetXianShi(kj);
        }
        protected void XianShiShuJu(int hangshu)
        {
            this.ubiaoGeFenYeKJ1.IniData(hangshu);
        }
        protected List<DataGridViewRow> GetData()
        {
            return this.ubiaoGeFenYeKJ1.GetData();
        }

        protected void AddLie<T>( Dictionary<int,Dictionary<string,string>> xianshizhi,bool isyoushanchu,bool isxiugai,bool isyoumingxi)
        {
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            List<string> ziduan = new List<string>();
            List<LieModel> lies = new List<LieModel>();
            int zuidalie = 0;
            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(LieTeXing), true))
                {
                    LieTeXing xi = (LieTeXing)item.GetCustomAttributes(true)[0];
                    LieModel model = new LieModel();
                    model.BangDingName = item.Name;
                    model.DiLie = xi.GetDiJiLie();
                    model.FullChang = xi.GetManKuan();
                    model.IsKeJian = xi.GetKeJian();
                    model.IsShanChuHang = false;
                    model.IsZhiDu = true;
                    model.IsZhiXianShiBangDingName = false;
                    model.LeiXing = 1;
                    model.LieName = xi.GetLieName();
                    lies.Add(model);
                    if(zuidalie < model.DiLie)
                    {
                        zuidalie = model.DiLie;
                    }
                    if (xianshizhi.ContainsKey(model.DiLie))
                    {
                        model.XianShiZhi = xianshizhi[model.DiLie];
                    }
                
                }
            }
            if (isyoumingxi)
            {
                LieModel model = new LieModel();
                model.BangDingName = "明细";
                model.DiLie = zuidalie+1;
                model.FullChang = 30;
                model.IsKeJian = true;
                model.IsShanChuHang = false;
                model.IsZhiDu = true;
                model.IsZhiXianShiBangDingName = true;
                model.LeiXing = 4;
                model.LieName = "明细";
                lies.Add(model);
                zuidalie++;
            }
            if (isxiugai)
            {
                LieModel model = new LieModel();
                model.BangDingName = "修改";
                model.DiLie = zuidalie +1;
                model.FullChang = 30;
                model.IsKeJian = true;
                model.IsShanChuHang = false;
                model.IsZhiDu = true;
                model.IsZhiXianShiBangDingName = true;
                model.LeiXing = 4;
                model.LieName = "修改";
                lies.Add(model);
                zuidalie++;
            }
            if (isyoushanchu)
            {
                LieModel model = new LieModel();
                model.BangDingName = "删除";
                model.DiLie = zuidalie + 1;
                model.FullChang = 30;
                model.IsKeJian = true;
                model.IsShanChuHang = true;
                model.IsZhiDu = true;
                model.IsZhiXianShiBangDingName = true;
                model.LeiXing = 4;
                model.LieName = "删除";
                lies.Add(model);
                zuidalie++;
            }
            this.ubiaoGeFenYeKJ1.AddLie(lies);
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
