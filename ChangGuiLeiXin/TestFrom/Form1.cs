using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseUI.DaYIngMoBan.Frm;
using BaseUI.FuFrom.KJ;
using BaseUI.FuFrom.XinWeiHuFrm;
using CommLei.DataChuLi;
using CommLei.JiChuLei;
using Common.DataChuLi;
using JieMianLei.FuFrom;
using SSheBei.Model;
using SSheBei.PeiZhi;

namespace TestFrom
{
    public partial class Form1 : BaseFuFrom
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZhuCeMaFrom zhuCeMaFrom = new ZhuCeMaFrom();
            zhuCeMaFrom.Show(this);
        }
        /// <summary>
        /// true  表示过期
        /// </summary>
        private bool IsGuoQi
        {
            get { return ZhuCheLei.Ceratei().GuoQi == 1; }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsGuoQi)
            {
                this.label1.Text = "设备过期";

            }
            else
            {
                this.label1.Text = "设备未过期";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> wenjian = new List<string>();
            wenjian.Add(@"tx: (4 bytes)
 01 09 10 00
rx: (13 bytes)
 04 0e 0a 01 09 10 00 38 bb b7 82 c6 2c
[MBT_TRANSPORT: /dev/ttyHS2]
Init UART ..........
");
            wenjian.Add("adb shell /data/mbt tx_test {0} 2442 4 1 15 339 9 0");
            string mac = "";
            for (int c = 0; c < wenjian.Count; c++)
            {

                if (c == 0)
                {
                  
                    string chazhao =ChangYong.StrDataCut(wenjian[c], "rx:", "[", 2).Replace("\r", "").Replace("\n", "");
                    string[] kongge = chazhao.Split(' ');
                    if (kongge.Length >= 6)
                    {
                        int count = kongge.Length;
                        mac = $"{kongge[count - 6]}{kongge[count - 5]}{kongge[count - 4]}{kongge[count - 3]}{kongge[count - 2]}{kongge[count - 1]}";
                    }
                  
                }
                else
                {
                    string pingjie = string.Format(wenjian[c], mac);
                    MessageBox.Show(pingjie);
                }

                Thread.Sleep(1000);
            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            float Value = BitConverter.ToInt32(new byte[] { 0x00, 0xFF, 0xFF, 0xFF }, 0);
            //int ransuoji = new SuiJiShuLei().SuiJiData(0,2);
            //if (ransuoji == 1)
            //{
            //    this.ucJiLvContor1.LogAppend(Color.Red, "我是1 我是红色，好了吗");
            //}
            //else
            //{
            //    this.ucJiLvContor1.LogAppend(Color.Green, "我是0 我是绿色，好了吗");
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DaFromN frm = new DaFromN();
            frm.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            JiChuKJWeiHuFrm<PeiZhiKJ, JiaZaiSheBeiModel> frm = new JiChuKJWeiHuFrm<PeiZhiKJ, JiaZaiSheBeiModel>();
            frm.IsZhiXianShiX = true;
            frm.SetRowCount(2);
            frm.SetCanShu("设置设备", HCLisDataLei<JiaZaiSheBeiModel>.Ceratei().LisWuLiao);
            frm.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
           string LuJing = Path.GetDirectoryName(@"D:\ruanjian\geek.exe");

           List<string> shuju= getAllNum("-2.20nF");
        }

        private  List<string> getAllNum(string str)
        {
            List<String> _list = new List<string>();

            if (!String.IsNullOrEmpty(str))
            {
                // String pattern = @"\d*\.\d*|0\.\d*[1-9]\d*$";//^[+-]?\d*[.]?\d*$
                String pattern = @"[+-]?\d*[.]?\d*$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                MatchCollection matchs = regex.Matches(str);
                foreach (Match m in matchs)
                {
                    _list.Add(m.Groups[0].Value);
                }
                str = Regex.Replace(str, pattern, "");

                String pattern2 = @"[0-9]+";
                Regex regex2 = new Regex(pattern2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                MatchCollection matchs2 = regex2.Matches(str);
                foreach (Match m in matchs2)
                {
                    _list.Add(m.Groups[0].Value);
                }
            }

            return _list;
        }

    }
}
