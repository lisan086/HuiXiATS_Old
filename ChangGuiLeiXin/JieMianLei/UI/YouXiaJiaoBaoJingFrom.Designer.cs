
namespace BaseUI.UI
{
    partial class YouXiaJiaoBaoJingFrom
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucCaoZuoBtn1 = new JieMianLei.UC.UCCaoZuoBtn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buKaPanl1 = new BaseUI.UC.BuKaPanl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.ucCaoZuoBtn1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 40);
            this.panel1.TabIndex = 0;
            // 
            // ucCaoZuoBtn1
            // 
            this.ucCaoZuoBtn1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucCaoZuoBtn1.FlatAppearance.BorderSize = 0;
            this.ucCaoZuoBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ucCaoZuoBtn1.GouKouCha = JieMianLei.UC.GouKouCha.GuanBi;
            this.ucCaoZuoBtn1.Location = new System.Drawing.Point(558, 0);
            this.ucCaoZuoBtn1.Name = "ucCaoZuoBtn1";
            this.ucCaoZuoBtn1.Size = new System.Drawing.Size(38, 36);
            this.ucCaoZuoBtn1.TabIndex = 0;
            this.ucCaoZuoBtn1.UseVisualStyleBackColor = true;
            this.ucCaoZuoBtn1.ZColor = System.Drawing.Color.Black;
            this.ucCaoZuoBtn1.Click += new System.EventHandler(this.ucCaoZuoBtn1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buKaPanl1
            // 
            this.buKaPanl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buKaPanl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buKaPanl1.Location = new System.Drawing.Point(0, 40);
            this.buKaPanl1.Name = "buKaPanl1";
            this.buKaPanl1.Size = new System.Drawing.Size(600, 446);
            this.buKaPanl1.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(400, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 25);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "下次不要提醒";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // YouXiaJiaoBaoJingFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 486);
            this.Controls.Add(this.buKaPanl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "YouXiaJiaoBaoJingFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "YouXiaJiaoBaoJingFrom";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private JieMianLei.UC.UCCaoZuoBtn ucCaoZuoBtn1;
        private System.Windows.Forms.Timer timer1;
        private UC.BuKaPanl buKaPanl1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}