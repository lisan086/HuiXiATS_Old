
namespace JieMianLei.FuFrom
{
    partial class MiMaFrom
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxE1 = new JieMianLei.UC.TextBoxE();
            this.ucFpanl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).BeginInit();
            this.ucPanL1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFpanl1
            // 
            this.ucFpanl1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucFpanl1.Size = new System.Drawing.Size(645, 43);
            // 
            // labFbiaoTi
            // 
            this.labFbiaoTi.Location = new System.Drawing.Point(51, 8);
            this.labFbiaoTi.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labFbiaoTi.Size = new System.Drawing.Size(92, 27);
            this.labFbiaoTi.Text = "输入密码";
            // 
            // ucPanL1
            // 
            this.ucPanL1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPanL1.Controls.Add(this.textBoxE1);
            this.ucPanL1.Controls.Add(this.label1);
            this.ucPanL1.Controls.Add(this.button1);
            this.ucPanL1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucPanL1.Size = new System.Drawing.Size(645, 336);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "密码:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(428, 192);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 59);
            this.button1.TabIndex = 2;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxE1
            // 
            this.textBoxE1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxE1.JianPan = JieMianLei.UC.JianPanType.NO;
            this.textBoxE1.Location = new System.Drawing.Point(180, 82);
            this.textBoxE1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxE1.Name = "textBoxE1";
            this.textBoxE1.Size = new System.Drawing.Size(395, 34);
            this.textBoxE1.TabIndex = 4;
            // 
            // MiMaFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 379);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "MiMaFrom";
            this.Text = "MiMaFrom";
            this.ucFpanl1.ResumeLayout(false);
            this.ucFpanl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFico)).EndInit();
            this.ucPanL1.ResumeLayout(false);
            this.ucPanL1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UC.TextBoxE textBoxE1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}