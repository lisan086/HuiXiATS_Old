using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.JieMianLei
{
    /// <summary>
    /// 按下enter进入下一空件
    /// </summary>
    public class AnEnterJinRuXia
    {
        /// <summary>
        /// 绑定按下到另外一个控件
        /// </summary>
        public void BangDingAnXiaToKongJian(Control danqiankongjian, Control xiakongjian)
        {
            danqiankongjian.Tag = xiakongjian;
            danqiankongjian.KeyDown += danqiankongjian_KeyDown;
        }

        void danqiankongjian_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (sender is Control)
                {
                    Control kongjian = sender as Control;
                    if (kongjian.Tag != null)
                    {
                        if (kongjian.Tag is Control)
                        {
                            Control kongjian1 = kongjian.Tag as Control;
                            if (kongjian1 is TextBox)
                            {
                                TextBox texb = kongjian1 as TextBox;
                                texb.Focus();
                                texb.SelectAll();
                            }
                            else
                            {
                                kongjian1.Focus();
                            }

                        }

                    }

                }
            }
        }
    }
}
