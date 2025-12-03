using CommLei.DataChuLi;
using CommLei.JiChuLei;
using Common.SheBeiTeXing;
using JieMianLei.FuFrom;
using JieMianLei.FuFrom.KJ;
using JieMianLei.UC;
using System;
using System.Collections;
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
    public partial class JiChuKJWeiHuFrm<T,V> : BaseFuFrom where V:new() where T:Control,IFUCKJ<V>,new()
    {
        /// <summary>
        /// true 是采用本地保存
        /// </summary>
        public bool IsCaiYongBenDiBaoCun { get; set; } = true;
        private bool KongZhiChuFa = false;
        private List<T> LisKJ = new List<T>();
        private Dictionary<string, string> DaoRu = new Dictionary<string, string>();
        public JiChuKJWeiHuFrm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.uweiKuKJ1.DianJiEvent += WeiKuKJ1_DianJiEvent;        
            this.ucDaoHangKongJian1.Rows = 70;       
            this.ucDaoHangKongJian1.UCClilk += UcDaoHangKongJian1_UCClilk;
            this.BuXianShiCaiDan(new List<string>() { "查找"});
        }
        private void UcDaoHangKongJian1_UCClilk(object sender, DaoHangModel e)
        {

            GengShuJu(e.Row,e.Index);
        }
        public void SetRowCount(uint shuju)
        {
            this.ucDaoHangKongJian1.Rows = (int)shuju;
        }
        public void BuXianCaiDan(List<string> buxuyaocaidanname)
        {
            this.BuXianShiCaiDan(buxuyaocaidanname);
            if (buxuyaocaidanname.IndexOf("排序")>=0)
            {
                this.button1.Visible = false;
            }
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
        public void SetCanShu(string biaoqi,  List<V> canshus)
        {
            this.labFbiaoTi.Text = biaoqi;
            DaoRuGN();
            LisKJ.Clear();
            int allpage = canshus.Count;
            this.ucDaoHangKongJian1.AllPage = ChangYong.PageYeShu(this.ucDaoHangKongJian1.Rows, allpage);
            int row = this.ucDaoHangKongJian1.Rows;
            List<T> jiajinqu = new List<T>();
            for (int i = 0; i < canshus.Count; i++)
            {
                T kj = new T();
                kj.SetCanShu(canshus[i]);
                LisKJ.Add(kj);
                if (i<row)
                {
                    jiajinqu.Add(kj);
                }
            }
            JiaZaiKJ(jiajinqu);
        }
        private void JiaZaiKJ(List<T> kjs)
        {
            KongZhiChuFa = true;
            if (this.fGenSui1.Controls.Count > 0)
            {
                this.fGenSui1.Controls.Clear();
            }
            if (kjs.Count > 0)
            {
                this.fGenSui1.Controls.AddRange(kjs.ToArray());
            }
            KongZhiChuFa = false;
        }

        private void GengShuJu(int yihangshu,int row)
        {
            JiaZaiKJ(new List<T>());
            int index = row;
            int rowcount = yihangshu;
            int count = LisKJ.Count;
            List<T> shuju = new List<T>();
            for (int i = rowcount * (index); i < (index + 1) * rowcount; i++)
            {
                if (i >= count)
                {
                    break;
                }
                shuju.Add(LisKJ[i]);
            }
            JiaZaiKJ(shuju);
        }
        private void DaoRuGN()
        {
            DaoRu.Clear();
            List<List<string>> lis = HuoQuSheBeiTeXing.GetSheBeiLie<V>();
            for (int i = 0; i < lis.Count; i++)
            {
                if (lis[i].Count >= 2)
                {
                    if (DaoRu.ContainsKey(lis[i][0])==false)
                    {
                        DaoRu.Add(lis[i][0], lis[i][1]);
                    }
                }
            }
        }

        public List<V> GetCanShu()
        {
            List<V> lis = new List<V>();
            for (int i = 0; i < LisKJ.Count; i++)
            {
                lis.Add(LisKJ[i].GetModel());
            }
            return lis;
        }

        protected override void GuanBi()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void WeiKuKJ1_DianJiEvent(int biaozhi, string neirong)
        {
            if (biaozhi == 1)//查找
            {
                this.QiDongTiShiKuang("查找功能取消");
            }
            else if (biaozhi == 2)
            {
                int dijihang = this.ucDaoHangKongJian1.DangQianPage;
                T kj = new T();
                kj.SetCanShu(new V());
                LisKJ.Add(kj);
                this.ucDaoHangKongJian1.AllPage = ChangYong.PageYeShu(this.ucDaoHangKongJian1.Rows, LisKJ.Count);
                if (dijihang == this.ucDaoHangKongJian1.AllPage)
                {
                    this.fGenSui1.Controls.Add(kj);
                }
                   
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
                        List<V> canshu = new List<V>();
                        this.Waiting(() => {
                            canshu = daoChuXlxsLei.ShuChuExcelOrLisN<V>(wenjinnane, DaoRu);
                        }, "正在导入...", this);

                        if (canshu == null)
                        {
                            canshu = new List<V>();
                        }

                        SetCanShu(this.labFbiaoTi.Text, canshu);
                       
                    }              
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
                    Dictionary<string, List<object>> BiaoGe = GetDaoChuNeiRong();
                    this.Waiting(() => {
                      
                        daoChuXlxsLei.DaoChuExc(wenjinnane, BiaoGe);
                    }, "正在导出...", this);

                }
            }
            else if (biaozhi == 5)
            {
                if (IsCaiYongBenDiBaoCun)
                {
                    HCLisDataLei<V>.Ceratei().LisWuLiao = GetCanShu();
                    HCLisDataLei<V>.Ceratei().BaoCun();
                    this.QiDongTiShiKuang("保存成功");
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private  Dictionary<string, List<object>> GetDaoChuNeiRong()
        {
            Dictionary<string, List<object>> BiaoGe = new Dictionary<string, List<object>>();
            List<V> DuiXiang = GetCanShu();
            if (DuiXiang is IList)
            {
                int count = DaoRu.Count;
                List<string> keys = DaoRu.Keys.ToList();
                IList xin = DuiXiang as IList;
                foreach (var item in xin)
                {
                    for (int i = 0; i < count; i++)
                    {
                        object canshus = ConvertEnumerationItem(item, DaoRu[keys[i]]);
                        if (canshus!=null)
                        {
                            if (BiaoGe.ContainsKey(keys[i]) == false)
                            {
                                BiaoGe.Add(keys[i], new List<object>());
                            }
                            BiaoGe[keys[i]].Add(canshus);
                        }                    
                       
                      
                    }

                }
            }
            return BiaoGe;
        }

        private object ConvertEnumerationItem(object item, string fieldName)
        {
            DataRow row = item as DataRow;
            if (row != null)
            {
                if (!string.IsNullOrEmpty(fieldName))
                {
                    if (row.Table.Columns.Contains(fieldName))
                        return row[fieldName];
                }
                return row[0];
            }
            else
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(item).Find(fieldName, true);
                if (descriptor != null)
                    return (descriptor.GetValue(item) ?? null);
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dijigeweizhi = this.ucDaoHangKongJian1.DangQianPage;          
            FromSortZX(LisKJ, true);
            GengShuJu(this.ucDaoHangKongJian1.Rows, dijigeweizhi);
        }

        /// <summary>
        ///  从小到大排序
        /// </summary>
        /// <param name="lisObj">集合</param>
        /// <param name="IsSort">为true表示从小到大，为false则是从大到小</param>
        private void FromSortZX(List<T> lisObj, bool IsSort)
        {
            if (lisObj.Count > 0)
            {
                try
                {
                    T obj = null;
                    for (int i = 0; i < lisObj.Count; i++)
                    {
                        for (int j = i + 1; j < lisObj.Count; j++)
                        {
                            if (IsSort)
                            {
                                if (lisObj[i].GetSunXu() > lisObj[j].GetSunXu())
                                {
                                    obj = lisObj[i];
                                    lisObj[i] = lisObj[j];
                                    lisObj[j] = obj;

                                }
                            }
                            else
                            {
                                if (lisObj[i].GetSunXu() < lisObj[j].GetSunXu())
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

        private void fGenSui1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (KongZhiChuFa == false)
            {
                int jixiagaihang = this.ucDaoHangKongJian1.DangQianPage;
                for (int i = 0; i < LisKJ.Count; i++)
                {
                    if (LisKJ[i].Equals(e.Control))
                    {
                        LisKJ.Remove((e.Control as T));
                        break;
                    }
                }
                this.ucDaoHangKongJian1.AllPage = ChangYong.PageYeShu(this.ucDaoHangKongJian1.Rows, LisKJ.Count);
                if (jixiagaihang > this.ucDaoHangKongJian1.AllPage)
                {
                    jixiagaihang = this.ucDaoHangKongJian1.AllPage;
                  
                }
                GengShuJu(this.ucDaoHangKongJian1.Rows, jixiagaihang-1);
            }
        }
    }
}
