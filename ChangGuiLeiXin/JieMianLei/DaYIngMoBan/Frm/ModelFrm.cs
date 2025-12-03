using JieMianLei.FuFrom;
using JieMianLei.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.DaYIngMoBan.Frm
{
    public partial class ModelFrm : BaseFuFrom
    {
        public ModelFrm()
        {
            InitializeComponent();
            
        }

        public void SetCanShu(List<string> ziduan,List<string> mingxi)
        {
            {
                StringBuilder Builder = new StringBuilder();

                Builder.AppendLine(string.Format("public class TestModel"));
                Builder.AppendLine("{");
                for (int i = 0; i < ziduan.Count; i++)
                {
                    Builder.AppendLine(string.Format("public string {2} {0}get;set;{1}", "{", "}", ziduan[i]));
                }

                Builder.AppendLine("}");
                this.textBox1.AppendText(Builder.ToString());
                this.textBox1.AppendText("\r\n");
                this.textBox1.AppendText("\r\n");
                this.textBox1.AppendText("\r\n");
            }
            {
                if (mingxi.Count > 0)
                {
                    StringBuilder Builder = new StringBuilder();

                    Builder.AppendLine(string.Format("public class TestMxModel"));
                    Builder.AppendLine("{");
                    for (int i = 0; i < mingxi.Count; i++)
                    {
                        Builder.AppendLine(string.Format("public string {2} {0}get;set;{1}", "{", "}", mingxi[i]));
                    }

                    Builder.AppendLine("}");
                    this.textBox1.AppendText(Builder.ToString());
                    this.textBox1.AppendText("\r\n");
                    this.textBox1.AppendText("\r\n");
                    this.textBox1.AppendText("\r\n");
                }
            }
        }
    }
}
