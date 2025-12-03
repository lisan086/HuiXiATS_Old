using BaseUI.FuFrom.XinWeiHuFrm;
using CommLei.JiChuLei;
using JieMianLei.UI;
using SSheBei.Model;
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
    /// 配置的控件
    /// </summary>
    public partial class PeiZhiKJ : UserControl, IFUCKJ<JiaZaiSheBeiModel>
    {
        private JieMianPeiZhiLei JieMianPeiZhiLei;
        private JiaZaiSheBeiModel JiaZaiSheBeiModel;
        /// <summary>
        /// 配置控件
        /// </summary>
        public PeiZhiKJ()
        {
            InitializeComponent();
            JieMianPeiZhiLei = new JieMianPeiZhiLei();
            this.DoubleBuffered = true;
        }
        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            this.textBox1.Text = "";
            textBox7.Text = "";
            textBox3.Text = "";
            comboBox2.Items.Clear();
            textBox4.Text = "";
            textBox2.Text = "";
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        public JiaZaiSheBeiModel GetModel()
        {
            JiaZaiSheBeiModel model = ChangYong.FuZhiShiTi(JiaZaiSheBeiModel);
            model.IsShiYong = this.checkBox1.Checked;
            model.JiaZaiWanJianName = textBox4.Text;
            model.SheBeiID = ChangYong.TryInt(textBox7.Text,0);
            model.SheBeiName = textBox1.Text;
            model.SheBeiPeiZhi = textBox2.Text;
            model.SheBeiType = this.comboBox2.Text;
            model.SheBeiZu = this.commBoxE1.Text;
            return model;
        }
        /// <summary>
        /// 获取排血
        /// </summary>
        /// <returns></returns>
        public int GetSunXu()
        {
            return -1;
        }
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="canshu"></param>
        public void SetCanShu(JiaZaiSheBeiModel canshu)
        {
            if (canshu != null)
            {
               
                JiaZaiSheBeiModel = ChangYong.FuZhiShiTi(canshu);
                GengXinComBox();
                textBox7.Text = JiaZaiSheBeiModel.SheBeiID.ToString();
                textBox1.Text = JiaZaiSheBeiModel.SheBeiName;                          
                this.checkBox1.Checked = JiaZaiSheBeiModel.IsShiYong;
                ComBoxFuZhi(JiaZaiSheBeiModel.SheBeiType);
               
                textBox4.Text = JiaZaiSheBeiModel.JiaZaiWanJianName;
                textBox2.Text = JiaZaiSheBeiModel.SheBeiPeiZhi;
                this.commBoxE1.Text = JiaZaiSheBeiModel.SheBeiZu;
            }
            else
            {
                JiaZaiSheBeiModel = new JiaZaiSheBeiModel();
            }
        }
        /// <summary>
        /// 设置顺序
        /// </summary>
        /// <param name="sunxu"></param>
        public void SetSunXu(int sunxu)
        {
           
        }

        private void GengXinComBox()
        {
            this.comboBox2.Items.Clear();
            this.commBoxE1.Items.Clear();
            JieMianPeiZhiLei.IniChuShiHua(JiaZaiSheBeiModel);
            if (JieMianPeiZhiLei.SheBeis.Count > 0)
            {
                List<string> shebeitypes = JieMianPeiZhiLei.GetSheBeiType();
                for (int i = 0; i < shebeitypes.Count; i++)
                {
                    this.comboBox2.Items.Add(shebeitypes[i]);
                }
            }
            if (this.comboBox2.Items.Count>0)
            {
                this.comboBox2.SelectedIndex = 0;
            }
            List<string> liszu = ChangYong.MeiJuLisName(typeof(SheBeiZuType));
            for (int i = 0; i < liszu.Count; i++)
            {
                this.commBoxE1.Items.Add(liszu[i]);
            }
            if (this.commBoxE1.Items.Count > 0)
            {
                this.commBoxE1.SelectedIndex = 0;
            }
        }

        private void ComBoxFuZhi(string type)
        {
            
            for (int i = 0; i < this.comboBox2.Items.Count; i++)
            {
                if (this.comboBox2.Items[i].ToString().Equals(type))
                {
                    this.comboBox2.SelectedIndex = i;
                    break;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(this);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = JieMianPeiZhiLei.GetBanBen(this.comboBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Text = JieMianPeiZhiLei.GetSheBeiWenJianName();
            JiaZaiSheBeiModel.JiaZaiWanJianName = textBox4.Text;
            GengXinComBox();
        }

        /// <summary>
        /// 启动默认提示框，默认是5秒
        /// </summary>
        /// <param name="msg"></param>    
        /// <param name="shijian"></param>
        /// <returns></returns>
        protected void QiDongTiShiKuang(string msg, int shijian = 5)
        {
            MsgBoxFrom chuanti = new MsgBoxFrom();
            chuanti.AddMsg(msg);
            chuanti.SetCanShu(true, "确定", "", shijian);
            chuanti.TopMost = true;
            chuanti.BringToFront();
            chuanti.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                QiDongTiShiKuang("没有写配置变量");
                return;
            }
            JieMianFrmModel jiemian = JieMianPeiZhiLei.GetJieMian(this.comboBox2.Text, textBox2.Text);
            if (jiemian != null)
            {
                if (jiemian.Form != null)
                {
                    jiemian.Form.Show(this);
                }
            }
        }
    }
}
