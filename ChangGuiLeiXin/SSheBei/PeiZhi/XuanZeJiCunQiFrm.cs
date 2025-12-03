using CommLei.DataChuLi;
using CommLei.JiChuLei;
using Common.DataChuLi;
using JieMianLei.FuFrom;
using SSheBei.ABSSheBei;
using SSheBei.Model;
using SSheBei.ZongKongZhi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSheBei.PeiZhi
{
    /// <summary>
    /// 选择读写寄存器
    /// </summary>
    public partial class XuanZeJiCunQiFrm : BaseFuFrom
    {
        private Dictionary<int, string> BangDing = new Dictionary<int, string>();
        private List<JiCunQiModel> LisDus = new List<JiCunQiModel>();
        /// <summary>
        /// 获取寄存器唯一标识
        /// </summary>
        public string JiCunQiWeiYiBiaoShi { get; set; } = "";
        /// <summary>
        /// 设备的id
        /// </summary>
        public int SheBeiID { get; set; } = -1;

        private int Index = 0;
        private string ShangYiCi = "";

        /// <summary>
        /// 选择
        /// </summary>
        public XuanZeJiCunQiFrm()
        {
            InitializeComponent();
            this.IsZhiXianShiX = true;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        protected override void GuanBi()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 1  是读寄存器 2是写寄存器 3是选择全部
        /// </summary>
        /// <param name="jicunqiweiyibiaoshi"></param>
        /// <param name="shebeiid"></param>
        /// <param name="leixing"></param>
        public void SetCanShu(string jicunqiweiyibiaoshi,int shebeiid, int leixing)
        {
            this.Waiting(() => { IniChuShiHua(leixing); }, "正在加载读写寄存器", this);
            List<CheckBox> kjs = new List<CheckBox>();
            List<JiCunQiModel> chuid1s = LisDus;
            this.labFbiaoTi.Text = string.Format("选择{0}寄存器", leixing == 1 ? "读" : leixing == 2?"写":"全部");
            SheBeiID = shebeiid;
            foreach (var item1 in chuid1s)
            {
       
                int index = this.jiHeDataGrid1.Rows.Add();
                this.jiHeDataGrid1.Rows[index].Cells[0].Value = item1.WeiYiBiaoShi;
                this.jiHeDataGrid1.Rows[index].Cells[1].Value = item1.DuXie==1? "读" : item1.DuXie == 2?"写":"读写一起";
                this.jiHeDataGrid1.Rows[index].Cells[2].Value = $"{item1.SheBeiID}:{BangDing[item1.SheBeiID]}";
                this.jiHeDataGrid1.Rows[index].Cells[3].Value = $"{item1.MiaoSu}";
                this.jiHeDataGrid1.Rows[index].Cells[4].Value = false;

                this.jiHeDataGrid1.Rows[index].Height = 32;
                if (item1.WeiYiBiaoShi == jicunqiweiyibiaoshi&&item1.SheBeiID== shebeiid)
                {
                    this.jiHeDataGrid1.Rows[index].Cells[4].Value = true;
                    this.label1.Text = string.Format("{0}:{1}",shebeiid, item1.WeiYiBiaoShi);
                    this.jiHeDataGrid1.Rows[index].DefaultCellStyle.ForeColor = Color.Blue;
                    this.jiHeDataGrid1.ClearSelection();
                    this.jiHeDataGrid1.Rows[index].Cells[1].Selected = true;
                    this.jiHeDataGrid1.CurrentCell = this.jiHeDataGrid1.Rows[index].Cells[0];
                }
            }

        }

    

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void IniChuShiHua(int type)
        {
            LisDus.Clear();
            List<JiCunQiModel> jiCunQis= ZongSheBeiKongZhi.Cerate().GetPeiZhiLisJiCunQi(type);
            for (int i = 0; i < jiCunQis.Count; i++)
            {
                if (BangDing.ContainsKey(jiCunQis[i].SheBeiID) == false)
                {
                    BangDing.Add(jiCunQis[i].SheBeiID, ZongSheBeiKongZhi.Cerate().GetSheBeiName(jiCunQis[i].SheBeiID));
                }
                LisDus.Add(jiCunQis[i]);
            }


           
        }

        private void jiHeDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    this.jiHeDataGrid1.Rows[e.RowIndex].Cells[4].Value = true;
                    this.textBox1.Text = this.jiHeDataGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int i = 0; i < this.jiHeDataGrid1.Rows.Count; i++)
                    {
                        if (i != e.RowIndex)
                        {
                            this.jiHeDataGrid1.Rows[i].Cells[4].Value = false;
                        
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JiCunQiWeiYiBiaoShi = "";
            for (int i = 0; i < this.jiHeDataGrid1.Rows.Count; i++)
            {
                if (this.jiHeDataGrid1.Rows[i].Cells[4].Value.ToString().Contains("u"))
                {
                    JiCunQiWeiYiBiaoShi = this.jiHeDataGrid1.Rows[i].Cells[0].Value.ToString();
                    SheBeiID = ChangYong.TryInt(this.jiHeDataGrid1.Rows[i].Cells[2].Value.ToString().Split(':')[0],-1);
                    break;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
            {
                this.QiDongTiShiKuang("请填写寄存器的名称");

                return;
            }

            string wenjian = this.textBox1.Text.Trim();
            if (wenjian.Equals(ShangYiCi) == false)
            {
                Index = 0;
                ShangYiCi = wenjian;
            }
            bool iszhaodao = false;
            for (int i = Index; i < this.jiHeDataGrid1.Rows.Count; i++)
            {
                if (this.jiHeDataGrid1.Rows[i].Cells[0].Value.ToString().Contains(wenjian))
                {
                    this.jiHeDataGrid1.ClearSelection();
                    this.jiHeDataGrid1.Rows[i].Cells[1].Selected = true;
                    this.jiHeDataGrid1.CurrentCell = this.jiHeDataGrid1.Rows[i].Cells[0];
                    Index = i;
                    iszhaodao = true;
                    break;
                }
            }
            if (iszhaodao)
            {
                Index = Index + 1;
                if (Index >= this.jiHeDataGrid1.Rows.Count)
                {
                    Index = 0;
                }
            }
        }
    }
}
