using System.Windows.Forms;

namespace BaseUI.FuFrom.XinWeiHuFrm
{
    partial class JiChuKJWeiHuFrm<T, V> where V : new() where T : Control, IFUCKJ<V>, new()
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.uweiKuKJ1 = new JieMianLei.FuFrom.KJ.CaiDanKJ();
            this.fGenSui1 = new JieMianLei.FuFrom.KJ.FGenSui();
            this.ucDaoHangKongJian1 = new JieMianLei.UC.DaoHangKJ();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucFpanl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).BeginInit();
            this.ucPanL1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFpanl1
            // 
            this.ucFpanl1.Size = new System.Drawing.Size(928, 43);
            // 
            // labFbiaoTi
            // 
            this.labFbiaoTi.Size = new System.Drawing.Size(92, 27);
            this.labFbiaoTi.Text = "基础维护";
            // 
            // ucPanL1
            // 
            this.ucPanL1.Controls.Add(this.fGenSui1);
            this.ucPanL1.Controls.Add(this.panel2);
            this.ucPanL1.Controls.Add(this.panel1);
            this.ucPanL1.Size = new System.Drawing.Size(928, 631);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.uweiKuKJ1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 48);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(835, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 46);
            this.button1.TabIndex = 1;
            this.button1.Text = "排序";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // uweiKuKJ1
            // 
            this.uweiKuKJ1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uweiKuKJ1.Location = new System.Drawing.Point(0, 0);
            this.uweiKuKJ1.Margin = new System.Windows.Forms.Padding(5);
            this.uweiKuKJ1.Name = "uweiKuKJ1";
            this.uweiKuKJ1.Size = new System.Drawing.Size(797, 46);
            this.uweiKuKJ1.TabIndex = 0;
            // 
            // fGenSui1
            // 
            this.fGenSui1.AutoScroll = true;
            this.fGenSui1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fGenSui1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGenSui1.Location = new System.Drawing.Point(0, 48);
            this.fGenSui1.Name = "fGenSui1";
            this.fGenSui1.Size = new System.Drawing.Size(926, 535);
            this.fGenSui1.TabIndex = 4;
            this.fGenSui1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.fGenSui1_ControlRemoved);
            // 
            // ucDaoHangKongJian1
            // 
            this.ucDaoHangKongJian1.AllPage = 1;
            this.ucDaoHangKongJian1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDaoHangKongJian1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucDaoHangKongJian1.EndBtnEnabled = false;
            this.ucDaoHangKongJian1.Location = new System.Drawing.Point(0, 0);
            this.ucDaoHangKongJian1.Margin = new System.Windows.Forms.Padding(5);
            this.ucDaoHangKongJian1.Name = "ucDaoHangKongJian1";
            this.ucDaoHangKongJian1.NextBtnEnabled = false;
            this.ucDaoHangKongJian1.Rows = 200;
            this.ucDaoHangKongJian1.Size = new System.Drawing.Size(789, 46);
            this.ucDaoHangKongJian1.TabIndex = 5;
            this.ucDaoHangKongJian1.TopBtnEnabled = false;
            this.ucDaoHangKongJian1.UpBtnEnabled = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucDaoHangKongJian1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 583);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(926, 46);
            this.panel2.TabIndex = 6;
            // 
            // JiChuKJWeiHuFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 674);
            this.Name = "JiChuKJWeiHuFrm";
            this.Text = "JiChuKJWeiHuFrm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ucFpanl1.ResumeLayout(false);
            this.ucFpanl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).EndInit();
            this.ucPanL1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private JieMianLei.FuFrom.KJ.FGenSui fGenSui1;
        private Panel panel1;
        private JieMianLei.FuFrom.KJ.CaiDanKJ uweiKuKJ1;
        private Button button1;
        private JieMianLei.UC.DaoHangKJ ucDaoHangKongJian1;
        private Panel panel2;
    }
}