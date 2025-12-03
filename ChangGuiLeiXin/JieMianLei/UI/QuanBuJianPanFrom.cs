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
    public partial class QuanBuJianPanFrom : Form
    {
        private MoveShiJianEvent _MoveShiJianEvent;
        public QuanBuJianPanFrom()
        {
            InitializeComponent();
            _MoveShiJianEvent = new MoveShiJianEvent();
            _MoveShiJianEvent.BangDingMove(pictureBox1, this);

            for (int i = 0; i < this.tableLayoutPanel1.Controls.Count; i++)
            {
                if (this.tableLayoutPanel1.Controls[i] is Button)
                {
                    this.tableLayoutPanel1.Controls[i].Click += button_Click;
                }
            }
           
        }
        private int jilvGuangBiaoWeiZhi = 0;
        public void SetCanShu(string tsex, bool isyouxihao = false)
        {
            this.textBox1.Text = tsex;
            if (isyouxihao)
            {
                this.textBox1.PasswordChar = '*';
            }
        }

        public string GetstrNeiRong
        {
            get
            {
                return this.textBox1.Text;
            }
          
        }

        private void ZhuanHuan(bool IsDaXie)
        {
            if (IsDaXie)
            {
                this.button38.Text = this.button38.Text.ToUpper();
                this.button37.Text = this.button37.Text.ToUpper();
                this.button36.Text = this.button36.Text.ToUpper();
                this.button35.Text = this.button35.Text.ToUpper();
                this.button34.Text = this.button34.Text.ToUpper();
                this.button33.Text = this.button33.Text.ToUpper();
                this.button30.Text = this.button30.Text.ToUpper();
                this.button31.Text = this.button31.Text.ToUpper();
                this.button29.Text = this.button29.Text.ToUpper();

                this.button32.Text = this.button32.Text.ToUpper();

                this.button50.Text = this.button50.Text.ToUpper();
                this.button49.Text = this.button49.Text.ToUpper();
                this.button48.Text = this.button48.Text.ToUpper();
                this.button47.Text = this.button47.Text.ToUpper();
                this.button46.Text = this.button46.Text.ToUpper();

                this.button45.Text = this.button45.Text.ToUpper();
                this.button44.Text = this.button44.Text.ToUpper();
                this.button43.Text = this.button43.Text.ToUpper();

                this.button42.Text = this.button42.Text.ToUpper();

                this.button60.Text = this.button60.Text.ToUpper();
                this.button59.Text = this.button59.Text.ToUpper();

                this.button58.Text = this.button58.Text.ToUpper();
                this.button57.Text = this.button57.Text.ToUpper();
                this.button54.Text = this.button54.Text.ToUpper();
                this.button62.Text = this.button62.Text.ToUpper();
                this.button61.Text = this.button61.Text.ToUpper();
                this.button60.Text = this.button60.Text.ToUpper();
                this.button52.Text = "?";
            }
            else
            {

                this.button38.Text = this.button38.Text.ToLower();
                this.button37.Text = this.button37.Text.ToLower();
                this.button36.Text = this.button36.Text.ToLower();
                this.button35.Text = this.button35.Text.ToLower();
                this.button34.Text = this.button34.Text.ToLower();
                this.button33.Text = this.button33.Text.ToLower();
                this.button30.Text = this.button30.Text.ToLower();
                this.button31.Text = this.button31.Text.ToLower();
                this.button29.Text = this.button29.Text.ToLower();

                this.button32.Text = this.button32.Text.ToLower();

                this.button50.Text = this.button50.Text.ToLower();
                this.button49.Text = this.button49.Text.ToLower();
                this.button48.Text = this.button48.Text.ToLower();
                this.button47.Text = this.button47.Text.ToLower();
                this.button46.Text = this.button46.Text.ToLower();

                this.button45.Text = this.button45.Text.ToLower();
                this.button44.Text = this.button44.Text.ToLower();
                this.button43.Text = this.button43.Text.ToLower();

                this.button42.Text = this.button42.Text.ToLower();

                this.button60.Text = this.button60.Text.ToLower();
                this.button59.Text = this.button59.Text.ToLower();

                this.button58.Text = this.button58.Text.ToLower();
                this.button57.Text = this.button57.Text.ToLower();
                this.button54.Text = this.button54.Text.ToLower();
                this.button62.Text = this.button62.Text.ToLower();
                this.button61.Text = this.button61.Text.ToLower();
                this.button60.Text = this.button60.Text.ToLower();
                this.button52.Text = ".";
            }
            this.textBox1.Select(this.textBox1.SelectionStart, 0);
            this.textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int GuanBiaoWeiZhi = this.textBox1.SelectionStart;
            int XuanZhongItem = this.textBox1.SelectionLength;
            if (XuanZhongItem == 0)
            {

                if (GuanBiaoWeiZhi > 0)
                {
                    this.textBox1.Text = this.textBox1.Text.Remove(GuanBiaoWeiZhi - 1, 1);
                    GuanBiaoWeiZhi--;
                }
                this.textBox1.Select(GuanBiaoWeiZhi, 0);
                jilvGuangBiaoWeiZhi = GuanBiaoWeiZhi;
                this.textBox1.Focus();
            }
            else if (XuanZhongItem > 0)
            {
                if (GuanBiaoWeiZhi >= 0)
                {
                    this.textBox1.Text = this.textBox1.Text.Remove(GuanBiaoWeiZhi, XuanZhongItem);




                }
                this.textBox1.Select(GuanBiaoWeiZhi, 0);
                jilvGuangBiaoWeiZhi = GuanBiaoWeiZhi;
                this.textBox1.Focus();
            }
        }

        private void button74_Click(object sender, EventArgs e)
        {
            if (button74.Text.Contains("大"))
            {
                ZhuanHuan(true);
                button74.Text = "小字母转换";
            }
            else
            {
                ZhuanHuan(false);
                button74.Text = "大字母转换";
            }
        }

        private void button69_Click(object sender, EventArgs e)
        {
            if (jilvGuangBiaoWeiZhi == this.textBox1.Text.Length)
            {
                this.textBox1.Text += " ";

            }
            else
            {

                string msg = this.textBox1.Text;


                this.textBox1.Text = msg.Insert(jilvGuangBiaoWeiZhi, " ");
            }
            this.textBox1.Select(jilvGuangBiaoWeiZhi + 1, 0);
            jilvGuangBiaoWeiZhi = this.textBox1.SelectionStart;
            this.textBox1.Focus();
        }

        private void button63_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            jilvGuangBiaoWeiZhi = this.textBox1.SelectionStart;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (jilvGuangBiaoWeiZhi == this.textBox1.Text.Length)
            {
                Button bt = sender as Button;
                string msg = bt.Text.Trim();
                if (msg.Contains(@"\&"))
                {
                    this.textBox1.Text += msg.Substring(0, 1);

                }
                else
                {
                    this.textBox1.Text += bt.Text.Trim();
                }

            }
            else
            {
                Button bt = sender as Button;
                string msg1 = bt.Text.Trim();
                string msg = this.textBox1.Text;

                if (msg1.Contains(@"\&"))
                {

                    this.textBox1.Text = msg.Insert(jilvGuangBiaoWeiZhi, msg1.Substring(0, 1));

                }
                else
                {
                    this.textBox1.Text = msg.Insert(jilvGuangBiaoWeiZhi, bt.Text.Trim());
                }



            }
            this.textBox1.Select(jilvGuangBiaoWeiZhi + 1, 0);
            jilvGuangBiaoWeiZhi = this.textBox1.SelectionStart;
            this.textBox1.Focus();
        }
    }
}
