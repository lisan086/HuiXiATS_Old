
namespace JieMianLei.FuFrom.KJ
{
    partial class BiaoGeFenYeKJ
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.heJiKJ1 = new JieMianLei.UC.HeJiKJ();
            this.daoHangKJ1 = new JieMianLei.UC.DaoHangKJ();
            this.dataGridView1 = new JieMianLei.FuFrom.KJ.JiHeDataGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.heJiKJ1);
            this.panel1.Controls.Add(this.daoHangKJ1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 440);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 45);
            this.panel1.TabIndex = 5;
            // 
            // heJiKJ1
            // 
            this.heJiKJ1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.heJiKJ1.Location = new System.Drawing.Point(463, 0);
            this.heJiKJ1.Name = "heJiKJ1";
            this.heJiKJ1.Size = new System.Drawing.Size(209, 43);
            this.heJiKJ1.TabIndex = 1;
            // 
            // daoHangKJ1
            // 
            this.daoHangKJ1.AllPage = 1;
            this.daoHangKJ1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.daoHangKJ1.Dock = System.Windows.Forms.DockStyle.Left;
            this.daoHangKJ1.EndBtnEnabled = false;
            this.daoHangKJ1.Location = new System.Drawing.Point(0, 0);
            this.daoHangKJ1.Name = "daoHangKJ1";
            this.daoHangKJ1.NextBtnEnabled = false;
            this.daoHangKJ1.Rows = 200;
            this.daoHangKJ1.Size = new System.Drawing.Size(463, 43);
            this.daoHangKJ1.TabIndex = 0;
            this.daoHangKJ1.TopBtnEnabled = false;
            this.daoHangKJ1.UpBtnEnabled = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(674, 440);
            this.dataGridView1.TabIndex = 6;
            // 
            // BiaoGeFenYeKJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "BiaoGeFenYeKJ";
            this.Size = new System.Drawing.Size(674, 485);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UC.HeJiKJ heJiKJ1;
        private UC.DaoHangKJ daoHangKJ1;
        private JiHeDataGrid dataGridView1;
    }
}
