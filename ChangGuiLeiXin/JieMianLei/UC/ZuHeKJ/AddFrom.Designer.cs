
namespace BaseUI.UC.ZuHeKJ
{
    partial class AddFrom
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
            this.pans1 = new BaseUI.UC.ZuHeKJ.Pans();
            this.panel2 = new BaseUI.UC.ZuHeKJ.XinPanL();
            this.SuspendLayout();
            // 
            // pans1
            // 
            this.pans1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pans1.Location = new System.Drawing.Point(0, 0);
            this.pans1.Name = "pans1";
            this.pans1.Size = new System.Drawing.Size(320, 39);
            this.pans1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(320, 273);
            this.panel2.TabIndex = 1;
            // 
            // AddFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pans1);
            this.Name = "AddFrom";
            this.Size = new System.Drawing.Size(320, 312);
            this.ResumeLayout(false);

        }

        #endregion

        private Pans pans1;
        private XinPanL panel2;
    }
}
