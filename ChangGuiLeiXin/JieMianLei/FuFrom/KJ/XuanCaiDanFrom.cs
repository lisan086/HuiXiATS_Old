using CommLei.JiChuLei;
using JieMianLei.FuFrom;
using JieMianLei.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.FuFrom.KJ
{
    public partial class XuanCaiDanFrom : BaseFuFrom
    {
        private Dictionary<int, TreeNode> AllS = new Dictionary<int, TreeNode>();

        private List<GongNengModel> ShanYiCi = new List<GongNengModel>();
        public XuanCaiDanFrom()
        {
            InitializeComponent();
            this.IsZhiXianShiX = true;
        }
        public void SetWenBen(string biaoti,string tishi,string queding)
        {
            this.labFbiaoTi.Text = biaoti;
            this.label1.Text = tishi;
            this.button1.Text = queding;
        }
        public void SetCanShu(List<GongNengModel> all,List<GongNengModel> bufen)
        {
            if (all==null|| all.Count==0)
            {
                return;
            }
          
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].ZhuCaiDan==1)
                {
                    if (AllS.ContainsKey(all[i].BianHao)==false)
                    {
                        TreeNode nodel= this.treeView1.Nodes.Add(all[i].Name);
                        AllS.Add(all[i].BianHao, nodel);
                        nodel.Tag = all[i];
                    }
                }
            }
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].ZhuCaiDan == 2)
                {
                    if (AllS.ContainsKey(all[i].BianHao) )
                    {
                        TreeNode nodel = AllS[all[i].BianHao];
                        TreeNode node2=  nodel.Nodes.Add(all[i].Name);
                        node2.Tag = all[i];
                    }
                }
            }        
            if (bufen == null|| bufen.Count==0)
            {
                return;
            }
            Dictionary<int, TreeNode> bufend = new Dictionary<int, TreeNode>();
            for (int i = 0; i < bufen.Count; i++)
            {
                if (bufen[i].ZhuCaiDan == 1)
                {
                    if (bufend.ContainsKey(bufen[i].BianHao) == false)
                    {
                        TreeNode nodel = this.treeView2.Nodes.Add(bufen[i].Name);
                        bufend.Add(bufen[i].BianHao, nodel);
                        nodel.Tag = bufen[i];
                    }
                }
            }
            for (int i = 0; i < bufen.Count; i++)
            {
                if (bufen[i].ZhuCaiDan == 2)
                {
                    if (bufend.ContainsKey(bufen[i].BianHao))
                    {
                        TreeNode nodel = bufend[bufen[i].BianHao];
                        TreeNode node2 = nodel.Nodes.Add(bufen[i].Name);
                        node2.Tag = bufen[i];
                    }
                }
            }
            ShanYiCi.AddRange(bufen);
        }
        protected override void GuanBi()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public List<GongNengModel> GetCanShu(out bool isheshangciyiyang)
        {
            isheshangciyiyang = false;
            List<GongNengModel> suan = new List<GongNengModel>();
            for (int i = 0; i < treeView2.Nodes.Count; i++)
            {
                TreeNode node = treeView2.Nodes[i];
                if (node.Tag is GongNengModel)
                {
                    GongNengModel model = node.Tag as GongNengModel;
                    suan.Add(model);
                }
                if (node.Nodes.Count>0)
                {
                    for (int c = 0; c < node.Nodes.Count; c++)
                    {
                        TreeNode node1 = node.Nodes[c];
                        if (node1.Tag is GongNengModel)
                        {
                            GongNengModel model = node1.Tag as GongNengModel;
                            suan.Add(model);
                        }
                    }
                }
            }

            if (suan.Count==0&& ShanYiCi.Count==0)
            {
                isheshangciyiyang = true;
            }
            if (suan.Count == ShanYiCi.Count)
            {
                bool xiangdeng = false;
                for (int i = 0; i < suan.Count; i++)
                {
                    GongNengModel gong= ShanYiCi.Find((x)=> { return x.BianHao == suan[i].BianHao && x.ZhuCaiDan == suan[i].ZhuCaiDan && x.Name == suan[i].Name; });
                    if (gong==null)
                    {
                        xiangdeng = true;
                        break;
                    }
                }
                if (xiangdeng==false)
                {
                    isheshangciyiyang = true;
                }
            }

            return suan;
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.treeView1.Nodes.Count>0)
            {
                TreeNode jiedian = this.treeView1.SelectedNode;
                if (jiedian!=null)
                {
                    if (jiedian.Tag is GongNengModel)
                    {
                        GongNengModel model = jiedian.Tag as GongNengModel;
                        bool iscunzai = false;
                        TreeNode mode13 = null;
                        for (int i = 0; i < this.treeView2.Nodes.Count; i++)
                        {
                            if (this.treeView2.Nodes[i].Tag is GongNengModel)
                            {
                                GongNengModel gongneng = this.treeView2.Nodes[i].Tag as GongNengModel;
                                if (gongneng.ZhuCaiDan==1 && gongneng.BianHao == model.BianHao)
                                {
                                    mode13 = this.treeView2.Nodes[i];
                                    iscunzai = true;
                                    break;
                                }
                            }
                        }
                        if (model.ZhuCaiDan == 1)
                        {                        
                            if (iscunzai == false)
                            {
                                TreeNode mode1l = this.treeView2.Nodes.Add(model.Name);
                                mode1l.Tag =ChangYong.FuZhiShiTi(model);
                              
                            }
                        }
                        else
                        {
                            if (iscunzai == false)
                            {
                                if (AllS.ContainsKey(model.BianHao))
                                {
                                    TreeNode mode1l = AllS[model.BianHao];
                                   
                                    if (mode1l.Tag is GongNengModel)
                                    {
                                        GongNengModel zhubianhao = mode1l.Tag as GongNengModel;
                                        TreeNode mode33l = this.treeView2.Nodes.Add(zhubianhao.Name);
                                      
                                        mode33l.Tag = ChangYong.FuZhiShiTi(zhubianhao);
                                        TreeNode mode3= mode33l.Nodes.Add(model.Name);
                                        mode3.Tag= ChangYong.FuZhiShiTi(model);
                                    }
                                }
                            }
                            else
                            {
                                bool isyou = false;
                                for (int i = 0; i < mode13.Nodes.Count; i++)
                                {
                                    if (mode13.Nodes[i].Text.Equals(model.Name))
                                    {
                                        isyou = true;
                                        break;
                                    }
                                }
                                if (isyou == false)
                                {
                                    TreeNode mode1l = mode13.Nodes.Add(model.Name);
                                    mode1l.Tag = ChangYong.FuZhiShiTi(model);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void treeView2_DoubleClick(object sender, EventArgs e)
        {
            if (this.treeView2.Nodes.Count > 0)
            {
                TreeNode jiedian = this.treeView2.SelectedNode;
                if (jiedian != null&& jiedian.Tag is GongNengModel)
                {
                    GongNengModel model = jiedian.Tag as GongNengModel;
                    if (model.ZhuCaiDan == 1)
                    {
                        this.treeView2.Nodes.Remove(jiedian);
                    }
                    else
                    {
                        TreeNode fu= jiedian.Parent;
                        if (fu!=null)
                        {
                            fu.Nodes.Remove(jiedian);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    public class GongNengModel
    {
        public object ID { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 1为主菜单 2为子菜单
        /// </summary>
        public int ZhuCaiDan { get; set; }

        /// <summary>
        /// 会根据编号来分
        /// </summary>
        public int BianHao { get; set; }
    }
}
