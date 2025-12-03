using CommLei.DataChuLi;
using CommLei.JiChuLei;
using Common.SheBeiTeXing;
using JieMianLei.FuFrom;
using JieMianLei.FuFrom.KJ;
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
    public partial class JiChuLieBiaoWeiHuFrom<T> : BaseFuFrom where T:new()
    {
        private int LieShu = 0;
        private int JiShu = 1;
        private string ChaoZhaoMC = "";
        private bool IsMingXi = false;
        private Dictionary<string, string> DaoRu = new Dictionary<string, string>();

        private Func<List<T>, T, int> QueDingJieGuo=null;
        private Func<List<T>, T, int> ShanChu = null;

        private Func<List<T>, string, List<T>> ChaoZhao = null;

        public  Action<T> ChaKanMX;

        private List<T> ShuJu = new List<T>();
        public JiChuLieBiaoWeiHuFrom()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            MoveShiJianKJ se = new MoveShiJianKJ();
            se.BangDingMove(this.upanel2);
            this.uweiKuKJ1.DianJiEvent += WeiKuKJ1_DianJiEvent;
            this.ubiaoGeFenYeKJ1.ChuaJieGuoEvent += BiaoGeKJ1_ChuaJieGuoEvent;
        }

        /// <summary>
        /// 不显示菜单
        /// </summary>
        /// <param name="buxianshicaidan"></param>
        public void BuXianShiCaiDan(List<string> buxianshicaidan)
        {
            this.uweiKuKJ1.SetXianShi(buxianshicaidan);
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="biaoqi"></param>
        /// <param name="canshus"></param>
        /// <param name="buxiangshilie"></param>
        /// <param name="lieduiyingming"></param>
        public void SetCanShu(string biaoqi,string chazhaomc,bool isyoumingxi, List<T> canshus, Func<List<T>, T, int> queding, Func<List<T>, string, List<T>> chaozhao, Func<List<T>, T, int> shanchu) 
        {
            this.labFbiaoTi.Text = biaoqi;
            QueDingJieGuo = queding;
            ShanChu = shanchu;
            ChaoZhao = chaozhao;
            ChaoZhaoMC = chazhaomc;
            IsMingXi = isyoumingxi;
            DaoRuGN(isyoumingxi);
            if (canshus != null&& canshus.Count!=0)
            {
                ShuJu = ChangYong.FuZhiShiTi(canshus);
            }
            else
            {
                ShuJu = new List<T>();
            }
            this.ubiaoGeFenYeKJ1.FuZhi<T>(ShuJu);
        }

        private void DaoRuGN(bool isyoumingxi)
        {
            DaoRu.Clear();
            List<List<string>> lis = HuoQuSheBeiTeXing.GetSheBeiLie<T>();
            List<List<string>> lismingxi = HuoQuSheBeiTeXing.GetSheBeiXianShi<T>();
            List<JiChuLieModel> lislie = new List<JiChuLieModel>();
           
            for (int i = 0; i < lis.Count; i++)
            {
                if (lis[i].Count >= 4)
                {
                    if (DaoRu.ContainsKey(lis[i][0]) == false)
                    {
                        DaoRu.Add(lis[i][0], lis[i][1]);
                    }
                    JiChuLieModel model = new JiChuLieModel();
                    model.LieName = lis[i][0];
                    model.BangDingName = lis[i][1];
                    model.DiJiLie = ChangYong.TryInt(lis[i][2],1);
                    model.Kuan= ChangYong.TryFloat(lis[i][3], 100);
                    if (model.Kuan<=0)
                    {
                        model.Kuan = 100;
                    }
                    for (int c = 0; c < lismingxi.Count; c++)
                    {
                        if (lismingxi[c].Count>=3)
                        {
                            if (lismingxi[c][2].Equals(lis[i][1]))
                            {
                                model.XianShiZhi.Add(lismingxi[c][0], lismingxi[c][1]);
                              
                            }
                        }
                    }
                    lislie.Add(model);
                }
            }
            FromSortZX(lislie,true);
            SetLie(lislie,isyoumingxi);

        }
        /// <summary>
        ///  从小到大排序
        /// </summary>
        /// <param name="lisObj">集合</param>
        /// <param name="IsSort">为true表示从小到大，为false则是从大到小</param>
        private void FromSortZX(List<JiChuLieModel> lisObj, bool IsSort)
        {
            if (lisObj.Count > 0)
            {
                try
                {
                    JiChuLieModel obj = null;
                    for (int i = 0; i < lisObj.Count; i++)
                    {
                        for (int j = i + 1; j < lisObj.Count; j++)
                        {
                            if (IsSort)
                            {
                                if (lisObj[i].DiJiLie > lisObj[j].DiJiLie)
                                {
                                    obj = lisObj[i];
                                    lisObj[i] = lisObj[j];
                                    lisObj[j] = obj;

                                }
                            }
                            else
                            {
                                if (lisObj[i].DiJiLie < lisObj[j].DiJiLie)
                                {
                                    obj = lisObj[i];
                                    lisObj[i] = lisObj[j];
                                    lisObj[j] = obj;

                                }
                            }
                        }
                    }
                }
                catch
                {


                }

            }
        }
        public void SetKJ<C>(C kj) where C:Control,IFUCKJ<T>
        {
            if (kj != null)           
            {
                if (this.upanel4.Controls.Count <= 0)
                {
                    kj.Dock = DockStyle.Fill;
                    this.upanel4.Controls.Add(kj);
                }
            }
        }

        public List<T> GetCanShu()
        {
            return ShuJu;
        }

        protected override void GuanBi()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetLie(List<JiChuLieModel> lieduiyingming,bool ismingxi)
        {
            List<LieModel> lies = new List<LieModel>();
            
            if (lieduiyingming != null)
            {
                int liecount = 0;
                foreach (var item in lieduiyingming)
                {
                   
                    LieModel model = new LieModel();
                    model.BangDingName = item.BangDingName;
                    model.DiLie = liecount;
                    model.FullChang = item.Kuan;
                    model.IsKeJian = true;
                    model.IsShanChuHang = false;
                    model.IsZhiDu = true;
                    model.IsZhiXianShiBangDingName = false;
                    model.LeiXing = 1;
                    model.LieName = item.LieName;                
                    model.XianShiZhi = item.XianShiZhi;
                    lies.Add(model);
                    liecount++;
                }          
                if (ismingxi)
                {
                    LieModel model = new LieModel();
                    model.BangDingName = "明细";
                    model.DiLie = liecount;
                    model.FullChang = 30;
                    model.IsKeJian = true;
                    model.IsShanChuHang = false;
                    model.IsZhiDu = true;
                    model.IsZhiXianShiBangDingName = true;
                    model.LeiXing = 4;
                    model.LieName = "明细";
                    lies.Add(model);
                    liecount++;
                }
                {
                    LieModel model = new LieModel();
                    model.BangDingName = "修改";
                    model.DiLie = liecount;
                    model.FullChang = 30;
                    model.IsKeJian = true;
                    model.IsShanChuHang = false;
                    model.IsZhiDu = true;
                    model.IsZhiXianShiBangDingName = true;
                    model.LeiXing = 4;
                    model.LieName = "修改";
                    lies.Add(model);
                    liecount++;
                }
                {
                    LieModel model = new LieModel();
                    model.BangDingName = "删除";
                    model.DiLie = liecount;
                    model.FullChang = 30;
                    model.IsKeJian = true;
                    model.IsShanChuHang = false;
                    model.IsZhiDu = true;
                    model.IsZhiXianShiBangDingName = true;
                    model.LeiXing = 4;
                    model.LieName = "删除";
                    lies.Add(model);
                }
            }
            LieShu = lies.Count;
            this.ubiaoGeFenYeKJ1.AddLie(lies);
        }
        private void WeiKuKJ1_DianJiEvent(int biaozhi, string neirong)
        {
            if (biaozhi == 1)//查找
            {
                ChaoZhaoFrm fem = new ChaoZhaoFrm();
                fem.SetCanShu(ChaoZhaoMC);
                fem.ChaZhao = (x) => {
                    if (ChaoZhao!=null)
                    {
                        List<T> shu = ChaoZhao(ShuJu,x);
                        this.ubiaoGeFenYeKJ1.FuZhi<T>(shu);
                    }
                };
                fem.ShowDialog(this);
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
                        DuRuExclWenDan daoChuXlxsLei = new DuRuExclWenDan();
                        this.Waiting(() => {
                            ShuJu = daoChuXlxsLei.ShuChuExcelOrLisN<T>(wenjinnane, DaoRu);
                        }, "正在导入...", this.ubiaoGeFenYeKJ1);

                        if (ShuJu==null)
                        {
                            ShuJu = new List<T>();
                        }
                        this.ubiaoGeFenYeKJ1.FuZhi<T>(ShuJu);
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
                    DuRuExclWenDan daoChuXlxsLei = new DuRuExclWenDan();
                    Dictionary<string, List<object>> BiaoGe = this.ubiaoGeFenYeKJ1.GetDaoChuNeiRong();
                    this.Waiting(() => {
                       
                        daoChuXlxsLei.DaoChuExc(wenjinnane, BiaoGe);
                    }, "正在导出...", this.ubiaoGeFenYeKJ1);

                }
            }
            else if (biaozhi == 5)
            {
                HCLisDataLei<T>.Ceratei().LisWuLiao = GetCanShu();
                HCLisDataLei<T>.Ceratei().BaoCun();
                this.QiDongTiShiKuang("保存成功");
            }
        }
        private void BiaoGeKJ1_ChuaJieGuoEvent(object sender, DataGridViewRow row, int lie, bool isshanchu)
        {
            if (IsMingXi)
            {
                if (lie == LieShu - 3)
                {

                    if (row.Tag is T)
                    {
                        T model = (T)row.Tag;
                        if (ChaKanMX!=null)
                        {
                            ChaKanMX(model);
                        }

                    }
                    this.upanel3.Visible = true;
                }
            }
            if (lie == LieShu-2)
            {
               
                if (row.Tag is T)
                {
                    T model = (T)row.Tag;
                    if (this.upanel4.Controls.Count > 0)
                    {
                        if (this.upanel4.Controls[0] is IFUCKJ<T>)
                        {
                            (this.upanel4.Controls[0] as IFUCKJ<T>).SetCanShu(model);
                           
                        }
                    }

                }
                this.upanel3.Visible = true;
            }
            else if (lie == LieShu - 1)
            {
                if (row.Tag is T)
                {
                    T model = (T)row.Tag;
                    if (ShanChu != null)
                    {
                        int index = ShanChu(ShuJu, model);
                        if (index<ShuJu.Count)
                        {
                            ShuJu.RemoveAt(index);
                        }
                        this.ubiaoGeFenYeKJ1.FuZhi<T>(ShuJu);
                    }
                   
                }

            }
        }
        private  void XingZeng()
        {
            if (this.upanel4.Controls.Count>0)
            {
                if (this.upanel4.Controls[0] is IFUCKJ<T>)
                {
                    (this.upanel4.Controls[0] as IFUCKJ<T>).Clear();

                }
            }
           
            if (JiShu == 1)
            {
                JiShu++;
                this.upanel3.Location = new Point(this.Width / 2 - this.upanel3.Width / 2, this.Height / 2 - this.upanel3.Height / 2);

            }
            this.upanel3.Visible = true;
        }



        private void ubutton2_Click(object sender, EventArgs e)
        {
            this.upanel3.Visible = false;
        }

        private void ubutton4_Click(object sender, EventArgs e)
        {
            if (this.upanel4.Controls.Count > 0)
            {
                if (this.upanel4.Controls[0] is IFUCKJ<T>)
                {
                    T model = (this.upanel4.Controls[0] as IFUCKJ<T>).GetModel();
                    if (QueDingJieGuo != null)
                    {
                        int index = QueDingJieGuo(ShuJu, model);
                        if (index >= 0)
                        {
                            if (index< ShuJu.Count)
                            {
                                ShuJu[index] = model;
                            }
                        }
                        else
                        {
                            ShuJu.Add(model);
                        }
                    }
                    else
                    {
                        ShuJu.Add(model);
                    }
                    this.ubiaoGeFenYeKJ1.FuZhi<T>(ShuJu);
                }
            }
            this.upanel3.Visible = false;
        }
    }
}
