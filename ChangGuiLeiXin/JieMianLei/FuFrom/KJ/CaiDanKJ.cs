using System;
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
    /// <summary>
    /// 1为查看 2为新增 3为导入 4为导出 5为保存
    /// </summary>
    /// <param name="biaozhi"></param>
    public delegate void DianJiS(int biaozhi,string miaoshu);
    public partial class CaiDanKJ : UserControl
    {
        /// <summary>
        /// 1为查看 2为新增 3为导入 4为导出 5为保存
        /// </summary>
        public event DianJiS DianJiEvent;
        public CaiDanKJ()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        public void SetXianShi(List<string> isxianshi)
        {
            if (isxianshi == null || isxianshi.Count == 0)
            {
                return;
            }
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].Visible = true;
                if (isxianshi.IndexOf(this.Controls[i].Text) >= 0)
                {
                    this.Controls[i].Visible = false;
                }
            }

        }

        public void XianShiWenBen(List<string> liswenben)
        {
            for (int i = 0; i < liswenben.Count; i++)
            {
                if (i == 0)
                {
                    this.button1.Text = liswenben[i];
                }
                else if (i == 1)
                {
                    this.button3.Text = liswenben[i];
                }
                else if (i == 2)
                {
                    this.button4.Text = liswenben[i];
                }
                else if (i == 3)
                {
                    this.button5.Text = liswenben[i];
                }
                else if (i == 4)
                {
                    this.button2.Text = liswenben[i];
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DianJiEvent != null)
            {
                DianJiEvent(1, "查看");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (DianJiEvent != null)
            {
                DianJiEvent(2, "新增");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DianJiEvent != null)
            {
                DianJiEvent(3, "导入");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DianJiEvent != null)
            {
                DianJiEvent(4, "导出");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (DianJiEvent != null)
            {
                DianJiEvent(5, "保存");
            }
        }
    }
}
