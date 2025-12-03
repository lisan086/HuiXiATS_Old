using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UC
{
    public partial class TianCanShuKJ : UserControl
    {
        public TianCanShuKJ()
        {
            InitializeComponent();
        }

        public string BiaoTi
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public void SetCanShu(string shuju)
        { 
            this.textBox1.Text = shuju;
        }

        public string GetCanShu()
        { 
            return this.textBox1.Text;
        }
    }
}
