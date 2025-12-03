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
    public partial class UCJiLvContor : UserControl
    {
        private RichTextBoxN RichTextBoxN;
        public UCJiLvContor()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            RichTextBoxN = new RichTextBoxN();
            RichTextBoxN.Dock = DockStyle.Fill;
            this.Controls.Add(RichTextBoxN);
        }
        private bool _IsZhiDu = true;
        private int _ChangDuXianZhi = 150;
        private int _YiChuCount = 30;

        private readonly object Suo = new object();
        private Font _ZiTi = null;
        public bool IsZhiDu
        {
            get
            {
                return _IsZhiDu;
            }

            set
            {
                _IsZhiDu = value;
                if (RichTextBoxN!=null)
                {
                    RichTextBoxN.ReadOnly = _IsZhiDu;
                }
            }
        }

        public Font ZiTi
        {
            get
            {
                return _ZiTi;
            }

            set
            {
                _ZiTi = value;
                if (RichTextBoxN != null)
                {
                    RichTextBoxN.Font = _ZiTi;
                }
            }
        }

        public int ChangDuXianZhi
        {
            get
            {
                return _ChangDuXianZhi;
            }

            set
            {
                _ChangDuXianZhi = value;
            }
        }

        public int YiChuCount
        {
            get
            {
                return _YiChuCount;
            }

            set
            {
                _YiChuCount = value;
            }
        }

        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, string text)
        {
            if (RichTextBoxN.Lines.Length> _ChangDuXianZhi)
            {
                lock (Suo)
                {
                    if (RichTextBoxN.Lines.Length> _ChangDuXianZhi)
                    {
                        if (_YiChuCount >= _ChangDuXianZhi)
                        {
                            RichTextBoxN.Clear();
                          
                        }
                        else
                        {
                            RichTextBoxN.SelectionStart = 0;
                            RichTextBoxN.SelectionLength = RichTextBoxN.GetFirstCharIndexFromLine(_YiChuCount)+1;
                            RichTextBoxN.SelectedText = ".";
                            RichTextBoxN.Select(RichTextBoxN.Text.Length, 0);
                            RichTextBoxN.ScrollToCaret();

                        }
                    }
                }
            }
            RichTextBoxN.SelectionStart = RichTextBoxN.TextLength;
            RichTextBoxN.SelectionLength = 0;
            RichTextBoxN.SelectionColor = color;
            RichTextBoxN.AppendText($"{text}\n");
            RichTextBoxN.SelectionColor = RichTextBoxN.ForeColor;

          
            RichTextBoxN.ScrollToCaret();

        }

        public void Clear()
        {
            RichTextBoxN.Clear();
        }

    }

    internal class RichTextBoxN : RichTextBox
    {
        public RichTextBoxN() : base()
        {
            this.DoubleBuffered = true;
        }
    }
}
