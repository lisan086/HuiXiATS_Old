using BaseUI.DaYIngMoBan.Lei;
using BaseUI.DaYIngMoBan.Model;
using BaseUI.DaYIngMoBan.TuXing;
using CommLei.JiChuLei;
using JieMianLei.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.DaYIngMoBan.Frm
{
    public partial class DaFromN : BaseFrom
    {
        private bool AnXia = false;
        private string LuJing = "";

        /// <summary>
        /// 重新做的
        /// </summary>
        private List<FuTuLei> ChongXinLFuTu = new List<FuTuLei>();

        /// <summary>
        /// 对齐
        /// </summary>
        private List<FuTuLei> DuiQiFuTu = new List<FuTuLei>();
        /// <summary>
        /// 临时增加用的
        /// </summary>
        private FuTuLei LianShiTu = null;

        private DaYingLeiXin<TestModel, TestModel> DaYingLeiXin;
        private Point Point = new Point(0, 0);
        private JiaZaiMoXing MuBanHelper=JiaZaiMoXing.Ceratei();
        public DaFromN()
        {
            InitializeComponent();
            this.lstbMingXi.Items.Clear();
        }
        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (AnXia)
            {
                AnXia = false;
                if (LianShiTu != null)
                {
                    LianShiTu.SetWeiZhi(new Point(e.X, e.Y), 2);
                    ChongXinLFuTu.Add(LianShiTu.FuZhi(LianShiTu));
                    LianShiTu = null;
                   (sender as Control).Refresh();
                }
               
            }        
            this.flowLayoutPanel1.Controls[0].Cursor = Cursors.Default;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (AnXia)
            {
                if (LianShiTu != null)
                {
                    LianShiTu.SetWeiZhi(new Point(e.X, e.Y), 2);
                    (sender as Control).Refresh();
                }
                else
                {
                    int x = e.X - Point.X;
                    int y = e.Y - Point.Y;
                    Point.X = e.X;
                    Point.Y = e.Y;
                    YiDong(x, y);
                    (sender as Control).Refresh();
                }
            }
            else
            {
                ShuBianFangXiang(new Point(e.X, e.Y));
            }
         
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {         
            if (LianShiTu!=null)
            {
                LianShiTu.SetWeiZhi(new Point(e.X, e.Y), 1);             
                AnXia = true;
            }
            else
            {
                if (ChongXinLFuTu.Count > 0)
                {
                    if (this.flowLayoutPanel1.Controls[0].Cursor == Cursors.Default)
                    {

                        XuanZhongYiGe(new Point(e.X, e.Y));
                        (sender as Control).Refresh();
                    }
                    else
                    {
                        Point.X = e.X;
                        Point.Y = e.Y;
                        AnXia = true;
                    }
                }
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            if (ChongXinLFuTu.Count > 0)
            {           
                foreach (var item in ChongXinLFuTu)
                {
                   
                    item.HuaZiJi(e.Graphics);
                }
            }
            try
            {
                if (LianShiTu!=null)
                {
                    LianShiTu.HuaZiJi(e.Graphics);
                }
            }
            catch 
            {

               
            }
           
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            DaYingMoBanSFrom chuanti = new DaYingMoBanSFrom();
            if (chuanti.ShowDialog(this) == DialogResult.OK)
            {
                ZBiaoQianModel model = ChangYong.FuZhiShiTi(chuanti.ZBiaoQianModel);
                if (model != null && model.name != "")
                {
                    JiaZaiMoBan(model, "");
                }
                else
                {
                    this.QiDongTiShiKuang("模板名称没有");
                }
            }
        }

        private void 直线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                JiaRuShiLi(TuLeiType.ZhiXian);
            }
            else
            {
                this.QiDongTiShiKuang("没有建立模板");
            }
        }

        private void 矩形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                JiaRuShiLi(TuLeiType.JuXing);
            }
            else
            {
                this.QiDongTiShiKuang("没有建立模板");
            }
        }

        private void 文本控件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                JiaRuShiLi(TuLeiType.WenZi);
            }
            else
            {
                this.QiDongTiShiKuang("没有建立模板");
            }
        }
        private void 二维码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                JiaRuShiLi(TuLeiType.ErWeiMa);
            }
            else
            {
                this.QiDongTiShiKuang("没有建立模板");
            }
        }
        private void lstbBianLiang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstbBianLiang.Items.Count > 0)
            {
                int index = this.lstbBianLiang.SelectedIndex;
                if (index >= 0)
                {
                    if (this.propertyGrid1.SelectedObject != null)
                    {
                        FuTuLei fu = this.propertyGrid1.SelectedObject as FuTuLei;
                        if (fu != null)
                        {
                            fu.BanDingText = this.lstbBianLiang.Items[index].ToString();
                            this.propertyGrid1.SelectedObject = fu;
                        }
                    }
                }
            }
        }

        private void lstbMingXi_DoubleClick(object sender, EventArgs e)
        {
            if (this.lstbMingXi.Items.Count > 0)
            {
                int index = this.lstbMingXi.SelectedIndex;
                if (index >= 0)
                {
                    if (this.propertyGrid1.SelectedObject != null)
                    {
                        FuTuLei fu = this.propertyGrid1.SelectedObject as FuTuLei;
                        if (fu != null)
                        {
                            fu.BanDingText = this.lstbMingXi.Items[index].ToString();
                            this.propertyGrid1.SelectedObject = fu;
                        }
                    }
                }
            }
        }

      
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var item = propertyGrid1.SelectedObject;
            if (item != null)
            {
                string wenjian = e.ChangedItem.Parent.Label;
                object b = e.ChangedItem.Parent.Value;
                SetPropertyVisibility(item, wenjian, b);//WindowSize必须是自定义属性中的属性名，如下也是
                propertyGrid1.SelectedObject = item;
                if (this.flowLayoutPanel1.Controls.Count > 0)
                {
                    this.flowLayoutPanel1.Controls[0].Refresh();
                }
            }
        }
        private void SetPropertyVisibility(object obj, string propertyName, object visible)
        {
            Type type = obj.GetType();
            PropertyInfo[] shuxing = type.GetProperties();
            foreach (PropertyInfo item in shuxing)
            {
                if (item.Name == propertyName)
                {
                    item.SetValue(obj, visible);
                }
            }
        }

      

        private void button7_Click(object sender, EventArgs e)
        {
            int bianhao = ChangYong.TryInt(this.textBox1.Text, -1);
            if (bianhao >= 0)
            {
                if (ChongXinLFuTu.Count > 0)
                {
                    for (int i = 0; i < ChongXinLFuTu.Count; i++)
                    {
                        if (ChongXinLFuTu[i].TongShuID == bianhao)
                        {
                            ChongXinLFuTu[i].IsXuanZhong = true;
                            if (DuiQiFuTu.IndexOf(ChongXinLFuTu[i]) < 0)
                            {
                                DuiQiFuTu.Add(ChongXinLFuTu[i]);
                            }
                        }
                    }                
                    if (this.flowLayoutPanel1.Controls.Count > 0)
                    {
                        (this.flowLayoutPanel1.Controls[0]).Refresh();
                    }

                }
            }
            else
            {
                if (ChongXinLFuTu.Count > 0)
                {
                    for (int i = 0; i < ChongXinLFuTu.Count; i++)
                    {
                        ChongXinLFuTu[i].IsXuanZhong = true;
                    }
                   
                    if (this.flowLayoutPanel1.Controls.Count > 0)
                    {
                        (this.flowLayoutPanel1.Controls[0]).Refresh();
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZhiXianCaoZuo(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZhiXianCaoZuo(2);
        }



        private void button3_Click(object sender, EventArgs e)
        {
            int juli = ChangYong.TryInt(this.textBox2.Text, 1);

            ZhengTiYiDong(0,-Math.Abs(juli));
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int juli = ChangYong.TryInt(this.textBox2.Text, 1);
            ZhengTiYiDong(0,Math.Abs(juli));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int juli = ChangYong.TryInt(this.textBox2.Text, 1);
            ZhengTiYiDong(-Math.Abs(juli),0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int juli = ChangYong.TryInt(this.textBox2.Text, 1);
            ZhengTiYiDong(Math.Abs(juli), 0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
           
            int bianhao = ChangYong.TryInt(this.textBox3.Text, -2);

            FuZhiTu(bianhao);
        }

    

        private void 加载ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string name = openFileDialog.FileName;
              
                bool pts = JiaZaiMoXing.Ceratei().LoadMoXing(name);
                if (pts)
                {
                    ZBiaoQianModel pt = JiaZaiMoXing.Ceratei().LisWuLiao;
                    JiaZaiMoBan(pt, name);

                }
                else
                {
                    this.QiDongTiShiKuang("加载模版失败");
                }
            }
        }

        private void 图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                JiaRuShiLi(TuLeiType.TuPian);
            }
            else
            {
                this.QiDongTiShiKuang("没有建立模板");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
           

        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lujing = LuJing;
            LuJing = "";
            BaoCunMuBan();
            LuJing = lujing;
        }

        private void 生成modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelFrm modelFrm = new ModelFrm();
            List<string> ziduans = new List<string>();
            List<string> mixis = new List<string>();//*&
            for (int i = 0; i < this.lstbBianLiang.Items.Count; i++)
            {
                ziduans.Add(this.lstbBianLiang.Items[i].ToString().Replace("?", ""));
            }
            for (int i = 0; i < this.lstbMingXi.Items.Count; i++)
            {
                mixis.Add(this.lstbMingXi.Items[i].ToString().Replace("*&", ""));
            }
            modelFrm.SetCanShu(ziduans, mixis);
            modelFrm.Show(this);
        }

        private void 保存模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCunMuBan();
        }

        private void 打印预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCunMuBan(false);
            DaYingLeiXin = new DaYingLeiXin<TestModel, TestModel>();
            DaYingLeiXin.DaYingYuLan(new TestModel(), new List<TestModel>(), 1, "", false);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            DuiQiCaoZuo(1);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            DuiQiCaoZuo(2);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DuiQiCaoZuo(3);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DuiQiCaoZuo(4);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            WenZiCaoZuo(1);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            WenZiCaoZuo(2);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            WenZiCaoZuo(3);
        }

        private void 删除标签ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShanChuBiaoQian();
        }

        private void 清除所有标签ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = null;
            ChongXinLFuTu.Clear();
            this.flowLayoutPanel1.Controls[0].Cursor = Cursors.Default;

            this.flowLayoutPanel1.Controls[0].Refresh();
        }


        #region 代码整理
        /// <summary>
        /// 加载与新建模版
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="lujing"></param>
        private void JiaZaiMoBan(ZBiaoQianModel pt,string lujing)
        {
            LuJing = lujing;
            ClearK();
            this.Text = string.Format("{0}:正在设计:{1}", pt.name, pt.DaYingMoShi == 2 ? "横着打印" : "纵向打印");
            this.Tag = pt.DaYingMoShi;
            AddK(pt.KuanDu, pt.GaoDu);         
            this.lsbName.Items.Add(pt.name);
            string[] zhi = pt.ZiDuan.Split(',');
            bool isyoudanqian = false;
            for (int i = 0; i < zhi.Length; i++)
            {
                if (zhi[i] == "")
                {
                    continue;
                }
                if (zhi[i].Equals("?DanQianShi"))
                {
                    isyoudanqian = true;
                }
                this.lstbBianLiang.Items.Add(zhi[i]);
            }
            if (isyoudanqian==false)
            {
                this.lstbBianLiang.Items.Add("?DanQianShi");
            }
            string[] zhis = pt.MingXiZiDuan.Split(',');
            for (int i = 0; i < zhis.Length; i++)
            {
                if (zhis[i] == "")
                {
                    continue;
                }
                this.lstbMingXi.Items.Add(zhis[i]);
            }
         
            #region 开始会图
            foreach (BiaoQian lc in pt.lc)
            {
                JiaRuShiLi(lc.Type, lc);
            }
            foreach (BiaoQian lc in pt.MingXian)
            {
                JiaRuShiLi(lc.Type, lc);

            }
            #endregion

            this.flowLayoutPanel1.Controls[0].Cursor = Cursors.Default;

            this.flowLayoutPanel1.Controls[0].Refresh();
        }

        /// <summary>
        /// 保存模版 与另存为模版 ，还有打印预览模板 
        /// </summary>
        /// <param name="isBaoCunMuBan"></param>
        private void BaoCunMuBan(bool isBaoCunMuBan = true)
        {
            if (this.flowLayoutPanel1.Controls.Count <= 0)
            {
                this.QiDongTiShiKuang("还没有新建模板");
                return;
            }
            ZBiaoQianModel muBanCanShu = new ZBiaoQianModel();
            muBanCanShu.GaoDu = this.flowLayoutPanel1.Controls[0].Height;
            muBanCanShu.KuanDu = this.flowLayoutPanel1.Controls[0].Width;
            muBanCanShu.name = this.Text.Split(':')[0];
            muBanCanShu.DaYingMoShi = ChangYong.TryInt(this.Tag, 1);
            muBanCanShu.IsCaiYongTuPianShuChu = false;
            {
                StringBuilder ziduan = new StringBuilder();
                for (int i = 0; i < this.lstbBianLiang.Items.Count; i++)
                {
                    if (i == this.lstbBianLiang.Items.Count - 1)
                    {
                        ziduan.Append(string.Format("{0}", this.lstbBianLiang.Items[i]));
                    }
                    else
                    {
                        ziduan.Append(string.Format("{0},", this.lstbBianLiang.Items[i]));
                    }

                }
                muBanCanShu.ZiDuan = ziduan.ToString();
            }
            {
                StringBuilder ziduan = new StringBuilder();
                for (int i = 0; i < this.lstbMingXi.Items.Count; i++)
                {
                    if (i == this.lstbMingXi.Items.Count - 1)
                    {
                        ziduan.Append(string.Format("{0}", this.lstbMingXi.Items[i]));
                    }
                    else
                    {
                        ziduan.Append(string.Format("{0},", this.lstbMingXi.Items[i]));
                    }

                }
                muBanCanShu.MingXiZiDuan = ziduan.ToString();
            }

            muBanCanShu.lc = new List<BiaoQian>();
            muBanCanShu.MingXian = new List<BiaoQian>();
            foreach (var item in ChongXinLFuTu)
            {
                bool minxi = false;
                BiaoQian model = item.GetModel(out minxi);
                if (minxi == false)
                {
                    if (model != null)
                    {
                        muBanCanShu.lc.Add(model);
                    }
                }
                else
                {
                    if (model != null)
                    {
                        muBanCanShu.MingXian.Add(model);
                    }
                }

            }
            MuBanHelper.LisWuLiao = muBanCanShu;
            if (isBaoCunMuBan)
            {
                if (string.IsNullOrEmpty(LuJing) == false)
                {
                    MuBanHelper.BaoCun(LuJing);
                }
                else
                {
                    SaveFileDialog wendan = new SaveFileDialog();
                    wendan.Filter = "*.txt|*.txt";
                    if (wendan.ShowDialog(this) == DialogResult.OK)
                    {
                        string wenjiangeishi = wendan.FileName;

                        LuJing = wenjiangeishi;
                        MuBanHelper.BaoCun(wenjiangeishi);
                    }
                }

            }
        }


        /// <summary>
        /// 删除模版
        /// </summary>
        private void ShanChuBiaoQian()
        {
            List<FuTuLei> shuju = new List<FuTuLei>();
            foreach (var item in ChongXinLFuTu)
            {
                if (item.IsXuanZhong)
                {
                    shuju.Add(item);

                }
            }
            for (int i = 0; i < shuju.Count; i++)
            {
                ChongXinLFuTu.Remove(shuju[i]);
            }
            if (shuju.Count > 0)
            {
                this.propertyGrid1.SelectedObject = null;
            }
            this.flowLayoutPanel1.Controls[0].Refresh();
        }

        /// <summary>
        /// 总清理
        /// </summary>
        private void ClearK()
        {
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                this.flowLayoutPanel1.Controls[0].MouseUp -= Panel_MouseUp;
                this.flowLayoutPanel1.Controls[0].Paint -= Panel_Paint;
                this.flowLayoutPanel1.Controls[0].MouseDown -= Panel_MouseDown;
                this.flowLayoutPanel1.Controls[0].MouseMove -= Panel_MouseMove;
            }

            ChongXinLFuTu.Clear();
            this.lsbName.Items.Clear();
            this.lstbMingXi.Items.Clear();
            this.flowLayoutPanel1.Controls.Clear();
            this.lstbBianLiang.Items.Clear();          
            this.propertyGrid1.SelectedObject = null;
        }

        /// <summary>
        /// 增加画板
        /// </summary>
        /// <param name="Kuan"></param>
        /// <param name="Gao"></param>
        private void AddK(int Kuan, int Gao)
        {
            PanLMoBan panel = new PanLMoBan();
            panel.Size = new Size(Kuan, Gao);
            panel.Paint += Panel_Paint;
            panel.MouseDown += Panel_MouseDown;
            panel.MouseMove += Panel_MouseMove;
            panel.MouseUp += Panel_MouseUp;
            this.flowLayoutPanel1.Controls.Add(panel);
        }

        /// <summary>
        /// 加入控件
        /// </summary>
        /// <param name="tuLeiType"></param>
        private void JiaRuShiLi(TuLeiType tuLeiType)
        {
            AllBuXuan();
            FuTuLei fu = FuTuLei.JianLiMoXing(tuLeiType);
            if (fu != null)
            {
                LianShiTu= fu;
                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.UpArrow;
                this.flowLayoutPanel1.Controls[0].Refresh();
            }
        }
        /// <summary>
        /// 模版加进来用的
        /// </summary>
        /// <param name="type"></param>
        /// <param name="biaoQian"></param>
        private void JiaRuShiLi(int type, BiaoQian biaoQian)
        {
            FuTuLei futu = FuTuLei.FanHuiTuXin(biaoQian);
            if (futu != null)
            {
                if (!(biaoQian.Width == 0 && biaoQian.Gao == 0))
                {
                    ChongXinLFuTu.Add(futu);
                }
              
            }

        }

        private void AllBuXuan()
        {
            DuiQiFuTu.Clear();
            this.textBox1.Clear();
            this.textBox3.Clear();
            this.propertyGrid1.SelectedObject = null;
            this.flowLayoutPanel1.Controls[0].Cursor = Cursors.Default;
            if (ChongXinLFuTu.Count > 0)
            {
                foreach (var item in ChongXinLFuTu)
                {
                    item.IsXuanZhong = false;
                }
            }
        }

        private void XuanZhongYiGe(Point point)
        {
            AllBuXuan();
           
            if (ChongXinLFuTu.Count > 0)
            {
                int count = ChongXinLFuTu.Count;
               
                for (int i = count - 1; i >= 0; i--)
                {
                    if (ChongXinLFuTu[i].IsShuBiaoShiFouZai(point))
                    {
                        ChongXinLFuTu[i].IsXuanZhong = true;
                        this.propertyGrid1.SelectedObject = ChongXinLFuTu[i];
                        this.textBox1.Text = ChongXinLFuTu[i].TongShuID.ToString();
                        this.textBox3.Text = ChongXinLFuTu[i].TongShuID.ToString();                                           
                        break;
                    }
                }
               
            }

        }
        private void ShuBianFangXiang(Point point)
        {
            for (int i = 0; i < ChongXinLFuTu.Count; i++)
            {
                if (ChongXinLFuTu[i].IsXuanZhong)
                {
                    JianTouNS jianTouNS = ChongXinLFuTu[i].GetShuBiaoNS(point);
                    switch (jianTouNS)
                    {
                        case JianTouNS.Shang:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeNS;
                            }
                            break;
                        case JianTouNS.Xia:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeNS;
                            }
                            break;
                        case JianTouNS.Zuo:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeWE;
                            }
                            break;
                        case JianTouNS.You:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeWE;
                            }
                            break;
                        case JianTouNS.Zuo1ShangXia:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeNWSE;
                            }
                            break;
                        case JianTouNS.Zuo2ShangXia:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeNESW;
                            }
                            break;
                        case JianTouNS.You1ShangXia:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeNESW;
                            }
                            break;
                        case JianTouNS.You2ShangXia:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeNWSE;
                            }
                            break;
                        case JianTouNS.Wu:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.Default;
                            }
                            break;
                        case JianTouNS.QuBu:
                            {
                                this.flowLayoutPanel1.Controls[0].Cursor = Cursors.SizeAll;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        private void YiDong(int x, int y)
        {
            List<FuTuLei> kj = new List<FuTuLei>();
            JianTouNS jianTouNS = JianTouNS.Wu;
            for (int i = 0; i < ChongXinLFuTu.Count; i++)
            {
                if (ChongXinLFuTu[i].IsXuanZhong)
                {
                    FuTuLei tu = ChongXinLFuTu[i];
                    JianTouNS jianTouNSs = tu.JianTouNS;
                    if (jianTouNSs != JianTouNS.Wu)
                    {
                        jianTouNS = jianTouNSs;
                    }
                    kj.Add(tu);
                       
                }
            }

            for (int i = 0; i < kj.Count; i++)
            {
                FuTuLei tu = kj[i];
               
                switch (jianTouNS)
                {
                    case JianTouNS.Shang:
                        {
                            tu.D1point = new Point(tu.D1point.X, tu.D1point.Y + y);
                        }
                        break;
                    case JianTouNS.Xia:
                        {
                            tu.D2point = new Point(tu.D2point.X, tu.D2point.Y + y);
                        }
                        break;
                    case JianTouNS.Zuo:
                        {
                            tu.D1point = new Point(tu.D1point.X + x, tu.D1point.Y);
                        }
                        break;
                    case JianTouNS.You:
                        {
                            tu.D2point = new Point(tu.D2point.X + x, tu.D2point.Y);
                        }
                        break;
                    case JianTouNS.Zuo1ShangXia:
                        {
                            tu.D1point = new Point(tu.D1point.X + x, tu.D1point.Y + y);
                        }
                        break;
                    case JianTouNS.Zuo2ShangXia:
                        {

                            tu.D1point = new Point(tu.D1point.X + x, tu.D1point.Y);
                            tu.D2point = new Point(tu.D2point.X, tu.D2point.Y + y);

                        }
                        break;
                    case JianTouNS.You1ShangXia:
                        {
                            if (tu.Type == TuLeiType.ZhiXian)
                            {
                                tu.D2point = new Point(tu.D2point.X + x, tu.D2point.Y + y);
                            }
                            else
                            {
                                tu.D2point = new Point(tu.D2point.X + x, tu.D2point.Y);
                                tu.D1point = new Point(tu.D1point.X, tu.D1point.Y + y);
                            }

                        }
                        break;
                    case JianTouNS.You2ShangXia:
                        {
                            tu.D2point = new Point(tu.D2point.X + x, tu.D2point.Y + y);
                        }
                        break;
                    case JianTouNS.QuBu:
                        {
                            tu.D2point = new Point(tu.D2point.X + x, tu.D2point.Y + y);
                            tu.D1point = new Point(tu.D1point.X + x, tu.D1point.Y + y);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void ZhengTiYiDong(int x, int y)
        {
            if (ChongXinLFuTu.Count > 0)
            {
                for (int i = 0; i < ChongXinLFuTu.Count; i++)
                {
                    if (ChongXinLFuTu[i].IsXuanZhong)
                    {
                        FuTuLei tu = ChongXinLFuTu[i];
                        tu.SetWeiZhi(new Point(tu.D1point.X + x, tu.D1point.Y + y), 1);
                        tu.SetWeiZhi(new Point(tu.D2point.X + x, tu.D2point.Y + y), 2);
                    }
                }
                if (this.flowLayoutPanel1.Controls.Count > 0)
                {
                    (this.flowLayoutPanel1.Controls[0]).Refresh();
                }
            }
        }
        private void ZhiXianCaoZuo(int biaozhi)
        {
            if (ChongXinLFuTu.Count > 0)
            {
                int diyige = 0;
                for (int i = 0; i < ChongXinLFuTu.Count; i++)
                {
                    if (ChongXinLFuTu[i].IsXuanZhong)
                    {
                        if (ChongXinLFuTu[i].Type == TuLeiType.ZhiXian)
                        {
                            if (biaozhi == 1)
                            {
                                ChongXinLFuTu[i].SetWeiZhi(new Point(ChongXinLFuTu[i].D1point.X, ChongXinLFuTu[i].D2point.Y), 2);
                            }
                            else if (biaozhi == 2)
                            {
                                ChongXinLFuTu[i].SetWeiZhi(new Point(ChongXinLFuTu[i].D2point.X, ChongXinLFuTu[i].D1point.Y), 2);
                            }
                            if (diyige == 0)
                            {
                                diyige = 1;
                                this.propertyGrid1.SelectedObject = ChongXinLFuTu[i];
                            }

                        }
                    }
                }


                if (this.flowLayoutPanel1.Controls.Count > 0)
                {
                    (this.flowLayoutPanel1.Controls[0]).Refresh();
                }

            }
        }

        private void WenZiCaoZuo(int biaozhi)
        {
            if (ChongXinLFuTu.Count > 0)
            {
              
                for (int i = 0; i < ChongXinLFuTu.Count; i++)
                {
                    if (ChongXinLFuTu[i].IsXuanZhong)
                    {
                        if (ChongXinLFuTu[i].Type == TuLeiType.WenZi)
                        {
                            if (ChongXinLFuTu[i].Type == TuLeiType.WenZi)
                            {
                                DrawText draw = ChongXinLFuTu[i] as DrawText;
                                draw.IsJuZhong = biaozhi;
                            }

                        }
                    }
                }


                if (this.flowLayoutPanel1.Controls.Count > 0)
                {
                    (this.flowLayoutPanel1.Controls[0]).Refresh();
                }

            }
        }

        private void FuZhiTu(int bianhao)
        {
            List<FuTuLei> shuju = new List<FuTuLei>();
            for (int i = 0; i < ChongXinLFuTu.Count; i++)
            {
                if (ChongXinLFuTu[i].TongShuID == bianhao)
                {
                    FuTuLei fuTuLei = ChongXinLFuTu[i].FuZhi(ChongXinLFuTu[i]);
                    if (fuTuLei != null)
                    {
                        shuju.Add(fuTuLei);
                    }

                }
            }
            AllBuXuan();
            int zuidai = ZuiDaBianHoa();
            for (int i = 0; i < shuju.Count; i++)
            {
                shuju[i].TongShuID = zuidai;
                ChongXinLFuTu.Add(shuju[i]);
                shuju[i].IsXuanZhong = true;

            }


            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                (this.flowLayoutPanel1.Controls[0]).Refresh();
            }
        }

        private int ZuiDaBianHoa()
        {
            int bianhao = -1;
            for (int i = 0; i < ChongXinLFuTu.Count; i++)
            {
                if (ChongXinLFuTu[i].TongShuID > bianhao)
                {
                    bianhao = ChongXinLFuTu[i].TongShuID;
                }
            }
            return bianhao + 1;
        }

        private void DuiQiCaoZuo(int biaoshi)
        {
            if (DuiQiFuTu.Count > 0)
            {
                FuTuLei shouge = null;
                List<FuTuLei> lisxuanzhong = new List<FuTuLei>();
                for (int i = 0; i < DuiQiFuTu.Count; i++)
                {
                    if (DuiQiFuTu[i].IsXuanZhong)
                    {
                        if (shouge == null)
                        {
                            shouge = DuiQiFuTu[i];
                        }
                        else
                        {
                            lisxuanzhong.Add(DuiQiFuTu[i]);
                        }
                    }
                }
                if (shouge != null)
                {
                    for (int i = 0; i < lisxuanzhong.Count; i++)
                    {
                        if (lisxuanzhong[i].IsXuanZhong)
                        {
                            if (lisxuanzhong[i].Equals(shouge) == false)
                            {
                                if (biaoshi == 1)
                                {
                                    int chazhi = lisxuanzhong[i].D1point.Y - shouge.D1point.Y;
                                    lisxuanzhong[i].SetWeiZhi(new Point(lisxuanzhong[i].D1point.X, shouge.D1point.Y), 1);
                                    lisxuanzhong[i].SetWeiZhi(new Point(lisxuanzhong[i].D2point.X, lisxuanzhong[i].D2point.Y - chazhi), 2);
                                }
                                else if (biaoshi == 2)
                                {
                                    int chazhi = lisxuanzhong[i].D2point.Y - shouge.D2point.Y;
                                    lisxuanzhong[i].SetWeiZhi(new Point(lisxuanzhong[i].D1point.X, lisxuanzhong[i].D1point.Y - chazhi), 1);
                                    lisxuanzhong[i].SetWeiZhi(new Point(lisxuanzhong[i].D2point.X, shouge.D2point.Y), 2);
                                }
                                else if (biaoshi == 3)
                                {
                                    int chazhi = lisxuanzhong[i].D1point.X - shouge.D1point.X;
                                    lisxuanzhong[i].SetWeiZhi(new Point(shouge.D1point.X, lisxuanzhong[i].D1point.Y), 1);
                                    lisxuanzhong[i].SetWeiZhi(new Point(lisxuanzhong[i].D2point.X - chazhi, lisxuanzhong[i].D2point.Y), 2);
                                }
                                else if (biaoshi == 4)
                                {
                                    int chazhi = lisxuanzhong[i].D2point.X - shouge.D2point.X;
                                    lisxuanzhong[i].SetWeiZhi(new Point(lisxuanzhong[i].D1point.X - chazhi, lisxuanzhong[i].D1point.Y), 1);
                                    lisxuanzhong[i].SetWeiZhi(new Point(shouge.D2point.X, lisxuanzhong[i].D2point.Y), 2);
                                }
                            }
                        }
                    }
                    AllBuXuan();
                }
              
                if (this.flowLayoutPanel1.Controls.Count > 0)
                {
                    (this.flowLayoutPanel1.Controls[0]).Refresh();
                }
            }
        }
        #endregion
    }
}
