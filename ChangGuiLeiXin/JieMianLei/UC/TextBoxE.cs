using BaseUI.UI;
using JieMianLei.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.UC
{
    public class TextBoxE:TextBox
    {
      
        private JianPanType JianPanType1 = JianPanType.NO;

        public JianPanType JianPan
        {
            get
            {
                return JianPanType1;
            }

            set
            {
                JianPanType1 = value;
            }
        }

      

        public TextBoxE() : base()
        {
            this.DoubleBuffered = true;
            this.Click += TextBoxE_Click;
        }

        private void TextBoxE_Click(object sender, EventArgs e)
        {

            switch (JianPanType1)
            {
                case JianPanType.ShuZhi:
                    {
                        ShuZiJianPanFrom uIShuZhiJianPanFrom = new ShuZiJianPanFrom();
                        if (this.PasswordChar == '\0')
                        {
                            uIShuZhiJianPanFrom.SetCanShu(this.Text);
                        }
                        else
                        {
                            uIShuZhiJianPanFrom.SetCanShu(this.Text);
                        }
                        if (uIShuZhiJianPanFrom.ShowDialog(this) == DialogResult.OK)
                        {
                            this.Text = uIShuZhiJianPanFrom.ShuZhi;
                        }
                    }
                    break;
                case JianPanType.PingYing:
                    {
                        QuanBuJianPanFrom uIShuZhiJianPanFrom = new QuanBuJianPanFrom();
                        if (this.PasswordChar == '\0')
                        {
                            uIShuZhiJianPanFrom.SetCanShu(this.Text);
                        }
                        else
                        {
                            uIShuZhiJianPanFrom.SetCanShu(this.Text, true);
                        }
                        if (uIShuZhiJianPanFrom.ShowDialog(this) == DialogResult.OK)
                        {
                            this.Text = uIShuZhiJianPanFrom.GetstrNeiRong;
                        }
                    }
                    break;
                case JianPanType.ShouXie:
                    {
                        ShouXieJianPanFrom uIShuZhiJianPanFrom = new ShouXieJianPanFrom();
                        if (this.PasswordChar == '\0')
                        {
                            uIShuZhiJianPanFrom.SetCanShu(this.Text);
                        }
                        else
                        {
                            uIShuZhiJianPanFrom.SetCanShu(this.Text, true);
                        }
                        if (uIShuZhiJianPanFrom.ShowDialog(this) == DialogResult.OK)
                        {
                            this.Text = uIShuZhiJianPanFrom.FullCACText;
                        }
                    }
                    break;
                case JianPanType.NO:
                    {
                        
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public enum JianPanType
    {
        ShuZhi = 0,
        PingYing = 1,
        NO = 2,
        ShouXie=3,
    }
}
