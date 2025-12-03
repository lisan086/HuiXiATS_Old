using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommLei.JiChuLei;

namespace JieMianLei.UC
{
    public class CommBoxE:ComboBox
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommBoxE()
        {
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软姚黑", 12);
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x020A)
            {

            }
            else
            {
                base.WndProc(ref m);
            }
        }


        public void SetCanShu<T>() where T :Enum
        {
            this.Items.Clear();
            List<string> strings = ChangYong.MeiJuLisName(typeof(T));
            for (int i = 0; i < strings.Count; i++)
            {
                this.Items.Add(strings[i]);
            }
            if (this.Items.Count>0)
            {
                this.SelectedIndex = 0;
            }
        }

        public T GetCanShu<T>() where T : Enum
        {
            return ChangYong.GetMeiJuZhi<T>(this.Text);
        }

        public void SetCanShu(List<string> data,string yuanzhi,string fengefu)
        {
          
            for (int i = 0; i < data.Count; i++)
            {
                this.Items.Add(data[i]);
            }
            
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }
            if (string.IsNullOrEmpty(fengefu))
            {
                this.Text = yuanzhi;
            }
            else
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    string[] fenss = this.Items[i].ToString().Split(new string[] { fengefu },StringSplitOptions.None);
                    if (fenss[0].Equals(yuanzhi))
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
