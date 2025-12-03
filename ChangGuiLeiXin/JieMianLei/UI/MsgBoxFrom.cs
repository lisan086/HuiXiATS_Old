using JieMianLei.FuZhuLei;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.UI
{
    public partial class MsgBoxFrom : Form
    {
        private MoveShiJianEvent _MoveShiJianEvent;
        public MsgBoxFrom()
        {
            InitializeComponent();
          
            _MoveShiJianEvent = new MoveShiJianEvent();
            _MoveShiJianEvent.BangDingMove(this.label1, this);
        }
        public bool JieGuo = false;
        private int ShiJian = 0;
        private string name = "";
        private DateTime StarShiJian = new DateTime();
        public void AddMsg(string lismsg)
        {
            this.label2.Text = lismsg;
        }

        /// <summary>
        /// 打开提示框的状态
        /// </summary>
        /// <param name="ControlGeiShu">设置控件的个数</param>
        /// <param name="KongJian1Name">设置控件的名称</param>
        /// <param name="KongJian2Name">设置控件的名称</param>
        /// <param name="IsQiDongZiDongGuanBi">是否启动自动关闭</param>
        /// <param name="GuanBiShiJian">关闭的时间</param>
        public void SetCanShu(bool IsQiDongZiDongGuanBi, string KongJian1Name, string KongJian2Name, int GuanBiShiJian)
        {
            if (IsQiDongZiDongGuanBi)
            {
                this.button1.Visible = false;
                this.button3.Visible = false;
                this.button2.Visible = true;
                if (KongJian1Name != "")
                {
                    this.button2.Text = KongJian1Name;
                }
                name = this.button2.Text;
                this.ShiJian = GuanBiShiJian < 0 ? 2 : GuanBiShiJian;
                this.timer1.Enabled = IsQiDongZiDongGuanBi;
                StarShiJian = DateTime.Now;
            }
            else
            {
                this.button1.Visible = true;
                this.button3.Visible = true;
                this.button2.Visible = false;
                if (KongJian1Name != "")
                {
                    this.button1.Text = KongJian1Name;
                }
                if (KongJian2Name != "")
                {
                    this.button3.Text = KongJian2Name;
                }
                name = this.button1.Text;
                this.ShiJian = GuanBiShiJian < 0 ? 0 : GuanBiShiJian;
                this.timer1.Enabled = IsQiDongZiDongGuanBi;
                StarShiJian = DateTime.Now;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime jisushijian = DateTime.Now;
            try
            {
                this.label1.Invoke(new Action(() =>
                {
                    int miaoshu = (int)(jisushijian - StarShiJian).TotalSeconds;
                    if (miaoshu >= ShiJian)
                    {
                        this.timer1.Enabled = false;
                        this.DialogResult = DialogResult.OK;
                        JieGuo = false;
                        this.Close();
                    }

                    this.button2.Text = string.Format("{0}({1}s)", name, ShiJian - miaoshu);
                 
                }));
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            JieGuo = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.DialogResult = DialogResult.OK;
            JieGuo = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            JieGuo = true;
            this.Close();
        }
    }
}
