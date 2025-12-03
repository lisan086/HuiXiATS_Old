
namespace JieMianLei.FuFrom
{
    partial class BaseFuFrom
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
            this.ucFpanl1 = new JieMianLei.UC.UCPanL();
            this.Fpancaozuo = new System.Windows.Forms.Panel();
            this.ucFzuixiao = new JieMianLei.UC.UCCaoZuoBtn();
            this.ucFzuida = new JieMianLei.UC.UCCaoZuoBtn();
            this.ucFguanbi = new JieMianLei.UC.UCCaoZuoBtn();
            this.labFbiaoTi = new System.Windows.Forms.Label();
            this.picFico = new System.Windows.Forms.PictureBox();
            this.ucPanL1 = new JieMianLei.UC.UCPanL();
            this.ucFpanl1.SuspendLayout();
            this.Fpancaozuo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).BeginInit();
            this.SuspendLayout();
            // 
            // ucFpanl1
            // 
            this.ucFpanl1.BackColor = System.Drawing.Color.AliceBlue;
            this.ucFpanl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucFpanl1.Controls.Add(this.Fpancaozuo);
            this.ucFpanl1.Controls.Add(this.labFbiaoTi);
            this.ucFpanl1.Controls.Add(this.picFico);
            this.ucFpanl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFpanl1.FColor = System.Drawing.Color.Transparent;
            this.ucFpanl1.IsKongZhi = false;
            this.ucFpanl1.Location = new System.Drawing.Point(0, 0);
            this.ucFpanl1.Margin = new System.Windows.Forms.Padding(4);
            this.ucFpanl1.Name = "ucFpanl1";
            this.ucFpanl1.Size = new System.Drawing.Size(1067, 43);
            this.ucFpanl1.TabIndex = 0;
            this.ucFpanl1.TColor = System.Drawing.Color.PaleGoldenrod;
            // 
            // Fpancaozuo
            // 
            this.Fpancaozuo.BackColor = System.Drawing.Color.Transparent;
            this.Fpancaozuo.Controls.Add(this.ucFzuixiao);
            this.Fpancaozuo.Controls.Add(this.ucFzuida);
            this.Fpancaozuo.Controls.Add(this.ucFguanbi);
            this.Fpancaozuo.Dock = System.Windows.Forms.DockStyle.Right;
            this.Fpancaozuo.Location = new System.Drawing.Point(916, 0);
            this.Fpancaozuo.Margin = new System.Windows.Forms.Padding(4);
            this.Fpancaozuo.Name = "Fpancaozuo";
            this.Fpancaozuo.Size = new System.Drawing.Size(149, 41);
            this.Fpancaozuo.TabIndex = 6;
            // 
            // ucFzuixiao
            // 
            this.ucFzuixiao.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucFzuixiao.FlatAppearance.BorderSize = 0;
            this.ucFzuixiao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.ucFzuixiao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ucFzuixiao.GouKouCha = JieMianLei.UC.GouKouCha.ZuiXiao;
            this.ucFzuixiao.Location = new System.Drawing.Point(8, 0);
            this.ucFzuixiao.Margin = new System.Windows.Forms.Padding(4);
            this.ucFzuixiao.Name = "ucFzuixiao";
            this.ucFzuixiao.Size = new System.Drawing.Size(47, 41);
            this.ucFzuixiao.TabIndex = 0;
            this.ucFzuixiao.UseVisualStyleBackColor = true;
            this.ucFzuixiao.ZColor = System.Drawing.Color.Black;
            this.ucFzuixiao.Click += new System.EventHandler(this.ucFzuixiao_Click);
            // 
            // ucFzuida
            // 
            this.ucFzuida.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucFzuida.FlatAppearance.BorderSize = 0;
            this.ucFzuida.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.ucFzuida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ucFzuida.GouKouCha = JieMianLei.UC.GouKouCha.ZuiDa;
            this.ucFzuida.Location = new System.Drawing.Point(55, 0);
            this.ucFzuida.Margin = new System.Windows.Forms.Padding(4);
            this.ucFzuida.Name = "ucFzuida";
            this.ucFzuida.Size = new System.Drawing.Size(47, 41);
            this.ucFzuida.TabIndex = 1;
            this.ucFzuida.UseVisualStyleBackColor = true;
            this.ucFzuida.ZColor = System.Drawing.Color.Black;
            this.ucFzuida.Click += new System.EventHandler(this.ucFzuida_Click);
            // 
            // ucFguanbi
            // 
            this.ucFguanbi.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucFguanbi.FlatAppearance.BorderSize = 0;
            this.ucFguanbi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.ucFguanbi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ucFguanbi.GouKouCha = JieMianLei.UC.GouKouCha.GuanBi;
            this.ucFguanbi.Location = new System.Drawing.Point(102, 0);
            this.ucFguanbi.Margin = new System.Windows.Forms.Padding(4);
            this.ucFguanbi.Name = "ucFguanbi";
            this.ucFguanbi.Size = new System.Drawing.Size(47, 41);
            this.ucFguanbi.TabIndex = 2;
            this.ucFguanbi.UseVisualStyleBackColor = true;
            this.ucFguanbi.ZColor = System.Drawing.Color.Black;
            this.ucFguanbi.Click += new System.EventHandler(this.ucFguanbi_Click);
            // 
            // labFbiaoTi
            // 
            this.labFbiaoTi.AutoSize = true;
            this.labFbiaoTi.BackColor = System.Drawing.Color.Transparent;
            this.labFbiaoTi.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFbiaoTi.Location = new System.Drawing.Point(48, 8);
            this.labFbiaoTi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFbiaoTi.Name = "labFbiaoTi";
            this.labFbiaoTi.Size = new System.Drawing.Size(78, 27);
            this.labFbiaoTi.TabIndex = 5;
            this.labFbiaoTi.Text = "XX公司";
            this.labFbiaoTi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picFico
            // 
            this.picFico.BackColor = System.Drawing.Color.Transparent;
            this.picFico.Dock = System.Windows.Forms.DockStyle.Left;
            this.picFico.Location = new System.Drawing.Point(0, 0);
            this.picFico.Margin = new System.Windows.Forms.Padding(4);
            this.picFico.Name = "picFico";
            this.picFico.Size = new System.Drawing.Size(40, 41);
            this.picFico.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFico.TabIndex = 4;
            this.picFico.TabStop = false;
            // 
            // ucPanL1
            // 
            this.ucPanL1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucPanL1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPanL1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPanL1.FColor = System.Drawing.Color.Transparent;
            this.ucPanL1.IsKongZhi = false;
            this.ucPanL1.Location = new System.Drawing.Point(0, 43);
            this.ucPanL1.Margin = new System.Windows.Forms.Padding(4);
            this.ucPanL1.Name = "ucPanL1";
            this.ucPanL1.Size = new System.Drawing.Size(1067, 707);
            this.ucPanL1.TabIndex = 2;
            this.ucPanL1.TColor = System.Drawing.Color.Transparent;
            // 
            // BaseFuFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 750);
            this.Controls.Add(this.ucPanL1);
            this.Controls.Add(this.ucFpanl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "BaseFuFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseFuFrom";
            this.ucFpanl1.ResumeLayout(false);
            this.ucFpanl1.PerformLayout();
            this.Fpancaozuo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected UC.UCPanL ucFpanl1;
        private System.Windows.Forms.Panel Fpancaozuo;
        private UC.UCCaoZuoBtn ucFguanbi;
        private UC.UCCaoZuoBtn ucFzuida;
        private UC.UCCaoZuoBtn ucFzuixiao;
        protected System.Windows.Forms.PictureBox picFico;
        protected System.Windows.Forms.Label labFbiaoTi;
        protected UC.UCPanL ucPanL1;
    }
}