namespace BaseUI.FuFrom.XinWeiHuFrm
{
    partial class ChaoZhaoFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ucFpanl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).BeginInit();
            this.ucPanL1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFpanl1
            // 
            this.ucFpanl1.Size = new System.Drawing.Size(717, 43);
            // 
            // labFbiaoTi
            // 
            this.labFbiaoTi.Size = new System.Drawing.Size(52, 27);
            this.labFbiaoTi.Text = "查找";
            // 
            // ucPanL1
            // 
            this.ucPanL1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPanL1.Controls.Add(this.button1);
            this.ucPanL1.Controls.Add(this.textBox1);
            this.ucPanL1.Controls.Add(this.label1);
            this.ucPanL1.Size = new System.Drawing.Size(717, 443);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(62, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(274, 162);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(298, 34);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(423, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 69);
            this.button1.TabIndex = 2;
            this.button1.Text = "查找";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChaoZhaoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 486);
            this.Name = "ChaoZhaoFrm";
            this.Text = "ChaoZhaoFrm";
            this.ucFpanl1.ResumeLayout(false);
            this.ucFpanl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).EndInit();
            this.ucPanL1.ResumeLayout(false);
            this.ucPanL1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}