namespace TestFrom
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ucJiLvContor1 = new BaseUI.UC.UCJiLvContor();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.ucFpanl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).BeginInit();
            this.ucPanL1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFpanl1
            // 
            this.ucFpanl1.Size = new System.Drawing.Size(936, 43);
            // 
            // ucPanL1
            // 
            this.ucPanL1.Controls.Add(this.button6);
            this.ucPanL1.Controls.Add(this.button5);
            this.ucPanL1.Controls.Add(this.button4);
            this.ucPanL1.Controls.Add(this.button3);
            this.ucPanL1.Controls.Add(this.label1);
            this.ucPanL1.Controls.Add(this.ucJiLvContor1);
            this.ucPanL1.Controls.Add(this.button2);
            this.ucPanL1.Controls.Add(this.button1);
            this.ucPanL1.Size = new System.Drawing.Size(936, 531);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(750, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 130);
            this.button1.TabIndex = 1;
            this.button1.Text = "注册";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(636, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(750, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 130);
            this.button2.TabIndex = 2;
            this.button2.Text = "分析";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ucJiLvContor1
            // 
            this.ucJiLvContor1.ChangDuXianZhi = 5;
            this.ucJiLvContor1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucJiLvContor1.IsZhiDu = true;
            this.ucJiLvContor1.Location = new System.Drawing.Point(0, 0);
            this.ucJiLvContor1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucJiLvContor1.Name = "ucJiLvContor1";
            this.ucJiLvContor1.Size = new System.Drawing.Size(468, 529);
            this.ucJiLvContor1.TabIndex = 3;
            this.ucJiLvContor1.YiChuCount = 2;
            this.ucJiLvContor1.ZiTi = null;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(524, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 65);
            this.button3.TabIndex = 15;
            this.button3.Text = "分析";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(539, 174);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(152, 65);
            this.button4.TabIndex = 16;
            this.button4.Text = "设计";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(539, 371);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(152, 65);
            this.button5.TabIndex = 17;
            this.button5.Text = "分析";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(735, 371);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(152, 65);
            this.button6.TabIndex = 18;
            this.button6.Text = "分析";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 574);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ucFpanl1.ResumeLayout(false);
            this.ucFpanl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).EndInit();
            this.ucPanL1.ResumeLayout(false);
            this.ucPanL1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private BaseUI.UC.UCJiLvContor ucJiLvContor1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

