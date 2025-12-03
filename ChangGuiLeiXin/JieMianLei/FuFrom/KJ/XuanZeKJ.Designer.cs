
namespace JieMianLei.FuFrom.KJ
{
    partial class XuanZeKJ
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.weiKuKJ1 = new JieMianLei.FuFrom.KJ.CaiDanKJ();
            this.panel1 = new System.Windows.Forms.Panel();
            this.heJiKJ1 = new JieMianLei.UC.HeJiKJ();
            this.ucDaoHangKongJian1 = new JieMianLei.UC.DaoHangKJ();
            this.flowLayoutPanel1 = new JieMianLei.FuFrom.KJ.FGenSui();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.weiKuKJ1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(533, 38);
            this.panel2.TabIndex = 9;
            // 
            // weiKuKJ1
            // 
            this.weiKuKJ1.Dock = System.Windows.Forms.DockStyle.Left;
            this.weiKuKJ1.Location = new System.Drawing.Point(0, 0);
            this.weiKuKJ1.Name = "weiKuKJ1";
            this.weiKuKJ1.Size = new System.Drawing.Size(528, 36);
            this.weiKuKJ1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.heJiKJ1);
            this.panel1.Controls.Add(this.ucDaoHangKongJian1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 435);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 47);
            this.panel1.TabIndex = 8;
            // 
            // heJiKJ1
            // 
            this.heJiKJ1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.heJiKJ1.Location = new System.Drawing.Point(463, 0);
            this.heJiKJ1.Name = "heJiKJ1";
            this.heJiKJ1.Size = new System.Drawing.Size(68, 45);
            this.heJiKJ1.TabIndex = 3;
            // 
            // ucDaoHangKongJian1
            // 
            this.ucDaoHangKongJian1.AllPage = 1;
            this.ucDaoHangKongJian1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDaoHangKongJian1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucDaoHangKongJian1.EndBtnEnabled = false;
            this.ucDaoHangKongJian1.Location = new System.Drawing.Point(0, 0);
            this.ucDaoHangKongJian1.Name = "ucDaoHangKongJian1";
            this.ucDaoHangKongJian1.NextBtnEnabled = false;
            this.ucDaoHangKongJian1.Rows = 200;
            this.ucDaoHangKongJian1.Size = new System.Drawing.Size(463, 45);
            this.ucDaoHangKongJian1.TabIndex = 2;
            this.ucDaoHangKongJian1.TopBtnEnabled = false;
            this.ucDaoHangKongJian1.UpBtnEnabled = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 38);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(533, 397);
            this.flowLayoutPanel1.TabIndex = 10;
            this.flowLayoutPanel1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_ControlRemoved);
            // 
            // XuanZeKJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "XuanZeKJ";
            this.Size = new System.Drawing.Size(533, 482);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private CaiDanKJ weiKuKJ1;
        private System.Windows.Forms.Panel panel1;
        private UC.HeJiKJ heJiKJ1;
        private UC.DaoHangKJ ucDaoHangKongJian1;
        private FGenSui flowLayoutPanel1;
    }
}
