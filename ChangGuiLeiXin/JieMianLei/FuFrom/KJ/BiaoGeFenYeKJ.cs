using CommLei.JiChuLei;
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

namespace JieMianLei.FuFrom.KJ
{
  
    public delegate void ChuaJieGuo(object sender, DataGridViewRow row, int lie, bool isshanchu);

    public delegate void DiJiYe(object sender, int dijiye,int rows);
    public partial class BiaoGeFenYeKJ : UserControl
    {
        private List<LieModel> LieModels;

        public event ChuaJieGuo ChuaJieGuoEvent;

        public event DiJiYe DiJiYeEvent;

        private object DuiXiang = new object();
        public BiaoGeFenYeKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
          
            IniData(400);
            this.daoHangKJ1.UCClilk += UcDaoHangKongJian1_UCClilk;
            this.dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }
        public void IniData(int xianshiliang)
        {
            if (xianshiliang <= 0)
            {
                this.daoHangKJ1.Rows = 200;
            }
            this.daoHangKJ1.Rows = xianshiliang;
        }
        public void AddLie(List<LieModel> Lies)
        {
            if (Lies == null || Lies.Count == 0)
            {
                return;
            }
            LieModels = Lies;
            FromSortZX(LieModels, true);
            for (int i = 0; i < LieModels.Count; i++)
            {
                LieModels[i].DiLie = i;
            }
            List<DataGridViewColumn> duixiang = new List<DataGridViewColumn>();
            for (int i = 0; i < LieModels.Count; i++)
            {
                if (LieModels[i].LeiXing == 1)
                {
                    DataGridViewTextBoxColumn Column10 = new DataGridViewTextBoxColumn();
                    Column10.FillWeight = LieModels[i].FullChang;
                    Column10.HeaderText = LieModels[i].LieName;
                    Column10.MinimumWidth = 6;
                    Column10.Name = string.Format("Column{0}", i + 1);
                    Column10.ReadOnly = LieModels[i].IsZhiDu;
                    Column10.Visible = LieModels[i].IsKeJian;
                    duixiang.Add(Column10);
                }
                else if (LieModels[i].LeiXing == 2)
                {
                    DataGridViewCheckBoxColumn Column10 = new DataGridViewCheckBoxColumn();
                    Column10.FillWeight = LieModels[i].FullChang;
                    Column10.HeaderText = LieModels[i].LieName;
                    Column10.MinimumWidth = 6;
                    Column10.Name = string.Format("Column{0}", i + 1);
                    Column10.ReadOnly = LieModels[i].IsZhiDu;
                    Column10.Visible = LieModels[i].IsKeJian;
                    duixiang.Add(Column10);
                }
                else if (LieModels[i].LeiXing == 3)
                {
                    DataGridViewButtonColumn Column10 = new DataGridViewButtonColumn();
                    Column10.FillWeight = LieModels[i].FullChang;
                    Column10.HeaderText = LieModels[i].LieName;
                    Column10.MinimumWidth = 6;
                    Column10.Name = string.Format("Column{0}", i + 1);
                    Column10.ReadOnly = LieModels[i].IsZhiDu;
                    Column10.Visible = LieModels[i].IsKeJian;
                    duixiang.Add(Column10);
                }
                else if (LieModels[i].LeiXing == 4)
                {
                    DataGridViewButtonColumn Column10 = new DataGridViewButtonColumn();
                    Column10.FillWeight = LieModels[i].FullChang;
                    Column10.HeaderText = LieModels[i].LieName;
                    Column10.MinimumWidth = 6;
                    Column10.Name = string.Format("Column{0}", i + 1);
                    Column10.ReadOnly = LieModels[i].IsZhiDu;
                    Column10.Visible = LieModels[i].IsKeJian;
                    Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                    Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
                    duixiang.Add(Column10);

                }
            }
            this.dataGridView1.Columns.AddRange(duixiang.ToArray());
        }

        public void FuZhi<T>(List<T> LisT)
        {
            this.dataGridView1.Rows.Clear();
            if (LisT == null || LisT.Count == 0)
            {
                return;
            }
            if (LieModels == null || LieModels.Count == 0)
            {
                return;
            }
            DuiXiang = LisT;
            int allpage = LisT.Count;
            this.daoHangKJ1.AllPage = ChangYong.PageYeShu(this.daoHangKJ1.Rows, allpage);
            int row = this.daoHangKJ1.Rows;
            List<T> xinshuju = new List<T>();
            for (int i = 0; i < row; i++)
            {
                if (i >= allpage)
                {
                    break;
                }
                xinshuju.Add(LisT[i]);
            }
            GengXin(xinshuju);
            Dictionary<string, string> zidian = new Dictionary<string, string>();
            zidian.Add("总数量", LisT.Count.ToString());
            HeJiNeiRong(zidian);
        }

        public void HeJiNeiRong(Dictionary<string, string> HeJi)
        {
            this.heJiKJ1.SetKongJian(HeJi);
        }

        public void Clear()
        {
            this.dataGridView1.Rows.Clear();
        }

        public List<DataGridViewRow> GetData()
        {
            List<DataGridViewRow> lis = new List<DataGridViewRow>();
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                lis.Add(this.dataGridView1.Rows[i]);
            }
            return lis;
        }
        private void GengXin(object duixiang)
        {
            this.dataGridView1.Rows.Clear();
            if (duixiang == null)
            {
                return;
            }
            if (duixiang is IList)
            {
                IList xin = duixiang as IList;
                List<DataGridViewRow> shuju = ChuLiItems(xin);
                this.dataGridView1.Rows.AddRange(shuju.ToArray());
            }
        }


        public Dictionary<string, List<object>> GetDaoChuNeiRong()
        {
            Dictionary<string, List<object>> BiaoGe = new Dictionary<string, List<object>>();
            if (DuiXiang is IList)
            {

                int count = LieModels.Count;
                IList xin = DuiXiang as IList;
                foreach (var item in xin)
                {

                    for (int i = 0; i < count; i++)
                    {
                        string canshu = "";
                        if (LieModels[i].IsZhiXianShiBangDingName == false)
                        {
                            object zhi = ConvertEnumerationItem(item, LieModels[i].BangDingName);

                            canshu = XianShiZhi(LieModels[i], zhi);
                        }
                        else
                        {
                            canshu = LieModels[i].BangDingName;
                        }
                        if (LieModels[i].IsZhiXianShiBangDingName == false)
                        {
                            if (BiaoGe.ContainsKey(LieModels[i].LieName) == false)
                            {
                                BiaoGe.Add(LieModels[i].LieName, new List<object>());
                            }
                            BiaoGe[LieModels[i].LieName].Add(canshu);
                        }

                    }

                }
            }
            return BiaoGe;
        }


        private List<DataGridViewRow> ChuLiItems(IList jieheData)
        {
            int count = LieModels.Count;
            List<DataGridViewRow> itlist = new List<DataGridViewRow>();
            foreach (var item in jieheData)
            {
                DataGridViewRow row = new DataGridViewRow();
                for (int i = 0; i < count; i++)
                {
                    string canshu = "";
                    if (LieModels[i].IsZhiXianShiBangDingName == false)
                    {
                        object zhi = ConvertEnumerationItem(item, LieModels[i].BangDingName);

                        canshu = XianShiZhi(LieModels[i], zhi);
                    }
                    else
                    {
                        canshu = LieModels[i].BangDingName;
                    }
                    if (LieModels[i].LeiXing == 1)
                    {
                        DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                        cell.Value = canshu;
                        row.Cells.Add(cell);
                    }
                    else if (LieModels[i].LeiXing == 2)
                    {
                        DataGridViewCheckBoxCell cell = new DataGridViewCheckBoxCell();
                        cell.Value = canshu;
                        row.Cells.Add(cell);
                    }
                    else if (LieModels[i].LeiXing == 3)
                    {
                        DataGridViewButtonCell cell = new DataGridViewButtonCell();
                        cell.Value = canshu;
                        row.Cells.Add(cell);
                    }
                    else if (LieModels[i].LeiXing == 4)
                    {
                        DataGridViewLinkCell cell = new DataGridViewLinkCell();
                        cell.Value = canshu;
                        row.Cells.Add(cell);
                    }
                }
                row.Height = 32;
                row.Tag = item;

                itlist.Add(row);
            }

            return itlist;
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
        /// <summary>
        ///  从小到大排序
        /// </summary>
        /// <param name="lisObj">集合</param>
        /// <param name="IsSort">为true表示从小到大，为false则是从大到小</param>
        private void FromSortZX(List<LieModel> lisObj, bool IsSort)
        {
            if (lisObj.Count > 0)
            {
                try
                {
                    LieModel obj = null;
                    for (int i = 0; i < lisObj.Count; i++)
                    {
                        for (int j = i + 1; j < lisObj.Count; j++)
                        {
                            if (IsSort)
                            {
                                if (lisObj[i].DiLie > lisObj[j].DiLie)
                                {
                                    obj = lisObj[i];
                                    lisObj[i] = lisObj[j];
                                    lisObj[j] = obj;

                                }
                            }
                            else
                            {
                                if (lisObj[i].DiLie < lisObj[j].DiLie)
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


        private string XianShiZhi(LieModel model, object value)
        {
            if (value == null)
            {
                return "";
            }
            if (model.XianShiZhi.Count > 0)
            {
                foreach (var item in model.XianShiZhi.Keys)
                {
                    if (value.ToString().ToLower().Equals(item.ToLower()))
                    {
                        return model.XianShiZhi[item];
                    }
                }
            }
            return value.ToString();
        }

        private void UcDaoHangKongJian1_UCClilk(object sender, DaoHangModel e)
        {
            if (DiJiYeEvent != null)
            {
                int index = e.Index;
                int hangshu = e.Row;
                DiJiYeEvent(this, index, hangshu);
            }
            else
            {
                #region 显示
                int index = e.Index;
                int hangshu = e.Row;
                if (DuiXiang is IList)
                {
                    IList list = DuiXiang as IList;
                    int count = list.Count;
                    List<object> shuju = new List<object>();
                    for (int i = hangshu * (index); i < (index + 1) * hangshu; i++)
                    {
                        if (i >= count)
                        {
                            break;
                        }
                        shuju.Add(list[i]);
                    }
                    GengXin(shuju);
                }
                #endregion
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0)
            {
                if (e.ColumnIndex >= 0)
                {
                    if (e.RowIndex >= 0)
                    {
                        int lie = e.ColumnIndex;
                        LieModel li = LieModels[lie];
                        if (li.IsShanChuHang)
                        {
                            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                            if (ChuaJieGuoEvent != null)
                            {
                                ChuaJieGuoEvent(this, row, lie, true);
                            }
                            //this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                        else
                        {
                            if (li.LeiXing == 3 || li.LeiXing == 4)
                            {
                                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                                if (ChuaJieGuoEvent != null)
                                {
                                    ChuaJieGuoEvent(this, row, lie, false);
                                }
                            }
                        }

                    }
                }
            }
        }
    }
    public class LieModel
    {
        public string LieName { get; set; } = "";

        /// <summary>
        /// true表示可见
        /// </summary>
        public bool IsKeJian { get; set; } = true;

        /// <summary>
        /// true 表示只读
        /// </summary>
        public bool IsZhiDu { get; set; } = false;

        /// <summary>
        /// 表示第几列
        /// </summary>
        public int DiLie { get; set; } = 0;

        /// <summary>
        /// 绑定的参数
        /// </summary>
        public string BangDingName { get; set; } = "";

        /// <summary>
        /// true表示删除行
        /// </summary>
        public bool IsShanChuHang { get; set; } = false;

        /// <summary>
        /// true 表示cell用绑定的name
        /// </summary>
        public bool IsZhiXianShiBangDingName { get; set; } = false;

        /// <summary>
        /// 1表示是textbox 2表示checkbox 3表示是btn 4表示linklab
        /// </summary>
        public int LeiXing { get; set; } = 1;

        /// <summary>
        /// 满长度
        /// </summary>
        public float FullChang { get; set; } = 100f;

        /// <summary>
        /// 界面显示的value值 key是小写的  没有就是显示原来值
        /// </summary>
        public Dictionary<string, string> XianShiZhi { get; set; } = new Dictionary<string, string>();
    }
}
